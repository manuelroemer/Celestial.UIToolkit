using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public class EasingBrushKeyFrame : EasingKeyFrameBase<Brush>
    {

        protected override Freezable CreateInstanceCore() => new EasingBrushKeyFrame();

        protected override Brush InterpolateValueAfterEase(Brush baseValue, double easedProgress)
        {
            if (easedProgress <= 0) return baseValue;
            if (easedProgress >= 1) return this.Value;
            return AnimatedBrushHelpers.SupportedTypeHelpers[baseValue.GetType()]
                                       .InterpolateValue(baseValue, this.Value, easedProgress);
        }

    }

}
