using System.Globalization;
using System.Windows.Media;

namespace Celestial.UIToolkit.Converters
{

    /// <summary>
    /// A converter which converts <see cref="Color"/> objects to <see cref="SolidColorBrush"/> objects,
    /// and vice versa.
    /// </summary>
    public class ColorToSolidColorBrushConverter : ValueConverter<Color, SolidColorBrush>
    {

        /// <summary>
        /// Gets a default instance of the <see cref="ColorToSolidColorBrushConverter"/>.
        /// </summary>
        public static ColorToSolidColorBrushConverter Default { get; } = new ColorToSolidColorBrushConverter();

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorToSolidColorBrushConverter"/> class.
        /// </summary>
        public ColorToSolidColorBrushConverter() { }

        /// <summary>
        /// Returns a new <see cref="SolidColorBrush"/> instance, whose <see cref="SolidColorBrush.Color"/> property
        /// is set to the specified <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to be converted.</param>
        /// <param name="parameter">A parameter. Not used.</param>
        /// <param name="culture">A culture info. Not used.</param>
        /// <returns>
        /// The newly created <see cref="SolidColorBrush"/> instance.
        /// </returns>
        public override SolidColorBrush Convert(Color color, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(color);
        }

        /// <summary>
        /// Returns the <see cref="SolidColorBrush.Color"/> property of the specified <paramref name="brush"/>.
        /// </summary>
        /// <param name="brush">The <see cref="SolidColorBrush"/>.</param>
        /// <param name="parameter">A parameter. Not used.</param>
        /// <param name="culture">A culture info. Not used.</param>
        /// <returns>
        /// A <see cref="Color"/> instance, provided by the specified <paramref name="brush"/>
        /// or <see cref="Colors.Transparent"/>, if <paramref name="brush"/> is <c>null</c>.
        /// </returns>
        public override Color ConvertBack(SolidColorBrush brush, object parameter, CultureInfo culture)
        {
            return brush?.Color ?? Colors.Transparent;
        }

    }

}
