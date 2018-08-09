using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// An implementation of the <see cref="IValueConverter"/> interface
    /// which negates the input variable, if possible.
    /// This works on numbers and a few WPF types like <see cref="Thickness"/>.
    /// </summary>
    /// <remarks>
    /// Note that this converter will produce wrong values, or might loose precision
    /// for very high numbers.
    /// </remarks>
    [ValueConversion(typeof(object), typeof(object))]
    public class NegateValueConverter : IValueConverter
    {

        /// <summary>
        /// Gets a default instance of the <see cref="NegateValueConverter"/>.
        /// </summary>
        public static NegateValueConverter Default { get; } = new NegateValueConverter();

        /// <summary>
        /// Negates the specified value, if the type is supported.
        /// If not, a <see cref="NotSupportedException"/> is thrown.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The negated value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type valueType = value.GetType();
            
            if (typeof(IConvertible).IsAssignableFrom(valueType))
            {
                return this.NegateConvertible((IConvertible)value);
            }
            else if (valueType == typeof(Thickness))
            {
                return this.NegateThickness((Thickness)value);
            }
            else if (valueType == typeof(CornerRadius))
            {
                return this.NegateCornerRadius((CornerRadius)value);
            }
            else if (valueType == typeof(Point))
            {
                return this.NegatePoint((Point)value);
            }
            else
            {
                throw new NotSupportedException(
                    $"The type '{valueType.FullName}' is not supported by this converter.");
            }
        }

        /// <summary>
        /// Negates the specified value, if the type is supported.
        /// If not, a <see cref="NotSupportedException"/> is thrown.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The negated value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }

        private IConvertible NegateConvertible(IConvertible convertible)
        {
            double d = System.Convert.ToDouble(convertible);
            double result = d * -1;
            return (IConvertible)System.Convert.ChangeType(result, convertible.GetType());
        }

        private Thickness NegateThickness(Thickness thickness)
        {
            return new Thickness(
                thickness.Left * -1,
                thickness.Top * -1,
                thickness.Right * -1,
                thickness.Bottom * -1);
        }

        private CornerRadius NegateCornerRadius(CornerRadius cornerRadius)
        {
            return new CornerRadius(
                cornerRadius.TopLeft * -1,
                cornerRadius.TopRight * -1,
                cornerRadius.BottomRight * -1,
                cornerRadius.BottomLeft * -1);
        }

        private Point NegatePoint(Point point)
        {
            return new Point(
                point.X * -1,
                point.Y * -1);
        }
        
    }

}
