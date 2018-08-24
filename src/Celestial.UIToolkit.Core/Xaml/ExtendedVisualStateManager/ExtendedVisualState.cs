using System.Windows;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Represents a <see cref="VisualState"/> which can decide if its state specific values
    /// should be applied or not.
    /// </summary>
    public interface IActivatableVisualState
    {

        /// <summary>
        /// Gets a value indicating whether the <see cref="VisualState"/> wants to apply its values.
        /// If so, its values will be applied.
        /// If not, it will be ignored by the <see cref="ExtendedVisualState"/>, even though
        /// the state is currently active.
        /// </summary>
        bool IsActive { get; }
    }

    /// <summary>
    /// An extension of the <see cref="VisualState"/> class which represents the visual appearance
    /// of the control when it is in a specific state.
    /// This class extends the default <see cref="VisualState"/> with support for setters.
    /// </summary>
    public class ExtendedVisualState : VisualState, IActivatableVisualState
    {

        /// <summary>
        /// Gets a collection of <see cref="SetterBase"/> instances which can be used to change
        /// the appearance of <see cref="UIElement"/> objects when this <see cref="VisualState"/>
        /// is active.
        /// </summary>
        public SetterBaseCollection Setters { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="VisualState"/> wants to apply its values.
        /// If so, its values will be applied.
        /// If not, it will be ignored by the <see cref="ExtendedVisualState"/>, even though
        /// the state is currently active.
        /// </summary>
        public bool IsActive => true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedVisualState"/> class.
        /// </summary>
        public ExtendedVisualState()
        {
            Setters = new SetterBaseCollection();
        }

    }

}
