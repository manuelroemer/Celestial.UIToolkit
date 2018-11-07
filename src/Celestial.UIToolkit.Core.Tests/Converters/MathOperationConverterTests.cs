using Celestial.UIToolkit.Converters;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class MathOperationConverterTests
    {

        [Fact]
        public void ConvertsValues()
        {
            var converter = new MathOperationConverter();

            double l = 10.5;
            double r = 5.5;

            converter.Operator = MathOperator.Add;
            Assert.Equal(l + r, converter.Convert(l, r, CultureInfo.CurrentCulture));

            converter.Operator = MathOperator.Subtract;
            Assert.Equal(l - r, converter.Convert(l, r, CultureInfo.CurrentCulture));

            converter.Operator = MathOperator.Multiply;
            Assert.Equal(l * r, converter.Convert(l, r, CultureInfo.CurrentCulture));

            converter.Operator = MathOperator.Divide;
            Assert.Equal(l / r, converter.Convert(l, r, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void InputTypeEqualsOutputType()
        {
            var converter = new MathOperationConverter();

            Assert.IsType<string>(converter.Convert("10", 5.5, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void AcceptsConvertibles()
        {
            var converter = new MathOperationConverter(MathOperator.Multiply);

            // I won't test every convertible here, only two very contrary types.
            var result = converter.Convert("10", (decimal)10.123, CultureInfo.CurrentCulture);
            Assert.IsType<string>(result);
        }

        [Fact]
        public void SilentFailForNullValues()
        {
            var converter = new MathOperationConverter();

            Assert.Null(converter.Convert(null, 1, CultureInfo.CurrentCulture));
            Assert.Equal(10, converter.Convert(10, null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ThrowsForWrongInputTypes()
        {
            var converter = new MathOperationConverter();

            Assert.Throws<NotSupportedException>(() =>
                converter.Convert(10, new object(), CultureInfo.CurrentCulture));
        }

        [Fact]
        public void SupportsMultiValueConversion()
        {
            IMultiValueConverter converter = new MathOperationConverter(MathOperator.Add);
            object[] values = { 1, 2, 3, 4, 5, 6 };
            int sum = values.Sum(val => (int)val);

            Assert.Equal(
                sum,
                converter.Convert(values, typeof(int), null, null));
        }

        [Fact]
        public void MultiValueConversionThrowsForEmptyValues()
        {
            IMultiValueConverter converter = new MathOperationConverter();
            Assert.Throws<ArgumentException>(() =>
                converter.Convert(new object[] { }, typeof(object), null, null));
        }

        [Fact]
        public void MultiValueConversionThrowsForNonIConvertible()
        {
            IMultiValueConverter converter = new MathOperationConverter();
            Assert.Throws<NotSupportedException>(() =>
                converter.Convert(new object[] { new object() }, typeof(object), null, null));
        }

    }

}
