using Celestial.UIToolkit.Media.Animations;
using System.Windows;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    public class DoubleAnimationUsingKeyFrames
        : AnimationUsingKeyFramesBase<double, KeyFrameBase<double>, DoubleKeyFrameCollection>
    {

        protected override Freezable CreateInstanceCore()
        {
            return new DoubleAnimationUsingKeyFrames();
        }

        protected override double GetZeroValue()
        {
            return 0d;
        }

        protected override double AddValues(double a, double b)
        {
            return a + b;
        }

        protected override double ScaleValue(double value, double factor)
        {
            return value * factor;
        }

        protected override double GetDistanceBetweenCore(double from, double to)
        {
            return to - from;
        }

    }

    public class DoubleKeyFrameCollection : FreezableCollection<KeyFrameBase<double>> { }

    public class DiscreteDoubleKeyFrame : DiscreteKeyFrameBase<double>
    {
        protected override Freezable CreateInstanceCore() => new DiscreteDoubleKeyFrame();
    }

    public class LinearDoubleKeyFrame : KeyFrameBase<double>
    {
        protected override Freezable CreateInstanceCore() => new LinearDoubleKeyFrame();

        protected override double InterpolateValueCore(double baseValue, double keyFrameProgress)
        {
            if (keyFrameProgress <= 0) return baseValue;
            if (keyFrameProgress >= 1) return Value;
            return baseValue + (Value - baseValue) * keyFrameProgress;
        }
    }

    public class EasingDoubleKeyFrame : EasingKeyFrameBase<double>
    {
        protected override Freezable CreateInstanceCore() => new EasingDoubleKeyFrame();

        protected override double InterpolateValueAfterEase(double baseValue, double easedProgress)
        {
            if (easedProgress <= 0) return baseValue;
            if (easedProgress >= 1) return Value;
            return baseValue + (Value - baseValue) * easedProgress;
        }
    }

    public class SplineDoubleKeyFrame : SplineKeyFrameBase<double>
    {
        protected override Freezable CreateInstanceCore() => new SplineDoubleKeyFrame();

        protected override double InterpolateValueWithSplineProgress(double baseValue, double splineProgress)
        {
            if (splineProgress <= 0) return baseValue;
            if (splineProgress >= 1) return Value;
            return baseValue + (Value - baseValue) * splineProgress;
        }
    }

}
