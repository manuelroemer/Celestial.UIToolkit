using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A special type of shadow which aids in creating a unified shadow experience across the
    /// whole application by providing an elevation API to create layering and depth effects.
    /// 
    /// It can be attached to any type of controls via the <see cref="ShadowProperty"/>.
    /// For the shadow to be displayed, control template authors are required to display the
    /// <see cref="ThemeShadow"/> provided via the attached <see cref="ShadowProperty"/>.
    /// The easiest way to do this is by simply adding a <see cref="ThemeShadowPresenter"/> to the 
    /// control template.
    /// </summary>
    public partial class ThemeShadow : DependencyObject
    {

        /// <summary>
        /// Gets or sets a value with which the <see cref="ElevationProperty"/> 
        /// is multiplied, before the shadow's final length is calculated.
        /// </summary>
        public double ElevationShadowLengthMultiplier { get; set; } = 4d;
        // Not making the above property a DependencyProperty, because there is no way
        // to listen to change notifications and updating all elements, on which a shadow
        // is set. I don't expect this property to change anyway - it should be set once
        // when defining the shadow - and that's it.

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeShadow"/> class.
        /// </summary>
        public ThemeShadow()
        {
        }
        
        private static void ShadowProperty_Changed(
            DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            // Called whenever a property which defines the shadow changes.
            // e.g. the attached Shadow property itself, or an element's elevation.
            // In either case, update the calculated drop shadow values for the element.
            ThemeShadow themeShadow = GetShadow(element);
            if (themeShadow != null)
            {
                themeShadow.UpdateCurrentDropShadowValuesForElement(element);
            }
        }
        
        private void UpdateCurrentDropShadowValuesForElement(DependencyObject element)
        {
            if (element == null) return;
            
            Vector shadowOffset = CalculateCurrentShadowOffset(element);
            double blurRadius = CalculateCurrentBlurRadius(element);

            SetBlurRadius(element, blurRadius);
            SetOffsetX(element, shadowOffset.X);
            SetOffsetY(element, shadowOffset.Y);
        }

        /// <summary>
        /// Calculates a drop shadow's blur radius, based on the <see cref="ThemeShadow"/>'s 
        /// current <see cref="ElevationShadowLengthMultiplier"/> and the 
        /// <paramref name="element"/>'s current elevation.
        /// </summary>
        /// <param name="element">
        /// The element whose drop shadow values should be calculated.
        /// </param>
        /// <returns>
        /// The blur radius of a drop shadow.
        /// </returns>
        protected virtual double CalculateCurrentBlurRadius(DependencyObject element)
        {
            double elevation = GetElevation(element);
            return elevation * ElevationShadowLengthMultiplier;
        }

        /// <summary>
        /// Calculates a drop shadow's offset in relation to the shadow-casting element.
        /// An offset of (0;0) means that the shadow is directly under the shadow-casting element.
        /// </summary>
        /// <param name="element">
        /// The element whose drop shadow values should be calculated.
        /// </param>
        /// <returns>
        /// A <see cref="Vector"/> which represents the drop shadow's offset.
        /// By default, this returns (0;0).
        /// </returns>
        protected virtual Vector CalculateCurrentShadowOffset(DependencyObject element)
        {
            // The default ThemeShadow is ambient - it draws the shadow in neither direction.
            return new Vector(0d, 0d);
        }
        
    }
    
}
