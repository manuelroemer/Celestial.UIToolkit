using Celestial.UIToolkit.Media.Animations;
using System.Windows;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    /// <summary>
    /// A From/To/By animation which animates doubles.
    /// </summary>
    public class FromToByDoubleAnimation : FromToByAnimationBase<double>
    {

        public FromToByDoubleAnimation(
            double? from = null,
            double? to = null,
            double? by = null)
        {
            if (from != null)
                From = from.Value;
            if (to != null)
                To = to.Value;
            if (by != null)
                By = by.Value;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new FromToByDoubleAnimation();
        }

        protected override double AddValues(double a, double b)
        {
            return a + b;
        }

        protected override double SubtractValues(double a, double b)
        {
            return a - b;
        }

        protected override double ScaleValue(double value, double factor)
        {
            return value * factor;
        }

        protected override double InterpolateValue(double from, double to, double progress)
        {
            return from + (to - from) * progress;
        }

    }

}
