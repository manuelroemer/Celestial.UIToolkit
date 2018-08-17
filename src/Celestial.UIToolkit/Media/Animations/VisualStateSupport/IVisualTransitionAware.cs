using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Defines an element which is aware of a visual transition which gets
    /// managed by the <see cref="ExtendedVisualStateManager"/>.
    /// Such an element can create custom transition timelines and thus,
    /// fully benefits from the transitioning logic of the <see cref="ExtendedVisualStateManager"/>.
    /// </summary>
    public interface IVisualTransitionAware
    {

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions away from
        /// the element.
        /// The timeline which gets returned by this method is then used as a transitioning
        /// animation.
        /// </summary>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition away from this element.
        /// </returns>
        Timeline CreateFromTransitionTimeline(IEasingFunction easingFunction);

        /// <summary>
        /// Called when the <see cref="ExtendedVisualStateManager"/> transitions to the element.
        /// The timeline which gets returned by this method is then used as a transitioning animation.
        /// </summary>
        /// <param name="easingFunction">
        /// An easing function to be applied to the resulting timeline.
        /// Can be null.
        /// </param>
        /// <returns>
        /// A <see cref="Timeline"/> which displays a visual transition to this element.
        /// </returns>
        Timeline CreateToTransitionTimeline(IEasingFunction easingFunction);

    }

}
