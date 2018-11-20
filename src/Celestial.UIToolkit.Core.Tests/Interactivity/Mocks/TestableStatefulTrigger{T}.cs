using System.Windows;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public sealed class TestableStatefulTrigger<T> : StatefulTriggerBehavior<T> where T : DependencyObject
    {

        public void InvokeActions(object parameter = null)
        {
            OnTriggered(parameter);
        }

        public void InvokeActions(bool isActive, object parameter = null)
        {
            OnTriggered(isActive, parameter);
        }

    }

}
