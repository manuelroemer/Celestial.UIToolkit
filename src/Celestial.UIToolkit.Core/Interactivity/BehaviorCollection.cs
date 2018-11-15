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
    public sealed class BehaviorCollection 
        : FreezableCollection<Behavior>, IBehavior, IBehaviorCollection<Behavior>
    {

        private FreezableCollection<Behavior> _validAddedBehaviors;
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
        /// Initializes a new and empty instance of the <see cref="BehaviorCollection"/> class.
        /// </summary>
        public BehaviorCollection()
        {
            _validAddedBehaviors = new FreezableCollection<Behavior>();
            ((INotifyCollectionChanged)this).CollectionChanged += OnCollectionChanged;
        }

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
            foreach (var behavior in _validAddedBehaviors)
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
            
            foreach (var behavior in _validAddedBehaviors)
            {
                behavior.Detach();
            }
            AssociatedObject = null;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WritePreamble();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    OnCollectionReset();
                    break;

                case NotifyCollectionChangedAction.Add:
                    OnCollectionItemsAdded(e.NewItems.Cast<Behavior>());
                    break;

                case NotifyCollectionChangedAction.Remove:
                    OnCollectionItemsRemoved(e.OldItems.Cast<Behavior>());
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (e.NewItems != null)
                    {
                        OnCollectionItemsAdded(e.NewItems.Cast<Behavior>());
                    }
                    if (e.OldItems != null)
                    {
                        OnCollectionItemsRemoved(e.OldItems.Cast<Behavior>());
                    }
                    break;

                default:
                    break;
            }
            WritePostscript();
        }

        private void OnCollectionReset()
        {
            // This is where the snapshot comes into play.
            // When the collection gets reset, we need to detach every single element that was
            // previously added.
            // The problem is that the base collection doesn't provide us with this info.
            // This is why we manually filled the snapshotList.
            foreach (var oldBehavior in _validAddedBehaviors)
            {
                OnBehaviorRemoved(oldBehavior);
            }
            _validAddedBehaviors.Clear();

            OnCollectionItemsAdded(this);
        }
        
        private void OnCollectionItemsAdded(IEnumerable<Behavior> newItems)
        {
            foreach (var addedBehavior in newItems)
            {
                OnBehaviorAdded(addedBehavior);
                _validAddedBehaviors.Insert(IndexOf(addedBehavior), addedBehavior);
            }
        }

        private void OnCollectionItemsRemoved(IEnumerable<Behavior> oldItems)
        {
            foreach (var removedBehavior in oldItems)
            {
                OnBehaviorRemoved(removedBehavior);
                _validAddedBehaviors.Remove(removedBehavior);
            }
        }

        private void OnBehaviorAdded(Behavior behavior)
        {
            if (_validAddedBehaviors.Contains(behavior))
            {
                throw new InvalidOperationException(
                    $"The behavior has already been added to the collection."
                );
                // We are throwing an exception here, but that doesn't stop the item from
                // being added to the FreezableCollection.
                // We also can't call Remove(), since the FreezableCollection then throws an
                // exception too.
                // We won't update the internal Behavior list with the new one though,
                // so all should be good.
            }

            // If this collection is already attached to something, we need to make sure that
            // new elements are automatically attached aswell.
            if (IsAttached)
            {
                behavior.Attach(AssociatedObject);
            }
        }

        private void OnBehaviorRemoved(Behavior behavior)
        {
            // When removed from this collection, we lose control over the behavior.
            // => Detach it now, so that no memory leak occurs later on.
            behavior.Detach();
        }

        /// <summary>
        /// Creates a new <see cref="BehaviorCollection"/> instance.
        /// </summary>
        /// <returns>A new <see cref="BehaviorCollection"/> instance.</returns>
        protected override Freezable CreateInstanceCore() => new BehaviorCollection();
        
        /// <summary>
        ///     Makes this instance a clone (deep copy) of the specified 
        ///     <see cref="BehaviorCollection"/> using base (non-animated) property values.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="BehaviorCollection"/> to copy.
        /// </param>
        protected override void CloneCore(Freezable source)
        {
            base.CloneCore(source);
            var collection = (BehaviorCollection)source;
            collection._validAddedBehaviors = _validAddedBehaviors.Clone();
            collection._associatedObject = _associatedObject;
        }

        /// <summary>
        ///     Makes this instance a clone (deep copy) of the specified 
        ///     <see cref="BehaviorCollection"/> using current property values.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="BehaviorCollection"/> to copy.
        /// </param>
        protected override void CloneCurrentValueCore(Freezable source)
        {
            base.CloneCurrentValueCore(source);
            var collection = (BehaviorCollection)source;
            collection._validAddedBehaviors = _validAddedBehaviors.CloneCurrentValue();
            collection._associatedObject = _associatedObject;
        }

        /// <summary>
        ///     Makes this instance a frozen clone of the specified <see cref="BehaviorCollection"/>
        ///     using base (non-animated) property values.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="BehaviorCollection"/> to copy.
        /// </param>
        protected override void GetAsFrozenCore(Freezable source)
        {
            base.GetAsFrozenCore(source);
            var collection = (BehaviorCollection)source;
            collection._validAddedBehaviors = _validAddedBehaviors.GetAsFrozen() as BehaviorCollection;
            collection._associatedObject = _associatedObject;
        }

        /// <summary>
        ///     Makes this instance a frozen clone of the specified <see cref="BehaviorCollection"/>
        ///     using current property values.
        /// </summary>
        /// <param name="source">
        ///     The <see cref="BehaviorCollection"/> to copy.
        /// </param>
        protected override void GetCurrentValueAsFrozenCore(Freezable source)
        {
            base.GetCurrentValueAsFrozenCore(source);
            var collection = (BehaviorCollection)source;
            collection._validAddedBehaviors = _validAddedBehaviors.GetCurrentValueAsFrozen() as BehaviorCollection;
            collection._associatedObject = _associatedObject;
        }

    }

}
