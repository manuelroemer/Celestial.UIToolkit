using Celestial.UIToolkit.Converters;
using System;
using System.Globalization;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class ValueConverterTests
    {

        [Fact]
        public void ThrowsForUnimplementedMethods()
        {
            var converter = new UnimplementedValueConverter();
            Assert.Throws<NotSupportedException>(() =>
                converter.Convert(0, typeof(int), null, CultureInfo.CurrentCulture));
            Assert.Throws<NotSupportedException>(() =>
                converter.ConvertBack(0, typeof(int), null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ThrowsForWrongInputs()
        {
            var converter = new ImplementedValueConverter();

            // Wrong Input type.
            Assert.Throws<NotSupportedException>(() =>
                converter.Convert("String", typeof(int), null, CultureInfo.CurrentCulture));
            Assert.Throws<NotSupportedException>(() =>
                converter.ConvertBack("String", typeof(int), null, CultureInfo.CurrentCulture));

            // Wrong target type.
            Assert.Throws<NotSupportedException>(() =>
                converter.Convert(0, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Throws<NotSupportedException>(() =>
                converter.ConvertBack(0, typeof(string), null, CultureInfo.CurrentCulture));
        }
        
        private class UnimplementedValueConverter : ValueConverter<int, int> { }

        private class ImplementedValueConverter : UnimplementedValueConverter
        {
            public override int Convert(int value, object parameter, CultureInfo culture) => value;
            public override int ConvertBack(int value, object parameter, CultureInfo culture) => value;
        }
        
    }

}
