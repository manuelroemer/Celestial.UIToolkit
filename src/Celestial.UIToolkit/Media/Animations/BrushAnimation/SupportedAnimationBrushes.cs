using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Defines which brushes are supported by the <see cref="BrushAnimation"/>
    /// and <see cref="BrushAnimationUsingKeyFrames"/> and provides
    /// helper methods for these two classes regarding these supported brushes.
    /// </summary>
    internal static class SupportedAnimationBrushes
    {

        /// <summary>
        /// A dictionary which maps <see cref="Brush"/> types to their corresponding
        /// animation helpers.
        /// </summary>
        private static Dictionary<Type, IAnimationHelper<Brush>> _supportedBrushesAnimationHelperMap
            = new Dictionary<Type, IAnimationHelper<Brush>>()
        {
            [typeof(SolidColorBrush)]     = SolidColorBrushAnimationHelper.Instance,
            [typeof(LinearGradientBrush)] = LinearGradientBrushAnimationHelper.Instance,
            [typeof(RadialGradientBrush)] = RadialGradientBrushAnimationHelper.Instance
        };

        /// <summary>
        /// Gets a set of the supported brush types.
        /// </summary>
        public static IEnumerable<Type> SupportedTypes => _supportedBrushesAnimationHelperMap.Keys;

        /// <summary>
        /// Returns a value indicating whether the specified <paramref name="brush"/>
        /// is supported by the animations.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <returns>
        /// <c>true</c> if the brush is supported; false if not.
        /// </returns>
        public static bool IsSupported(Brush brush)
        {
            if (brush == null) throw new ArgumentNullException(nameof(brush));
            return IsSupported(brush.GetType());
        }

        /// <summary>
        /// Returns a value indicating whether a brush of the specified type
        /// is supported by the animations.
        /// </summary>
        /// <param name="brushType">The brush type.</param>
        /// <returns>
        /// <c>true</c> if the brush type is supported; false if not.
        /// </returns>
        public static bool IsSupported(Type brushType)
        {
            return _supportedBrushesAnimationHelperMap.ContainsKey(brushType);
        }

        /// <summary>
        /// Returns an animation helper for the specified <paramref name="brush"/>
        /// and throws an exception, if the brush's type is not supported.
        /// </summary>
        /// <param name="brush">
        /// The brush for which an animation helper is required.
        /// </param>
        /// <returns>
        /// An <see cref="IAnimationHelper{Brush}"/> which can animate
        /// the specified <paramref name="brush"/>.
        /// </returns>
        public static IAnimationHelper<Brush> GetAnimationHelper(Brush brush)
        {
            if (brush == null) throw new ArgumentNullException(nameof(brush));
            return GetAnimationHelper(brush.GetType());
        }

        /// <summary>
        /// Returns an animation helper for the specified <paramref name="brushType"/>
        /// and throws an exception, if the type is not supported.
        /// </summary>
        /// <param name="brushType">
        /// The brush type for which an animation helper is required.
        /// </param>
        /// <returns>
        /// An <see cref="IAnimationHelper{Brush}"/> which can animate
        /// brushes of the specified type.
        /// </returns>
        public static IAnimationHelper<Brush> GetAnimationHelper(Type brushType)
        {
            if (_supportedBrushesAnimationHelperMap.TryGetValue(brushType, out var helper))
            {
                return helper;
            }
            else
            {
                throw new InvalidOperationException(
                    $"The type {brushType.FullName} is not supported by the brush animation.");
            }
        }

    }

}
