namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    ///     Represents an <see cref="ITriggerAction"/> whose effects can be reverted after it has
    ///     been executed.
    ///     These actions can be reversed by <see cref="IStatefulTriggerBehavior"/> instances.
    /// </summary>
    public interface IReversibleTriggerAction : ITriggerAction
    {

        /// <summary>
        ///     Reverts the effect of a previously called <see cref="ITriggerAction.Execute(object)"/>
        ///     action call.
        /// </summary>
        /// <param name="parameter">
        ///     A parameter which is passed to the action by the 
        ///     <see cref="IStatefulTriggerBehavior"/> which executed it.
        ///     This can be anything, depending on the trigger.
        /// </param>
        void Revert(object parameter);

    }

}
