using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// A freezable collection of other <see cref="Freezable"/> children.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the children which are stored inside this collection.
    /// </typeparam>
    public class FreezableCollection<T> : ExtendedFreezable, IList<T>, IList
        where T : Freezable
    {

        private List<T> _items;

        /// <summary>
        /// Initializes a new, empty instance of the <see cref="FreezableCollection{T}"/> class.
        /// </summary>
        public FreezableCollection()
            : this(4) { }

        /// <summary>
        /// Initializes a new, empty instance of the <see cref="FreezableCollection{T}"/> class
        /// with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the collection.</param>
        public FreezableCollection(int capacity)
        {
            _items = new List<T>(capacity);
        }
        
        #region Freezable

        /// <summary>
        /// Creates a new instance of the <see cref="FreezableCollection{T}"/> class.
        /// </summary>
        /// <returns>A new <see cref="FreezableCollection{T}"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new FreezableCollection<T>();

        /// <summary>
        /// Makes the collection unmodifiable or tests whether it can
        /// be made unmodifiable.
        /// </summary>
        /// <param name="isChecking">
        /// <c>true</c> to return an indication of whether the object can be frozen (without actually
        /// freezing it); <c>false</c> to actually freeze the object.
        /// </param>
        /// <returns>
        /// If <paramref name="isChecking"/> is <c>true</c>, this method returns 
        /// <c>true</c> if the <see cref="Freezable"/> can be made unmodifiable, 
        /// or <c>false</c> if it cannot be made unmodifiable. 
        /// 
        /// If <paramref name="isChecking"/> is <c>false</c>, this method returns
        /// <c>true</c> if the specified <see cref="Freezable"/>
        /// is now unmodifiable, or <c>false</c> if it cannot be made unmodifiable.
        /// </returns>
        protected override bool FreezeCore(bool isChecking)
        {
            bool result = base.FreezeCore(isChecking);
            for (int i = 0; i < _items.Count; i++)
            {
                result &= Freeze(_items[i], isChecking);
            }

            return result;
        }

        /// <summary>
        /// Makes this instance a clone (deep copy) of the specified <paramref name="sourceFreezable"/>
        /// using base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to clone.</param>
        protected override void CloneCore(Freezable sourceFreezable)
        {
            var srcCollection = (FreezableCollection<T>)sourceFreezable;
            base.CloneCore(sourceFreezable);
            this.CloneCollectionMembers(srcCollection, (item) => item.Clone());
        }

        /// <summary>
        /// Makes the instance a modifiable clone (deep copy) of the specified <paramref name="sourceFreezable"/>
        /// using current property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to be cloned.</param>
        protected override void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            var srcCollection = (FreezableCollection<T>)sourceFreezable;
            base.CloneCurrentValueCore(sourceFreezable);
            this.CloneCollectionMembers(srcCollection, (item) => item.CloneCurrentValue());
        }

        /// <summary>
        /// Makes the instance a frozen clone of the specified <see cref="Freezable"/> using
        /// base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to copy.</param>
        protected override void GetAsFrozenCore(Freezable sourceFreezable)
        {
            var srcCollection = (FreezableCollection<T>)sourceFreezable;
            base.GetAsFrozenCore(sourceFreezable);
            this.CloneCollectionMembers(srcCollection, (item) => item.GetAsFrozen());
        }

        /// <summary>
        /// Makes the instance a frozen clone of the specified <see cref="Freezable"/> using
        /// base (non-animated) property values.
        /// </summary>
        /// <param name="sourceFreezable">The object to copy.</param>
        protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            var srcCollection = (FreezableCollection<T>)sourceFreezable;
            base.GetCurrentValueAsFrozenCore(sourceFreezable);
            this.CloneCollectionMembers(srcCollection, (item) => item.GetCurrentValueAsFrozen());
        }

        private void CloneCollectionMembers(
            FreezableCollection<T> srcCollection, Func<Freezable, Freezable> cloneMemberFunc)
        {
            var srcItems = srcCollection._items;
            _items = new List<T>();

            for (int i = 0; i < srcItems.Count; i++)
            {
                var item = (T)cloneMemberFunc(srcItems[i]);
                _items.Add(item);
                this.OnFreezablePropertyChanged(null, item);
            }
        }

        #endregion

        #region IList<T>

        /// <summary>
        /// Gets the total number of items inside this collection.
        /// </summary>
        public int Count => this.EnterReadScope(() => _items.Count);

        /// <summary>
        /// Gets a value indicating whether this collection is read-only,
        /// which is the case, if it is frozen.
        /// </summary>
        public bool IsReadOnly => this.EnterReadScope(() => this.IsFrozen);

        /// <summary>
        /// Gets a value indicating whether the collection has a fixed size.
        /// </summary>
        public bool IsFixedSize => this.EnterReadScope(() => this.IsFrozen);

        /// <summary>
        /// Gets an object that can be used to synchronize access to the collection.
        /// </summary>
        public object SyncRoot => this.EnterReadScope(() => ((ICollection)_items).SyncRoot);

        /// <summary>
        /// Gets a value indicating whether access to the collection is synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized => this.EnterReadScope(() => this.IsFrozen ? true : this.Dispatcher != null);

        /// <summary>
        /// Gets or sets the item at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get or set.</param>
        /// <returns>The item at the specified <paramref name="index"/>.</returns>
        public T this[int index]
        {
            get
            {
                return this.EnterReadScope(() => _items[index]);
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                this.EnterWriteScope(() =>
                {
                    this.OnFreezablePropertyChanged(_items[index], value);
                    _items[index] = value;
                });
            }
        }

        /// <summary>
        /// Determines whether the collection contains a specific items.
        /// </summary>
        /// <param name="item">The item to locate in the collection.</param>
        /// <returns>
        /// <c>true</c> if the item is found in the collection;
        /// <c>false</c> if not.
        /// </returns>
        public bool Contains(T item)
        {
            return this.EnterReadScope(() => _items.Contains(item));
        }

        /// <summary>
        /// Copies the items of the collection into an array,
        /// starting at <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the
        /// items copied from the collection.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in the array, at which copying begins.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.EnterReadScope(() => _items.CopyTo(array, arrayIndex));
        }

        /// <summary>
        /// Determines the index of the specified item in the collection.
        /// </summary>
        /// <param name="item">The item to locate in this collection.</param>
        /// <returns>The index of the item if found in the collection; -1 otherwise.</returns>
        public int IndexOf(T item)
        {
            return this.EnterReadScope(() => _items.IndexOf(item));
        }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The item to be added to the collection.</param>
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            this.EnterWriteScope(() =>
            {
                this.OnFreezablePropertyChanged(null, item);
                _items.Add(item);
            });
        }

        /// <summary>
        /// Inserts an item to to the collection at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert into the collection.</param>
        public void Insert(int index, T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (index < 0 || index > _items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            this.EnterWriteScope(() =>
            {
                this.OnFreezablePropertyChanged(null, item);
                _items.Insert(index, item);
            });
        }

        /// <summary>
        /// Inserts an item at the end of the collection and returns the index,
        /// at which the item was inserted.
        /// </summary>
        /// <param name="item">The item to be inserted.</param>
        /// <returns>The index at which the item was inserted.</returns>
        public int InsertEnd(T item)
        {
            return this.EnterReadScope(() =>
            {
                int index = this.Count;
                this.Insert(index, item);
                return index;
            });
        }

        /// <summary>
        /// Removes the first occurrence of a specified item from the collection.
        /// </summary>
        /// <param name="item">The item to be removed from the collection.</param>
        /// <returns>
        /// <c>true</c> if the item was successfully removed from the collection;
        /// <c>false</c> if not.
        /// </returns>
        public bool Remove(T item)
        {
            return this.EnterWriteScope(() =>
            {
                if (_items.Contains(item))
                {
                    this.OnFreezablePropertyChanged(item, null);
                    return _items.Remove(item);
                }
                else
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > _items.Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index));

            this.EnterWriteScope(() =>
            {
                this.OnFreezablePropertyChanged(_items[index], null);
                _items.RemoveAt(index);
            });
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            this.EnterWriteScope(() =>
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    this.OnFreezablePropertyChanged(_items[i], null);
                }
                _items.Clear();
            });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.EnterReadScope(() => _items.GetEnumerator());
        }

        #endregion

        #region IList

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (T)value;
        }

        int IList.Add(object value) => this.InsertEnd((T)value);

        bool IList.Contains(object value) => this.Contains((T)value);

        void IList.Clear() => this.Clear();

        int IList.IndexOf(object value) => this.IndexOf((T)value);

        void IList.Insert(int index, object value) => this.Insert(index, (T)value);

        void IList.Remove(object value) => this.Remove((T)value);

        void IList.RemoveAt(int index) => this.RemoveAt(index);

        void ICollection.CopyTo(Array array, int index) => this.CopyTo((T[])array, index);

        #endregion

    }

}
