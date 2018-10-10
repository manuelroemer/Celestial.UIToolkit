using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides static properties which can be used by control templates to style properties
    /// of popups.
    /// </summary>
    public static class PopupProperties
    {

        /// <summary>
        /// Identifies the BackgroundBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "BackgroundBrush",
                typeof(Brush),
                typeof(PopupProperties),
                new PropertyMetadata(Brushes.Transparent));

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



        /// <summary>
        /// Identifies the BorderBrush attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.RegisterAttached(
                "BorderBrush",
                typeof(Brush),
                typeof(PopupProperties),
                new PropertyMetadata(Brushes.Transparent));

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



        /// <summary>
        /// Identifies the BorderThickness attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.RegisterAttached(
                "BorderThickness",
                typeof(Thickness),
                typeof(PopupProperties),
                new PropertyMetadata(new Thickness(0d)));

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



        /// <summary>
        /// Identifies the CornerRadius attached dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(PopupProperties),
                new PropertyMetadata(new CornerRadius(0d)));

        /// <summary>
        /// Gets the value of the <see cref="CornerRadiusProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="CornerRadiusProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="CornerRadiusProperty"/> attached dependency property.
        /// </returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj) =>
            (CornerRadius)obj.GetValue(CornerRadiusProperty);

        /// <summary>
        /// Sets the value of the <see cref="CornerRadiusProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="CornerRadiusProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) =>
            obj.SetValue(CornerRadiusProperty, value);




        /// <summary>
        /// Identifies the Padding attached dependency property.
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.RegisterAttached(
                "Padding",
                typeof(Thickness),
                typeof(PopupProperties),
                new PropertyMetadata(new Thickness(0d)));

        /// <summary>
        /// Gets the value of the <see cref="PaddingProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="PaddingProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="PaddingProperty"/> attached dependency property.
        /// </returns>
        public static Thickness GetPadding(DependencyObject obj) =>
            (Thickness)obj.GetValue(PaddingProperty);

        /// <summary>
        /// Sets the value of the <see cref="PaddingProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="PaddingProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetPadding(DependencyObject obj, Thickness value) =>
            obj.SetValue(PaddingProperty, value);

    }

}
