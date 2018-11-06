using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using System.Windows.Media.Animation;
using Xunit;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class EasingFromToByAnimationTests
    {

        [Fact]
        public void AppliesEasingFunction()
        {
            const double defaultOrigin = 0;
            const double defaultDestination = 100;
            var defaultAnimation = new FromToByDoubleAnimation();
            var easingAnimation = new FromToByDoubleAnimation();
            var clock = new ControllableAnimationClock();
            var easingFunction = new QuadraticEase();

            easingAnimation.EasingFunction = easingFunction;

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                // Manually apply the easing function.
                double easedProgress = easingFunction.Ease(progress);
                clock.CurrentProgress = easedProgress;
                double expectedResult = defaultAnimation.GetCurrentValue(defaultOrigin, defaultDestination, clock);

                // Then test if the animation does it on its own.
                clock.CurrentProgress = progress;
                double easedResult = easingAnimation.GetCurrentValue(defaultOrigin, defaultDestination, clock);

                Assert.Equal(expectedResult, easedResult);
            }
        }

    }

}
