namespace Celestial.UIToolkit.Media.Animations
{

    internal sealed class AnimatedDoubleHelper : IAnimatedTypeHelper<double>
    {

        public static AnimatedDoubleHelper Default { get; } = new AnimatedDoubleHelper();

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
