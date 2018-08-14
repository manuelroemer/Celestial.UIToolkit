using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public class LinearBrushKeyFrame : KeyFrameBase<Brush>
    {

        protected override Freezable CreateInstanceCore() => new LinearBrushKeyFrame();

        protected override Brush InterpolateValueCore(Brush baseValue, double keyFrameProgress)
        {
            if (keyFrameProgress <= 0) return baseValue;
            if (keyFrameProgress >= 1) return this.Value;
            return AnimatedBrushHelpers.SupportedTypeHelpers[baseValue.GetType()]
                                       .InterpolateValue(baseValue, this.Value, keyFrameProgress);
        }

    }

}
