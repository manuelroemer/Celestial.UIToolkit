using System;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class BehaviorOfTTests
    {

        [WpfFact]
        public void ThrowsForInvalidAssociatedObjectType()
        {
            // Use Control, but any type that derives from DependencyObject is fine.
            var behavior = new TestableBehavior<Control>();
            var disallowedObj = new DependencyObject();
            Assert.Throws<InvalidOperationException>(() => behavior.Attach(disallowedObj));
        }

        [WpfFact]
        public void AllowsAttachingToTypeT()
        {
            var behavior = new TestableBehavior<Control>();
            var associatedObj = new Control();
            behavior.Attach(associatedObj);
            Assert.Same(associatedObj, behavior.AssociatedObject);
        }

        [WpfFact]
        public void AllowsAttachingToTypeDerivedT()
        {
            var behavior = new TestableBehavior<Control>();
            var associatedObj = new Button();
            behavior.Attach(associatedObj);
            Assert.Same(associatedObj, behavior.AssociatedObject);
        }

    }

}
