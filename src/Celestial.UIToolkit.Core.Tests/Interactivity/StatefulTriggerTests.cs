using System;
using Celestial.UIToolkit.Core.Tests.Interactivity.Mocks;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Interactivity
{

    public class StatefulTriggerTests
    {

        [Fact]
        public void ChangesToActive()
        {
            var trigger = new TestableStatefulTrigger();
            trigger.InvokeActions(true);
            Assert.True(trigger.IsActive);
        }

        [Fact]
        public void ChangesToInactive()
        {
            var trigger = new TestableStatefulTrigger();
            trigger.InvokeActions(false);
            Assert.False(trigger.IsActive);
        }

        [Fact]
        public void OnInvokedWithoutParamsActivatesByDefault()
        {
            var trigger = new TestableStatefulTrigger();
            trigger.InvokeActions(); // This calls OnTriggered() without specifying isActive.
            Assert.True(trigger.IsActive);
        }

        [Fact]
        public void ExecutesEnterActionsWhenActive()
        {
            var trigger = new TestableStatefulTrigger();
            var action = new TestableTriggerAction();
            trigger.EnterActions.Add(action);

            Assert.Raises<EventArgs<object>>(
                (handler) => action.Executed += handler,
                (handler) => action.Executed -= handler,
                () => trigger.InvokeActions(true)
            );
        }
        
        [Fact]
        public void ExecutesExitActionsWhenInactive()
        {
            var trigger = new TestableStatefulTrigger();
            var action = new TestableTriggerAction();
            trigger.ExitActions.Add(action);

            trigger.InvokeActions(true);
            Assert.Raises<EventArgs<object>>(
                (handler) => action.Executed += handler,
                (handler) => action.Executed -= handler,
                () => trigger.InvokeActions(false)
            );
        }

        [Fact]
        public void ExecutesActionsWhenActive()
        {
            var trigger = new TestableStatefulTrigger();
            var action = new TestableTriggerAction();
            trigger.Actions.Add(action);

            Assert.Raises<EventArgs<object>>(
                (handler) => action.Executed += handler,
                (handler) => action.Executed -= handler,
                () => trigger.InvokeActions(true)
            );
        }

        [Fact]
        public void RevertsActionsWhenInactive()
        {
            var trigger = new TestableStatefulTrigger();
            var action = new TestableReversibleTriggerAction();
            trigger.Actions.Add(action);

            trigger.InvokeActions(true);
            Assert.Raises<EventArgs<object>>(
                (handler) => action.Reverted += handler,
                (handler) => action.Reverted -= handler,
                () => trigger.InvokeActions(false)
            );
        }

        [Fact]
        public void DoesntExecuteExitActionsWhenActive()
        {
            var trigger = new TestableStatefulTrigger();
            var action = new TestableTriggerAction();
            trigger.ExitActions.Add(action);
            action.Executed += Action_Executed;

            try
            {
                trigger.InvokeActions(true);
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
        public void DoesntExecuteEnterActionsWhenInactive()
        {
            var trigger = new TestableStatefulTrigger();
            var action = new TestableTriggerAction();

            trigger.InvokeActions(true);
            trigger.EnterActions.Add(action);
            action.Executed += Action_Executed;

            try
            {
                trigger.InvokeActions(false);
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

    }

}
