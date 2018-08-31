using System;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Provides event data for the <see cref="NavigationView.DisplayModeChanged"/> event.
    /// This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class NavigationViewDisplayModeChangedEventArgs : EventArgs
    {

        /// <summary>
        /// Gets the old display mode.
        /// </summary>
        public NavigationViewDisplayMode OldDisplayMode { get; }

        /// <summary>
        /// Gets the new display mode.
        /// </summary>
        public NavigationViewDisplayMode NewDisplayMode { get; }
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NavigationViewDisplayModeChangedEventArgs"/> class with the specified
        /// event data.
        /// </summary>
        /// <param name="oldDisplayMode">The old display mode.</param>
        /// <param name="newDisplayMode">The new display mode.</param>
        public NavigationViewDisplayModeChangedEventArgs(
            NavigationViewDisplayMode oldDisplayMode,
            NavigationViewDisplayMode newDisplayMode)
        {
            OldDisplayMode = oldDisplayMode;
            NewDisplayMode = newDisplayMode;
        }

    }

}
