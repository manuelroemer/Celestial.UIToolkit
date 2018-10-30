using System.Windows;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides static theming properties regarding selection states.
    /// </summary>
    public static class SelectionProperties
    {
        
        /// <summary>
        /// Identifies an attached dependency property which determines whether the control
        /// displays another control which indicates a selection.
        /// </summary>
        public static readonly DependencyProperty ShowSelectionIndicatorProperty =
            DependencyProperty.RegisterAttached(
                "ShowSelectionIndicator",
                typeof(bool),
                typeof(SelectionProperties),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets the value of the <see cref="ShowSelectionIndicatorProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShowSelectionIndicatorProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShowSelectionIndicatorProperty"/> attached dependency property.
        /// </returns>
        public static bool GetShowSelectionIndicator(DependencyObject obj) =>
            (bool)obj.GetValue(ShowSelectionIndicatorProperty);

        /// <summary>
        /// Sets the value of the <see cref="ShowSelectionIndicatorProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShowSelectionIndicatorProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShowSelectionIndicator(DependencyObject obj, bool value) =>
            obj.SetValue(ShowSelectionIndicatorProperty, value);



        /// <summary>
        /// Identifies an attached dependency property which determines the size (width or height)
        /// of the selection indicator which is displayed.
        /// </summary>
        public static readonly DependencyProperty SelectionIndicatorSizeProperty =
            DependencyProperty.RegisterAttached(
                "SelectionIndicatorSize",
                typeof(double),
                typeof(SelectionProperties),
                new PropertyMetadata(4d));

        /// <summary>
        /// Gets the value of the <see cref="SelectionIndicatorSizeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="SelectionIndicatorSizeProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="SelectionIndicatorSizeProperty"/> attached dependency property.
        /// </returns>
        public static double GetSelectionIndicatorSize(DependencyObject obj) =>
            (double)obj.GetValue(SelectionIndicatorSizeProperty);

        /// <summary>
        /// Sets the value of the <see cref="SelectionIndicatorSizeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="SelectionIndicatorSizeProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetSelectionIndicatorSize(DependencyObject obj, double value) =>
            obj.SetValue(SelectionIndicatorSizeProperty, value);

    }

}
