using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for arrays.
    /// </summary>
    public static class ArrayExtensions
    {

        /// <summary>
        /// Returns a set of segments which identify certain groups in an array.
        /// </summary>
        /// <typeparam name="T">The array's type.</typeparam>
        /// <param name="array">The array to be split into segments.</param>
        /// <param name="isElementPartOfSegment">
        /// A function which is used to determine if an element belongs to the segment groups.
        /// </param>
        /// <returns>The set of array segments.</returns>
        /// <example>
        /// Say that you have an array of numbers and want to retrieve groups of even numbers.
        /// This method will return each group segment which satisfies that conditions.
        /// 
        /// For example:
        /// 
        /// [1, 2, 4, 6, 3, 5, 2, 9, 10, 12]
        ///    [^  ^  ^]      [^]    [^   ^]   ^= 3 segments of even numbers.
        /// </example>
        public static IEnumerable<ArraySegment<T>> GetGroupSegments<T>(
            this T[] array, Func<T, bool> isElementPartOfSegment)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (isElementPartOfSegment == null) throw new ArgumentNullException(nameof(isElementPartOfSegment));

            const int NoSegment = -1;
            int currentSegmentStartIndex = NoSegment;
            for (int i = 0; i < array.Length; i++)
            {
                if (isElementPartOfSegment(array[i]))
                {
                    if (currentSegmentStartIndex == NoSegment)
                        currentSegmentStartIndex = i;
                }
                else
                {
                    // Did a segment end?
                    if (currentSegmentStartIndex != NoSegment)
                    {
                        int segmentLength = i - currentSegmentStartIndex;
                        yield return new ArraySegment<T>(
                            array, 
                            currentSegmentStartIndex, 
                            segmentLength);

                        currentSegmentStartIndex = -1;
                    }
                }
            }

            if (currentSegmentStartIndex != NoSegment)
            {
                int segmentLength = array.Length - currentSegmentStartIndex;
                yield return new ArraySegment<T>(
                            array,
                            currentSegmentStartIndex,
                            segmentLength);
            }
        }
        
    }

}
