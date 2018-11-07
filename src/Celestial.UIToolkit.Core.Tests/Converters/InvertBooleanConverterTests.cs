using Celestial.UIToolkit.Converters;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class InvertBooleanConverterTests
    {

        [Fact]
        public void InvertsBoolean()
        {
            var converter = new InvertBooleanConverter();

            Assert.True(converter.Convert(false, null, null));
            Assert.False(converter.Convert(true, null, null));
        }

        [Fact]
        public void InvertsBooleanBack()
        {
            var converter = new InvertBooleanConverter();

            Assert.True(converter.ConvertBack(false, null, null));
            Assert.False(converter.ConvertBack(true, null, null));
        }

    }

}
