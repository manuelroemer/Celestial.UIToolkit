using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Provides event data for when a <see cref="ListViewItem"/> is invoked.
    /// </summary>
    public class ListViewItemInvokedEventArgs : EventArgs
    {

        /// <summary>
        /// Gets or sets the invoked item.
        /// </summary>
        public ListViewItem InvokedItem { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewItemInvokedEventArgs"/> class.
        /// </summary>
        /// <param name="invokedItem">The invoked item.</param>
        public ListViewItemInvokedEventArgs(ListViewItem invokedItem)
        {
            InvokedItem = invokedItem ?? throw new ArgumentNullException(nameof(invokedItem));
        }

        /// <summary>
        /// Returns a string representation of this class.
        /// </summary>
        /// <returns>A string representing this class.</returns>
        public override string ToString()
        {
            return $"{nameof(ListViewItemInvokedEventArgs)}: {InvokedItem}";
        }

    }

    /// <summary>
    /// An extension of the <see cref="ListView"/> control which provides events and methods
    /// dealing with its Item-Containers.
    /// </summary>
    /// <remarks>
    /// This custom list view is mainly intended to be used by the <see cref="NavigationView"/>.
    /// Since the functionality here is universal and not specific to the 
    /// <see cref="NavigationView"/>, it is provided as a publicly available class.
    /// 
    /// Note that it is not designed to work performantly with a large amount of items.
    /// The normal ListView does a better job at that.
    /// </remarks>
    public class ExtendedListView : ListView
    {
        
        private List<object> _unmonitoredItems = new List<object>();
        
        /// <summary>
        /// Occurs when one of the items in the <see cref="ListView"/> is invoked.
        /// </summary>
        public event EventHandler<ListViewItemInvokedEventArgs> ItemContainerInvoked;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedListView"/> class.
        /// </summary>
        public ExtendedListView()
        {
            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        /// <summary>
        /// Called when the <see cref="ItemsControl.Items"/> collection changes.
        /// Attaches <see cref="ItemContainerInvoked"/> handlers to new items.
        /// </summary>
        /// <param name="e">Event data about the collection change.</param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            // The goal of this class is to attach an event to an item's generated container.
            // The ItemContainerGenerator takes some time to generate the items though.
            // -> Store the items in a separate collection and wait for the generator to generate
            //    the container. Once done, attach the event and forget about the item.
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    _unmonitoredItems.Add(newItem);
                }
            }

            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                {
                    // Even though we are using weak events, it doesn't hurt to remove unused
                    // handlers manually.
                    StopMonitoringItem(oldItem);
                    _unmonitoredItems.Remove(oldItem);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Reset means that the list was either cleared, or initialized with a range of items.
                _unmonitoredItems.Clear();

                foreach (var item in Items)
                    _unmonitoredItems.Add(item);
            }

            base.OnItemsChanged(e);
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            // Once the initial item containers have been generated, attach event handlers
            // to the items.
            // This event gets called multiple times, so add a flag to only attach the handlers once.
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                for (int i = _unmonitoredItems.Count - 1; i >= 0; i--)
                {
                    var item = _unmonitoredItems.ElementAt(i);
                    if (StartMonitoringItem(item))
                    {
                        _unmonitoredItems.Remove(item);
                    }
                }
            }
        }

        private bool StartMonitoringItem(object item)
        {
            // Don't attach events twice, so do an unregister first.
            StopMonitoringItem(item);

            var itemContainers = GetAllContainersFromItem(item);
            if (itemContainers.Count() == 0)
                return false;

            foreach (var itemContainer in itemContainers)
            {
                // We don't really know when the item is removed.
                // -> Use WeakEvents.
                WeakEventManager<ListViewItem, MouseButtonEventArgs>.AddHandler(
                    itemContainer,
                    nameof(PreviewMouseLeftButtonDown),
                    ItemContainer_Clicked);
                WeakEventManager<ListViewItem, KeyEventArgs>.AddHandler(
                    itemContainer,
                    nameof(PreviewKeyDown),
                    ItemContainer_KeyDown);
            }
            return true;
        }

        private void StopMonitoringItem(object item)
        {
            foreach (var itemContainer in GetAllContainersFromItem(item))
            {
                if (itemContainer != null)
                {
                    WeakEventManager<ListViewItem, MouseButtonEventArgs>.RemoveHandler(
                        itemContainer,
                        nameof(PreviewMouseLeftButtonDown),
                        ItemContainer_Clicked);
                    WeakEventManager<ListViewItem, KeyEventArgs>.RemoveHandler(
                        itemContainer,
                        nameof(PreviewKeyDown),
                        ItemContainer_KeyDown);
                }
            }
        }

        /// <summary>
        /// Returns all item containers for the specified <paramref name="item"/>.
        /// </summary>
        private IEnumerable<ListViewItem> GetAllContainersFromItem(object item)
        {
            // If the same instance of an object gets added to the LV's items multiple times,
            // the ItemContainerGenerator.ContainerFromItem() would only return the container
            // of the first instance in the list.
            // -> We need to go through the items manually for finding all containers.
            for (int i = 0; i < ItemContainerGenerator.Items.Count; i++)
            {
                if (item == ItemContainerGenerator.Items[i])
                {
                    yield return (ListViewItem)ItemContainerGenerator.ContainerFromIndex(i);
                }
            }
        }

        private void ItemContainer_Clicked(object sender, MouseButtonEventArgs e)
        {
            var invokedContainer = (ListViewItem)sender;
            OnItemContainerInvoked(new ListViewItemInvokedEventArgs(invokedContainer));
        }

        private void ItemContainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                var invokedContainer = (ListViewItem)sender;
                OnItemContainerInvoked(new ListViewItemInvokedEventArgs(invokedContainer));
            }
        }
        
        /// <summary>
        /// Raises the <see cref="ItemContainerInvoked"/> event.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected virtual void OnItemContainerInvoked(ListViewItemInvokedEventArgs e)
        {
            ItemContainerInvoked?.Invoke(this, e);
        }
        
    }

}
