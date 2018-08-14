using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A helper class which is internally being used to
    /// animate properties of brushes.
    /// </summary>
    internal class OldBrushAnimationHelper
    {

        private OldBrushAnimation _brushAnimation;
        private Lazy<BrushAnimationToDoubleAnimationMapper> _doubleAnimationMapperLazy;
        private Lazy<BrushAnimationToColorAnimationMapper> _colorAnimationMapperLazy;

        public OldBrushAnimationHelper(OldBrushAnimation brushAnimation)
        {
            _brushAnimation = brushAnimation ?? throw new ArgumentNullException(nameof(brushAnimation));
            _doubleAnimationMapperLazy = new Lazy<BrushAnimationToDoubleAnimationMapper>(() =>
                new BrushAnimationToDoubleAnimationMapper(_brushAnimation));
            _colorAnimationMapperLazy = new Lazy<BrushAnimationToColorAnimationMapper>(() =>
                new BrushAnimationToColorAnimationMapper(_brushAnimation));
        }

        public double GetCurrentDouble(double origin, double destination, AnimationClock animationClock)
        {
            return _doubleAnimationMapperLazy.Value.GetCurrentValue(origin, destination, animationClock);
        }

        public Color GetCurrentColor(Color origin, Color destination, AnimationClock animationClock)
        {
            return _colorAnimationMapperLazy.Value.GetCurrentValue(origin, destination, animationClock);
        }

        public Point GetCurrentPoint(Point origin, Point destination, AnimationClock animationClock)
        {
            double x = this.GetCurrentDouble(origin.X, destination.X, animationClock);
            double y = this.GetCurrentDouble(origin.Y, destination.Y, animationClock);
            return new Point(x, y);
        }
        
    }

    /// <summary>
    /// A helper class which is designed for the <see cref="OldGradientBrushAnimation"/> classes.
    /// In addition to the default helper functions, it provides a caching mechanism and helper functions
    /// for the gradient stops to be animated.
    /// </summary>
    internal class GradientBrushAnimationHelper : OldBrushAnimationHelper
    {

        // The main task of this class is to manage a single cached gradient stop collection,
        // so that no new one needs to be created in each animation frame.
        private GradientStopCollection _currentGradientStops;

        public GradientBrushAnimationHelper(OldGradientBrushAnimation brushAnimation)
            : base(brushAnimation) { }

        private void InitializeGradientStops(int count)
        {
            if (_currentGradientStops == null || _currentGradientStops.Count != count)
            {
                _currentGradientStops = new GradientStopCollection(count);
                for (int i = 0; i < count; i++)
                {
                    _currentGradientStops.Add(new GradientStop());
                }
            }
        }

        public GradientStopCollection GetCurrentGradientStops(
            GradientStopCollection origin, GradientStopCollection destination, AnimationClock animationClock)
        {
            if (origin == null) throw new ArgumentNullException(nameof(origin));
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            
            // Only animate Min(origin, dest) gradient stops, to avoid IndexOutOfBounds.
            // In theory, the two collections should always have the same size,
            // but if someone overrides something (e.g. validation), this could potentially be required.
            int stopsToAnimate = Math.Min(origin.Count, destination.Count);

            this.InitializeGradientStops(stopsToAnimate);
            for (int i = 0; i < stopsToAnimate; i++)
            {
                this.UpdateGradientStopAtIndex(i, origin[i], destination[i], animationClock);
            }

            return _currentGradientStops;
        }

        private void UpdateGradientStopAtIndex(
            int index, GradientStop origin, GradientStop destination, AnimationClock animationClock)
        {
            GradientStop current = _currentGradientStops[index];
            current.Color = this.GetCurrentColor(origin.Color, destination.Color, animationClock);
            current.Offset = this.GetCurrentDouble(origin.Offset, destination.Offset, animationClock);
        }

    }

    /// <summary>
    ///     Used to delegate the animation of a Brush's property (like Opacity, Color, ...)
    ///     down to a WPF native animation, like DoubleAnimation, ColorAnimation, ...
    ///     This class is created, so that properties like BrushAnimation.EasingFunction
    ///     are automatically copied to the WPF animation, so that the results are as expected.
    ///     
    ///     This base class takes care of the general logic of when to copy values.
    ///     The derivations call the WPF animation's values and do the actual copying.
    /// </summary>
    /// <typeparam name="TAnimation">
    ///     The type of the other animation.
    /// </typeparam>
    /// <typeparam name="TAnimated">
    ///     The type which is being animated by the other animation.
    /// </typeparam>
    internal abstract class BrushAnimationMapper<TAnimation, TAnimated>
        where TAnimation : AnimationTimeline
    {

        private bool _copiedFrozenValues;
        private OldBrushAnimation _brushAnimation;
        private TAnimation _animation;

        public BrushAnimationMapper(OldBrushAnimation brushAnimation, Func<TAnimation> animationFactory)
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
            OldBrushAnimation brushAnimation, TAnimation targetAnimation);

    }

    internal sealed class BrushAnimationToColorAnimationMapper : BrushAnimationMapper<ColorAnimation, Color>
    {
        public BrushAnimationToColorAnimationMapper(OldBrushAnimation brushAnimation)
            : base(brushAnimation, () => new ColorAnimation()) { }

        protected override void MapBrushAnimationValues(OldBrushAnimation brushAnim, ColorAnimation colorAnim)
        {
            colorAnim.EasingFunction = brushAnim.EasingFunction;
            colorAnim.IsAdditive = brushAnim.IsAdditive;
            colorAnim.IsCumulative = brushAnim.IsCumulative;
            colorAnim.FillBehavior = brushAnim.FillBehavior;
        }
    }

    internal sealed class BrushAnimationToDoubleAnimationMapper : BrushAnimationMapper<DoubleAnimation, double>
    {
        public BrushAnimationToDoubleAnimationMapper(OldBrushAnimation brushAnimation)
            : base(brushAnimation, () => new DoubleAnimation()) { }

        protected override void MapBrushAnimationValues(OldBrushAnimation brushAnim, DoubleAnimation doubleAnim)
        {
            doubleAnim.EasingFunction = brushAnim.EasingFunction;
            doubleAnim.IsAdditive = brushAnim.IsAdditive;
            doubleAnim.IsCumulative = brushAnim.IsCumulative;
            doubleAnim.FillBehavior = brushAnim.FillBehavior;
        }
    }

}
