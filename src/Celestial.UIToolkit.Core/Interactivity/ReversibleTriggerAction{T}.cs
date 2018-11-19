namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    ///     An abstract base class for <see cref="IReversibleTriggerAction"/> implementers 
    ///     which expects a parameter of a specific type in the 
    ///     <see cref="ITriggerAction.Execute(object)"/> and
    ///     <see cref="IReversibleTriggerAction.Revert(object)"/>
    ///     methods.
    /// </summary>
    /// <typeparam name="TParameter">
    ///     The expected type of the parameter in the <see cref="ITriggerAction.Execute(object)"/>
    ///     and <see cref="IReversibleTriggerAction.Revert(object)"/> methods.
    /// </typeparam>
    public abstract class ReversibleTriggerAction<TParameter> 
        : TriggerAction<TParameter>, IReversibleTriggerAction
    {

        /// <summary>
        ///     Reverts the effect of a previously called <see cref="ITriggerAction.Execute(object)"/>
        ///     action call if the specified <paramref name="parameter"/> is of type
        ///     <typeparamref name="TParameter"/>.
        ///     Otherwise, nothing happens.
        /// </summary>
        /// <param name="parameter">
        ///     A parameter which is passed to the action by the 
        ///     <see cref="IStatefulTriggerBehavior"/> which executed it.
        ///     This can be anything, depending on the trigger.
        /// </param>
        public void Revert(object parameter)
        {
            if (parameter is TParameter || (parameter is null && ExecuteWithNullParameter))
            {
                Revert((TParameter)parameter);
            }
        }

        /// <summary>
        /// Executes the action.
        /// Called by <see cref="Revert(object)"/> when a parameter of type
        /// <typeparamref name="TParameter"/> was passed.
        /// </summary>
        /// <param name="parameter">
        /// A parameter which is passed to the action by the <see cref="ITriggerBehavior"/> which
        /// executed it.
        /// </param>
        protected abstract void Revert(TParameter parameter);

    }

}
