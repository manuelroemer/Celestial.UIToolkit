using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using Celestial.UIToolkit.Common;
using ShowMeTheXAML;

namespace ControlGallery.Controls
{
    
    public class XamlFormatterEx : Singleton<XamlFormatterEx>, IXamlFormatter
    {
        
        private XamlFormatterEx() { }

        public string FormatXaml(string xaml)
        {
            return PrettyXml(xaml);
        }

        static string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            // Skip the XamlDisplayElement.
            var element = XElement.Parse(xml);
            element = element.Elements().First();

            var settings = new XmlWriterSettings()
            {
                DoNotEscapeUriAttributes = true,
                Indent = true,
                IndentChars = "    ",
                NewLineOnAttributes = true,
                OmitXmlDeclaration = true
            };

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

    }

}
