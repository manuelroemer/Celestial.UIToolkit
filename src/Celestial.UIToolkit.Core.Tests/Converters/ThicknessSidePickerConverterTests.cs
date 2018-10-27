using Celestial.UIToolkit.Converters;
using System.Windows;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class ThicknessSidePickerConverterTests
    {

        [Fact]
        public void ChangesAllSides()
        {
            var converter = new ThicknessSidePickerConverter()
            {
                Top = 5,
                Left = 5,
                Right = 5,
                Bottom = 5
            };

            var thickness = new Thickness(1, 2, 3, 4);
            var expectedThickness = new Thickness(5);

            Assert.Equal(
                expectedThickness,
                converter.Convert(thickness, null, null));
        }

        [Fact]
        public void KeepsAllSides()
        {
            var converter = new ThicknessSidePickerConverter();
            var thickness = new Thickness(1, 2, 3, 4);
            Assert.Equal(
                thickness,
                converter.Convert(thickness, null, null));
        }

    }

}
