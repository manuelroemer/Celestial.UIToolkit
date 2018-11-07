using Celestial.UIToolkit.Converters;
using System;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class GroupConverterTests
    {

        [Fact]
        public void UsesMultipleConverters()
        {
            var converter = new GroupConverter();
            converter.Add(new MathOperationConverter(MathOperator.Add));
            converter.Add(new MathOperationConverter(MathOperator.Subtract));

            Assert.Equal(5, converter.Convert(5, typeof(int), 1, null));
        }

        [Fact]
        public void CannotConvertBack()
        {
            var converter = new GroupConverter();
            Assert.Throws<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
        }

    }

}
