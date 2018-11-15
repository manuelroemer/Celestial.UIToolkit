using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Core.Tests.Interactivity.Mocks
{

    public class TestableTrigger : Trigger
    {

        public void InvokeActions(object parameter = null)
        {
            OnTriggered(parameter);
        }

    }

}
