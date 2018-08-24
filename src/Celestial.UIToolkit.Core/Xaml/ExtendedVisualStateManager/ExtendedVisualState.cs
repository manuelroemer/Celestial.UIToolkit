using System.Windows;

namespace Celestial.UIToolkit.Xaml
{
    
    /// <summary>
    /// An extension of the <see cref="VisualState"/> class which represents the visual appearance
    /// of the control when it is in a specific state.
    /// This class extends the default <see cref="VisualState"/> with support for setters.
    /// </summary>
    public class ExtendedVisualState : VisualState
    {

        /// <summary>
        /// Gets a collection of <see cref="SetterBase"/> instances which can be used to change
        /// the appearance of <see cref="UIElement"/> objects when this <see cref="VisualState"/>
        /// is active.
        /// </summary>
        public SetterBaseCollection Setters { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedVisualState"/> class.
        /// </summary>
        public ExtendedVisualState()
        {
            Setters = new SetterBaseCollection();
        }

    }

}
