using System.Windows;
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
                if (IsValidSetter(setterBase))
                {
                    RemoveSetter((Setter)setterBase);
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
                VisualStateSource.Verbose(
                    "Removed visual state setter for property {0} from element {1}.", 
                    setter.Property.Name, 
                    setterTarget);
            }
        }

        private void ApplyCurrentStateSetters()
        {
            if (ExtendedToState == null) return;

            foreach (SetterBase setterBase in ExtendedToState.Setters)
            {
                if (IsValidSetter(setterBase))
                {
                    ApplySetter((Setter)setterBase);
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
                VisualStateSource.Verbose(
                    "Setting property {0} to {1} on element {2}.",
                    setter.Property.Name,
                    setter.Value,
                    setterTarget);
            }
        }

        private DependencyObject FindSetterTarget(Setter setter)
        {
            // If no target name is specified, the setter referes (by convention) to the control.
            if (string.IsNullOrEmpty(setter.TargetName))
                return Control;

            // Try to locate the target in the template part, or in the control itself.
            var target = StateGroupsRoot.FindName(setter.TargetName) as DependencyObject ??
                         Control.FindName(setter.TargetName) as DependencyObject;

            if (target == null)
            {
                VisualStateSource.Warn(
                    "Couldn't find the visual state setter target \"{0}\".", setter.TargetName);
            }

            return target;
        }

        private bool IsValidSetter(SetterBase setterBase)
        {
            // We can't enforce that the setter's Property matches the Value (and that's not our 
            // job), but we must ensure that its TargetName and Property are valid values,
            // since this class makes use of them.
            return setterBase is Setter setter &&
                   setter.Property != null;
        }

    }

}
