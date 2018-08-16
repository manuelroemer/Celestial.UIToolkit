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
        public static ExtendedVisualStateManager Default { get; } = new ExtendedVisualStateManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedVisualStateManager"/>.
        /// </summary>
        public ExtendedVisualStateManager()
        {
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
            couldTransitionToState |= new AnimationVisualStateSwitcher().GoToState(
                control, stateGroupsRoot, stateName, group, state, useTransitions);
        
            if (!couldTransitionToState)
                couldTransitionToState = base.GoToStateCore(control, stateGroupsRoot, stateName, group, state, useTransitions);

            return couldTransitionToState;
        }

    }

    /// <summary>
    /// A class which is used by the <see cref="ExtendedVisualStateManager"/> to
    /// transition an element between two states.
    /// </summary>
    public abstract class VisualStateSwitcher
    {

        private bool _wasUsed = false;

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
            if (_wasUsed)
            {
                throw new InvalidOperationException(
                    $"A {nameof(VisualStateSwitcher)} can only be used once.");
            }
            _wasUsed = true;

            if (state == null)
                state = group.GetStateByName(stateName);

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

}
