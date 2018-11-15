using System;
using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A strongly-typed version of the <see cref="Behavior"/> class, which attaches to
    /// <see cref="DependencyObject"/> instances of type <typeparamref name="T"/> and extends
    /// them with external functionality.
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to which this behavior can be attached.
    /// </typeparam>
    public abstract class Behavior<T> : Behavior where T : DependencyObject
    {

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// </summary>
        public new T AssociatedObject => (T)base.AssociatedObject;

        internal sealed override void AttachImpl(DependencyObject associatedObject)
        {
            if (!(associatedObject is null) && !(associatedObject is T))
            {
                throw new InvalidOperationException(
                    $"The behavior can only be attached to objects of type {typeof(T).FullName}, " +
                    $"but received an object of type {associatedObject.GetType().FullName}."
                );
            }
            base.AttachImpl(associatedObject);
        }

    }

}
