using System;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class TestableTriggerAction<T> : TriggerAction<T>
    {

        public event EventHandler<EventArgs<T>> Executed;

        public bool AllowNullParam { get; set; }

        protected override bool AllowNullParameter => AllowNullParam;

        protected override void Execute(T parameter)
        {
            Executed?.Invoke(this, new EventArgs<T>(parameter));
        }

    }

}
