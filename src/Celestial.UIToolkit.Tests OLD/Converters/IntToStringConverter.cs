using System;
using System.Globalization;
using System.Windows.Data;

namespace Celestial.UIToolkit.Tests.Converters
{

    internal class IntToStringConverter : IValueConverter
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
