using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    [TemplateVisualState(Name = PaneOpenVisualState, GroupName = PaneToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = PaneClosedVisualState, GroupName = PaneToggleStatesVisualStateGroup)]
    public partial class SplitView
    {

        internal const string PaneToggleStatesVisualStateGroup = "PaneToggleStates";
        internal const string PaneOpenVisualState = "Open";
        internal const string PaneClosedVisualState = "Closed";

        private void EnterCurrentPaneToggleVisualState()
        {
            EnterCurrentPaneToggleVisualState(true);
        }

        private void EnterCurrentPaneToggleVisualState(bool useTransitions)
        {
            if (IsPaneOpen)
            {
                VisualStateManager.GoToState(this, PaneOpenVisualState, true);
            }
            else
            {
                VisualStateManager.GoToState(this, PaneClosedVisualState, true);
            }
        }

    }

}
