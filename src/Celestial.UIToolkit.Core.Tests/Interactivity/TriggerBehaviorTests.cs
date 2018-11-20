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

    public class TriggerBehaviorTests
    {

        [Fact]
        public void ActionsReturnsNewCollectionByDefault()
        {
            var trigger = new TestableTrigger();
            Assert.NotNull(trigger.Actions);
            Assert.Empty(trigger.Actions);
        }
        
        [Fact]
        public void InvokesActions()
        {
            var trigger = new TestableTrigger();
            trigger.Actions.Add(new TestableTriggerAction());
            trigger.Actions.Add(new TestableTriggerAction());
            trigger.Actions.Add(new TestableTriggerAction());

            foreach (TestableTriggerAction action in trigger.Actions)
            {
                Assert.Raises<EventArgs<object>>(
                    (handler) => action.Executed += handler,
                    (handler) => action.Executed -= handler,
                    () => trigger.InvokeActions()
                );
            }
        }

        [Fact]
        public void PassesParameter()
        {
            var parameter = new object();
            var trigger = new TestableTrigger();
            var action = new TestableTriggerAction();

            try
            {
                action.Executed += Action_Executed;
                trigger.Actions.Add(action);
                trigger.InvokeActions(parameter);
            }
            finally
            {
                action.Executed -= Action_Executed;
            }

            void Action_Executed(object sender, EventArgs<object> e)
            {
                Assert.Same(parameter, e.Data);
            }
        }

    }

}
