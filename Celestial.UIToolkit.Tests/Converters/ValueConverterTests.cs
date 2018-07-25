using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Celestial.UIToolkit.Tests.Converters
{

    [TestClass]
    public class ValueConverterTests
    {

        [TestMethod]
        public void ThrowsForUnimplementedMethods()
        {
            var converter = new UnimplementedValueConverter();
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.Convert(0, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.ConvertBack(0, typeof(int), null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ThrowsForWrongInputs()
        {
            var converter = new ImplementedValueConverter();

            // Wrong Input type.
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.Convert("String", typeof(int), null, CultureInfo.CurrentCulture));
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.ConvertBack("String", typeof(int), null, CultureInfo.CurrentCulture));

            // Wrong target type.
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.Convert(0, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.ThrowsException<NotSupportedException>(() =>
                converter.ConvertBack(0, typeof(string), null, CultureInfo.CurrentCulture));
        }

        #region Test Class Implementations

        private class UnimplementedValueConverter : ValueConverter<int, int> { }

        private class ImplementedValueConverter : UnimplementedValueConverter
        {
            public override int Convert(int value, object parameter, CultureInfo culture) => value;
            public override int ConvertBack(int value, object parameter, CultureInfo culture) => value;
        }

        #endregion

    }

}
