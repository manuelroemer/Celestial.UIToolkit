using System;
using System.Windows;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="DependencyProperty"/> type.
    /// </summary>
    public static class DependencyPropertyExtensions
    {

        /// <summary>
        /// Checks if the dependency property's local value is set for the specified
        /// dependency object.
        /// </summary>
        /// <param name="dp">The <see cref="DependencyProperty"/>.</param>
        /// <param name="depObj">A <see cref="DependencyObject"/>.</param>
        /// <returns>
        /// <c>true</c>, if the dependency property is set for the specified dependency object;
        /// <c>false</c> if not.
        /// </returns>
        public static bool HasLocalValue(this DependencyProperty dp, DependencyObject depObj)
        {
            if (dp == null) throw new ArgumentNullException(nameof(dp));
            if (depObj == null) throw new ArgumentNullException(nameof(depObj));
            return depObj.ReadLocalValue(dp) != DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Checks if the dependency property's local value is set for the specified
        /// dependency object.
        /// </summary>
        /// <param name="depObj">A <see cref="DependencyObject"/>.</param>
        /// <param name="dp">The <see cref="DependencyProperty"/>.</param>
        /// <returns>
        /// <c>true</c>, if the dependency property is set for the specified dependency object;
        /// <c>false</c> if not.
        /// </returns>
        public static bool IsDependencyPropertySet(this DependencyObject depObj, DependencyProperty dp)
        {
            // This does exactly the same as DependencyProperty.HasLocalValue.
            // Depending on the code section, either version might sound better than the other one though.
            if (depObj == null) throw new ArgumentNullException(nameof(depObj));
            if (dp == null) throw new ArgumentNullException(nameof(dp));
            return dp.HasLocalValue(depObj);
        }

    }

}
