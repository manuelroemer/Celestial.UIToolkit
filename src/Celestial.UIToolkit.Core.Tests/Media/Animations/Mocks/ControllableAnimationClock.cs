using Celestial.UIToolkit.Media.Animations;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    /// <summary>
    /// An implementation of the <see cref="IAnimationClock"/> interface which supports instant
    /// property changes.
    /// </summary>
    public sealed class ControllableAnimationClock : IAnimationClock
    {

        public static ControllableAnimationClock Started { get; } = new ControllableAnimationClock()
        {
            CurrentProgress = 0d
        };

        public static ControllableAnimationClock Finished { get; } = new ControllableAnimationClock()
        {
            CurrentProgress = 1d
        };

        public event EventHandler Completed;
        public event EventHandler RemoveRequested;
        public event EventHandler CurrentTimeInvalidated;
        public event EventHandler CurrentStateInvalidated;
        public event EventHandler CurrentGlobalSpeedInvalidated;

        public IClock Parent { get; set; }

        Timeline IClock.Timeline => Timeline;

        public AnimationTimeline Timeline { get; set; }

        public TimeSpan? CurrentTime { get; set; }

        public double? CurrentProgress { get; set; }

        public int? CurrentIteration { get; set; }

        public ClockState CurrentState { get; set; }

        public bool IsPaused { get; set; }

        public Duration NaturalDuration { get; set; }

        public bool HasControllableRoot { get; set; }

        public IClockController Controller { get; set; }

        public double? CurrentGlobalSpeed { get; set; }

        public object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue)
        {
            // This method cannot work, since the AnimationTimeline instances require a real
            // AnimationClock instance.
            throw new NotSupportedException();
        }

        public void RaiseCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseRemoveRequested()
        {
            RemoveRequested?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseCurrentTimeInvalidated()
        {
            CurrentTimeInvalidated?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseCurrentStateInvalidated()
        {
            CurrentStateInvalidated?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseCurrentGlobalSpeedInvalidated()
        {
            CurrentGlobalSpeedInvalidated?.Invoke(this, EventArgs.Empty);
        }

    }

}
