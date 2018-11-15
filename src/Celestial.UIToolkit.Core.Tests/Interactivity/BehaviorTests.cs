using System;
using System.Windows;
using Celestial.UIToolkit.Interactivity;
using Celestial.UIToolkit.Core.Tests.Interactivity.Mocks;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{

    /// <summary>
    /// Contains test methods for the <see cref="Behavior"/> class, which is replaced
    /// with the <see cref="TestableBehavior"/>, since <see cref="Behavior"/> is abstract.
    /// </summary>
    public class BehaviorTests
    {
        
        [Fact]
        public void ThrowsIfAttachingToNull()
        {
            var behavior = new TestableBehavior();
            Assert.Throws<ArgumentNullException>(() => behavior.Attach(null));
        }

        [Fact]
        public void AttachesToObject()
        {
            var behavior = new TestableBehavior();
            var associatedObj = new DependencyObject();

            Assert.Raises<EventArgs>(
                (handler) => behavior.Attached += handler,
                (handler) => behavior.Attached -= handler,
                () => behavior.Attach(associatedObj)
            );
            Assert.Same(associatedObj, behavior.AssociatedObject);
            Assert.True(behavior.IsAttached);
        }

        [Fact]
        public void CannotAttachTwoObjects()
        {
            var behavior = new TestableBehavior();
            var associatedObj1 = new DependencyObject();
            var associatedObj2 = new DependencyObject();

            behavior.Attach(associatedObj1);
            Assert.Throws<InvalidOperationException>(() => behavior.Attach(associatedObj2));
        }

        [Fact]
        public void AttachingSameElementTwiceDoesntThrow()
        {
            var behavior = new TestableBehavior();
            var associatedObj = new DependencyObject();

            behavior.Attach(associatedObj);
            behavior.Attach(associatedObj); // If this throws, the test should fail.
        }

        [Fact]
        public void DetachesFromObject()
        {
            var behavior = new TestableBehavior();
            var associatedObj = new DependencyObject();
            behavior.Attach(associatedObj);

            Assert.Raises<EventArgs>(
                (handler) => behavior.Detaching += handler,
                (handler) => behavior.Detaching -= handler,
                () => behavior.Detach()
            );
            Assert.Null(behavior.AssociatedObject);
            Assert.False(behavior.IsAttached);
        }

    }

}
