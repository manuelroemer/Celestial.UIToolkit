using System;
using System.Windows;
using System.Windows.Media.Animation;
using Celestial.UIToolkit.Media.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void FromAnimationHasExpectedValues()
        {
            this.TestSetAnimationStageResults(_fromAnimation, From, DefaultDestination);
        }

        private void TestSetAnimationStageResults(
            DoubleFromToByAnimation animation,
            double expectedStartValue,
            double expectedEndValue)
        {
            var clock = (AnimationClock)animation.CreateClock(true);
            clock.Controller.Begin();
            double startEndDiff = expectedStartValue - expectedEndValue;
            
            Assert.AreEqual(
                expectedStartValue,
                animation.GetCurrentValue(DefaultOrigin, DefaultDestination, clock));

            clock.Controller.Seek(Duration, TimeSeekOrigin.BeginTime);
            Assert.AreEqual(
                expectedEndValue,
                animation.GetCurrentValue(DefaultOrigin, DefaultDestination, clock));
        }

    }

    internal class MockedAnimationClock : Clock
    {

        public TimeSpan CurrentTime { get; set; }

        public MockedAnimationClock(AnimationTimeline animation)
            : base(animation) { }

        public MockedAnimationClock(AnimationTimeline animation, TimeSpan currentTime)
            : base(animation)
        {
            this.CurrentTime = currentTime;
        }

        protected override TimeSpan GetCurrentTimeCore()
        {
            return this.CurrentTime;
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
