using System;
using System.Windows;
using System.Windows.Media.Animation;
using Celestial.UIToolkit.Media.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Celestial.UIToolkit.Tests.Media.Animations.AnimationClocks;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    [TestClass]
    public class FromToByAnimationTests
    {

        internal readonly TimeSpan Duration = TimeSpan.FromSeconds(10);
        internal const double DefaultOrigin = 0;
        internal const double DefaultDestination = 100;
        internal const double From = 200;
        internal const double To = 300;
        internal const double By = 400;

        private DoubleFromToByAnimation _automaticAnimation,
                                        _fromAnimation,
                                        _toAnimation,
                                        _byAnimation,
                                        _fromToAnimation,
                                        _fromByAnimation;

        [TestInitialize]
        public void Initialize()
        {
            // These are all variations, in which values can be set.
            _automaticAnimation = new DoubleFromToByAnimation() { Duration = Duration };
            _fromAnimation      = new DoubleFromToByAnimation() { Duration = Duration, From = From };
            _toAnimation        = new DoubleFromToByAnimation() { Duration = Duration, To = To };
            _byAnimation        = new DoubleFromToByAnimation() { Duration = Duration, By = By };
            _fromToAnimation    = new DoubleFromToByAnimation() { Duration = Duration, From = From, To = To};
            _fromByAnimation    = new DoubleFromToByAnimation() { Duration = Duration, From = From, By = By};
        }

        [TestMethod]
        public void FromAnimationReturnsExpectedValues()
        {
            this.TestAnimationResultRange(_fromAnimation, From, DefaultDestination);
        }

        [TestMethod]
        public void ToAnimationReturnsExpectedValues()
        {
            this.TestAnimationResultRange(_toAnimation, DefaultOrigin, To);
        }

        [TestMethod]
        public void ByAnimationReturnsExpectedValues()
        {
            this.TestAnimationResultRange(_byAnimation, DefaultOrigin, DefaultOrigin + By);
        }

        [TestMethod]
        public void FromToAnimationReturnsExpectedValues()
        {
            this.TestAnimationResultRange(_fromToAnimation, From, To);
        }

        [TestMethod]
        public void FromByAnimationReturnsExpectedValues()
        {
            this.TestAnimationResultRange(_fromByAnimation, From, From + By);
        }

        private void TestAnimationResultRange(
            DoubleFromToByAnimation animation,
            double expectedStartValue,
            double expectedEndValue)
        {
            // Calls the GetCurrentValue() for the complete range of progress values.
            const double progressIncrement = 0.01;
            double startEndDiff = expectedEndValue - expectedStartValue;

            for (double progress = 0; progress <= 1.0; progress += progressIncrement)
            {
                double currentExpectedValue = expectedStartValue + startEndDiff * progress;
                var animationClock = GetClockWithProgress(animation, progress);
                TestAnimationResult(animation, currentExpectedValue, animationClock);
            }
        }

        private void TestAnimationResult(
            DoubleFromToByAnimation animation,
            double expectedValue,
            AnimationClock clock)
        {
            var animResult = (double)animation.GetCurrentValue(DefaultOrigin, DefaultDestination, clock);
            Assert.AreEqual(
                Math.Round(expectedValue, 2), 
                Math.Round(animResult, 2));
            // Round the results, because otherwise, the assert can fail for irrelevant differences
            // due to the floating number nature. (e.g. 160.9999999997 != 161)
        }

    }
    
    // Using double values for a test animation, since they are very easy to understand/implement.
    public class DoubleFromToByAnimation : FromToByAnimationBase<double>
    {
        
        protected override Freezable CreateInstanceCore() => new DoubleFromToByAnimation();

        protected override double AddValues(double a, double b) => a + b;

        protected override double InterpolateValue(double from, double to, double progress) =>
            from + (to - from) * progress;

        protected override double ScaleValue(double value, double factor) => value * factor;

        protected override double SubtractValues(double a, double b) => a - b;
    }
    
}
