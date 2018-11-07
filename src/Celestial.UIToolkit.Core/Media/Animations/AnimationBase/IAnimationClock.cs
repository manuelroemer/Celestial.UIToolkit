using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;

// This file creates wrapper interfaces for the Clock, ClockController and AnimationClock classes
// which are provided by WPF.
// The custom animations defined in this project use these interfaces, instead of the real classes.
// The only reason for this is that we want to be able to UnitTest the animation classes.
// For that, we need a controllable clock.
// The default class provided by WPF cannot be fully controlled in a test-scenario though, since
// it relies on a TimeGiver.
// For instant updates and value changes required by tests, we need more control over it.

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Defines a clock which maintains run-time timing state for a 
    /// <see cref="System.Windows.Media.Animation.Timeline"/>.
    /// </summary>
    public interface IClock
    {

        /// <summary>
        /// Occurs when this clock has completely finished playing.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// Occurs when the <see cref="IClockController.Remove"/> method
        /// is called on this <see cref="IClock"/> or one of its parent clocks.
        /// </summary>
        event EventHandler RemoveRequested;

        /// <summary>
        /// Occurs when this clock's <see cref="CurrentTime"/> becomes invalid.
        /// </summary>
        event EventHandler CurrentTimeInvalidated;

        /// <summary>
        /// Occurs when the clock's <see cref="CurrentState"/> property is updated.
        /// </summary>
        event EventHandler CurrentStateInvalidated;

        /// <summary>
        /// Occurs when the clock's speed is updated.
        /// </summary>
        event EventHandler CurrentGlobalSpeedInvalidated;

        /// <summary>
        /// Gets the <see cref="System.Windows.Media.Animation.Timeline"/> 
        /// from which this <see cref="IClock"/> was created.
        /// </summary>
        Timeline Timeline { get; }

        /// <summary>
        /// Gets the clock that is the parent of this clock.
        /// </summary>
        IClock Parent { get; }

        /// <summary>
        /// Gets the natural duration of this clock's <see cref="Timeline"/>.
        /// </summary>
        Duration NaturalDuration { get; }

        /// <summary>
        /// Gets a value that indicates whether this <see cref="IClock"/>,
        /// or any of its parents, is paused.
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// Gets a value that indicates whether this <see cref="Clock"/>
        /// is part of a controllable clock tree.
        /// </summary>
        bool HasControllableRoot { get; }

        /// <summary>
        /// Gets a value indicating whether the clock is currently <see cref="ClockState.Active"/>,
        /// <see cref="ClockState.Filling"/> or <see cref="ClockState.Stopped"/>.
        /// </summary>
        ClockState CurrentState { get; }

        /// <summary>
        /// Gets the current progress of this <see cref="IClock"/> within
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
        /// Gets the rate at which the clock's time is currently progressing, compared to
        /// real-world time.
        /// </summary>
        double? CurrentGlobalSpeed { get; }
        
    }

    /// <summary>
    /// Represents an object which can interactively control an <see cref="IClock"/>.
    /// </summary>
    public interface IClockController
    {

        /// <summary>
        /// Gets the <see cref="IClock"/> controlled by this <see cref="ClockController"/>.
        /// </summary>
        IClock Clock { get; }

        /// <summary>
        /// Gets or sets the interactive speed of the target <see cref="Clock"/>.
        /// </summary>
        double SpeedRatio { get; set; }

        /// <summary>
        /// Sets the target <see cref="Clock"/> to begin at the next tick.
        /// </summary>
        void Begin();

        /// <summary>
        /// Stops the target <see cref="Clock"/> from progressing.
        /// </summary>
        void Pause();

        /// <summary>
        /// Removes the <see cref="Clock"/> associated with this <see cref="IClockController"/>
        /// from the properties it animates. The clock and its child clocks will no longer
        /// affect these properties.
        /// </summary>
        void Remove();

        /// <summary>
        /// Enables a <see cref="IClock"/> that was previously paused to
        /// resume progressing.
        /// </summary>
        void Resume();

        /// <summary>
        /// Seeks the target <see cref="Clock"/> by the
        /// specified amount when the next tick occurs. If the target clock is stopped, seeking
        /// makes it active again.
        /// </summary>
        /// <param name="offset">
        /// The seek offset, measured in the target clock's time. 
        /// This offset is relative
        /// to the clock's <see cref="TimeSeekOrigin.BeginTime"/> or
        /// <see cref="TimeSeekOrigin.Duration"/>,
        /// depending on the value of origin.
        /// </param>
        /// <param name="origin">
        /// A value that indicates whether the specified offset is relative to the target
        /// clock's <see cref="TimeSeekOrigin.BeginTime"/> or
        /// <see cref="TimeSeekOrigin.Duration"/>,
        /// </param>
        void Seek(TimeSpan offset, TimeSeekOrigin origin);

        /// <summary>
        /// Seeks the target <see cref="Clock"/> by the specified amount
        /// immediately. If the target clock is stopped, seeking makes it active again.
        /// </summary>
        /// <param name="offset">
        /// The seek offset, measured in the target clock's time. This offset is relative
        /// to the clock's <see cref="TimeSeekOrigin.BeginTime"/> or 
        /// <see cref="TimeSeekOrigin.Duration"/>,
        /// depending on the value of origin.
        /// </param>
        /// <param name="origin">
        /// A value that indicates whether the specified offset is relative to the target
        /// clock's <see cref="TimeSeekOrigin.BeginTime"/> or 
        /// <see cref="TimeSeekOrigin.Duration"/>.
        /// </param>
        void SeekAlignedToLastTick(TimeSpan offset, TimeSeekOrigin origin);

        /// <summary>
        /// Advances the current time of the target <see cref="Clock"/>
        /// to the end of its active period.
        /// </summary>
        void SkipToFill();

        /// <summary>
        /// Stops the target <see cref="Clock"/>.
        /// </summary>
        void Stop();

    }

    /// <summary>
    /// Defines a clock which maintains the run-time state of an <see cref="AnimationTimeline"/>
    /// and processes its output values.
    /// </summary>
    public interface IAnimationClock : IClock
    {

        /// <summary>
        /// Gets the <see cref="AnimationTimeline"/> that describes this
        /// clock's behavior.
        /// </summary>
        new AnimationTimeline Timeline { get; }

        /// <summary>
        ///     Gets the current output value of the <see cref="IAnimationClock"/>.
        /// </summary>
        /// <param name="defaultOriginValue">
        ///     The origin value provided to the clock if its animation does not have its own
        ///     start value. If this clock is the first in a composition chain it will be the
        ///     base value of the property being animated; otherwise it will be the value returned
        ///     by the previous clock in the chain
        /// </param>
        /// <param name="defaultDestinationValue">
        ///     The destination value provided to the clock if its animation does not have its
        ///     own destination value.If this clock is the first in a composition chain it will
        ///     be the base value of the property being animated; otherwise it will be the value
        ///     returned by the previous clock in the chain
        /// </param>
        /// <returns>
        ///     The current value of this <see cref="IAnimationClock"/>.
        /// </returns>
        object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue);

    }



    [DebuggerStepThrough]
    internal class ClockAdapter : IClock
    {

        private Clock _clock;
        private ClockAdapter _parent;
        private IClockController _clockController;

        public event EventHandler Completed;
        public event EventHandler RemoveRequested;
        public event EventHandler CurrentTimeInvalidated;
        public event EventHandler CurrentStateInvalidated;
        public event EventHandler CurrentGlobalSpeedInvalidated;

        public ClockAdapter(Clock clock)
        {
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));

            if (_clock.Parent != null)
                _parent = new ClockAdapter(_clock.Parent);
            if (clock.Controller != null)
                _clockController = new ClockControllerAdapter(this, _clock.Controller);

            _clock.Completed += (sender, e) => Completed?.Invoke(sender, e);
            _clock.RemoveRequested += (sender, e) => RemoveRequested?.Invoke(sender, e);
            _clock.CurrentTimeInvalidated += (sender, e) => CurrentTimeInvalidated?.Invoke(sender, e);
            _clock.CurrentStateInvalidated += (sender, e) => CurrentStateInvalidated?.Invoke(sender, e);
            _clock.CurrentGlobalSpeedInvalidated += (sender, e) => CurrentGlobalSpeedInvalidated?.Invoke(sender, e);
        }

        public IClockController Controller => _clockController;

        public IClock Parent => _parent;

        public Timeline Timeline => _clock.Timeline;

        public Duration NaturalDuration => _clock.NaturalDuration;

        public bool IsPaused => _clock.IsPaused;

        public bool HasControllableRoot => _clock.HasControllableRoot;

        public ClockState CurrentState => _clock.CurrentState;

        public double? CurrentProgress => _clock.CurrentProgress;

        public TimeSpan? CurrentTime => _clock.CurrentTime;

        public int? CurrentIteration => _clock.CurrentIteration;

        public double? CurrentGlobalSpeed => _clock.CurrentGlobalSpeed;
        
    }

    [DebuggerStepThrough]
    internal sealed class ClockControllerAdapter : IClockController
    {

        private ClockController _clockController;

        public ClockControllerAdapter(IClock clock, ClockController clockController)
        {
            Clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _clockController = clockController ?? throw new ArgumentNullException(nameof(clockController));
        }

        public IClock Clock { get; }

        public double SpeedRatio
        {
            get => _clockController.SpeedRatio;
            set => _clockController.SpeedRatio = value;
        }

        public void Begin() => _clockController.Begin();

        public void Pause() => _clockController.Pause();

        public void Remove() => _clockController.Remove();

        public void Resume() => _clockController.Resume();

        public void Seek(TimeSpan offset, TimeSeekOrigin origin) => 
            _clockController.Seek(offset, origin);

        public void SeekAlignedToLastTick(TimeSpan offset, TimeSeekOrigin origin) => 
            _clockController.SeekAlignedToLastTick(offset, origin);

        public void SkipToFill() => _clockController.SkipToFill();

        public void Stop() => _clockController.Stop();

    }

    [DebuggerStepThrough]
    internal sealed class AnimationClockAdapter : ClockAdapter, IAnimationClock
    {

        private AnimationClock _animationClock;

        public AnimationClockAdapter(AnimationClock animationClock)
            : base(animationClock)
        {
            _animationClock = animationClock ?? throw new ArgumentNullException(nameof(animationClock));
        }

        public new AnimationTimeline Timeline => _animationClock.Timeline;

        public object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue) =>
            _animationClock.GetCurrentValue(defaultOriginValue, defaultDestinationValue);

    }

}
