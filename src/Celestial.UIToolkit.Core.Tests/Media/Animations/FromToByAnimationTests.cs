using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    // Terms used in this class:
    // 
    // - Empty Animation:
    //   An animation which only has the defaultOrigin/defaultDestination values from
    //   the GetCurrentValue() method.
    //   From/To/By are not set.

    public class FromToByAnimationTests
    {
        
        #region Empty Animation

        [Fact]
        public void EmptyAnimationReturnsDefaultOriginWhenStarted()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.Finished;
            double defaultOrigin = 50.0;
            double defaultDestination = 100.0;

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultDestination, result);
        }

        [Fact]
        public void EmptyAnimationReturnsDefaultDestinationWhenFinished()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.Started;
            double defaultOrigin = 50.0;
            double defaultDestination = 100.0;

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultOrigin, result);
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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                To = to
            };
            var clock = ControllableAnimationClock.Started;

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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                To = to
            };
            var clock = ControllableAnimationClock.Finished;

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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                To = to
            };
            var clock = ControllableAnimationClock.Finished;

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;
                double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
                double expected = from + (to - from) * progress;
                Assert.Equal(expected, result);
            }
        }

        #endregion


    }

}
