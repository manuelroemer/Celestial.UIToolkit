using Celestial.UIToolkit.Converters;
using System.Globalization;
using System.Windows.Media;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class ColorToSolidColorBrushConverterTests
    {

        [Fact]
        public void ConvertsColorToBrush()
        {
            var converter = new ColorToSolidColorBrushConverter();
            SolidColorBrush result = converter.Convert(Colors.Red, null, CultureInfo.CurrentCulture);
            
            Assert.Equal(Colors.Red, result.Color);
        }

        [Fact]
        public void ConvertsBrushBackToColor()
        {
            var converter = new ColorToSolidColorBrushConverter();
            Color result = converter.ConvertBack(Brushes.Red, null, CultureInfo.CurrentCulture);

            Assert.Equal(Colors.Red, result);
        }

    }

}
