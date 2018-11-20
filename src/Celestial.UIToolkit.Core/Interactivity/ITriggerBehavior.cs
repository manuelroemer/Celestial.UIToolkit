namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// Represents a specialized type of behavior which executes a set of actions, once
    /// a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    public interface ITriggerBehavior : IBehavior
    {

        /// <summary>
        /// Gets a collection of actions which always get executed when the trigger gets triggered.
        /// </summary>
        TriggerActionCollection Actions { get; }

    }

}
