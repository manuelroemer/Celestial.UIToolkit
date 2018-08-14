using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public class BrushAnimation : EasingFromToByAnimationBase<Brush>
    {

        public BrushAnimation() { }

        protected override Freezable CreateInstanceCore() => new BrushAnimation();

        protected override void ValidateAnimationValues(Brush from, Brush to)
        {
            this.ValidateThatBrushesAreNotNull(from, to);
            this.ValidateThatBrushesHaveSameType(from, to);
            this.ValidateThatBrushesHaveSameTransform(from, to);
        }

        private void ValidateThatBrushesAreNotNull(Brush from, Brush to)
        {
            if (from == null || to == null)
            {
                throw new InvalidOperationException(
                    $"The animation cannot animate a brush which is set to null.");
            }
        }

        private void ValidateThatBrushesHaveSameType(Brush from, Brush to)
        {
            if (from.GetType() != to.GetType())
            {
                throw new InvalidOperationException(
                    $"The animation can only handle brushes of the same type. " +
                    $"Ensure that the {nameof(From)}, {nameof(To)} and {nameof(By)} properties " +
                    $"are set to brushes of the same type.");
            }
        }

        private void ValidateThatBrushesHaveSameTransform(Brush from, Brush to)
        {
            if (from.Transform != to.Transform ||
                from.RelativeTransform != to.RelativeTransform)
            {
                throw new InvalidOperationException(
                    $"The {nameof(Brush.Transform)} and {nameof(Brush.RelativeTransform)} properties of the " +
                    $"brushes must have the same values.");
            }
        }
        
        protected override Brush AddValues(Brush a, Brush b)
        {
            return AnimatedSolidColorBrushHelper.Instance.AddValues((SolidColorBrush)a, (SolidColorBrush)b);
        }

        protected override Brush SubtractValues(Brush a, Brush b)
        {
            return AnimatedSolidColorBrushHelper.Instance.SubtractValues((SolidColorBrush)a, (SolidColorBrush)b);
        }

        protected override Brush ScaleValue(Brush value, double factor)
        {
            return AnimatedSolidColorBrushHelper.Instance.ScaleValue((SolidColorBrush)value, factor);
        }

        protected override Brush InterpolateValueCore(Brush from, Brush to, double progress)
        {
            return AnimatedSolidColorBrushHelper.Instance.InterpolateValue((SolidColorBrush)from, (SolidColorBrush)to, progress);
        }
        
    }

}
