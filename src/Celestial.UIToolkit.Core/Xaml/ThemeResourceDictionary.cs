using System.Collections.Generic;
using System.Windows;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    ///     A resource dictionary which provides built-in support for dynamic theme changes
    ///     which come from the <see cref="ThemeManager"/>.
    ///     Use the <see cref="ThemeDictionaries"/> property to specify nested, theme specific
    ///     dictionaries.
    /// </summary>
    /// <remarks>
    ///     This resource dictionary is directly modeled after the UWP ResourceDictionary class,
    ///     which provides a similar API.
    ///     See https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.ResourceDictionary#Windows_UI_Xaml_ResourceDictionary_ThemeDictionaries
    ///     for details.
    /// </remarks>
    public class ThemeResourceDictionary : SharedResourceDictionary
    {

        private ResourceDictionary _activeThemeDictionary;
        
        /// <summary>
        ///     Gets a collection of <see cref="ResourceDictionary"/> instances which dynamically
        ///     get loaded whenever the application's theme changes.
        ///     The key under which the dictionary is stored must match the theme's name to get
        ///     the dictionary loaded.
        /// </summary>
        public Dictionary<object, ResourceDictionary> ThemeDictionaries { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeResourceDictionary"/>.
        /// </summary>
        public ThemeResourceDictionary()
        {
            ThemeDictionaries = new Dictionary<object, ResourceDictionary>();
            ThemeManager.Current.ThemeChanged += ThemeManager_ThemeChanged;

            SwapActiveThemeDictionary(ThemeManager.Current.CurrentTheme);
        }
        
        private void ThemeManager_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            SwapActiveThemeDictionary(e.ThemeName);
        }

        private void SwapActiveThemeDictionary(string themeName)
        {
            RemoveActiveThemeDictionary();
            if (TryGetDictionaryForTheme(themeName, out var themeDictionary))
            {
                SetActiveThemeDictionary(themeDictionary);
            }
        }

        private bool TryGetDictionaryForTheme(string themeName, out ResourceDictionary dictionary)
        {
            if (themeName != null)
            {
                // We simply want to find the first dictionary whose key exactly matches the the 
                // theme name.
                var wasSuccessful = ThemeDictionaries.TryGetValue(themeName, out var result);
                dictionary = result;
                return wasSuccessful;
            }
            else
            {
                dictionary = null;
                return false;
            }
        }
        
        private void RemoveActiveThemeDictionary()
        {
            if (_activeThemeDictionary != null)
            {
                ResourcesSource.Verbose("Removing previously active theme dictionary.");
                Application.Current.Resources.MergedDictionaries.Remove(_activeThemeDictionary);
            }
            else
            {
                ResourcesSource.Verbose("Didn't find an active theme dictionary. Nothing to remove.");
            }
        }

        private void SetActiveThemeDictionary(ResourceDictionary dictionary)
        {
            if (dictionary != null)
            {
                ResourcesSource.Verbose("Adding active theme dictionary.");
                _activeThemeDictionary = dictionary;

                // I would love to not add this to the global app's resources, but
                // it breaks the resource lookup otherwise.
                // So far, this seems to be the only working solution.
                Application.Current.Resources.MergedDictionaries.Add(_activeThemeDictionary);
            }
            else
            {
                ResourcesSource.Verbose("Didn't find an active dictionary to load.");
            }
        }
        
    }
    
}
