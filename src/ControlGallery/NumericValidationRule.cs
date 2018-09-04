using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlGallery
{

    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse(value.ToString(), out double number))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Cannot convert \"" + value + "\" to a number.");
            }
        }
    }

}
