using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    // When editing this file, please note that this class inherits from FreezableCollection
    // and thus is a Freezable object.
    // Don't forget about the Freezable implementation details!


    /// <summary>
    /// A collection of <see cref="IBehavior"/> instances which are all attached to the same
    /// object.
    /// When this collection is attached to an object, all items are automatically attached
    /// to the element aswell.
    /// </summary>
    public sealed class BehaviorCollection<T> : FreezableCollection<T>, IBehavior 
        where T : DependencyObject, IBehavior
    {

        private readonly List<T> _snapshotList = new List<T>();
        private DependencyObject _associatedObject;

        /// <summary>
        /// Gets the object to which this behavior collection and all of its items are attached.
        /// </summary>
        public DependencyObject AssociatedObject
        {
            get
            {
                ReadPreamble();
                return _associatedObject;
            }
            private set
            {
                WritePreamble();
                _associatedObject = value;
                WritePostscript();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this behavior collection 
        /// is currently attached to an object.
        /// </summary>
        public bool IsAttached
        {
            get
            {
                ReadPreamble();
                return AssociatedObject != null;
            }
        }

        /// <summary>
        /// Initializes a new and empty instance of the <see cref="BehaviorCollection{T}"/> class.
        /// </summary>
        public BehaviorCollection()
        {
            ((INotifyCollectionChanged)this).CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Creates a new <see cref="BehaviorCollection{T}"/> instance.
        /// </summary>
        /// <returns>A new <see cref="BehaviorCollection{T}"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BehaviorCollection<T>();

        /// <summary>
        ///     Attaches this behavior collection and all of its items
        ///     to the specified <paramref name="associatedObject"/>.
        /// </summary>
        /// <param name="associatedObject">
        ///     A <see cref="DependencyObject"/> to which this behavior will be attached.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="associatedObject"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the behavior collection or one of its items
        ///     is already attached to another element.
        /// </exception>
        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject == null)
                throw new ArgumentNullException(nameof(associatedObject));

            if (IsAttached)
            {
                if (associatedObject == AssociatedObject)
                {
                    return;
                }
                else
                {
                    throw new InvalidOperationException(
                        "A BehaviorCollection can only be attached to a single object at a time."
                    );
                }
            }

            // I don't know the reason for this, but both the XamlBehaviors, aswell as 
            // the legacy System.Windows.Interactivity DLLs don't load in design mode.
            // I mean, there must be a reason for this, so let's follow their example.
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            AssociatedObject = associatedObject;
            foreach (T behavior in this)
            {
                // This could potentially throw, if the behavior is already attached to something.
                // This is wanted behavior though.
                behavior.Attach(associatedObject);
            }
        }

        /// <summary>
        /// Detaches the behavior collection and all of its items from the current 
        /// associated object.
        /// </summary>
        public void Detach()
        {
            if (!IsAttached)
                return;
            

            foreach (T behavior in this)
            {
                behavior.Detach();
            }
            AssociatedObject = null;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnCollectionItemsAdded(e.NewItems.Cast<T>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    OnCollectionItemsRemoved(e.OldItems.Cast<T>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (e.NewItems != null)
                    {
                        OnCollectionItemsAdded(e.NewItems.Cast<T>());
                    }
                    if (e.OldItems != null)
                    {
                        OnCollectionItemsRemoved(e.OldItems.Cast<T>());
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    OnCollectionReset();
                    break;

                default:
                    break;
            }
        }

        private void OnCollectionItemsAdded(IEnumerable<T> newItems)
        {
            foreach (T addedBehavior in newItems)
            {
                OnBehaviorAdded(addedBehavior);
                _snapshotList.Insert(IndexOf(addedBehavior), addedBehavior);
            }
        }

        private void OnCollectionItemsRemoved(IEnumerable<T> oldItems)
        {
            foreach (T removedBehavior in oldItems)
            {
                OnBehaviorRemoved(removedBehavior);
                _snapshotList.Remove(removedBehavior);
            }
        }

        private void OnCollectionReset()
        {
            // This is where the snapshot comes into play.
            // When the collection gets reset, we need to detach every single element that was
            // previously added.
            // The problem is that the base collection doesn't provide us with this info.
            // This is why we manually filled the snapshotList.
            OnCollectionItemsRemoved(_snapshotList);
            OnCollectionItemsAdded(this);
        }
        
        private void OnBehaviorAdded(T behavior)
        {
            if (_snapshotList.Contains(behavior))
            {
                throw new InvalidOperationException(
                    $"The behavior has already been added to the collection."
                );
            }

            // If this collection is already attached to something, we need to make sure that
            // new elements are automatically attached aswell.
            if (IsAttached)
            {
                behavior.Attach(AssociatedObject);
            }
        }

        private void OnBehaviorRemoved(T behavior)
        {
            // When removed from this collection, we lose control over the behavior.
            // => Detach it now, so that no memory leak occurs later on.
            behavior.Detach();
        }

    }

}
