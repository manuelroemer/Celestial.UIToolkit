using System;
using System.Diagnostics;
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
        internal const double DefaultOrigin = 250;
        internal const double DefaultDestination = 1000;
        internal const double From = 200;
        internal const double To = 300;
        internal const double By = 400;

        private DoubleFromToByAnimation _automaticAnimation,
                                        _fromAnimation,
                                        _toAnimation,
                                        _byAnimation,
                                        _toByAnimation,
                                        _fromToAnimation,
                                        _fromByAnimation,
                                        _fromToByAnimation;
        private DoubleFromToByAnimation[] _allAnimations;

        [TestInitialize]
        public void Initialize()
        {
            // These are all variations, in which values can be set.

            // IsAdditive must not influence the first 4 animations, which is why we can already set it to true here,
            // as the results should not be different than if it was false.
            _automaticAnimation = new DoubleFromToByAnimation() { Duration = Duration,              IsAdditive = true };
            _fromAnimation      = new DoubleFromToByAnimation() { Duration = Duration, From = From, IsAdditive = true };
            _toAnimation        = new DoubleFromToByAnimation() { Duration = Duration, To = To,     IsAdditive = true };
            _byAnimation        = new DoubleFromToByAnimation() { Duration = Duration, By = By,     IsAdditive = true};

            // These animations have an invalid set of properties. If both To and By are set,
            // By should be ignored.
            _toByAnimation     = new DoubleFromToByAnimation() { Duration = Duration,              To = To, By = By, IsAdditive = true };
            _fromToByAnimation = new DoubleFromToByAnimation() { Duration = Duration, From = From, To = To, By = By };
            
            _fromToAnimation    = new DoubleFromToByAnimation() { Duration = Duration, From = From, To = To };
            _fromByAnimation    = new DoubleFromToByAnimation() { Duration = Duration, From = From, By = By };

            _allAnimations = new DoubleFromToByAnimation[]
            {
                _automaticAnimation,
                _fromAnimation,
                _toAnimation,
                _byAnimation,
                _fromToAnimation,
                _fromByAnimation
            };
        }

        [TestMethod]
        public void AnimationsReturnExpectedValues()
        {
            this.TestAnimationResultRange(_automaticAnimation, DefaultOrigin, DefaultDestination);
            this.TestAnimationResultRange(_fromAnimation, From, DefaultDestination);
            this.TestAnimationResultRange(_toAnimation, DefaultOrigin, To);
            this.TestAnimationResultRange(_byAnimation, DefaultOrigin, DefaultOrigin + By);

            this.TestAnimationResultRange(_fromToAnimation, From, To);
            this.TestAnimationResultRange(_fromByAnimation, From, From + By);

            this.TestAnimationResultRange(_toByAnimation, DefaultOrigin, To);
            this.TestAnimationResultRange(_fromToByAnimation, From, To);
        }
        
        [TestMethod]
        public void IsAdditiveInfluencesDualValueAnimations()
        {
            // FromTo/FromBy are the only animations which should produce different values for IsAdditive.
            // -> Do a separate test case with correct expected values here.
            _fromToAnimation.IsAdditive = true;
            _fromByAnimation.IsAdditive = true;
            _fromToByAnimation.IsAdditive = true;

            this.TestAnimationResultRange(_fromToAnimation, DefaultOrigin + From, DefaultOrigin + To);
            this.TestAnimationResultRange(_fromByAnimation, DefaultOrigin + From, DefaultOrigin + From + By);
            this.TestAnimationResultRange(_fromToByAnimation, DefaultOrigin + From, DefaultOrigin + To);

            _fromByAnimation.IsAdditive = false;
            _fromByAnimation.IsAdditive = false;
            _fromToByAnimation.IsAdditive = false;
        }

        [TestMethod]
        public void AnimationValuesAccumulate()
        {
            // The IsCumulative method can only be tested, if the AnimationClock.CurrentIteration
            // property can be set.
            // I cannot figure any way/hack out right now, so I will not add a test for this.
            // If anyone knows of a way to set an AnimationClock's CurrentIteration property,
            // please tell me/create a valid test case here.
            Trace.WriteLine(
                nameof(AnimationValuesAccumulate) + " is not implemented due to .NET Framework restrictions.");
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
            // due to floating point number nature. (e.g. 160.9999999997 != 161)
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
