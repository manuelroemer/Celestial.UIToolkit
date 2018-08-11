using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An abstract base class for a key frame that participates in
    /// a key frame animation.
    /// </summary>
    /// <typeparam name="T">The type of this key frame's <see cref="Value"/> property.</typeparam>
    public abstract class KeyFrameBase<T> : Freezable, IKeyFrame
    {
        
        /// <summary>
        /// Identifies the <see cref="KeyTime"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register(
            nameof(KeyTime), typeof(KeyTime), typeof(KeyFrameBase<T>), new PropertyMetadata(KeyTime.Uniform));

        /// <summary>
        /// Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(T), typeof(KeyFrameBase<T>), new PropertyMetadata(default(T)));

        /// <summary>
        /// Gets or sets a <see cref="KeyTime"/> value associated with this key frame.
        /// </summary>
        public KeyTime KeyTime
        {
            get { return (KeyTime)GetValue(KeyTimeProperty); }
            set { SetValue(KeyTimeProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the value which is associated with this key frame.
        /// </summary>
        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        object IKeyFrame.Value
        {
            get { return this.Value; }
            set
            {
                if (value != null && value.GetType() != typeof(T))
                    throw new ArgumentException($"The {this.GetType().Name} only supports values of type {typeof(T).FullName}.");
                this.Value = (T)value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyFrameBase{T}"/> class
        /// with default values.
        /// </summary>
        protected KeyFrameBase() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyFrameBase{T}"/> class
        /// with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        protected KeyFrameBase(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyFrameBase{T}"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        /// <param name="keyTime">A <see cref="KeyTime"/> value which is associated with this key frame.</param>
        protected KeyFrameBase(T value, KeyTime keyTime)
        {
            this.Value = value;
            this.KeyTime = keyTime;
        }

        /// <summary>
        /// Returns the interpolated value of this key frame at the provided progress increment.
        /// </summary>
        /// <param name="baseValue">The value to animate from.</param>
        /// <param name="keyFrameProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// </param>
        /// <returns>The output value of this key frame given the specified base value and progress.</returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        public T InterpolateValue(T baseValue, double keyFrameProgress)
        {
            if (keyFrameProgress < 0d || keyFrameProgress > 1d)
                throw new ArgumentNullException(nameof(keyFrameProgress));
            return this.InterpolateValueCore(baseValue, keyFrameProgress);
        }

        /// <summary>
        /// Calculates the value of a key frame at the progress increment provided.
        /// </summary>
        /// <param name="baseValue">The value to animate from; typically the value of the previous key frame.</param>
        /// <param name="keyFrameProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// </param>
        /// <returns>The output value of this key frame given the specified base value and progress.</returns>
        protected abstract T InterpolateValueCore(T baseValue, double keyFrameProgress);

    }

    /// <summary>
    /// An abstract base class for a discrete key frame.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="KeyFrameBase{T}.Value"/> property.</typeparam>
    public abstract class DiscreteKeyFrameBase<T> : KeyFrameBase<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteKeyFrameBase{T}"/> class
        /// with default values.
        /// </summary>
        protected DiscreteKeyFrameBase()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteKeyFrameBase{T}"/> class
        /// with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        protected DiscreteKeyFrameBase(T value)
            : base(value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteKeyFrameBase{T}"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        /// <param name="keyTime">A <see cref="KeyTime"/> value which is associated with this key frame.</param>
        protected DiscreteKeyFrameBase(T value, KeyTime keyTime)
            : base(value, keyTime) { }

        /// <summary>
        /// Returns the key frame's <see cref="KeyFrameBase{T}.Value"/> property
        /// if <paramref name="keyFrameProgress"/> is 1.0.
        /// Otherwise, returns <paramref name="baseValue"/>.
        /// </summary>
        /// <param name="baseValue">The value to animate from; typically the value of the previous key frame.</param>
        /// <param name="keyFrameProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// </param>
        /// <returns>
        /// <see cref="KeyFrameBase{T}.Value"/>, if <paramref name="keyFrameProgress"/> is 1.0.
        /// <paramref name="baseValue"/> otherwise.
        /// </returns>
        protected override T InterpolateValueCore(T baseValue, double keyFrameProgress)
        {
            return keyFrameProgress == 1d ? this.Value : baseValue;
        }

    }

    /// <summary>
    /// An abstract base class for a key frame which can be associated with an easing function.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="KeyFrameBase{T}.Value"/> property.</typeparam>
    public abstract class EasingKeyFrameBase<T> : KeyFrameBase<T>
    {

        /// <summary>
        /// Identifies the <see cref="EasingFunction"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            nameof(EasingFunction), typeof(IEasingFunction), typeof(EasingKeyFrameBase<T>));

        /// <summary>
        /// Gets or sets the easing function applied to the key frame.
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EasingKeyFrameBase{T}"/> class
        /// with default values.
        /// </summary>
        protected EasingKeyFrameBase()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EasingKeyFrameBase{T}"/> class
        /// with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        protected EasingKeyFrameBase(T value)
            : base(value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EasingKeyFrameBase{T}"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        /// <param name="keyTime">A <see cref="KeyTime"/> value which is associated with this key frame.</param>
        protected EasingKeyFrameBase(T value, KeyTime keyTime)
            : base(value, keyTime) { }

    }

    /// <summary>
    /// An abstract base class for a key frame using splined interpolation.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="KeyFrameBase{T}.Value"/> property.</typeparam>
    public abstract class SplineKeyFrameBase<T> : KeyFrameBase<T>
    {

        /// <summary>
        /// Identifies the <see cref="KeySpline"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register(
            nameof(KeySpline), typeof(KeySpline), typeof(SplineKeyFrameBase<T>), new PropertyMetadata(new KeySpline()));

        /// <summary>
        /// Gets or sets the two control points that define animation progress for this key frame.
        /// </summary>
        public KeySpline KeySpline
        {
            get { return (KeySpline)GetValue(KeySplineProperty); }
            set { SetValue(KeySplineProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplineKeyFrameBase{T}"/> class
        /// with default values.
        /// </summary>
        protected SplineKeyFrameBase()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplineKeyFrameBase{T}"/> class
        /// with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        protected SplineKeyFrameBase(T value)
            : base(value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplineKeyFrameBase{T}"/> class
        /// with the specified values.
        /// </summary>
        /// <param name="value">The value which is associated with this key frame.</param>
        /// <param name="keyTime">A <see cref="KeyTime"/> value which is associated with this key frame.</param>
        protected SplineKeyFrameBase(T value, KeyTime keyTime)
            : base(value, keyTime) { }

    }

}
