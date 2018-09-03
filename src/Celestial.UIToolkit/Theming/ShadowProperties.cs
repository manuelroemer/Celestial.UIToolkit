using System;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Identifies attached dependency properties which can change an element's
    /// shadow.
    /// </summary>
    public class ShadowProperties
    {

        /// <summary>
        /// Occurs when the <see cref="AreShadowsEnabled"/> property changes.
        /// </summary>
        public static event EventHandler AreShadowsEnabledChanged;

        private static bool _areShadowsEnabled = true;

        /// <summary>
        /// Gets or sets a value which can be used to enable or disable shadows on an application
        /// level.
        /// </summary>
        public static bool AreShadowsEnabled
        {
            get { return _areShadowsEnabled; }
            set
            {
                if (_areShadowsEnabled != value)
                {
                    _areShadowsEnabled = value;
                    AreShadowsEnabledChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

    }

}
