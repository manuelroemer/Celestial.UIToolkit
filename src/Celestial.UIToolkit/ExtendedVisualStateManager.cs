using Celestial.UIToolkit.Media.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// A base class for a custom <see cref="VisualStateManager"/> that offloads its
    /// transitioning logic into several independent <see cref="VisualStateSwitcher"/> elements.
    /// </summary>
    /// <seealso cref="VisualStateSwitcher"/>
    /// <seealso cref="VisualStateSwitchers"/>
    public class ExtendedVisualStateManager : VisualStateManager
    {
        
        /// <summary>
        /// Gets a default instance of the <see cref="ExtendedVisualStateManager"/>
        /// which is used by the UI Toolkit.
        /// If you implement a custom <see cref="VisualStateSwitcher"/> and want it to be known
        /// to the toolkit's default styles, register them in this instance.
        /// </summary>
        public static ExtendedVisualStateManager Default { get; }

        /// <summary>
        /// Gets a collection of <see cref="VisualStateSwitcher"/> objects
        /// which can be used to switch an element's state.
        /// When transitioning to an element's state, the <see cref="ExtendedVisualStateManager"/>
        /// will go through this collection and see if any of the registered elements is able
        /// to do a transition.
        /// If not, the default transition logic by the <see cref="VisualStateManager"/> will be used.
        /// </summary>
        public virtual IList<VisualStateSwitcher> VisualStateSwitchers { get; }
        
        static ExtendedVisualStateManager()
        {
            Default = new ExtendedVisualStateManager();
            Default.VisualStateSwitchers.Add(new AnimationVisualStateSwitcher());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedVisualStateManager"/>.
        /// </summary>
        public ExtendedVisualStateManager()
        {
            this.VisualStateSwitchers = new List<VisualStateSwitcher>(2);
        }

        /// <summary>
        /// Transitions a control between states,
        /// by calling the <see cref="VisualStateSwitcher.GoToState(FrameworkElement, FrameworkElement, string, VisualStateGroup, VisualState, bool)"/>
        /// method of each <see cref="VisualStateSwitcher"/> in the <see cref="VisualStateSwitchers"/>
        /// collection.
        /// Afterwards, the default <see cref="VisualStateManager.GoToStateCore(FrameworkElement, FrameworkElement, string, VisualStateGroup, VisualState, bool)"/>
        /// logic will be used for the transition.
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
        /// true if the control successfully transitioned to the new state; otherwise, false.
        /// </returns>
        protected override bool GoToStateCore(FrameworkElement control, FrameworkElement stateGroupsRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions)
        {
            if (control == null || stateGroupsRoot == null || stateName == null || group == null || state == null)
                return false;

            // We allow one switcher to do the transition.
            // If none exists, we will let the default VSM do the work.
            bool couldTransitionToState = false;
            if (this.VisualStateSwitchers != null)
            {
                foreach (var stateSwitcher in VisualStateSwitchers)
                {
                    if (stateSwitcher != null)
                    {
                        bool successfulTransition = stateSwitcher.GoToState(
                            control, stateGroupsRoot, stateName, group, state, useTransitions);
                        couldTransitionToState |= successfulTransition;
                    }
                }
            }
            
            couldTransitionToState |= base.GoToStateCore(
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
        public VisualState FromState => this.Group.CurrentState;

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
        /// Initializes a new instance of the <see cref="VisualStateSwitcher"/> class.
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
        /// true if the switcher is able to transition the element between its states;
        /// false if not.
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
                state = VSMExtensions.GetStateByName(group, stateName);

            this.Control = control ?? throw new ArgumentNullException(nameof(control));
            this.StateGroupsRoot = stateGroupsRoot ?? throw new ArgumentNullException(nameof(stateGroupsRoot));
            this.StateName = stateName ?? throw new ArgumentNullException(nameof(stateName));
            this.Group = group ?? throw new ArgumentNullException(nameof(group));
            this.ToState = state ?? throw new ArgumentNullException(nameof(state));
            this.UseTransitions = useTransitions;

            // No need to do anything if we aren't between two states.
            if (this.FromState == this.ToState) return false;

            return this.GoToStateCore();
        }
        
        /// <summary>
        /// Called to transition to another state, based on the properties
        /// defined in this class.
        /// </summary>
        /// <returns>
        /// true if the switcher is able to transition the element between its states;
        /// false if not.
        /// </returns>
        protected abstract bool GoToStateCore();
        
    }

    /// <summary>
    /// Provides extension methods for a set of members which fall into the 
    /// <see cref="VisualStateManager"/> category.
    /// </summary>
    public static class VisualStateMemberExtensions
    {

        /// <summary>
        /// Returns the first state in the <see cref="VisualStateGroup.States"/>
        /// which has the specified <paramref name="stateName"/>,
        /// or <c>null</c>, if no state with the specified name could be found.
        /// </summary>
        /// <param name="group">The group in which a <see cref="VisualState"/> should be retrieved.</param>
        /// <param name="stateName">The name of a state to be retrieved.</param>
        /// <returns>
        /// The first <see cref="VisualState"/> with the specified <paramref name="stateName"/>
        /// or <c>null</c>, if no such state was found.
        /// </returns>
        public static VisualState GetStateByName(this VisualStateGroup group, string stateName)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            if (stateName == null) return null;

            foreach (VisualState state in group.States)
            {
                if (state.Name == stateName) return state;
            }
            return null;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <paramref name="transition"/>
        /// can be treated as a default transition inside a collection.
        /// This is the case, if its <see cref="VisualTransition.From"/> and
        /// <see cref="VisualTransition.To"/> properties are <c>null</c>.
        /// </summary>
        /// <param name="transition">The transition to be checked.</param>
        /// <returns>
        /// <c>true</c> if the transition can be treated as default; false if not.
        /// </returns>
        public static bool IsDefault(this VisualTransition transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            return transition.From == null && transition.To == null;
        }

    }

}
