using Celestial.UIToolkit.Media.Animations;
using System.Windows;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    /// <summary>
    /// A custom double animation with From/To/By properties and an easing function,
    /// which is used by the basic animation tests.
    /// It's using doubles, since it is a very easy data-type to test.
    /// </summary>
    public class DoubleFromToByAnimation : EasingFromToByAnimationBase<double>
    {

        protected override Freezable CreateInstanceCore() => new DoubleFromToByAnimation();

        protected override double AddValues(double a, double b) => a + b;

        protected override double InterpolateValueCore(double from, double to, double progress) =>
            from + (to - from) * progress;

        protected override double ScaleValue(double value, double factor) => value * factor;

        protected override double SubtractValues(double a, double b) => a - b;
    }

}
