using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class FromToByAnimationTests
    {

        [Fact]
        public void EmptyAnimationReturnsDefaultDestinationWhenFinished()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.Started;
            double from = 50.0;
            double to = 100.0;

            double result = animation.GetCurrentValue(from, to, clock);
            Assert.Equal(from, result);
        }

        [Fact]
        public void EmptyAnimationReturnsDefaultDestinationWhenStarted()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.Finished;
            double from = 50.0;
            double to = 100.0;

            double result = animation.GetCurrentValue(from, to, clock);
            Assert.Equal(to, result);
        }


    }

}
