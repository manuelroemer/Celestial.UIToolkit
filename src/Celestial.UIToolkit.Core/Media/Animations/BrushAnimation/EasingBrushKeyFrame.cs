using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A key frame which allows you to associate easing functions with a
    /// <see cref="BrushAnimationUsingKeyFrames"/> animation.
    /// </summary>
    public class EasingBrushKeyFrame : EasingKeyFrameBase<Brush>
    {

        /// <summary>
        /// Creates a new instance of the <see cref="EasingBrushKeyFrame"/> class.
        /// </summary>
        /// <returns>A new <see cref="EasingBrushKeyFrame"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new EasingBrushKeyFrame();

        /// <summary>
        /// Calculates the value of a key frame at the progress increment provided.
        /// </summary>
        /// <param name="baseValue">The value to animate from; typically the value of the previous key frame.</param>
        /// <param name="easedProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// This value has already been eased by the <see cref="EasingFunction"/>.
        /// </param>
        /// <returns>The output value of this key frame given the specified base value and progress.</returns>
        protected override Brush InterpolateValueAfterEase(Brush baseValue, double easedProgress)
        {
            if (easedProgress <= 0) return baseValue;
            if (easedProgress >= 1) return Value;
            BrushAnimationValidator.ValidateBrushes(baseValue, Value);
            return SupportedAnimationBrushes.GetAnimationHelper(baseValue)
                                            .InterpolateValue(baseValue, Value, easedProgress);
        }

    }

}
