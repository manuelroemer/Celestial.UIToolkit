using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A custom <see cref="ListView"/> which is supposed to be used with a 
    /// <see cref="NavigationView"/> for displaying the <see cref="NavigationView.MenuItems"/>.
    /// In comparison to a normal <see cref="ListView"/>, this class provides events for when
    /// an item gets invoked, creates <see cref="NavigationViewItem"/> container elements by
    /// default and disallows Keyboard Navigation.
    /// </summary>
    public class NavigationViewListView : ExtendedListView
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewListView"/> class.
        /// </summary>
        public NavigationViewListView()
        {
        }

        /// <summary>
        /// Creates and returns a new <see cref="NavigationViewItem"/> container.
        /// </summary>
        /// <returns>A new <see cref="NavigationViewItem"/>.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NavigationViewItem();
        }

    }

}
