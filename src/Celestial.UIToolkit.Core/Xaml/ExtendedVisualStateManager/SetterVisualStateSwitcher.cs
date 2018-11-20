using System;
using System.Windows;
using System.Windows.Data;
using Celestial.UIToolkit.Extensions;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// A <see cref="VisualStateSwitcher"/> for the <see cref="ExtendedVisualStateManager"/>
    /// which transitions between two states by applying the 
    /// <see cref="ExtendedVisualState.Setters"/> of an <see cref="ExtendedVisualState"/>.
    /// </summary>
    internal sealed class SetterVisualStateSwitcher : VisualStateSwitcher
    {

        /// <summary>
        /// Gets the <see cref="ExtendedVisualState"/> to which this switcher is transitioning.
        /// If that state is not of type <see cref="ExtendedVisualState"/>, this returns null.
        /// </summary>
        public ExtendedVisualState ExtendedToState => ToState as ExtendedVisualState;

        /// <summary>
        /// Gets the <see cref="ExtendedVisualState"/> from which this switcher is transitioning.
        /// If that state is not of type <see cref="ExtendedVisualState"/>, this returns null.
        /// </summary>
        public ExtendedVisualState ExtendedFromState => FromState as ExtendedVisualState;

        /// <summary>
        /// If the target state is of type <see cref="ExtendedVisualState"/>,
        /// checks the state for setters and applies them to the targets.
        /// </summary>
        /// <returns>
        /// true if the target state was of type <see cref="ExtendedVisualState"/>;
        /// false if not.
        /// </returns>
        protected override bool GoToStateCore()
        {
            RemovePreviousStateSetters();
            ApplyCurrentStateSetters();

            return true;
        }

        private void RemovePreviousStateSetters()
        {
            if (ExtendedFromState == null) return;

            foreach (var setterBase in ExtendedFromState.Setters)
            {
                if (setterBase is Setter setter)
                {
                    setter.RemoveFromElement(StateGroupsRoot);
                }
                else
                {
                    ThrowInvalidSetterTypeException();
                }
            }
        }
        
        private void ApplyCurrentStateSetters()
        {
            if (ExtendedToState == null) return;

            foreach (var setterBase in ExtendedToState.Setters)
            {
                if (setterBase is Setter setter)
                {
                    setter.ApplyToElement(StateGroupsRoot);
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
                $"The {nameof(ExtendedVisualStateManager)} only supports {typeof(Setter).FullName} " +
                $"instances."
            );
        }
        
    }

}
