using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides static, attached dependency properties which are used for styling
    /// the title bar of a <see cref="Window"/>.
    /// </summary>
    public class WindowTitlebarProperties
    {

        #region ExtendContentIntoTitlebar

        /// <summary>
        /// Identifies the ExtendContentIntoTitlebar attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ExtendContentIntoTitlebarProperty =
            DependencyProperty.RegisterAttached(
                "ExtendContentIntoTitlebar",
                typeof(bool),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets the value of the <see cref="ExtendContentIntoTitlebarProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ExtendContentIntoTitlebarProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ExtendContentIntoTitlebarProperty"/> attached dependency property.
        /// </returns>
        public static bool GetExtendContentIntoTitlebar(DependencyObject obj) =>
            (bool)obj.GetValue(ExtendContentIntoTitlebarProperty);

        /// <summary>
        /// Sets the value of the <see cref="ExtendContentIntoTitlebarProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ExtendContentIntoTitlebarProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetExtendContentIntoTitlebar(DependencyObject obj, bool value) =>
            obj.SetValue(ExtendContentIntoTitlebarProperty, value);

        #endregion

        #region Height

        /// <summary>
        /// Identifies the Height attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached(
                "Height",
                typeof(double),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(SystemParameters.WindowCaptionHeight));

        /// <summary>
        /// Gets the value of the <see cref="HeightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeightProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeightProperty"/> attached dependency property.
        /// </returns>
        public static double GetHeight(DependencyObject obj) =>
            (double)obj.GetValue(HeightProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeightProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeight(DependencyObject obj, double value) =>
            obj.SetValue(HeightProperty, value);

        #endregion

        #region BorderThickness

        /// <summary>
        /// Identifies the BorderThickness attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.RegisterAttached(
                "BorderThickness",
                typeof(Thickness),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(new Thickness()));

        /// <summary>
        /// Gets the value of the <see cref="BorderThicknessProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BorderThicknessProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BorderThicknessProperty"/> attached dependency property.
        /// </returns>
        public static Thickness GetBorderThickness(DependencyObject obj) =>
            (Thickness)obj.GetValue(BorderThicknessProperty);

        /// <summary>
        /// Sets the value of the <see cref="BorderThicknessProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BorderThicknessProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetBorderThickness(DependencyObject obj, Thickness value) =>
            obj.SetValue(BorderThicknessProperty, value);

        #endregion

        #region BackgroundBrush

        /// <summary>
        /// Identifies the BackgroundBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "BackgroundBrush",
                typeof(Brush),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// Gets the value of the <see cref="BackgroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BackgroundBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BackgroundBrushProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetBackgroundBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(BackgroundBrushProperty);

        /// <summary>
        /// Sets the value of the <see cref="BackgroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BackgroundBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetBackgroundBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(BackgroundBrushProperty, value);

        #endregion

        #region InactiveBackgroundBrush

        /// <summary>
        /// Identifies the InactiveBackgroundBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty InactiveBackgroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "InactiveBackgroundBrush",
                typeof(Brush),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Gets the value of the <see cref="InactiveBackgroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="InactiveBackgroundBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="InactiveBackgroundBrushProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetInactiveBackgroundBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(InactiveBackgroundBrushProperty);

        /// <summary>
        /// Sets the value of the <see cref="InactiveBackgroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="InactiveBackgroundBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetInactiveBackgroundBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(InactiveBackgroundBrushProperty, value);

        #endregion

        #region BorderBrush

        /// <summary>
        /// Identifies the BorderBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.RegisterAttached(
                "BorderBrush",
                typeof(Brush),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// Gets the value of the <see cref="BorderBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BorderBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BorderBrushProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetBorderBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(BorderBrushProperty);

        /// <summary>
        /// Sets the value of the <see cref="BorderBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BorderBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetBorderBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(BorderBrushProperty, value);

        #endregion

        #region InactiveBorderBrush

        /// <summary>
        /// Identifies the InactiveBorderBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty InactiveBorderBrushProperty =
            DependencyProperty.RegisterAttached(
                "InactiveBorderBrush",
                typeof(Brush),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(Brushes.White));

        /// <summary>
        /// Gets the value of the <see cref="InactiveBorderBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="InactiveBorderBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="InactiveBorderBrushProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetInactiveBorderBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(InactiveBorderBrushProperty);

        /// <summary>
        /// Sets the value of the <see cref="InactiveBorderBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="InactiveBorderBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetInactiveBorderBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(InactiveBorderBrushProperty, value);

        #endregion

        #region ForegroundBrush

        /// <summary>
        /// Identifies the ForegroundBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "ForegroundBrush",
                typeof(Brush),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets the value of the <see cref="ForegroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ForegroundBrushProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetForegroundBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(ForegroundBrushProperty);

        /// <summary>
        /// Sets the value of the <see cref="ForegroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetForegroundBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(ForegroundBrushProperty, value);

        #endregion

        #region InactiveForegroundBrush

        /// <summary>
        /// Identifies the InactiveForegroundBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty InactiveForegroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "InactiveForegroundBrush",
                typeof(Brush),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// Gets the value of the <see cref="InactiveForegroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="InactiveForegroundBrushProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="InactiveForegroundBrushProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetInactiveForegroundBrush(DependencyObject obj) =>
            (Brush)obj.GetValue(InactiveForegroundBrushProperty);

        /// <summary>
        /// Sets the value of the <see cref="InactiveForegroundBrushProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="InactiveForegroundBrushProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetInactiveForegroundBrush(DependencyObject obj, Brush value) =>
            obj.SetValue(InactiveForegroundBrushProperty, value);

        #endregion

        #region MinimizeButtonStyle

        /// <summary>
        /// Identifies the MinimizeButtonStyle attached dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimizeButtonStyleProperty =
            DependencyProperty.RegisterAttached(
                "MinimizeButtonStyle",
                typeof(Style),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the <see cref="MinimizeButtonStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="MinimizeButtonStyleProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="MinimizeButtonStyleProperty"/> attached dependency property.
        /// </returns>
        public static Style GetMinimizeButtonStyle(DependencyObject obj) =>
            (Style)obj.GetValue(MinimizeButtonStyleProperty);

        /// <summary>
        /// Sets the value of the <see cref="MinimizeButtonStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="MinimizeButtonStyleProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetMinimizeButtonStyle(DependencyObject obj, Style value) =>
            obj.SetValue(MinimizeButtonStyleProperty, value);

        #endregion

        #region MaximizeButtonStyle

        /// <summary>
        /// Identifies the MaximizeButtonStyle attached dependency property.
        /// </summary>
        public static readonly DependencyProperty MaximizeButtonStyleProperty =
            DependencyProperty.RegisterAttached(
                "MaximizeButtonStyle",
                typeof(Style),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the <see cref="MaximizeButtonStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="MaximizeButtonStyleProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="MaximizeButtonStyleProperty"/> attached dependency property.
        /// </returns>
        public static Style GetMaximizeButtonStyle(DependencyObject obj) =>
            (Style)obj.GetValue(MaximizeButtonStyleProperty);

        /// <summary>
        /// Sets the value of the <see cref="MaximizeButtonStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="MaximizeButtonStyleProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetMaximizeButtonStyle(DependencyObject obj, Style value) =>
            obj.SetValue(MaximizeButtonStyleProperty, value);

        #endregion

        #region CloseButtonStyle

        /// <summary>
        /// Identifies the CloseButtonStyle attached dependency property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonStyleProperty =
            DependencyProperty.RegisterAttached(
                "CloseButtonStyle",
                typeof(Style),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the <see cref="CloseButtonStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="CloseButtonStyleProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="CloseButtonStyleProperty"/> attached dependency property.
        /// </returns>
        public static Style GetCloseButtonStyle(DependencyObject obj) =>
            (Style)obj.GetValue(CloseButtonStyleProperty);

        /// <summary>
        /// Sets the value of the <see cref="CloseButtonStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="CloseButtonStyleProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetCloseButtonStyle(DependencyObject obj, Style value) =>
            obj.SetValue(CloseButtonStyleProperty, value);

        #endregion

        #region TitleFontSize

        /// <summary>
        /// Identifies the TitleFontSize attached dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "TitleFontSize",
                typeof(double),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(12d));

        /// <summary>
        /// Gets the value of the <see cref="TitleFontSizeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontSizeProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="TitleFontSizeProperty"/> attached dependency property.
        /// </returns>
        public static double GetTitleFontSize(DependencyObject obj) =>
            (double)obj.GetValue(TitleFontSizeProperty);

        /// <summary>
        /// Sets the value of the <see cref="TitleFontSizeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontSizeProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetTitleFontSize(DependencyObject obj, double value) =>
            obj.SetValue(TitleFontSizeProperty, value);

        #endregion

        #region TitleFontFamily

        /// <summary>
        /// Identifies the TitleFontFamily attached dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleFontFamilyProperty =
            DependencyProperty.RegisterAttached(
                "TitleFontFamily",
                typeof(FontFamily),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the <see cref="TitleFontFamilyProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontFamilyProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="TitleFontFamilyProperty"/> attached dependency property.
        /// </returns>
        public static FontFamily GetTitleFontFamily(DependencyObject obj) =>
            (FontFamily)obj.GetValue(TitleFontFamilyProperty);

        /// <summary>
        /// Sets the value of the <see cref="TitleFontFamilyProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontFamilyProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetTitleFontFamily(DependencyObject obj, FontFamily value) =>
            obj.SetValue(TitleFontFamilyProperty, value);

        #endregion

        #region TitleFontStretch

        /// <summary>
        /// Identifies the FontStretch attached dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleFontStretchProperty =
            DependencyProperty.RegisterAttached(
                "TitleFontStretch",
                typeof(FontStretch),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(FontStretches.Normal));

        /// <summary>
        /// Gets the value of the <see cref="TitleFontStretchProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontStretchProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="TitleFontStretchProperty"/> attached dependency property.
        /// </returns>
        public static FontStretch GetTitleFontStretch(DependencyObject obj) =>
            (FontStretch)obj.GetValue(TitleFontStretchProperty);

        /// <summary>
        /// Sets the value of the <see cref="TitleFontStretchProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontStretchProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetTitleFontStretch(DependencyObject obj, FontStretch value) =>
            obj.SetValue(TitleFontStretchProperty, value);

        #endregion

        #region TitleFontStyle

        /// <summary>
        /// Identifies the TitleFontStyle attached dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleFontStyleProperty =
            DependencyProperty.RegisterAttached(
                "TitleFontStyle",
                typeof(FontStyle),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(FontStyles.Normal));

        /// <summary>
        /// Gets the value of the <see cref="TitleFontStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontStyleProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="TitleFontStyleProperty"/> attached dependency property.
        /// </returns>
        public static FontStyle GetTitleFontStyle(DependencyObject obj) =>
            (FontStyle)obj.GetValue(TitleFontStyleProperty);

        /// <summary>
        /// Sets the value of the <see cref="TitleFontStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontStyleProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetTitleFontStyle(DependencyObject obj, FontStyle value) =>
            obj.SetValue(TitleFontStyleProperty, value);

        #endregion

        #region TitleFontWeight

        /// <summary>
        /// Identifies the TitleFontWeight attached dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleFontWeightProperty =
            DependencyProperty.RegisterAttached(
                "TitleFontWeight",
                typeof(FontWeight),
                typeof(WindowTitlebarProperties),
                new PropertyMetadata(FontWeights.Normal));

        /// <summary>
        /// Gets the value of the <see cref="TitleFontWeightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontWeightProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="TitleFontWeightProperty"/> attached dependency property.
        /// </returns>
        public static FontWeight GetTitleFontWeight(DependencyObject obj) =>
            (FontWeight)obj.GetValue(TitleFontWeightProperty);

        /// <summary>
        /// Sets the value of the <see cref="TitleFontWeightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TitleFontWeightProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetTitleFontWeight(DependencyObject obj, FontWeight value) =>
            obj.SetValue(TitleFontWeightProperty, value);

        #endregion

    }

}
