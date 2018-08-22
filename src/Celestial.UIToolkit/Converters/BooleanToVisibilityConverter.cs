using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A value converter which converts a <see cref="Boolean"/> to a <see cref="Visibility"/> value,
    /// depending on whether the object is <c>true</c> or <c>false</c>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : ValueConverter<bool, Visibility>
    {

        /// <summary>
        /// Gets a default instance of the <see cref="BooleanToVisibilityConverter"/> class.
        /// </summary>
        public static BooleanToVisibilityConverter Default { get; } = new BooleanToVisibilityConverter();

        /// <summary>
        /// Gets a default instance of the <see cref="BooleanToVisibilityConverter"/> class
        /// whose <see cref="TrueVisibility"/> and <see cref="FalseVisibility"/> are swapped
        /// (in comparison to the <see cref="Default"/> instance).
        /// </summary>
        public static BooleanToVisibilityConverter Inverted { get; } = new BooleanToVisibilityConverter();

        /// <summary>
        /// Gets or sets a <see cref="Visibility"/> value which is mapped
        /// to or from a <see cref="Boolean"/> value which is <c>true</c>.
        /// </summary>
        public Visibility TrueVisibility { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="Visibility"/> value which is mapped
        /// to or from a <see cref="Boolean"/> value which is <c>false</c>.
        /// </summary>
        public Visibility FalseVisibility { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class,
        /// with the <see cref="TrueVisibility"/> set to <see cref="Visibility.Visible"/> and
        /// the <see cref="FalseVisibility"/> set to <see cref="Visibility.Collapsed"/>.
        /// </summary>
        public BooleanToVisibilityConverter()
            : this(Visibility.Visible, Visibility.Collapsed) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter"/> class
        /// with the specified values
        /// </summary>
        /// <param name="trueVisibility">
        /// A <see cref="Visibility"/> value which is mapped to or from a <see cref="Boolean"/> value 
        /// which is <c>true</c>.
        /// </param>
        /// <param name="falseVisibility">
        /// A <see cref="Visibility"/> value which is mapped to or from a <see cref="Boolean"/> value 
        /// which is <c>false</c>.
        /// </param>
        public BooleanToVisibilityConverter(Visibility trueVisibility, Visibility falseVisibility)
        {
            TrueVisibility = trueVisibility;
            FalseVisibility = falseVisibility;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use. Not used.</param>
        /// <param name="culture">The culture to use in the converter. Not used.</param>
        /// <returns>A <see cref="Visibility"/> object depending on the <paramref name="value"/>.</returns>
        public override Visibility Convert(bool value, object parameter, CultureInfo culture)
        {
            return value ? TrueVisibility : FalseVisibility;
        }

        /// <summary>
        /// Converts a value back.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use. Not used.</param>
        /// <param name="culture">The culture to use in the converter. Not used.</param>
        /// <returns>A <see cref="Visibility"/> object depending on the <paramref name="value"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="TrueVisibility"/> holds the same value as the <see cref="FalseVisibility"/>
        /// or if <paramref name="value"/> matches neither.
        /// </exception>
        public override bool ConvertBack(Visibility value, object parameter, CultureInfo culture)
        {
            if (TrueVisibility == FalseVisibility)
            {
                throw new InvalidOperationException(
                    $"Cannot perform a backwards value conversion, because the converter's " +
                    $"{nameof(TrueVisibility)} and {nameof(FalseVisibility)} properties have the same values. " +
                    $"Thus, the converter is unable to decide on a correct boolean value. " +
                    $"To fix this, make sure that the two properties have different values.");
            }

            if (value == TrueVisibility)
                return true;
            else if (value == FalseVisibility)
                return false;
            else
                throw new InvalidOperationException(
                    $"Couldn't convert \"{value}\", because it neither matches the converter's " +
                    $"{nameof(TrueVisibility)} property, nor the converter's {nameof(FalseVisibility)} value.");
        }
        
    }

}
