using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        private ItemsSourceCollection _menuItems;
        
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
        /// Identifies the <see cref="SelectedMenuItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedMenuItemProperty =
            DependencyProperty.Register(
                nameof(SelectedMenuItem),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(null));

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
        /// Gets or sets the currently selected menu item.
        /// </summary>
        [Bindable(true), Category("Content")]
        public object SelectedMenuItem
        {
            get { return (object)GetValue(SelectedMenuItemProperty); }
            set { SetValue(SelectedMenuItemProperty, value); }
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

    }

}
