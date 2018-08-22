using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// An implementation of the <see cref="IValueConverter"/> interface
    /// which expects a <see cref="Thickness"/>, sets certain edge
    /// properties to a desired value and then returns the result.
    /// </summary>
    [ValueConversion(typeof(Thickness), typeof(Thickness))]
    public class ThicknessSidePickerConverter : ValueConverter<Thickness, Thickness>
    {

        /// <summary>
        /// Gets or sets a value to which the <see cref="Thickness.Left"/>
        /// value will be set.
        /// If this is <c>null</c>, the original value is retained.
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Gets or sets a value to which the <see cref="Thickness.Top"/>
        /// value will be set.
        /// If this is <c>null</c>, the original value is retained.
        /// </summary>
        public double? Top { get; set; }

        /// <summary>
        /// Gets or sets a value to which the <see cref="Thickness.Right"/>
        /// value will be set.
        /// If this is <c>null</c>, the original value is retained.
        /// </summary>
        public double? Right { get; set; }

        /// <summary>
        /// Gets or sets a value to which the <see cref="Thickness.Bottom"/>
        /// value will be set.
        /// If this is <c>null</c>, the original value is retained.
        /// </summary>
        public double? Bottom { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThicknessSidePickerConverter"/>.
        /// </summary>
        public ThicknessSidePickerConverter() { }

        /// <summary>
        /// Changes the values of the input thickness to the values set in this
        /// converter's properties.
        /// </summary>
        /// <param name="value">The thickness whose values will be altered.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The altered <see cref="Thickness"/>.</returns>
        public override Thickness Convert(Thickness value, object parameter, CultureInfo culture)
        {
            return new Thickness(
                Left ?? value.Left,
                Top ?? value.Top,
                Right ?? value.Right,
                Bottom ?? value.Bottom);
        }

    }

}
