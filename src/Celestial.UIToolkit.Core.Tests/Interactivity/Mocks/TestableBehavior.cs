using System;
using System.Collections.Generic;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    /// <summary>
    /// A simple behavior which only raises events whenever 
    /// <see cref="Behavior.OnAttached"/> and <see cref="Behavior.OnDetaching"/> are called.
    /// This allows testing if these methods get called correctly.
    /// </summary>
    public sealed class TestableBehavior : Behavior
    {

        /// <summary>
        /// Occurs when <see cref="OnAttached"/> is called.
        /// </summary>
        public event EventHandler<EventArgs> Attached;

        /// <summary>
        /// Occurs when <see cref="OnDetaching"/> is called.
        /// </summary>
        public event EventHandler<EventArgs> Detaching;

        public new IReadOnlyCollection<Behavior> OwningCollection => base.OwningCollection;

        protected override void OnAttached()
        {
            Attached?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnDetaching()
        {
            Detaching?.Invoke(this, EventArgs.Empty);
        }

    }

}
