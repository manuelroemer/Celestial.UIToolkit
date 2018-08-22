using Celestial.UIToolkit.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An equality comparer for <see cref="Timeline"/> objects
    /// which compares them based on attached <see cref="Storyboard"/> properties
    /// which define a timeline's target.
    /// </summary>
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
