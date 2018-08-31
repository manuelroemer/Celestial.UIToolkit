using Celestial.UIToolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Celestial.UIToolkit.Tests.Extensions
{

    public class EnumerableExtensionsTests
    {

        [Theory]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, -1)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 0)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 1)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 2)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 3)]
        public void ElementAfterReturnsCorrectValue(int[] data, int index)
        {
            Assert.Equal(
                data[index + 1],
                data.ElementAfter(index));
        }

        [Theory]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 1)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 2)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 3)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 4)]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, 5)]
        public void ElementBeforeReturnsCorrectValue(int[] data, int index)
        {
            Assert.Equal(
                data[index - 1],
                data.ElementBefore(index));
        }

        [Fact]
        public void ElementAfterOrDefaultReturnsDefaultValue()
        {
            Assert.Equal(
                default(int),
                new int[0].ElementAfterOrDefault(0));
        }

        [Fact]
        public void ElementBeforeOrDefaultReturnsDefaultValue()
        {
            Assert.Equal(
                default(int),
                new int[0].ElementBeforeOrDefault(0));
        }

        [Theory]
        [InlineData(new int[] { 0, 1, 2 }, 0, 0)]
        [InlineData(new int[] { 0, 1, 2 }, 1, 1)]
        [InlineData(new int[] { 0, 1, 2 }, 2, 2)]
        [InlineData(new int[] { 0, 1, 2 }, 3, -1)]
        public void IndexOfReturnsCorrectIndex(
            IEnumerable<int> data, int item, int expectedIndex)
        {
            int index = data.IndexOf(item);
            Assert.Equal(expectedIndex, index);
        }

        [Fact]
        public void AddRangeAddsElements()
        {
            var baseCollection = new Collection<int>();
            var nums = new int[] { 1, 2, 3 };

            baseCollection.AddRange(nums);
            Assert.Equal(nums, baseCollection);
        }

        [Fact]
        public void AddRangeThrowsForNullCollection()
        {
            var collection = new Collection<int>();
            var ex = Record.Exception(() => collection.AddRange(null));
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void RemoveAllRemovesAllItems()
        {
            var collection = new Collection<int>() { 0, 1, 2, 3, 4, 5, 6 };
            Func<int, bool> IsEven = (num) => num % 2 == 0;

            collection.RemoveAll(IsEven);
            Assert.DoesNotContain(collection, (num) => IsEven(num));
        }

    }

}
