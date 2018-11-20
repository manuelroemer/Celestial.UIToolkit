using System;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Provides event data for the <see cref="ThemeManager.ThemeChanged"/> changed event.
    /// </summary>
    public class ThemeChangedEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the name of the new theme.
        /// This can be null.
        /// </summary>
        public string ThemeName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeChangedEventArgs"/> class.
        /// </summary>
        /// <param name="themeName">
        /// The name of the new theme. This can be null.
        /// </param>
        public ThemeChangedEventArgs(string themeName)
        {
            ThemeName = themeName;
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string representing this instance.</returns>
        public override string ToString()
        {
            return $"{nameof(ThemeChangedEventArgs)}: " +
                   $"{nameof(ThemeName)}: {ThemeName}";
        }

    }

}
