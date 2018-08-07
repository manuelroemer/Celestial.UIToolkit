using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// An implementation of the <see cref="IValueConverter"/> interface which
    /// bundles other <see cref="IValueConverter"/> instances and uses these
    /// in order to convert an object.
    /// </summary>
    public class GroupConverter : List<IValueConverter>, IValueConverter
    {

        /// <summary>
        /// Goes through the list of converters and uses these, in order,
        /// to convert the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var converter in this)
            {
                value = converter.Convert(value, targetType, parameter, culture);
            }
            return value;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="value">Not supported.</param>
        /// <param name="targetType">Not supported.</param>
        /// <param name="parameter">Not supported.</param>
        /// <param name="culture">Not supported.</param>
        /// <returns>Not supported.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                $"The {nameof(GroupConverter)} cannot convert values back, since there is no guarantee " +
                $"that each value is reversible after normal conversion.");
        }
    }

}
