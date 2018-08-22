using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Defines an abstract base class for a From/To/By animation,
    /// which, in comparison to the <see cref="FromToByAnimationBase{T}"/>,
    /// additionally uses an easing function.
    /// </summary>
    /// <typeparam name="T">The type which is being animated by the animation.</typeparam>
    public abstract class EasingFromToByAnimationBase<T> : FromToByAnimationBase<T>
    {
        
        /// <summary>
        /// Identifies the <see cref="EasingFunction"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            nameof(EasingFunction), typeof(IEasingFunction), typeof(EasingFromToByAnimationBase<T>));

        /// <summary>
        /// Gets or sets the easing function applied to this animation.
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        /// <summary>
        /// Applies the <see cref="EasingFunction"/> to the <paramref name="progress"/>
        /// and then calls <see cref="InterpolateValueCore(T, T, double)"/>,
        /// to get the desired interpolation result.
        /// </summary>
        /// <param name="from">The value from which the animation starts.</param>
        /// <param name="to">The value at which the animation ends.</param>
        /// <param name="progress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed during this animation.
        /// </param>
        /// <returns>The output value of the interpolation, given the specified values.</returns>
        protected override sealed T InterpolateValue(T from, T to, double progress)
        {
            if (EasingFunction != null)
            {
                progress = EasingFunction.Ease(progress);
            }
            return InterpolateValueCore(from, to, progress);
        }

        /// <summary>
        /// Returns the result of the interpolation of the two <paramref name="from"/> and <paramref name="to"/>
        /// values at the provided progress increment.
        /// </summary>
        /// <param name="from">The value from which the animation starts.</param>
        /// <param name="to">The value at which the animation ends.</param>
        /// <param name="progress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed during this animation.
        /// The local <see cref="EasingFunction"/> is already applied to this value.
        /// </param>
        /// <returns>The output value of the interpolation, given the specified values.</returns>
        protected abstract T InterpolateValueCore(T from, T to, double progress);

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions away from
        /// the element.
        /// The timeline which gets returned by this method is then used as a transitioning
        /// animation.
        /// </summary>
        /// <param name="fromTimeline">
        /// The animation for which a visual transition timeline should be generated.
        /// The VisualStateManager wants to transition away from this timeline.
        /// By default, this can only be an animation of the same type as this class.
        /// </param>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition away from this element.
        /// </returns>
        public override Timeline CreateFromTransitionTimeline(Timeline fromTimeline, IEasingFunction easingFunction)
        {
            var animation = (EasingFromToByAnimationBase<T>)base.CreateFromTransitionTimeline(
                fromTimeline, easingFunction);
            animation.EasingFunction = easingFunction;
            return animation;
        }

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions to the element.
        /// The timeline which gets returned by this method is then used as a transitioning animation.
        /// </summary>
        /// <param name="toTimeline">
        /// The animation, for which a visual transition timeline should be generated.
        /// The VisualStateManager wants to transition to this timeline.
        /// By default, this can only be an animation of the same type as this class.
        /// </param>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition to this element.
        /// </returns>
        public override Timeline CreateToTransitionTimeline(Timeline toTimeline, IEasingFunction easingFunction)
        {
            var animation = (EasingFromToByAnimationBase<T>)base.CreateToTransitionTimeline(toTimeline, easingFunction);
            animation.EasingFunction = easingFunction;
            return animation;
        }

    }

}
