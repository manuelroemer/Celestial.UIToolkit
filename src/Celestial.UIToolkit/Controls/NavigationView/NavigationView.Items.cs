using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        /// <summary>
        /// Occurs when the currently selected item changes.
        /// </summary>
        public event EventHandler<NavigationViewItemEventArgs> SelectedItemChanged;

        private ItemsSourceCollection _menuItems;

        private static readonly DependencyPropertyKey SettingsItemPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(SettingsItem),
                typeof(NavigationViewItem),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="SettingsItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SettingsItemProperty =
            SettingsItemPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="IsSettingsVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSettingsVisibleProperty =
            DependencyProperty.Register(
                nameof(IsSettingsVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(false));
        
        /// <summary>
        /// Identifies the <see cref="MenuItemsSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemsSourceProperty =
            DependencyProperty.Register(
                nameof(MenuItemsSource),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    MenuItemsSource_Changed));
        
        /// <summary>
        /// Identifies the <see cref="MenuItemTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemTemplateProperty =
            DependencyProperty.Register(
                nameof(MenuItemTemplate),
                typeof(DataTemplate),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="MenuItemTemplateSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemTemplateSelectorProperty =
            DependencyProperty.Register(
                nameof(MenuItemTemplateSelector),
                typeof(DataTemplateSelector),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="MenuItemContainerStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemContainerStyleProperty =
            DependencyProperty.Register(
                nameof(MenuItemContainerStyle),
                typeof(Style),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="MenuItemContainerStyleSelector"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MenuItemContainerStyleSelectorProperty =
            DependencyProperty.Register(
                nameof(MenuItemContainerStyleSelector),
                typeof(StyleSelector),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="SelectedItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(
                    null,
                    SelectedItem_Changed,
                    CoerceSelectedItem));

        /// <summary>
        /// Gets the special settings <see cref="NavigationViewItem"/>.
        /// This can return null if the current template doesn't implement it.
        /// </summary>
        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public NavigationViewItem SettingsItem
        {
            get { return (NavigationViewItem)GetValue(SettingsItemProperty); }
            private set { SetValue(SettingsItemPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the special settings navigation item is
        /// visible.
        /// </summary>
        [Bindable(true), Category("Content")]
        public bool IsSettingsVisible
        {
            get { return (bool)GetValue(IsSettingsVisibleProperty); }
            set { SetValue(IsSettingsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets an object which serves as a source for the menu items of
        /// the <see cref="NavigationView"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        public object MenuItemsSource
        {
            get { return (object)GetValue(MenuItemsSourceProperty); }
            set { SetValue(MenuItemsSourceProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> used to display each menu item.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplate MenuItemTemplate
        {
            get { return (DataTemplate)GetValue(MenuItemTemplateProperty); }
            set { SetValue(MenuItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a a custom <see cref="DataTemplateSelector"/> which 
        /// returns a template to apply to items.
        /// </summary>
        [Bindable(true), Category("Content")]
        public DataTemplateSelector MenuItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(MenuItemTemplateSelectorProperty); }
            set { SetValue(MenuItemTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style that is used when rendering the menu item containers.
        /// </summary>
        [Bindable(true), Category("Content")]
        public Style MenuItemContainerStyle
        {
            get { return (Style)GetValue(MenuItemContainerStyleProperty); }
            set { SetValue(MenuItemContainerStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a custom <see cref="StyleSelector"/> which returns different
        /// <see cref="Style"/> values to use for the item container based on characteristics
        /// of the object being displayed.
        /// </summary>
        [Bindable(true), Category("Content")]
        public StyleSelector MenuItemContainerStyleSelector
        {
            get { return (StyleSelector)GetValue(MenuItemContainerStyleSelectorProperty); }
            set { SetValue(MenuItemContainerStyleSelectorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the currently selected item.
        /// </summary>
        [Bindable(true), Category("Content")]
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets a collection of the <see cref="NavigationView"/>'s menu items.
        /// The collection's contents can either directly be set by this collection, or via
        /// the <see cref="MenuItemsSource"/>.
        /// </summary>
        [Bindable(true), Category("Content")]
        public ItemsSourceCollection MenuItems
        {
            get
            {
                if (_menuItems == null)
                {
                    _menuItems = new ItemsSourceCollection();
                    _menuItems.CollectionChanged += MenuItems_Changed;
                }
                return _menuItems;
            }   
        }
        
        private static void MenuItemsSource_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            self.MenuItems.ItemsSource = e.NewValue;
        }

        private void MenuItems_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                    AddLogicalChild(newItem);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove ||
                     e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var oldItem in e.OldItems)
                    RemoveLogicalChild(oldItem);
            }
        }

        private static void SelectedItem_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (NavigationView)d;
            bool isSettingsItem = e.NewValue != null &&
                                  e.NewValue == self.SettingsItem;
            var itemChangedArgs = new NavigationViewItemEventArgs(e.NewValue, isSettingsItem);

            self.RaiseSelectedItemChanged(itemChangedArgs);
        }

        private static object CoerceSelectedItem(DependencyObject d, object newSelectedItem)
        {
            var self = (NavigationView)d;
            bool acceptsNewItem = newSelectedItem == null ||
                                  self.MenuItems.Contains(newSelectedItem) ||
                                  newSelectedItem == self.SettingsItem;
            return acceptsNewItem ? newSelectedItem : null;
        }
        
        /// <summary>
        /// Raises the <see cref="SelectedItemChanged"/> event and
        /// calls the <see cref="OnSelectedItemChanged"/> method afterwards.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected void RaiseSelectedItemChanged(NavigationViewItemEventArgs e)
        {
            OnSelectedItemChanged(e);
            SelectedItemChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Called before the <see cref="SelectedItemChanged"/> event occurs.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected virtual void OnSelectedItemChanged(NavigationViewItemEventArgs e) { }

    }

}
