using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
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
    public class AnimationVisualStateSwitcher : VisualStateSwitcher
    {

        /// <summary>
        /// Transitions to another state by generating dynamic transitioning animations.
        /// </summary>
        /// <returns>
        /// Returns <c>true</c>, if the method could transition to another state.
        /// If the current <see cref="VisualStateGroup"/> has the same state as the target state,
        /// nothing will happen and this method returns <c>false</c>.
        /// </returns>
        protected override bool GoToStateCore()
        {
            if (Group.GetCurrentState() == ToState) return false;

            VisualTransition currentTransition = GetCurrentVisualTransition();
            Storyboard dynamicTransitionStoryboard = CreateDynamicTransitionStoryboard(currentTransition);
            
            if (currentTransition == null || currentTransition.HasZeroDuration())
            {
                // Without a transition (or a transition which has no duration), the animations 
                // defined in the ToState are supposed to start immediately.
                PlayToStateAnimations(currentTransition);
            }
            else
            {
                PlayTransitionAnimations(currentTransition, dynamicTransitionStoryboard);
            }

            Group.SetCurrentState(ToState);
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
            currentTransition.SetDynamicStoryboardCompleted(false);
            dynamicTransitionStoryboard.Completed += dynamicStoryboardCompletedHandler;

            // If a storyboard has been defined INSIDE the VisualTransition 
            // (-> explicit storyboard), play that aswell.
            if (currentTransition.Storyboard != null &&
                currentTransition.GetExplicitStoryboardCompleted())
            {
                currentTransition.SetExplicitStoryboardCompleted(false);
                currentTransition.Storyboard.Completed += currentStoryboardCompletedHandler;
            }

            Group.StartNewThenStopOldStoryboards(
                StateGroupsRoot,
                currentTransition.Storyboard,
                dynamicTransitionStoryboard);
        }

        private void OnDynamicTransitionStoryboardCompleted(
            Storyboard dynamicTransitionStoryboard, VisualTransition currentTransition)
        {
            if (currentTransition.Storyboard != null || 
                currentTransition.GetExplicitStoryboardCompleted())
            {
                if (ShouldRunStateStoryboard())
                {
                    Group.StartNewThenStopOldStoryboards(StateGroupsRoot, ToState.Storyboard);
                }
            }
            currentTransition.SetDynamicStoryboardCompleted(true);
        }

        private void OnCurrentTransitionStoryboardCompleted(VisualTransition currentTransition)
        {
            if (currentTransition.GetDynamicStoryboardCompleted() &&
                ShouldRunStateStoryboard())
            {
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
                   controlParent != null &&
                   Control.IsLoaded &&
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

            currentGroupTimelines.ExceptWith(transitionTimelines);
            toStateTimelines.ExceptWith(transitionTimelines);

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
    
}
