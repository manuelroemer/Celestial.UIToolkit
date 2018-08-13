using System;
using System.Collections.Generic;
using System.Linq;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Returns the element after the specified <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="index">The index which will be used to find the next element.</param>
        /// <returns>
        /// The element after the element at the specified <paramref name="index"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is not in the enumerable's range, or if
        /// the element at the specified <paramref name="index"/> is the last element in the
        /// sequence.
        /// </exception>
        public static T ElementAfter<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return enumerable.ElementAt(index + 1);
        }

        /// <summary>
        /// Returns the element after the specified <paramref name="index"/>
        /// or a default value, if the element at the specified <paramref name="index"/>
        /// is the last element in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="index">The index which will be used to find the next element.</param>
        /// <returns>
        /// The element after the element at the specified <paramref name="index"/>
        /// or a default value.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is not in the enumerable's range.
        /// </exception>
        public static T ElementAfterOrDefault<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return enumerable.ElementAtOrDefault(index + 1);
        }

        /// <summary>
        /// Returns the element which comes before the specified <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="index">The index which will be used to find the previous element.</param>
        /// <returns>
        /// The element which comes before the element at the specified <paramref name="index"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is not in the enumerable's range, or if
        /// the element at the specified <paramref name="index"/> is the first element in the
        /// sequence.
        /// </exception>
        public static T ElementBefore<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return enumerable.ElementAt(index - 1);
        }

        /// <summary>
        /// Returns the element which comes before the specified <paramref name="index"/>
        /// or a default value, if the element at the specified <paramref name="index"/>
        /// is the first element in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="index">The index which will be used to find the previous element.</param>
        /// <returns>
        /// The element which comes before the element at the specified <paramref name="index"/>
        /// or a default value.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index"/> is not in the enumerable's range.
        /// </exception>
        public static T ElementBeforeOrDefault<T>(this IEnumerable<T> enumerable, int index)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            return enumerable.ElementAtOrDefault(index - 1);
        }

    }

}
