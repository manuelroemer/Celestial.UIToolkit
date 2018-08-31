using System;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    [TemplateVisualState(Name = MinimalVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = CompactVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = ExpandedVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    public partial class NavigationView
    {
        
        internal const string DisplayModeStatesVisualStateGroup = "DisplayModeStates";
        internal const string MinimalVisualState = "Minimal";
        internal const string CompactVisualState = "Compact";
        internal const string ExpandedVisualState = "Expanded";

        private void EnterCurrentDisplayModeVisualState()
        {
            EnterCurrentDisplayModeVisualState(true);
        }

        private void EnterCurrentDisplayModeVisualState(bool useTransitions)
        {
            switch (DisplayMode)
            {
                case NavigationViewDisplayMode.Minimal:
                    VisualStateManager.GoToState(this, MinimalVisualState, useTransitions);
                    break;
                case NavigationViewDisplayMode.Compact:
                    VisualStateManager.GoToState(this, CompactVisualState, useTransitions);
                    break;
                case NavigationViewDisplayMode.Expanded:
                    VisualStateManager.GoToState(this, ExpandedVisualState, useTransitions);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

    }

}
