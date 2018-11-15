namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// An abstract base class for <see cref="ITriggerAction"/> implementers which expect
    /// a parameter of a specific type in the <see cref="ITriggerAction.Execute(object)"/>
    /// method.
    /// Override <see cref="ExecuteWithNullParameter"/> to control null value behavior.
    /// </summary>
    /// <typeparam name="TParameter">
    /// The expected type of the parameter in the <see cref="ITriggerAction.Execute(object)"/>
    /// method.
    /// </typeparam>
    public abstract class TriggerAction<TParameter> : ITriggerAction
    {

        /// <summary>
        /// Gets a value indicating whether the action gets executed when the trigger
        /// passes null as a parameter.
        /// The default value is <c>false</c>.
        /// </summary>
        protected virtual bool ExecuteWithNullParameter { get; } = false;

        /// <summary>
        /// Executes the action, if the specified parameter is of type 
        /// <typeparamref name="TParameter"/>.
        /// Otherwise, nothing happens.
        /// </summary>
        /// <param name="parameter">
        /// A parameter which is passed to the action by the <see cref="ITrigger"/> which
        /// executed it.
        /// </param>
        public void Execute(object parameter)
        {
            if (parameter is TParameter || (parameter is null && ExecuteWithNullParameter))
            {
                Execute((TParameter)parameter);
            }
        }

        /// <summary>
        /// Executes the action.
        /// Called by <see cref="Execute(object)"/> when a parameter of type
        /// <typeparamref name="TParameter"/> was passed.
        /// </summary>
        /// <param name="parameter">
        /// A parameter which is passed to the action by the <see cref="ITrigger"/> which
        /// executed it.
        /// </param>
        protected abstract void Execute(TParameter parameter);

    }

}
