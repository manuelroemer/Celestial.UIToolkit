using System;
using System.Linq;
using Celestial.UIToolkit.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Extensions
{

    [TestClass]
    public class GetGroupSegmentsTests
    {

        [TestMethod]
        public void ReturnsCorrectNumberOfSegments()
        {
            TestSegmentCount(
                new int[] { 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1 },
                4);
            TestSegmentCount(
                new int[] { 0, 0, 0, 0, 0, 0, 0 },
                0);
        }
        
        private void TestSegmentCount(int[] numbers, int expectedSegments)
        {
            var segments = numbers.GetGroupSegments(num => num == 1);
            Assert.AreEqual(expectedSegments, segments.Count());
        }

        [TestMethod]
        public void SegmentsHaveRightIndices()
        {
            var numbers = new int[] { 0, 1, 1, 1, 0, 0, 1 };
        }

        [TestMethod]
        public void SegmentsHaveCorrectValuesAndRanges()
        {
            var numbers = new int[] { 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 0 };
            var number2 = new int[] { 1, 1, 1, 0, 0, 1, 1, 0, 1, 0, 1, 0, 1 };

            foreach (var segment in numbers.GetGroupSegments(num => num == 1))
                VerifySegmentIntegrity(numbers, segment);

            foreach (var segment in number2.GetGroupSegments(num => num == 1))
                VerifySegmentIntegrity(number2, segment);
        }
        
        private void VerifySegmentIntegrity(int[] numbers, ArraySegment<int> segment)
        {
            Assert.IsTrue(segment.All(num => num == 1));

            if (segment.Offset > 0)
            {
                Assert.AreNotEqual(1, numbers[segment.Offset - 1]);
            }

            if ((segment.Offset + segment.Count) < numbers.Length)
            {
                Assert.AreNotEqual(1, numbers[segment.Offset + segment.Count]);
            }
        }

    }

}

