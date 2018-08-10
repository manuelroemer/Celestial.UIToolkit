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
        /// Gets the type of brush which is being animated by the animation,
        /// which is <see cref="SolidColorBrush"/>.
        /// </summary>
        protected override Type AnimationBrushType => typeof(SolidColorBrush);

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
