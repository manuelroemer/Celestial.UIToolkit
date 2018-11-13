using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Represents a container with two areas.
    /// One area is a pane which typically displays navigation elements or popup-content.
    /// The other area displays the main content which typically relates to the elements in the
    /// pane.
    /// </summary>
    public partial class SplitView : ContentControl
    {

        static SplitView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SplitView), new FrameworkPropertyMetadata(typeof(SplitView)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitView"/> class.
        /// </summary>
        public SplitView()
        {
            Loaded += (sender, e) =>
            {
                // When loading, ensure that we are running the appropriate VS, so that
                // the pane is layed out correctly.
                // Use "Default" to reset anything which was previously set via property changes.
                EnterDefaultDisplayModeVisualState();
                EnterCurrentDisplayModeVisualState(false);
            };
        }

        private static void DisplayModeProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Called whenever a property which updates the DisplayModes visual states changes.
            var self = (SplitView)d;
            self.EnterCurrentDisplayModeVisualState();
        }

        private static void IsPaneOpen_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // When the pane is opened/closed, call the corresponding events and change the
            // visual state.
            var self = (SplitView)d;
            self.UpdateCurrentPaneToggleState();
        }

        private void UpdateCurrentPaneToggleState()
        {
            // IsPaneOpen already holds the new, desired value. -> Transition to these states.
            if (IsPaneOpen)
                OpenPane();
            else
                ClosePane();
        }
        
        private void OpenPane()
        {
            OnPaneOpening();
            EnterCurrentDisplayModeVisualState();
            OnPaneOpened();
        }

        private void ClosePane()
        {
            OnPaneClosing();
            EnterCurrentDisplayModeVisualState();
            OnPaneClosed();
        }
        
    }

}
