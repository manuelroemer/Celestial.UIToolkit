using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    public class LinearGradientBrushAnimation : GradientBrushAnimation
    {

        protected override Freezable CreateInstanceCore() => new LinearGradientBrushAnimation();

        protected override void ValidateTimelineBrushesCore(Brush origin, Brush destination)
        {
            base.ValidateTimelineBrushesCore(origin, destination);
            this.ValidateThatBrushesAreLinear(origin, destination);
        }

        private void ValidateThatBrushesAreLinear(Brush origin, Brush destination)
        {
            if (origin.GetType() != typeof(LinearGradientBrush) ||
                destination.GetType() != typeof(LinearGradientBrush))
            {
                throw new InvalidOperationException(
                    $"The animation can only animate brushes of type {nameof(LinearGradientBrush)}.");
            }
        }

        protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
        {
            throw new NotImplementedException();
        }

    }

}
