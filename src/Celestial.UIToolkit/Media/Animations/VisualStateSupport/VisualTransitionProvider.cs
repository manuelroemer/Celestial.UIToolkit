using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Defines an element which can generate a visual transition for a specific type
    /// of <see cref="Timeline"/>.
    /// This interface is being used by the <see cref="VisualTransitionProvider"/>
    /// and <see cref="ExtendedVisualStateManager"/> to create visual transitions
    /// between animations in different visual states.
    /// </summary>
    public interface IVisualTransitionProvider
    {

        /// <summary>
        /// Returns a value indicating whether this provider can generate
        /// a visual transition for the specified type of timeline.
        /// </summary>
        /// <param name="timeline">
        /// The <see cref="Timeline"/> for which a visual transition is supposed to
        /// be generated.
        /// </param>
        /// <returns>
        /// <c>true</c> if the provider can create a visual transition for the <paramref name="timeline"/>;
        /// <c>false</c> if not.
        /// </returns>
        bool SupportsTimeline(Timeline timeline);

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions away from
        /// the element.
        /// The timeline which gets returned by this method is then used as a transitioning
        /// animation.
        /// </summary>
        /// <param name="fromTimeline">
        /// The input timeline, for which a visual transition timeline should be generated.
        /// The VisualStateManager wants to transition away from this timeline.
        /// </param>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition away from this element.
        /// </returns>
        Timeline CreateFromTransitionTimeline(Timeline fromTimeline, IEasingFunction easingFunction);

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions to the element.
        /// The timeline which gets returned by this method is then used as a transitioning animation.
        /// </summary>
        /// <param name="toTimeline">
        /// The input timeline, for which a visual transition timeline should be generated.
        /// The VisualStateManager wants to transition to this timeline.
        /// </param>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition to this element.
        /// </returns>
        Timeline CreateToTransitionTimeline(Timeline toTimeline, IEasingFunction easingFunction);

    }
    
    /// <summary>
    /// A class which provides the <see cref="ExtendedVisualStateManager"/>
    /// with transition timelines between states.
    /// </summary>
    public static class VisualTransitionProvider
    {

        private static List<IVisualTransitionProvider> _transitionProviders = new List<IVisualTransitionProvider>();

        static VisualTransitionProvider()
        {
            // These animations are supported by the default WPF VisualStateManager.
            // We will create them ourselves, so that we don't have to rely on the original one
            // at all.
            _transitionProviders.Add(new PointAnimationTransitionProvider());
            _transitionProviders.Add(new DoubleAnimationTransitionProvider());
            _transitionProviders.Add(new ColorAnimationTransitionProvider());
        }

        /// <summary>
        /// Adds the specified transition provider to the supported ones.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IVisualTransitionProvider"/> to be added to the providers
        /// which are used by the <see cref="ExtendedVisualStateManager"/>.
        /// </param>
        public static void AddProvider(IVisualTransitionProvider provider)
        {
            if (provider != null)
            {
                _transitionProviders.Add(provider);
            }
        }

        /// <summary>
        /// Returns a registered <see cref="IVisualTransitionProvider"/> which supports
        /// the specified <paramref name="timeline"/>.
        /// </summary>
        /// <param name="timeline">
        /// The <see cref="Timeline"/>.
        /// </param>
        /// <returns>
        /// A <see cref="IVisualTransitionProvider"/> or <c>null</c>, if none was found.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no added provider supports the <paramref name="timeline"/>.
        /// </exception>
        public static IVisualTransitionProvider GetProviderForTimeline(Timeline timeline)
        {
            if (TryGetProviderForTimeline(timeline, out var provider))
            {
                return provider;
            }
            else
            {
                throw new InvalidOperationException(
                    $"No provider has been registered for the specified timeline.");
            }
        }

        /// <summary>
        /// Tries to return a registered <see cref="IVisualTransitionProvider"/> which supports
        /// the specified <paramref name="timeline"/>.
        /// If a provider was found, this method returns <c>true</c> and stores the provider
        /// in the <paramref name="result"/> parameter.
        /// If none was found, this method returns <c>false</c> and <paramref name="result"/>
        /// is set to <c>null</c>.
        /// </summary>
        /// <param name="timeline">
        /// The <see cref="Timeline"/>.
        /// </param>
        /// <param name="result">
        /// An <see cref="IVisualTransitionProvider"/> which will hold the result of this method.
        /// </param>
        /// <returns>
        /// A <see cref="Boolean"/> indicating whether getting a provider succeeded;
        /// </returns>
        public static bool TryGetProviderForTimeline(Timeline timeline, out IVisualTransitionProvider result)
        {
            if (timeline == null) throw new ArgumentNullException(nameof(timeline));

            // Allow timelines to implement the interface themselves.
            // This allows them to generate the transitions on their own, without an external class.
            // This is, for example, used by the FromToByAnimationBase, which automatically provides
            // custom transitions for each animation that derives from it.
            if (timeline is IVisualTransitionProvider selfAwareTimelineProvider &&
                selfAwareTimelineProvider.SupportsTimeline(timeline))
            {
                result = selfAwareTimelineProvider;
                return true;
            }

            // If the timeline doesn't self provide the values, check for a registered provider.
            return TryFindRegisteredProviderForTimeline(timeline, out result);
        }

        private static bool TryFindRegisteredProviderForTimeline(
            Timeline timeline, out IVisualTransitionProvider result)
        {
            // Go through the list from behind to allow overwriting of the standard providers.
            for (int i = _transitionProviders.Count - 1; i >= 0; i--)
            {
                IVisualTransitionProvider currentProvider = _transitionProviders[i];
                if (currentProvider.SupportsTimeline(timeline))
                {
                    result = currentProvider;
                    return true;
                }
            }

            result = null;
            return false;
        }

    }

    internal sealed class ColorAnimationTransitionProvider : IVisualTransitionProvider
    {
        public bool SupportsTimeline(Timeline timeline) =>
            timeline is ColorAnimation || timeline is ColorAnimationUsingKeyFrames;

        public Timeline CreateFromTransitionTimeline(Timeline fromTimeline, IEasingFunction easingFunction)
        {
            return new ColorAnimation()
            {
                EasingFunction = easingFunction
            };
        }

        public Timeline CreateToTransitionTimeline(Timeline toTimeline, IEasingFunction easingFunction)
        {
            Color targetColor = new Color();
            if (toTimeline is ColorAnimation colorAnim)
            {
                targetColor = colorAnim.From ?? colorAnim.To ?? new Color();
            }
            else if (toTimeline is ColorAnimationUsingKeyFrames colorKeyFrameAnim)
            {
                if (colorKeyFrameAnim.KeyFrames != null && colorKeyFrameAnim.KeyFrames.Count > 0)
                    targetColor = colorKeyFrameAnim.KeyFrames[0].Value;
            }

            return new ColorAnimation()
            {
                To = targetColor,
                EasingFunction = easingFunction
            };
        }
    }

    internal sealed class DoubleAnimationTransitionProvider : IVisualTransitionProvider
    {
        public bool SupportsTimeline(Timeline timeline) =>
            timeline is DoubleAnimation || timeline is DoubleAnimationUsingKeyFrames;

        public Timeline CreateFromTransitionTimeline(Timeline fromTimeline, IEasingFunction easingFunction)
        {
            return new DoubleAnimation()
            {
                EasingFunction = easingFunction
            };
        }

        public Timeline CreateToTransitionTimeline(Timeline toTimeline, IEasingFunction easingFunction)
        {
            double targetValue = 0d;
            if (toTimeline is DoubleAnimation doubleAnim)
            {
                targetValue = doubleAnim.From ?? doubleAnim.To ?? 0d;
            }
            else if (toTimeline is DoubleAnimationUsingKeyFrames doubleKeyFrameAnim)
            {
                if (doubleKeyFrameAnim.KeyFrames != null && doubleKeyFrameAnim.KeyFrames.Count > 0)
                    targetValue = doubleKeyFrameAnim.KeyFrames[0].Value;
            }

            return new DoubleAnimation()
            {
                To = targetValue,
                EasingFunction = easingFunction
            };
        }
    }

    internal sealed class PointAnimationTransitionProvider : IVisualTransitionProvider
    {
        public bool SupportsTimeline(Timeline timeline) =>
            timeline is PointAnimation || timeline is PointAnimationUsingKeyFrames;

        public Timeline CreateFromTransitionTimeline(Timeline fromTimeline, IEasingFunction easingFunction)
        {
            return new PointAnimation()
            {
                EasingFunction = easingFunction
            };
        }

        public Timeline CreateToTransitionTimeline(Timeline toTimeline, IEasingFunction easingFunction)
        {
            Point targetPoint = new Point(0d, 0d);
            if (toTimeline is PointAnimation pointAnim)
            {
                targetPoint = pointAnim.From ?? pointAnim.To ?? new Point(0d, 0d);
            }
            else if (toTimeline is PointAnimationUsingKeyFrames pointKeyFrameAnim)
            {
                if (pointKeyFrameAnim.KeyFrames != null && pointKeyFrameAnim.KeyFrames.Count > 0)
                    targetPoint = pointKeyFrameAnim.KeyFrames[0].Value;
            }

            return new PointAnimation()
            {
                To = targetPoint,
                EasingFunction = easingFunction
            };
        }
    }

}
