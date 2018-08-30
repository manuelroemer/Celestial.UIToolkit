using System;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Provides generic event data regarding a single item
    /// which is hosted by a <see cref="NavigationView"/>.
    /// </summary>
    public class NavigationViewItemEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the item to which this event data object refers.
        /// </summary>
        public object Item { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Item"/> is the special
        /// settings navigation item.
        /// </summary>
        public bool IsSettingsItem { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewItemEventArgs"/> class
        /// </summary>
        /// <param name="item">The item to which this event data object refers.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is null.
        /// </exception>
        public NavigationViewItemEventArgs(object item)
            : this(item, false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewItemEventArgs"/> class
        /// </summary>
        /// <param name="item">The item to which this event data object refers.</param>
        /// <param name="isSettingsItem">
        /// A value indicating whether the <see cref="Item"/> is the special settings
        /// navigation item.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is null.
        /// </exception>
        public NavigationViewItemEventArgs(object item, bool isSettingsItem)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            IsSettingsItem = isSettingsItem;
        }

        /// <summary>
        /// Returns a string representation of this event data object.
        /// </summary>
        /// <returns>A string representing this object.</returns>
        public override string ToString()
        {
            return $"{nameof(Item)}: {Item}, {nameof(IsSettingsItem)}: {IsSettingsItem}";
        }

    }

}
