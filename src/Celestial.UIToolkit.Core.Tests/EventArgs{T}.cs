using System;

namespace Celestial.UIToolkit.Core.Tests
{

    /// <summary>
    /// Generic event args for Xunit's Assert.Raises method, which requires event args.
    /// </summary>
    /// <typeparam name="T">
    /// The type of event data.
    /// </typeparam>
    public class EventArgs<T> : EventArgs
    {

        public T Data { get; }

        public EventArgs(T data = default)
        {
            Data = data;
        }

    }

}
