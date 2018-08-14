using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    ///     An animation which is able to animate <see cref="SolidColorBrush"/>,
    ///     <see cref="LinearGradientBrush"/> and <see cref="RadialGradientBrush"/>
    ///     objects.
    /// </summary>
    /// <remarks>
    ///     This animation is internally choosing one of the <see cref="OldBrushAnimation"/>
    ///     derivations.
    ///     Their limitations apply to this class aswell, meaning that the animated brushes
    ///     have to be of the same type, need to have certain shared properties, ...
    /// </remarks>
    public class OldCompositeBrushAnimation : OldBrushAnimation
    {

        private static IDictionary<Type, Func<OldBrushAnimation>> _supportedAnimationTypesMap;
        private OldBrushAnimation _currentBrushAnimation;

        static OldCompositeBrushAnimation()
        {
            // Map the supported brush types to factory functions, for a painless creation
            // of the "sub-animations".
            _supportedAnimationTypesMap = new Dictionary<Type, Func<OldBrushAnimation>>()
            {
                [typeof(SolidColorBrush)]     = () => new OldSolidColorBrushAnimation(),
                [typeof(LinearGradientBrush)] = () => new OldLinearGradientBrushAnimation(),
                [typeof(RadialGradientBrush)] = () => new OldRadialGradientBrushAnimation()
            };
        }

        /// <summary>
        /// Returns a new instance of the <see cref="OldCompositeBrushAnimation"/> class.
        /// </summary>
        /// <returns>A new <see cref="OldCompositeBrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new OldCompositeBrushAnimation();

        /// <summary>
        /// Ensures that the specified brush types are supported by the <see cref="OldCompositeBrushAnimation"/>.
        /// </summary>
        /// <param name="origin">The animation's origin brush.</param>
        /// <param name="destination">The animation's destination brush.</param>
        protected override void ValidateTimelineBrushesCore(Brush origin, Brush destination)
        {
            this.ValidateThatBrushIsSupported(origin);
            this.ValidateThatBrushIsSupported(destination);
        }
        
        private void ValidateThatBrushIsSupported(Brush brush)
        {
            if (!_supportedAnimationTypesMap.Keys.Contains(brush.GetType()))
            {
                throw new InvalidOperationException(
                    $"The {nameof(OldCompositeBrushAnimation)} doesn't support brushes of type {brush.GetType().FullName}. " +
                    $"The supported types are: " +
                    $"{string.Join(", ", _supportedAnimationTypesMap.Keys.Select(type => type.Name))}");
            }
        }

        /// <summary>
        /// Calculates the brush which represents the current value of the animation,
        /// by delegating the call down to the appropriate kind of <see cref="OldBrushAnimation"/>
        /// and returning the result.
        /// </summary>
        /// <param name="origin">
        /// The brush which serves as the animation's origin.
        /// </param>
        /// <param name="destination">
        /// The brush which serves as the animation's destination.
        /// </param>
        /// <param name="animationClock">
        /// The <see cref="AnimationClock"/> to be used by the animation to generate its output value.
        /// </param>
        /// <returns>The brush which this animation believes to be the current one.</returns>
        protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
        {
            this.SetCurrentBrushAnimation(origin);
            this.MapValuesToUnderlyingBrushAnimation(); // This should probably improved - 
                                                        // copying at each frame is expensive in the long run.

            return (Brush)_currentBrushAnimation.GetCurrentValue(origin, destination, animationClock);
        }

        private void SetCurrentBrushAnimation(Brush brush)
        {
            if (_supportedAnimationTypesMap.TryGetValue(brush.GetType(), out var brushAnimFactory))
            {
                _currentBrushAnimation = brushAnimFactory();
            }
            else
            {
                // If we get here, the validation function got overridden and we are
                // dealing with an unsupported brush type.
                // -> Validate.. throws the appropriate exception.
                this.ValidateThatBrushIsSupported(brush);
            }
        }

        private void MapValuesToUnderlyingBrushAnimation()
        {
            _currentBrushAnimation.From = this.From;
            _currentBrushAnimation.To = this.To;
            _currentBrushAnimation.EasingFunction = this.EasingFunction;
            _currentBrushAnimation.IsAdditive = this.IsAdditive;
            _currentBrushAnimation.IsCumulative = this.IsCumulative;
        }

    }

}
