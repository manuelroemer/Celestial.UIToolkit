namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    ///     A specialization of the <see cref="ITriggerBehavior"/> interface which represents a
    ///     trigger that knows about whether it is currently active or not.
    /// </summary>
    /// <remarks>
    ///     In comparison to the default <see cref="ITriggerBehavior"/> triggers (which are
    ///     basically fire-and-forget triggers), a stateful trigger is active over a longer
    ///     period of time.
    ///     
    ///     When released, it is able to revert actions that were previously executed.
    ///     This allows certain states that are set by actions to only be active over 
    ///     a certain amount of time.
    ///     
    ///     To allow your actions to be reversible by this type of trigger, they must implement
    ///     the <see cref="IReversibleTriggerAction"/> interface.
    ///     If an action is not reversible, the same effect can potentially be achieved by
    ///     using the <see cref="EnterActions"/> and <see cref="ExitActions"/> properties.
    /// </remarks>
    public interface IStatefulTriggerBehavior : ITriggerBehavior
    {

        /// <summary>
        /// Gets a value indicating whether the trigger is currently active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets a collection of actions which get executed when the trigger becomes active.
        /// </summary>
        TriggerActionCollection EnterActions { get; }

        /// <summary>
        /// Gets a collection of actions which get executed when the trigger becomes inactive 
        /// again.
        /// </summary>
        TriggerActionCollection ExitActions { get; }

    }

}
