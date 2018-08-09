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
    /// An abstract base class which, when implemented, animates
    /// a <see cref="Brush"/>.
    /// </summary>
    public abstract class BrushAnimationBase : AnimationBase<Brush> { }

    /// <summary>
    ///     An animation which is able to animate <see cref="SolidColorBrush"/>,
    ///     <see cref="LinearGradientBrush"/> and <see cref="RadialGradientBrush"/>
    ///     objects.
    /// </summary>
    /// <remarks>
    ///     Even though the animation supports a set of different brushes,
    ///     it will typically not be able to do "inter-brush" animations.
    ///     One animation can only animate a single type of brush.
    ///     
    ///     In addition, when animating gradient brushes, the animation can
    ///     only deal with brush values which have a fixed number of
    ///     gradient stops.
    ///     Deviations from this number will cause the animation to fail.
    /// </remarks>
    public class BrushAnimation : BrushAnimationBase
    {
        
        /// <summary>
        /// Identifies the <see cref="From"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From), typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="To"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To), typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a <see cref="Brush"/> which serves as the animation's
        /// starting value.
        /// </summary>
        public Brush From
        {
            get { return (Brush)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a <see cref="Brush"/> which serves as the animation's ending value.
        /// </summary>
        public Brush To
        {
            get { return (Brush)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }
        
        /// <summary>
        /// Returns a new instance of the <see cref="BrushAnimation"/> class.
        /// </summary>
        /// <returns>A new <see cref="BrushAnimation"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BrushAnimation();

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
        protected override Brush GetCurrentValueCore(Brush defaultOriginValue, Brush defaultDestinationValue, AnimationClock animationClock)
        {
            Brush origin = this.From ?? defaultOriginValue;
            Brush destination = this.To ?? defaultDestinationValue;
            this.ValidateTimelineValues(origin, destination);
            throw new NotImplementedException();
        }

        private void ValidateTimelineValues(Brush origin, Brush destination)
        {
            this.ValidateThatBrushesAreNotNull(origin, destination);
            this.ValidateThatBrushesHaveSameType(origin, destination);
            this.ValidateThatBrushHasSupportedType(origin); // Call only once, since originType == destType.
        }

        private void ValidateThatBrushesAreNotNull(Brush origin, Brush destination)
        {
            if (origin == null || destination == null)
                throw new InvalidOperationException(
                    $"The {nameof(BrushAnimation)} requires an origin and a destination value. " +
                    $"Ensure that both the {nameof(From)} and {nameof(To)} properties are set.");
        }

        private void ValidateThatBrushesHaveSameType(Brush origin, Brush destination)
        {
            if (origin.GetType() != destination.GetType())
                throw new InvalidOperationException(
                    $"The {nameof(BrushAnimation)} requires all brushes to be of the same type.");
        }

        private void ValidateThatBrushHasSupportedType(Brush brush)
        {
            Type myBrushType = brush.GetType();
            if (myBrushType != typeof(SolidColorBrush) &&
                myBrushType != typeof(LinearGradientBrush) &&
                myBrushType != typeof(RadialGradientBrush))
            {
                throw new InvalidOperationException(
                    $"The {nameof(BrushAnimation)} does not support brushes of type {myBrushType.FullName}.");
            }
        }
        
    }

}
