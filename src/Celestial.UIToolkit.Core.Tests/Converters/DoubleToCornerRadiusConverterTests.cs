using Celestial.UIToolkit.Converters;
using System.Windows;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class DoubleToCornerRadiusConverterTests
    {

        [Fact]
        public void ConvertsDoubleToUniformCornerRadius()
        {
            var converter = new DoubleToCornerRadiusConverter();
            CornerRadius result = converter.Convert(5d, null, null);
            
            Assert.True(result.BottomLeft == result.BottomRight &&
                        result.BottomLeft == result.TopLeft &&
                        result.BottomLeft == result.TopRight &&
                        result.BottomLeft == 5);
        }

    }

}
