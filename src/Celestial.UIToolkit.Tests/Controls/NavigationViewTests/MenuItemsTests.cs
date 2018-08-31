using Celestial.UIToolkit.Controls;
using Celestial.UIToolkit.Extensions;
using System.Linq;
using Xunit;

namespace Celestial.UIToolkit.Tests.Controls.NavigationViewTests
{

    public class MenuItemsTests
    {

        [WpfFact]
        public void FiresMenuItemsSelectionChangedEvent()
        {
            var navView = new NavigationView();
            navView.MenuItems.AddRange(new object[] { 1, 2, 3, "Four", "Five" });

            Assert.Raises<NavigationViewItemEventArgs>(
                (handler) => navView.MenuItemsSelectionChanged += handler,
                (handler) => navView.MenuItemsSelectionChanged -= handler,
                () => navView.SelectedMenuItem = navView.MenuItems.First());
        }

        [WpfFact]
        public void CanOnlySwitchToKnownSelectedMenuItem()
        {
            var navView = new NavigationView();
            navView.MenuItems.Add("ExistingItem");

            navView.SelectedMenuItem = 1; // Does not exist in MenuItems collection.
            Assert.Null(navView.SelectedMenuItem);

            navView.SelectedMenuItem = navView.MenuItems.First();
            Assert.NotNull(navView.SelectedMenuItem);
        }

        [WpfFact]
        public void CanSetItemsViaItemsSource()
        {
            var navView = new NavigationView();
            var itemsSource = new int[] { 1, 2, 3 };

            navView.MenuItemsSource = itemsSource;
            Assert.True(
                navView.MenuItems.Cast<int>().SequenceEqual(itemsSource));
        }

    }

}
