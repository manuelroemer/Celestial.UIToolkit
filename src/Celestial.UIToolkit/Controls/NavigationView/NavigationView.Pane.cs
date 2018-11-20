using System;
using System.Windows;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        /// <summary>
        /// Identifies the <see cref="IsPaneOpen"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneOpenProperty =
            DependencyProperty.Register(
                nameof(IsPaneOpen),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsPaneContentVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneContentVisibleProperty =
            DependencyProperty.Register(
                nameof(IsPaneContentVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="OpenPaneLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OpenPaneLengthProperty =
            DependencyProperty.Register(
                nameof(OpenPaneLength),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(DefaultOpenPaneLength));

        /// <summary>
        /// Identifies the <see cref="CompactPaneLength"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CompactPaneLengthProperty =
            DependencyProperty.Register(
                nameof(CompactPaneLength),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(DefaultCompactPaneLength));

        /// <summary>
        /// Identifies the <see cref="PaneBackground"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneBackgroundProperty =
            DependencyProperty.Register(
                nameof(PaneBackground),
                typeof(Brush),
                typeof(NavigationView),
                new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Identifies the <see cref="AutoCloseOverlayingPane"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AutoCloseOverlayingPaneProperty =
            DependencyProperty.Register(
                nameof(AutoCloseOverlayingPane),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether the pane is currently expanded to its full 
        /// width.
        /// </summary>
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pane is currently visible.
        /// This value relates to the panes <see cref="UIElement.Visibility"/> property.
        /// </summary>
        public bool IsPaneContentVisible
        {
            get { return (bool)GetValue(IsPaneContentVisibleProperty); }
            set { SetValue(IsPaneContentVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of the pane when it is fully expanded.
        /// </summary>
        public double OpenPaneLength
        {
            get { return (double)GetValue(OpenPaneLengthProperty); }
            set { SetValue(OpenPaneLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of the pane when the <see cref="NavigationView"/> is in its
        /// <see cref="NavigationViewDisplayMode.Compact"/> display mode.
        /// </summary>
        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            set { SetValue(CompactPaneLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the background brush of the pane.
        /// </summary>
        public Brush PaneBackground
        {
            get { return (Brush)GetValue(PaneBackgroundProperty); }
            set { SetValue(PaneBackgroundProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the pane gets automatically closed if the
        ///     <see cref="NavigationView"/> is currently overlaying the content.
        /// 
        ///     If this is true, the pane gets closed when the user presses outside of the pane,
        ///     or when he invokes an item.
        /// </summary>
        /// <remarks>
        ///     Overlaying means, that the <see cref="NavigationView"/> is either in the
        ///     <see cref="NavigationViewDisplayMode.Minimal"/> or 
        ///     <see cref="NavigationViewDisplayMode.Compact"/> <see cref="DisplayMode"/>.
        ///     
        ///     If this is the case, the pane overlays the actual content.
        /// </remarks>
        public bool AutoCloseOverlayingPane
        {
            get { return (bool)GetValue(AutoCloseOverlayingPaneProperty); }
            set { SetValue(AutoCloseOverlayingPaneProperty, value); }
        }

    }

}
