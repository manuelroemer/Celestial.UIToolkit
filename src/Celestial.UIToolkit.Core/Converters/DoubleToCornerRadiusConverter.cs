using System.Globalization;
using System.Windows;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A converter which converts a double value to a <see cref="CornerRadius"/>
    /// instance with uniform values.
    /// </summary>
    public class DoubleToCornerRadiusConverter : ValueConverter<double, CornerRadius>
    {

        /// <summary>
        /// Gets a default instance of the <see cref="DoubleToCornerRadiusConverter"/> class.
        /// </summary>
        public static DoubleToCornerRadiusConverter Default { get; } = new DoubleToCornerRadiusConverter();

        /// <summary>
        /// Converts a double to a <see cref="CornerRadius"/> with uniform values.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The corner radius instance.</returns>
        public override CornerRadius Convert(double value, object parameter, CultureInfo culture)
        {
            return new CornerRadius(value);
        }
        
    }

}
