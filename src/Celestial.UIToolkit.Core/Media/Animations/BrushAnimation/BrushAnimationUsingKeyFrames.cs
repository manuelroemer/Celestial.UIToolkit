using Celestial.UIToolkit.Xaml;
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
        : AnimationUsingKeyFramesBase<Brush, KeyFrameBase<Brush>, BrushKeyFrameCollection>,
          IVisualTransitionProvider
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
            return BrushAnimationHelper.Instance.GetZeroValue();
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
            BrushAnimationInput transformedInput = BrushAnimationInputTransformer.Transform(a, b);
            a = transformedInput.From;
            b = transformedInput.To;

            BrushAnimationValidator.ValidateBrushes(a, b);
            return BrushAnimationHelper.Instance.AddValues(a, b);
        }

        /// <summary>
        /// Scales the specified <see cref="Brush"/> by the specified <paramref name="factor"/>.
        /// </summary>
        /// <param name="value">The <see cref="Brush"/> to be scaled.</param>
        /// <param name="factor">The factor by which the brush should be scaled.</param>
        /// <returns>A new <see cref="Brush"/>, which represents the scaling's result.</returns>
        protected override sealed Brush ScaleValue(Brush value, double factor)
        {
            value = BrushAnimationInputTransformer.Transform(value);
            return BrushAnimationHelper.Instance.ScaleValue(value, factor);
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

        /// <summary>
        /// Returns <c>true</c> if the <paramref name="timeline"/> is of type
        /// <see cref="BrushAnimationUsingKeyFrames"/>.
        /// </summary>
        /// <param name="timeline">The timeline to be checked.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="timeline"/> is of type
        /// <see cref="BrushAnimationUsingKeyFrames"/>.
        /// </returns>
        public bool SupportsTimeline(Timeline timeline)
            => timeline is BrushAnimationUsingKeyFrames;

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions away from
        /// the element.
        /// The timeline which gets returned by this method is then used as a transitioning
        /// animation.
        /// </summary>
        /// <param name="fromTimeline">
        /// The input timeline, for which a visual transition timeline should be generated.
        /// The VisualStateManager wants to transition away from this timeline.
        /// </param>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition away from this element.
        /// </returns>
        public Timeline CreateFromTransitionTimeline(
            Timeline fromTimeline, IEasingFunction easingFunction)
        {
            return new BrushAnimation()
            {
                EasingFunction = easingFunction
            };
        }

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions to the element.
        /// The timeline which gets returned by this method is then used as a transitioning animation.
        /// </summary>
        /// <param name="toTimeline">
        /// The input timeline, for which a visual transition timeline should be generated.
        /// The VisualStateManager wants to transition to this timeline.
        /// </param>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition to this element.
        /// </returns>
        public Timeline CreateToTransitionTimeline(
            Timeline toTimeline, IEasingFunction easingFunction)
        {
            var result = new BrushAnimation() { EasingFunction = easingFunction };
            if (KeyFrames.Count > 0)
            {
                result.To = KeyFrames.First().Value;
            }
            return result;
        }

    }

}
