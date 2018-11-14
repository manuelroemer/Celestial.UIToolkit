using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    public class ThemeResourceDictionary : SharedResourceDictionary
    {

        private ResourceDictionary _activeThemeDictionary;
        
        public Dictionary<object, ResourceDictionary> ThemeDictionaries { get; }

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
                ResourcesSource.Verbose("Removing previous active theme dictionary..");
                Application.Current.Resources.MergedDictionaries.Remove(_activeThemeDictionary);
            }
        }

        private void SetActiveThemeDictionary(ResourceDictionary dictionary)
        {
            if (dictionary != null)
            {
                ResourcesSource.Verbose("Adding active theme dictionary.");
                _activeThemeDictionary = dictionary;
                
                Application.Current.Resources.MergedDictionaries.Add(_activeThemeDictionary);
            }
        }
        
    }
    
}
