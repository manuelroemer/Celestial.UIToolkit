using System;
using System.Globalization;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{
    
    /// <summary>
    /// A strongly-typed base class for converters which implement the 
    /// <see cref="IValueConverter"/> interface.
    /// </summary>
    public abstract class ValueConverter<TValue, TTarget> : IValueConverter
    {

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown if the <see cref="Convert(TValue, object, CultureInfo)"/> method has not been overridden.
        /// </exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.EnforceType(value?.GetType(), typeof(TValue));
            this.EnforceType(typeof(TTarget), targetType);
            return this.Convert((TValue)value, parameter, culture);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown if the <see cref="ConvertBack(TTarget, object, CultureInfo)"/> method has not been overridden.
        /// </exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.EnforceType(value?.GetType(), typeof(TTarget));
            this.EnforceType(typeof(TValue, targetType));
            return this.ConvertBack((TTarget)value, parameter, culture);
        }

        /// <summary>
        /// Converts a value.
        /// If not overridden, this throws a <see cref="System.NotSupportedException"/>.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown if the <see cref="Convert(TValue, object, CultureInfo)"/> method has not been overridden.
        /// </exception>
        public virtual TTarget Convert(TValue value, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                "This converter cannot be used to convert a value.");
        }

        /// <summary>
        /// Converts a value.
        /// If not overridden, this throws a <see cref="System.NotSupportedException"/>.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown if the <see cref="ConvertBack(TTarget, object, CultureInfo)"/> method has not been overridden.
        /// </exception>
        public virtual TValue ConvertBack(TTarget value, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(
                   "This converter cannot be used to convert a value back.");
        }
        
        /// <summary>
        /// Ensures that <paramref name="t"/> is of type <paramref name="expected"/>.
        /// Throws an exception if not.
        /// </summary>
        /// <param name="t">The type.</param>
        /// <param name="expected">The expected type.</param>
        private void EnforceType(Type t, Type expected)
        {
            if (t == null || expected == null) return;
            if (t != expected && !t.IsSubclassOf(expected))
            {
                throw new NotSupportedException(
                    $"The converter expected the type {expected.FullName}, but got the type {t.FullName}. " +
                    $"Ensure that a valid type is passed to the converter.");
            }
        }

    }

}
