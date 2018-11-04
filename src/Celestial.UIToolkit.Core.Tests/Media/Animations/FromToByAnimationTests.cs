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
        public void EmptyAnimationReturnsDefaultDestinationOnceFinished()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = new ControllableAnimationClock(animation);

            var d = clock.CurrentProgress;
            var x = clock.CurrentTime;
        }

    }

}
