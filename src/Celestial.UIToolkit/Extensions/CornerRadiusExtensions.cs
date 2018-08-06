using System.Windows;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="CornerRadius"/> class.
    /// </summary>
    public static class CornerRadiusExtensions
    {

        /// <summary>
        /// Returns a value indicating whether the corner radius'
        /// properties all have the same values.
        /// </summary>
        /// <param name="cornerRadius">The <see cref="CornerRadius"/>.</param>
        /// <returns>
        /// <c>true</c> if all 4 properties have the same values; 
        /// <c>false</c> if not.
        /// </returns>
        public static bool HasUnifiedValues(this CornerRadius cornerRadius)
        {
            return cornerRadius.TopLeft == cornerRadius.TopRight &&
                   cornerRadius.TopLeft == cornerRadius.BottomLeft &&
                   cornerRadius.TopLeft == cornerRadius.BottomRight;
        }

    }

}
