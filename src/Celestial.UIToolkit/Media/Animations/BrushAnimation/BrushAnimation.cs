using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Animates the value of a <see cref="Brush"/> property between two targets,
    /// using linear interpolation over a specified <see cref="Duration"/>.
    /// </summary>
    /// <remarks>
    /// When using this animation, be aware that it is a lot heavier than other animations,
    /// since it needs to create a new <see cref="Brush"/> instance during every animation
    /// cycle.
    /// With that being said, assuming a "normal" usage, no performance decrease will be noticeable.
    /// 
    /// The animation only supports brushes of the following types: 
    /// - <see cref="SolidColorBrush"/>
    /// - <see cref="LinearGradientBrush"/>
    /// - <see cref="RadialGradientBrush"/>.
    /// In addition, the animation can only animate from one brush to another brush of the same type.
    /// This means that you can, for instance, animate from a <see cref="LinearGradientBrush"/> to another
    /// <see cref="LinearGradientBrush"/>, but not from a <see cref="SolidColorBrush"/> to a 
    /// <see cref="RadialGradientBrush"/>.
    /// Furthermore, the animation will throw an exception if un-animatable properties like enumeration values
    /// (for example, <see cref="GradientBrush.ColorInterpolationMode"/>) of two brushes have the same value.
    /// Finally, <see cref="GradientBrush"/> objects must always have the same number of gradient stops.
    /// </remarks>
    public class BrushAnimation : EasingFromToByAnimationBase<Brush>
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BrushAnimation"/> class.
        /// </summary>
        public BrushAnimation() { }
        
        /// <summary>
        /// Creates a new instance of the <see cref="BrushAnimation"/> class.
        /// </summary>
        /// <returns>A new <see cref="BrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BrushAnimation();

        /// <summary>
        /// Validates that the specified brushes satisfy all rules which
        /// are required for the animation to be able to work.
        /// </summary>
        /// <param name="from">The origin brush.</param>
        /// <param name="to">The destination brush.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if one of the validation rules fails.
        /// </exception>
        protected override void ValidateAnimationValues(Brush from, Brush to)
        {
            BrushAnimationValidator.ValidateBrushes(from, to);
        }

        /// <summary>
        /// Returns a new brush which equals the result of an
        /// addition of the two specified brushes.
        /// </summary>
        /// <param name="a">The first brush.</param>
        /// <param name="b">The second brush.</param>
        /// <returns>The result of the addition.</returns>
        protected override sealed Brush AddValues(Brush a, Brush b)
        {
            return AnimatedBrushHelpers.SupportedTypeHelpers[a.GetType()]
                                       .AddValues(a, b);
        }

        /// <summary>
        /// Returns a new brush which equals the result of a
        /// subtraction of the two specified brushes.
        /// </summary>
        /// <param name="a">The first brush.</param>
        /// <param name="b">The second brush.</param>
        /// <returns>The result of the subtraction.</returns>
        protected override sealed Brush SubtractValues(Brush a, Brush b)
        {
            return AnimatedBrushHelpers.SupportedTypeHelpers[a.GetType()]
                                       .SubtractValues(a, b);
        }

        /// <summary>
        /// Returns a new brush whose animatable properties
        /// have been scaled by the specified <paramref name="factor"/>.
        /// </summary>
        /// <param name="value">The brush to be scaled.</param>
        /// <param name="factor">The factor by which the brush should be scaled.</param>
        /// <returns>The result of the scaling.</returns>
        protected override sealed Brush ScaleValue(Brush value, double factor)
        {
            return AnimatedBrushHelpers.SupportedTypeHelpers[value.GetType()]
                                       .ScaleValue(value, factor);
        }

        /// <summary>
        /// Returns the result of the interpolation of the two <paramref name="from"/> and <paramref name="to"/>
        /// brush at the provided progress increment.
        /// </summary>
        /// <param name="from">The brush from which denotes the animation's start.</param>
        /// <param name="to">The brush which denotes the animation's end.</param>
        /// <param name="progress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed during this animation.
        /// </param>
        /// <returns>The output value of the interpolation, given the specified values.</returns>
        protected override sealed Brush InterpolateValueCore(Brush from, Brush to, double progress)
        {
            return AnimatedBrushHelpers.SupportedTypeHelpers[from.GetType()]
                                       .InterpolateValue(from, to, progress);
        }
        
    }

}
