using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Animates from the <see cref="Brush"/> value of the previous key frame to its own 
    /// <see cref="KeyFrameBase{T}.Value"/> using linear interpolation.
    /// </summary>
    public class LinearBrushKeyFrame : KeyFrameBase<Brush>
    {

        /// <summary>
        /// Creates a new instance of the <see cref="LinearBrushKeyFrame"/> class.
        /// </summary>
        /// <returns>A new <see cref="LinearBrushKeyFrame"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new LinearBrushKeyFrame();

        /// <summary>
        /// Calculates the value of a key frame at the progress increment provided.
        /// </summary>
        /// <param name="baseValue">The value to animate from; typically the value of the previous key frame.</param>
        /// <param name="keyFrameProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// </param>
        /// <returns>The output value of this key frame given the specified base value and progress.</returns>
        protected override Brush InterpolateValueCore(Brush baseValue, double keyFrameProgress)
        {
            if (keyFrameProgress <= 0) return baseValue;
            if (keyFrameProgress >= 1) return this.Value;
            BrushAnimationValidator.ValidateBrushes(baseValue, this.Value);
            return AnimatedBrushHelpers.SupportedTypeHelpers[baseValue.GetType()]
                                       .InterpolateValue(baseValue, this.Value, keyFrameProgress);
        }

    }

}
