using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Xunit;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class AnimationBaseTests
    {

        [Theory]
        [InlineData(123, 0d)]
        [InlineData(0d, 123)]
        [InlineData("Invalid", 1d)]
        [InlineData("Invalid", "Also invalid")]
        public void ThrowsIfInputValuesHaveWrongType(object from, object to)
        {
            var animation = new SimpleDoubleAnimation();
            var clock = new ControllableAnimationClock();

            Assert.Throws<InvalidOperationException>(() =>
                animation.GetCurrentValue(from, to, clock));
        }

        [Fact]
        public void ThrowsIfAnimationClockIsNull()
        {
            var animation = new SimpleDoubleAnimation();
            AnimationClock clock = null;

            Assert.Throws<ArgumentNullException>(() =>
                animation.GetCurrentValue(0d, 0d, clock));
        }

        [Fact]
        public void HasCorrectTargetType()
        {
            var animation = new SimpleDoubleAnimation();
            Assert.Equal(typeof(double), animation.TargetPropertyType);
        }

    }

}
