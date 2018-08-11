using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An animation which animates <see cref="SolidColorBrush"/> objects.
    /// </summary>
    public class SolidColorBrushAnimation : BrushAnimation
    {
        
        private SolidColorBrush _animatedBrush;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrushAnimation"/>.
        /// </summary>
        public SolidColorBrushAnimation()
        {
            _animatedBrush = new SolidColorBrush();
        }

        /// <summary>
        /// Returns a new instance of the <see cref="SolidColorBrushAnimation"/>.
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

        protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
        {
            var solidOrigin = (SolidColorBrush)origin;
            var solidDestination = (SolidColorBrush)destination;
            
            _animatedBrush.Color = this.GetCurrentColor(
                solidOrigin.Color, solidDestination.Color, animationClock);
            _animatedBrush.Opacity = this.GetCurrentDouble(
                solidOrigin.Opacity, solidDestination.Opacity, animationClock);
            return _animatedBrush;
        }
        
    }

}
