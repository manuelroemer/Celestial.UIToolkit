using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An animation which animates <see cref="SolidColorBrush"/> objects.
    /// </summary>
    public class SolidColorBrushAnimation : OldBrushAnimation
    {
        
        private SolidColorBrush _animatedBrush;
        private BrushAnimationHelper _animationHelper;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrushAnimation"/> class.
        /// </summary>
        public SolidColorBrushAnimation()
        {
            _animationHelper = new BrushAnimationHelper(this);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="SolidColorBrushAnimation"/> class.
        /// </summary>
        /// <returns>A new <see cref="SolidColorBrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new SolidColorBrushAnimation();

        /// <summary>
        /// Used to validate that the specified brushes can be animated by
        /// the <see cref="SolidColorBrushAnimation"/>.
        /// </summary>
        /// <param name="origin">The origin brush.</param>
        /// <param name="destination">The destination brush.</param>
        protected override void ValidateTimelineBrushesCore(Brush origin, Brush destination)
        {
            ValidateThatBrushesAreSolid(origin, destination);
        }

        private void ValidateThatBrushesAreSolid(Brush origin, Brush destination)
        {
            if (origin.GetType() != typeof(SolidColorBrush) ||
                destination.GetType() != typeof(SolidColorBrush))
            {
                throw new InvalidOperationException(
                    $"The animation can only animate brushes of type {nameof(SolidColorBrush)}.");
            }
        }

        /// <summary>
        /// Calculates the <see cref="SolidColorBrush"/> which represents the current value of the animation.
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
        /// <returns>The <see cref="SolidColorBrush"/> which this animation believes to be the current one.</returns>
        protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
        {
            var solidOrigin = (SolidColorBrush)origin;
            var solidDestination = (SolidColorBrush)destination;

            this.InitializeAnimatedBrush();
            this.SetCurrentColor(solidOrigin, solidDestination, animationClock);
            this.SetCurrentOpacity(solidOrigin, solidDestination, animationClock);

            return _animatedBrush;
        }

        private void InitializeAnimatedBrush()
        {
            if (_animatedBrush == null || _animatedBrush.IsFrozen)
            {
                _animatedBrush = new SolidColorBrush();
            }
        }

        private void SetCurrentColor(SolidColorBrush origin, SolidColorBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.Color = _animationHelper.GetCurrentColor(
                origin.Color, destination.Color, animationClock);
        }

        private void SetCurrentOpacity(SolidColorBrush origin, SolidColorBrush destination, AnimationClock animationClock)
        {
            _animatedBrush.Opacity = _animationHelper.GetCurrentDouble(
                origin.Opacity, destination.Opacity, animationClock);
        }

    }

}
