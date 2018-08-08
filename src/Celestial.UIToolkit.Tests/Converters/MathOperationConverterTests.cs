using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Converters
{

    [TestClass]
    public class MathOperationConverterTests
    {

        [TestMethod]
        public void ConvertsValues()
        {
            var converter = new MathOperationConverter();

            double l = 10.5;
            double r = 5.5;

            converter.Operator = MathOperator.Add;
            Assert.AreEqual(l + r, converter.Convert(l, r, CultureInfo.CurrentCulture));

            converter.Operator = MathOperator.Subtract;
            Assert.AreEqual(l - r, converter.Convert(l, r, CultureInfo.CurrentCulture));

            converter.Operator = MathOperator.Multiply;
            Assert.AreEqual(l * r, converter.Convert(l, r, CultureInfo.CurrentCulture));

            converter.Operator = MathOperator.Divide;
            Assert.AreEqual(l / r, converter.Convert(l, r, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void InputTypeEqualsOutputType()
        {
            var converter = new MathOperationConverter();

            Assert.IsInstanceOfType(
                converter.Convert("10", 5.5, CultureInfo.CurrentCulture),
                typeof(string));
        }

        [TestMethod]
        public void AcceptsConvertibles()
        {
            var converter = new MathOperationConverter(MathOperator.Multiply);

            // I won't test every convertible here, only two very contrary types.
            var result = converter.Convert("10", (decimal)10.123, CultureInfo.CurrentCulture);
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void SilentFailForNullValues()
        {
            var converter = new MathOperationConverter();

            Assert.IsNull(converter.Convert(null, 1, CultureInfo.CurrentCulture));
            Assert.AreEqual(10, converter.Convert(10, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ThrowsForWrongInputTypes()
        {
            var converter = new MathOperationConverter();

            Assert.ThrowsException<NotSupportedException>(() =>
                converter.Convert(10, new object(), CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void SupportsMultiValueConversion()
        {
            IMultiValueConverter converter = new MathOperationConverter(MathOperator.Add);
            object[] values = { 1, 2, 3, 4, 5, 6 };
            int sum = values.Sum(val => (int)val);

            Assert.AreEqual(
                sum,
                converter.Convert(values, typeof(int), null, null));
        }

        [TestMethod]
        public void MultiValueConversionThrowsForEmptyValues()
        {
            IMultiValueConverter converter = new MathOperationConverter();
            Assert.ThrowsException<ArgumentException>(() =>
                converter.Convert(new object[] { }, typeof(object), null, null));
        }

        [TestMethod]
        public void MultiValueConversionThrowsForNonIConvertible()
        {
            IMultiValueConverter converter = new MathOperationConverter();
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.Convert(new object[] { new object() }, typeof(object), null, null));
        }

    }

}
