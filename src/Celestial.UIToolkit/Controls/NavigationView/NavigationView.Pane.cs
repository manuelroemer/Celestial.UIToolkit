using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    //
    // This file defines general members which deal with the NavigationView's pane.
    // 

    [TemplateVisualState(Name = MinimalOpenVisualStateName, GroupName = PaneToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = MinimalClosedVisualStateName, GroupName = PaneToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = CompactOpenVisualStateName, GroupName = PaneToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = CompactClosedVisualStateName, GroupName = PaneToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = ExpandedOpenVisualStateName, GroupName = PaneToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = ExpandedClosedVisualStateName, GroupName = PaneToggleStatesVisualStateGroup)]
    public partial class NavigationView
    {

        internal const string PaneToggleStatesVisualStateGroup = "PaneToggleStates";
        internal const string MinimalOpenVisualStateName = "MinimalOpen";
        internal const string MinimalClosedVisualStateName = "MinimalClosed";
        internal const string CompactOpenVisualStateName = "CompactOpen";
        internal const string CompactClosedVisualStateName = "CompactClosed";
        internal const string ExpandedOpenVisualStateName = "ExpandedOpen";
        internal const string ExpandedClosedVisualStateName = "ExpandedClosed";

        internal const bool DefaultIsPaneOpen = false; // Default is Minimal state. No open pane.

        /// <summary>
        /// Identifies the <see cref="IsPaneOpen"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPaneOpenProperty =
            DependencyProperty.Register(
                nameof(IsPaneOpen),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(
                    DefaultIsPaneOpen,
                    IsPaneOpen_Changed));

        /// <summary>
        /// Gets or sets a value indicating whether the pane is currently expanded to its full 
        /// width.
        /// </summary>
        public bool IsPaneOpen
        {
            get { return (bool)GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        private static void IsPaneOpen_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.EnterCurrentPaneToggledVisualState();
        }

        private void EnterCurrentPaneToggledVisualState()
        {
            if (IsPaneOpen)
            {
                VisualStateManager.GoToState(this, ExpandedOpenVisualStateName, true);
            }
            else
            {
                VisualStateManager.GoToState(this, ExpandedClosedVisualStateName, true);
            }
        }

        private void GoToOpenVisualState()
        {

        }

    }

}
