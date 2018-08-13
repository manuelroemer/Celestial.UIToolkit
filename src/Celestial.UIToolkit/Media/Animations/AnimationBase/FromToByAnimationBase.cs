using Celestial.UIToolkit.Extensions;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Defines the base class for a From/To/By animation.
    /// </summary>
    /// <typeparam name="T">
    /// The type which is being animated by the animation.
    /// </typeparam>
    public abstract class FromToByAnimationBase<T> : AnimationBase<T>
    {

        private bool _areAnimationValuesUpToDate = false;
        private AnimationType _animationType;
        private T _actualFrom;
        private T _actualTo;
        private T _additiveModifier;
        private T _cumulativeModifier;
        private bool _useAdditiveModifier;
        private bool _useCumulativeModifier;

        /// <summary>
        /// Identifies the <see cref="From"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From), typeof(T), typeof(FromToByAnimationBase<T>), new PropertyMetadata(default(T), FromToBy_Changed));

        /// <summary>
        /// Identifies the <see cref="By"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ByProperty = DependencyProperty.Register(
            nameof(By), typeof(T), typeof(FromToByAnimationBase<T>), new PropertyMetadata(default(T), FromToBy_Changed));
        
        /// <summary>
        /// Identifies the <see cref="To"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To), typeof(T), typeof(FromToByAnimationBase<T>), new PropertyMetadata(default(T), FromToBy_Changed));

        /// <summary>
        /// Gets or sets the animation's starting value.
        /// </summary>
        public T From
        {
            get { return (T)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        /// Gets or sets the animation's ending value.
        /// </summary>
        public T To
        {
            get { return (T)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the total amount by which the animation changes its starting value.
        /// If both this property and <see cref="To"/> are set, this one is going to be ignored.
        /// </summary>
        public T By
        {
            get { return (T)GetValue(ByProperty); }
            set { SetValue(ByProperty, value); }
        }
        
        /// <summary>
        /// Gets a value that specifies whether the animation's output value is added to
        /// the base value of the property being animated.
        /// </summary>
        public bool IsAdditive
        {
            get { return (bool)GetValue(IsAdditiveProperty); }
            set { SetValue(IsAdditiveProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the animation's value accumulates
        /// when it repeats.
        /// </summary>
        public bool IsCumulative
        {
            get { return (bool)GetValue(IsCumulativeProperty); }
            set { SetValue(IsCumulativeProperty, value); }
        }

        private static void FromToBy_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (FromToByAnimationBase<T>)d;
            self._areAnimationValuesUpToDate = false;
        }
        
        protected override sealed T GetCurrentValueCore(T defaultOriginValue, T defaultDestinationValue, AnimationClock animationClock)
        {
            this.SetCurrentAnimationValues(defaultOriginValue, defaultDestinationValue, animationClock);

            double progress = animationClock.CurrentProgress.Value;
            T interpolatedValue;

            interpolatedValue = this.InterpolateValue(_actualFrom, _actualTo, progress);
            if (_useCumulativeModifier)
                interpolatedValue = this.AddValues(interpolatedValue, _cumulativeModifier);
            if (_useAdditiveModifier)
                interpolatedValue = this.AddValues(interpolatedValue, _additiveModifier);

            return interpolatedValue;
        }
        
        private void SetCurrentAnimationValues(T defaultOrigin, T defaultDestination, AnimationClock animationClock)
        {
            if (_areAnimationValuesUpToDate) return;

            this.SetCurrentAnimationType();
            this.SetCurrentFromAndTo(defaultOrigin, defaultDestination);
            this.SetCurrentAdditiveModifier(defaultOrigin);
            this.SetCurrentCumulativeModifier(animationClock);

            _areAnimationValuesUpToDate = true;
        }

        private void SetCurrentAnimationType()
        {
            bool isFromSet = FromProperty.HasLocalValue(this);
            bool isToSet = ToProperty.HasLocalValue(this);
            bool isBySet = ByProperty.HasLocalValue(this);

            if (isFromSet && isToSet) _animationType = AnimationType.FromTo;
            if (isFromSet && isBySet) _animationType = AnimationType.FromBy;
            if (isFromSet) _animationType = AnimationType.From;
            if (isToSet) _animationType = AnimationType.To;
            if (isBySet) _animationType = AnimationType.By;
            _animationType = AnimationType.Automatic;
        }

        private void SetCurrentFromAndTo(T defaultOrigin, T defaultDestination)
        {
            switch (_animationType)
            {
                case AnimationType.Automatic:
                    this.SetActualFromAndTo(defaultOrigin, defaultDestination);
                    break;
                case AnimationType.From:
                    this.SetActualFromAndTo(this.From, defaultDestination);
                    break;
                case AnimationType.To:
                    this.SetActualFromAndTo(defaultOrigin, this.To);
                    break;
                case AnimationType.By:
                    this.SetActualFromAndTo(defaultOrigin, this.AddValues(this.From, this.By));
                    break;
                case AnimationType.FromTo:
                    this.SetActualFromAndTo(this.From, this.By);
                    break;
                case AnimationType.FromBy:
                    this.SetActualFromAndTo(this.From, this.AddValues(this.From, this.By));
                    break;
                default:
                    throw new NotImplementedException("Unknown animation type.");
            }
        }

        private void SetCurrentAdditiveModifier(T defaultOrigin)
        {
            if ((_animationType == AnimationType.FromTo || _animationType == AnimationType.FromBy))
            {
                if (this.IsAdditive)
                {
                    _additiveModifier = defaultOrigin;
                    _useAdditiveModifier = true;
                }
            }
        }

        private void SetCurrentCumulativeModifier(AnimationClock animationClock)
        {
            if (this.IsCumulative)
            {
                double factor = animationClock.CurrentIteration.GetValueOrDefault() - 1;
                if (factor > 0d)
                {
                    T toFromDistance = this.SubtractValues(_actualTo, _actualFrom);
                    _cumulativeModifier = this.ScaleValue(toFromDistance, factor);
                    _useCumulativeModifier = true;
                }
            }
        }

        private void SetActualFromAndTo(T from, T to)
        {
            _actualFrom = from;
            _actualTo = to;
        }
 
        /// <summary>
        /// Adds the two specified values and returns the result.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The result of the addition of the two values.</returns>
        protected abstract T AddValues(T a, T b);

        /// <summary>
        /// Subtracts the two specified values and returns the result.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The result of the subtraction of the two values.</returns>
        protected abstract T SubtractValues(T a, T b);

        /// <summary>
        /// Scales the specified <paramref name="value"/> by a <paramref name="factor"/>.
        /// </summary>
        /// <param name="value">The value to be scaled.</param>
        /// <param name="factor">The factor by which the value should be scaled.</param>
        /// <returns>
        /// The result of the scaling.
        /// </returns>
        protected abstract T ScaleValue(T value, double factor);

        /// <summary>
        /// Returns the result of the interpolation of the two <paramref name="from"/> and <paramref name="to"/>
        /// values at the provided progress increment.
        /// </summary>
        /// <param name="from">The value from which the animation starts.</param>
        /// <param name="to">The value at which the animation ends.</param>
        /// <param name="progress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed during this animation.
        /// </param>
        /// <returns>The output value of the interpolation, given the specified values.</returns>
        protected abstract T InterpolateValue(T from, T to, double progress);

        private enum AnimationType : byte
        {
            Automatic,
            From,
            To,
            By,
            FromTo,
            FromBy
        }
       
    }

}
