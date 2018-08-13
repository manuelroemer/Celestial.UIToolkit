using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A base class for an animation using key frames.
    /// </summary>
    /// <typeparam name="T">The type of animated object.</typeparam>
    /// <typeparam name="TKeyFrameCollection">The type of collection which holds the animation's key frames.</typeparam>
    [ContentProperty(nameof(KeyFrames))]
    public abstract class AnimationUsingKeyFramesBase<T, TKeyFrameCollection>
        : AnimationBase<T>, IAddChild, IKeyFrameAnimation, ISegmentLengthProvider
        where TKeyFrameCollection : Freezable, IList, IList<KeyFrameBase<T>>, new()
    {

        private TKeyFrameCollection _keyFrames;
        private ResolvedKeyFrame[] _resolvedKeyFrames;
        private bool _areKeyFramesResolved;

        /// <summary>
        /// Gets or sets the list of key frames which define the animation.
        /// </summary>
        public TKeyFrameCollection KeyFrames
        {
            get
            {
                this.ReadPreamble();
                if (_keyFrames == null)
                {
                    if (this.IsFrozen)
                    {
                        _keyFrames = new TKeyFrameCollection();
                    }
                    else
                    {
                        this.KeyFrames = new TKeyFrameCollection();
                    }
                }
                return _keyFrames;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (ReferenceEquals(value, _keyFrames)) return;
                this.WritePreamble();

                this.OnFreezablePropertyChanged(_keyFrames, value);
                _keyFrames = value;
                this.WritePostscript();
            }
        }

        IList IKeyFrameAnimation.KeyFrames
        {
            get => this.KeyFrames;
            set => this.KeyFrames = (TKeyFrameCollection)value;
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
                this.ResolveKeyTimes();
            return freezing;
        }

        /// <summary>
        /// Makes this instance a clone (deep copy) of the specified <paramref name="sourceFreezable"/>
        /// using base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void CloneCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            this.CloneMembers(source, false);
        }

        /// <summary>
        /// Makes the instance a modifiable clone (deep copy) of the specified <paramref name="sourceFreezable"/>
        /// using current property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to be cloned.</param>
        protected override void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            this.CloneMembers(source, true);
        }

        /// <summary>
        /// Makes the instance a frozen clone of the specified <see cref="Freezable"/> using
        /// base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to copy.</param>
        protected override void GetAsFrozenCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            this.CloneMembers(source, false);
        }

        /// <summary>
        /// Makes the instance a frozen clone of the specified <see cref="Freezable"/> using
        /// base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to copy.</param>
        protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            var source = (AnimationUsingKeyFramesBase<T, TKeyFrameCollection>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            this.CloneMembers(source, true);
        }

        private void CloneMembers(
            AnimationUsingKeyFramesBase<T, TKeyFrameCollection> source,
            bool cloneCurrentValue)
        {
            _areKeyFramesResolved = source._areKeyFramesResolved;
            _keyFrames = cloneCurrentValue ? (TKeyFrameCollection)source._keyFrames.CloneCurrentValue() :
                                             (TKeyFrameCollection)source._keyFrames.Clone();
            this.OnFreezablePropertyChanged(null, _keyFrames);

            // We can't simply use Array.Clone() or sth. similar, since ResolvedKeyFrame is a non-freezable class.
            // This forces us to resolve the whole array again when cloning.
            this.ResolveKeyTimes();
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
            this.ReadPreamble();
            return _keyFrames != null && ((IList)_keyFrames).Count > 0;
        }

        void IAddChild.AddChild(object child) => this.AddChild(child);

        void IAddChild.AddText(string text) => this.AddText(text);

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
                $"The {this.GetType().FullName} doesn't support adding textual children.");
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
            return new Duration(this.GetDuration());
        }

        protected override T GetCurrentValueCore(T defaultOriginValue, T defaultDestinationValue, AnimationClock animationClock)
        {
            this.ResolveKeyTimes();
            if (_resolvedKeyFrames == null || _resolvedKeyFrames.Length == 0)
                return defaultDestinationValue;

            var currentTime = animationClock.CurrentTime.GetValueOrDefault();
            var currentFrameIndex = this.FindCurrentKeyFrameIndex(currentTime);
            var currentFrame = _resolvedKeyFrames[currentFrameIndex];

            T currentValue;
            if (currentFrame == _resolvedKeyFrames.Last() && currentFrame.IsTimeAfter(currentTime))
            {
                // We are past the last frame.
                currentValue = (T)currentFrame.Value;
            }
            else if (currentFrame.ResolvedKeyTime == currentTime)
            {
                // We are exactly on a frame.
                currentValue = (T)currentFrame.Value;
            }
            else if (currentFrame == _resolvedKeyFrames.First())
            {
                var baseValue = this.IsAdditive ? this.GetZeroValue() : defaultOriginValue;
                double progress = currentFrame.GetProgress(currentTime);

                KeyFrameBase<T> origKeyFrame = (KeyFrameBase<T>)currentFrame.OriginalKeyFrame;
                currentValue = origKeyFrame.InterpolateValue(baseValue, progress);
            }
            else
            {
                // Between two frames
                var previousFrame = _resolvedKeyFrames[currentFrameIndex - 1];
                T baseValue = (T)previousFrame.Value;

                var timeDiff = currentTime - previousFrame.ResolvedKeyTime;
                var fullDuration = currentFrame.ResolvedKeyTime - previousFrame.ResolvedKeyTime;

                double progress = timeDiff.TotalMilliseconds / fullDuration.TotalMilliseconds;

                KeyFrameBase<T> origKeyFrame = (KeyFrameBase<T>)currentFrame.OriginalKeyFrame;
                currentValue = origKeyFrame.InterpolateValue(baseValue, progress);
            }

            if (this.IsCumulative)
            {
                double factor = animationClock.CurrentIteration.GetValueOrDefault() - 1;
                if (factor > 0d)
                {
                    T scaledValue = this.ScaleValues((T)currentFrame.Value, factor);
                    currentValue = this.AddValues(currentValue, scaledValue);
                }
            }

            if (this.IsAdditive)
            {
                this.AddValues(defaultOriginValue, currentValue);
            }

            return currentValue;
        }
        
        private int FindCurrentKeyFrameIndex(TimeSpan currentTime)
        {
            if (_resolvedKeyFrames == null || _resolvedKeyFrames.Length == 0)
            {
                throw new InvalidOperationException(
                    "The animation's resolved key frames did not contain any frames. " +
                    "Unable to find a correct one for the specified time.");
            }

            // Find the first frame whose time is >= currentTime, but
            // choose the last frame of a set which have the same time.
            for (int i = 0; i < _resolvedKeyFrames.Length; i++)
            {
                var currentFrame = _resolvedKeyFrames[i];
                var nextFrame = i < _resolvedKeyFrames.Length - 1 ? _resolvedKeyFrames[i + 1] : null;
                bool nextFrameEqualsCurrent = nextFrame != null &&
                                              nextFrame.ResolvedKeyTime == currentFrame.ResolvedKeyTime;

                if (currentFrame.IsTimeBeforeOrInside(currentTime) && !nextFrameEqualsCurrent)
                {
                    return i;
                }
            }
            return _resolvedKeyFrames.Length - 1;
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
        protected abstract T ScaleValues(T value, double factor);

        /// <summary>
        /// Calculates the distance between the two specified objects.
        /// These objects are required to be of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="from">The origin element for the distance measurement.</param>
        /// <param name="to">The destination element for the distance measurement.</param>
        /// <returns>
        /// The distance between the two elements, as double.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="from"/> or <paramref name="to"/> are not of type
        /// <typeparamref name="T"/>.
        /// </exception>
        public double GetSegmentLength(object from, object to)
        {
            if (from.GetType() != typeof(T) ||
                to.GetType() != typeof(T))
            {
                throw new ArgumentException(
                    $"The parameters {nameof(from)} and {nameof(to)} must both be of " +
                    $"type {typeof(T).FullName}.");
            }
            return this.GetSegmentLengthCore((T)from, (T)to);
        }

        /// <summary>
        /// Calculates the distance between the two elements as a double.
        /// </summary>
        /// <param name="from">The origin element for the distance measurement.</param>
        /// <param name="to">The destination element for the distance measurement.</param>
        /// <returns>
        /// The distance between the two elements, as double.
        /// </returns>
        protected abstract double GetSegmentLengthCore(T from, T to);

        private void ResolveKeyTimes()
        {
            if (_areKeyFramesResolved) return;
            _resolvedKeyFrames = KeyFrameResolver.ResolveKeyFrames(
                _keyFrames, this.GetDuration(), this);
            _areKeyFramesResolved = true;
        }

        private TimeSpan GetDuration()
        {
            if (this.Duration != Duration.Automatic &&
                this.Duration != Duration.Forever &&
                this.Duration.HasTimeSpan)
            {
                return this.Duration.TimeSpan;
            }
            else
            {
                return this.GetDurationBasedOnKeyFrames();
            }
        }

        private TimeSpan GetDurationBasedOnKeyFrames()
        {
            TimeSpan duration = TimeSpan.Zero;
            foreach (IKeyFrame frame in _keyFrames)
            {
                if (frame.KeyTime.Type == KeyTimeType.TimeSpan &&
                    frame.KeyTime.TimeSpan > duration)
                {
                    duration = frame.KeyTime.TimeSpan;
                }
            }

            return duration == TimeSpan.Zero ? TimeSpan.FromSeconds(1)
                                             : duration;
        }
        
    }

    /// <summary>
    /// Used by the <see cref="KeyFrameResolver"/> to calculate
    /// the distance between two elements for a paced animation.
    /// </summary>
    internal interface ISegmentLengthProvider
    {
        double GetSegmentLength(object from, object to);
    }

    // This class is following the KeyFrameAnimation algorithm defined here:
    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/key-frame-animations-overview#combining-key-times-out-of-order-key-frames
    //
    // When talking time-complexity, this version is doing worse than the official .NET
    // implementation.
    // I am willingly trading a few CPU cycles for a much clearer code-base.
    internal sealed class KeyFrameResolver
    {

        // Using an IList instead of IList<T>, to provide backward-compatibility for
        // WPF's *KeyFrameCollection classes, which don't implement the generic interface.
        private int _frameCount;
        private ResolvedKeyFrame[] _keyFrames;
        private TimeSpan _duration;
        private ISegmentLengthProvider _segmentLengthProvider;

        [DebuggerStepThrough]
        private KeyFrameResolver(IList keyFrames, TimeSpan duration, ISegmentLengthProvider segmentLengthProvider)
        {
            _frameCount = keyFrames?.Count ?? 0;
            _keyFrames = new ResolvedKeyFrame[_frameCount];
            _duration = duration;
            _segmentLengthProvider = segmentLengthProvider ?? throw new ArgumentNullException(nameof(segmentLengthProvider));

            for (int i = 0; i < _frameCount; i++)
            {
                IKeyFrame currentFrame = (IKeyFrame)keyFrames[i];
                _keyFrames[i] = new ResolvedKeyFrame(currentFrame);
            }
        }

        public static ResolvedKeyFrame[] ResolveKeyFrames(
            IList keyFrames, TimeSpan duration, ISegmentLengthProvider segmentLengthProvider)
        {
            if (keyFrames == null || keyFrames.Count == 0) return null;

            var resolver = new KeyFrameResolver(keyFrames, duration, segmentLengthProvider);
            resolver.ResolveTimeSpanAndPercentKeyFrames();
            resolver.ResolveLastKeyFrame();
            resolver.ResolveFirstKeyFrame();
            resolver.ResolveUniformKeyFrames();
            resolver.ResolvePacedKeyFrames();
            resolver.SortKeyFrames();
            return resolver._keyFrames;
        }

        private void ResolveTimeSpanAndPercentKeyFrames()
        {
            for (int i = 0; i < _frameCount; i++)
            {
                KeyTime currentKeyTime = _keyFrames[i].KeyTime;

                if (currentKeyTime.Type == KeyTimeType.TimeSpan)
                    this.ResolveTimeSpanFrame(i);
                else if (currentKeyTime.Type == KeyTimeType.Percent)
                    this.ResolvePercentFrame(i);
            }
        }

        private void ResolveTimeSpanFrame(int index)
        {
            TimeSpan finalKeyTime = _keyFrames[index].KeyTime.TimeSpan;
            _keyFrames[index].Resolve(finalKeyTime);
        }

        private void ResolvePercentFrame(int index)
        {
            double percent = _keyFrames[index].KeyTime.Percent;
            double finalDurationInMSecs = _duration.TotalMilliseconds * percent;
            TimeSpan finalKeyTime = TimeSpan.FromMilliseconds(finalDurationInMSecs);
            _keyFrames[index].Resolve(finalKeyTime);
        }

        private void ResolveLastKeyFrame()
        {
            ResolvedKeyFrame lastFrame = _keyFrames[_keyFrames.Length - 1];

            // We will only inside this condition, if the frame's KeyTime is Uniform or Paced.
            if (!lastFrame.IsResolved)
            {
                lastFrame.Resolve(_duration);
            }
        }

        private void ResolveFirstKeyFrame()
        {
            ResolvedKeyFrame firstFrame = _keyFrames[0];
            if (firstFrame.KeyTime.Type == KeyTimeType.Paced && _frameCount > 1)
            {
                firstFrame.Resolve(TimeSpan.Zero);
            }
            // If this is the only frame (_frameCount <= 1), it was already resolved by ResolveLastKeyFrame().
            // Nothing left to do here.
        }

        private void ResolveUniformKeyFrames()
        {
            foreach (var segment in this.GetUniformSegments())
            {
                var startTime = this.GetUniformSegmentStartTime(segment);
                var endTime = this.GetUniformSegmentEndTime(segment);
                var timeDiff = endTime - startTime;
                var timeIncrement = TimeSpan.FromTicks(timeDiff.Ticks / (segment.Count + 1));

                TimeSpan currentTime = startTime + timeIncrement;
                foreach (var keyFrame in segment)
                {
                    keyFrame.Resolve(currentTime);
                    currentTime += timeIncrement;
                }
            }
        }

        private TimeSpan GetUniformSegmentStartTime(ArraySegment<ResolvedKeyFrame> segment)
        {
            // Usually the KeyTime of the previous frame.
            // If there is none, it's 0:0:0.
            if (segment.IncludesFirstArrayItem())
            {
                return TimeSpan.Zero;
            }
            else
            {
                return _keyFrames[segment.Offset - 1].ResolvedKeyTime;
            }
        }

        private TimeSpan GetUniformSegmentEndTime(ArraySegment<ResolvedKeyFrame> segment)
        {
            // Usually the KeyTime of the frame after the segment.
            // If there is none (i.e. the segment's last element is the end), use the global end time.
            if (segment.IncludesLastArrayItem())
            {
                return _duration;
            }
            else
            {
                return _keyFrames[segment.Offset + segment.Count].ResolvedKeyTime;
            }
        }
        
        private IEnumerable<ArraySegment<ResolvedKeyFrame>> GetUniformSegments()
        {
            return _keyFrames.GetGroupSegments(keyFrame =>
            {
                // Only uniform and paced segments are not resolved.
                // According to MSDN, treat Paced key frames as Uniform.
                // !IsResolved only targets the last element in the array (which got resolved earlier).
                return (keyFrame.KeyTime.Type == KeyTimeType.Uniform ||
                        keyFrame.KeyTime.Type == KeyTimeType.Paced) &&
                       !keyFrame.IsResolved;
            });
        }

        private void ResolvePacedKeyFrames()
        {
            var pacedSegments = this.GetRelevantPacedSegments();

            foreach (var pacedSegment in pacedSegments)
            {
                var startIndex = pacedSegment.Offset;
                var startTime = _keyFrames[startIndex - 1].ResolvedKeyTime;
                var segmentLengths = new List<double>(pacedSegment.Count + 1);
                var totalSegmentLength = 0d;
                var from = _keyFrames[startIndex - 1].Value;

                // Calculate the segment lengths for the whole segment. Also include
                // the element after the segment.
                for (int i = pacedSegment.Offset; i < pacedSegment.Offset + pacedSegment.Count; i++)
                {
                    var to = _keyFrames[i].Value;
                    totalSegmentLength += _segmentLengthProvider.GetSegmentLength(from, to);
                    segmentLengths.Add(totalSegmentLength);
                    from = to;
                }

                int frameAfterSegmentIndex = pacedSegment.Offset + pacedSegment.Count;
                var frameAfterSegment = _keyFrames[frameAfterSegmentIndex];
                TimeSpan totalSegmentDuration = frameAfterSegment.ResolvedKeyTime - startTime;
                totalSegmentLength += _segmentLengthProvider.GetSegmentLength(from, frameAfterSegment.Value);

                for (int i = pacedSegment.Offset; i < pacedSegment.Offset + pacedSegment.Count; i++)
                {
                    var currentFrame = _keyFrames[i];
                    var currentSegmentLength = segmentLengths[i - pacedSegment.Offset];

                    currentFrame.Resolve(
                        startTime + TimeSpan.FromMilliseconds(
                            currentSegmentLength / totalSegmentLength * totalSegmentDuration.TotalMilliseconds));
                }
            }
        }

        private IEnumerable<ArraySegment<ResolvedKeyFrame>> GetRelevantPacedSegments()
        {
            // Exclude the arrays head and tail, because these values were already set.
            // This also allows accessing the index before the segments Offset.
            return _keyFrames.GetGroupSegments(keyFrame =>
                keyFrame.KeyTime.Type == KeyTimeType.Paced &&
                _keyFrames.First() != keyFrame &&
                _keyFrames.Last()  != keyFrame);
        }

        private void SortKeyFrames()
        {
            Array.Sort(_keyFrames);
        }

    }

    [DebuggerDisplay("ResolvedKeyTime: {ResolvedKeyTime}, IsResolved: {IsResolved}")]
    [DebuggerStepThrough]
    internal sealed class ResolvedKeyFrame 
        : IKeyFrame, IComparable, IComparable<ResolvedKeyFrame>
    {

        public IKeyFrame OriginalKeyFrame { get; }

        public KeyTime KeyTime
        {
            get => this.OriginalKeyFrame.KeyTime;
            set => this.OriginalKeyFrame.KeyTime = value;
        }

        public object Value
        {
            get => this.OriginalKeyFrame.Value;
            set => this.OriginalKeyFrame.Value = value;
        }

        public TimeSpan ResolvedKeyTime { get; private set; }
        
        public bool IsResolved { get; private set; }

        public ResolvedKeyFrame(IKeyFrame originalKeyFrame)
        {
            this.OriginalKeyFrame = originalKeyFrame;
        }

        public void Resolve(TimeSpan resolvedKeyTime)
        {
            this.ResolvedKeyTime = resolvedKeyTime;
            this.IsResolved = true;
        }
        
        public int CompareTo(object obj)
        {
            if (!(obj is ResolvedKeyFrame otherFrame))
                throw new ArgumentException($"{nameof(obj)} must be another {nameof(ResolvedKeyFrame)}.");
            return this.CompareTo(otherFrame);
        }

        public int CompareTo(ResolvedKeyFrame otherFrame)
        {
            return this.ResolvedKeyTime.CompareTo(otherFrame.ResolvedKeyTime);
        }

        public bool IsTimeAfter(TimeSpan time)
        {
            return time > this.ResolvedKeyTime;
        }

        public bool IsTimeBeforeOrInside(TimeSpan time)
        {
            return time <= this.ResolvedKeyTime;
        }
        
        public double GetProgress(TimeSpan time)
        {
            return time.TotalMilliseconds / this.ResolvedKeyTime.TotalMilliseconds;
        }

    }

}
