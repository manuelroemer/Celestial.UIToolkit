using System.Collections.Generic;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// Represents a collection of <see cref="IBehavior"/> instances which are all attached to the
    /// same object.
    /// When this collection is attached to an object, all items are automatically attached
    /// to the element aswell.
    /// </summary>
    public interface IBehaviorCollection<T> : IList<T>, IBehavior where T : IBehavior
    {
    }

}
