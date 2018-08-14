using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Animates from the <see cref="Brush"/> value of the previous key frame to its own 
    /// <see cref="KeyFrameBase{T}.Value"/> using splined interpolation.
    /// </summary>
    public class SplineBrushKeyFrame : SplineKeyFrameBase<Brush>
    {

        /// <summary>
        /// Creates a new instance of the <see cref="SplineBrushKeyFrame"/> class.
        /// </summary>
        /// <returns>A new <see cref="SplineBrushKeyFrame"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new SplineBrushKeyFrame();

        /// <summary>
        /// Calculates the value of a key frame at the progress increment provided.
        /// </summary>
        /// <param name="baseValue">The value to animate from; typically the value of the previous key frame.</param>
        /// <param name="splineProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// The <see cref="KeySpline"/> has already been applied to this progress.
        /// </param>
        /// <returns>The output value of this key frame given the specified base value and progress.</returns>
        protected override Brush InterpolateValueWithSplineProgress(Brush baseValue, double splineProgress)
        {
            if (splineProgress <= 0) return baseValue;
            if (splineProgress >= 1) return this.Value;
            BrushAnimationValidator.ValidateBrushes(baseValue, this.Value);
            return AnimatedBrushHelpers.SupportedTypeHelpers[baseValue.GetType()]
                                       .InterpolateValue(baseValue, this.Value, splineProgress);
        }

    }

}
