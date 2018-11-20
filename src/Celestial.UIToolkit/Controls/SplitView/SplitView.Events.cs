using System;

namespace Celestial.UIToolkit.Controls
{
    
    public partial class SplitView
    {

        /// <summary>
        /// Occurs when the <see cref="SplitView"/> pane is closing.
        /// </summary>
        public event EventHandler PaneClosing;

        /// <summary>
        /// Occurs when the <see cref="SplitView"/> pane is closed.
        /// </summary>
        public event EventHandler PaneClosed;

        /// <summary>
        /// Occurs when the <see cref="SplitView"/> pane is opening.
        /// </summary>
        public event EventHandler PaneOpening;

        /// <summary> the <see cref="SplitView"/> pane is opened.
        /// Occurs when 
        /// </summary>
        public event EventHandler PaneOpened;
        
        /// <summary>
        /// Raises the <see cref="PaneClosing"/> event.
        /// </summary>
        protected virtual void OnPaneClosing()
        {
            PaneClosing?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PaneClosed"/> event.
        /// </summary>
        protected virtual void OnPaneClosed()
        {
            PaneClosed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PaneOpening"/> event.
        /// </summary>
        protected virtual void OnPaneOpening()
        {
            PaneOpening?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PaneOpened"/> event.
        /// </summary>
        protected virtual void OnPaneOpened()
        {
            PaneOpened?.Invoke(this, EventArgs.Empty);
        }

    }

}
