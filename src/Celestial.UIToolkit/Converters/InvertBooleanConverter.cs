using System;
using System.Globalization;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A value converter which inverts a given <see cref="Boolean"/> value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : ValueConverter<bool, bool>
    {

        /// <summary>
        /// Gets a default instance of the <see cref="InvertBooleanConverter"/> class.
        /// </summary>
        public static InvertBooleanConverter Default { get; } = new InvertBooleanConverter();

        /// <summary>
        /// Inverts and returns the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to be inverted.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The inverted <see cref="Boolean"/> value.</returns>
        public override bool Convert(bool value, object parameter, CultureInfo culture)
        {
            return !value;
        }

    }

}
