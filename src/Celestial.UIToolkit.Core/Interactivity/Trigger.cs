using System.Windows;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// A specialized type of behavior which executes a set of actions, once
    /// a certain condition is met (i.e. the behavior is triggered).
    /// </summary>
    [ContentProperty(nameof(Actions))]
    public class Trigger : Behavior, ITrigger
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

    }

}
