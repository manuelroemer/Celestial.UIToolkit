using System;
using System.Windows;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// A custom visual state manager which provides the same features, aswell as additional ones,
    /// as the default <see cref="VisualStateManager"/>.
    /// </summary>
    /// <remarks>
    /// For using this extended visual state manager, the 
    /// <see cref="VisualStateManager.CustomVisualStateManagerProperty"/> dependency property must
    /// be set on a control which defines the visual state groups (-> the state groups root 
    /// element).
    /// </remarks>
    public class ExtendedVisualStateManager : VisualStateManager
    {

        /// <summary>
        /// Gets a default instance of the <see cref="ExtendedVisualStateManager"/> for quick
        /// access (for instance in XAML).
        /// </summary>
        public static ExtendedVisualStateManager Default { get; } = 
            new ExtendedVisualStateManager();
        
        /// <summary>
        /// Transitions a control between states by letting each registered
        /// <see cref="VisualStateSwitcher"/> transition to the new state.
        /// </summary>
        /// <param name="control">The control to transition between states.</param>
        /// <param name="stateGroupsRoot">
        /// The root element that contains the <see cref="ExtendedVisualStateManager"/>.
        /// </param>
        /// <param name="stateName">The name of the state to transition to.</param>
        /// <param name="group"> The <see cref="VisualStateGroup"/> that the state belongs to.</param>
        /// <param name="state">The representation of the state to transition to.</param>
        /// <param name="useTransitions">
        /// true to use a <see cref="VisualTransition"/> object to transition between states;
        /// otherwise, false.
        /// </param>
        /// <returns>
        /// <c>true</c> if the control successfully transitioned to the new state; 
        /// otherwise, <c>false</c>.
        /// </returns>
        protected override bool GoToStateCore(
            FrameworkElement control, 
            FrameworkElement stateGroupsRoot, 
            string stateName, 
            VisualStateGroup group, 
            VisualState state, 
            bool useTransitions)
        {
            if (control == null || stateGroupsRoot == null || stateName == null || group == null || state == null)
                return false;
            if (!ShouldTransitionToState(control, group, state))
                return true;
            return TransitionToState(control, stateGroupsRoot, stateName, group, state, useTransitions);
        }

        private bool ShouldTransitionToState(FrameworkElement control, VisualStateGroup group, VisualState state)
        {
            // No need to transition, if we are already at the target state.
            if ((group.GetCurrentState() ?? group.CurrentState) == state)
            {
                VisualStateSource.Verbose(
                    "Not changing to visual state {0}, because the control \"{1}\" is already at that state.",
                    state.Name,
                    control
                );
                return false;
            }

            // The ExtendedVisualState can have a set of Conditions, which need to apply.
            if (state is ExtendedVisualState extendedState &&
                !extendedState.AreConditionsApplyingToControl(control))
            {
                VisualStateSource.Verbose(
                    "Not changing to visual state {0}, because one of " +
                    "the state's conditions doesn't apply.",
                    state.Name
                );
                return false;
            }

            return true;
        }
        
        private static bool TransitionToState(
            FrameworkElement control, 
            FrameworkElement stateGroupsRoot, 
            string stateName, 
            VisualStateGroup group, 
            VisualState state, 
            bool useTransitions)
        {
            VisualStateSource.Info(
                "Changing visual state from \"{0}\" to \"{1}\" on control {2} with root element {3}.",
                group.GetCurrentState()?.Name ?? group.CurrentState?.Name,
                stateName,
                control,
                stateGroupsRoot
            );

            // We offload the actual transitioning logic into multiple different StateSwitchers,
            // so that this class doesn't become cluttered.
            // Simply call them in order and check if one of them managed to transition to a new state.
            bool couldTransitionToState = false;
            couldTransitionToState |= new SetterVisualStateSwitcher().GoToState(
                control, stateGroupsRoot, stateName, group, state, useTransitions
            );
            couldTransitionToState |= new AnimationVisualStateSwitcher().GoToState(
                control, stateGroupsRoot, stateName, group, state, useTransitions
            );

            group.SetCurrentState(state);
            return couldTransitionToState;
        }

    }

}
