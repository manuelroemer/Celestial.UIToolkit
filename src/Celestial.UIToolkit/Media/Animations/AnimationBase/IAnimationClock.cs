using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    // The following interface is only introduced, because we need a way to
    // control an AnimationClock in the Test-Project.
    //
    // The normal AnimationClock cannot be controlled outside of a WPF application,
    // which is why we create an interface layer and a wrapper around it.
    // Each custom animation in this project will then use this interface, instead
    // of the real object.
    //
    // To still access the "real" WPF clocks, we create wrapper/adapter classes, which
    // are used by default.
    // The test project can provide a custom IClock though.

    /// <summary>
    /// Defines an interface for a clock which maintains a timeline's runtime state.
    /// </summary>
    public interface IClock
    {

        /// <summary>
        /// Occurs when this clock's <see cref="CurrentTime"/> becomes invalid.
        /// </summary>
        event EventHandler CurrentTimeInvalidated;

        /// <summary>
        /// Occurs when this clock has completely finished playing.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// Occurs when the clock's speed is updated.
        /// </summary>
        event EventHandler CurrentGlobalSpeedInvalidated;

        /// <summary>
        /// Occurs when the clock's <see cref="CurrentState"/> property is updated.
        /// </summary>
        event EventHandler CurrentStateInvalidated;

        /// <summary>
        /// Occurs when the <see cref="IClockController.Remove"/> method is called on this 
        /// <see cref="IClock"/> or one of its parent clocks.
        /// </summary>
        event EventHandler RemoveRequested;

        /// <summary>
        /// Gets the <see cref="Timeline"/> from which this <see cref="Clock"/> was craeted.
        /// </summary>
        Timeline Timeline { get; }

        /// <summary>
        /// Gets the clock that is the parent of this clock.
        /// </summary>
        Clock Parent { get; }

        /// <summary>
        /// Gets the natural duration of this clock's <see cref="Timeline"/>.
        /// </summary>
        Duration NaturalDuration { get; }

        /// <summary>
        /// Gets a value that indicates whether this <see cref="IClock"/> or any of its parents, is paused.
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// Gets a value that indicates whether this <see cref="IClock"/> is part of a controllable clock tree.
        /// </summary>
        bool HasControllableRoot { get; }

        /// <summary>
        /// Gets a value indicating whether the clock is currently <see cref="ClockState.Active"/>,
        /// <see cref="ClockState.Filling"/>, or <see cref="ClockState.Stopped"/>.
        /// </summary>
        ClockState CurrentState { get; }

        /// <summary>
        /// Gets the current progress of this <see cref="Clock"/> within
        /// its current iteration.
        /// </summary>
        double? CurrentProgress { get; }

        /// <summary>
        /// Gets this clock's current time within its current iteration.
        /// </summary>
        TimeSpan? CurrentTime { get; }

        /// <summary>
        /// Get the current iteration of this clock.
        /// </summary>
        int? CurrentIteration { get; }

        /// <summary>
        /// Gets an <see cref="IClockController"/> that can be used to start,
        /// pause, resume, seek, skip, stop, or remove this <see cref="IClock"/>.
        /// </summary>
        IClockController Controller { get; }

        /// <summary>
        /// Gets the rate at which the clock's time is currently progressing, compared to real-world time.
        /// </summary>
        double? CurrentGlobalSpeed { get; }

    }

    /// <summary>
    /// Interactively controls an <see cref="IClock"/>.
    /// </summary>
    public interface IClockController
    {

        /// <summary>
        /// Gets the <see cref="IClock"/> controlled by this <see cref="IClockController"/>.
        /// </summary>
        IClock Clock { get; }

        /// <summary>
        /// Gets or sets the interactive speed of the target <see cref="IClock"/>.
        /// </summary>
        double SpeedRatio { get; set; }

        /// <summary>
        /// Sets the target <see cref="IClock"/> to begin at the next tick.
        /// </summary>
        void Begin();

        /// <summary>
        /// Stops the target <see cref="IClock"/> from progressing.
        /// </summary>
        void Pause();

        /// <summary>
        /// Removes the <see cref="IClock"/> associated with this <see cref="IClockController"/>
        /// from the properties it animates. The clock and its child clocks will no longer
        /// affect these properties.
        /// </summary>
        void Remove();

        /// <summary>
        /// Enables a <see cref="IClock"/> that was previously paused to resume progressing.
        /// </summary>
        void Resume();

        /// <summary>
        /// Seeks the target <see cref="IClock"/> by the
        /// specified amount when the next tick occurs. If the target clock is stopped, seeking
        /// makes it active again.
        /// </summary>
        /// <param name="offset">
        /// The seek offset, measured in the target clock's time. This offset is relative
        /// to the clock's <see cref="TimeSeekOrigin.BeginTime"/> or <see cref="TimeSeekOrigin.Duration"/>,
        /// depending on the value of origin.
        /// </param>
        /// <param name="origin">
        /// A value that indicates whether the specified offset is relative to the target
        /// clock's <see cref="TimeSeekOrigin.BeginTime"/> or <see cref="TimeSeekOrigin.Duration"/>.
        /// </param>
        void Seek(TimeSpan offset, TimeSeekOrigin origin);

        /// <summary>
        /// Seeks the target <see cref="IClock"/> by the specified amount
        /// immediately. If the target clock is stopped, seeking makes it active again.
        /// </summary>
        /// <param name="offset">
        /// The seek offset, measured in the target clock's time. This offset is relative
        /// to the clock's <see cref="TimeSeekOrigin.BeginTime"/> or <see cref="TimeSeekOrigin.Duration"/>,
        /// depending on the value of origin.
        /// </param>
        /// <param name="origin">
        /// A value that indicates whether the specified offset is relative to the target
        /// clock's <see cref="TimeSeekOrigin.BeginTime"/> or <see cref="TimeSeekOrigin.Duration"/>.
        /// </param>
        void SeekAlignedToLastTick(TimeSpan offset, TimeSeekOrigin origin);

        /// <summary>
        /// Advances the current time of the target <see cref="IClock"/>
        /// to the end of its active period.
        /// </summary>
        void SkipToFill();

        /// <summary>
        /// Stops the target <see cref="IClock"/>.
        /// </summary>
        void Stop();

    }

    /// <summary>
    /// Defines an interface for an animation clock which is being used by an animation
    /// to maintain its runtime state.
    /// </summary>
    public interface IAnimationClock : IClock
    {

        /// <summary>
        /// Gets the <see cref="AnimationTimeline"/> that describes the clock's behavior.
        /// </summary>
        new AnimationTimeline Timeline { get; }

        /// <summary>
        /// Gets the current output value of the <see cref="IAnimationClock"/>.
        /// </summary>
        /// <param name="defaultOriginValue">
        /// The origin value provided to the clock if its animation does not have its own
        /// start value. If this clock is the first in a composition chain it will be the
        /// base value of the property being animated; otherwise it will be the value returned
        /// by the previous clock in the chain
        /// </param>
        /// <param name="defaultDestinationValue">
        /// The destination value provided to the clock if its animation does not have its
        /// own destination value. If this clock is the first in a composition chain it will
        /// be the base value of the property being animated; otherwise it will be the value
        /// returned by the previous clock in the chain.
        /// </param>
        /// <returns>
        /// The current value of this <see cref="IAnimationClock"/>.
        /// </returns>
        object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue);

    }

    /// <summary>
    /// An implementation of <see cref="IClock"/>, which
    /// wraps an underlying <see cref="Clock"/> object.
    /// </summary>
    [DebuggerStepThrough]
    internal class ClockAdapter : IClock
    {

        private ClockControllerAdapter _clockControllerAdapter;

        public event EventHandler CurrentTimeInvalidated;
        public event EventHandler Completed;
        public event EventHandler CurrentGlobalSpeedInvalidated;
        public event EventHandler CurrentStateInvalidated;
        public event EventHandler RemoveRequested;
        
        protected internal Clock UnderlyingClock { get; }

        public IClockController Controller
        {
            get
            {
                if (this.UnderlyingClock.Controller == null) return null;
                if (_clockControllerAdapter == null)
                {
                    _clockControllerAdapter = new ClockControllerAdapter(this);
                }
                return _clockControllerAdapter;
            }
        }

        public Timeline Timeline => this.UnderlyingClock.Timeline;

        public Clock Parent => this.UnderlyingClock.Parent;

        public Duration NaturalDuration => this.UnderlyingClock.NaturalDuration;

        public bool IsPaused => this.UnderlyingClock.IsPaused;

        public bool HasControllableRoot => this.UnderlyingClock.HasControllableRoot;

        public ClockState CurrentState => this.UnderlyingClock.CurrentState;

        public double? CurrentProgress => this.UnderlyingClock.CurrentProgress;

        public TimeSpan? CurrentTime => this.UnderlyingClock.CurrentTime;

        public int? CurrentIteration => this.UnderlyingClock.CurrentIteration;

        public double? CurrentGlobalSpeed => this.UnderlyingClock.CurrentGlobalSpeed;

        public ClockAdapter(Clock underlyingClock)
        {
            if (underlyingClock == null) throw new ArgumentNullException(nameof(underlyingClock));
            this.UnderlyingClock = underlyingClock;

            this.UnderlyingClock.CurrentTimeInvalidated += (sender, e) => 
                this.CurrentTimeInvalidated?.Invoke(sender, e);
            this.UnderlyingClock.Completed += (sender, e) =>
                this.Completed?.Invoke(sender, e);
            this.UnderlyingClock.CurrentGlobalSpeedInvalidated += (sender, e) =>
                this.CurrentGlobalSpeedInvalidated?.Invoke(sender, e);
            this.UnderlyingClock.CurrentStateInvalidated += (sender, e) =>
                this.CurrentStateInvalidated?.Invoke(sender, e);
            this.UnderlyingClock.RemoveRequested += (sender, e) =>
                this.RemoveRequested?.Invoke(sender, e);
        }
        
    }

    [DebuggerStepThrough]
    internal class ClockControllerAdapter : IClockController
    {

        protected ClockAdapter UnderlyingClockAdapter { get; }

        protected ClockController UnderlyingClockController
        {
            get => this.UnderlyingClockAdapter.UnderlyingClock.Controller;
        }

        public IClock Clock => this.UnderlyingClockAdapter;

        public ClockControllerAdapter(ClockAdapter clockAdapter)
        {
            if (clockAdapter == null) throw new ArgumentNullException(nameof(clockAdapter));
            this.UnderlyingClockAdapter = clockAdapter;
        }

        public double SpeedRatio
        {
            get => this.UnderlyingClockController.SpeedRatio;
            set => this.UnderlyingClockController.SpeedRatio = value;
        }

        public void Begin() => this.UnderlyingClockController.Begin();

        public void Pause() => this.UnderlyingClockController.Pause();

        public void Remove() => this.UnderlyingClockController.Remove();

        public void Resume() => this.UnderlyingClockController.Resume();

        public void Seek(TimeSpan offset, TimeSeekOrigin origin) => 
            this.UnderlyingClockController.Seek(offset, origin);

        public void SeekAlignedToLastTick(TimeSpan offset, TimeSeekOrigin origin) =>
            this.UnderlyingClockController.SeekAlignedToLastTick(offset, origin);

        public void SkipToFill() =>
            this.UnderlyingClockController.SkipToFill();

        public void Stop() =>
            this.UnderlyingClockController.Stop();
        
    }

    [DebuggerStepThrough]
    internal class AnimationClockAdapter : ClockAdapter, IAnimationClock
    {

        protected new AnimationClock UnderlyingClock => (AnimationClock)base.UnderlyingClock;

        public AnimationClockAdapter(AnimationClock animationClock)
            : base(animationClock) { }

        public new AnimationTimeline Timeline => (AnimationTimeline)base.Timeline;

        public object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue)
        {
            return this.UnderlyingClock.GetCurrentValue(defaultOriginValue, defaultDestinationValue);
        }

    }

}
