namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Identifies a common interface for helper classes,
    /// which calculate values that are required for animations.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the animated object.
    /// </typeparam>
    internal interface IAnimatedTypeHelper<T>
    {

        /// <summary>
        /// Returns an object of type <typeparamref name="T"/> which represents
        /// a zero-value.
        /// For instance, if <typeparamref name="T"/> was <see cref="int"/>, this
        /// would return 0.
        /// </summary>
        /// <returns>A zero-value for the type <typeparamref name="T"/>.</returns>
        T GetZeroValue();

        /// <summary>
        /// Adds the two specified values and returns the result.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The result of the addition of the two values.</returns>
        T AddValues(T a, T b);

        /// <summary>
        /// Subtracts the two specified values and returns the result.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <returns>The result of the subtraction of the two values.</returns>
        T SubtractValues(T a, T b);

        /// <summary>
        /// Scales the specified <paramref name="value"/> by a <paramref name="factor"/>.
        /// </summary>
        /// <param name="value">The value to be scaled.</param>
        /// <param name="factor">The factor by which the value should be scaled.</param>
        /// <returns>
        /// The result of the scaling.
        /// </returns>
        T ScaleValue(T value, double factor);

        /// <summary>
        /// Returns the result of the interpolation of the two <paramref name="from"/> and <paramref name="to"/>
        /// values at the provided progress increment.
        /// </summary>
        /// <param name="from">The value from which the animation starts.</param>
        /// <param name="to">The value at which the animation ends.</param>
        /// <param name="progress">
        /// A value between 0.0 and 1.0, inclusive, that specifies the percentage of time
        /// that has elapsed during this animation.
        /// </param>
        /// <returns>The output value of the interpolation, given the specified values.</returns>
        T InterpolateValue(T from, T to, double progress);

    }

}
