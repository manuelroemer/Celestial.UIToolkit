using Celestial.UIToolkit.Converters;
using System;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class GetTypeConverterTests
    {

        [Fact]
        public void ReturnsCorrectType()
        {
            var converter = new GetTypeConverter();
            object value = "A String.";
            Type result = converter.Convert(value, null, null);

            Assert.Equal(value.GetType(), result);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionForNullValue()
        {
            var converter = new GetTypeConverter();
            Assert.Throws<ArgumentNullException>(() => converter.Convert(null, null, null));
        }

    }

}
