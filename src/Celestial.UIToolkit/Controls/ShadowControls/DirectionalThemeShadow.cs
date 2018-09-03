using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A <see cref="ThemeShadow"/> which moves the shadow in a certain direction,
    /// defined by the <see cref="ShadowDirection"/> property.
    /// </summary>
    public class DirectionalThemeShadow : ThemeShadow
    {

        /// <summary>
        /// Gets or sets the direction in which the shadow is pointing.
        /// </summary>
        public Dock ShadowDirection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalThemeShadow"/> which points
        /// to the Bottom.
        /// </summary>
        public DirectionalThemeShadow()
        {
            ShadowDirection = Dock.Bottom;
        }

        /// <summary>
        /// Calculates the shadow's offset, based on the <see cref="ShadowDirection"/>.
        /// </summary>
        /// <param name="element">The shadow casting element.</param>
        /// <returns>
        /// A <see cref="Vector"/> which is the final location of the shadow.
        /// </returns>
        protected override Vector CalculateCurrentShadowOffset(DependencyObject element)
        {
            // Move the Shadow by the Current Elevation Level.
            double elevation = GetElevation(element);

            switch (ShadowDirection)
            {
                case Dock.Left:
                    return new Vector(-elevation, 0d);
                case Dock.Right:
                    return new Vector(elevation, 0d);
                case Dock.Top:
                    return new Vector(0d, elevation);
                case Dock.Bottom:
                    return new Vector(0d, -elevation);
                default:
                    return base.CalculateCurrentShadowOffset(element);
            }
        }

    }

}
