using System;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="ArraySegment{T}"/> structure.
    /// </summary>
    public static class ArraySegmentExtensions
    {

        /// <summary>
        /// Returns a value indicating whether the segment includes
        /// the item at the specified <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <param name="index">The index to be checked.</param>
        /// <returns>
        /// <c>true</c> if the segment includes the item at the specified <paramref name="index"/>;
        /// <c>false</c> if not.
        /// </returns>
        public static bool IncludesElementAt<T>(this ArraySegment<T> segment, int index)
        {
            if (index < 0 || index >= segment.Array.Length)
                throw new IndexOutOfRangeException(nameof(index));
            return index >= segment.Offset &&
                   index <= segment.Offset + segment.Count - 1;
        }

        /// <summary>
        /// Returns a value indicating whether the segment includes
        /// the underlying array's first item.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>
        /// <c>true</c> if the segment includes the underlying array's first item;
        /// <c>false</c> if not.
        /// </returns>
        public static bool IncludesFirstArrayItem<T>(this ArraySegment<T> segment)
        {
            return segment.IncludesElementAt(0);
        }

        /// <summary>
        /// Returns a value indicating whether the segment includes
        /// the underlying array's last item.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>
        /// <c>true</c> if the segment includes the underlying array's last item;
        /// <c>false</c> if not.
        /// </returns>
        public static bool IncludesLastArrayItem<T>(this ArraySegment<T> segment)
        {
            return segment.IncludesElementAt(segment.Array.Length - 1);
        }
        
    }

}
