using Celestial.UIToolkit.Converters;
using System;
using System.Globalization;
using System.Windows.Data;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class InvertingConverterTests
    {

        [Fact]
        public void InvertsOtherConverter()
        {
            IValueConverter normalConverter = new IntToStringConverter();
            var invertingConverter = new InvertingConverter(normalConverter);

            const int convertValue = 123;
            const string convertBackValue = "123";

            Assert.Equal(
                normalConverter.Convert(convertValue, typeof(string), null, null),
                invertingConverter.ConvertBack(convertValue, typeof(string), null, null));
            Assert.Equal(
                normalConverter.ConvertBack(convertBackValue, typeof(string), null, null),
                invertingConverter.Convert(convertBackValue, typeof(string), null, null));
        }

        [Fact]
        public void ThrowsWithoutUnderlyingConverter()
        {
            var invertingConverter = new InvertingConverter();

            Assert.Throws<InvalidOperationException>(() => invertingConverter.Convert(
                new object(), typeof(object), null, CultureInfo.CurrentCulture));

            Assert.Throws<InvalidOperationException>(() => invertingConverter.ConvertBack(
                new object(), typeof(object), null, CultureInfo.CurrentCulture));
        }

        private class IntToStringConverter : IValueConverter
        {

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value.ToString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return int.Parse((String)value);
            }

        }

    }

}
