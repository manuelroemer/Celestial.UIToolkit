using System;
using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    ///     A strongly-typed version of the <see cref="StatefulTriggerBehavior"/> class which 
    ///     represents a trigger that knows about whether it is currently active or not.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to which this trigger can be attached.
    /// </typeparam>
    /// <remarks>
    ///     See the <see cref="IStatefulTriggerBehavior"/> remarks for details about the
    ///     difference between the <see cref="ITriggerBehavior"/> and 
    ///     <see cref="IStatefulTriggerBehavior"/> interfaces.
    /// </remarks>
    /// <seealso cref="IStatefulTriggerBehavior"/>
    public abstract class StatefulTriggerBehavior<T> : StatefulTriggerBehavior 
        where T : DependencyObject
    {

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// </summary>
        public new T AssociatedObject => (T)base.AssociatedObject;

        internal override Type RequiredAssociatedObjectType => typeof(T);

    }

}
