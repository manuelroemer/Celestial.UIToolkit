using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    //
    // This file provides the properties which deal with the NavigationView's "DisplayMode".
    // It defines:
    // - Threshold properties for switching between display modes
    // - Pane properties for specific display modes
    // - Methods dealing with the display modes.
    //

    [TemplateVisualState(Name = MinimumVisualStateName,  GroupName = DisplayModeVisualStateGroup)]
    [TemplateVisualState(Name = CompactVisualStateName,  GroupName = DisplayModeVisualStateGroup)]
    [TemplateVisualState(Name = ExpandedVisualStateName, GroupName = DisplayModeVisualStateGroup)]
    public partial class NavigationView
    {

        internal const string DisplayModeVisualStateGroup = "DisplayModes";
        internal const string MinimumVisualStateName = "Minimal";
        internal const string CompactVisualStateName = "Compact";
        internal const string ExpandedVisualStateName = "Expanded";

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
                    ThresholdWidth_Changed));

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
                    ThresholdWidth_Changed));

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

        private static void DisplayModeProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;

            self.EnterNewDisplayMode();
            var eventData = new NavigationViewDisplayModeChangedEventArgs(
                (NavigationViewDisplayMode)e.OldValue,
                (NavigationViewDisplayMode)e.NewValue);
            self.RaiseDisplayModeChanged(eventData);
        }

        private void EnterNewDisplayMode()
        {
            // Called when DisplayMode got changed.
            // When this happens, we need to update a few properties, aswell as the current
            // VisualState.
            EnterCurrentDisplayModeVisualState();
        }

        private void EnterCurrentDisplayModeVisualState()
        {
            switch (DisplayMode)
            {
                case NavigationViewDisplayMode.Minimal:
                    VisualStateManager.GoToState(this, MinimumVisualStateName, true);
                    break;
                case NavigationViewDisplayMode.Compact:
                    VisualStateManager.GoToState(this, CompactVisualStateName, true);
                    break;
                case NavigationViewDisplayMode.Expanded:
                    VisualStateManager.GoToState(this, ExpandedVisualStateName, true);
                    break;
                default: throw new NotImplementedException();
            }
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

        private static void ThresholdWidth_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.UpdateDisplayMode();
        }

        /// <summary>
        /// Updates the <see cref="DisplayMode"/> property based on the current
        /// actual size and the threshold properties.
        /// </summary>
        private void UpdateDisplayMode()
        {
            // Ensure that Expanded takes precedence over Compact, so that the view gets expanded
            // if CompactThreshold > ExpandedThreshold.
            if (ActualWidth > ExpandedModeThresholdWidth)
            {
                DisplayMode = NavigationViewDisplayMode.Expanded;
            }
            else if (ActualWidth > CompactModeThresholdWidth)
            {
                DisplayMode = NavigationViewDisplayMode.Compact;
            }
            else
            {
                DisplayMode = NavigationViewDisplayMode.Minimal;
            }
        }

    }

}
