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
        /// Raises the <see cref="PaneClosing"/> event and
        /// calls the <see cref="OnPaneClosing"/> method afterwards.
        /// </summary>
        protected void RaisePaneClosing()
        {
            OnPaneClosing();
            PaneClosing?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PaneClosed"/> event and
        /// calls the <see cref="OnPaneClosed"/> method afterwards.
        /// </summary>
        protected void RaisePaneClosed()
        {
            OnPaneClosed();
            PaneClosed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PaneOpening"/> event and
        /// calls the <see cref="OnPaneOpening"/> method afterwards.
        /// </summary>
        protected void RaisePaneOpening()
        {
            OnPaneOpening();
            PaneOpening?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="PaneOpened"/> event and
        /// calls the <see cref="OnPaneOpened"/> method afterwards.
        /// </summary>
        protected void RaisePaneOpened()
        {
            OnPaneOpened();
            PaneOpened?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called before the <see cref="PaneClosing"/> event occurs.
        /// </summary>
        protected virtual void OnPaneClosing() { }

        /// <summary>
        /// Called before the <see cref="PaneClosed"/> event occurs.
        /// </summary>
        protected virtual void OnPaneClosed() { }

        /// <summary>
        /// Called before the <see cref="PaneOpening"/> event occurs.
        /// </summary>
        protected virtual void OnPaneOpening() { }

        /// <summary>
        /// Called before the <see cref="PaneOpened"/> event occurs.
        /// </summary>
        protected virtual void OnPaneOpened() { }

    }

}
