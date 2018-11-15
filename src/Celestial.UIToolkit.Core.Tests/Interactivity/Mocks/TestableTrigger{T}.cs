using System.Windows;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class TestableTrigger<T> : Trigger<T> where T : DependencyObject
    {

        public void InvokeActions(object parameter = null)
        {
            OnTriggered(parameter);
        }

    }

}
