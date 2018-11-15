namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Provides extension methods to the <see cref="ThemeManager"/> class.
    /// </summary>
    public static class ThemeManagerExtensions
    {

        /// <summary>
        /// Sets the current theme to one of the toolkit's valid themes.
        /// </summary>
        /// <param name="themeManager">The <see cref="ThemeManager"/>.</param>
        /// <param name="theme">The requested application theme.</param>
        public static void ChangeTheme(this ThemeManager themeManager, ApplicationTheme theme)
        {
            themeManager.ChangeTheme(theme.ToThemeName());
        }

    }

}
