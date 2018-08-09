using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An animation which animates <see cref="SolidColorBrush"/> objects.
    /// </summary>
    public class SolidColorBrushAnimation : BrushAnimationBase
    {
        
        private SolidColorBrush _currentBrush;
        private BrushColorAnimationMapper _colorAnimationMapper;
        private BrushDoubleAnimationMapper _doubleAnimationMapper;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrushAnimation"/>.
        /// </summary>
        public SolidColorBrushAnimation()
        {
            _currentBrush = new SolidColorBrush();
            _colorAnimationMapper = new BrushColorAnimationMapper(this);
            _doubleAnimationMapper = new BrushDoubleAnimationMapper(this);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="SolidColorBrushAnimation"/>.
        /// </summary>
        /// <returns>A new <see cref="SolidColorBrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new SolidColorBrushAnimation();

        /// <summary>
        ///     Calculates a value that represents the current value of the property being animated, 
        ///     as determined by the host animation.
        /// </summary>
        /// <param name="defaultOriginValue">
        ///      The suggested origin value, used if the animation 
        ///      does not have its own explicitly set start value.
        /// </param>
        /// <param name="defaultDestinationValue">
        ///     The suggested destination value, used if the animation 
        ///     does not have its own explicitly set end value.
        /// </param>
        /// <param name="animationClock">
        ///     The <see cref="AnimationClock"/> which can generate the <see cref="Clock.CurrentTime"/>
        ///     or <see cref="Clock.CurrentProgress"/> value to be used by the
        ///     animation to generate its output value.
        /// </param>
        /// <returns>The value this animation believes should be the current value for the property.</returns>
        protected override Brush GetCurrentValueCore(
            Brush defaultOriginValue, Brush defaultDestinationValue, AnimationClock animationClock)
        {
            defaultOriginValue = this.From ?? defaultDestinationValue;
            defaultDestinationValue = this.To ?? defaultDestinationValue;
            this.ValidateThatBrushesAreSolid(defaultOriginValue, defaultDestinationValue);

            var origin = (SolidColorBrush)defaultOriginValue;
            var destination = (SolidColorBrush)defaultDestinationValue;

            return this.CalculateCurrentBrush(origin, destination, animationClock);
        }

        private void ValidateThatBrushesAreSolid(Brush origin, Brush destination)
        {
            if (!(origin is SolidColorBrush) ||
                !(destination is SolidColorBrush))
            {
                throw new InvalidOperationException(
                    $"The {nameof(SolidColorBrushAnimation)} can only animate {nameof(SolidColorBrush)} " +
                    $"objects.");
            }
        }

        private Brush CalculateCurrentBrush(
            SolidColorBrush origin, SolidColorBrush destination, AnimationClock animationClock)
        {
            this.CalculateCurrentColor(origin, destination, animationClock);
            this.CalculateCurrentOpacity(origin, destination, animationClock);
            return _currentBrush;
        }

        private void CalculateCurrentColor(
            SolidColorBrush origin, SolidColorBrush destination, AnimationClock animationClock)
        {
            _currentBrush.Color = _colorAnimationMapper.GetCurrentValue(
                origin.Color, destination.Color, animationClock);
        }

        private void CalculateCurrentOpacity(
            SolidColorBrush origin, SolidColorBrush destination, AnimationClock animationClock)
        {
            _currentBrush.Opacity = _doubleAnimationMapper.GetCurrentValue(
                origin.Opacity, destination.Opacity, animationClock);
        }

    }

}
