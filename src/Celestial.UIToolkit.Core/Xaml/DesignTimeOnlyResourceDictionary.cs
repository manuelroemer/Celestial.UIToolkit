using System;
using System.Windows;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// A special resource dictionary which only loads its content, if it is currently in the
    /// design mode.
    /// Otherwise, the loading process will simply be ignored.
    /// </summary>
    public class DesignTimeOnlyResourceDictionary : ResourceDictionary
    {

        private Uri _source;

        /// <summary>
        /// Gets a value indicating whether the dictionary has loaded any content.
        /// </summary>
        public bool IsLoaded => base.Source != null;

        /// <summary>
        /// Gets or sets the source <see cref="Uri"/> of the resource dictionary.
        /// </summary>
        public new Uri Source
        {
            get { return _source; }
            set
            {
                _source = value;
                if (DesignMode.IsEnabled)
                {
                    base.Source = value;
                    ResourcesSource.TraceInformation(
                        "Loaded design time resource dictionary at {0}", Source);
                }
                else
                {
                    ResourcesSource.TraceInformation(
                        "Skipping design time resource dictionary at {0}", Source);
                }
            }
        }

        /// <summary>
        /// Returns a string which represents the current state of the resource dictionary.
        /// </summary>
        /// <returns>A string representing the current state of the resource dictionary.</returns>
        public override string ToString()
        {
            return $"{nameof(DesignTimeOnlyResourceDictionary)}: " +
                   $"{nameof(IsLoaded)}: {IsLoaded}, " +
                   $"{nameof(Source)}: {Source}";
        }

    }

}
