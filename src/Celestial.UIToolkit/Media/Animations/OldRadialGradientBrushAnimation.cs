using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An animation which animates <see cref="RadialGradientBrush"/> objects.
    /// </summary>
    public class OldRadialGradientBrushAnimation : OldGradientBrushAnimation
    {

        private RadialGradientBrush _animatedBrush;
        private GradientBrushAnimationHelper _animationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OldRadialGradientBrushAnimation"/> class.
        /// </summary>
        public OldRadialGradientBrushAnimation()
        {
            _animationHelper = new GradientBrushAnimationHelper(this);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="OldRadialGradientBrushAnimation"/> class.
        /// </summary>
        /// <returns>A new <see cref="OldRadialGradientBrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new OldRadialGradientBrushAnimation();

        /// <summary>
        /// In addition to the validation performed by the <see cref="OldGradientBrushAnimation"/>,
        /// this method also ensures that the animated brushes are <see cref="RadialGradientBrush"/>
        /// instances.
        /// </summary>
        /// <param name="origin">he animation's origin.</param>
        /// <param name="destination">The animation's destination.</param>
        protected override void ValidateTimelineBrushesCore(Brush origin, Brush destination)
        {
            base.ValidateTimelineBrushesCore(origin, destination);
            this.ValidateThatBrushesAreRadial(origin, destination);
        }

        private void ValidateThatBrushesAreRadial(Brush origin, Brush destination)
        {
            if (origin.GetType() != typeof(RadialGradientBrush) ||
                destination.GetType() != typeof(RadialGradientBrush))
            {
                throw new InvalidOperationException(
                    $"The animation can only animate brushes of type {nameof(RadialGradientBrush)}.");
            }
        }

        /// <summary>
        /// Calculates the <see cref="RadialGradientBrush"/> which represents the current value of the animation.
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
        /// <returns>The <see cref="RadialGradientBrush"/> which this animation believes to be the current one.</returns>
        protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
        {
            var radialOrigin = (RadialGradientBrush)origin;
            var radialDestination = (RadialGradientBrush)destination;

            this.InitializeAnimatedBrush();
            this.SetCurrentOpacity(radialOrigin, radialDestination, animationClock);
            this.SetCurrentGradientStops(radialOrigin, radialDestination, animationClock);
            this.SetCurrentCenter(radialOrigin, radialDestination, animationClock);
            this.SetCurrentRadiusX(radialOrigin, radialDestination, animationClock);
            this.SetCurrentRadiusY(radialOrigin, radialDestination, animationClock);
            this.SetCurrentGradientOrigin(radialOrigin, radialDestination, animationClock);

            return _animatedBrush;
        }

        private void InitializeAnimatedBrush()
        {
            if (_animatedBrush == null || _animatedBrush.IsFrozen)
            {
                _animatedBrush = new RadialGradientBrush();
            }
        }

        private void SetCurrentOpacity(
            RadialGradientBrush origin, RadialGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.Opacity = _animationHelper.GetCurrentDouble(
                origin.Opacity, destination.Opacity, animationClock);
        }

        private void SetCurrentGradientStops(
            RadialGradientBrush origin, RadialGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.GradientStops = _animationHelper.GetCurrentGradientStops(
                origin.GradientStops, destination.GradientStops, animationClock);
        }
        
        private void SetCurrentCenter(
            RadialGradientBrush origin, RadialGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.Center = _animationHelper.GetCurrentPoint(
                origin.Center, destination.Center, animationClock);
        }

        private void SetCurrentRadiusX(
            RadialGradientBrush origin, RadialGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.RadiusX = _animationHelper.GetCurrentDouble(
                origin.RadiusX, destination.RadiusY, animationClock);
        }

        private void SetCurrentRadiusY(
            RadialGradientBrush origin, RadialGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.RadiusY = _animationHelper.GetCurrentDouble(
                origin.RadiusY, destination.RadiusY, animationClock);
        }
        
        private void SetCurrentGradientOrigin(
            RadialGradientBrush origin, RadialGradientBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.GradientOrigin = _animationHelper.GetCurrentPoint(
                origin.GradientOrigin, destination.GradientOrigin, animationClock);
        }

    }

}
