using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides attached dependency properties for controls which want to support placeholders
    /// instead of empty content.
    /// </summary>
    public static class PlaceholderProperties
    {
        
        /// <summary>
        /// Identifies an attached dependency property which defines a placeholder object which
        /// can be displayed by empty controls instead of no content.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(object),
                typeof(PlaceholderProperties),
                new PropertyMetadata(
                    null,
                    Placeholder_Changed));

        /// <summary>
        /// Gets the value of the <see cref="PlaceholderProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="PlaceholderProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="PlaceholderProperty"/> attached dependency property.
        /// </returns>
        public static object GetPlaceholder(DependencyObject obj) =>
            (object)obj.GetValue(PlaceholderProperty);

        /// <summary>
        /// Sets the value of the <see cref="PlaceholderProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="PlaceholderProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetPlaceholder(DependencyObject obj, object value) =>
            obj.SetValue(PlaceholderProperty, value);

        private static void Placeholder_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetHasPlaceholder(d, e.NewValue != null);
        }



        private static readonly DependencyPropertyKey HasPlaceholderPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "HasPlaceholder",
                typeof(bool),
                typeof(PlaceholderProperties),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies a read-only attached dependency property which indicates whether
        /// the <see cref="PlaceholderProperty"/> is set on a given dependency object.
        /// </summary>
        public static readonly DependencyProperty HasPlaceholderProperty =
            HasPlaceholderPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the value of the <see cref="HasPlaceholderProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HasPlaceholderProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HasPlaceholderProperty"/> attached dependency property.
        /// </returns>
        public static bool GetHasPlaceholder(DependencyObject obj) =>
            (bool)obj.GetValue(HasPlaceholderProperty);

        private static void SetHasPlaceholder(DependencyObject obj, bool value) =>
            obj.SetValue(HasPlaceholderPropertyKey, value);


        
    }

}
