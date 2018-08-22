using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

// Allow the Test Project to see (and test) internal classes.
[assembly: InternalsVisibleTo("Celestial.UIToolkit.Tests")]

[assembly: AssemblyTitle("Celestial.UIToolkit")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Celestial.UIToolkit")]
[assembly: AssemblyCopyright("Copyright © 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyFileVersion("0.1.0.0")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Controls")]
[assembly: XmlnsDefinition("http://celestial-ui.com", "Celestial.UIToolkit.Theming")]
[assembly: XmlnsPrefix("http://celestial-ui.com", "c")]
