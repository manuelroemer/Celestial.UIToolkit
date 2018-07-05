using System;
using System.Collections.Generic;
using System.Windows;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// Manages cached <see cref="ResourceDictionary"/> instances.
    /// If this class is used to retrieve a <see cref="ResourceDictionary"/>,
    /// it allows a single resource dictionary to be referenced multiple times,
    /// instead of being reloaded each time it is included in another dictionary.
    /// </summary>
    public static class SharedResourceDictionaryManager
    {

        private static IList<WeakReference<ResourceDictionary>> _dictionaries = 
            new List<WeakReference<ResourceDictionary>>();

        /// <summary>
        ///     Returns <see cref="ResourceDictionary"/> instance which is either already cached,
        ///     or directly loaded and then cached by the manager.
        /// </summary>
        /// <param name="sourceUriString">
        ///     The URI string of a <see cref="ResourceDictionary"/> to be retrieved.
        /// </param>
        /// <returns>
        ///     A <see cref="ResourceDictionary"/> instance which was either loaded, or cached.
        /// </returns>
        public static ResourceDictionary GetDictionary(string sourceUriString)
        {
            return GetDictionary(new Uri(sourceUriString));
        }

        /// <summary>
        ///     Returns <see cref="ResourceDictionary"/> instance which is either already cached,
        ///     or directly loaded and then cached by the manager.
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
                var loadedDict = (ResourceDictionary)Application.LoadComponent(source);
                CacheDictionary(loadedDict);
                return loadedDict;
            }
        }
        
        /// <summary>
        ///     Adds the specified <paramref name="dictionary"/> to the cache.
        /// </summary>
        /// <param name="dictionary">
        ///     The <see cref="ResourceDictionary"/> instance to be cached.
        /// </param>
        private static void CacheDictionary(ResourceDictionary dictionary)
        {
            if (dictionary != null)
            {
                if (!ContainsDictionary(dictionary.Source)) // This check is prob. not necessary, but keep it
                {                                           // in case some other methods are added in the future.
                    _dictionaries.Add(new WeakReference<ResourceDictionary>(dictionary));
                }
            }
        }

        /// <summary>
        ///     Tries to find a cached <see cref="ResourceDictionary"/>, based on the specified
        ///     <paramref name="source"/>.
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
        private static bool TryGetDictionary(Uri source, out ResourceDictionary resourceDictionary)
        {
            for (int i = _dictionaries.Count - 1; i >= 0; i--)
            {
                var dictRef = _dictionaries[i];
                if (dictRef.TryGetTarget(out ResourceDictionary dict))
                {
                    if (dict.Source == source)
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

}
