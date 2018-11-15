using System;
using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A strongly-typed version of the <see cref="Trigger"/> class which executes a set of 
    /// actions, once a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to which this trigger can be attached.
    /// </typeparam>
    public abstract class Trigger<T> : Trigger where T : DependencyObject
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
                    $"The trigger can only be attached to objects of type {typeof(T).FullName}, " +
                    $"but received an object of type {associatedObject.GetType().FullName}."
                );
            }
            base.AttachImpl(associatedObject);
        }

    }

}
