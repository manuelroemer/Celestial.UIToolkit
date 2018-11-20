using Celestial.UIToolkit.Xaml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ShowMeTheXAML;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace ControlGallery
{

    public partial class App : Application
    {
        
        public App()
        {
            // Important: Load this here, because otherwise, the XAML won't be able to see them.
            LoadAvalonEditEmbeddedXshdDefinitions();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            XamlDisplay.Init();
            ThemeManager.Current.ChangeTheme("Light");
            new GalleryBootstrapper().Run();
        }

        private static void LoadAvalonEditEmbeddedXshdDefinitions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var xshdFiles = assembly.GetManifestResourceNames().Where(file => file.EndsWith(".xshd"));

            foreach (var xshdFile in xshdFiles)
            {
                using (var stream = assembly.GetManifestResourceStream(xshdFile))
                {
                    if (stream == null)
                        continue;

                    using (var reader = new XmlTextReader(stream))
                    {
                        var xshdDefinition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                        HighlightingManager.Instance.RegisterHighlighting(
                            xshdDefinition.Name, new string[] { }, xshdDefinition
                        );
                    }
                }
            }
        }

    }

}
