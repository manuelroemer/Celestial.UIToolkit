namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// Represents an arbitrary action which gets executed by an <see cref="ITriggerBehavior"/>.
    /// </summary>
    public interface ITriggerAction
    {

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="parameter">
        /// A parameter which is passed to the action by the <see cref="ITriggerBehavior"/> which
        /// executed it.
        /// This can be anything, depending on the trigger.
        /// </param>
        void Execute(object parameter);

    }

}
