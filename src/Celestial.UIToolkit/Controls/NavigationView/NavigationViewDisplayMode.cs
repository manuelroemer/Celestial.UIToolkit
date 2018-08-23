namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Defines the different display modes of a <see cref="NavigationView"/> control.
    /// </summary>
    public enum NavigationViewDisplayMode
    {

        /// <summary>
        /// The <see cref="NavigationView"/> displays only the minimal amount of elements,
        /// which happens to be the menu button.
        /// With this button, the pane can be shown and hidden.
        /// </summary>
        Minimal,

        /// <summary>
        /// The <see cref="NavigationView"/> shows a small strip which can be expanded via the
        /// menu button.
        /// </summary>
        Compact,

        /// <summary>
        /// The <see cref="NavigationView"/> is fully expanded, meaning that the pane stays open
        /// the whole time.
        /// </summary>
        Expanded

    }

}
