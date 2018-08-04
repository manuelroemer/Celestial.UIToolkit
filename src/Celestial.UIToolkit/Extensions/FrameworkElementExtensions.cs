using System.Windows;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="FrameworkElement"/> class.
    /// </summary>
    public static class FrameworkElementExtensions
    {

        /// <summary>
        /// Returns a <see cref="Point"/> which represents the element's center.
        /// </summary>
        /// <param name="frameworkElement">The <see cref="FrameworkElement"/>.</param>
        /// <returns>
        /// A <see cref="Point"/> instance, pointing to the element's center.
        /// </returns>
        public static Point GetCenterPoint(this FrameworkElement frameworkElement)
        {
            return new Point(
                frameworkElement.ActualWidth / 2,
                frameworkElement.ActualHeight / 2);
        }

        /// <summary>
        /// Returns a value indicating whether the specified point is inside the
        /// element's bounds.
        /// </summary>
        /// <param name="frameworkElement">The <see cref="FrameworkElement"/>.</param>
        /// <param name="point">
        /// A point which may or may not be inside the bounds of the element.
        /// The point is expected to be relative to the element, meaning that (0; 0) 
        /// points to the element's top-left corner.
        /// </param>
        /// <returns>
        /// <c>true</c> if the <paramref name="point"/> is inside the element's bounds;
        /// <c>false</c> if not.
        /// </returns>
        public static bool IsPointInControlBounds(this FrameworkElement frameworkElement, Point point)
        {
            return point.X >= 0d &&
                   point.Y >= 0d &&
                   point.X <= frameworkElement.ActualWidth &&
                   point.Y <= frameworkElement.ActualHeight;
        }

    }

}
