﻿using Celestial.UIToolkit.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides static members used by <see cref="TextBox"/> control.
    /// These properties enhance the default functionality of the <see cref="TextBox"/>,
    /// if the corresponding control template adds support for them.
    /// </summary>
    public static class TextBoxProperties
    {

        /// <summary>
        /// Identifies an attached dependency property which defines a placeholder object which
        /// gets displayed by the <see cref="TextBox"/>, when no other text is entered.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(object),
                typeof(TextBoxProperties),
                new PropertyMetadata(null));

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



        /// <summary>
        /// Identifies an attached dependency property which defines the placeholder's display
        /// type.
        /// </summary>
        public static readonly DependencyProperty PlaceholderTypeProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderType",
                typeof(PlaceholderDisplayType),
                typeof(TextBoxProperties),
                new PropertyMetadata(PlaceholderDisplayType.Floating));

        /// <summary>
        /// Gets the value of the <see cref="PlaceholderTypeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="PlaceholderTypeProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="PlaceholderTypeProperty"/> attached dependency property.
        /// </returns>
        public static PlaceholderDisplayType GetPlaceholderType(DependencyObject obj) =>
            (PlaceholderDisplayType)obj.GetValue(PlaceholderTypeProperty);

        /// <summary>
        /// Sets the value of the <see cref="PlaceholderTypeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="PlaceholderTypeProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetPlaceholderType(DependencyObject obj, PlaceholderDisplayType value) =>
            obj.SetValue(PlaceholderTypeProperty, value);




        /// <summary>
        /// Identifies an attached dependency property which defines an assistive text which helps
        /// the user enter the correct data into the <see cref="TextBox"/>.
        /// </summary>
        public static readonly DependencyProperty AssistiveTextProperty =
            DependencyProperty.RegisterAttached(
                "AssistiveText",
                typeof(string),
                typeof(TextBoxProperties),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the <see cref="AssistiveTextProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="AssistiveTextProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="AssistiveTextProperty"/> attached dependency property.
        /// </returns>
        public static string GetAssistiveText(DependencyObject obj) =>
            (string)obj.GetValue(AssistiveTextProperty);

        /// <summary>
        /// Sets the value of the <see cref="AssistiveTextProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="AssistiveTextProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetAssistiveText(DependencyObject obj, string value) =>
            obj.SetValue(AssistiveTextProperty, value);
        
    }

}
