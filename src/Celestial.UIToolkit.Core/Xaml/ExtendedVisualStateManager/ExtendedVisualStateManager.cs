using System;
using System.Collections.Generic;
using System.Windows;

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
        /// Gets a list of <see cref="VisualStateSwitcher"/> instances which are used by the
        /// <see cref="ExtendedVisualStateManager"/> to transition an element between its visual
        /// states.
        /// If you want to implement your own <see cref="VisualStateSwitcher"/> for custom
        /// transitioning logic, add it to this list to make the 
        /// <see cref="ExtendedVisualStateManager"/> use it.
        /// </summary>
        public static IList<VisualStateSwitcher> VisualStateSwitchers { get; private set; }

        static ExtendedVisualStateManager()
        {
            // The extended version of the VSM covers all of the default VSM's features.
            // To not clutter this class, we delegate the actual transitioning logic
            // to VisualStateSwitcher objects.
            // This has the additional benefit of not having to pass around the gigantic list
            // of parameters (-> see GoToStateCore) all the time, since we can just use properties
            // in the VisualStateSwitcher class.
            //
            // The switchers which are registered here are the ones supported by default.
            // Users of this class can add custom switchers for custom logic to the list aswell.
            VisualStateSwitchers = new List<VisualStateSwitcher>()
            {
                //new AnimationVisualStateSwitcher()
            };
        }

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
            if (control == null || 
                stateGroupsRoot == null || 
                stateName == null || 
                group == null || 
                state == null)
                return false;
            
            // Allow each registered state switcher to transition to the new state.
            // If any of them could sucessfully transition to the new state, that's considered
            // a success.
            bool couldTransitionToState = false;
            foreach (var stateSwitcher in VisualStateSwitchers)
            {
                if (stateSwitcher != null)
                {
                    couldTransitionToState |= stateSwitcher.GoToState(
                        control, stateGroupsRoot, stateName, group, state, useTransitions);
                }
            }

            couldTransitionToState |= new AnimationVisualStateSwitcher().GoToState(
                control, stateGroupsRoot, stateName, group, state, useTransitions);
            return couldTransitionToState;
        }

    }

    /// <summary>
    /// A class which is used by the <see cref="ExtendedVisualStateManager"/> to
    /// transition an element between two states.
    /// </summary>
    public abstract class VisualStateSwitcher
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
        /// Gets the <see cref="VisualState"/> from which the class is transitioning.
        /// </summary>
        public VisualState FromState => Group.CurrentState;

        /// <summary>
        /// Gets the <see cref="VisualState"/> to which the class should transition to.
        /// </summary>
        public VisualState ToState { get; private set; }

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

            // No need to do anything if we aren't between two states.
            if (FromState == ToState) return false;

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
