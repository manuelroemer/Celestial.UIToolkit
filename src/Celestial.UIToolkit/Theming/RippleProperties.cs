using System.Windows;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides attached dependency properties for controls which display
    /// a ripple animation.
    /// </summary>
    public static class RippleProperties
    {
        
        /// <summary>
        /// Identifies the ShowRippleAnimation attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowRippleAnimationProperty = DependencyProperty.RegisterAttached(
            "ShowRippleAnimation", typeof(bool), typeof(RippleProperties), new PropertyMetadata(true));

        /// <summary>
        /// Gets the value of the <see cref="ShowRippleAnimationProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShowRippleAnimationProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShowRippleAnimationProperty"/> attached dependency property,
        /// which is of type <see cref="bool"/>.
        /// </returns>
        public static bool GetShowRippleAnimation(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowRippleAnimationProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ShowRippleAnimationProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShowRippleAnimationProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShowRippleAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowRippleAnimationProperty, value);
        }
        
    }

}
