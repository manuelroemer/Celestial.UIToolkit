using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class TestableStatefulTrigger : StatefulTriggerBehavior
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
