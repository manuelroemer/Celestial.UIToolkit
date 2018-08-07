using Celestial.UIToolkit.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Identifies attached dependency properties which can change an element's
    /// shadow.
    /// </summary>
    public class ShadowProperties
    {
        
        /// <summary>
        /// Identifies the ShadowType attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowTypeProperty = DependencyProperty.RegisterAttached(
            "ShadowType", typeof(ShadowType), typeof(ShadowProperties), new PropertyMetadata(ShadowType.Ambient));

        /// <summary>
        /// Identifies the ShadowDirection attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowDirectionProperty = DependencyProperty.RegisterAttached(
            "ShadowDirection", typeof(Dock), typeof(ShadowProperties), new PropertyMetadata(Dock.Bottom));

        /// <summary>
        /// Identifies the ShadowOpacity attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.RegisterAttached(
            "ShadowOpacity", typeof(double), typeof(ShadowProperties), new PropertyMetadata(1d));

        /// <summary>
        /// Identifies the ShadowColor attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.RegisterAttached(
            "ShadowColor", typeof(Color), typeof(ShadowProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Identifies the Elevation attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ElevationProperty = DependencyProperty.RegisterAttached(
            "Elevation", typeof(double), typeof(ShadowProperties), new PropertyMetadata(0d));

        /// <summary>
        /// Gets the value of the <see cref="ShadowTypeProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowTypeProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShadowTypeProperty"/> attached dependency property,
        /// which is of type <see cref="int"/>.
        /// </returns>
        public static int GetShadowType(DependencyObject obj)
        {
            return (int)obj.GetValue(ShadowTypeProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ShadowTypeProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowTypeProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShadowType(DependencyObject obj, int value)
        {
            obj.SetValue(ShadowTypeProperty, value);
        }
        
        /// <summary>
        /// Gets the value of the <see cref="ShadowDirectionProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowDirectionProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShadowDirectionProperty"/> attached dependency property,
        /// which is of type <see cref="Dock"/>.
        /// </returns>
        public static Dock GetShadowDirection(DependencyObject obj)
        {
            return (Dock)obj.GetValue(ShadowDirectionProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ShadowDirectionProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowDirectionProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShadowDirection(DependencyObject obj, Dock value)
        {
            obj.SetValue(ShadowDirectionProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="ShadowOpacityProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowOpacityProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShadowOpacityProperty"/> attached dependency property,
        /// which is of type <see cref="double"/>.
        /// </returns>
        public static double GetShadowOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(ShadowOpacityProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ShadowOpacityProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowOpacityProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShadowOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(ShadowOpacityProperty, value);
        }


        /// <summary>
        /// Gets the value of the <see cref="ShadowColorProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowColorProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShadowColorProperty"/> attached dependency property,
        /// which is of type <see cref="Color"/>.
        /// </returns>
        public static Color GetShadowColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(ShadowColorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ShadowColorProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowColorProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShadowColor(DependencyObject obj, Color value)
        {
            obj.SetValue(ShadowColorProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="ElevationProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ElevationProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ElevationProperty"/> attached dependency property,
        /// which is of type <see cref="double"/>.
        /// </returns>
        public static double GetElevation(DependencyObject obj)
        {
            return (double)obj.GetValue(ElevationProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ElevationProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ElevationProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetElevation(DependencyObject obj, double value)
        {
            obj.SetValue(ElevationProperty, value);
        }

    }

}
