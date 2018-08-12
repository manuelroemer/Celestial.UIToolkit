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
        : AnimationBase<T>, IAddChild, IKeyFrameAnimation
        where TKeyFrameCollection : Freezable, IList, new()
    {

        private TKeyFrameCollection _keyFrames;
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
            return _keyFrames != null && _keyFrames.Count > 0;
        }

        IList IKeyFrameAnimation.KeyFrames
        {
            get => this.KeyFrames;
            set => this.KeyFrames = (TKeyFrameCollection)value;
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

        private void ResolveKeyTimes()
        {
            int frameCount = _keyFrames?.Count ?? 0;
            throw new NotImplementedException();
        }

    }

    // This class is following the KeyFrameAnimation algorithm defined here:
    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/key-frame-animations-overview#combining-key-times-out-of-order-key-frames
    internal sealed class KeyFrameResolver
    {

        private TimeSpan _totalDuration;
        private IList _keyFrames;
        private ResolvedKeyFrame[] _resolvedKeyFrames;
        private int _frameCount;
        
        public ResolvedKeyFrame[] ResolveKeyFrames(IList keyFrames, TimeSpan totalDuration)
        {
            this.Initialize(keyFrames, totalDuration);
            if (_frameCount == 0) return null;

            this.ResolveTimeSpanAndPercentKeyFrames();
            this.ResolveLastKeyFrame();
            this.ResolveFirstKeyFrame();
            this.ResolveUniformKeyFrames();
            this.ResolvePacedKeyFrames();
            this.SortKeyFrames();

            return _resolvedKeyFrames;
        }

        [DebuggerStepThrough]
        private void Initialize(IList keyFrames, TimeSpan totalDuration)
        {
            _totalDuration = totalDuration;
            _frameCount = keyFrames?.Count ?? 0;
            _keyFrames = keyFrames;
            _resolvedKeyFrames = new ResolvedKeyFrame[_frameCount];

            for (int i = 0; i < _frameCount; i++)
            {
                IKeyFrame currentFrame = (IKeyFrame)_keyFrames[i];
                _resolvedKeyFrames[i] = new ResolvedKeyFrame(currentFrame);
            }
        }

        private void ResolveTimeSpanAndPercentKeyFrames()
        {
            for (int i = 0; i < _frameCount; i++)
            {
                KeyTime currentKeyTime = ((IKeyFrame)_keyFrames[i]).KeyTime;

                if (currentKeyTime.Type == KeyTimeType.TimeSpan)
                    this.ResolveTimeSpanFrame(i);
                else if (currentKeyTime.Type == KeyTimeType.Percent)
                    this.ResolvePercentFrame(i);
            }
        }

        private void ResolveTimeSpanFrame(int index)
        {
            TimeSpan finalKeyTime = ((IKeyFrame)_keyFrames[index]).KeyTime.TimeSpan;
            _resolvedKeyFrames[index].Resolve(finalKeyTime);
        }

        private void ResolvePercentFrame(int index)
        {
            double percent = ((IKeyFrame)_keyFrames[index]).KeyTime.Percent;
            double finalDurationInMSecs = _totalDuration.TotalMilliseconds * percent;
            TimeSpan finalKeyTime = TimeSpan.FromMilliseconds(finalDurationInMSecs);
            _resolvedKeyFrames[index].Resolve(finalKeyTime);
        }

        private void ResolveLastKeyFrame()
        {
            ResolvedKeyFrame lastFrame = _resolvedKeyFrames[_resolvedKeyFrames.Length - 1];

            // We will only inside this condition, if the frame's KeyTime is Uniform or Paced.
            if (!lastFrame.IsResolved)
            {
                lastFrame.Resolve(_totalDuration);
            }
        }

        private void ResolveFirstKeyFrame()
        {
            ResolvedKeyFrame firstFrame = _resolvedKeyFrames[0];
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
                return _resolvedKeyFrames[segment.Offset - 1].ResolvedKeyTime;
            }
        }

        private TimeSpan GetUniformSegmentEndTime(ArraySegment<ResolvedKeyFrame> segment)
        {
            // Usually the KeyTime of the frame after the segment.
            // If there is none (i.e. the segment's last element is the end), use the global end time.
            if (segment.IncludesLastArrayItem())
            {
                return _totalDuration;
            }
            else
            {
                return _resolvedKeyFrames[segment.Offset + segment.Count].ResolvedKeyTime;
            }
        }
        
        private IEnumerable<ArraySegment<ResolvedKeyFrame>> GetUniformSegments()
        {
            return _resolvedKeyFrames.GetGroupSegments(keyFrame =>
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
            if (_frameCount <= 2) return; // Already set by first/last.
        }

        private IEnumerable<ArraySegment<ResolvedKeyFrame>> GetPacedSegments()
        {
            return _resolvedKeyFrames.GetGroupSegments(keyFrame =>
                keyFrame.KeyTime.Type == KeyTimeType.Paced);
        }

        private void SortKeyFrames()
        {
            Array.Sort(_resolvedKeyFrames);
        }

    }

    [DebuggerDisplay("ResolvedKeyTime: {ResolvedKeyTime}, IsResolved: {IsResolved}")]
    [DebuggerStepThrough]
    internal sealed class ResolvedKeyFrame 
        : IKeyFrame, IComparable, IComparable<ResolvedKeyFrame>
    {

        private IKeyFrame _originalKeyFrame;

        public KeyTime KeyTime
        {
            get => _originalKeyFrame.KeyTime;
            set => _originalKeyFrame.KeyTime = value;
        }

        public object Value
        {
            get => _originalKeyFrame.Value;
            set => _originalKeyFrame.Value = value;
        }

        public TimeSpan ResolvedKeyTime { get; private set; }
        
        public bool IsResolved { get; private set; }

        public ResolvedKeyFrame(IKeyFrame originalKeyFrame)
        {
            _originalKeyFrame = originalKeyFrame;
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
        
    }

}
