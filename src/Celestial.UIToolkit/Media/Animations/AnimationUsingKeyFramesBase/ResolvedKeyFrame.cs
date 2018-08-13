using System;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

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
