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

        public ControllableAnimationClock()
            : this(null) { }

        public ControllableAnimationClock(double? progress)
        {
            CurrentProgress = progress;
        }

        /// <summary>
        /// Returns a new <see cref="ControllableAnimationClock"/> instance whose
        /// <see cref="CurrentProgress"/> is set to 0.
        /// </summary>
        public static ControllableAnimationClock NewStarted()
        {
            return new ControllableAnimationClock(0d);
        }

        /// <summary>
        /// Returns a new <see cref="ControllableAnimationClock"/> instance whose
        /// <see cref="CurrentProgress"/> is set to 1.
        /// </summary>
        public static ControllableAnimationClock NewFinished()
        {
            return new ControllableAnimationClock(1d);
        }

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
