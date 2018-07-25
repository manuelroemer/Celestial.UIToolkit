using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{
#if false // Temporarily exclude this, as it is irrelevant right now
    /// <summary>
    /// Represents a button control which, in addition to its content,
    /// displays an <see cref="IconElement"/>.
    /// </summary>
    public class IconButton : Button
    {
        
        /// <summary>
        /// Identifies the <see cref="Icon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(IconElement), typeof(IconButton), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the <see cref="IconElement"/> which is being displayed
        /// by the button.
        /// </summary>
        public IconElement Icon
        {
            get { return (IconElement)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        
        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IconButton),
                new FrameworkPropertyMetadata(typeof(IconButton)));
        }

    }
#endif
}
