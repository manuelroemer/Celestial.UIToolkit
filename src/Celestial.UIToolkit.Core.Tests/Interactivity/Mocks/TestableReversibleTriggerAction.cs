using System;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class TestableReversibleTriggerAction : TestableTriggerAction, IReversibleTriggerAction
    {

        public event EventHandler<EventArgs<object>> Reverted;

        public void Revert(object parameter)
        {
            Reverted?.Invoke(this, new EventArgs<object>(parameter));
        }

    }

}
