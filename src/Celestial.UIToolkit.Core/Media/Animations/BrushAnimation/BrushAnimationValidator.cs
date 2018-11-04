using System;
using System.Linq;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Provides static validation methods for <see cref="Brush"/> objects
    /// which are used by the <see cref="BrushAnimation"/> and <see cref="BrushAnimationUsingKeyFrames"/>
    /// classes.
    /// </summary>
    internal static class BrushAnimationValidator
    {

        private static readonly Type[] _supportedBrushes = new Type[]
        {
            typeof(SolidColorBrush),
            typeof(LinearGradientBrush),
            typeof(RadialGradientBrush)
        };

        /// <summary>
        /// Ensures that the two brushes are not null, have the same supported type
        /// and that their un-animatable properties have the same values.
        /// If that is not the case, an exception is thrown.
        /// </summary>
        /// <param name="origin">The origin brush.</param>
        /// <param name="destination">The destination brush.</param>
        public static void ValidateBrushes(Brush origin, Brush destination)
        {
            ValidateThatBrushesAreNotNull(origin, destination);
            ValidateThatBrushesHaveSameType(origin, destination);
            ValidateThatBrushesHaveSupportedType(origin, destination);
            ValidateThatBrushesHaveSameTransform(origin, destination);
            ValidateGradientBrushes(origin, destination);
        }

        private static void ValidateThatBrushesAreNotNull(Brush origin, Brush destination)
        {
            // One of the two brushes is allowed to be null.
            if (origin == null && destination == null)
            {
                throw new InvalidOperationException(
                    $"The animation cannot animate a brush which is set to null (Nothing in VB).");
            }
        }

        private static void ValidateThatBrushesHaveSameType(Brush origin, Brush destination)
        {
            if (origin == null || destination == null)
                return;

            // The two brushes must have the same type,
            // except if one of the two is a SolidColorBrush.
            // Then we support inter-brush animations.
            if (origin.GetType() != destination.GetType() &&
                !(origin is SolidColorBrush) &&
                !(destination is SolidColorBrush))
            {
                throw new InvalidOperationException(
                    $"The animation can only handle brushes of the same type. " +
                    $"The current brush types are: " +
                    $"{origin.GetType().FullName}, {destination.GetType().FullName}");
            }
        }

        private static void ValidateThatBrushesHaveSupportedType(Brush origin, Brush destination)
        {
            if (!HasSupportedType(origin) ||
                !HasSupportedType(destination))
            {
                throw new InvalidOperationException(
                    $"The animation can only animate brushes of the following types: " +
                    $"{string.Join(", ", _supportedBrushes.Select(type => type.Name))}. " +
                    $"Ensure that all target properties of this animation have been set to brushes of " +
                    $"the specified types.");
            }
        }

        private static bool HasSupportedType(Brush brush)
        {
            return brush == null ||
                   _supportedBrushes.Any(supportedType => brush.GetType() == supportedType);
        }

        private static void ValidateThatBrushesHaveSameTransform(Brush origin, Brush destination)
        {
            if (origin == null || destination == null)
                return;

            if (origin.Transform != destination.Transform ||
                origin.RelativeTransform != destination.RelativeTransform)
            {
                throw new InvalidOperationException(
                    $"The {nameof(Brush.Transform)} and {nameof(Brush.RelativeTransform)} properties of the " +
                    $"brushes must have the same values.");
            }
        }

        private static void ValidateGradientBrushes(Brush origin, Brush destination)
        {
            GradientBrush gradientOrigin = origin as GradientBrush;
            GradientBrush gradientDestination = destination as GradientBrush;
            if (gradientOrigin == null || gradientDestination == null) return;

            ValidateColorInterpolationModeEquality(gradientOrigin, gradientDestination);
            ValidateBrushMappingModeEquality(gradientOrigin, gradientDestination);
            ValidateSpreadMethodEquality(gradientOrigin, gradientDestination);
            ValidateGradientStops(gradientOrigin, gradientDestination);
        }

        private static void ValidateColorInterpolationModeEquality(GradientBrush origin, GradientBrush destination)
        {
            if (origin.ColorInterpolationMode != destination.ColorInterpolationMode)
                ThrowForUnequalEnumProperty(nameof(GradientBrush.ColorInterpolationMode));
        }

        private static void ValidateBrushMappingModeEquality(GradientBrush origin, GradientBrush destination)
        {
            if (origin.MappingMode != destination.MappingMode)
                ThrowForUnequalEnumProperty(nameof(GradientBrush.MappingMode));
        }

        private static void ValidateSpreadMethodEquality(GradientBrush origin, GradientBrush destination)
        {
            if (origin.SpreadMethod != destination.SpreadMethod)
                ThrowForUnequalEnumProperty(nameof(GradientBrush.SpreadMethod));
        }

        private static void ValidateGradientStops(GradientBrush origin, GradientBrush destination)
        {
            if (origin.GradientStops == null || destination.GradientStops == null)
                throw new InvalidOperationException(
                    $"The {nameof(GradientBrush.GradientStops)} property must not be null.");

            if (origin.GradientStops.Count != destination.GradientStops.Count)
                throw new InvalidOperationException(
                    $"The animation requires both gradient brushes to have the same number of " +
                    $"gradient stops.");
        }

        private static void ThrowForUnequalEnumProperty(string propertyName)
        {
            throw new InvalidOperationException(
                    $"The animation requires the two {nameof(GradientBrush)} objects to have " +
                    $"the same {propertyName} values.");
        }

    }

}
