using System;
using System.Globalization;
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

    }

}
