using Celestial.UIToolkit.Extensions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Provides attached dependency properties regarding margins
    /// which can be helpful when creating layouts.
    /// </summary>
    public static class MarginHelper
    {
        
        /// <summary>
        /// Identifies the ChildrenMargin attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ChildrenMarginProperty = DependencyProperty.RegisterAttached(
            "ChildrenMargin", 
            typeof(Thickness), 
            typeof(MarginHelper), 
            new PropertyMetadata(
                new Thickness(), 
                ChildrenMargin_Changed));
        
        /// <summary>
        /// Gets the value of the <see cref="ChildrenMarginProperty"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be retrieved.</param>
        /// <returns>A <see cref="string"/>.</returns>
        public static Thickness GetChildrenMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ChildrenMarginProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ChildrenMarginProperty"/> for the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The dependency object for which the property should be set.</param>
        /// <param name="value">A <see cref="Thickness"/>.</param>
        public static void SetChildrenMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ChildrenMarginProperty, value);
        }


        
        /// <summary>
        /// Called when the <see cref="ChildrenMarginProperty"/> of a <see cref="DependencyObject"/> is changed.
        /// When this happens, this handler iterates through each of the object's children (if any can be found)
        /// and sets their margin to the value of the <see cref="ChildrenMarginProperty"/>.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> whose <see cref="ChildrenMarginProperty"/> got changed.</param>
        /// <param name="e">Event args about the changed property.</param>
        private static void ChildrenMargin_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is FrameworkElement frameworkElement)
            {
                if (!frameworkElement.IsInitialized)
                {
                    frameworkElement.Initialized += Element_Initialized;
                }
                else
                {
                    SetChildrenMargin(frameworkElement, (Thickness)e.NewValue);
                }
            }
            else
            {
                UpdateChildrenMargins(obj, (Thickness)e.NewValue);
            }

            void Element_Initialized(object sender, EventArgs args)
            {
                frameworkElement.Initialized -= Element_Initialized;
                UpdateChildrenMargins(obj, (Thickness)e.NewValue);
            }
        }
        
        private static void UpdateChildrenMargins(DependencyObject parent, Thickness margin)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement child)
                {
                    Debug.WriteLine(child.ToString());
                    // If a child's margin is explicitly set, don't overwrite it.
                    if (!child.IsDependencyPropertySet(FrameworkElement.MarginProperty))
                    {
                        child.Margin = margin;
                    }
                }
            }
        }

    }
    
}
