using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    /// <summary>
    /// Provides quick-access methods for generating <see cref="AnimationClock"/>
    /// objects for tests.
    /// </summary>
    public static class AnimationClocks
    {

        /// <summary>
        /// Returns an <see cref="AnimationClock"/> for the specified <paramref name="timeline"/>
        /// whose <see cref="Clock.CurrentTime"/> is set to <paramref name="time"/>.
        /// </summary>
        /// <param name="timeline">
        /// The animation timeline for which a clock should be retrieved.
        /// </param>
        /// <param name="time">
        /// The expected time of the clock.
        /// </param>
        /// <returns>An <see cref="AnimationClock"/> with the specified values.</returns>
        public static AnimationClock GetClockWithTime(AnimationTimeline timeline, TimeSpan time)
        {
            if (timeline == null) throw new ArgumentNullException(nameof(timeline));

            var clock = (AnimationClock)timeline.CreateClock(true);
            clock.Controller.SeekAlignedToLastTick(time, TimeSeekOrigin.BeginTime);
            return clock;
        }

        /// <summary>
        /// Returns an <see cref="AnimationClock"/> for the specified <paramref name="timeline"/>
        /// whose <see cref="Clock.CurrentProgress"/> is set to <paramref name="progress"/>.
        /// </summary>
        /// <param name="timeline">
        /// The animation timeline for which a clock should be retrieved.
        /// </param>
        /// <param name="progress">
        /// The expected progress of the clock.
        /// A value between 0 and 1.
        /// </param>
        /// <returns>An <see cref="AnimationClock"/> with the specified values.</returns>
        public static AnimationClock GetClockWithProgress(AnimationTimeline timeline, double progress)
        {
            if (timeline == null) throw new ArgumentNullException(nameof(timeline));
            if (!timeline.Duration.HasTimeSpan)
            {
                throw new InvalidOperationException(
                    "The specified timeline doesn't have a TimeSpan duration.");
            }

            TimeSpan progressTime = TimeSpan.FromMilliseconds(
                timeline.Duration.TimeSpan.TotalMilliseconds * progress);
            return GetClockWithTime(timeline, progressTime);
        }

        /// <summary>
        /// Returns an <see cref="AnimationClock"/> with a progress of 0.
        /// </summary>
        /// <param name="timeline">
        /// The animation timeline for which a clock should be retrieved.
        /// </param>
        /// <returns>An <see cref="AnimationClock"/> with has just been started.</returns>
        public static AnimationClock GetStartedClock(AnimationTimeline timeline)
        {
            return GetClockWithProgress(timeline, 0d);
        }

        /// <summary>
        /// Returns an <see cref="AnimationClock"/> with a progress of 1.
        /// </summary>
        /// <param name="timeline">
        /// The animation timeline for which a clock should be retrieved.
        /// </param>
        /// <returns>An <see cref="AnimationClock"/> with has been stopped.</returns>
        public static AnimationClock GetFinishedClock(AnimationTimeline timeline)
        {
            return GetClockWithProgress(timeline, 1d);
        }

    }

}
