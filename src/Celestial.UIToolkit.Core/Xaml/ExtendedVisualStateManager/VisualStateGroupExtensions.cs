using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// Provides extension methods which are required for custom animation
    /// logic inside the toolkit's custom visual state manager.
    /// </summary>
    internal static class VisualStateGroupStoryboards
    {

        // Shoutout to:
        // https://github.com/mtebenev/winrt-samples/blob/master/UiCore.WinRt/Platform/ExtendedVisualStateManagerExtenstions.cs
        // which inspired the creation of this one (and thus the general layout of 
        // the AnimationVisualStateSwitcher).
        
        // We abuse attached dependency properties to "inject" properties into the VisualStateGroup
        // via extension methods.
        // They can only be accessed by the Get/Set extension methods.
        private static readonly DependencyPropertyKey _currentStoryboardsPropertyKey = 
            DependencyProperty.RegisterAttachedReadOnly(
                "CurrentStoryboards",
                typeof(Collection<Storyboard>),
                typeof(VisualStateGroupStoryboards),
                new PropertyMetadata(null));

        private static readonly DependencyPropertyKey _currentStatePropertyKey = 
            DependencyProperty.RegisterAttachedReadOnly(
                "CurrentState",
                typeof(VisualState),
                typeof(VisualStateGroupStoryboards),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets a collection of storyboards which are running right now.
        /// Whenever the group's visual state changes, this collection will change.
        /// </summary>
        /// <param name="stateGroup">The <see cref="VisualStateGroup"/>.</param>
        public static ICollection<Storyboard> GetCurrentStoryboards(this VisualStateGroup stateGroup)
        {
            if (stateGroup == null) throw new ArgumentNullException(nameof(stateGroup));

            var collection = (Collection<Storyboard>)stateGroup.GetValue(
                _currentStoryboardsPropertyKey.DependencyProperty);
            if (collection == null)
            {
                collection = new Collection<Storyboard>();
                stateGroup.SetValue(_currentStoryboardsPropertyKey, collection);
            }
            return collection;
        }

        public static VisualState GetCurrentState(this VisualStateGroup stateGroup)
        {
            if (stateGroup == null) throw new ArgumentNullException(nameof(stateGroup));
            return (VisualState)stateGroup.GetValue(_currentStatePropertyKey.DependencyProperty);
        }
        
        public static void SetCurrentState(this VisualStateGroup stateGroup, VisualState state)
        {
            if (stateGroup == null) throw new ArgumentNullException(nameof(stateGroup));
            stateGroup.SetValue(_currentStatePropertyKey, state);
        }
        
        /// <summary>
        /// Starts the specified <paramref name="newStoryboards"/> on the <paramref name="element"/>.
        /// Afterwards, the currently running storyboards of the visual state group are stopped.
        /// </summary>
        /// <param name="group">The group on which the storyboards will be started/stopped.</param>
        /// <param name="element">The element on which the storyboards will be run.</param>
        /// <param name="newStoryboards">The new storyboards to be run.</param>
        public static void StartNewAndStopOldStoryboards(
            this VisualStateGroup group, FrameworkElement element, params Storyboard[] newStoryboards)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            // Stop old storyboards before starting new ones.
            StopCurrentStoryboards(group, element);

            // Merge all of the passed Storyboards into one single master storyboard.
            // This fixes a bug in the WPF animation system which sometimes causes certain
            // storyboards to not fire a completed event.
            // See https://stackoverflow.com/questions/5002501/visualstatemanager-not-working-as-advertised
            // for details.
            Storyboard masterStoryboard = BuildMasterStoryboard(newStoryboards);
            if (masterStoryboard != null)
            {
                masterStoryboard.Begin(element, HandoffBehavior.SnapshotAndReplace, true);
                group.GetCurrentStoryboards().Add(masterStoryboard);
            }
        }

        /// <summary>
        /// Wraps all specified <paramref name="storyboards"/> into a single
        /// master storyboard.
        /// </summary>
        /// <param name="storyboards">
        /// A range of storyboards. If this is null or empty, the resulting master storyboard will
        /// also be null.
        /// </param>
        /// <returns>
        /// A single master storyboard or null, if no input storyboards were provided.
        /// </returns>
        private static Storyboard BuildMasterStoryboard(Storyboard[] storyboards)
        {
            if (storyboards == null || storyboards.Length == 0)
                return null;

            if (storyboards.Length == 1 && storyboards[0] != null)
            {
                // If there is only one storyboard, we can recycle it.
                // No need to create a new instance.
                return storyboards[0];
            }
            else
            {
                var masterStoryboard = new Storyboard();
                foreach (var sb in storyboards)
                    if (sb != null)
                        masterStoryboard.Children.Add(sb);
                return masterStoryboard;
            }
        }
        
        private static void StopCurrentStoryboards(VisualStateGroup group, FrameworkElement element)
        {
            var currentStoryboards = group.GetCurrentStoryboards();
            foreach (var sb in currentStoryboards)
            {
                if (sb != null)
                {
                    sb.Remove(element);
                }
            }
            currentStoryboards.Clear();
        }

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

    }

}
