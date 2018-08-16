using Celestial.UIToolkit.Common;
using Celestial.UIToolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private Storyboard _transitionStoryboard;

        protected override bool GoToStateCore()
        {
            _currentTransition = this.GetCurrentVisualTransition();
            _transitionStoryboard = this.CreateTransitionStoryboard();
            
            return false;
        }

        private VisualTransition GetCurrentVisualTransition()
        {
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

        private Storyboard CreateTransitionStoryboard()
        {
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = _currentTransition?.GeneratedDuration ?? 
                                  new Duration(TimeSpan.Zero);

            ISet<Timeline> groupTimelines = this.FlattenTimelines(null); // TODO: group.CurrentStoryboards replacement
            ISet<Timeline> transitionTimelines = this.FlattenTimelines(_currentTransition?.Storyboard);
            ISet<Timeline> toStateTimelines = this.FlattenTimelines(this.ToState.Storyboard);
            
            foreach (var transitionTimeline in transitionTimelines)
            {
                groupTimelines.Remove(transitionTimeline);
                toStateTimelines.Remove(transitionTimeline);
            }

            foreach (var timeline in toStateTimelines)
            {
                var toAnimation = this.GenerateToAnimation(timeline);
                AddTimelineToCurrentStoryboard(toAnimation);
                groupTimelines.Remove(timeline);
            }

            foreach (var timeline in groupTimelines)
            {
                var fromAnimation = this.GenerateFromAnimation(timeline);
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

        private Timeline GenerateToAnimation(Timeline timeline)
        {
            // TODO
            return null;
        }

        private Timeline GenerateFromAnimation(Timeline timeline)
        {
            // TODO
            return null;
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
                        results.AddOrReplace(timeline);
                    }
                }
            }
        }

        /// <summary>
        /// An equality comparer for <see cref="Timeline"/> objects
        /// which compares them based on attached storyboard properties
        /// which define a timeline's target.
        /// </summary>
        private sealed class StoryboardTargetTimelineEqualityComparer 
            : Singleton<StoryboardTargetTimelineEqualityComparer>, IEqualityComparer<Timeline>
        {
            private StoryboardTargetTimelineEqualityComparer() { }

            public bool Equals(Timeline a, Timeline b)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(Timeline timelines)
            {
                throw new NotImplementedException();
            }
        }

    }
    
}
