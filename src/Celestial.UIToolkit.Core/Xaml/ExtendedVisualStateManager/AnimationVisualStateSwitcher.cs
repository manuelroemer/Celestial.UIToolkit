using Celestial.UIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Xaml
{

    /// <summary>
    /// A <see cref="VisualStateSwitcher"/> for the <see cref="ExtendedVisualStateManager"/>
    /// which transitions between two states by generating dynamic transitioning animations.
    /// </summary>
    /// <remarks>
    /// This class mimics the behavior of the default WPF <see cref="VisualStateManager"/> and 
    /// extends the transitioning functionality to custom animations.
    /// 
    /// If you want to create a custom transition for a specific animation, create an
    /// <see cref="IVisualTransitionProvider"/> for the animation and register it in the
    /// <see cref="VisualTransitionProvider"/> class.
    /// Afterwards, the custom transition will be used by this class.
    /// </remarks>
    internal sealed class AnimationVisualStateSwitcher : VisualStateSwitcher
    {

        /// <summary>
        /// Transitions to another state by generating dynamic transitioning animations.
        /// </summary>
        /// <returns>
        /// Always returns <c>true</c>.
        /// </returns>
        protected override bool GoToStateCore()
        {
            VisualTransition currentTransition = GetCurrentVisualTransition();
            Storyboard dynamicTransitionStoryboard = CreateDynamicTransitionStoryboard(currentTransition);
            
            if (currentTransition == null || currentTransition.HasZeroDuration())
            {
                // Without a transition (or a transition which has no duration), the animations 
                // defined in the ToState are supposed to start immediately.
                VisualStateSource.Verbose(
                    "Not using transitions. Transitions available: {0}",
                    currentTransition != null);
                PlayToStateAnimations(currentTransition);
            }
            else
            {
                VisualStateSource.Verbose("Using generated transition storyboard...");
                PlayTransitionAnimations(currentTransition, dynamicTransitionStoryboard);
            }

            return true;
        }

        private void PlayToStateAnimations(VisualTransition currentTransition)
        {
            // Immediately start playing the ToState animations.
            Group.StartNewThenStopOldStoryboards(
                StateGroupsRoot, currentTransition?.Storyboard, ToState.Storyboard);
        }

        private void PlayTransitionAnimations(
            VisualTransition currentTransition, Storyboard dynamicTransitionStoryboard)
        {
            // Create these local event handlers, so that we can pass the local variables
            // to the actual handler functions, while also being able to de-register the event
            // handlers again, to not create memory leaks.
            EventHandler dynamicStoryboardCompletedHandler = null;
            EventHandler currentStoryboardCompletedHandler = null;
            dynamicStoryboardCompletedHandler = (sender, e) =>
            {
                dynamicTransitionStoryboard.Completed -= dynamicStoryboardCompletedHandler;
                OnDynamicTransitionStoryboardCompleted(dynamicTransitionStoryboard, currentTransition);
            };
            currentStoryboardCompletedHandler = (sender, e) =>
            {
                currentTransition.Storyboard.Completed -= currentStoryboardCompletedHandler;
                OnCurrentTransitionStoryboardCompleted(currentTransition);
            };

            // Play the dynamically created storyboard every single time.
            VisualStateSource.Verbose("Preparing dynamically generated transition storyboard.");
            currentTransition.SetDynamicStoryboardCompleted(false);
            dynamicTransitionStoryboard.Completed += dynamicStoryboardCompletedHandler;

            // If a storyboard has been defined INSIDE the VisualTransition 
            // (-> explicit storyboard), play that aswell.
            if (currentTransition.Storyboard != null &&
                currentTransition.GetExplicitStoryboardCompleted())
            {
                VisualStateSource.Verbose("Preparing explicit storyboard defined in transition.");
                currentTransition.SetExplicitStoryboardCompleted(false);
                currentTransition.Storyboard.Completed += currentStoryboardCompletedHandler;
            }

            VisualStateSource.Verbose("Starting storyboards.");
            Group.StartNewThenStopOldStoryboards(
                StateGroupsRoot,
                currentTransition.Storyboard,
                dynamicTransitionStoryboard);
        }
        
        private void OnDynamicTransitionStoryboardCompleted(
            Storyboard dynamicTransitionStoryboard, VisualTransition currentTransition)
        {
            VisualStateSource.Verbose("Dynamic transition storyboard completed. State: {0}", StateName);
            if (currentTransition.Storyboard != null || 
                currentTransition.GetExplicitStoryboardCompleted())
            {
                if (ShouldRunStateStoryboard())
                {
                    VisualStateSource.Verbose("Running ToState storyboards.");
                    Group.StartNewThenStopOldStoryboards(StateGroupsRoot, ToState.Storyboard);
                }
            }
            currentTransition.SetDynamicStoryboardCompleted(true);
        }

        private void OnCurrentTransitionStoryboardCompleted(VisualTransition currentTransition)
        {
            VisualStateSource.Verbose("Explicit storyboard completed. State: {0}", StateName);
            if (currentTransition.GetDynamicStoryboardCompleted() &&
                ShouldRunStateStoryboard())
            {
                VisualStateSource.Verbose("Running ToState storyboards.");
                Group.StartNewThenStopOldStoryboards(StateGroupsRoot, ToState.Storyboard);
            }
            currentTransition.SetExplicitStoryboardCompleted(true);
        }

        private bool ShouldRunStateStoryboard()
        {
            // Ensure that the controls are loaded/in a tree, so that the storyboards can find them.
            // IsLoaded never gets set to false when unloading, so make use of the element's parent.
            // (Should possibly be updated with better unloaded-detection).
            var rootParent = VisualTreeHelper.GetParent(StateGroupsRoot);
            var controlParent = VisualTreeHelper.GetParent(Control);
            return rootParent != null &&
                   StateGroupsRoot.IsLoaded &&
                   StateGroupsRoot.IsVisible &&
                   controlParent != null &&
                   Control.IsLoaded &&
                   Control.IsVisible &&
                   ToState == Group.GetCurrentState();
        }

        private VisualTransition GetCurrentVisualTransition()
        {
            if (!UseTransitions) return null;
            VisualTransition result = null;

            foreach (VisualTransition transition in Group.Transitions)
            {                
                // We want to find the transition which matches the current states the best.
                // -> If there is a transition whose From/To properties match both states, use it.
                //    If not, use a transition which matches only one state.
                //    If none of that is found, use a transition without any From/To values (a default one).
                if (IsTransitionBetterMatch(result, transition))
                {
                    result = transition;
                }
            }
            return result;
        }

        private bool IsTransitionBetterMatch(VisualTransition current, VisualTransition transitionToTest)
        {
            return GetMatchPriorityNumber(current) < GetMatchPriorityNumber(transitionToTest);
            
            sbyte GetMatchPriorityNumber(VisualTransition transition)
            {
                const sbyte PerfectMatch = 3,
                            ToMatch = 2,
                            FromMatch = 1,
                            DefaultTransitionMatch = 0,
                            NoMatch = -1;

                if (transition == null) return NoMatch;
                VisualState transitionFromState = Group.GetStateByName(transition.From);
                VisualState transitionToState = Group.GetStateByName(transition.To);

                if (FromState == transitionFromState && ToState == transitionToState)
                    return PerfectMatch;
                else if (ToState == transitionToState && transitionFromState == null)
                    return ToMatch;
                else if (FromState == transitionFromState && transitionToState == null)
                    return FromMatch;
                else if (transition.IsDefault())
                    return DefaultTransitionMatch;
                else
                    return NoMatch;
            }
        }

        private Storyboard CreateDynamicTransitionStoryboard(VisualTransition currentTransition)
        {
            var storyboard = new Storyboard();
            var easingFunction = currentTransition?.GeneratedEasingFunction;
            storyboard.Duration = currentTransition?.GeneratedDuration ??
                                  new Duration(TimeSpan.Zero);

            FillDynamicTransitionStoryboard(storyboard, currentTransition, easingFunction);
            return storyboard;
        }

        private void FillDynamicTransitionStoryboard(
            Storyboard storyboard, VisualTransition currentTransition, IEasingFunction easingFunction)
        {
            ISet<Timeline> currentGroupTimelines = FlattenTimelines(Group.GetCurrentStoryboards().ToArray());
            ISet<Timeline> transitionTimelines = FlattenTimelines(currentTransition?.Storyboard);
            ISet<Timeline> toStateTimelines = FlattenTimelines(ToState.Storyboard);

            // If the transition already covers an animation, there is no need for that animation.
            // Also, if there is already a "To" animation, we must never use a "From" animation,
            // because the two animations would fight over the same property.
            currentGroupTimelines.ExceptWith(transitionTimelines);
            toStateTimelines.ExceptWith(transitionTimelines);
            currentGroupTimelines.ExceptWith(toStateTimelines);

            IList<Timeline> toTransitions = CreateToTransitions(toStateTimelines, easingFunction);
            IList<Timeline> fromTransitions = CreateFromTransitions(currentGroupTimelines, easingFunction);

            foreach (var toTransition in toTransitions)
            {
                AddTimelineToCurrentStoryboard(toTransition);
                currentGroupTimelines.Remove(toTransition);
            }

            foreach (var fromTransition in fromTransitions)
            {
                AddTimelineToCurrentStoryboard(fromTransition);
            }

            void AddTimelineToCurrentStoryboard(Timeline timeline)
            {
                if (timeline != null)
                {
                    timeline.Duration = storyboard.Duration;
                    storyboard.Children.Add(timeline);
                }
            }
        }

        private IList<Timeline> CreateToTransitions(
            ICollection<Timeline> toStateTimelines, IEasingFunction easingFunction)
        {
            var toTransitions = new List<Timeline>(toStateTimelines.Count);
            foreach (var timeline in toStateTimelines)
            {
                var toAnimation = CreateToAnimation(timeline, easingFunction);
                StoryboardHelper.CopyTargetProperties(StateGroupsRoot, timeline, toAnimation);
                toTransitions.Add(toAnimation);
            }
            return toTransitions;
        }

        private IList<Timeline> CreateFromTransitions(
            ICollection<Timeline> fromStateTimelines, IEasingFunction easingFunction)
        {
            var fromTransitions = new List<Timeline>(fromStateTimelines.Count);
            foreach (var timeline in fromStateTimelines)
            {
                var fromAnimation = CreateFromAnimation(timeline, easingFunction);
                StoryboardHelper.CopyTargetProperties(StateGroupsRoot, timeline, fromAnimation);
                fromTransitions.Add(fromAnimation);
            }
            return fromTransitions;
        }

        private Timeline CreateToAnimation(Timeline timeline, IEasingFunction easingFunction)
        {
            VisualTransitionProvider.TryGetProviderForTimeline(timeline, out var provider);
            return provider?.CreateToTransitionTimeline(timeline, easingFunction);
        }
        
        private Timeline CreateFromAnimation(Timeline timeline, IEasingFunction easingFunction)
        {
            VisualTransitionProvider.TryGetProviderForTimeline(timeline, out var provider);
            return provider?.CreateFromTransitionTimeline(timeline, easingFunction);
        }
        
        private ISet<Timeline> FlattenTimelines(params Storyboard[] storyboards)
        {
            // A storyboard can have other storyboards as children.
            // The goal of this method is to extract a single set of all timelines from the storyboard(s).
            var result = new HashSet<Timeline>(StoryboardTargetTimelineEqualityComparer.Instance);
            if (storyboards != null)
            {
                foreach (var storyboard in storyboards)
                {
                    FlattenSingleStoryboard(storyboard, result);
                }
            }
            return result;

            void FlattenSingleStoryboard(Storyboard storyboard, ISet<Timeline> results)
            {
                if (storyboard == null) return;
                foreach (var timeline in storyboard.Children)
                {
                    if (timeline is Storyboard childStoryboard)
                    {
                        FlattenSingleStoryboard(childStoryboard, results);
                    }
                    else if (timeline != null)
                    {
                        // If a storyboard has the same target, replace the old one with the new 
                        // one, so that the last storyboard in a list will be run.
                        results.Remove(timeline);
                        results.Add(timeline);
                    }
                }
            }
        }

    }

    internal static class StoryboardHelper
    {

        /// <summary>
        /// Copies the <see cref="Storyboard.TargetProperty"/>,
        /// <see cref="Storyboard.TargetNameProperty"/> and 
        /// <see cref="Storyboard.TargetPropertyProperty"/>
        /// from the <paramref name="source"/> timeline to the
        /// <paramref name="destination"/> timeline,
        /// if none of them is null.
        /// </summary>
        /// <param name="rootContainer">
        /// A root element which contains the timelines.
        /// Used to find a target, if only its name is specified.
        /// </param>
        /// <param name="source">The source timeline.</param>
        /// <param name="destination">The destination timeline.</param>
        public static void CopyTargetProperties(FrameworkElement rootContainer, Timeline source, Timeline destination)
        {
            if (source == null || destination == null) return;
            var targetName = Storyboard.GetTargetName(source);
            var target = Storyboard.GetTarget(source);
            var targetProperty = Storyboard.GetTargetProperty(source);

            // If no target is set, try to locate it via its name.
            if (target == null && !string.IsNullOrEmpty(targetName))
                target = rootContainer.FindName(targetName) as DependencyObject;

            if (!string.IsNullOrEmpty(targetName))
                Storyboard.SetTargetName(destination, targetName);
            if (target != null)
                Storyboard.SetTarget(destination, target);
            if (targetProperty != null)
                Storyboard.SetTargetProperty(destination, targetProperty);
        }

    }

    /// <summary>
    /// An equality comparer for <see cref="Timeline"/> objects
    /// which compares them based on attached <see cref="Storyboard"/> properties
    /// which define a timeline's target.
    /// </summary>
    [DebuggerStepThrough]
    internal sealed class StoryboardTargetTimelineEqualityComparer
        : Singleton<StoryboardTargetTimelineEqualityComparer>, IEqualityComparer<Timeline>
    {

        private StoryboardTargetTimelineEqualityComparer() { }

        public bool Equals(Timeline a, Timeline b)
        {
            var sbA = new StoryboardTargetProperties(a);
            var sbB = new StoryboardTargetProperties(b);

            return AreTargetsAndTargetNamesEqual(sbA, sbB) &&
                   ArePropertyPathsEqual(sbA, sbB) &&
                   ArePropertyPathParametersEqual(sbA, sbB);
        }

        private bool AreTargetsAndTargetNamesEqual(
            StoryboardTargetProperties targetPropsA, StoryboardTargetProperties targetPropsB)
        {
            if (targetPropsA.TargetName == null)
            {
                if (targetPropsA.Target == null)
                {
                    return targetPropsB.Target == null && targetPropsB.TargetName == null;
                }
                else
                {
                    return targetPropsA.Target == targetPropsB.Target;
                }
            }
            else
            {
                return targetPropsA.TargetName == targetPropsB.TargetName;
            }
        }

        private bool ArePropertyPathsEqual(StoryboardTargetProperties targetPropsA, StoryboardTargetProperties targetPropsB)
        {
            return targetPropsA.TargetProperty.Path == targetPropsB.TargetProperty.Path;
        }

        private bool ArePropertyPathParametersEqual(
            StoryboardTargetProperties targetPropsA, StoryboardTargetProperties targetPropsB)
        {
            var parametersA = targetPropsA.TargetProperty.PathParameters;
            var parametersB = targetPropsB.TargetProperty.PathParameters;
            return parametersA.SequenceEqual(parametersB);
        }

        public int GetHashCode(Timeline timelines)
        {
            var targetProps = new StoryboardTargetProperties(timelines);
            int targetHash = targetProps.Target?.GetHashCode() ?? 0;
            int targetNameHash = targetProps.TargetName?.GetHashCode() ?? 0;
            int targetPropertyHash = targetProps.TargetProperty?.Path?.GetHashCode() ?? 0;

            return (targetProps.TargetName == null ? targetHash : targetNameHash) ^ targetPropertyHash;
        }



        private struct StoryboardTargetProperties
        {
            public DependencyObject Target;
            public string TargetName;
            public PropertyPath TargetProperty;

            public StoryboardTargetProperties(Timeline timeline)
            {
                Target = Storyboard.GetTarget(timeline);
                TargetName = Storyboard.GetTargetName(timeline);
                TargetProperty = Storyboard.GetTargetProperty(timeline);
            }
        }

    }

}
