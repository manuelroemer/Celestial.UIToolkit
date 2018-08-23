using Celestial.UIToolkit.Extensions;
using System;
using Xunit;

namespace Celestial.UIToolkit.Tests.Extensions
{

    public class ArraySegmentExtensionsTests
    {

        [Fact]
        public void IncludesElementAtReturnsCorrectValue()
        {
            var array = new int[] { 0, 1, 2, 3, 4 };
            var segment = new ArraySegment<int>(array, 1, 4);

            Assert.False(segment.IncludesElementAt(0));
            for (int i = segment.Offset; i < segment.Offset + segment.Count; i++)
            {
                Assert.True(segment.IncludesElementAt(i));
            }
        }
        
    }

}
