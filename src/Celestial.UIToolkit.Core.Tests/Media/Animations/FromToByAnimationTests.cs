﻿using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
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
        
        #region Automatic Animation

        // Automatic animations are animations without a From/To/By value.
        // They simply "get fed" with the defaultOrigin and defaultDestination values.

        [Fact]
        public void EmptyAnimationReturnsDefaultOriginWhenStarted()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.NewFinished();
            double defaultOrigin = 50.0;
            double defaultDestination = 100.0;

            double result = animation.GetCurrentValue(defaultOrigin, defaultDestination, clock);
            Assert.Equal(defaultDestination, result);
        }

        [Fact]
        public void EmptyAnimationReturnsDefaultDestinationWhenFinished()
        {
            var animation = new FromToByDoubleAnimation();
            var clock = ControllableAnimationClock.NewStarted();
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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                To = to
            };
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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                To = to
            };
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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                By = by
            };
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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                By = by
            };
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
            var animation = new FromToByDoubleAnimation()
            {
                From = from,
                By = by
            };
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

    }

}
