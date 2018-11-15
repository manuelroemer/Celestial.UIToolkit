namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// Represents a specialized type of behavior which executes a set of actions, once
    /// a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    public interface ITrigger : IBehavior
    {

        /// <summary>
        /// Gets a collection of actions which get executed, once a certain condition of this
        /// behavior is met.
        /// </summary>
        ActionCollection Actions { get; }

    }

}
