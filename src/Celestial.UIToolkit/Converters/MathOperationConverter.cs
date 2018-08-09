using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A converter which performs the most basic mathematical operations
    /// between two values.
    /// </summary>
    /// <remarks>
    /// Note that this converter will produce wrong values, or might loose precision
    /// for very high numbers.
    /// </remarks>
    [ValueConversion(typeof(IConvertible), typeof(IConvertible))]
    public class MathOperationConverter : ValueConverter<IConvertible, IConvertible>, IMultiValueConverter
    {

        /// <summary>
        /// Gets or sets the operator for the operation.
        /// </summary>
        public MathOperator Operator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathOperationConverter"/> class.
        /// </summary>
        public MathOperationConverter() 
            : this(MathOperator.Add) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathOperationConverter"/> class
        /// with the specified operator.
        /// </summary>
        /// <param name="op">
        /// The operator for the operation.
        /// </param>
        public MathOperationConverter(MathOperator op)
        {
            this.Operator = op;
        }

        /// <summary>
        /// Performs the mathematical operation on the two values and returns the result.
        /// </summary>
        /// <param name="value">The left-hand value in the mathematical operation.</param>
        /// <param name="parameter">The right-hand value in the mathematical operation.</param>
        /// <param name="culture">The culture to use in the converter. Not used.</param>
        /// <returns>A <see cref="Visibility"/> object depending on the <paramref name="value"/>.</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown if <paramref name="parameter"/> is not of type <see cref="IConvertible"/>.
        /// </exception>
        public override IConvertible Convert(IConvertible value, object parameter, CultureInfo culture)
        {
            // Fail silently, if parameter is null.
            if (value == null || parameter == null) return value;

            // Don't throw for null values (because of bindings).
            // Require IConvertibles though.
            IConvertible paramConvertible = parameter as IConvertible;
            if (paramConvertible == null)
                throw new NotSupportedException(
                    $"The converter requires the provided parameter to be of type " +
                    $"{typeof(IConvertible).FullName}.");

            // Get the right hand side from the parameter.
            double l = System.Convert.ToDouble(value);
            double r = System.Convert.ToDouble(paramConvertible);
            double res = l;

            switch (this.Operator)
            {
                case MathOperator.Add:
                    res = l + r;
                    break;
                case MathOperator.Subtract:
                    res = l - r;
                    break;
                case MathOperator.Multiply:
                    res = l * r;
                    break;
                case MathOperator.Divide:
                    res = l / r;
                    break;
                default: throw new NotImplementedException("Unimplemented MathOperator.");
            }

            // Return the original type of the input.
            return (IConvertible)System.Convert.ChangeType(res, value.GetType());
        }

        /// <summary>
        /// Performs the mathematical operation on all of the provided values.
        /// In comparison to the single <see cref="IValueConverter"/>'s Convert method,
        /// this one doesn't use the <paramref name="parameter"/>,
        /// since all numbers are provided by the <paramref name="values"/> parameter.
        /// </summary>
        /// <param name="values">The values which are being added/subtracted/multiplied/divided.</param>
        /// <param name="targetType">The target type of the conversion.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The result of the mathematical operation.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException(
                    "When using multi conversion, the converter requires at least one value.");
            }
            this.Convert(values[0], targetType, null, null); // This does a type-check for IConvertible
                                                             // without actually changing anything.

            object currentValue = values[0];
            foreach (var value in values.Skip(1))
            {
                currentValue = this.Convert(currentValue, targetType, value, culture);
            }

            return currentValue;
        }

        /// <summary>
        /// Throws <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="value">Not supported.</param>
        /// <param name="targetTypes">Not supported.</param>
        /// <param name="parameter">Not supported.</param>
        /// <param name="culture">Not supported.</param>
        /// <returns>Not supported.</returns>
        /// <exception cref="NotSupportedException" />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // Will throw an exception.
            base.ConvertBack(value, targetTypes.FirstOrDefault(), parameter, culture);
            return null;
        }

        // This converter won't, by default, provide a ConvertBack method.
        // That's because we cannot guarantee a correct conversion in all cases.
        // For instance for the following values:
        //     10 / 3 = 3.333
        // but gets rounded to 3 (int).
        // When converting back, we'd get
        //      3 * 3 = 9
        // which would be wrong.
        // If necessary, a user will have to extend the class by himself
        // (and customize it to his needs).

    }

    /// <summary>
    /// Defines the supported mathematical operations which are used by the
    /// <see cref="MathOperationConverter"/>.
    /// </summary>
    public enum MathOperator
    {

        /// <summary>
        /// Two values are added to another.
        /// </summary>
        Add,

        /// <summary>
        /// Two values are subtracted from each other.
        /// </summary>
        Subtract,

        /// <summary>
        /// Two values are multiplied by each other.
        /// </summary>
        Multiply,

        /// <summary>
        /// Two values are divided by each other.
        /// </summary>
        Divide

    }

}
