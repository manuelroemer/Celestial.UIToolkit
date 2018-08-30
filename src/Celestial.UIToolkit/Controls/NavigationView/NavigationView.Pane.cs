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
        
        private static readonly DependencyPropertyKey IsPaneVisiblePropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(IsPaneVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsPaneVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneVisibleProperty =
            IsPaneVisiblePropertyKey.DependencyProperty;

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
        /// Gets or sets a value indicating whether the pane is currently expanded to its full 
        /// width.
        /// </summary>
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the pane is currently visible.
        /// </summary>
        public bool IsPaneVisible
        {
            get { return (bool)GetValue(IsPaneVisibleProperty); }
            private set { SetValue(IsPaneVisiblePropertyKey, value); }
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

    }

}
