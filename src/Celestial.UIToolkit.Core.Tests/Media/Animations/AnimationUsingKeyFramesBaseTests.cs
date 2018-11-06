using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoubleAnimationUsingKeyFrames = Celestial.UIToolkit.Tests.Media.Animations.Mocks.DoubleAnimationUsingKeyFrames;
using DiscreteDoubleKeyFrame = Celestial.UIToolkit.Tests.Media.Animations.Mocks.DiscreteDoubleKeyFrame;
using LinearDoubleKeyFrame = Celestial.UIToolkit.Tests.Media.Animations.Mocks.LinearDoubleKeyFrame;
using EasingDoubleKeyFrame = Celestial.UIToolkit.Tests.Media.Animations.Mocks.EasingDoubleKeyFrame;
using SplineDoubleKeyFrame = Celestial.UIToolkit.Tests.Media.Animations.Mocks.SplineDoubleKeyFrame;
using DoubleKeyFrameCollection = Celestial.UIToolkit.Tests.Media.Animations.Mocks.DoubleKeyFrameCollection;
using Celestial.UIToolkit.Media.Animations;
using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using Xunit;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class AnimationUsingKeyFramesBaseTests
    {

        [Fact]
        public void ReturnsLastKeyFrameValueIfPastFinalKeyTime()
        {
            const double finalValue = 10;

            var animation = new DoubleAnimationUsingKeyFrames(new Duration(TimeSpan.FromSeconds(10)));
            var clock = ControllableAnimationClock.FromTime(TimeSpan.FromSeconds(10));
            animation.KeyFrames = new DoubleKeyFrameCollection()
            {
                new DiscreteDoubleKeyFrame(finalValue, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)))
            };

            double result = animation.GetCurrentValue(0d, 0d, clock);

            Assert.Equal(finalValue, result);
        }
        
    }

}
