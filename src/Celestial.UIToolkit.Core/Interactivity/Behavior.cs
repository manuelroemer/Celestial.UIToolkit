using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// An object which can be attached to a <see cref="DependencyObject"/> and thus provide
    /// external behaviors for the associated object.
    /// </summary>
    public abstract class Behavior : Animatable, IBehavior
    {

        private DependencyObject _associatedObject;

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
        /// Gets a value indicating whether this behavior is currently attached to an object.
        /// </summary>
        public bool IsAttached => AssociatedObject != null;

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
            AttachImpl(associatedObject);
        }
        
        // Contains the actual logic for attaching an object to this behavior.
        // This is virtual, so that the Behavior<T> class can
        // add/override the logic with type-specific logic.
        // 
        // By implementing it like this, Behavior<T> doesn't have to override OnAttached,
        // which allows users of the generic behavior class to not call base.OnAttached().
        internal virtual void AttachImpl(DependencyObject associatedObject)
        {
            if (associatedObject is null)
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
                        "A Behavior can only be attached to a single object at a time."
                    );
                }
            }

            AssociatedObject = associatedObject;
            OnAttached();
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
