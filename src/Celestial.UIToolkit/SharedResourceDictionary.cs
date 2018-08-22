using System;
using System.ComponentModel;
using System.Windows;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// An extension of the <see cref="ResourceDictionary"/> class which shadows
    /// the <see cref="ResourceDictionary.Source"/> property.
    /// The difference to a normal resource dictionary is that when this class'
    /// <see cref="Source"/> property is set, the dictionary from that source gets
    /// loaded via the <see cref="SharedResourceDictionaryManager"/>, which allows
    /// caching of the resource dictionaries.
    /// The loaded resource dictionary set by the <see cref="Source"/> property is then
    /// accessible via the <see cref="ResourceDictionary.MergedDictionaries"/> property.
    /// </summary>
    public class SharedResourceDictionary : ResourceDictionary
    {

        private static bool _isInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedResourceDictionary"/> class.
        /// </summary>
        public SharedResourceDictionary() { }

        private Uri _source;
        /// <summary>
        /// Gets or sets the dictionaries source <see cref="Uri"/>.
        /// When set, the <see cref="SharedResourceDictionary"/> tries to load
        /// the dictionary by using the <see cref="SharedResourceDictionaryManager.GetDictionary(Uri)"/>
        /// method and adds the result to the <see cref="ResourceDictionary.MergedDictionaries"/>
        /// collection.
        /// </summary>
        public new Uri Source
        {
            get { return _source; }
            set
            {
                if (_isInDesignMode)
                {
                    try
                    {
                        base.Source = value;
                        _source = base.Source;
                    } catch { } // Avoids wrong design-time error messages like "type not found"
                }
                else
                {
                    var baseUri = this.GetBaseSourceUri();
                    _source = value.IsAbsoluteUri ? value : new Uri(baseUri, value);
                    if (SharedResourceDictionaryManager.TryGetDictionary(_source, out var dict))
                    {
                        MergedDictionaries.Add(dict);
                    }
                    else
                    {
                        base.Source = value;
                        SharedResourceDictionaryManager.CacheDictionary(this);
                    }
                }
            }
        }

    }

}
