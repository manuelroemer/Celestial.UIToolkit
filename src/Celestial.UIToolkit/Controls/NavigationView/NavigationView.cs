using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
    [TemplatePart(Name = PaneButtonContainerPart, Type = typeof(UIElement))]
    [TemplatePart(Name = MenuItemsListViewPart, Type = typeof(NavigationViewListView))]
    [TemplatePart(Name = SettingsItemListViewPart, Type = typeof(NavigationViewListView))]
    [TemplatePart(Name = SettingsItemPart, Type = typeof(NavigationViewItem))]
    public partial class NavigationView : HeaderedContentControl
    {

        internal const string BackButtonTemplatePart = "PART_BackButton";
        internal const string ToggleButtonTemplatePart = "PART_ToggleButton";
        internal const string PaneContentContainerPart = "PART_PaneContentContainer";
        internal const string PaneButtonContainerPart = "PART_PaneButtonContainer";
        internal const string MenuItemsListViewPart = "PART_MenuItemsListView";
        internal const string SettingsItemListViewPart = "PART_SettingsItemListView";
        internal const string SettingsItemPart = "PART_SettingsItem";

        private ButtonBase _backButton;
        private ButtonBase _toggleButton;
        private UIElement _paneContentContainer;
        private UIElement _paneButtonContainer;
        private NavigationViewListView _menuItemsListView;
        private NavigationViewListView _settingsItemListView;

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
                if (PaneCustomContent != null)
                    yield return PaneCustomContent;

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
            SizeChanged += AdaptiveLayoutProperty_Changed;
            PreviewMouseLeftButtonDown += NavigationView_MouseDown;
            PreviewMouseRightButtonDown += NavigationView_MouseDown;
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
            _paneButtonContainer = GetTemplateChild(PaneButtonContainerPart) as UIElement;
            _menuItemsListView = GetTemplateChild(MenuItemsListViewPart) as NavigationViewListView;
            _settingsItemListView = GetTemplateChild(SettingsItemListViewPart) as NavigationViewListView;
            SettingsItem = GetTemplateChild(SettingsItemPart) as NavigationViewItem;

            HookPaneButtonEvents();
            HookItemInvokedEvents();
        }

        private void HookPaneButtonEvents()
        {
            if (_backButton != null)
                _backButton.Click += BackButton_Click;

            if (_toggleButton != null)
                _toggleButton.Click += ToggleButton_Click;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseBackRequested();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsPaneOpen = !IsPaneOpen;
        }

        private void HookItemInvokedEvents()
        {
            if (_menuItemsListView != null)
            {
                _menuItemsListView.ItemContainerInvoked += ItemsListView_ItemInvoked;
            }

            if (_settingsItemListView != null)
            {
                _settingsItemListView.ItemContainerInvoked += ItemsListView_ItemInvoked;
            }
        }

        private void ItemsListView_ItemInvoked(object sender, ListViewItemInvokedEventArgs e)
        {
            // We always get a NavigationViewItem in the event args (the clicked item).
            // We want to raise the ItemInvoked event with the "real" item in the MenuItems
            // collection though.
            // -> We need the ItemContainerGenerator to retrieve it.
            var itemsContainer = (ItemsControl)sender;
            var item = itemsContainer.ItemContainerGenerator.ItemFromContainer(e.InvokedItem);            
            bool isSettingsItem = SettingsItem != null &&
                                  e.InvokedItem == SettingsItem;

            var eventData = new NavigationViewItemEventArgs(item, isSettingsItem);
            RaiseItemInvoked(eventData);
        }

        private void AdaptiveLayoutProperty_Changed(object sender, object e)
        {
            // Called when something size-related changed.
            // When this happens, we might have to update some adaptive properties.
            UpdateAdaptiveProperties();
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

        private void NavigationView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClosePaneOnOutsideClick(e.OriginalSource as DependencyObject);
        }

        /// <summary>
        /// Closes the pane if it is in the "Overlay" mode and the user clicked outside of it.
        /// </summary>
        /// <param name="clickedElement">The element that was clicked.</param>
        private void ClosePaneOnOutsideClick(DependencyObject clickedElement)
        {
            if (clickedElement != null && IsInOverlayMode)
            {
                // We need to close the pane if the user clicks outside of the pane.
                // -> Check if the clicked element is inside the pane. If not, close the pane.
                // Also include the container of the back/toggle button, since they might be
                // floating over the pane and thus aren't direct children of it.
                if (!clickedElement.HasVisualAncestor(_paneContentContainer) &&
                    !clickedElement.HasVisualAncestor(_paneButtonContainer))
                {
                    IsPaneOpen = false;
                }
            }
        }

        private static void DisplayModeProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            
            self.EnterCurrentDisplayModeVisualState();
            var eventData = new NavigationViewDisplayModeChangedEventArgs(
                (NavigationViewDisplayMode)e.OldValue,
                (NavigationViewDisplayMode)e.NewValue);
            self.RaiseDisplayModeChanged(eventData);
        }
        
        private static void MenuItemsSource_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.MenuItems.ItemsSource = e.NewValue;
        }

        private void MenuItems_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Keep the logical children tree in sync.
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                    AddLogicalChild(newItem);
            }

            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                    RemoveLogicalChild(oldItem);
            }
        }

        private static void SelectedItem_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Notify any listeners when the selected item changed.
            var self = (NavigationView)d;
            bool isSettingsItem = e.NewValue != null &&
                                  e.NewValue == self.SettingsItem;
            var itemChangedArgs = new NavigationViewItemEventArgs(e.NewValue, isSettingsItem);

            self.RaiseSelectedItemChanged(itemChangedArgs);
        }

        private static object CoerceSelectedItem(DependencyObject d, object newSelectedItem)
        {
            // Mimicing the ListView:
            // If SelectedItem gets set to something which isn't part of the NavigationView
            // (-> not added to MenuItems and not SettingsItem),
            // change it to null.
            var self = (NavigationView)d;
            bool acceptsNewItem = newSelectedItem == null ||
                                  self.MenuItems.Contains(newSelectedItem) ||
                                  newSelectedItem == self.SettingsItem;
            return acceptsNewItem ? newSelectedItem : null;
        }
     
        private DependencyObject ContainerFromItem(object item)
        {
            var listView = new ListView();
            var containerGenerator = listView.ItemContainerGenerator;

            return containerGenerator.ContainerFromItem(item);
        }

    }

}
