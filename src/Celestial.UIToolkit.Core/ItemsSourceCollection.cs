using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// A collection which can retrieve its items from an items source, if specified.
    /// If not, it behaves like a normal collection to which any item can be added.
    /// </summary>
    /// <seealso cref="ItemsSource"/>
    public class ItemsSourceCollection : IList, IList<object>
    {
        
        private object _itemsSource;
        private IEnumerable<object> _enumerableItemsSource;
        private List<object> _innerCollection;

        /// <summary>
        /// Gets a value indicating whether the collection and its contents is currently
        /// based on an items source provided by the <see cref="ItemsSource"/> property.
        /// </summary>
        public bool IsUsingItemsSource => ItemsSource != null;

        /// <summary>
        /// Returns a value indicating whether the <see cref="ItemsSource"/> implements
        /// the <see cref="IEnumerable"/> interface.
        /// </summary>
        /// <remarks>
        /// Note that the non-generic <see cref="IEnumerable"/> interface is tested by this
        /// property.
        /// </remarks>
        protected bool HasEnumerableItemsSource => HasEnumerableItemsSource;

        /// <summary>
        /// Gets or sets an object from which this collection retrieves its items.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this property is set while the collection contains any items which do not
        /// come from another items source.
        /// </exception>
        public object ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                ThrowIfItemsSourceIsNotChangeable();
                _itemsSource = value;

                // ItemsSource can be any object, but if we can convert it to an enumerable, 
                // we have a whole lot of additional methods that we can use.
                if (_itemsSource != null && _itemsSource is IEnumerable enumerable)
                {
                    _enumerableItemsSource = enumerable.Cast<object>();
                }
            }
        }
        
        /// <summary>
        /// Gets the element at the specified index, or
        /// sets the item at the specified index within the current collection, if
        /// no <see cref="ItemsSource"/> is set.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// The element at the specified index in the collection, if <see cref="ItemsSource"/>
        /// is null.
        /// If <see cref="ItemsSource"/> implements <see cref="IEnumerable"/>, returns the element
        /// at the specified index from the <see cref="ItemsSource"/>.
        /// If <see cref="ItemsSource"/> is set, but does not implement <see cref="IEnumerable"/>,
        /// this will return the <see cref="ItemsSource"/>, as long as the <paramref name="index"/>
        /// is 0.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException" />
        /// <exception cref="InvalidOperationException">
        /// Thrown if an attempt to set this property was made, while the collection had an active
        /// <see cref="ItemsSource"/>.
        /// </exception>
        public object this[int index]
        {
            get
            {
                return GetElementAt(index);
            }
            set
            {
                ThrowIfInnerCollectionIsNotWriteable();
                _innerCollection[index] = value;
            }
        }

        /// <summary>
        /// Gets the number of elements in this collection or an active <see cref="ItemsSource"/>
        /// which implements <see cref="IEnumerable"/>.
        /// If <see cref="ItemsSource"/> is set, but does not implement <see cref="IEnumerable"/>,
        /// this returns 1.
        /// </summary>
        public int Count
        {
            get
            {
                if (IsUsingItemsSource)
                {
                    if (HasEnumerableItemsSource)
                    {
                        return _enumerableItemsSource.Count();
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return _innerCollection.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is currently read-only,
        /// meaning that any methods which modify it will throw an exception.
        /// </summary>
        public bool IsReadOnly => IsUsingItemsSource;

        /// <summary>
        /// Gets a value indicating whether the collection has a fixed size.
        /// </summary>
        public bool IsFixedSize => IsUsingItemsSource;

        /// <summary>
        /// Gets an object which can be used for cross-thread synchronization.
        /// Accessing this property while an <see cref="ItemsSource"/> is set will
        /// throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// Thrown when this property is accessed while the <see cref="ItemsSource"/> is set to a 
        /// valid value.
        /// </exception>
        public object SyncRoot
        {
            get
            {
                if (IsUsingItemsSource)
                    throw new NotSupportedException(
                        $"The {nameof(ItemsSourceCollection)} doesn't provide a synchronization " +
                        $"object if an {nameof(ItemsSource)} is used.");
                return ((ICollection)_innerCollection).SyncRoot;
            }
        }

        /// <summary>
        /// Gets a value which indicates whether the collection is synchronized.
        /// This is not the case. This returns false.
        /// </summary>
        public bool IsSynchronized => false;

        /// <summary>
        /// Initializes a new, empty <see cref="ItemsSourceCollection"/> without an
        /// <see cref="ItemsSource"/>.
        /// </summary>
        public ItemsSourceCollection()
        {
            _innerCollection = new List<object>();
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// The element at the specified index in the collection, if <see cref="ItemsSource"/>
        /// is null.
        /// If <see cref="ItemsSource"/> implements <see cref="IEnumerable"/>, returns the element
        /// at the specified index from the <see cref="ItemsSource"/>.
        /// If <see cref="ItemsSource"/> is set, but does not implement <see cref="IEnumerable"/>,
        /// this will return the <see cref="ItemsSource"/>, as long as the <paramref name="index"/>
        /// is 0.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException" />
        public object GetElementAt(int index)
        {
            if (IsUsingItemsSource)
            {
                if (HasEnumerableItemsSource)
                {
                    return _enumerableItemsSource.ElementAt(index);
                }
                else
                {
                    if (index != 0)
                    {
                        throw new IndexOutOfRangeException(
                            $"For a non-enumerable {nameof(ItemsSource)}, the only supported " +
                            $"index is 0.");
                    }
                    return ItemsSource;
                }
            }
            else
            {
                return _innerCollection[index];
            }
        }

        /// <summary>
        /// Returns the index of the specified <paramref name="value"/> within the collection.
        /// </summary>
        /// <param name="value">The value to be found.</param>
        /// <returns>
        /// The index of the <paramref name="value"/> within the collection or the 
        /// <see cref="ItemsSource"/>, if the latter implements <see cref="IEnumerable"/>.
        /// If <see cref="ItemsSource"/> is set, but does not implement <see cref="IEnumerable"/>,
        /// this method compares the <see cref="ItemsSource"/> object to <paramref name="value"/>
        /// and returns 0, if they are equal.
        /// 
        /// If nothing was found, returns -1.
        /// </returns>
        public int IndexOf(object value)
        {
            if (IsUsingItemsSource)
            {
                if (HasEnumerableItemsSource)
                {
                    return _enumerableItemsSource.IndexOf(value);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return _innerCollection.IndexOf(value);
            }
        }

        /// <summary>
        /// Returns a value indicating whether the specified <paramref name="value"/> can be found
        /// inside this collection.
        /// </summary>
        /// <param name="value">
        /// The value to be checked.
        /// </param>
        /// <returns>
        /// true if either the collection, or an <see cref="ItemsSource"/> which implements
        /// <see cref="IEnumerable"/> contains the provided <paramref name="value"/>.
        /// If <see cref="ItemsSource"/> does not implement <see cref="IEnumerable"/>, 
        /// this returns a value indicating whether it equals the specified
        /// <paramref name="value"/>.
        /// </returns>
        public bool Contains(object value)
        {
            if (IsUsingItemsSource)
            {
                if (HasEnumerableItemsSource)
                {
                    return _enumerableItemsSource.Contains(value);
                }
                else
                {
                    return ItemsSource == value;
                }
            }
            else
            {
                return _innerCollection.Contains(value);
            }
        }

        /// <summary>
        /// Copies the elements of this collection to the specified <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="index">
        /// The zero-based index in <paramref name="array"/> from which copying starts.
        /// </param>
        public void CopyTo(Array array, int index)
        {
            if (IsUsingItemsSource)
            {
                if (HasEnumerableItemsSource)
                {
                    _enumerableItemsSource.ToArray().CopyTo(array, index);
                }
                else
                {
                    array.SetValue(ItemsSource, index);
                }
            }
            else
            {
                ((ICollection)_innerCollection).CopyTo(array, index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator which can be used to enumerate the collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> which can be used to enumerate over the
        /// collection.
        /// </returns>
        public IEnumerator<object> GetEnumerator()
        {
            if (IsUsingItemsSource)
            {
                if (HasEnumerableItemsSource)
                {
                    return _enumerableItemsSource.GetEnumerator();
                }
                else
                {
                    return new SingleItemEnumerator(ItemsSource);
                }
            }
            else
            {
                return _innerCollection.GetEnumerator();
            }
        }

        /// <summary>
        /// Adds the specified <paramref name="value"/> to the collection.
        /// </summary>
        /// <param name="value">The value to be added.</param>
        /// <returns>The index, at which the value was added.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="ItemsSource"/> property is set.
        /// </exception>
        public int Add(object value)
        {
            ThrowIfInnerCollectionIsNotWriteable();
            return ((IList)_innerCollection).Add(value);
        }

        /// <summary>
        /// Removes all elements from the collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="ItemsSource"/> property is set.
        /// </exception>
        public void Clear()
        {
            ThrowIfInnerCollectionIsNotWriteable();
            _innerCollection.Clear();
        }

        /// <summary>
        /// Inserts an element into the collection at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index at which the element should be inserted.</param>
        /// <param name="value">The element to be inserted.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="ItemsSource"/> property is set.
        /// </exception>
        public void Insert(int index, object value)
        {
            ThrowIfInnerCollectionIsNotWriteable();
            _innerCollection.Insert(index, value);
        }

        void IList.Remove(object value)
        {
            Remove(value);
        }

        /// <summary>
        /// If found, removes the specified <paramref name="value"/> from the collection.
        /// </summary>
        /// <param name="value">The value to be removed from the collection.</param>
        /// <returns>
        /// true if removing the item succeeded; false if not.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="ItemsSource"/> property is set.
        /// </exception>
        public bool Remove(object value)
        {
            ThrowIfInnerCollectionIsNotWriteable();
            return _innerCollection.Remove(value);
        }

        /// <summary>
        /// Removes the element at the specified <paramref name="index"/> from the collection.
        /// </summary>
        /// <param name="index">The element's index.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="ItemsSource"/> property is set.
        /// </exception>
        public void RemoveAt(int index)
        {
            ThrowIfInnerCollectionIsNotWriteable();
            _innerCollection.RemoveAt(index);
        }

        private void ThrowIfItemsSourceIsNotChangeable()
        {
            // Only allow switching to an ItemsSource, if the underlying collection is empty.
            // Otherwise, items could get lost/we'd have to merge the two sources.
            if (!IsUsingItemsSource && _innerCollection.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Switching to an {nameof(ItemsSource)} failed, because the " +
                    $"items collection wasn't empty.");
            }
        }

        private void ThrowIfItemsSourceIsNotEnumerable()
        {
            if (_enumerableItemsSource == null)
            {
                throw new InvalidOperationException(
                    $"The current operation is not supported, since the currently active " +
                    $"{nameof(ItemsSource)} is not of type {nameof(IEnumerable)}.");
            }
        }

        private void ThrowIfInnerCollectionIsNotWriteable()
        {
            if (IsUsingItemsSource)
            {
                throw new InvalidOperationException(
                    $"Cannot modify the collection directly, while an active " +
                    $"{nameof(ItemsSource)} is provided. " +
                    $"To manipulate the elements in this collection, use the value of the " +
                    $"{nameof(ItemsSource)} property.");
            }
        }

        #region IList<object> members

        int ICollection<object>.Count => Count;

        bool ICollection<object>.IsReadOnly => ((IList)this).IsReadOnly;

        object IList<object>.this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        int IList<object>.IndexOf(object item) => IndexOf(item);

        void IList<object>.Insert(int index, object item) => Insert(index, item);

        void IList<object>.RemoveAt(int index) => RemoveAt(index);

        void ICollection<object>.Add(object item) => Add(item);

        void ICollection<object>.Clear() => Clear();

        bool ICollection<object>.Contains(object item) => Contains(item);

        void ICollection<object>.CopyTo(object[] array, int arrayIndex) 
            => CopyTo(array, arrayIndex);

        bool ICollection<object>.Remove(object item) => Remove(item);

        IEnumerator<object> IEnumerable<object>.GetEnumerator() => GetEnumerator();

        #endregion

        /// <summary>
        /// An enumerator which only enumerates over a single item.
        /// Used to provide enumeration for an ItemsSource which does not implement
        /// IEnumerable.
        /// </summary>
        private struct SingleItemEnumerator : IEnumerator<object>
        {

            // This enumerator can basically do nothing but provide the element with which it
            // has been initialized.

            public object Current { get; }

            public SingleItemEnumerator(object item)
            {
                Current = item;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return false;
            }

            public void Reset()
            {
            }

            public override string ToString()
            {
                return Current?.ToString() ?? "";
            }

        }

    }

}
