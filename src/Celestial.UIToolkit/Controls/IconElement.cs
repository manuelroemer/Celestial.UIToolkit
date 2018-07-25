using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// The base class for an icon UI element.
    /// These elements are being used for instances, where icons are supposed to be displayed
    /// in a single, distinct brush.
    /// </summary>
    public class IconElement : FrameworkElement
    {

        /// <summary>
        /// Identifies the <see cref="Foreground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            nameof(Foreground), 
            typeof(Brush), 
            typeof(IconElement), 
            new FrameworkPropertyMetadata(
                SystemColors.ControlTextBrush,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets a <see cref="Brush"/> which identifies the icon's color.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

    }

}
