using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{
    
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
    [TemplatePart(Name = BackButtonTemplatePart, Type = typeof(ButtonBase))]
    [TemplatePart(Name = ToggleButtonTemplatePart, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PaneContentContainerPart, Type = typeof(UIElement))]
    public partial class NavigationView : HeaderedContentControl
    {

        internal const string BackButtonTemplatePart = "PART_BackButton";
        internal const string ToggleButtonTemplatePart = "PART_ToggleButton";
        internal const string PaneContentContainerPart = "PART_PaneContentContainer";

        private ButtonBase _backButton;
        private ButtonBase _toggleButton;
        private UIElement _paneContentContainer;
        private ListView _menuItemsListView;

        /// <summary>
        /// Gets a value indicating whether the pane is overlaying other content.
        /// </summary>
        protected bool IsInOverlayMode =>
            DisplayMode != NavigationViewDisplayMode.Expanded && IsPaneOpen;

        /// <summary>
        /// Gets an enumerator on the <see cref="NavigationViewItem"/>'s logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                yield return base.LogicalChildren;

                if (PaneFooter != null)
                    yield return PaneFooter;
                if (PaneHeader != null)
                    yield return PaneHeader;

                foreach (var menuItem in MenuItems)
                    yield return menuItem;
            }
        }

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
            PreviewMouseLeftButtonDown += NavigationView_MouseDown;
            PreviewMouseRightButtonDown += NavigationView_MouseDown;
        }

        private void NavigationView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // A size change might update the DisplayMode, depending on the thresholds.
            UpdateAdaptiveProperties();
        }

        private void NavigationView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClosePaneOnOutsideClick(e);
        }

        private void ClosePaneOnOutsideClick(MouseButtonEventArgs e)
        {
            // If the user clicks outside of the pane and the pane is currently in an "Overlay"
            // mode, it will be closed.
            if (IsInOverlayMode)
            {
                if (e.OriginalSource is DependencyObject originalSource)
                {
                    // Not clicking on the pane == clicking on the content.
                    if (!originalSource.HasVisualAncestor(_paneContentContainer))
                    {
                        IsPaneOpen = false;
                    }
                }
            }
        }

        /// <summary>
        /// Attaches the back- and toggle-button of the <see cref="NavigationView"/> to
        /// event handlers, to that their inputs are registered and handled.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _backButton = GetTemplateChild(BackButtonTemplatePart) as ButtonBase;
            _toggleButton = GetTemplateChild(ToggleButtonTemplatePart) as ButtonBase;
            _paneContentContainer = GetTemplateChild(PaneContentContainerPart) as UIElement;

            InitializeBackButton();
            InitializeToggleButton();
        }

        private void InitializeBackButton()
        {
            if (_backButton != null)
            {
                _backButton.Click += BackButton_Click;
            }
        }

        private void InitializeToggleButton()
        {
            if (_toggleButton != null)
            {
                _toggleButton.Click += ToggleButton_Click;
            }
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsBackButtonEnabled)
            {
                RaiseBackRequested();
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsPaneOpen = !IsPaneOpen;
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
