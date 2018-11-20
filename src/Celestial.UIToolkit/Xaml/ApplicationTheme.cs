namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Defines the application themes which are supported by the toolkit out of the box.
    /// </summary>
    public enum ApplicationTheme
    {

        /// <summary>
        /// The application uses the Light theme.
        /// </summary>
        Light,

        /// <summary>
        /// The application uses the Dark theme.
        /// </summary>
        Dark

    }
    
    internal static class ApplicationThemeExtensions
    {

        public static string ToThemeName(this ApplicationTheme theme)
        {
            switch (theme)
            {
                // This could probably all be done via Enum.ToString(),
                // but if, for some reason, it gets refactored, I want to ensure backwards
                // compatibility.
                case ApplicationTheme.Light:
                    return "Light";
                case ApplicationTheme.Dark:
                    return "Dark";
                default:
                    return theme.ToString();
            }
        }

    }

}
