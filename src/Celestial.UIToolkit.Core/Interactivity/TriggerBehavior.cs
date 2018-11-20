using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A specialized type of behavior which executes a set of actions once
    /// a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    [ContentProperty(nameof(Actions))]
    public abstract class TriggerBehavior : Behavior, ITriggerBehavior
    {
        
        private static readonly DependencyPropertyKey ActionsPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(Actions),
                typeof(TriggerActionCollection),
                typeof(TriggerBehavior),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="Actions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty =
            ActionsPropertyKey.DependencyProperty;
        
        /// <summary>
        /// Gets a collection of actions which always get executed when the trigger gets triggered.
        /// </summary>
        public TriggerActionCollection Actions
        {
            get
            {
                var collection = (TriggerActionCollection)GetValue(ActionsProperty);
                if (collection == null)
                {
                    collection = new TriggerActionCollection();
                    Actions = collection;
                }
                return collection;
            }
            private set { SetValue(ActionsPropertyKey, value); }
        }
        
        /// <summary>
        ///     When called, activates the trigger and executes the <see cref="Actions"/>.
        /// </summary>
        /// <param name="parameter">
        ///     A parameter to be passed to each action. This can be null.
        /// </param>
        protected void OnTriggered(object parameter)
        {
            OnTriggeredImpl(parameter);
        }

        // This method only exists to allow the StatefulTriggerBehavior to override the
        // Action execution logic.
        // See the StatefulTriggerBehavior class for details on why this is required.
        // 
        // This is a separate method, because OnTriggered(parameter) should not be virtual.
        internal virtual void OnTriggeredImpl(object parameter)
        {
            InteractivitySource.Info(
                GetHashCode(),
                "{0} was triggered.",
                GetType().FullName
            );
            ExecuteAllActions(Actions, parameter);
        }

        internal static void ExecuteAllActions(IList<ITriggerAction> actions, object parameter)
        {
            foreach (var action in actions)
            {
                action.Execute(parameter);
            }
        }

    }

}
