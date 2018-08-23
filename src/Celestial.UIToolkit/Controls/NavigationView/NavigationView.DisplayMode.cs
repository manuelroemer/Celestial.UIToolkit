using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{
	
    public partial class NavigationView
    {

        internal const double DefaultCompactModeThresholdWidth = 641;
        internal const double DefaultExpandedModeThresholdWidth = 1008;

        /// <summary>
        /// Occurs when the <see cref="DisplayMode"/> property changes.
        /// </summary>
        public event EventHandler<NavigationViewDisplayModeChangedEventArgs> DisplayModeChanged;

        private static readonly DependencyPropertyKey DisplayModePropertyKey =
		   DependencyProperty.RegisterReadOnly(
			   nameof(DisplayMode),
			   typeof(NavigationViewDisplayMode),
			   typeof(NavigationView),
			   new PropertyMetadata(
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
                new PropertyMetadata(DefaultCompactModeThresholdWidth));

        /// <summary>
        /// Identifies the <see cref="ExpandedThresholdWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpandedThresholdWidthProperty =
            DependencyProperty.Register(
                nameof(ExpandedThresholdWidth),
                typeof(double),
                typeof(NavigationView),
                new PropertyMetadata(DefaultExpandedModeThresholdWidth));

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
        public double ExpandedThresholdWidth
        {
            get { return (double)GetValue(ExpandedThresholdWidthProperty); }
            set { SetValue(ExpandedThresholdWidthProperty, value); }
        }

        private static void DisplayModeProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            var eventData = new NavigationViewDisplayModeChangedEventArgs(
                (NavigationViewDisplayMode)e.OldValue,
                (NavigationViewDisplayMode)e.NewValue);
            self.RaiseDisplayModeChanged(eventData);
        }

        internal void RaiseDisplayModeChanged(NavigationViewDisplayModeChangedEventArgs e)
        {
            OnDisplayModeChanged(e);
            DisplayModeChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Called before the <see cref="DisplayModeChanged"/> event occurs.
        /// </summary>
        /// <param name="e">Event data for the changed event.</param>
        protected virtual void OnDisplayModeChanged(NavigationViewDisplayModeChangedEventArgs e) { }

    }

}
