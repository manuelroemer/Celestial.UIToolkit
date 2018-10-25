using ControlGallery.Common;
using ControlGallery.Data;
using MahApps.Metro.IconPacks;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlGallery.Xaml
{

    public class MainWindowViewModel : NavigationAwareViewModel
    {

        private IRegionManager _regionManager;
        private IControlDataSource _controlDataSource;

        public ObservableCollection<NavigationMenuItemInfo> MenuItems { get; }

        private NavigationMenuItemInfo _currentMenuItem;
        public NavigationMenuItemInfo CurrentMenuItem
        {
            get { return _currentMenuItem; }
            set
            {
                // When the current nav. menu item gets changed, navigate to the new page.
                SetProperty(ref _currentMenuItem, value);
                NavigateToCurrentMenuItem();
            }
        }

        private GalleryPageViewModel _currentPage;
        public GalleryPageViewModel CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }

        public MainWindowViewModel(
            IControlDataSource controlDataSource,
            IRegionManager regionManager)
        {
            _controlDataSource = controlDataSource;
            _regionManager = regionManager;
            MenuItems = new ObservableCollection<NavigationMenuItemInfo>();
            
            InitializeMenuItems();

            // The following is ugly, but ultimately, it works.
            // In the MainWindow, we don't have a default NavigationService, since we aren't in
            // a region.
            // In addition, the RegionManager is not initialized when this Constructor is called.
            // To get the "Main" region's NavigationService (to allow going Back via the global
            // NavigationView), we can wait until the Region Collection gets populated.
            // Then we can grab the NavigationService of the Main region.
            regionManager.Regions.CollectionChanged += (sender, e) =>
            {
                if (NavigationService == null)
                {
                    foreach (var region in regionManager.Regions)
                    {
                        if (region.Name == RegionNames.Main)
                        {
                            NavigationService = region.NavigationService;
                        }
                    }
                }
            };
        }

        private void InitializeMenuItems()
        {
            MenuItems.Clear();
            CreateHomeMenuItem();
            CreateCategoryMenuItems();
        }

        private void CreateHomeMenuItem()
        {
            MenuItems.Add(new NavigationMenuItemInfo(
                "Home",
                new PackIconMaterial() { Kind = PackIconMaterialKind.Home },
                typeof(HomePage),
                null));
        }

        private void CreateCategoryMenuItems()
        {
            // We have a lot of controls to display.
            // To not clutter the navigation menu, they are grouped into categories.
            // Display each control category as an item in the nav. menu.
            foreach (var category in _controlDataSource.GetCategories())
            {
                CreateControlCategoryMenuItem(category);
            }
        }

        private void CreateControlCategoryMenuItem(ControlCategory category)
        {
            // The CategoryPage expects the Category-VM to be displayed as a navigation
            // parameter. If not provided, nothing will be displayed.
            var navParams = new NavigationParameters();
            navParams.Add(CategoryPageViewModel.CategoryParameterName, category);

            var item = new NavigationMenuItemInfo(
                category.Name,
                category.Icon,
                typeof(CategoryPage),
                navParams);
            MenuItems.Add(item);
        }

        private void NavigateToCurrentMenuItem()
        {
            _regionManager.RequestNavigate(
                RegionNames.Main,
                CurrentMenuItem.NavigationTarget,
                CurrentMenuItem.NavigationParameters);
        }
        
    }

    /// <summary>
    /// Data class for a menu item in the main window.
    /// </summary>
    public class NavigationMenuItemInfo : BindableBase
    {

        public string Title { get; }

        public object Icon { get; }

        public Type NavigationTarget { get; }

        public NavigationParameters NavigationParameters { get; }
        
        public NavigationMenuItemInfo(
            string title, 
            object icon,
            Type navigationTarget,
            NavigationParameters navigationParameters)
        {
            Title = title;
            Icon = icon;
            NavigationTarget = navigationTarget;
            NavigationParameters = navigationParameters;
        }

    }

}
