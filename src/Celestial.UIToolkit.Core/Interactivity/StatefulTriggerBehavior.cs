using System.Collections.Generic;
using System.Windows;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    ///     A specialization of the <see cref="TriggerBehavior"/> class which represents a
    ///     trigger that knows about whether it is currently active or not.
    /// </summary>
    /// <remarks>
    ///     See the <see cref="IStatefulTriggerBehavior"/> remarks for details about the
    ///     difference between the <see cref="ITriggerBehavior"/> and 
    ///     <see cref="IStatefulTriggerBehavior"/> interfaces.
    /// </remarks>
    /// <seealso cref="IStatefulTriggerBehavior"/>
    public abstract class StatefulTriggerBehavior : TriggerBehavior, IStatefulTriggerBehavior
    {

        private static readonly DependencyPropertyKey IsActivePropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(IsActive),
                typeof(bool),
                typeof(StatefulTriggerBehavior),
                new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="IsActive"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            IsActivePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey EnterActionsPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(EnterActions),
                typeof(TriggerActionCollection),
                typeof(StatefulTriggerBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="EnterActions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EnterActionsProperty =
            EnterActionsPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey ExitActionsPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(ExitActions),
                typeof(TriggerActionCollection),
                typeof(StatefulTriggerBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="ExitActions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExitActionsProperty =
            ExitActionsPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a value indicating whether the trigger is currently active.
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            private set { SetValue(IsActivePropertyKey, value); }
        }

        /// <summary>
        /// Gets a collection of actions which get executed when the trigger becomes active.
        /// </summary>
        public TriggerActionCollection EnterActions
        {
            get
            {
                var collection = (TriggerActionCollection)GetValue(EnterActionsProperty);
                if (collection == null)
                {
                    collection = new TriggerActionCollection();
                    EnterActions = collection;
                }
                return collection;
            }
            private set { SetValue(EnterActionsPropertyKey, value); }
        }

        /// <summary>
        /// Gets a collection of actions which get executed when the trigger becomes inactive 
        /// again.
        /// </summary>
        public TriggerActionCollection ExitActions
        {
            get
            {
                var collection = (TriggerActionCollection)GetValue(ExitActionsProperty);
                if (collection == null)
                {
                    collection = new TriggerActionCollection();
                    ExitActions = collection;
                }
                return collection;
            }
            private set { SetValue(ExitActionsPropertyKey, value); }
        }

        // Called when the TriggerBehavior.OnTriggered(parameter) method in the 
        // base class gets called.
        // The base class calls all actions in the "Actions" collection.
        // By overriding this, we can redirect the OnTriggered(..) call to our implementation
        // which also changes the Active state of the trigger.
        internal sealed override void OnTriggeredImpl(object parameter)
        {
            OnTriggered(true, parameter);
        }

        /// <summary>
        ///     Changes the state of the trigger to the provided value.
        ///     Depending on whether the trigger becomes active or not, certain trigger
        ///     actions are executed or reverted.
        /// </summary>
        /// <param name="isActive">
        ///     A value indicating whether the trigger is now active (i.e. triggered) or not.
        /// </param>
        /// <param name="parameter">
        ///     A parameter to be passed to each action. This can be null.
        /// </param>
        protected void OnTriggered(bool isActive, object parameter)
        {
            if (IsActive == isActive)
                return;
            IsActive = isActive;

            InteractivitySource.Info(
                GetHashCode(),
                "{0} was triggered. IsActive: {1}",
                GetType().FullName,
                IsActive
            );

            if (IsActive)
            {
                ExecuteAllActions(EnterActions, parameter);
                ExecuteAllActions(Actions, parameter);
            }
            else
            {
                ExecuteAllActions(ExitActions, parameter);
                RevertAllActions(Actions, parameter);
            }
        }

        internal static void RevertAllActions(IList<ITriggerAction> actions, object parameter)
        {
            foreach (var action in actions)
            {
                if (action is IReversibleTriggerAction reversibleTriggerAction)
                {
                    reversibleTriggerAction.Revert(parameter);
                }
            }
        }

    }

}
