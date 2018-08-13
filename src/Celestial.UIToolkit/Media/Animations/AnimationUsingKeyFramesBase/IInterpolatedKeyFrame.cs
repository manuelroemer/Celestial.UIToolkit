using System;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// An extension of the <see cref="IKeyFrame"/> interface,
    /// which defines a key frame which has the ability to interpolate
    /// its value, depending on a progress factor.
    /// </summary>
    public interface IInterpolatedKeyFrame : IKeyFrame
    {

        /// <summary>
        /// Returns the interpolated value of this key frame at the provided progress increment.
        /// </summary>
        /// <param name="baseValue">The value to animate from.</param>
        /// <param name="keyFrameProgress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed for this key frame.
        /// </param>
        /// <returns>The output value of this key frame given the specified base value and progress.</returns>
        /// <exception cref="ArgumentOutOfRangeException" />
        object InterpolateValue(object baseValue, double keyFrameProgress);

    }

}
