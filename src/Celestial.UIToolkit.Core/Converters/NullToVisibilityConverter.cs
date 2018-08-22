using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A value converter which converts any object to a <see cref="Visibility"/> value,
    /// depending on whether the object is <c>null</c> or not.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class NullToVisibilityConverter : ValueConverter<object, Visibility>
    {

        /// <summary>
        /// Gets or sets the <see cref="Visibility"/> which is returned by the converter
        /// if the object is <c>null</c>.
        /// </summary>
        public Visibility NullVisibility { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Gets or sets the <see cref="Visibility"/> which is returned by the converter
        /// if the object is not <c>null</c>.
        /// </summary>
        public Visibility NotNullVisibility { get; set; } = Visibility.Visible;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullToVisibilityConverter"/> class.
        /// </summary>
        public NullToVisibilityConverter() { }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A <see cref="Visibility"/> object depending on the <paramref name="value"/>.</returns>
        public override Visibility Convert(object value, object parameter, CultureInfo culture)
        {
            return value == null ? NullVisibility : NotNullVisibility;
        }
        
    }

    /// <summary>
    /// An extension of the <see cref="NullToVisibilityConverter"/>,
    /// which also includes a null-string check in the conversion.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class StringToVisibilityConverter : NullToVisibilityConverter
    {

        /// <summary>
        /// Gets or sets a value indicating whether a white-space string
        /// should be treated as an empty string/null-value.
        /// </summary>
        public bool IncludeWhiteSpace { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToVisibilityConverter"/> class.
        /// </summary>
        public StringToVisibilityConverter() { }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A <see cref="Visibility"/> object depending on the <paramref name="value"/>.</returns>
        public override Visibility Convert(object value, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                if (IncludeWhiteSpace)
                {
                    if (string.IsNullOrWhiteSpace(s)) return NullVisibility;
                }
                else
                {
                    if (string.IsNullOrEmpty(s)) return NullVisibility;
                }
                return NotNullVisibility;
            }
            
            return base.Convert(value, parameter, culture);
        }

    }

}
