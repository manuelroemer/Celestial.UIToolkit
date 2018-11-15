using System;
using System.Collections.ObjectModel;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A collection of <see cref="IAction"/> instances.
    /// </summary>
    /// <remarks>
    /// This collection throws if null values are added to it.
    /// </remarks>
    public sealed class ActionCollection : Collection<IAction>
    {

        /// <summary>
        /// Inserts an <see cref="IAction"/> into the collection.
        /// </summary>
        /// <param name="index">The index at which to insert.</param>
        /// <param name="item">The action to insert.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is null.
        /// </exception>
        protected override void InsertItem(int index, IAction item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Sets an <see cref="IAction"/> at the specified position.
        /// </summary>
        /// <param name="index">The index at which to set.</param>
        /// <param name="item">The action to set.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is null.
        /// </exception>
        protected override void SetItem(int index, IAction item)
        {
            base.SetItem(index, item);
        }

    }

}
