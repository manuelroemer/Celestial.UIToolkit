using System;
using System.Windows;

namespace Celestial.UIToolkit.Tests.Resources
{

    /// <summary>
    /// Provides keys for the resources in the "SampleDictionary.xaml"
    /// resource dictionary.
    /// </summary>
    public static class SampleDictionaryKeys
    {

        public static readonly Uri DictionaryUri;

        public static readonly string CheckBoxStyleKey = "CheckBoxStyle";
        public static readonly string PointResourceKey = "PointResource";
        public static readonly string ThicknessResourceKey = "ThicknessResource";
        public static readonly string CornerRadiusResourceKey = "CornerRadiusResource";

        static SampleDictionaryKeys()
        {
            InitializePackUriScheme();
            DictionaryUri = new Uri(
                "pack://application:,,,/Celestial.UIToolkit.Core.Tests;component/Resources/SampleDictionary.xaml", 
                UriKind.RelativeOrAbsolute);
        }

        private static void InitializePackUriScheme()
        {
            // Creating a new Application instance registers the required pack:// scheme.
            // Without this, creating such an URI will throw an UriFormatException.
            new Application();
        }

    }

}
