using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

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
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedResourceDictionary"/> class.
        /// </summary>
        public SharedResourceDictionary() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedResourceDictionary"/> class
        /// with the specified <paramref name="source"/>.
        /// </summary>
        /// <param name="source">
        /// The dictionary's source <see cref="Uri"/>.
        /// </param>
        public SharedResourceDictionary(Uri source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        private Uri _source;
        /// <summary>
        /// Gets or sets the dictionary's source <see cref="Uri"/>.
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
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (DesignMode.IsEnabled)
                {
                    LoadDesignMode(value);
                }
                else
                {
                    LoadDefault(value);
                }
            }
        }

        private void LoadDesignMode(Uri value)
        {
            // Always load a dictionary normally if in design-mode.
            try
            {
                base.Source = value;
                _source = base.Source;
            }
            catch { } // Avoids wrong design-time error messages like "type not found"
        }

        private void LoadDefault(Uri value)
        {
            SetFinalSourceUri(value);

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

        private void SetFinalSourceUri(Uri value)
        {
            // Depending on whether the new source is a relative URI or not, we might need
            // to construct the final absolute URI based on the dictionary's own base URI.
            if (value.IsAbsoluteUri)
            {
                _source = value;
            }
            else
            {
                // The source URI is relative. This requires this dict. to have a valid base URI.
                var baseUri = this.GetBaseSourceUri();
                if (baseUri == null)
                {
                    throw new InvalidOperationException(
                        $"Cannot load a relative source URI if the " +
                        $"{nameof(SharedResourceDictionary)}'s base URI is null.");
                }
                _source = new Uri(baseUri, value);
            }
        }
    }

    /// <summary>
    /// Manages cached <see cref="ResourceDictionary"/> instances.
    /// If this class is used to retrieve a <see cref="ResourceDictionary"/>,
    /// it allows a single resource dictionary to be referenced multiple times,
    /// instead of being reloaded each time it is included in another dictionary.
    /// </summary>
    internal static class SharedResourceDictionaryManager
    {

        private static readonly object _lock = new object();
        private static IList<WeakReference<ResourceDictionary>> _dictionaries =
            new List<WeakReference<ResourceDictionary>>();

        /// <summary>
        ///     Returns an <see cref="ResourceDictionary"/> instance which is either already cached,
        ///     or directly loaded and then cached by the manager.
        ///     In essence, this method combines the 
        ///     <see cref="TryGetDictionary(string, out ResourceDictionary)"/>
        ///     and <see cref="CacheDictionary(ResourceDictionary)"/> methods.
        /// </summary>
        /// <param name="sourceUriString">
        ///     The URI string of a <see cref="ResourceDictionary"/> to be retrieved.
        /// </param>
        /// <returns>
        ///     A <see cref="ResourceDictionary"/> instance which was either loaded, or cached.
        /// </returns>
        public static ResourceDictionary GetDictionary(string sourceUriString)
        {
            return GetDictionary(new Uri(sourceUriString, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        ///     Returns <see cref="ResourceDictionary"/> instance which is either already cached,
        ///     or directly loaded and then cached by the manager.
        ///     In essence, this method combines the 
        ///     <see cref="TryGetDictionary(Uri, out ResourceDictionary)"/>
        ///     and <see cref="CacheDictionary(ResourceDictionary)"/> methods.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="Uri"/> of a <see cref="ResourceDictionary"/> to be retrieved.
        /// </param>
        /// <returns>
        ///     A <see cref="ResourceDictionary"/> instance which was either loaded, or cached.
        /// </returns>
        public static ResourceDictionary GetDictionary(Uri source)
        {
            // Either return a cached dictionary or create a new one (and cache it).
            if (TryGetDictionary(source, out ResourceDictionary dictionary))
            {
                return dictionary;
            }
            else
            {
                // Load the dictionary and then cache it.
                var loadedDict = new ResourceDictionary() { Source = source };
                CacheDictionary(loadedDict);
                return loadedDict;
            }
        }

        /// <summary>
        ///     Tries to find a cached <see cref="ResourceDictionary"/>, based on the specified
        ///     <paramref name="sourceUriString"/>.
        ///     In comparison to the <see cref="GetDictionary(string)"/> method, this one doesn't
        ///     try to load a new dictionary from the <paramref name="sourceUriString"/> if it 
        ///     fails to find a cached one.
        /// </summary>
        /// <param name="sourceUriString">
        ///     The URI string of a <see cref="ResourceDictionary"/> to be retrieved.
        /// </param>
        /// <param name="resourceDictionary">
        ///     A parameter which will hold a found <see cref="ResourceDictionary"/> instance,
        ///     if one was cached.
        /// </param>
        /// <returns>
        ///     true if a cached <see cref="ResourceDictionary"/> instance was found;
        ///     false if not.
        /// </returns>
        public static bool TryGetDictionary(string sourceUriString, out ResourceDictionary resourceDictionary)
        {
            return TryGetDictionary(
                new Uri(sourceUriString, UriKind.RelativeOrAbsolute),
                out resourceDictionary);
        }

        /// <summary>
        ///     Tries to find a cached <see cref="ResourceDictionary"/>, based on the specified
        ///     <paramref name="source"/>.
        ///     In comparison to the <see cref="GetDictionary(Uri)"/> method, this one doesn't
        ///     try to load a new dictionary from the <paramref name="source"/> if it fails to find
        ///     a cached one.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="Uri"/> of a potentially cached dictionary.
        /// </param>
        /// <param name="resourceDictionary">
        ///     A parameter which will hold a found <see cref="ResourceDictionary"/> instance,
        ///     if one was cached.
        /// </param>
        /// <returns>
        ///     true if a cached <see cref="ResourceDictionary"/> instance was found;
        ///     false if not.
        /// </returns>
        public static bool TryGetDictionary(Uri source, out ResourceDictionary resourceDictionary)
        {
            lock (_lock)
            {
                for (int i = _dictionaries.Count - 1; i >= 0; i--)
                {
                    var dictRef = _dictionaries[i];
                    if (dictRef.TryGetTarget(out ResourceDictionary dict))
                    {
                        // A dict's source can be a relative URI, e.g. /Dict.xaml
                        // source might be absolute, e.g. pack://***/Dict.xaml
                        // In this case, don't only compare the Source, but also the whole Uri.
                        bool canGetAbsoluteUri = dict.GetBaseSourceUri() != null;
                        if (dict.Source == source ||
                            (canGetAbsoluteUri && dict.GetAbsoluteSourceUri() == source))
                        {
                            resourceDictionary = dict;
                            return true;
                        }
                    }
                    else
                    {
                        // Remove garbage-collected references, so that the collection stays small.
                        _dictionaries.RemoveAt(i);
                    }
                }

                resourceDictionary = null;
                return false;
            }
        }

        /// <summary>
        ///     Adds the specified <paramref name="dictionary"/> to the cache.
        /// </summary>
        /// <param name="dictionary">
        ///     The <see cref="ResourceDictionary"/> instance to be cached.
        /// </param>
        public static void CacheDictionary(ResourceDictionary dictionary)
        {
            if (dictionary != null)
            {
                if (!ContainsDictionary(dictionary.Source))
                {
                    lock (_lock)
                    {
                        _dictionaries.Add(new WeakReference<ResourceDictionary>(dictionary));
                    }
                }
            }
        }

        /// <summary>
        ///     Returns a value indicating whether the manager caches a <see cref="ResourceDictionary"/>
        ///     with the specified <paramref name="source"/>.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="Uri"/> of a potentially cached dictionary.
        /// </param>
        /// <returns>
        ///     true if the manager contains such a <see cref="ResourceDictionary"/>;
        ///     false if not.
        /// </returns>
        private static bool ContainsDictionary(Uri source)
        {
            return TryGetDictionary(source, out var dict);
        }

    }

    /// <summary>
    /// Provides extension methods for the <see cref="ResourceDictionary"/> class
    /// which are used by the <see cref="SharedResourceDictionaryManager"/>.
    /// </summary>
    internal static class ResourceDictionaryExtensions
    {

        /// <summary>
        /// Returns the base <see cref="Uri"/> of the specified <see cref="ResourceDictionary"/>.
        /// </summary>
        /// <param name="dict">
        /// The <see cref="ResourceDictionary"/> whose base <see cref="Uri"/> should be retrieved.
        /// </param>
        /// <returns>The dictionaries base <see cref="Uri"/>.</returns>
        public static Uri GetBaseSourceUri(this ResourceDictionary dict)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));
            return ((IUriContext)dict).BaseUri;
        }

        /// <summary>
        /// Returns an absolute <see cref="Uri"/>, based on the <paramref name="dict"/>'s
        /// source.
        /// </summary>
        /// <param name="dict">The <see cref="ResourceDictionary"/>.</param>
        /// <returns>
        /// An absolute <see cref="Uri"/>, pointing to the <paramref name="dict"/>'s source.
        /// </returns>
        public static Uri GetAbsoluteSourceUri(this ResourceDictionary dict)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));
            if (dict.Source == null) throw new ArgumentException(
                "The dictionaries Source property must not be null.", nameof(dict));
            if (dict.Source.IsAbsoluteUri) return dict.Source;

            Uri baseUri = dict.GetBaseSourceUri();
            if (baseUri == null) throw new ArgumentException(
                "Cannot compose an absolute Uri, since no base Uri could be retrieved.");

            return new Uri(baseUri, dict.Source);
        }

    }

}
