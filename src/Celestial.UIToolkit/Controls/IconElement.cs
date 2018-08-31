using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// The base class for an icon UI element.
    /// </summary>
    public class IconElement : FrameworkElement
    {

        /// <summary>
        /// Identifies the <see cref="Foreground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
            nameof(Foreground), 
            typeof(Brush), 
            typeof(IconElement), 
            new FrameworkPropertyMetadata(
                SystemColors.ControlTextBrush,
                FrameworkPropertyMetadataOptions.Inherits |
                FrameworkPropertyMetadataOptions.AffectsRender));
        
        /// <summary>
        /// Gets or sets a <see cref="Brush"/> which identifies the icon's color.
        /// </summary>
        public Brush Foreground
        {
            get { return GetForeground(this); }
            set { SetForeground(this, value); }
        }

        static IconElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IconElement), new FrameworkPropertyMetadata(typeof(IconElement)));
        }

        /// <summary>
        /// Gets the value of the <see cref="ForegroundProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ForegroundProperty"/> attached dependency property,
        /// which is of type <see cref="Brush"/>.
        /// </returns>
        public static Brush GetForeground(DependencyObject obj) =>
            (Brush)obj.GetValue(ForegroundProperty);

        /// <summary>
        /// Sets the value of the <see cref="ForegroundProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetForeground(DependencyObject obj, Brush value) =>
            obj.SetValue(ForegroundProperty, value);

    }

}
