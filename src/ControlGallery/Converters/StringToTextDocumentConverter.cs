using Celestial.UIToolkit.Converters;
using ICSharpCode.AvalonEdit.Document;
using System.Globalization;

namespace ControlGallery.Converters
{

    public class StringToTextDocumentConverter : ValueConverter<string, TextDocument>
    {

        public static StringToTextDocumentConverter Default { get; } 
            = new StringToTextDocumentConverter();

        public override TextDocument Convert(string value, object parameter, CultureInfo culture)
        {
            return new TextDocument(value);
        }

    }

}
