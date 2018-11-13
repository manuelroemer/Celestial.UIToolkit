using System;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        internal const double DefaultCompactModeThresholdWidth = 641;
        internal const double DefaultExpandedModeThresholdWidth = 1008;
        internal const double DefaultOpenPaneLength = 320;
        internal const double DefaultCompactPaneLength = 48;

        /// <summary>
        /// Occurs when the <see cref="DisplayMode"/> property changes.
        /// </summary>
        public event EventHandler<NavigationViewDisplayModeChangedEventArgs> DisplayModeChanged;

        private static readonly DependencyPropertyKey DisplayModePropertyKey =
		   DependencyProperty.RegisterReadOnly(
			   nameof(DisplayMode),
			   typeof(NavigationViewDisplayMode),
			   typeof(NavigationView),
			   new FrameworkPropertyMetadata(
				   NavigationViewDisplayMode.Minimal,
				   DisplayModeProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="DisplayMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayModeProperty =
            DisplayModePropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="CompactModeThresholdWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CompactModeThresholdWidthProperty =
            DependencyProperty.Register(
                nameof(CompactModeThresholdWidth),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(
                    DefaultCompactModeThresholdWidth,
                    (d, e) => ((NavigationView)d).AdaptiveLayoutProperty_Changed(d, e)));

        /// <summary>
        /// Identifies the <see cref="ExpandedModeThresholdWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpandedModeThresholdWidthProperty =
            DependencyProperty.Register(
                nameof(ExpandedModeThresholdWidth),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(
                    DefaultExpandedModeThresholdWidth,
                    (d, e) => ((NavigationView)d).AdaptiveLayoutProperty_Changed(d, e)));

        /// <summary>
        /// Gets the current display mode of the <see cref="NavigationView"/>.
        /// </summary>
        public NavigationViewDisplayMode DisplayMode
        {
            get { return (NavigationViewDisplayMode)GetValue(DisplayModeProperty); }
            internal set { SetValue(DisplayModePropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width at which the <see cref="DisplayMode"/> property of the
        /// <see cref="NavigationView"/> gets set to 
        /// <see cref="NavigationViewDisplayMode.Compact"/>.
        /// </summary>
        public double CompactModeThresholdWidth
        {
            get { return (double)GetValue(CompactModeThresholdWidthProperty); }
            set { SetValue(CompactModeThresholdWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width at which the <see cref="DisplayMode"/> property of the
        /// <see cref="NavigationView"/> gets set to 
        /// <see cref="NavigationViewDisplayMode.Expanded"/>.
        /// </summary>
        public double ExpandedModeThresholdWidth
        {
            get { return (double)GetValue(ExpandedModeThresholdWidthProperty); }
            set { SetValue(ExpandedModeThresholdWidthProperty, value); }
        }
        
        /// <summary>
        /// Raises the <see cref="DisplayModeChanged"/> event.
        /// </summary>
        /// <param name="e">Event data for the changed event.</param>
        protected virtual void OnDisplayModeChanged(NavigationViewDisplayModeChangedEventArgs e)
        {
            DisplayModeChanged?.Invoke(this, e);
        }

    }

}
