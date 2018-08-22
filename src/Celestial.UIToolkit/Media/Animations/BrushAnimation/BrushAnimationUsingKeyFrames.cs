using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Animates the value of a <see cref="Brush"/> property along a set of <see cref="IKeyFrameAnimation.KeyFrames"/>.
    /// </summary>
    public class BrushAnimationUsingKeyFrames
        : AnimationUsingKeyFramesBase<Brush, KeyFrameBase<Brush>, BrushKeyFrameCollection>
    {

        /// <summary>
        /// Creates a new instance of the <see cref="BrushAnimationUsingKeyFrames"/> class.
        /// </summary>
        /// <returns>A new <see cref="BrushAnimationUsingKeyFrames"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BrushAnimationUsingKeyFrames();

        /// <summary>
        /// Returns a <see cref="Brush"/> which serves as a zero-value for an additive animation.
        /// The exact type of the brush is dependent on the first key frame in the animation.
        /// </summary>
        /// <returns>A new <see cref="Brush"/> which serves as a zero-value.</returns>
        protected override sealed Brush GetZeroValue()
        {
            if (KeyFrames.Count == 0)
                throw new InvalidOperationException(
                    "Creating a zero-brush requires at least one registered key frame.");

            var firstFrame = KeyFrames.First();
            var brushType = firstFrame.Value.GetType();
            return SupportedAnimationBrushes.GetAnimationHelper(brushType)
                                            .GetZeroValue();
        }

        /// <summary>
        /// Adds the values of the two specified brushes
        /// and returns a new brush, which represents the addition's
        /// result.
        /// </summary>
        /// <param name="a">the first brush.</param>
        /// <param name="b">The second brush.</param>
        /// <returns>
        /// The result of the addition.
        /// </returns>
        protected override sealed Brush AddValues(Brush a, Brush b)
        {
            BrushAnimationValidator.ValidateBrushes(a, b);
            return SupportedAnimationBrushes.GetAnimationHelper(a)
                                            .AddValues(a, b);
        }

        /// <summary>
        /// Scales the specified <see cref="Brush"/> by the specified <paramref name="factor"/>.
        /// </summary>
        /// <param name="value">The <see cref="Brush"/> to be scaled.</param>
        /// <param name="factor">The factor by which the brush should be scaled.</param>
        /// <returns>A new <see cref="Brush"/>, which represents the scaling's result.</returns>
        protected override sealed Brush ScaleValue(Brush value, double factor)
        {
            return SupportedAnimationBrushes.GetAnimationHelper(value)
                                            .ScaleValue(value, factor);
        }

        /// <summary>
        /// Returns 1.
        /// </summary>
        /// <param name="from">Not used.</param>
        /// <param name="to">Not used.</param>
        /// <returns>1.</returns>
        protected override sealed double GetSegmentLengthCore(Brush from, Brush to)
        {
            // Brushes are nominal which means that we can't get any kind of distance out of them.
            // By returning a uniform value, we basically treat Paced KeyTimes as Uniform.
            return 1d;
        }

    }

}
