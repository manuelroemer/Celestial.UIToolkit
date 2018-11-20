using System;
using System.Windows;
using System.Windows.Controls;
using Celestial.UIToolkit.Core.Tests.Interactivity.Mocks;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{
    public class TriggerBehaviorOfTTests
    {

        [WpfFact]
        public void ThrowsForInvalidAssociatedObjectType()
        {
            // Use Control, but any type that derives from DependencyObject is fine.
            var trigger = new TestableTrigger<Control>();
            var disallowedObj = new DependencyObject();
            Assert.Throws<InvalidOperationException>(() => trigger.Attach(disallowedObj));
        }

        [WpfFact]
        public void AllowsAttachingToTypeT()
        {
            var trigger = new TestableTrigger<Control>();
            var associatedObj = new Control();
            trigger.Attach(associatedObj);
            Assert.Same(associatedObj, trigger.AssociatedObject);
        }

        [WpfFact]
        public void AllowsAttachingToTypeDerivedT()
        {
            var trigger = new TestableTrigger<Control>();
            var associatedObj = new Button();
            trigger.Attach(associatedObj);
            Assert.Same(associatedObj, trigger.AssociatedObject);
        }

    }
}
