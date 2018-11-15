using System;
using System.Collections.ObjectModel;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A collection of <see cref="ITriggerAction"/> instances.
    /// </summary>
    /// <remarks>
    /// This collection throws if null values are added to it.
    /// </remarks>
    public sealed class TriggerActionCollection : Collection<ITriggerAction>
    {

        /// <summary>
        /// Inserts an <see cref="ITriggerAction"/> into the collection.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="item">The action to insert.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is null.
        /// </exception>
        protected override void InsertItem(int index, ITriggerAction item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Sets an <see cref="ITriggerAction"/> at the specified position.
        /// </summary>
        /// <param name="index">The index at which to set.</param>
        /// <param name="item">The action to set.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is null.
        /// </exception>
        protected override void SetItem(int index, ITriggerAction item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item)); 
            base.SetItem(index, item);
        }

    }

}
