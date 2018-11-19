using System;
using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A strongly-typed version of the <see cref="TriggerBehavior"/> class which executes a set of 
    /// actions, once a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    /// <typeparam name="T">
    /// The type of objects to which this trigger can be attached.
    /// </typeparam>
    public abstract class TriggerBehavior<T> : TriggerBehavior where T : DependencyObject
    {

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// </summary>
        public new T AssociatedObject => (T)base.AssociatedObject;

        internal override Type RequiredAssociatedObjectType => typeof(T);

    }

}
