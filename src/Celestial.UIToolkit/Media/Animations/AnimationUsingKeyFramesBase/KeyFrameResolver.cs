using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    // This class is following the KeyFrameAnimation algorithm defined here:
    // https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/key-frame-animations-overview#combining-key-times-out-of-order-key-frames
    //
    // When talking time-complexity, this version is doing worse than the official .NET
    // implementation.
    // I am willingly trading a few CPU cycles for a much clearer code-base.

    /// <summary>
    /// Internally being used to turn an unordered collection of key frames with
    /// different types of KeyTimes (TimeSpan, Percent, Uniform, Paced)
    /// into an ordered collection of <see cref="ResolvedKeyFrame"/> instances,
    /// which all have a fixed time.
    /// </summary>
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

        [DebuggerStepThrough]
        public static IReadOnlyList<ResolvedKeyFrame> ResolveKeyFrames(
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
            return new ReadOnlyCollection<ResolvedKeyFrame>(resolver._keyFrames);
        }

        private void ResolveTimeSpanAndPercentKeyFrames()
        {
            for (int i = 0; i < _frameCount; i++)
            {
                KeyTime currentKeyTime = _keyFrames[i].OriginalKeyTime;

                if (currentKeyTime.Type == KeyTimeType.TimeSpan)
                    this.ResolveTimeSpanFrame(i);
                else if (currentKeyTime.Type == KeyTimeType.Percent)
                    this.ResolvePercentFrame(i);
            }
        }

        private void ResolveTimeSpanFrame(int index)
        {
            TimeSpan finalKeyTime = _keyFrames[index].OriginalKeyTime.TimeSpan;
            _keyFrames[index].Resolve(finalKeyTime);
        }

        private void ResolvePercentFrame(int index)
        {
            double percent = _keyFrames[index].OriginalKeyTime.Percent;
            double finalDurationInMSecs = _duration.TotalMilliseconds * percent;
            TimeSpan finalKeyTime = TimeSpan.FromMilliseconds(finalDurationInMSecs);
            _keyFrames[index].Resolve(finalKeyTime);
        }

        private void ResolveLastKeyFrame()
        {
            ResolvedKeyFrame lastFrame = _keyFrames.Last();

            // We will only inside this condition, if the frame's KeyTime is Uniform or Paced.
            if (!lastFrame.IsResolved)
            {
                lastFrame.Resolve(_duration);
            }
        }

        private void ResolveFirstKeyFrame()
        {
            ResolvedKeyFrame firstFrame = _keyFrames.First();
            if (firstFrame.OriginalKeyTime.Type == KeyTimeType.Paced && _frameCount > 1)
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
                return (keyFrame.OriginalKeyTime.Type == KeyTimeType.Uniform ||
                        keyFrame.OriginalKeyTime.Type == KeyTimeType.Paced) &&
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
                keyFrame.OriginalKeyTime.Type == KeyTimeType.Paced &&
                _keyFrames.First() != keyFrame &&
                _keyFrames.Last() != keyFrame);
        }

        private void SortKeyFrames()
        {
            Array.Sort(_keyFrames);
        }

    }

}
