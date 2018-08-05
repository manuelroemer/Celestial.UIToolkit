using System;
using System.Globalization;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// An implementation of the <see cref="IValueConverter"/> interface which
    /// uses another <see cref="IValueConverter"/> specified via the <see cref="UnderlyingConverter"/>
    /// property to convert values, but in an inverted way.
    /// 
    /// In the <see cref="Convert(object, Type, object, CultureInfo)"/> method, the underlying converter's
    /// <see cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/> method is called.
    /// Likewise, in the <see cref="ConvertBack(object, Type, object, CultureInfo)"/> method, the underlying converter's
    /// <see cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/> is called.
    /// </summary>
    public class InvertingConverter : IValueConverter
    {

        /// <summary>
        /// Gets or sets the underlying <see cref="IValueConverter"/> which is being used
        /// by the <see cref="InvertingConverter"/>.
        /// </summary>
        public IValueConverter UnderlyingConverter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertingConverter"/> class
        /// without an underlying converter.
        /// The <see cref="UnderlyingConverter"/> property will have be set at a later
        /// point in time.
        /// Otherwise, an exception will be thrown during the conversion.
        /// </summary>
        public InvertingConverter()
            : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertingConverter"/> class,
        /// with the specified <paramref name="underlyingConverter"/>.
        /// </summary>
        /// <param name="underlyingConverter">
        /// The underlying <see cref="IValueConverter"/> which is being used by the
        /// <see cref="InvertingConverter"/>.
        /// </param>
        public InvertingConverter(IValueConverter underlyingConverter)
        {
            this.UnderlyingConverter = underlyingConverter;
        }

        /// <summary>
        /// Performs the value conversion by calling the <see cref="UnderlyingConverter"/>'s
        /// <see cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/> method
        /// with the provided parameters.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">A parameter to be used during conversion.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> to be used for the conversion.</param>
        /// <returns>The converted result.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.AssertUnderlyingConverterNotNull();
            return this.UnderlyingConverter.ConvertBack(value, targetType, parameter, culture);
        }

        /// <summary>
        /// Performs the backward value conversion by calling the <see cref="UnderlyingConverter"/>'s
        /// <see cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/> method
        /// with the provided parameters.
        /// </summary>
        /// <param name="value">The value to be converted back.</param>
        /// <param name="targetType">The target conversion type.</param>
        /// <param name="parameter">A parameter to be used during conversion.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> to be used for the conversion.</param>
        /// <returns>The converted result.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.AssertUnderlyingConverterNotNull();
            return this.UnderlyingConverter.Convert(value, targetType, parameter, culture);
        }

        private void AssertUnderlyingConverterNotNull()
        {
            if (this.UnderlyingConverter == null)
                throw new InvalidOperationException(
                    $"The {nameof(InvertingConverter)} cannot function without another converter. " +
                    $"Set this converter via the {nameof(UnderlyingConverter)} property.");
        }

    }

}
