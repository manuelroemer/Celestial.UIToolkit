using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A base class for any animation that animates a <see cref="Brush"/>.
    /// </summary>
    public abstract class BrushAnimationBase : AnimationBase<Brush>
    {
        
        /// <summary>
        /// Identifies the <see cref="From"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From), typeof(Brush), typeof(BrushAnimationBase), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="To"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To), typeof(Brush), typeof(BrushAnimationBase), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="EasingFunction"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            nameof(EasingFunction), typeof(IEasingFunction), typeof(BrushAnimationBase), new PropertyMetadata(null));
        
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
        /// Gets or sets the easing function applied to this animation.
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the target property's current value 
        /// should be added to this animation's starting value.
        /// </summary>
        public bool IsAdditive { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies whether the animation's value accumulates when it repeats.
        /// </summary>
        public bool IsCumulative { get; set; }

        /// <summary>
        ///     Returns the current value of the animation.
        /// </summary>
        /// <param name="defaultOriginValue">
        ///     The origin value provided to the animation if the animation does not have its
        ///     own start value. If this animation is the first in a composition chain it will
        ///     be the base value of the property being animated; otherwise it will be the value
        ///     returned by the previous animation in the chain.
        /// </param>
        /// <param name="defaultDestinationValue">
        ///     The destination value provided to the animation if the animation does not have
        ///     its own destination value.
        /// </param>
        /// <param name="animationClock">
        ///     The <see cref="AnimationClock"/> which can generate the <see cref="Clock.CurrentTime"/>
        ///     or <see cref="Clock.CurrentProgress"/> value to be used by the
        ///     animation to generate its output value.
        /// </param>
        /// <returns>The value this animation believes should be the current value for the property.</returns>
        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            Brush origin = this.From ?? defaultOriginValue as Brush;
            Brush destination = this.To ?? defaultDestinationValue as Brush;
            this.ValidateTimelineValues(origin, destination);

            return base.GetCurrentValue(defaultOriginValue, defaultDestinationValue, animationClock);
        }

        private void ValidateTimelineValues(Brush origin, Brush destination)
        {
            this.ValidateThatBrushesAreNotNull(origin, destination);
            this.ValidateThatBrushesHaveSameType(origin, destination);
        }

        private void ValidateThatBrushesAreNotNull(Brush origin, Brush destination)
        {
            if (origin == null || destination == null)
                throw new InvalidOperationException(
                    $"The brush animation requires an origin and a destination value. " +
                    $"Ensure that both the {nameof(From)} and {nameof(To)} properties are set " +
                    $"and that both of them are of type {nameof(Brush)}.");
        }

        private void ValidateThatBrushesHaveSameType(Brush origin, Brush destination)
        {
            if (origin.GetType() != destination.GetType())
                throw new InvalidOperationException(
                    $"The brush animation requires all brushes to be of the same type.");
        }
        
    }
    
}
