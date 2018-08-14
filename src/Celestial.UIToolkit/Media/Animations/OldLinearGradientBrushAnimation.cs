using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An animation which animates <see cref="LinearGradientBrush"/> objects.
    /// </summary>
    public class OldLinearGradientBrushAnimation : OldGradientBrushAnimation
    {

        private LinearGradientBrush _animatedBrush;
        private GradientBrushAnimationHelper _animationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OldLinearGradientBrushAnimation"/> class.
        /// </summary>
        public OldLinearGradientBrushAnimation()
        {
            _animationHelper = new GradientBrushAnimationHelper(this);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="OldLinearGradientBrushAnimation"/> class.
        /// </summary>
        /// <returns>A new <see cref="OldLinearGradientBrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new OldLinearGradientBrushAnimation();

        /// <summary>
        /// In addition to the validation performed by the <see cref="OldGradientBrushAnimation"/>,
        /// this method also ensures that the animated brushes are <see cref="LinearGradientBrush"/>
        /// instances.
        /// </summary>
        /// <param name="origin">he animation's origin.</param>
        /// <param name="destination">The animation's destination.</param>
        protected override void ValidateTimelineBrushesCore(Brush origin, Brush destination)
        {
            base.ValidateTimelineBrushesCore(origin, destination);
            this.ValidateThatBrushesAreLinear(origin, destination);
        }

        private void ValidateThatBrushesAreLinear(Brush origin, Brush destination)
        {
            if (origin.GetType() != typeof(LinearGradientBrush) ||
                destination.GetType() != typeof(LinearGradientBrush))
            {
                throw new InvalidOperationException(
                    $"The animation can only animate brushes of type {nameof(LinearGradientBrush)}.");
            }
        }

        /// <summary>
        /// Calculates the <see cref="LinearGradientBrush"/> which represents the current value of the animation.
        /// </summary>
        /// <param name="origin">
        /// The brush which serves as the animation's origin.
        /// </param>
        /// <param name="destination">
        /// The brush which serves as the animation's destination.
        /// </param>
        /// <param name="animationClock">
        ///     The <see cref="AnimationClock"/> to be used by the animation to generate its output value.
        /// </param>
        /// <returns>The <see cref="LinearGradientBrush"/> which this animation believes to be the current one.</returns>
        protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
        {
            var linearOrigin = (LinearGradientBrush)origin;
            var linearDestination = (LinearGradientBrush)destination;

            this.InitializeAnimatedBrush();
            this.SetCurrentOpacity(linearOrigin, linearDestination, animationClock);
            this.SetCurrentGradientStops(linearOrigin, linearDestination, animationClock);
            this.SetCurrentStartPoint(linearOrigin, linearDestination, animationClock);
            this.SetCurrentEndPoint(linearOrigin, linearDestination, animationClock);

            return _animatedBrush;
        }

        private void InitializeAnimatedBrush()
        {
            if (_animatedBrush == null || _animatedBrush.IsFrozen)
            {
                _animatedBrush = new LinearGradientBrush();
            }
        }

        private void SetCurrentOpacity(
            LinearGradientBrush origin, LinearGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.Opacity = _animationHelper.GetCurrentDouble(
                origin.Opacity, destination.Opacity, animationClock);
        }

        private void SetCurrentGradientStops(
            LinearGradientBrush origin, LinearGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.GradientStops = _animationHelper.GetCurrentGradientStops(
                origin.GradientStops, destination.GradientStops, animationClock);
        }

        private void SetCurrentStartPoint(
            LinearGradientBrush origin, LinearGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.StartPoint = _animationHelper.GetCurrentPoint(
                origin.StartPoint, destination.StartPoint, animationClock);
        }

        private void SetCurrentEndPoint(
            LinearGradientBrush origin, LinearGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.EndPoint = _animationHelper.GetCurrentPoint(
                origin.EndPoint, destination.EndPoint, animationClock);
        }

    }

}
