using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Defines static theming properties for a <see cref="TabItem"/>.
    /// </summary>
    public static class TabItemProperties
    {

        /// <summary>
        /// Identifies the HeaderForeground attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.RegisterAttached(
                "HeaderForeground",
                typeof(Brush),
                typeof(TabItemProperties),
                new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets the value of the <see cref="HeaderForegroundProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderForegroundProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeaderForegroundProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetHeaderForeground(DependencyObject obj) =>
            (Brush)obj.GetValue(HeaderForegroundProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeaderForegroundProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderForegroundProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeaderForeground(DependencyObject obj, Brush value) =>
            obj.SetValue(HeaderForegroundProperty, value);



        /// <summary>
        /// Identifies the HeaderFontSize attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "HeaderFontSize",
                typeof(double),
                typeof(TabItemProperties),
                new PropertyMetadata(0d));

        /// <summary>
        /// Gets the value of the <see cref="HeaderFontSizeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontSizeProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeaderFontSizeProperty"/> attached dependency property.
        /// </returns>
        public static double GetHeaderFontSize(DependencyObject obj) =>
            (double)obj.GetValue(HeaderFontSizeProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeaderFontSizeProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontSizeProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeaderFontSize(DependencyObject obj, double value) =>
            obj.SetValue(HeaderFontSizeProperty, value);



        /// <summary>
        /// Identifies the HeaderFontFamily attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontFamilyProperty =
            DependencyProperty.RegisterAttached(
                "HeaderFontFamily",
                typeof(FontFamily),
                typeof(TabItemProperties),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the value of the <see cref="HeaderFontFamilyProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontFamilyProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeaderFontFamilyProperty"/> attached dependency property.
        /// </returns>
        public static FontFamily GetHeaderFontFamily(DependencyObject obj) =>
            (FontFamily)obj.GetValue(HeaderFontFamilyProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeaderFontFamilyProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontFamilyProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeaderFontFamily(DependencyObject obj, FontFamily value) =>
            obj.SetValue(HeaderFontFamilyProperty, value);



        /// <summary>
        /// Identifies the FontStretch attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontStretchProperty =
            DependencyProperty.RegisterAttached(
                "HeaderFontStretch",
                typeof(FontStretch),
                typeof(TabItemProperties),
                new PropertyMetadata(FontStretches.Normal));

        /// <summary>
        /// Gets the value of the <see cref="HeaderFontStretchProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontStretchProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeaderFontStretchProperty"/> attached dependency property.
        /// </returns>
        public static FontStretch GetHeaderFontStretch(DependencyObject obj) =>
            (FontStretch)obj.GetValue(HeaderFontStretchProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeaderFontStretchProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontStretchProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeaderFontStretch(DependencyObject obj, FontStretch value) =>
            obj.SetValue(HeaderFontStretchProperty, value);

        

        /// <summary>
        /// Identifies the HeaderFontStyle attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontStyleProperty =
            DependencyProperty.RegisterAttached(
                "HeaderFontStyle",
                typeof(FontStyle),
                typeof(TabItemProperties),
                new PropertyMetadata(FontStyles.Normal));

        /// <summary>
        /// Gets the value of the <see cref="HeaderFontStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontStyleProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeaderFontStyleProperty"/> attached dependency property.
        /// </returns>
        public static FontStyle GetHeaderFontStyle(DependencyObject obj) =>
            (FontStyle)obj.GetValue(HeaderFontStyleProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeaderFontStyleProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontStyleProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeaderFontStyle(DependencyObject obj, FontStyle value) =>
            obj.SetValue(HeaderFontStyleProperty, value);



        /// <summary>
        /// Identifies the HeaderFontWeight attached dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderFontWeightProperty =
            DependencyProperty.RegisterAttached(
                "HeaderFontWeight",
                typeof(FontWeight),
                typeof(TabItemProperties),
                new PropertyMetadata(FontWeights.Normal));

        /// <summary>
        /// Gets the value of the <see cref="HeaderFontWeightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontWeightProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="HeaderFontWeightProperty"/> attached dependency property.
        /// </returns>
        public static FontWeight GetHeaderFontWeight(DependencyObject obj) =>
            (FontWeight)obj.GetValue(HeaderFontWeightProperty);

        /// <summary>
        /// Sets the value of the <see cref="HeaderFontWeightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="HeaderFontWeightProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetHeaderFontWeight(DependencyObject obj, FontWeight value) =>
            obj.SetValue(HeaderFontWeightProperty, value);
        
    }

}
