using System;
using System.Globalization;

namespace Celestial.UIToolkit.Converters
{

    public class MathOperationConverter : ValueConverter<IConvertible, IConvertible>
    {

        public MathOperator Operator { get; set; }

        public override IConvertible Convert(IConvertible value, object parameter, CultureInfo culture)
        {
            return this.DoMathConversion(value, parameter, this.Operator);
        }

        public override IConvertible ConvertBack(IConvertible value, object parameter, CultureInfo culture)
        {
            // For floating-point operations, we can simply swap out
            // the operator and do a normal conversion.
            // This conversion can fail in specific instances, e.g. for Divisions with integers.
            // TODO: Maybe produce an exception/a warning? Or disallow it entirely?
            MathOperator op = this.Operator;
            switch (this.Operator)
            {
                case MathOperator.Add:
                    op = MathOperator.Subtract;
                    break;
                case MathOperator.Subtract:
                    op = MathOperator.Add;
                    break;
                case MathOperator.Multiply:
                    op = MathOperator.Divide;
                    break;
                case MathOperator.Divide:
                    op = MathOperator.Multiply;
                    break;
                default: break;
            }

            return this.DoMathConversion(value, parameter, op);
        }

        private IConvertible DoMathConversion(IConvertible value, object parameter, MathOperator op)
        {
            // Fail silently, if parameter is not an IConvertible.
            // Allows using null values.
            IConvertible paramConvertible = parameter as IConvertible;
            if (paramConvertible == null) return value;

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
                default: break;
            }

            // Return the original type of the input.
            return (IConvertible)System.Convert.ChangeType(res, value.GetType());
        }

    }

    public enum MathOperator
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

}
