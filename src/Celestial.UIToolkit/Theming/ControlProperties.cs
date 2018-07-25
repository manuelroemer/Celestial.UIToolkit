using System.Windows;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides attached dependency properties for generic control properties 
    /// which can be used by styles.
    /// </summary>
    public static class ControlProperties
    {

        /// <summary>
        /// Identifies the CornerRadius attached dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(CornerRadius), typeof(ControlProperties), new PropertyMetadata(new CornerRadius()));

        /// <summary>
        /// Gets the value of the <see cref="CornerRadiusProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="CornerRadiusProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="CornerRadiusProperty"/> attached dependency property,
        /// which is of type <see cref="CornerRadius"/>.
        /// </returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="CornerRadiusProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="CornerRadiusProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

    }

}
