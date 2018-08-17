using Celestial.UIToolkit.Common;
using Celestial.UIToolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (this.Group.GetCurrentState() == this.ToState)
                return true;

            _currentTransition = this.GetCurrentVisualTransition();
            _dynamicTransitionStoryboard = this.CreateDynamicTransitionStoryboard();
            
            if (_currentTransition == null || _currentTransition.HasZeroDuration())
            {
                // Without a transition, the animation ToState animation is supposed to start immediately.
                // That's also the case, if the transition's duration is 0 (because it won't take any time to complete).
                this.Group.StartNewThenStopOldStoryboards(
                    this.StateGroupsRoot, _currentTransition?.Storyboard, this.ToState.Storyboard);
            }
            else
            {
                // We have a transition animation which has a duration > 0.
                // -> Run the transition storyboard before the ToState storyboard.
                _currentTransition.SetDynamicStoryboardCompleted(false);
                _dynamicTransitionStoryboard.Completed += this._transitionStoryboard_Completed;
                
                if (_currentTransition.Storyboard != null &&
                    _currentTransition.GetExplicitStoryboardCompleted())
                {
                    _currentTransition.SetExplicitStoryboardCompleted(false);
                    _currentTransition.Storyboard.Completed += _currentTransitionStoryboard_Completed;
                }

                this.Group.StartNewThenStopOldStoryboards(this.StateGroupsRoot, _currentTransition.Storyboard, _dynamicTransitionStoryboard);
            }

            this.Group.SetCurrentState(this.ToState);
            return true;
        }

        private void _transitionStoryboard_Completed(object sender, EventArgs e)
        {
            _dynamicTransitionStoryboard.Completed -= _transitionStoryboard_Completed;
            _currentTransition.SetDynamicStoryboardCompleted(true);

            if (_currentTransition.Storyboard != null || _currentTransition.GetExplicitStoryboardCompleted())
            {
                if (this.ShouldRunStateStoryboard())
                {
                    this.Group.StartNewThenStopOldStoryboards(this.StateGroupsRoot, this.ToState.Storyboard);
                }
            }
        }

        private void _currentTransitionStoryboard_Completed(object sender, EventArgs e)
        {
            _currentTransition.Storyboard.Completed -= _currentTransitionStoryboard_Completed;
            _currentTransition.SetExplicitStoryboardCompleted(true);

            if (_currentTransition.GetDynamicStoryboardCompleted() &&
                this.ShouldRunStateStoryboard())
            {
                this.Group.StartNewThenStopOldStoryboards(this.StateGroupsRoot, this.ToState.Storyboard);
            }
        }

        private bool ShouldRunStateStoryboard()
        {
            // Ensure that the controls are loaded/in a tree, so that the storyboards can find them.
            // IsLoaded never gets set to false when unloading, so make use of the Parent property.
            // (Should possibly be updated with better unloaded-detection).
            var rootParent = VisualTreeHelper.GetParent(this.StateGroupsRoot);
            var controlParent = VisualTreeHelper.GetParent(this.Control);
            return rootParent != null &&
                   this.StateGroupsRoot.IsLoaded &&
                   controlParent != null &&
                   this.Control.IsLoaded;
        }

        private VisualTransition GetCurrentVisualTransition()
        {
            if (!this.UseTransitions) return null;
            VisualTransition result = null;

            foreach (VisualTransition transition in this.Group.Transitions)
            {                
                // We want to find the transition which matches the current states the best.
                // -> If there is a transition whose From/To properties match both states, use it.
                //    If not, use a transition which matches only one state.
                //    If none of that is found, use a transition without any From/To values (a default one).
                if (this.IsTransitionBetterMatch(result, transition))
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
                VisualState transitionFromState = this.Group.GetStateByName(transition.From);
                VisualState transitionToState = this.Group.GetStateByName(transition.To);

                if (this.FromState == transitionFromState && this.ToState == transitionToState)
                    return PerfectMatch;
                else if (this.ToState == transitionToState && transitionFromState == null)
                    return ToMatch;
                else if (this.FromState == transitionFromState && transitionToState == null)
                    return FromMatch;
                else if (transition.IsDefault())
                    return DefaultTransitionMatch;
                else
                    return NoMatch;
            }
        }

        private Storyboard CreateDynamicTransitionStoryboard()
        {
            Storyboard storyboard = new Storyboard()
            {
                Duration = _currentTransition?.GeneratedDuration ?? new Duration(TimeSpan.Zero)
            };
            var easingFunction = _currentTransition?.GeneratedEasingFunction;

            ISet<Timeline> currentGroupTimelines = this.FlattenTimelines(this.Group.GetCurrentStoryboards().ToArray());
            ISet<Timeline> transitionTimelines = this.FlattenTimelines(_currentTransition?.Storyboard);
            ISet<Timeline> toStateTimelines = this.FlattenTimelines(this.ToState.Storyboard);

            currentGroupTimelines.ExceptWith(transitionTimelines);
            toStateTimelines.ExceptWith(transitionTimelines);
            
            foreach (var timeline in toStateTimelines)
            {
                var toAnimation = this.GenerateToAnimation(timeline, easingFunction);
                AddTimelineToCurrentStoryboard(toAnimation);
                currentGroupTimelines.Remove(timeline);
            }

            foreach (var timeline in currentGroupTimelines)
            {
                var fromAnimation = this.GenerateFromAnimation(timeline, easingFunction);
                AddTimelineToCurrentStoryboard(fromAnimation);
            }
            return storyboard;

            void AddTimelineToCurrentStoryboard(Timeline timeline)
            {
                if (timeline != null)
                {
                    timeline.Duration = storyboard.Duration;
                    storyboard.Children.Add(timeline);
                }
            }
        }

        private Timeline GenerateToAnimation(Timeline timeline, IEasingFunction easingFunction)
        {
            Timeline generatedTimeline = null;
            if (timeline is IVisualTransitionAware visualTransitionAware)
            {
                generatedTimeline = visualTransitionAware.CreateToTransitionTimeline();
            }

            StoryboardHelper.CopyTargetProperties(this.StateGroupsRoot, timeline, generatedTimeline);
            return generatedTimeline;
        }
        
        private Timeline GenerateFromAnimation(Timeline timeline, IEasingFunction easingFunction)
        {
            Timeline generatedTimeline = null;
            if (timeline is IVisualTransitionAware visualTransitionAware)
            {
                generatedTimeline = visualTransitionAware.CreateFromTransitionTimeline();
            }

            StoryboardHelper.CopyTargetProperties(this.StateGroupsRoot, timeline, generatedTimeline);
            return generatedTimeline;
        }
        
        private ISet<Timeline> FlattenTimelines(params Storyboard[] storyboards)
        {
            // A storyboard can have other storyboards as children.
            // The goal of this method is to extract a single list of all timelines from the storyboard(s).
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
                    else
                    {
                        results.Remove(timeline);
                        results.Add(timeline);
                    }
                }
            }
        }

    }
    
}
