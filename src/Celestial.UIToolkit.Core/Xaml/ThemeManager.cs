using System;
using System.Windows;
using System.Windows.Threading;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Manages an application's active visual theme.
    /// Use the <see cref="Current"/> property to get an instance of the 
    /// <see cref="ThemeManager"/>.
    /// </summary>
    public sealed class ThemeManager : DispatcherObject
    {

        /// <summary>
        /// Occurs when the application's current theme gets changed.
        /// </summary>
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        private static Lazy<ThemeManager> _current = new Lazy<ThemeManager>(true);
        private Application _application;
        private string _currentTheme;

        /// <summary>
        /// Gets an instance of the <see cref="ThemeManager"/> for the current application.
        /// </summary>
        public static ThemeManager Current
        {
            get { return _current.Value; }
        }

        private ThemeManager()
            : this(Application.Current) { }

        private ThemeManager(Application application)
        {
            if (_application is null)
                throw new ArgumentNullException(nameof(_application));
            _application = application;
        }

        /// <summary>
        /// Changes the application's currently active theme.
        /// </summary>
        /// <param name="themeName">
        /// The name of the new theme to be applied.
        /// Pass null to indicate that no theme is supposed to be used.
        /// </param>
        public void ChangeTheme(string themeName)
        {
            VerifyAccess();

            if (_currentTheme != themeName)
            {
                ResourcesSource.TraceInformation("Changing application theme to {0}.");
                _currentTheme = themeName;

                var themeEventArgs = new ThemeChangedEventArgs(themeName);
                RaiseThemeChanged(themeEventArgs);
            }
        }

        private void RaiseThemeChanged(ThemeChangedEventArgs e)
        {
            ThemeChanged?.Invoke(this, e);
        }
        
    }

}
