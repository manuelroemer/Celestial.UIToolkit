using System.Windows;

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
            if (ExtendedToState == null)
                return false;

            RemovePreviousStateSetters();
            ApplyCurrentStateSetters();

            return true;
        }

        private void RemovePreviousStateSetters()
        {
            if (ExtendedFromState == null) return;

            foreach (SetterBase setterBase in ExtendedFromState.Setters)
            {
                if (setterBase is Setter setter && 
                    IsSetterValid(setter))
                {
                    RemoveSetter(setter);
                }
            }
        }

        private void RemoveSetter(Setter setter)
        {
            // InvalidateProperty forces the previously changed property to be 
            // re-evaluated. This resets any previous changes.
            DependencyObject setterTarget = FindSetterTarget(setter);
            if (setterTarget != null)
            {
                setterTarget.InvalidateProperty(setter.Property);
            }
        }

        private void ApplyCurrentStateSetters()
        {
            if (ExtendedToState == null) return;

            foreach (SetterBase setterBase in ExtendedToState.Setters)
            {
                if (setterBase is Setter setter && 
                    IsSetterValid(setter))
                {
                    ApplySetter(setter);
                }
            }
        }

        private void ApplySetter(Setter setter)
        {
            DependencyObject setterTarget = FindSetterTarget(setter);
            if (setterTarget != null)
            {
                // SetCurrentValue doesn't change the property source, but changes the value until
                // reset.
                // This is ideal, as long as we reset the property again 
                // (which is done in the RemoveSetter method(s)).
                setterTarget.SetCurrentValue(setter.Property, setter.Value);
            }
        }

        private DependencyObject FindSetterTarget(Setter setter)
        {
            // Try to locate the target in the template part, or in the control itself.
            return StateGroupsRoot.FindName(setter.TargetName) as DependencyObject ??
                   Control.FindName(setter.TargetName) as DependencyObject;
        }

        private bool IsSetterValid(Setter setter)
        {
            // We can't enforce that the setter's Property matches the Value (and that's not our 
            // job), but we must ensure that its TargetName and Property are valid values,
            // since this class makes use of them.
            return !string.IsNullOrEmpty(setter.TargetName) &&
                   setter.Property != null;
        }

    }

}
