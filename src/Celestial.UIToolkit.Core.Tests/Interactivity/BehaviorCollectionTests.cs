using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Celestial.UIToolkit.Core.Tests.Interactivity.Mocks;
using Celestial.UIToolkit.Interactivity;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{

    public class BehaviorCollectionTests
    {

        #region Generic IBehavior

        [Fact]
        public void ThrowsIfAttachingToNull()
        {
            var behavior = new BehaviorCollection();
            Assert.Throws<ArgumentNullException>(() => behavior.Attach(null));
        }

        [Fact]
        public void CannotAttachTwoObjects()
        {
            var behavior = new BehaviorCollection();
            var associatedObj1 = new DependencyObject();
            var associatedObj2 = new DependencyObject();

            behavior.Attach(associatedObj1);
            Assert.Throws<InvalidOperationException>(() => behavior.Attach(associatedObj2));
        }

        [Fact]
        public void AttachingSameElementTwiceDoesntThrow()
        {
            var behavior = new BehaviorCollection();
            var associatedObj = new DependencyObject();

            behavior.Attach(associatedObj);
            behavior.Attach(associatedObj); // If this throws, the test should fail.
        }

        #endregion

        #region Attaching

        [Fact]
        public void AttachesAllItems()
        {
            var collection = new BehaviorCollection()
            {
                new TestableBehavior(),
                new TestableBehavior(),
                new TestableBehavior()
            };
            collection.Attach(new DependencyObject());

            Assert.True(collection.IsAttached);
            foreach (var behavior in collection)
                Assert.True(behavior.IsAttached);
        }

        [Fact]
        public void DetachesAllItems()
        {
            var collection = new BehaviorCollection()
            {
                new TestableBehavior(),
                new TestableBehavior(),
                new TestableBehavior()
            };
            collection.Attach(new DependencyObject());
            collection.Detach();

            Assert.False(collection.IsAttached);
            foreach (var behavior in collection)
                Assert.False(behavior.IsAttached);
        }

        [Fact]
        public void RetroactivelyAttachesNewElements()
        {
            var collection = new BehaviorCollection();
            collection.Attach(new DependencyObject());

            var newBehavior = new TestableBehavior();
            collection.Add(newBehavior);

            Assert.True(newBehavior.IsAttached);
        }

        #endregion

        #region Adding/Removing Items

        [Fact]
        public void ThrowsIfAddingNull()
        {
            var collection = new BehaviorCollection();
            Assert.ThrowsAny<Exception>(() => collection.Add(null));
        }

        [Fact]
        public void AddingBehaviorTwiceThrowsException()
        {
            var collection = new BehaviorCollection();
            var behavior = new TestableBehavior();

            collection.Add(behavior);
            Assert.Throws<InvalidOperationException>(() => collection.Add(behavior));
        }

        [Fact]
        public void DetachesBehaviorsWhichGetRemoved()
        {
            var collection = new BehaviorCollection();
            var behavior = new TestableBehavior();

            collection.Add(behavior);
            collection.Attach(new DependencyObject());
            collection.Remove(behavior);

            Assert.False(behavior.IsAttached);
        }

        [Fact]
        public void DetachesAllBehaviorsWhenCleared()
        {
            var behaviors = new List<Behavior>()
            {
                new TestableBehavior(),
                new TestableBehavior(),
                new TestableBehavior(),
                new TestableBehavior()
            };
            var collection = new BehaviorCollection();

            // Perform add and remove operations.
            foreach (var behavior in behaviors)
                collection.Add(behavior);
            collection.RemoveAt(0);

            collection.Attach(new DependencyObject());
            collection.Clear();

            foreach (var behavior in behaviors)
                Assert.False(behavior.IsAttached);
        }

        [Fact]
        public void AttachesAndDetachesOnReplace()
        {
            var collection = new BehaviorCollection();
            var behavior1 = new TestableBehavior();
            var behavior2 = new TestableBehavior();

            collection.Add(behavior1);
            collection.Attach(new DependencyObject());

            collection[0] = behavior2;

            Assert.False(behavior1.IsAttached);
            Assert.True(behavior2.IsAttached);
        }

        #endregion

        #region Behavior.OwningCollection

        [Fact]
        public void SetsOwningCollection()
        {
            var collection = new BehaviorCollection();
            var behavior = new TestableBehavior();

            collection.Add(behavior);
            Assert.NotNull(behavior.OwningCollection);
            Assert.Contains(behavior, behavior.OwningCollection);
        }

        [Fact]
        public void UnsetsOwningCollectionOnRemove()
        {
            var collection = new BehaviorCollection();
            var behavior = new TestableBehavior();

            collection.Add(behavior);
            collection.Remove(behavior);
            Assert.Null(behavior.OwningCollection);
        }

        [Fact]
        public void UnsetsOwningCollectionOnClear()
        {
            var collection = new BehaviorCollection();
            var behavior = new TestableBehavior();

            collection.Add(behavior);
            collection.Clear();
            Assert.Null(behavior.OwningCollection);
        }

        #endregion

    }

}
