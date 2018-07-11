using System;
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
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class SharedResourceDictionary : ResourceDictionary
    {

        private static bool _isInDesignMode = (bool)DesignerProperties.IsInDesignModeProperty
            .GetMetadata(typeof(DependencyObject)).DefaultValue;

        /// <summary>
        /// Gets this dictionary's base <see cref="Uri"/>.
        /// </summary>
        protected Uri BaseUri => ((IUriContext)this).BaseUri;

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
                    base.Source = value;
                }
                else
                {
                    _source = MakeAbsoluteUri(this.BaseUri, value);
                    if (SharedResourceDictionaryManager.TryGetDictionary(_source, out var dict))
                    {
                        this.MergedDictionaries.Add(dict);
                    }
                    else
                    {
                        base.Source = value;
                    }
                }
            }
        }

        /// <summary>
        /// Returns an absolute <see cref="Uri"/>, based on the state of the
        /// provided parameters.
        /// </summary>
        /// <param name="baseUri">A base <see cref="Uri"/> to be used.</param>
        /// <param name="other">Another <see cref="Uri"/> to be used. Can be both relative and absolute.</param>
        /// <returns>The created object.</returns>
        protected static Uri MakeAbsoluteUri(Uri baseUri, Uri other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (baseUri == null) throw new ArgumentNullException(nameof(baseUri));
            if (other.IsAbsoluteUri) return other;
            return new Uri(baseUri, other);
        }

    }

}
