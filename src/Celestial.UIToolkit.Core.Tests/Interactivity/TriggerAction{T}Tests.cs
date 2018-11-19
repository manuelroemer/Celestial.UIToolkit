using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celestial.UIToolkit.Core.Tests.Interactivity.Mocks;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{

    public class TriggerActionOfTTests
    {

        [Fact]
        public void IgnoresCallsWithInvalidParameterTypes()
        {
            var action = new TestableTriggerAction<int>();
            action.Executed += Action_Executed;

            try
            {
                action.Execute("Invalid parameter");
            }
            finally
            {
                action.Executed -= Action_Executed;
            }

            void Action_Executed(object sender, EventArgs<int> e)
            {
                throw new Exception("The action was executed, even though it shouldn't have been.");
            }
        }

        [Fact]
        public void AllowsCallsWithValidParameterTypes()
        {
            var action = new TestableTriggerAction<int>();

            Assert.Raises<EventArgs<int>>(
                (handler) => action.Executed += handler,
                (handler) => action.Executed -= handler,
                () => action.Execute(123)
            );
        }

        [Fact]
        public void DisallowsNullParameterIfSet()
        {
            var action = new TestableTriggerAction<object>();
            action.AllowNullParam = false;
            action.Executed += Action_Executed;

            try
            {
                action.Execute(null);
            }
            finally
            {
                action.Executed -= Action_Executed;
            }

            void Action_Executed(object sender, EventArgs<object> e)
            {
                throw new Exception("The action was executed, even though it shouldn't have been.");
            }
        }

        [Fact]
        public void AllowsNullParameterIfSet()
        {
            var action = new TestableTriggerAction<object>();
            action.AllowNullParam = true;

            Assert.Raises<EventArgs<object>>(
                (handler) => action.Executed += handler,
                (handler) => action.Executed -= handler,
                () => action.Execute(null)
            );
        }

    }

}
