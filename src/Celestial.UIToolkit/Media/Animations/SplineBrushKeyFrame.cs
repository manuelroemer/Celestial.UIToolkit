using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public class SplineBrushKeyFrame : SplineKeyFrameBase<Brush>
    {

        protected override Freezable CreateInstanceCore() => new SplineBrushKeyFrame();

        protected override Brush InterpolateValueWithSplineProgress(Brush baseValue, double splineProgress)
        {
            if (splineProgress <= 0) return baseValue;
            if (splineProgress >= 1) return this.Value;
            return AnimatedBrushHelpers.SupportedTypeHelpers[baseValue.GetType()]
                                       .InterpolateValue(baseValue, this.Value, splineProgress);
        }

    }

}
