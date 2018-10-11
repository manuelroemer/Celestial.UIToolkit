using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// The base class for an icon UI element.
    /// </summary>
    public class IconElement : ConstructedFrameworkElement
    {

        /// <summary>
        /// Identifies the <see cref="Foreground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            TextElement.ForegroundProperty.AddOwner(typeof(IconElement));
        
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
        /// Gets the value of the <see cref="ForegroundProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ForegroundProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ForegroundProperty"/> attached dependency property.
        /// </returns>
        public static Brush GetForeground(DependencyObject obj) =>
            (Brush)obj.GetValue(ForegroundProperty);

        /// <summary>
        /// Sets the value of the <see cref="ForegroundProperty"/> attached dependency property.
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
        
        /// <summary>
        /// Sets the <see cref="ConstructedFrameworkElement.Child"/> to a <see cref="Viewbox"/>
        /// which contains the specified <paramref name="child"/>.
        /// This can be used by deriving classes to correctly scale an element which displays
        /// an icon's content.
        /// </summary>
        /// <param name="child">The child to be put into a <see cref="Viewbox"/>.</param>
        internal void SetChildInViewbox(UIElement child)
        {
            if (child == null)
            {
                // If we aren't displaying anything, there is no need for a Viewbox.
                // Save that memory.
                Child = null;
            }
            else
            {
                Child = new Viewbox()
                {
                    Child = child
                };
            }
        }

    }

}
