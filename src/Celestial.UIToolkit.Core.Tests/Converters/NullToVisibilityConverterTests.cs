using Celestial.UIToolkit.Converters;
using System.Globalization;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class NullToVisibilityConverterTests
    {

        [Fact]
        public void ConvertsNullValues()
        {
            var converter = new NullToVisibilityConverter();
            Assert.Equal(
                converter.NullVisibility,
                converter.Convert(null, null, CultureInfo.CurrentCulture));
            Assert.Equal(
                converter.NotNullVisibility,
                converter.Convert(new object(), null, CultureInfo.CurrentCulture));
        }

    }

}
