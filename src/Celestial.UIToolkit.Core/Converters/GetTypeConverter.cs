using System;
using System.Globalization;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A value converter which returns the <see cref="Type"/> of an input object.
    /// </summary>
    public class GetTypeConverter : ValueConverter<object, Type>
    {

        /// <summary>
        /// Gets a default instance of the <see cref="GetTypeConverter"/>.
        /// </summary>
        public static GetTypeConverter Default { get; } = new GetTypeConverter();

        /// <summary>
        /// Returns the type of the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The object whose type should be returned.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The type of the <paramref name="obj"/>.</returns>
        /// <exception cref="ArgumentNullException" />
        public override Type Convert(object obj, object parameter, CultureInfo culture)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj)); 
            return obj.GetType();
        }

    }

}
