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
    public class NullVisibilityConverter : ValueConverter<object, Visibility>
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
        /// Initializes a new instance of the <see cref="NullVisibilityConverter"/> class.
        /// </summary>
        public NullVisibilityConverter() { }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A <see cref="Visibility"/> object depending on the <paramref name="value"/>.</returns>
        public override Visibility Convert(object value, object parameter, CultureInfo culture)
        {
            return value == null ? this.NullVisibility : this.NotNullVisibility;
        }
        
    }

    /// <summary>
    /// An extension of the <see cref="NullVisibilityConverter"/>,
    /// which also includes a null-string check in the conversion.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class StringVisibilityConverter : NullVisibilityConverter
    {

        /// <summary>
        /// Gets or sets a value indicating whether a white-space string
        /// should be treated as an empty string/null-value.
        /// </summary>
        public bool IncludeWhiteSpace { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringVisibilityConverter"/> class.
        /// </summary>
        public StringVisibilityConverter() { }

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
                if (this.IncludeWhiteSpace)
                {
                    if (string.IsNullOrWhiteSpace(s)) return this.NullVisibility;
                }
                else
                {
                    if (string.IsNullOrEmpty(s)) return this.NullVisibility;
                }
                return this.NotNullVisibility;
            }
            
            return base.Convert(value, parameter, culture);
        }

    }

}
