using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// Represents an object which can be attached to a <see cref="DependencyObject"/>
    /// and thus provide external behaviors for the associated object.
    /// </summary>
    public interface IBehavior
    {

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// </summary>
        DependencyObject AssociatedObject { get; }

        /// <summary>
        /// Attaches this behavior to the specified <paramref name="associatedObject"/>.
        /// </summary>
        /// <param name="associatedObject">
        /// A <see cref="DependencyObject"/> to which this behavior will be attached.
        /// </param>
        void Attach(DependencyObject associatedObject);

        /// <summary>
        /// Detaches this behavior from the current associated object.
        /// </summary>
        void Detach();

    }

}
