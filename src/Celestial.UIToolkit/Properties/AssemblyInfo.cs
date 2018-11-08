using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

// Allow the Test Project to see (and test) internal classes.
[assembly: InternalsVisibleTo("Celestial.UIToolkit.Tests")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Controls")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Theming")]
[assembly: XmlnsPrefix("http://celestial-ui.com", "c")]
