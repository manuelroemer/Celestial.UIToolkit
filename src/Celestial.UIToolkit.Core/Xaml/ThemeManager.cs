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

        private static Lazy<ThemeManager> _current 
            = new Lazy<ThemeManager>(() => new ThemeManager(), true);

        private Application _application;

        /// <summary>
        /// Gets an instance of the <see cref="ThemeManager"/> for the current application.
        /// </summary>
        public static ThemeManager Current
        {
            get { return _current.Value; }
        }

        /// <summary>
        /// Gets the name of the theme that is currently active.
        /// This can be null.
        /// </summary>
        public string CurrentTheme { get; private set; }

        private ThemeManager()
            : this(Application.Current) { }

        private ThemeManager(Application application)
        {
            if (application is null)
                throw new ArgumentNullException(nameof(application));
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

            if (CurrentTheme != themeName)
            {
                ResourcesSource.TraceInformation("Changing application theme to {0}.");
                CurrentTheme = themeName;

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
