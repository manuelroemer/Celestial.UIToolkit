using System;
using System.Windows;
using System.Windows.Markup;
using Celestial.UIToolkit.Extensions;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Interactions
{

    /// <summary>
    ///     A trigger action which, once executed, applies a set of <see cref="Setter"/> objects
    ///     to their target elements.
    ///     
    ///     This action implements <see cref="IReversibleTriggerAction"/> and can be reverted.
    /// </summary>
    [ContentProperty(nameof(Setters))]
    public sealed class SettersAction : ReversibleTriggerAction<FrameworkElement>
    {
       
        private static readonly DependencyPropertyKey SettersPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(Setters),
                typeof(SetterBaseCollection),
                typeof(SettersAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="Setters"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SettersProperty =
            SettersPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a collection of setters which get executed when a trigger becomes active.
        /// </summary>
        public SetterBaseCollection Setters
        {
            get
            {
                var collection = (SetterBaseCollection)GetValue(SettersProperty);
                if (collection is null)
                {
                    collection = new SetterBaseCollection();
                    SetValue(SettersPropertyKey, collection);
                }
                return collection;
            }
        }

        /// <summary>
        ///     Applies all setters in the <see cref="Setters"/> collection.
        /// </summary>
        /// <param name="element">
        ///     A <see cref="FrameworkElement"/> which is passed by the trigger.
        ///     This is used to locate the setter targets.
        /// </param>
        protected override void Execute(FrameworkElement element)
        {
            foreach (var setterBase in Setters)
            {
                if (setterBase is Setter setter)
                {
                    setter.ApplyToElement(element);
                }
                else
                {
                    ThrowInvalidSetterTypeException();
                }
            }
        }

        /// <summary>
        ///     Invalidates all previously applied setters in the <see cref="Setters"/> collection.
        /// </summary>
        /// <param name="element">
        ///     A <see cref="FrameworkElement"/> which is passed by the trigger.
        ///     This is used to locate the setter targets.
        /// </param>
        protected override void Revert(FrameworkElement element)
        {
            foreach (var setterBase in Setters)
            {
                if (setterBase is Setter setter)
                {
                    setter.RemoveFromElement(element);
                }
                else
                {
                    ThrowInvalidSetterTypeException();
                }
            }
        }

        private void ThrowInvalidSetterTypeException()
        {
            throw new InvalidOperationException(
                $"The {nameof(SettersAction)} only supports {typeof(Setter).FullName} objects."
            );
        }

    }

}
