using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public abstract class BrushAnimation<T> : EasingFromToByAnimationBase<T>
        where T : Brush
    {
        
        protected override void ValidateAnimationValues(T from, T to)
        {
            this.ValidateThatBrushesAreNotNull(from, to);
            this.ValidateThatBrushesHaveSameType(from, to);
            this.ValidateThatBrushesHaveSameTransform(from, to);
        }

        private void ValidateThatBrushesAreNotNull(T from, T to)
        {
            if (from == null || to == null)
            {
                throw new InvalidOperationException(
                    $"The animation cannot animate a brush which is set to null.");
            }
        }

        private void ValidateThatBrushesHaveSameType(T from, T to)
        {
            if (from.GetType() != to.GetType())
            {
                throw new InvalidOperationException(
                    $"The animation can only handle brushes of the same type. " +
                    $"Ensure that the {nameof(From)}, {nameof(To)} and {nameof(By)} properties " +
                    $"are set to brushes of the same type.");
            }
        }

        private void ValidateThatBrushesHaveSameTransform(T from, T to)
        {
            if (from.Transform != to.Transform ||
                from.RelativeTransform != to.RelativeTransform)
            {
                throw new InvalidOperationException(
                    $"The {nameof(Brush.Transform)} and {nameof(Brush.RelativeTransform)} properties of the " +
                    $"brushes must have the same values.");
            }
        }

    }

}
