using Microsoft.Practices.Unity;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGallery.Common
{


    /// <summary>
    /// Provides static methods for type-based region navigation using Prism.
    /// This follows the convention that every navigation target is registered under
    /// its type's full name.
    /// </summary>
    public static class RegionNavigationExtensions
    {

        public static void RequestNavigate<T>(
            this IRegionManager regionManager,
            string regionName,
            NavigationParameters navigationParams = null)
        {
            regionManager.RequestNavigate(regionName, typeof(T), navigationParams);
        }

        public static void RequestNavigate(
            this IRegionManager regionManager,
            string regionName,
            Type targetType,
            NavigationParameters navigationParams = null)
        {
            regionManager.RequestNavigate(regionName, targetType.FullName, navigationParams);
        }

        public static void RequestNavigate<T>(
            this IRegionNavigationService navigationService,
            NavigationParameters navigationParams = null)
        {
            navigationService.RequestNavigate(typeof(T), navigationParams);
        }

        public static void RequestNavigate(
            this IRegionNavigationService navigationService,
            Type targetType,
            NavigationParameters navigationParams = null)
        {
            navigationService.RequestNavigate(targetType.FullName, navigationParams);
        }

        public static void RegisterNavigationTarget<T>(this IUnityContainer container)
        {
            container.RegisterTypeForNavigation<T>(typeof(T).FullName);
        }

    }

}
