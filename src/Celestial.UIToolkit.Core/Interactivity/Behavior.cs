﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// An object which can be attached to a <see cref="DependencyObject"/> and thus provide
    /// external behaviors for the associated object.
    /// </summary>
    public abstract class Behavior : Animatable, IBehavior
    {

        private DependencyObject _associatedObject;
        private BehaviorCollection _owningBehaviorCollection;
        private IReadOnlyCollection<Behavior> _owningCollectionView;

        /// <summary>
        /// Gets the object to which this behavior is attached.
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
        /// Internally used to set the <see cref="BehaviorCollection"/> which owns this behavior.
        /// Setting this property automatically updates the read-only <see cref="OwningCollection"/>
        /// property.
        /// </summary>
        internal BehaviorCollection OwningBehaviorCollection
        {
            get
            {
                ReadPreamble();
                return _owningBehaviorCollection;
            }
            set
            {
                ReadPreamble();
                if (_owningBehaviorCollection == value)
                    return;

                if (_owningBehaviorCollection != null && value != null)
                {
                    throw new InvalidOperationException("The behavior is already owned by another collection.");
                }

                WritePreamble();
                _owningBehaviorCollection = value;
                WritePostscript();
            }
        }

        /// <summary>
        /// Gets a read-only view on a <see cref="BehaviorCollection"/> 
        /// which currently owns this behavior.
        /// This property gets set whenever this behavior gets added to a
        /// <see cref="BehaviorCollection"/>.
        /// This can be null, if this <see cref="Behavior"/> has not been added to a collection yet.
        /// </summary>
        protected internal IReadOnlyCollection<Behavior> OwningCollection
        {
            get
            {
                ReadPreamble();
                if (_owningCollectionView == null && _owningBehaviorCollection != null)
                {
                    WritePreamble();
                    _owningCollectionView = new ReadOnlyCollection<Behavior>(_owningBehaviorCollection);
                    WritePostscript();
                }

                return _owningCollectionView;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this behavior is currently attached to an object.
        /// </summary>
        public bool IsAttached => AssociatedObject != null;

        /// <summary>
        /// Gets a type of which an object in the <see cref="Attach(DependencyObject)"/>
        /// must be.
        /// If the object in the <see cref="Attach(DependencyObject)"/> has a different type,
        /// an exception is thrown.
        /// 
        /// This can be overridden by deriving generic classes which require the 
        /// <see cref="AssociatedObject"/> to be of a certain type.
        /// </summary>
        internal virtual Type RequiredAssociatedObjectType => typeof(object);

        /// <summary>
        ///     Tries to create an instance of the current behavior via the 
        ///     <see cref="Activator"/> class.
        ///     If that fails, throws an exception.
        /// </summary>
        /// <returns>
        ///     An instance of the current behavior class.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the deriving class doesn't have a parameterless constructor.
        /// </exception>
        protected override Freezable CreateInstanceCore()
        {
            try
            {
                return (Freezable)Activator.CreateInstance(GetType(), true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Cannot automatically create an instance of the {GetType().FullName} class. " +
                    $"Make sure that your class either has a parameter-less constructor, or that " +
                    $"it overrides the {nameof(CreateInstanceCore)} method.",
                    ex
                );
            }
        }

        /// <summary>
        ///     Attaches this behavior to the specified <paramref name="associatedObject"/>.
        /// </summary>
        /// <param name="associatedObject">
        ///     A <see cref="DependencyObject"/> to which this behavior will be attached.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="associatedObject"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the behavior is already attached to another element.
        /// </exception>
        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject is null)
                throw new ArgumentNullException(nameof(associatedObject));
            
            if (!RequiredAssociatedObjectType.IsAssignableFrom(associatedObject.GetType()))
            {
                throw new InvalidOperationException(
                    $"The behavior can only be attached to objects of type " +
                    $"{RequiredAssociatedObjectType.FullName}, but received an object of type " +
                    $"{associatedObject.GetType().FullName}."
                );
            }

            if (IsAttached)
            {
                if (associatedObject == AssociatedObject)
                {
                    return;
                }
                else
                {
                    throw new InvalidOperationException(
                        "A Behavior can only be attached to a single object at a time."
                    );
                }
            }

            AssociatedObject = associatedObject;
            OnAttached();

            InteractivitySource.Verbose(
                GetHashCode(),
                "{0} was attached to object \"{1}\".", 
                GetType().FullName,
                associatedObject
            );
        }
        
        /// <summary>
        /// Detaches the behavior from the current associated object.
        /// </summary>
        public void Detach()
        {
            if (!IsAttached)
                return;
            OnDetaching();
            AssociatedObject = null;

            InteractivitySource.Verbose(
                GetHashCode(),
                "{0} was detached.",
                GetType().FullName
            );
        }

        /// <summary>
        /// Called when the behavior was attached to an associated object.
        /// Can be overridden to initialize functionality regarding the associated object
        /// (e.g. setting up event handlers).
        /// </summary>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Called when the behavior is about to be detached from the current 
        /// <see cref="AssociatedObject"/>.
        /// At this point in time, the <see cref="AssociatedObject"/> still holds a value.
        /// Can be overridden to un-initialize functionality which was previously setup in the
        /// <see cref="OnAttached"/> method (e.g. removing event handlers).
        /// </summary>
        protected virtual void OnDetaching()
        {
        }

        /// <summary>
        /// Returns a string representation of the current behavior.
        /// </summary>
        /// <returns>A string representing this behavior.</returns>
        public override string ToString()
        {
            string result = $"{GetType().Name}: " +
                            $"{nameof(IsAttached)}: {IsAttached}";

            if (IsAttached)
            {
                result += $", {nameof(AssociatedObject)}: {AssociatedObject}";
            }
            return result;
        }

    }
}
