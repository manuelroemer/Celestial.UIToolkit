using ShowMeTheXAML;
using System.Windows;

namespace ControlGallery
{

    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            XamlDisplay.Init();
            new GalleryBootstrapper().Run();
        }

    }

}
