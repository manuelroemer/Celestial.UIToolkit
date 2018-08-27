using System;
using System.Collections;
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
        /// Returns the index of the specified <paramref name="value"/> in the sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="value">The value to be found.</param>
        /// <returns>
        /// The index of the found element; -1 if nothing was found.
        /// </returns>
        public static int IndexOf<T>(this IEnumerable<T> enumerable, T value)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (enumerable is IList list)
                return list.IndexOf(value);

            IEnumerator enumerator = enumerable.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                if (object.Equals(value, enumerator.Current))
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// Adds the specified sequence to the <paramref name="collection"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="items">The items to be added to the collection.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            if (collection is List<T> list)
            {
                list.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    collection.Add(item);
                }
            }
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
        
    }

}
