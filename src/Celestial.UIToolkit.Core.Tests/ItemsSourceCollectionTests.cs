using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Celestial.UIToolkit.Tests
{

    public class ItemsSourceCollectionTests
    {

        [Fact]
        public void IsUsingItemsSourceGetsSet()
        {
            var itemsSource = CreateIntItemsSource(1);
            var emptyCollection = new ItemsSourceCollection();
            Assert.False(emptyCollection.IsUsingItemsSource);

            emptyCollection.ItemsSource = itemsSource;
            Assert.True(emptyCollection.IsUsingItemsSource);
        }

        [Fact]
        public void CanAddItemsNormally()
        {
            var collection = new ItemsSourceCollection();
            int itemToAdd = 1;
            collection.Add(itemToAdd);
            Assert.Contains(itemToAdd, collection);
        }

        [Fact]
        public void ProvidesItemsViaItemsSource()
        {
            var itemsSource = CreateIntItemsSource(5);
            var collection = new ItemsSourceCollection();

            collection.ItemsSource = itemsSource;
            Assert.True(collection.Cast<int>().SequenceEqual(itemsSource));
        }

        [Fact]
        public void ThrowsWhenSwitchingItemsSourceInFilledCollection()
        {
            var itemsSource = CreateIntItemsSource(1);
            var collection = new ItemsSourceCollection() { 1, 2, 3 };

            var ex = Record.Exception(() => collection.ItemsSource = itemsSource);
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
        }

        [Fact]
        public void ThrowsWhenModifyingCollectionWithItemsSource()
        {
            var itemsSource = CreateIntItemsSource(5);
            var collection = new ItemsSourceCollection();

            collection.ItemsSource = itemsSource;
            TestForMethod(() => collection.Add(0));
            TestForMethod(() => collection.Remove(0));
            TestForMethod(() => collection.RemoveAt(0));
            TestForMethod(() => collection.Clear());
            Assert.True(collection.Cast<int>().SequenceEqual(itemsSource));

            void TestForMethod(Action method)
            {
                var ex = Record.Exception(method);
                Assert.NotNull(ex);
                Assert.IsType<InvalidOperationException>(ex);
            }
        }

        [Fact]
        public void CanUseNonEnumerableItemsSource()
        {
            var itemsSource = CreateNonEnumerableItemsSource();
            var collection = new ItemsSourceCollection();

            collection.ItemsSource = itemsSource;
        }

        /// <summary>
        /// Creates an items source with the specified number of items in it.
        /// </summary>
        private int[] CreateIntItemsSource(int itemCount)
        {
            itemCount = Math.Max(0, itemCount);
            int[] itemsSource = new int[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                itemsSource[i] = i;
            }
            return itemsSource;
        }

        private object CreateNonEnumerableItemsSource()
        {
            // any object is acceptable. just give it a name for convenience.
            return new { Name = "Non Enumerable Items Source" };
        }

    }

}
