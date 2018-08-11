using System;
using System.Collections;
using System.Collections.Generic;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An abstract base class, designed to hold <see cref="KeyFrameBase{T}"/> elements.
    /// </summary>
    /// <typeparam name="T">The type of the key frame's values.</typeparam>
    public abstract class KeyFrameCollectionBase<T> : ExtendedFreezable, IList<KeyFrameBase<T>>, IList
    {

        private List<KeyFrameBase<T>> _keyFrames;

        #region Freezable



        #endregion

        #region IList<T>

        /// <summary>
        /// Gets the total number of key frame elements inside this collection.
        /// </summary>
        public int Count => this.EnterReadScope(() => _keyFrames.Count);

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
        public object SyncRoot => this.EnterReadScope(() => ((ICollection)_keyFrames).SyncRoot);

        /// <summary>
        /// Gets a value indicating whether access to the collection is synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized => this.EnterReadScope(() => this.IsFrozen ? true : this.Dispatcher != null);

        /// <summary>
        /// Gets or sets the key frame at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the key frame to get or set.</param>
        /// <returns>The key frame at the specified <paramref name="index"/>.</returns>
        public KeyFrameBase<T> this[int index]
        {
            get
            {
                return this.EnterReadScope(() => _keyFrames[index]);
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                this.EnterWriteScope(() =>
                {
                    this.OnFreezablePropertyChanged(_keyFrames[index], value);
                    _keyFrames[index] = value;
                });
            }
        }

        /// <summary>
        /// Determines whether the collection contains a specific key frame.
        /// </summary>
        /// <param name="item">The key frame to locate in the collection.</param>
        /// <returns>
        /// <c>true</c> if the key frame is found in the collection;
        /// <c>false</c> if not.
        /// </returns>
        public bool Contains(KeyFrameBase<T> item)
        {
            return this.EnterReadScope(() => _keyFrames.Contains(item));
        }

        /// <summary>
        /// Copies the key frames of the collection into an array,
        /// starting at <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the
        /// key frames copied from the collection.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in the array, at which copying begins.
        /// </param>
        public void CopyTo(KeyFrameBase<T>[] array, int arrayIndex)
        {
            this.EnterReadScope(() => _keyFrames.CopyTo(array, arrayIndex));
        }

        /// <summary>
        /// Determines the index of the specified key frame in the collection.
        /// </summary>
        /// <param name="item">The key frame to locate in this collection.</param>
        /// <returns>The index of the key frame if found in the collection; -1 otherwise.</returns>
        public int IndexOf(KeyFrameBase<T> item)
        {
            return this.EnterReadScope(() => _keyFrames.IndexOf(item));
        }

        /// <summary>
        /// Adds a key frame to the collection.
        /// </summary>
        /// <param name="item">The key frame to be added to the collection.</param>
        public void Add(KeyFrameBase<T> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            this.EnterWriteScope(() =>
            {
                this.OnFreezablePropertyChanged(null, item);
                _keyFrames.Add(item);
            });
        }

        /// <summary>
        /// Inserts a key frame to to the collection at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which the key frame should be inserted.</param>
        /// <param name="item">The key frame to insert into the collection.</param>
        public void Insert(int index, KeyFrameBase<T> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (index < 0 || index > _keyFrames.Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index));

            this.EnterWriteScope(() =>
            {
                this.OnFreezablePropertyChanged(null, item);
                _keyFrames.Insert(index, item);
            });
        }

        /// <summary>
        /// Inserts a key frame at the end of the collection and returns the index,
        /// at which the key frame was inserted.
        /// </summary>
        /// <param name="item">The key frame to be inserted.</param>
        /// <returns>The index at which the key frame was inserted.</returns>
        public int InsertEnd(KeyFrameBase<T> item)
        {
            return this.EnterReadScope(() =>
            {
                int index = this.Count - 1;
                this.Insert(index, item);
                return index;
            });
        }

        /// <summary>
        /// Removes the first occurrence of a specified key frame from the collection.
        /// </summary>
        /// <param name="item">The key frame to be removed from the collection.</param>
        /// <returns>
        /// <c>true</c> if the key frame was successfully removed from the collection;
        /// <c>false</c> if not.
        /// </returns>
        public bool Remove(KeyFrameBase<T> item)
        {
            return this.EnterWriteScope(() =>
            {
                if (_keyFrames.Contains(item))
                {
                    this.OnFreezablePropertyChanged(item, null);
                    return _keyFrames.Remove(item);
                }
                else
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Removes the key frame at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index > _keyFrames.Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index));

            this.EnterWriteScope(() =>
            {
                this.OnFreezablePropertyChanged(_keyFrames[index], null);
                _keyFrames.RemoveAt(index);
            });
        }

        /// <summary>
        /// Removes all key frames from the collection.
        /// </summary>
        public void Clear()
        {
            this.EnterWriteScope(() =>
            {
                for (int i = 0; i < _keyFrames.Count; i++)
                {
                    this.OnFreezablePropertyChanged(_keyFrames[i], null);
                }
                _keyFrames.Clear();
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
        public IEnumerator<KeyFrameBase<T>> GetEnumerator()
        {
            return this.EnterReadScope(() => _keyFrames.GetEnumerator());
        }

        #endregion

        #region IList

        object IList.this[int index]
        {
            get => this[index];
            set => this[index] = (KeyFrameBase<T>)value;
        }

        int IList.Add(object value) => this.InsertEnd((KeyFrameBase<T>)value);

        bool IList.Contains(object value) => this.Contains((KeyFrameBase<T>)value);

        void IList.Clear() => this.Clear();

        int IList.IndexOf(object value) => this.IndexOf((KeyFrameBase<T>)value);

        void IList.Insert(int index, object value) => this.Insert(index, (KeyFrameBase<T>)value);

        void IList.Remove(object value) => this.Remove((KeyFrameBase<T>)value);

        void IList.RemoveAt(int index) => this.RemoveAt(index);

        void ICollection.CopyTo(Array array, int index) => this.CopyTo((KeyFrameBase<T>[])array, index);

        #endregion

    }

}
