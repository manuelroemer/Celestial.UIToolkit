using Celestial.UIToolkit.Common;

namespace Celestial.UIToolkit.Media.Animations
{

    internal sealed class DoubleAnimationHelper : Singleton<DoubleAnimationHelper>, IAnimationHelper<double>
    {

        private DoubleAnimationHelper() { }
        
        public double GetZeroValue() => 0d;

        public double AddValues(double a, double b) => a + b;

        public double SubtractValues(double a, double b) => a - b;

        public double ScaleValue(double value, double factor) => value * factor;

        public double InterpolateValue(double from, double to, double progress)
        {
            return from + (to - from) * progress;
        }

    }

}
