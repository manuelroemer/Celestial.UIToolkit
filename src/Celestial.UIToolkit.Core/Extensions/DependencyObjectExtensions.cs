using System;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="DependencyObject"/> type.
    /// </summary>
    public static class DependencyObjectExtensions
    {

        /// <summary>
        /// Returns a value indicating whether the provided <paramref name="ancestor"/>
        /// is one of the current dependency object's ancestor in the visual tree.
        /// </summary>
        /// <param name="depObj">The dependency object.</param>
        /// <param name="ancestor">A potential ancestor.</param>
        /// <returns>
        /// true if <paramref name="ancestor"/> is part of the object's visual tree;
        /// false if not.
        /// </returns>
        public static bool HasVisualAncestor(
            this DependencyObject depObj, DependencyObject ancestor)
        {
            if (depObj == null) throw new ArgumentNullException(nameof(depObj));
            if (ancestor == null) return false;
            var currentAncestor = VisualTreeHelper.GetParent(depObj);

            if (currentAncestor == null)
                return false;
            else if (currentAncestor == ancestor)
                return true;
            else
                return currentAncestor.HasVisualAncestor(ancestor);
        }

    }

}
