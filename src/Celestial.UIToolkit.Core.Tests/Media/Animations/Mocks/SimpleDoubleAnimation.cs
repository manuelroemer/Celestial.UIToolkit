using Celestial.UIToolkit.Media.Animations;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    /// <summary>
    /// A very simple double animation which directly inherits from <see cref="AnimationBase{T}"/>
    /// and animates the values passed into <see cref="GetCurrentValueCore(double, double, AnimationClock)"/>.
    /// </summary>
    public class SimpleDoubleAnimation : AnimationBase<double>
    {

        protected override Freezable CreateInstanceCore() => new SimpleDoubleAnimation();

        protected override double GetCurrentValueCore(
            double from,
            double to, 
            AnimationClock animationClock)
        {
            double progress = animationClock.CurrentProgress.Value;

            // Perform a very simple animation.
            // Return a scaled value between from and to, depending on the current progress.
            if (progress == 0)
                return from;
            else if (progress == 1)
                return to;
            else
                return from + (to - from) * progress;
        }

    }

}
