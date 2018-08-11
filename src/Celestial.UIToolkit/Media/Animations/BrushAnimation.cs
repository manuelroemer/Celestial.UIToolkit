using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{
    
    /// <summary>
    /// A base class for any animation that animates a <see cref="Brush"/>.
    /// </summary>
    public abstract class BrushAnimation : AnimationBase<Brush>
    {

        private Lazy<BrushAnimationToDoubleAnimationMapper> _doubleAnimWrapperLazy;
        private Lazy<BrushAnimationToColorAnimationMapper> _colorAnimWrapperLazy;
        
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
        /// Identifies the <see cref="EasingFunction"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            nameof(EasingFunction), typeof(IEasingFunction), typeof(BrushAnimation), new PropertyMetadata(null));
        
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
        /// Initializes a new instance of the <see cref="BrushAnimation"/> class.
        /// </summary>
        public BrushAnimation()
        {
            _doubleAnimWrapperLazy = new Lazy<BrushAnimationToDoubleAnimationMapper>(
                () => new BrushAnimationToDoubleAnimationMapper(this));
            _colorAnimWrapperLazy = new Lazy<BrushAnimationToColorAnimationMapper>(
                () => new BrushAnimationToColorAnimationMapper(this));
        }

        /// <summary>
        /// Calculates the brush which represents the current value of the animation.
        /// </summary>
        /// <param name="defaultOriginValue">
        /// The suggested origin brush, used if <see cref="From"/> is <c>null</c>.
        /// </param>
        /// <param name="defaultDestinationValue">
        /// The suggested origin brush, used if <see cref="To"/> is <c>null</c>.
        /// </param>
        /// <param name="animationClock">
        /// The <see cref="AnimationClock"/> to be used by the animation to generate its output value.
        /// </param>
        /// <returns>The brush which this animation believes to be the current one.</returns>
        protected override Brush GetCurrentValueCore(Brush defaultOriginValue, Brush defaultDestinationValue, AnimationClock animationClock)
        {
            Brush origin = this.From ?? defaultOriginValue;
            Brush destination = this.To ?? defaultDestinationValue;
            this.ValidateTimelineBrushes(origin, destination);

            return this.GetCurrentBrush(origin, destination, animationClock);
        }

        /// <summary>
        /// Performs basic validation on the <paramref name="origin"/> and <paramref name="destination"/>
        /// brushes.
        /// </summary>
        /// <param name="origin">
        /// The brush which serves as the animation's origin.
        /// </param>
        /// <param name="destination">
        /// The brush which serves as the animation's destination.
        /// </param>
        protected virtual void ValidateTimelineBrushes(Brush origin, Brush destination)
        {
            this.ValidateThatBrushesAreNotNull(origin, destination);
            this.ValidateThatBrushesHaveSameType(origin, destination);
            this.ValidateThatBrushesHaveSameTransform(origin, destination);
            this.ValidateTimelineBrushesCore(origin, destination);
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
            {
                throw new InvalidOperationException(
                    $"The animation can only animate two brushes of the same type.");
            }
        }

        private void ValidateThatBrushesHaveSameTransform(Brush origin, Brush destination)
        {
            if (origin.Transform != destination.Transform ||
                origin.RelativeTransform != destination.RelativeTransform)
            {
                throw new InvalidOperationException(
                    $"The {nameof(Brush.Transform)} and {nameof(Brush.RelativeTransform)} properties of the " +
                    $"brushes must have the same values.");
            }
        }

        /// <summary>
        /// If overridden, can be used to perform additional validation on the
        /// specified brushes, before the animation's <see cref="GetCurrentBrush(Brush, Brush, AnimationClock)"/>
        /// method is called.
        /// </summary>
        /// <param name="origin">
        /// The brush which serves as the animation's origin.
        /// </param>
        /// <param name="destination">
        /// The brush which serves as the animation's destination.
        /// </param>
        protected virtual void ValidateTimelineBrushesCore(Brush origin, Brush destination) { }

        /// <summary>
        /// Calculates the brush which represents the current value of the animation.
        /// When implementing this method, don't override <see cref="GetCurrentValueCore(Brush, Brush, AnimationClock)"/>.
        /// </summary>
        /// <param name="origin">
        /// The brush which serves as the animation's origin.
        /// </param>
        /// <param name="destination">
        /// The brush which serves as the animation's destination.
        /// </param>
        /// <param name="animationClock">
        ///     The <see cref="AnimationClock"/> to be used by the animation to generate its output value.
        /// </param>
        /// <returns>The brush which this animation believes to be the current one.</returns>
        protected abstract Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock);

        internal virtual double GetCurrentDouble(double origin, double destination, AnimationClock animationClock)
        {
            if (animationClock == null) throw new ArgumentNullException(nameof(animationClock));

            return _doubleAnimWrapperLazy.Value.GetCurrentValue(
                origin, destination, animationClock);
        }

        internal virtual Color GetCurrentColor(Color origin, Color destination, AnimationClock animationClock)
        {
            if (animationClock == null) throw new ArgumentNullException(nameof(animationClock));

            return _colorAnimWrapperLazy.Value.GetCurrentValue(
                origin, destination, animationClock);
        }

    }
    
}
