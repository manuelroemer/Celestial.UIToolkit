using System;
using System.Globalization;
using System.Windows.Data;
using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Converters
{
    [TestClass]
    public class InvertingConverterTests
    {

        [TestMethod]
        public void InvertsOtherConverter()
        {
            IValueConverter normalConverter = new IntToStringConverter();
            InvertingConverter invertingConverter = new InvertingConverter(normalConverter);

            const int convertValue = 123;
            const string convertBackValue = "123";

            Assert.AreEqual(
                normalConverter.Convert(convertValue, typeof(string), null, null),
                invertingConverter.ConvertBack(convertValue, typeof(string), null, null));
            Assert.AreEqual(
                normalConverter.ConvertBack(convertBackValue, typeof(string), null, null),
                invertingConverter.Convert(convertBackValue, typeof(string), null, null));
        }

        [TestMethod]
        public void ThrowsWithoutUnderlyingConverter()
        {
            var invertingConverter = new InvertingConverter();

            Assert.ThrowsException<InvalidOperationException>(() => invertingConverter.Convert(
                new object(), typeof(object), null, CultureInfo.CurrentCulture));

            Assert.ThrowsException<InvalidOperationException>(() => invertingConverter.ConvertBack(
                new object(), typeof(object), null, CultureInfo.CurrentCulture));
        }
        
    }
}
