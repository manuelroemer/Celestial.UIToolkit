using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Celestial.UIToolkit.Interactivity;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{

    public class InteractionTests
    {

        [Fact]
        public void BehaviorsReturnsNewCollectionByDefault()
        {
            var obj = new DependencyObject();
            var collection = Interaction.GetBehaviors(obj);

            Assert.NotNull(collection);
            Assert.Empty(collection);
        }

        [Fact]
        public void BehaviorsAlwaysReturnsSameCollection()
        {
            var obj = new DependencyObject();
            var collection1 = Interaction.GetBehaviors(obj);
            var collection2 = Interaction.GetBehaviors(obj);

            Assert.Same(collection1, collection2);
        }

        [Fact]
        public void BehaviorsCollectionIsAttachedToElement()
        {
            var obj = new DependencyObject();
            var collection = Interaction.GetBehaviors(obj);

            Assert.Same(obj, collection.AssociatedObject);
        }

    }

}
