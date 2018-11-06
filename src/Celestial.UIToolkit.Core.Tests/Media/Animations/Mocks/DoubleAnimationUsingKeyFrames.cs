using Celestial.UIToolkit.Media.Animations;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    public class DoubleAnimationUsingKeyFrames
        : AnimationUsingKeyFramesBase<double, KeyFrameBase<double>, DoubleKeyFrameCollection>
    {

        public DoubleAnimationUsingKeyFrames() { }

        public DoubleAnimationUsingKeyFrames(Duration duration)
        {
            Duration = duration;
        }

        public DoubleAnimationUsingKeyFrames(IEnumerable<KeyFrameBase<double>> keyFrames)
            : this(keyFrames == null ? null : new DoubleKeyFrameCollection(keyFrames)) { }

        public DoubleAnimationUsingKeyFrames(DoubleKeyFrameCollection keyFrames)
        {
            KeyFrames = keyFrames;
        }

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

    public class DoubleKeyFrameCollection : FreezableCollection<KeyFrameBase<double>>
    {
        public DoubleKeyFrameCollection()
            : base() { }

        public DoubleKeyFrameCollection(IEnumerable<KeyFrameBase<double>> collection) 
            : base(collection) { }
    }

    public class DiscreteDoubleKeyFrame : DiscreteKeyFrameBase<double>
    {

        public DiscreteDoubleKeyFrame(double? value = null, KeyTime? keyTime = null)
        {
            if (value != null)
                Value = value.Value;
            if (keyTime != null)
                KeyTime = keyTime.Value;
        }

        protected override Freezable CreateInstanceCore() => new DiscreteDoubleKeyFrame();

    }

    public class LinearDoubleKeyFrame : KeyFrameBase<double>
    {

        public LinearDoubleKeyFrame(double? value = null, KeyTime? keyTime = null)
        {
            if (value != null)
                Value = value.Value;
            if (keyTime != null)
                KeyTime = keyTime.Value;
        }

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

        public EasingDoubleKeyFrame(double? value = null, KeyTime? keyTime = null)
        {
            if (value != null)
                Value = value.Value;
            if (keyTime != null)
                KeyTime = keyTime.Value;
        }

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

        public SplineDoubleKeyFrame(double? value = null, KeyTime? keyTime = null)
        {
            if (value != null)
                Value = value.Value;
            if (keyTime != null)
                KeyTime = keyTime.Value;
        }

        protected override Freezable CreateInstanceCore() => new SplineDoubleKeyFrame();

        protected override double InterpolateValueWithSplineProgress(double baseValue, double splineProgress)
        {
            if (splineProgress <= 0) return baseValue;
            if (splineProgress >= 1) return Value;
            return baseValue + (Value - baseValue) * splineProgress;
        }

    }

}
