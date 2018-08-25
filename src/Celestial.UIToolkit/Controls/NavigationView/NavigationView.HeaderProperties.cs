using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {
        
        /// <summary>
        /// Identifies the <see cref="IsHeaderVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsHeaderVisibleProperty =
            DependencyProperty.Register(
                nameof(IsHeaderVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="HeaderedContentControl.Header"/>
        /// content of this <see cref="NavigationView"/> is currently visible.
        /// </summary>
        public bool IsHeaderVisible
        {
            get { return (bool)GetValue(IsHeaderVisibleProperty); }
            set { SetValue(IsHeaderVisibleProperty, value); }
        }
        
    }

}
