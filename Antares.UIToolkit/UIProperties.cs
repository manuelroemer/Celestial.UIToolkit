using System.Windows;
using System.Windows.Media;

namespace Antares.UIToolkit
{

    /// <summary>
    /// Defines attached properties which allow altering a control's style
    /// beyond the usual properties that the control provides.
    /// </summary>
    public static class UIProperties
    {
        
        #region CornerRadius

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a control's corner radius.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
                "CornerRadius", typeof(CornerRadius), typeof(UIProperties), new PropertyMetadata(new CornerRadius(0)));

        /// <summary>
        /// Gets the current effective value of the <see cref="CornerRadiusProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="CornerRadiusProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="CornerRadius"/>.</returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="CornerRadiusProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="CornerRadiusProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The corner radius.</param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        #endregion

        #region Background Colors

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Color
        /// that is supposed to be applied to the control, when it is hovered.
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundColorProperty = DependencyProperty.RegisterAttached(
            "MouseOverBackgroundColor", typeof(Color), typeof(UIProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="MouseOverBackgroundColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="MouseOverBackgroundColorProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Color"/>.</returns>
        public static Color GetMouseOverBackgroundColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(MouseOverBackgroundColorProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="MouseOverBackgroundColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="MouseOverBackgroundColorProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The color.</param>
        public static void SetMouseOverBackgroundColor(DependencyObject obj, Color value)
        {
            obj.SetValue(MouseOverBackgroundColorProperty, value);
        }

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Color
        /// that is supposed to be applied to the control, when it is pressed.
        /// </summary>
        public static readonly DependencyProperty PressedBackgroundColorProperty = DependencyProperty.RegisterAttached(
            "PressedBackgroundColor", typeof(Color), typeof(UIProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="PressedBackgroundColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="PressedBackgroundColorProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Color"/>.</returns>
        public static Color GetPressedBackgroundColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(PressedBackgroundColorProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="PressedBackgroundColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="PressedBackgroundColorProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The color.</param>
        public static void SetPressedBackgroundColor(DependencyObject obj, Color value)
        {
            obj.SetValue(PressedBackgroundColorProperty, value);
        }

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Color
        /// that is supposed to be applied to the control, when it is disabled.
        /// </summary>
        public static readonly DependencyProperty DisabledBackgroundColorProperty = DependencyProperty.RegisterAttached(
            "DisabledBackgroundColor", typeof(Color), typeof(UIProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="DisabledBackgroundColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="DisabledBackgroundColorProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Color"/>.</returns>
        public static Color GetDisabledBackgroundColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(DisabledBackgroundColorProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="DisabledBackgroundColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="DisabledBackgroundColorProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The color.</param>
        public static void SetDisabledBackgroundColor(DependencyObject obj, Color value)
        {
            obj.SetValue(DisabledBackgroundColorProperty, value);
        }

        #endregion

        #region Border Colors

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Color
        /// that is supposed to be applied to the control, when it is hovered.
        /// </summary>
        public static readonly DependencyProperty MouseOverBorderColorProperty = DependencyProperty.RegisterAttached(
            "MouseOverBorderColor", typeof(Color), typeof(UIProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="MouseOverBorderColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="MouseOverBorderColorProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Color"/>.</returns>
        public static Color GetMouseOverBorderColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(MouseOverBorderColorProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="MouseOverBorderColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="MouseOverBorderColorProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The color.</param>
        public static void SetMouseOverBorderColor(DependencyObject obj, Color value)
        {
            obj.SetValue(MouseOverBorderColorProperty, value);
        }

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Color
        /// that is supposed to be applied to the control, when it is pressed.
        /// </summary>
        public static readonly DependencyProperty PressedBorderColorProperty = DependencyProperty.RegisterAttached(
            "PressedBorderColor", typeof(Color), typeof(UIProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="PressedBorderColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="PressedBorderColorProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Color"/>.</returns>
        public static Color GetPressedBorderColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(PressedBorderColorProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="PressedBorderColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="PressedBorderColorProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The color.</param>
        public static void SetPressedBorderColor(DependencyObject obj, Color value)
        {
            obj.SetValue(PressedBorderColorProperty, value);
        }

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Color
        /// that is supposed to be applied to the control, when it is disabled.
        /// </summary>
        public static readonly DependencyProperty DisabledBorderColorProperty = DependencyProperty.RegisterAttached(
            "DisabledBorderColor", typeof(Color), typeof(UIProperties), new PropertyMetadata(Colors.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="DisabledBorderColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="DisabledBorderColorProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Color"/>.</returns>
        public static Color GetDisabledBorderColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(DisabledBorderColorProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="DisabledBorderColorProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="DisabledBorderColorProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The color.</param>
        public static void SetDisabledBorderColor(DependencyObject obj, Color value)
        {
            obj.SetValue(DisabledBorderColorProperty, value);
        }

        #endregion

        #region Foreground Brushes

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Brush
        /// that is supposed to be applied to the control, when it is hovered.
        /// </summary>
        public static readonly DependencyProperty MouseOverForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "MouseOverForegroundBrush", typeof(Brush), typeof(UIProperties), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="MouseOverForegroundBrushProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="MouseOverForegroundBrushProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Brush"/>.</returns>
        public static Brush GetMouseOverForegroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverForegroundBrushProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="MouseOverForegroundBrushProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="MouseOverForegroundBrushProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The Brush.</param>
        public static void SetMouseOverForegroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverForegroundBrushProperty, value);
        }

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Brush
        /// that is supposed to be applied to the control, when it is pressed.
        /// </summary>
        public static readonly DependencyProperty PressedForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "PressedForegroundBrush", typeof(Brush), typeof(UIProperties), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="PressedForegroundBrushProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="PressedForegroundBrushProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Brush"/>.</returns>
        public static Brush GetPressedForegroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PressedForegroundBrushProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="PressedForegroundBrushProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="PressedForegroundBrushProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The Brush.</param>
        public static void SetPressedForegroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressedForegroundBrushProperty, value);
        }

        /// <summary>
        /// Identifies an attached dependency property which gets or sets a Brush
        /// that is supposed to be applied to the control, when it is disabled.
        /// </summary>
        public static readonly DependencyProperty DisabledForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "DisabledForegroundBrush", typeof(Brush), typeof(UIProperties), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets the current effective value of the <see cref="DisabledForegroundBrushProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object for which the <see cref="DisabledForegroundBrushProperty"/> 
        ///     property should be set.
        /// </param>
        /// <returns>A <see cref="Brush"/>.</returns>
        public static Brush GetDisabledForegroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DisabledForegroundBrushProperty);
        }

        /// <summary>
        ///     Sets the value of the <see cref="DisabledForegroundBrushProperty"/> dependency property.
        /// </summary>
        /// <param name="obj">
        ///     The dependency object, for which the <see cref="DisabledForegroundBrushProperty"/>
        ///     property should be set.
        /// </param>
        /// <param name="value">The Brush.</param>
        public static void SetDisabledForegroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(DisabledForegroundBrushProperty, value);
        }

        #endregion

    }

}
