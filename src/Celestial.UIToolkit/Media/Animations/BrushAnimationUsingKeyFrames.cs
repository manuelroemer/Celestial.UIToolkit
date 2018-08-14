using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public class BrushAnimationUsingKeyFrames
        : AnimationUsingKeyFramesBase<Brush, KeyFrameBase<Brush>, BrushKeyFrameCollection>
    {

        protected override Freezable CreateInstanceCore() => new BrushAnimationUsingKeyFrames();

        protected override sealed Brush GetZeroValue()
        {
            var firstFrame = this.KeyFrames.First();
            var brushType = firstFrame.Value.GetType();

            return AnimatedBrushHelpers.SupportedTypeHelpers[brushType]
                                       .GetZeroValue();
        }

        protected override sealed Brush AddValues(Brush a, Brush b)
        {
            return AnimatedBrushHelpers.SupportedTypeHelpers[a.GetType()]
                                       .AddValues(a, b);
        }

        protected override sealed Brush ScaleValue(Brush value, double factor)
        {
            return AnimatedBrushHelpers.SupportedTypeHelpers[value.GetType()]
                                       .ScaleValue(value, factor);
        }

        /// <summary>
        /// Returns 1.
        /// </summary>
        /// <param name="from">Not used.</param>
        /// <param name="to">Not used.</param>
        /// <returns>1.</returns>
        protected override sealed double GetSegmentLengthCore(Brush from, Brush to)
        {
            // Brushes are nominal which means that we can't get any kind of distance out of them.
            // By returning a uniform value, we basically treat Paced KeyTimes as Uniform.
            return 1d;
        }

    }

}
