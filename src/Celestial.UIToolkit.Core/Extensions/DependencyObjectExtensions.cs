using System;
using System.Windows;
using System.Windows.Controls;
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

            return depObj.GetVisualAncestor(obj => obj == ancestor) != null;
        }

        /// <summary>
        /// Tries to find an ancestor of the specified <paramref name="depObj"/> which fulfills
        /// the specified <paramref name="predicate"/> in the visual tree. 
        /// </summary>
        /// <param name="depObj">The dependency object.</param>
        /// <param name="predicate">A predicate to be fullfilled.</param>
        /// <returns>
        /// The visual ancestor which fullfills the predicate or null, if none was found.
        /// </returns>
        public static DependencyObject GetVisualAncestor(
            this DependencyObject depObj, Predicate<DependencyObject> predicate)
        {
            if (depObj == null) throw new ArgumentNullException(nameof(depObj));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            var currentAncestor = VisualTreeHelper.GetParent(depObj);

            if (currentAncestor == null)
                return null;
            else if (predicate(currentAncestor))
                return currentAncestor;
            else
                return currentAncestor.GetVisualAncestor(predicate);
        }

        /// <summary>
        /// Tries to remove the specified <paramref name="child"/> from the elements logical
        /// tree.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child to be removed.</param>
        public static void RemoveLogicalChild(this DependencyObject parent, UIElement child)
        {
            // This method tries to remove the child from a logical tree.
            // The real .NET method is internal and thus can't be used.
            // As a result, we have to rely on a Fallback version described here:
            // https://stackoverflow.com/questions/19317064/disconnecting-an-element-from-any-unspecified-parent-container-in-wpf

            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (child == null || parent == null) return;

            if (parent is Decorator decorator && decorator.Child == child)
                decorator.Child = null;
            if (parent is ContentPresenter presenter && presenter.Content == child)
                presenter.Content = null;
            if (parent is ContentControl cc && cc.Content == child)
                cc.Content = null;
            if (parent is Panel panel)
                panel.Children.Remove(child);
            if (parent is ItemsControl itemsControl)
                itemsControl.Items.Remove(child);
        }

    }

}
