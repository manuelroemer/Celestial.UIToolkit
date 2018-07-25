using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides attached dependency properties which can be used by styles
    /// within different control states.
    /// This class provides properties for when a control is disabled.
    /// </summary>
    public static class DisabledProperties
    { 

        /// <summary>
        /// Identifies the BackgroundColor attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.RegisterAttached(
            "BackgroundColor", typeof(Color), typeof(DisabledProperties), new PropertyMetadata(Colors.Transparent));
        
        /// <summary>
        /// Identifies the BorderColor attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.RegisterAttached(
            "BorderColor", typeof(Color), typeof(DisabledProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Identifies the ForegroundBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "ForegroundBrush", typeof(Brush), typeof(DisabledProperties), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets the value of the <see cref="BackgroundColorProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BackgroundColorProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BackgroundColorProperty"/> attached dependency property,
        /// which is of type <see cref="Color"/>.
        /// </returns>
        public static Color GetBackgroundColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(BackgroundColorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="BackgroundColorProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BackgroundColorProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetBackgroundColor(DependencyObject obj, Color value)
        {
            obj.SetValue(BackgroundColorProperty, value);
        }
        
        /// <summary>
        /// Gets the value of the <see cref="BorderColorProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BorderColorProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BorderColorProperty"/> attached dependency property,
        /// which is of type <see cref="Color"/>.
        /// </returns>
        public static Color GetBorderColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(BorderColorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="BorderColorProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BorderColorProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetBorderColor(DependencyObject obj, Color value)
        {
            obj.SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="ForegroundBrushProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ForegroundBrushProperty"/> attached dependency property,
        /// which is of type <see cref="Brush"/>.
        /// </returns>
        public static Brush GetForegroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForegroundBrushProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ForegroundBrushProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetForegroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForegroundBrushProperty, value);
        }

    }

}
