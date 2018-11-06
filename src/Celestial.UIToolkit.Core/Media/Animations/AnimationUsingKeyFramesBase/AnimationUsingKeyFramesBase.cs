using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A base class for an animation using key frames.
    /// </summary>
    /// <typeparam name="T">The type of animated object.</typeparam>
    /// <typeparam name="TKeyFrame">The type of key frame which is being used by this animation.</typeparam>
    /// <typeparam name="TKeyFrameCollection">The type of collection which holds the animation's key frames.</typeparam>
    [ContentProperty(nameof(KeyFrames))]
    public abstract class AnimationUsingKeyFramesBase<T, TKeyFrame, TKeyFrameCollection>
        : AnimationBase<T>, IAddChild, IKeyFrameAnimation, ISegmentLengthProvider
        where TKeyFrame : IInterpolateKeyFrame<T>
        where TKeyFrameCollection : Freezable, IList, IList<TKeyFrame>, new()
    {

        private TKeyFrameCollection _keyFrames;
        private IReadOnlyList<ResolvedKeyFrame<TKeyFrame>> _resolvedKeyFrames;
        private bool _areKeyFramesResolved;

        /// <summary>
        /// Gets or sets the list of key frames which define the animation.
        /// </summary>
        public TKeyFrameCollection KeyFrames
        {
            get
            {
                ReadPreamble();
                if (_keyFrames == null)
                {
                    if (IsFrozen)
                    {
                        _keyFrames = new TKeyFrameCollection();
                    }
                    else
                    {
                        KeyFrames = new TKeyFrameCollection();
                    }
                }
                return _keyFrames;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (ReferenceEquals(value, _keyFrames)) return;
                WritePreamble();

                OnFreezablePropertyChanged(_keyFrames, value);
                _keyFrames = value;
                WritePostscript();
            }
        }
        
        IList IKeyFrameAnimation.KeyFrames
        {
            get => KeyFrames;
            set => KeyFrames = (TKeyFrameCollection)value;
        }

        /// <summary>
        ///     Gets a list of resolved key frames.
        ///     If the key frames haven't been resolved yet, they will be resolved when accessing
        ///     this property.
        ///     
        /// 
        ///     This property is only introduced for unit tests.
        ///     If you want to access the resolved key frames from inside this class,
        ///     use the <see cref="_resolvedKeyFrames"/> field for performance reasons.
        /// </summary>
        internal IReadOnlyList<ResolvedKeyFrame<TKeyFrame>> ResolvedKeyFrames
        {
            get
            {
                ReadPreamble();
                ResolveKeyTimes();
                return _resolvedKeyFrames;
            }
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

        #region Freezable Members

        /// <summary>
        /// Makes this animation unmodifiable or checks if it can be made
        /// unmodifiable.
        /// </summary>
        /// <param name="isChecking">
        /// <c>true</c> to return an indication of whether the object can be frozen (without actually
        /// freezing it); <c>false</c> to actually freeze the object.
        /// </param>
        /// <returns>
        /// If <paramref name="isChecking"/> is <c>true</c>, this method returns 
        /// <c>true</c> if the <see cref="Freezable"/> can be made unmodifiable, 
        /// or <c>false</c> if it cannot be made unmodifiable. 
        /// 
        /// If <paramref name="isChecking"/> is <c>false</c>, this method returns
        /// <c>true</c> if the specified <see cref="Freezable"/>
        /// is now unmodifiable, or <c>false</c> if it cannot be made unmodifiable.
        /// </returns>
        protected override bool FreezeCore(bool isChecking)
        {
            bool freezing = base.FreezeCore(isChecking);
            freezing &= Freeze(_keyFrames, isChecking);

            if (freezing)
                ResolveKeyTimes();
            return freezing;
        }

        /// <summary>
        /// Makes this instance a clone (deep copy) of the specified <paramref name="sourceFreezable"/>
        /// using base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void CloneCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrame, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            CloneMembers(source, false);
        }

        /// <summary>
        /// Makes the instance a modifiable clone (deep copy) of the specified <paramref name="sourceFreezable"/>
        /// using current property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to be cloned.</param>
        protected override void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrame, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            CloneMembers(source, true);
        }

        /// <summary>
        /// Makes the instance a frozen clone of the specified <see cref="Freezable"/> using
        /// base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to copy.</param>
        protected override void GetAsFrozenCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrame, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            CloneMembers(source, false);
        }

        /// <summary>
        /// Makes the instance a frozen clone of the specified <see cref="Freezable"/> using
        /// base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to copy.</param>
        protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrame, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            CloneMembers(source, true);
        }

        private void CloneMembers(
            AnimationUsingKeyFramesBase<T, TKeyFrame, TKeyFrameCollection> source,
            bool cloneCurrentValue)
        {
            _areKeyFramesResolved = source._areKeyFramesResolved;
            if (source._keyFrames != null)
            {
                _keyFrames = cloneCurrentValue ? (TKeyFrameCollection)source._keyFrames.CloneCurrentValue() :
                                                 (TKeyFrameCollection)source._keyFrames.Clone();
            }
            OnFreezablePropertyChanged(null, _keyFrames);

            // We can't simply use Array.Clone() or sth. similar, since ResolvedKeyFrame is a non-freezable class.
            // This forces us to resolve the whole array again when cloning.
            ResolveKeyTimes();
        }

        /// <summary>
        /// Called when the current <see cref="Freezable"/> object is modified.
        /// </summary>
        protected override void OnChanged()
        {
            _areKeyFramesResolved = false;
            base.OnChanged();
        }

        #endregion

        /// <summary>
        /// Returns a value indicating whether the <see cref="KeyFrames"/> property
        /// should be serialized or not.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <see cref="KeyFrames"/> should be serialized; <c>false</c> if not.
        /// </returns>
        public bool ShouldSerializeKeyFrames()
        {
            ReadPreamble();
            return _keyFrames != null && ((IList)_keyFrames).Count > 0;
        }

        void IAddChild.AddChild(object child) => AddChild(child);

        void IAddChild.AddText(string text) => AddText(text);

        /// <summary>
        /// Adds a child to the key frame animation.
        /// </summary>
        /// <param name="child">The child to be added.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException">
        /// Thrown if the <paramref name="child"/> is not of type <typeparamref name="T"/>.
        /// </exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void AddChild(object child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (!(child is T))
                throw new ArgumentException($"The child must be of type {typeof(T).FullName}.", nameof(child));
        }

        /// <summary>
        /// Adds a textual child to the key frame animation.
        /// By default, this throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="text">The textual child to be added.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void AddText(string text)
        {
            throw new InvalidOperationException(
                $"The {GetType().FullName} doesn't support adding textual children.");
        }

        private void ResolveKeyTimes()
        {
            if (_areKeyFramesResolved) return;
            _resolvedKeyFrames = KeyFrameResolver<TKeyFrame>.ResolveKeyFrames(
                _keyFrames, GetTotalInterpolationTime(), this);
            _areKeyFramesResolved = true;
        }
        
        /// <summary>
        /// Returns the length of a single iteration of this <see cref="AnimationTimeline"/>.
        /// </summary>
        /// <param name="clock">
        /// The clock that was created for this <see cref="AnimationTimeline"/>.
        /// </param>
        /// <returns>The animation's natural duration.</returns>
        protected override sealed Duration GetNaturalDurationCore(Clock clock)
        {
            return new Duration(GetTotalInterpolationTime());
        }

        /// <summary>
        /// Returns the length of a single iteration of this animation.
        /// </summary>
        /// <returns>
        /// The length of a single iteration of this animation, as <see cref="TimeSpan"/>.
        /// </returns>
        internal TimeSpan GetTotalInterpolationTime()
        {
            if (Duration != Duration.Automatic &&
                Duration != Duration.Forever &&
                Duration.HasTimeSpan)
            {
                return Duration.TimeSpan;
            }
            else
            {
                return GetDurationBasedOnKeyFrames();
            }
        }

        private TimeSpan GetDurationBasedOnKeyFrames()
        {
            // Return the KeyTime of the largest TimeSpan-KeyFrame.
            // If none is found, the time defaults to 1sec, as defined by the standard WPF algorithm.
            TimeSpan duration = TimeSpan.Zero;
            foreach (IKeyFrame frame in KeyFrames)
            {
                if (frame.KeyTime.Type == KeyTimeType.TimeSpan &&
                    frame.KeyTime.TimeSpan > duration)
                {
                    duration = frame.KeyTime.TimeSpan;
                }
            }
            return duration == TimeSpan.Zero ? TimeSpan.FromSeconds(1) : duration;
        }

        /// <summary>
        ///     Calculates a value that represents the current value of the property being animated, 
        ///     based on the provided <see cref="KeyFrames"/>.
        /// </summary>
        /// <param name="defaultOriginValue">
        ///      The suggested origin value, used if the animation 
        ///      does not have its own explicitly set start value.
        /// </param>
        /// <param name="defaultDestinationValue">
        ///     The suggested destination value. Not used by the animation.
        /// </param>
        /// <param name="animationClock">
        ///     An <see cref="IAnimationClock"/> which can generate the <see cref="IClock.CurrentTime"/>
        ///     or <see cref="IClock.CurrentProgress"/> value to be used by the
        ///     animation to generate its output value.
        /// </param>
        /// <returns>The value this animation believes should be the current value for the property.</returns>
        protected override sealed T GetCurrentValueCore(T defaultOriginValue, T defaultDestinationValue, IAnimationClock animationClock)
        {
            ResolveKeyTimes();
            if (_resolvedKeyFrames == null || _resolvedKeyFrames.Count == 0)
                return defaultDestinationValue;

            TimeSpan currentClockTime = animationClock.CurrentTime.GetValueOrDefault();
            int currentFrameIndex = _resolvedKeyFrames.FindCurrentKeyFrameIndex(currentClockTime);
            var currentFrame = _resolvedKeyFrames[currentFrameIndex];

            T currentValue;
            if (currentFrame == _resolvedKeyFrames.Last() && currentFrame.IsTimeAfterThisFrame(currentClockTime))
            {
                // We are past the last frame.
                currentValue = (T)currentFrame.Value;
            }
            else if (currentFrame.ResolvedKeyTime == currentClockTime)
            {
                // We are exactly on a frame.
                currentValue = (T)currentFrame.Value;
            }
            else if (currentFrame == _resolvedKeyFrames.First())
            {
                var baseValue = IsAdditive ? GetZeroValue() : defaultOriginValue;
                double progress = currentFrame.GetProgress(currentClockTime);
                currentValue = currentFrame.OriginalKeyFrame.InterpolateValue(baseValue, progress);
            }
            else
            {
                // Between two frames.
                var previousFrame = _resolvedKeyFrames.ElementBefore(currentFrameIndex);
                var currentTimeDiff = currentClockTime - previousFrame.ResolvedKeyTime;
                var frameTimeDiff = currentFrame.ResolvedKeyTime - previousFrame.ResolvedKeyTime;
                double progress = currentTimeDiff.TotalMilliseconds / frameTimeDiff.TotalMilliseconds;

                currentValue = currentFrame.OriginalKeyFrame.InterpolateValue((T)previousFrame.Value, progress);
            }

            // IsCumulative => Multiply the final key frame's value by the current repeat count.
            //                 Add the result to the return value.
            if (IsCumulative)
            {
                double factor = animationClock.CurrentIteration.GetValueOrDefault() - 1;
                if (factor > 0d)
                {
                    T scaledValue = ScaleValue((T)_resolvedKeyFrames.Last().Value, factor);
                    currentValue = AddValues(currentValue, scaledValue);
                }
            }

            // IsAdditive => Add the base value to the return value.
            if (IsAdditive)
            {
                currentValue = AddValues(defaultOriginValue, currentValue);
            }
            
            return currentValue;
        }
        
        /// <summary>
        /// Returns an object of type <typeparamref name="T"/> which represents
        /// a zero-value.
        /// For instance, if <typeparamref name="T"/> was <see cref="int"/>, this
        /// would return 0.
        /// </summary>
        /// <returns>A zero-value for the type <typeparamref name="T"/>.</returns>
        protected abstract T GetZeroValue();
        
        /// <summary>
        /// Adds the two specified values and returns the result.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The result of the addition of the two values.</returns>
        protected abstract T AddValues(T a, T b);

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
        /// Calculates the distance between the two specified objects.
        /// These objects are required to be of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="from">The origin element for the distance measurement.</param>
        /// <param name="to">The destination element for the distance measurement.</param>
        /// <returns>
        /// The distance between the two elements, as double.
        /// </returns>
        public double GetDistanceBetween(object from, object to)
        {
            return GetDistanceBetweenCore((T)from, (T)to);
        }

        /// <summary>
        /// Calculates the distance between the two elements as a double.
        /// </summary>
        /// <param name="from">The origin element for the distance measurement.</param>
        /// <param name="to">The destination element for the distance measurement.</param>
        /// <returns>
        /// The distance between the two elements, as double.
        /// </returns>
        protected abstract double GetDistanceBetweenCore(T from, T to);
        
    }

}
