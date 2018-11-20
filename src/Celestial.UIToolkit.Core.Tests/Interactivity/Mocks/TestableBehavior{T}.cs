using System.Windows;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public sealed class TestableBehavior<T> : Behavior<T> where T : DependencyObject
    {
        // We don't need any methods for tests. The only thing that we do need is a class
        // that can be instanciated.
    }

}
