using System;
using System.Windows;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// A class which is used by the <see cref="ExtendedVisualStateManager"/> to
    /// transition an element between two states.
    /// </summary>
    internal abstract class VisualStateSwitcher
    {

        /// <summary>
        /// Gets the control to transition between states.
        /// </summary>
        public FrameworkElement Control { get; private set; }

        /// <summary>
        /// Gets the root element that contains the <see cref="VisualStateManager"/>
        /// which is switching states.
        /// </summary>
        public FrameworkElement StateGroupsRoot { get; private set; }

        /// <summary>
        /// Gets the name of the state to transition to.
        /// </summary>
        public string StateName { get; private set; }

        /// <summary>
        /// Gets the <see cref="VisualStateGroup"/> that the <see cref="ToState"/> belongs to.
        /// </summary>
        public VisualStateGroup Group { get; private set; }

        /// <summary>
        /// Gets the <see cref="VisualState"/> to which the class should transition to.
        /// </summary>
        public VisualState ToState { get; private set; }

        /// <summary>
        /// Gets the <see cref="VisualState"/> from which the class is transitioning.
        /// </summary>
        public VisualState FromState => Group.CurrentState ?? Group.GetCurrentState();

        /// <summary>
        /// Gets a value indicating whether to use <see cref="VisualTransition"/> objects to transition
        /// between two states.
        /// </summary>
        public bool UseTransitions { get; private set; }

        /// <summary>
        /// Initializes the state-specific properties of this <see cref="VisualStateSwitcher"/>
        /// instance with the provided parameters.
        /// Afterwards, the <see cref="VisualStateSwitcher"/> tries to transition to a new state.
        /// </summary>
        /// <param name="control">The control to transition between states</param>
        /// <param name="stateGroupsRoot">
        /// The root element that contains the <see cref="VisualStateManager"/> 
        /// which is switching the states.
        /// </param>
        /// <param name="stateName">The name of the state to transition to.</param>
        /// <param name="group">The <see cref="VisualStateGroup"/> that the state belongs to.</param>
        /// <param name="state">The representation of the visual state to transition to.</param>
        /// <param name="useTransitions">
        /// A value indicating whether to use <see cref="VisualTransition"/> objects to transition
        /// between two states.
        /// </param>
        /// <returns>
        /// <c>true</c> if the switcher is able to transition the element between its states;
        /// <c>false</c> if not.
        /// </returns>
        public bool GoToState(
            FrameworkElement control,
            FrameworkElement stateGroupsRoot,
            string stateName,
            VisualStateGroup group,
            VisualState state,
            bool useTransitions)
        {
            if (state == null)
                state = group.GetStateByName(stateName);

            Control = control ?? throw new ArgumentNullException(nameof(control));
            StateGroupsRoot = stateGroupsRoot ?? throw new ArgumentNullException(nameof(stateGroupsRoot));
            StateName = stateName ?? throw new ArgumentNullException(nameof(stateName));
            Group = group ?? throw new ArgumentNullException(nameof(group));
            ToState = state ?? throw new ArgumentNullException(nameof(state));
            UseTransitions = useTransitions;

            return GoToStateCore();
        }

        /// <summary>
        /// Called to transition to another state, based on the properties
        /// defined in this class.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the switcher is able to transition the element between its states;
        /// <c>false</c> if not.
        /// </returns>
        protected abstract bool GoToStateCore();

    }

}
