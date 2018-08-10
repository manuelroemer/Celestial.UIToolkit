using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    public abstract class GradientBrushAnimation : BrushAnimationBase
    {

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            Brush origin = this.From ?? defaultOriginValue as Brush;
            Brush destination = this.To ?? defaultDestinationValue as Brush;

            // If one of the brushes is null, let the base class throw an exception.
            if (origin == null || destination == null)
                return base.GetCurrentValue(defaultOriginValue, defaultDestinationValue, animationClock);

            this.ValidateThatBrushesAreGradient(origin, destination);
            this.ValidateGradientBrushes((GradientBrush)origin, (GradientBrush)destination);

            return base.GetCurrentValue(defaultOriginValue, defaultDestinationValue, animationClock);
        }

        private void ValidateThatBrushesAreGradient(Brush origin, Brush destination)
        {
            if (!(origin is GradientBrush) ||
                !(destination is GradientBrush))
            {
                throw new InvalidOperationException(
                    $"The animation requires two {nameof(GradientBrush)} objects.");
            }
        }

        private void ValidateGradientBrushes(GradientBrush origin, GradientBrush destination)
        {
            this.ValidateColorInterpolationModeEquality(origin, destination);
            this.ValidateBrushMappingModeEquality(origin, destination);
            this.ValidateSpreadMethodEquality(origin, destination);
            this.ValidateGradientStops(origin, destination);
        }
        
        private void ValidateColorInterpolationModeEquality(GradientBrush origin, GradientBrush destination)
        {
            if (origin.ColorInterpolationMode != destination.ColorInterpolationMode)
                this.ThrowForUnequalEnumProperty(nameof(GradientBrush.ColorInterpolationMode));
        }

        private void ValidateBrushMappingModeEquality(GradientBrush origin, GradientBrush destination)
        {
            if (origin.MappingMode != destination.MappingMode)
                this.ThrowForUnequalEnumProperty(nameof(GradientBrush.MappingMode));
        }

        private void ValidateSpreadMethodEquality(GradientBrush origin, GradientBrush destination)
        {
            if (origin.SpreadMethod != destination.SpreadMethod)
                this.ThrowForUnequalEnumProperty(nameof(GradientBrush.SpreadMethod));
        }

        private void ValidateGradientStops(GradientBrush origin, GradientBrush destination)
        {
            if (origin.GradientStops == null || destination.GradientStops == null)
                throw new InvalidOperationException(
                    $"The {nameof(GradientBrush.GradientStops)} property must not be null.");

            if (origin.GradientStops.Count != destination.GradientStops.Count)
                throw new InvalidOperationException(
                    $"The animation requires both gradient brushes to have the same number of " +
                    $"gradient stops.");
        }

        private void ThrowForUnequalEnumProperty(string propertyName)
        {
            throw new InvalidOperationException(
                    $"The animation requires the two {nameof(GradientBrush)} objects to have " +
                    $"the same {propertyName} values.");
        }

        //
        // TODO:
        // Animate GradientStops base method
        // 
        // (also maybe create a virtual ValidateTimelineValues() method in the base.)
        //

    }
    
}
