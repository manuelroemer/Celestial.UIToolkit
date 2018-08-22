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
    /// which supports transitioning between custom animations defined in the
    /// <see cref="Celestial.UIToolkit.Media.Animations"/> namespace.
    /// </summary>
    /// <remarks>
    /// The default <see cref="VisualStateManager"/> does not support custom animations.
    /// As a result, this class provides support for the custom animations which are
    /// provided by the toolkit.
    /// </remarks>
    public class AnimationVisualStateSwitcher : VisualStateSwitcher
    {

        private VisualTransition _currentTransition;
        private Storyboard _dynamicTransitionStoryboard;

        protected override bool GoToStateCore()
        {
            if (Group.GetCurrentState() == ToState)
                return true;

            _currentTransition = GetCurrentVisualTransition();
            _dynamicTransitionStoryboard = CreateDynamicTransitionStoryboard();
            
            if (_currentTransition == null || _currentTransition.HasZeroDuration())
            {
                // Without a transition, the animation ToState animation is supposed to start 
                // immediately.
                // That's also the case, if the transition's duration is 0 (because it won't take 
                // any time to complete).
                Group.StartNewThenStopOldStoryboards(
                    StateGroupsRoot, _currentTransition?.Storyboard, ToState.Storyboard);
            }
            else
            {
                // We have a transition animation which has a duration > 0.
                // -> Run the transition storyboard before the ToState storyboard.
                _currentTransition.SetDynamicStoryboardCompleted(false);
                _dynamicTransitionStoryboard.Completed += DynamicTransitionStoryboard_Completed;
                
                if (_currentTransition.Storyboard != null &&
                    _currentTransition.GetExplicitStoryboardCompleted())
                {
                    _currentTransition.SetExplicitStoryboardCompleted(false);
                    _currentTransition.Storyboard.Completed += CurrentTransitionStoryboard_Completed;
                }

                Group.StartNewThenStopOldStoryboards(
                    StateGroupsRoot, 
                    _currentTransition.Storyboard,
                    _dynamicTransitionStoryboard);
            }

            Group.SetCurrentState(ToState);
            return true;
        }

        private void DynamicTransitionStoryboard_Completed(object sender, EventArgs e)
        {
            _dynamicTransitionStoryboard.Completed -= DynamicTransitionStoryboard_Completed;

            if (_currentTransition.Storyboard != null || _currentTransition.GetExplicitStoryboardCompleted())
            {
                if (ShouldRunStateStoryboard())
                {
                    Group.StartNewThenStopOldStoryboards(StateGroupsRoot, ToState.Storyboard);
                }
            }
            _currentTransition.SetDynamicStoryboardCompleted(true);
        }

        private void CurrentTransitionStoryboard_Completed(object sender, EventArgs e)
        {
            _currentTransition.Storyboard.Completed -= CurrentTransitionStoryboard_Completed;

            if (_currentTransition.GetDynamicStoryboardCompleted() &&
                ShouldRunStateStoryboard())
            {
                Group.StartNewThenStopOldStoryboards(StateGroupsRoot, ToState.Storyboard);
            }

            _currentTransition.SetExplicitStoryboardCompleted(true);
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

        private Storyboard CreateDynamicTransitionStoryboard()
        {
            var storyboard = new Storyboard();
            var easingFunction = _currentTransition?.GeneratedEasingFunction;
            storyboard.Duration = _currentTransition?.GeneratedDuration ??
                                  new Duration(TimeSpan.Zero);

            FillDynamicTransitionStoryboard(storyboard, easingFunction);
            return storyboard;
        }

        private void FillDynamicTransitionStoryboard(Storyboard storyboard, IEasingFunction easingFunction)
        {
            ISet<Timeline> currentGroupTimelines = FlattenTimelines(Group.GetCurrentStoryboards().ToArray());
            ISet<Timeline> transitionTimelines = FlattenTimelines(_currentTransition?.Storyboard);
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
