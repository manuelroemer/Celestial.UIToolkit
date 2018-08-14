using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    public class BrushAnimation : EasingFromToByAnimationBase<Brush>
    {

        // Defines both the supported brush types and the corresponding TypeHelper
        // instances which are used for modifying brushes.
        private static Dictionary<Type, IAnimatedTypeHelper<Brush>> _supportedTypeHelperMap
            = new Dictionary<Type, IAnimatedTypeHelper<Brush>>()
        {
            [typeof(SolidColorBrush)]     = AnimatedSolidColorBrushHelper.Instance,
            [typeof(LinearGradientBrush)] = AnimatedLinearGradientBrushHelper.Instance,
            [typeof(RadialGradientBrush)] = AnimatedRadialGradientBrushHelper.Instance
        };

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
            return this.GetAnimatedTypeHelper(a.GetType())
                       .AddValues(a, b);
        }

        protected override Brush SubtractValues(Brush a, Brush b)
        {
            return this.GetAnimatedTypeHelper(a.GetType())
                       .SubtractValues(a, b);
        }

        protected override Brush ScaleValue(Brush value, double factor)
        {
            return this.GetAnimatedTypeHelper(value.GetType())
                       .ScaleValue(value, factor);
        }

        protected override Brush InterpolateValueCore(Brush from, Brush to, double progress)
        {
            return this.GetAnimatedTypeHelper(from.GetType())
                       .InterpolateValue(from, to, progress);
        }

        private IAnimatedTypeHelper<Brush> GetAnimatedTypeHelper(Type brushType)
        {
            if (_supportedTypeHelperMap.TryGetValue(brushType, out var helper))
            {
                return helper;
            }
            else
            {
                throw new InvalidOperationException(
                    $"Could not map the type {brushType.FullName} to an animation helper.");
            }
        }
        
    }

}
