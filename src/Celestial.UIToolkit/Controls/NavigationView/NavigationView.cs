using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    //
    // This file contains the bootstrapping logic for the NavigationView.
    // This does, for instance, include registering event handlers.
    //

    /// <summary>
    ///     A control which provides a central navigation structure to an application by providing
    ///     a pane for navigation commands and a central place for the content which is the current
    ///     navigation target.
    /// </summary>
    /// <remarks>
    ///     This control is built after the UWP navigation control.
    ///     As a result, the control behaves similarly and provides the same kind of properties.
    ///     While feature-parity is not guaranteed, having a closer look at the official 
    ///     documentation by Microsoft will be a good idea.
    /// </remarks>
    public partial class NavigationView : HeaderedContentControl
    {

        static NavigationView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NavigationView), new FrameworkPropertyMetadata(typeof(NavigationView)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationView"/> class.
        /// </summary>
        public NavigationView()
        {
            SizeChanged += NavigationView_SizeChanged;
        }

        private void NavigationView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // A size change might update the DisplayMode, depending on the thresholds.
            UpdateAdaptiveProperties();
        }

        private static void ThresholdWidth_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.UpdateAdaptiveProperties();
        }

        /// <summary>
        /// Updates the <see cref="DisplayMode"/> and <see cref="IsPaneOpen"/> properties
        /// based on the control's actual size and the threshold properties.
        /// </summary>
        private void UpdateAdaptiveProperties()
        {
            // Ensure that Expanded takes precedence over Compact, so that the view gets expanded
            // if CompactThreshold > ExpandedThreshold.
            if (ActualWidth >= ExpandedModeThresholdWidth)
            {
                DisplayMode = NavigationViewDisplayMode.Expanded;
                IsPaneOpen = true;
            }
            else if (ActualWidth >= CompactModeThresholdWidth)
            {
                DisplayMode = NavigationViewDisplayMode.Compact;
                IsPaneOpen = false;
            }
            else
            {
                DisplayMode = NavigationViewDisplayMode.Minimal;
                IsPaneOpen = false;
            }
        }

    }

}
