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

        /// <summary>
        /// Removes all elements from the list (in place, the input list will be changed) 
        /// which satisfy the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">A predicate to be fulfilled for an item to be removed.</param>
        /// <returns>
        /// The same <paramref name="list"/> instance.
        /// </returns>
        public static IList<T> RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                    list.RemoveAt(i);
            }
            return list;
        }

        /// <summary>
        /// Adds an element to the specified <paramref name="set"/>.
        /// If an element equal to the current exists in the set,
        /// it will be replaced with the new one.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The set.</param>
        /// <param name="element">The element to be added to the set.</param>
        public static void AddOrReplace<T>(this ISet<T> set, T element)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            set.Remove(element);
            set.Add(element);
        }

    }

}
