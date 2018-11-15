using System.Windows;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A specialized type of behavior which executes a set of actions, once
    /// a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    [ContentProperty(nameof(Actions))]
    public abstract class Trigger : Behavior, ITrigger
    {

        private static readonly DependencyPropertyKey ActionsPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(Actions),
                typeof(TriggerActionCollection),
                typeof(Trigger),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="Actions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActionsProperty =
            ActionsPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a collection of actions which get executed, once a certain condition of this
        /// behavior is met.
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
        /// This method is supposed to be called when the trigger's condition is met.
        /// When called, this method goes through each registered <see cref="ITriggerAction"/> in
        /// the <see cref="Actions"/> collection and executes it.
        /// </summary>
        protected void OnTriggered()
        {
            OnTriggered(null);
        }
        
        /// <summary>
        /// This method is supposed to be called when the trigger's condition is met.
        /// When called, this method goes through each registered <see cref="ITriggerAction"/> in
        /// the <see cref="Actions"/> collection and executes it.
        /// </summary>
        /// <param name="parameter">
        /// A parameter to be passed to each action. This can be null.
        /// </param>
        protected void OnTriggered(object parameter)
        {
            foreach (var action in Actions)
            {
                action.Execute(parameter);
            }
        }

    }

}
