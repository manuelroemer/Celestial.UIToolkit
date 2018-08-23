using Celestial.UIToolkit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Celestial.UIToolkit.Tests.Extensions
{

    public class ArrayExtensionsTests
    {

        [Theory]
        [ClassData(typeof(SegmentTestDataSource))]
        public void GetGroupSegmentsTest(int[] array, IEnumerable<ArraySegment<int>> expectedSegments)
        {
            Func<int, bool> IsEven = (num) => num % 2 == 0;
            var groupSegments = array.GetGroupSegments(IsEven);
            Assert.Equal(expectedSegments, groupSegments);
        }

        private class SegmentTestDataSource : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new int[0],
                    new ArraySegment<int>[0]
                };

                yield return new object[]
                {
                    new int[] { 1, 1, 1, 1, 1 },
                    new ArraySegment<int>[0]
                };

                int[] evenOnlyArray = new int[] { 2, 2, 2, 2, 2 };
                yield return new object[]
                {
                    evenOnlyArray,
                    new ArraySegment<int>[]
                    {
                        new ArraySegment<int>(evenOnlyArray, 0, evenOnlyArray.Length)
                    }
                };

                int[] startEndSegmentsArray = new int[] { 2, 2, 2, 1, 2, 2, 2 };
                yield return new object[]
                {
                    startEndSegmentsArray,
                    new ArraySegment<int>[]
                    {
                        new ArraySegment<int>(startEndSegmentsArray, 0, 3),
                        new ArraySegment<int>(startEndSegmentsArray, 4, 3),
                    }
                };

                int[] middleSegmentArray = new int[] { 1, 2, 1 };
                yield return new object[]
                {
                    middleSegmentArray,
                    new ArraySegment<int>[]
                    {
                        new ArraySegment<int>(middleSegmentArray, 1, 1)
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }

}
