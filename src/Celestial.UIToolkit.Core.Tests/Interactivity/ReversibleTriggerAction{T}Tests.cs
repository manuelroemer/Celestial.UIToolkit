using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celestial.UIToolkit.Core.Tests.Interactivity.Mocks;
using Celestial.UIToolkit.Interactivity;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{

    public class ReversibleTriggerActionOfTTests
    {

        [Fact]
        public void IgnoresCallsWithInvalidParameterTypes()
        {
            var action = new TestableReversibleTriggerAction<int>();
            action.Reverted += Action_Reverted;

            try
            {
                action.Revert("Invalid parameter");
            }
            finally
            {
                action.Reverted -= Action_Reverted;
            }

            void Action_Reverted(object sender, EventArgs<int> e)
            {
                throw new Exception("The action was reverted, even though it shouldn't have been.");
            }
        }

        [Fact]
        public void AllowsCallsWithValidParameterTypes()
        {
            var action = new TestableReversibleTriggerAction<int>();

            Assert.Raises<EventArgs<int>>(
                (handler) => action.Reverted += handler,
                (handler) => action.Reverted -= handler,
                () => action.Revert(123)
            );
        }

        [Fact]
        public void DisallowsNullParameterIfSet()
        {
            var action = new TestableReversibleTriggerAction<object>();
            action.AllowNullParam = false;
            action.Reverted += Action_Reverted;

            try
            {
                action.Revert(null);
            }
            finally
            {
                action.Reverted -= Action_Reverted;
            }

            void Action_Reverted(object sender, EventArgs<object> e)
            {
                throw new Exception("The action was reverted, even though it shouldn't have been.");
            }
        }

        [Fact]
        public void AllowsNullParameterIfSet()
        {
            var action = new TestableReversibleTriggerAction<object>();
            action.AllowNullParam = true;

            Assert.Raises<EventArgs<object>>(
                (handler) => action.Reverted += handler,
                (handler) => action.Reverted -= handler,
                () => action.Revert(null)
            );
        }

    }

}
