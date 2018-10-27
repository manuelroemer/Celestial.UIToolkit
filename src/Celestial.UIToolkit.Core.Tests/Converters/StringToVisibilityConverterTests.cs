using Celestial.UIToolkit.Converters;
using System.Globalization;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class StringToVisibilityConverterTests
    {

        [Fact]
        public void ConvertsNullValues()
        {
            var converter = new StringToVisibilityConverter();
            Assert.Equal(
                converter.NullVisibility,
                converter.Convert(null, null, CultureInfo.CurrentCulture));
            Assert.Equal(
                converter.NotNullVisibility,
                converter.Convert(new object(), null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertsStringValues()
        {
            var converter = new StringToVisibilityConverter();
            converter.IncludeWhiteSpace = false;

            Assert.Equal(
                converter.NullVisibility,
                converter.Convert("", null, CultureInfo.CurrentCulture));
            Assert.Equal(
                converter.NotNullVisibility,
                converter.Convert("Not empty", null, CultureInfo.CurrentCulture));

            converter.IncludeWhiteSpace = true;
            Assert.Equal(
                converter.NullVisibility,
                converter.Convert("   \t", null, CultureInfo.CurrentCulture));
        }

    }

}
