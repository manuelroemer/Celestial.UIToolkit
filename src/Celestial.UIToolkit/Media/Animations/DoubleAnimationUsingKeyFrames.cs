using System;
using System.Windows;

namespace Celestial.UIToolkit.Media.Animations
{

    public class DoubleKeyFrameCollection : FreezableCollection<KeyFrameBase<double>>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new DoubleKeyFrameCollection();
        }
    }

    public class DoubleAnimationUsingKeyFrames : AnimationUsingKeyFramesBase<double, DoubleKeyFrameCollection>
    {
        protected override double AddValues(double a, double b)
        {
            return a + b;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new DoubleAnimationUsingKeyFrames();
        }
        
        protected override double GetSegmentLengthCore(double from, double to)
        {
            return Math.Abs(to - from);
        }

        protected override double GetZeroValue()
        {
            return 0d;
        }

        protected override double ScaleValues(double value, double factor)
        {
            return value * factor;
        }
    }

    public class LinearDoubleKeyFrame : KeyFrameBase<double>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new LinearDoubleKeyFrame();
        }

        protected override double InterpolateValueCore(double baseValue, double keyFrameProgress)
        {
            if (keyFrameProgress == 0d) return baseValue;
            if (keyFrameProgress == 1d) return this.Value;
            return baseValue + (Value - baseValue) * keyFrameProgress;
        }
    }

}
