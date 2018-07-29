using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Theming
{

    [MarkupExtensionReturnType(typeof(double))]
    public class GridUnitExtension : MarkupExtension
    {

        private static readonly double _dipMultiplier;

        /// <summary>
        /// Gets or sets the value of a single grid unit.
        /// This value equals the width and height of a single cell
        /// in the fictional grid.
        /// </summary>
        public static double GridCellSize { get; set; } = 4d;

        /// <summary>
        /// Gets or sets the value with which the <see cref="GridCellSize"/>
        /// will be multiplied.
        /// </summary>
        /// <example>
        /// Assume the following convention:
        /// 1gu = 1 grid unit(s) -> Multiplier = 1
        /// 2gu = 2 grid unit(s) -> Multiplier = 2
        /// 
        /// Thus, for a <see cref="GridCellSize"/> of 4:
        /// 
        /// 1gu = 4 * 1 = 4
        /// 2gu = 4 * 2 = 8
        /// 3gu = 4 * 3 = 12
        /// ...
        /// </example>
        [ConstructorArgument("multiplier")]
        public double Multiplier { get; set; }

        /// <summary>
        /// Gets or sets a type to which the unit will be converted,
        /// if possible.
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// calculation of the final value should include a conversion
        /// to device independent pixel.
        /// </summary>
        public bool? DipAware { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridUnitExtension"/> class,
        /// with a default <see cref="Multiplier"/> of 1.
        /// </summary>
        public GridUnitExtension()
            : this(1d) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridUnitExtension"/> class
        /// with the specified <see cref="Multiplier"/>.
        /// </summary>
        /// <param name="multiplier">
        /// The value with which the <see cref="GridCellSize"/>
        /// will be multiplied.
        /// </param>
        public GridUnitExtension(double multiplier)
        {
            this.Multiplier = multiplier;
        }

        static GridUnitExtension()
        {
            // Abuse the LengthConverter to get the value of 1dip (device independent pixel).
            // This is the converter which is also used in XAML, so if we convert "1.0",
            // we will get the "real" size of one pixel on the current device.
            _dipMultiplier = (double)new LengthConverter().ConvertFromString("1.0");
        }

        /// <summary>
        /// Multiplies the <see cref="Multiplier"/> with the <see cref="GridCellSize"/>,
        /// converts it to device independent pixels and finally returns that result.
        /// </summary>
        /// <param name="serviceProvider">
        /// An <see cref="IServiceProvider"/> to be used.
        /// </param>
        /// <returns>
        /// The <see cref="double"/> which is the result of the grid unit multiplication.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider is IProvideValueTarget target)
            {
                Type targetType = this.TargetType ??
                                  (target.TargetObject is Setter setter ? setter.Property?.PropertyType : null) ??
                                  (target.TargetProperty as PropertyInfo)?.PropertyType ??
                                  (target.TargetProperty as DependencyProperty)?.PropertyType ??
                                  typeof(double);
                
                // We know the type of the target-property.
                // We can now convert the value of the multiplication to supported types.
                if (targetType == typeof(double))
                {
                    return this.CalculateResult(false);
                }
                else if (typeof(IConvertible).IsAssignableFrom(targetType))
                {
                    // IConvertible basically covers all primitive data-types.
                    return Convert.ChangeType(this.CalculateResult(true), targetType);
                }
                else if (targetType == typeof(Thickness))
                {
                    return new Thickness(this.CalculateResult(false));
                }
                else if (targetType == typeof(Size))
                {
                    double value = this.CalculateResult(false);
                    return new Size(value, value);
                }
                else if (targetType == typeof(Point))
                {
                    double value = this.CalculateResult(false);
                    return new Point(value, value);
                }
                else if (targetType == typeof(CornerRadius))
                {
                    double value = this.CalculateResult(false);
                    return new CornerRadius(value);
                }
            }

            // If we get here, we cannot assume anything about the target property.
            // Assume double in that case, as it will be the result of the multiplication.
            return this.CalculateResult(true);
        }

        /// <summary>
        /// Performs the grid unit calculation and returns the result.
        /// </summary>
        /// <param name="includeDipMultiplier">
        /// A value indicating whether the device's independent pixel modifier will
        /// be included in the calculation.
        /// Inside this method, this parameter can be ignored, if <see cref="DipAware"/>
        /// is set by the user.
        /// </param>
        /// <returns>The <see cref="double"/> result of the calculation.</returns>
        private double CalculateResult(bool includeDipMultiplier)
        {
            // The DipAware property serves as an overwrite for the parameter.
            // If not specified (null), the inclusion of the multiplier will be decided
            // depending on the target type.
            includeDipMultiplier = this.DipAware ?? includeDipMultiplier;
            return (includeDipMultiplier ? _dipMultiplier : 1d) * Multiplier * GridCellSize;
        }
        
    }

}
