using System;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Xaml
{

    // Inspired by:
    // http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    // This is a modified version though.

    /// <summary>
    /// A custom markup extension which takes an enum's type and returns
    /// all of its members as a set of string values.
    /// These strings can, for instance, be used as a Binding source.
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {

        /// <summary>
        /// Gets or sets the type of the enum whose members are supposed to be
        /// converted to a set of strings.
        /// </summary>
        [ConstructorArgument("enumType")]
        public Type EnumType { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumBindingSourceExtension"/> class
        /// without an enum type.
        /// </summary>
        public EnumBindingSourceExtension()
            : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumBindingSourceExtension"/> with the
        /// specified <paramref name="enumType"/>.
        /// </summary>
        /// <param name="enumType">
        /// The type of the enum whose members are supposed to be converted to a set of strings.
        /// </param>
        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }

        /// <summary>
        /// Performs the conversion of the <see cref="EnumType"/> to a set of strings.
        /// </summary>
        /// <param name="serviceProvider">
        /// An <see cref="IServiceProvider"/> passed to the markup extension.
        /// </param>
        /// <returns>
        /// An enumerable set of strings, which represent the enum values.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null)
            {
                throw new InvalidOperationException(
                    $"The {nameof(EnumType)} property must have a value.");
            }

            Array enumValues = Enum.GetValues(EnumType);
            return enumValues;
        }

        /// <summary>
        /// Returns a string representation of the <see cref="EnumBindingSourceExtension"/>.
        /// </summary>
        /// <returns>A string representing the <see cref="EnumBindingSourceExtension"/>.</returns>
        public override string ToString()
        {
            return $"{nameof(EnumBindingSourceExtension)}: " +
                   $"{nameof(EnumType)}: {EnumType?.FullName}";
        }

    }
    
}
