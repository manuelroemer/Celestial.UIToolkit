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
        
        public IDictionary<object, ResourceDictionary> ThemeDictionaries { get; }

        public ThemeResourceDictionary()
        {
            ThemeDictionaries = new Dictionary<object, ResourceDictionary>();
            ThemeManager.Current.ThemeChanged += ThemeManager_ThemeChanged;
        }
        
        private void ThemeManager_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            RemoveActiveThemeDictionary();

            if (TryGetDictionaryForTheme(e.ThemeName, out var themeDictionary))
            {
                SetActiveThemeDictionary(themeDictionary);
            }
        }

        private bool TryGetDictionaryForTheme(string themeName, out ResourceDictionary dictionary)
        {
            // We simply want to find the first dictionary whose key exactly matches the the 
            // theme name.
            foreach (var themeDictPair in ThemeDictionaries)
            {
                if (themeDictPair.Key is string key && themeName == key)
                {
                    dictionary = themeDictPair.Value;
                    return true;
                }
            }

            dictionary = null;
            return false;
        }

        private void RemoveActiveThemeDictionary()
        {
            if (_activeThemeDictionary != null)
            {
                ResourcesSource.Verbose("Removing previous active theme dictionary..");
                MergedDictionaries.Remove(_activeThemeDictionary);
            }
        }

        private void SetActiveThemeDictionary(ResourceDictionary dictionary)
        {
            if (dictionary != null)
            {
                ResourcesSource.Verbose("Adding active theme dictionary.");
                _activeThemeDictionary = dictionary;
                MergedDictionaries.Add(_activeThemeDictionary);
            }
        }
        
    }
    
}
