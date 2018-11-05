using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using Xunit;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class FromToByAnimationTests
    {
        
        #region Automatic Animation

        // Automatic animations are animations without a From/To/By value.
        // They simply "get fed" with the defaultOrigin and defaultDestination values.

        [Fact]
        public void AutomaticAnimationReturnsDefaultOriginWhenStarted()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.NewFinished();
            double defaultOrigin = 50.0;
            double defaultDestination = 100.0;

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultDestination, result);
        }

        [Fact]
        public void AutomaticAnimationReturnsDefaultDestinationWhenFinished()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.NewStarted();
            double defaultOrigin = 50.0;
            double defaultDestination = 100.0;

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultOrigin, result);
        }

        [Fact]
        public void AutomaticAnimationReturnsExpectedIntermediaryValues()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            var animation = new FromToByDoubleAnimation();
            var clock = new ControllableAnimationClock();

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;
                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = defaultOrigin + (defaultDestination - defaultOrigin) * progress;
                Assert.Equal(expected, result);
            }
        }

        #endregion

        #region From/To Animation

        [Fact]
        public void FromToAnimationReturnsFromWhenStarted()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            double to = 100d;
            var animation = new FromToByDoubleAnimation(from, to);
            var clock = ControllableAnimationClock.NewStarted();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(from, result);
        }

        [Fact]
        public void FromToAnimationReturnsToWhenStopped()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            double to = 100d;
            var animation = new FromToByDoubleAnimation(from, to);
            var clock = ControllableAnimationClock.NewFinished();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(to, result);
        }

        [Fact]
        public void FromToAnimationReturnsExpectedIntermediaryValues()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            double to = 100d;
            var animation = new FromToByDoubleAnimation(from, to);
            var clock = new ControllableAnimationClock();

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;
                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = from + (to - from) * progress;
                Assert.Equal(expected, result);
            }
        }

        #endregion

        #region From/By Animation

        [Fact]
        public void FromByAnimationReturnsFromWhenStarted()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            double by = 100d;
            var animation = new FromToByDoubleAnimation(from: from, by: by);
            var clock = ControllableAnimationClock.NewStarted();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(from, result);
        }

        [Fact]
        public void FromByAnimationReturnsFromPlusByWhenStopped()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            double by = 100d;
            var animation = new FromToByDoubleAnimation(from: from, by: by);
            var clock = ControllableAnimationClock.NewFinished();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(from + by, result);
        }

        [Fact]
        public void FromByAnimationReturnsExpectedIntermediaryValues()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            double by = 100d;
            var animation = new FromToByDoubleAnimation(from: from, by: by);
            var clock = new ControllableAnimationClock();

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;

                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = from + by * progress;

                Assert.Equal(expected, result);
            }
        }

        #endregion

        #region From Animation

        [Fact]
        public void FromAnimationReturnsFromWhenStarted()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            var animation = new FromToByDoubleAnimation(from);
            var clock = ControllableAnimationClock.NewStarted();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(from, result);
        }

        [Fact]
        public void FromAnimationReturnsFromPlusByWhenStopped()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            var animation = new FromToByDoubleAnimation(from);
            var clock = ControllableAnimationClock.NewFinished();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultDestination, result);
        }

        [Fact]
        public void FromAnimationReturnsExpectedIntermediaryValues()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double from = 50d;
            var animation = new FromToByDoubleAnimation(from);
            var clock = new ControllableAnimationClock();

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;

                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = from + (defaultDestination - from) * progress;

                Assert.Equal(expected, result);
            }
        }

        #endregion

        #region To Animation

        [Fact]
        public void ToAnimationReturnsFromWhenStarted()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double to = 100d;
            var animation = new FromToByDoubleAnimation(to: to);
            var clock = ControllableAnimationClock.NewStarted();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultOrigin, result);
        }

        [Fact]
        public void ToAnimationReturnsFromPlusByWhenStopped()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double to = 100d;
            var animation = new FromToByDoubleAnimation(to: to);
            var clock = ControllableAnimationClock.NewFinished();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(to, result);
        }

        [Fact]
        public void ToAnimationReturnsExpectedIntermediaryValues()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double to = 100d;
            var animation = new FromToByDoubleAnimation(to: to);
            var clock = new ControllableAnimationClock();

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;

                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = defaultOrigin + (to - defaultOrigin) * progress;

                Assert.Equal(expected, result);
            }
        }

        #endregion

        #region By Animation

        [Fact]
        public void ByAnimationReturnsFromWhenStarted()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double by = 100d;
            var animation = new FromToByDoubleAnimation(by: by);
            var clock = ControllableAnimationClock.NewStarted();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultOrigin, result);
        }

        [Fact]
        public void ByAnimationReturnsFromPlusByWhenStopped()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double by = 100d;
            var animation = new FromToByDoubleAnimation(by: by);
            var clock = ControllableAnimationClock.NewFinished();

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultOrigin + by, result);
        }

        [Fact]
        public void ByAnimationReturnsExpectedIntermediaryValues()
        {
            double defaultOrigin = 25d;
            double defaultDestination = 200d;
            double by = 100d;
            var animation = new FromToByDoubleAnimation(by: by);
            var clock = new ControllableAnimationClock();

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;

                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = defaultOrigin + by * progress;

                Assert.Equal(expected, result);
            }
        }

        #endregion

    }

}
