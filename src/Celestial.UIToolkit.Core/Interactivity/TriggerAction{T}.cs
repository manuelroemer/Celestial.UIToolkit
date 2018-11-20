using System.Windows;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    ///     An abstract base class for <see cref="ITriggerAction"/> implementers which expects
    ///     a parameter of a specific type in the <see cref="ITriggerAction.Execute(object)"/>
    ///     method.
    ///     Override <see cref="AllowNullParameter"/> to control null value behavior.
    /// </summary>
    /// <typeparam name="TParameter">
    ///     The expected type of the parameter in the <see cref="ITriggerAction.Execute(object)"/>
    ///     method.
    /// </typeparam>
    public abstract class TriggerAction<TParameter> : DependencyObject, ITriggerAction
    {

        /// <summary>
        /// Gets a value indicating whether the action gets executed when the trigger
        /// passes null as a parameter.
        /// The default value is <c>false</c>.
        /// </summary>
        protected virtual bool AllowNullParameter { get; } = false;
        // Always keep the above as read-only and virtual, so that deriving classes can seal it off.

        /// <summary>
        /// Executes the action, if the specified <paramref name="parameter"/> is of type 
        /// <typeparamref name="TParameter"/>.
        /// Otherwise, nothing happens.
        /// </summary>
        /// <param name="parameter">
        /// A parameter which is passed to the action by the <see cref="ITriggerBehavior"/> which
        /// executed it.
        /// </param>
        public void Execute(object parameter)
        {
            if (parameter is TParameter || (parameter is null && AllowNullParameter))
            {
                InteractivitySource.Verbose(
                    GetHashCode(),
                    "Executing trigger action {0} with parameter {1}.",
                    GetType().FullName,
                    parameter
                );
                Execute((TParameter)parameter);
            }
            else
            {
                InteractivitySource.Verbose(
                    GetHashCode(),
                    "Not executing trigger action {0} because of invalid parameter: {1}.",
                    GetType().FullName,
                    parameter
                );
            }
        }

        /// <summary>
        /// Executes the action.
        /// Called by <see cref="Execute(object)"/> when a parameter of type
        /// <typeparamref name="TParameter"/> was passed.
        /// </summary>
        /// <param name="parameter">
        /// A parameter which is passed to the action by the <see cref="ITriggerBehavior"/> which
        /// executed it.
        /// </param>
        protected abstract void Execute(TParameter parameter);
        
    }

}
