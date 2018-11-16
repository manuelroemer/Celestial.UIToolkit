using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;

[assembly: InternalsVisibleTo("Celestial.UIToolkit")]
[assembly: InternalsVisibleTo("Celestial.UIToolkit.Core.Tests")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Converters")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Interactions")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Interactivity")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Media")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Media.Animations")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Xaml")]
[assembly: XmlnsPrefix("http://celestial-ui.com", "c")]
