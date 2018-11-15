using System;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class TestableTriggerAction : ITriggerAction
    {

        public event EventHandler<EventArgs<object>> Executed;

        public void Execute(object parameter)
        {
            Executed?.Invoke(this, new EventArgs<object>(parameter));
        }

    }

}
