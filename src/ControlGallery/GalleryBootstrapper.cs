using ControlGallery.Xaml;
using Prism.Unity;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using System.Reflection;
using System.Globalization;
using System;
using ControlGallery.Data;
using ControlGallery.Common;
using Prism.Regions;

namespace ControlGallery
{

    public class GalleryBootstrapper : UnityBootstrapper
    {

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IControlDataSource, ControlDataSource>(
                new ContainerControlledLifetimeManager());
            
            Container.RegisterNavigationTarget<HomePage>();
            Container.RegisterNavigationTarget<CategoryPage>();
            Container.RegisterNavigationTarget<ControlPage>();
        }
        
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Container.Resolve<IRegionManager>()
                     .RequestNavigate<HomePage>(RegionNames.Main);
            Application.Current.MainWindow.Show();
        }
        
        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = string.Format(
                    CultureInfo.InvariantCulture, 
                    "{0}{1}, {2}", 
                    viewName, suffix, viewAssemblyName);
                return Type.GetType(viewModelName);
            });

            ViewModelLocationProvider.SetDefaultViewModelFactory(type =>
                Container.Resolve(type));
        }

    }

}
