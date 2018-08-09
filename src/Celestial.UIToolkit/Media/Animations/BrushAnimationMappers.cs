using System;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    ///     Used internally by the <see cref="BrushAnimationBase"/> and its derivations.
    /// 
    ///     Serves as a base class for a mapper, which maps the properties of a
    ///     <see cref="BrushAnimationBase"/> to another animation.
    /// </summary>
    /// <typeparam name="TAnimation">
    ///     The type of the other animation.
    /// </typeparam>
    /// <typeparam name="TAnimated">
    ///     The type which is being animated by the other animation.
    /// </typeparam>
    /// <remarks>
    /// 
    ///     The mapping is done, because a brush consists of several components, e.g. Opacity,
    ///     Colors, ...
    ///     Each of these components can be animated by one of WPF's animations.
    ///     As a result, the brush animations simply delegate the animation down.
    ///     Hence, these mappers are created.
    ///     
    /// </remarks>
    internal abstract class BrushAnimationMapper<TAnimation, TAnimated> 
        where TAnimation : AnimationTimeline
    {

        private bool _copiedFrozenValues;
        private BrushAnimationBase _brushAnimation;
        private TAnimation _animation;

        public BrushAnimationMapper(BrushAnimationBase brushAnimation, Func<TAnimation> animationFactory)
        {
            if (brushAnimation == null) throw new ArgumentNullException(nameof(brushAnimation));
            if (animationFactory == null) throw new ArgumentNullException(nameof(animationFactory));

            _brushAnimation = brushAnimation;
            _animation = animationFactory() ?? throw new ArgumentException(
                "The animation factory must not return null.", nameof(animationFactory));
        }

        public TAnimated GetCurrentValue(TAnimated origin, TAnimated destination, AnimationClock animationClock)
        {
            this.UpdateAnimationProperties();
            return (TAnimated)_animation.GetCurrentValue(
                origin, destination, animationClock);
        }

        private void UpdateAnimationProperties()
        {
            // Only map the values, if:
            //   a) it hasn't been done yet.
            //   b) the values might have changed since the last time.
            //      (i.e. brush anim wasn't frozen the last time).
            if (_copiedFrozenValues) return;
            _copiedFrozenValues = _brushAnimation.IsFrozen;

            this.MapBrushAnimationValues(
                _brushAnimation, _animation);
        }

        protected abstract void MapBrushAnimationValues(
            BrushAnimationBase brushAnimation, TAnimation targetAnimation);

    }
    
    internal sealed class BrushAnimationToColorAnimationMapper : BrushAnimationMapper<ColorAnimation, Color>
    {
        public BrushAnimationToColorAnimationMapper(BrushAnimationBase brushAnimation) 
            : base(brushAnimation, () => new ColorAnimation()) { }

        protected override void MapBrushAnimationValues(BrushAnimationBase brushAnim, ColorAnimation colorAnim)
        {
            colorAnim.EasingFunction = brushAnim.EasingFunction;
            colorAnim.IsAdditive = brushAnim.IsAdditive;
            colorAnim.IsCumulative = brushAnim.IsCumulative;
            colorAnim.FillBehavior = brushAnim.FillBehavior;
        }
    }

    internal sealed class BrushAnimationToDoubleAnimationMapper : BrushAnimationMapper<DoubleAnimation, double>
    {
        public BrushAnimationToDoubleAnimationMapper(BrushAnimationBase brushAnimation) 
            : base(brushAnimation, () => new DoubleAnimation()) { }

        protected override void MapBrushAnimationValues(BrushAnimationBase brushAnim, DoubleAnimation doubleAnim)
        {
            doubleAnim.EasingFunction = brushAnim.EasingFunction;
            doubleAnim.IsAdditive = brushAnim.IsAdditive;
            doubleAnim.IsCumulative = brushAnim.IsCumulative;
            doubleAnim.FillBehavior = brushAnim.FillBehavior;
        }
    }

}
