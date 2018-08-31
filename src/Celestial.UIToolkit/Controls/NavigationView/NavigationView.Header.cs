using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {
        
        /// <summary>
        /// Identifies the <see cref="AlwaysShowHeader"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlwaysShowHeaderProperty =
            DependencyProperty.Register(
                nameof(AlwaysShowHeader),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="HeaderedContentControl.Header"/>
        /// content of this <see cref="NavigationView"/> is always visible.
        /// If false, the header is only shown, when the <see cref="NavigationView"/> is in the
        /// <see cref="NavigationViewDisplayMode.Minimal"/> display mode.
        /// </summary>
        public bool AlwaysShowHeader
        {
            get { return (bool)GetValue(AlwaysShowHeaderProperty); }
            set { SetValue(AlwaysShowHeaderProperty, value); }
        }
        
    }

}
