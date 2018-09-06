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


        
        /// <summary>
        /// Identifies an attached dependency property which determines whether the 
        /// <see cref="TextBox"/> shows/animates an indicator element about its focused
        /// state.
        /// </summary>
        public static readonly DependencyProperty ShowFocusedIndicatorProperty =
            DependencyProperty.RegisterAttached(
                "ShowFocusedIndicator",
                typeof(bool),
                typeof(TextBoxProperties),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets the value of the <see cref="ShowFocusedIndicatorProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShowFocusedIndicatorProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShowFocusedIndicatorProperty"/> attached dependency property.
        /// </returns>
        public static bool GetShowFocusedIndicator(DependencyObject obj) =>
            (bool)obj.GetValue(ShowFocusedIndicatorProperty);

        /// <summary>
        /// Sets the value of the <see cref="ShowFocusedIndicatorProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShowFocusedIndicatorProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShowFocusedIndicator(DependencyObject obj, bool value) =>
            obj.SetValue(ShowFocusedIndicatorProperty, value);

    }

}
