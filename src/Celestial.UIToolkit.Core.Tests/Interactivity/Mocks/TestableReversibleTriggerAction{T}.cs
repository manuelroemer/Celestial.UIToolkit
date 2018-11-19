using System;
using System.Windows;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public sealed class TestableReversibleTriggerAction<T> : ReversibleTriggerAction<T>
    {

        public event EventHandler<EventArgs<T>> Executed;

        public event EventHandler<EventArgs<T>> Reverted;

        public bool AllowNullParam { get; set; }

        protected override bool AllowNullParameter => AllowNullParam;

        protected override void Execute(T parameter)
        {
            Executed?.Invoke(this, new EventArgs<T>(parameter));
        }

        protected override void Revert(T parameter)
        {
            Reverted?.Invoke(this, new EventArgs<T>(parameter));
        }

    }

}
