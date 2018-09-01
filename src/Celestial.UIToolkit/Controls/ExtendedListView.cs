using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// An extension of the <see cref="ListView"/> control which provides events and methods
    /// dealing with its Item-Containers.
    /// </summary>
    public class ExtendedListView : ListView
    {

        private bool _areInitialContainersHooked;
        private Dictionary<object, ListViewItem> _monitoredContainers;
        
        /// <summary>
        /// Occurs when one of the items in the <see cref="ListView"/> is invoked.
        /// </summary>
        public event EventHandler<ListViewItem> ItemInvoked;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedListView"/> class.
        /// </summary>
        public ExtendedListView()
        {
            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            // Once the initial item containers have been generated, attach event handlers
            // to the items.
            // This event gets called multiple times, so add a flag to only attach the handlers once.
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated &&
                !_areInitialContainersHooked)
            {
                foreach (var item in Items)
                {
                    StartMonitoringItem(item);
                }
                _areInitialContainersHooked = true;
            }
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {

            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {

            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // When a collection gets cleared, we don't get any information about the
                // previous elements.
                // We need to remove everything manually.
                InvalidateMonitoredItemContainers();
            }
        }

        private void StartMonitoringItem(object item)
        {
            if (!IsMonitoringItem(item))
            {
                var itemContainer = (ListViewItem)ItemContainerGenerator.ContainerFromItem(item);
                if (itemContainer != null)
                {
                    itemContainer.PreviewMouseLeftButtonDown += ItemContainer_Clicked;
                    _monitoredContainers[item] = itemContainer;
                }
            }
        }

        private void StopMonitoringItem(object item)
        {
            if (_monitoredContainers.TryGetValue(item, out ListViewItem itemContainer))
            {
                itemContainer.PreviewMouseLeftButtonDown -= ItemContainer_Clicked;
                _monitoredContainers.Remove(item);
            }
        }

        /// <summary>
        /// Forces the currently monitored item container elements to be invalidated.s
        /// </summary>
        protected void InvalidateMonitoredItemContainers()
        {
            // This method should be called whenever the monitored items are no longer up to date.
            // We check which monitored container is no longer present in the ListView's items
            // and remove handlers from that to avoid memory leaks.
            foreach (var monitoredItem in _monitoredContainers.Keys)
            {
                if (!Items.Contains(monitoredItem))
                {
                    StopMonitoringItem(monitoredItem);
                }
            }
        }

        private bool IsMonitoringItem(object item)
        {
            // We can use Contains() since the class implements Equals()/GetHashCode()
            // for arbitrary objects. The container part is not compared.
            return _monitoredContainers.ContainsKey(item);
        }

        private void ItemContainer_Clicked(object sender, MouseEventArgs e)
        {
            var invokedContainer = (ListViewItem)sender;
            RaiseItemInvoked(invokedContainer);
        }

        /// <summary>
        /// Raises the <see cref="ItemInvoked"/> event and
        /// calls the <see cref="OnItemInvoked"/> method afterwards.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected void RaiseItemInvoked(ListViewItem e)
        {
            OnItemInvoked(e);
            ItemInvoked?.Invoke(this, e);
        }

        /// <summary>
        /// Called before the <see cref="ItemInvoked"/> event occurs.
        /// </summary>
        /// <param name="e">Event data for the event.</param>
        protected virtual void OnItemInvoked(ListViewItem e) { }

    }

}
