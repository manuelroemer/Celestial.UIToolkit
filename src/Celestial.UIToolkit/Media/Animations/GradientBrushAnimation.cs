using System;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// A base class for an animation which animates a <see cref="GradientBrush"/>.
    /// </summary>
    public abstract class GradientBrushAnimation : BrushAnimation
    {

        private GradientStopCollection _currentGradientStops;
        
        /// <summary>
        /// Performs some common validation on the specified brushes,
        /// shared by all gradient brush animations.
        /// </summary>
        /// <param name="origin">The animation's origin brush.</param>
        /// <param name="destination">The animation's destination brush.</param>
        protected override void ValidateTimelineBrushesCore(Brush origin, Brush destination)
        {
            var gradientOrigin = origin as GradientBrush;
            var gradientDestination = destination as GradientBrush;

            this.ValidateThatBrushesAreGradient(origin, destination);
            this.ValidateColorInterpolationModeEquality(gradientOrigin, gradientDestination);
            this.ValidateBrushMappingModeEquality(gradientOrigin, gradientDestination);
            this.ValidateSpreadMethodEquality(gradientOrigin, gradientDestination);
            this.ValidateGradientStops(gradientOrigin, gradientDestination);
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
        
    }
    
}
