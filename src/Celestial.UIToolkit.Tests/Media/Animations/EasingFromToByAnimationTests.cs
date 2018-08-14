using System;
using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Celestial.UIToolkit.Tests.Media.Animations.AnimationClocks;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    [TestClass]
    public class EasingFromToByAnimationTests
    {

        readonly TimeSpan Duration = TimeSpan.FromSeconds(10);
        const double From = 0;
        const double To = 100;

        private DoubleFromToByAnimation _defaultAnimation;
        private DoubleFromToByAnimation _easingAnimation;

        [TestInitialize]
        public void Initialize()
        {
            _defaultAnimation = new DoubleFromToByAnimation()
            {
                Duration = new Duration(Duration),
                From = From,
                To = To
            };
            _easingAnimation = (DoubleFromToByAnimation)_defaultAnimation.Clone();
            _easingAnimation.EasingFunction = new QuadraticEase();
        }

        [TestMethod]
        public void AnimationEasesProgress()
        {
            for (double progress = 0d; progress <= 1.0; progress += 0.01)
            {
                double easedProgress = _easingAnimation.EasingFunction.Ease(progress);

                double manuallyEasedValue = (double)_defaultAnimation.GetCurrentValue(
                    From, To, GetClockWithProgress(_defaultAnimation, easedProgress));
                double animationEasedValue = (double)_easingAnimation.GetCurrentValue(
                    From, To, GetClockWithProgress(_easingAnimation, progress));

                Assert.AreEqual(manuallyEasedValue, animationEasedValue, 0.01);
            }
        }

    }

}
