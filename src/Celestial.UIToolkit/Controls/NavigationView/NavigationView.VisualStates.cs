using System;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    [TemplateVisualState(Name = MinimalVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = CompactVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = ExpandedVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    public partial class NavigationView
    {

        /// <summary>
        /// Defines the name of the "DisplayModeStates" Visual State Group.
        /// </summary>
        public const string DisplayModeStatesVisualStateGroup = "DisplayModeStates";

        /// <summary>
        /// Defines the name of the "Minimal" Visual State.
        /// </summary>
        public const string MinimalVisualState = "Minimal";

        /// <summary>
        /// Defines the name of the "Compact" Visual State.
        /// </summary>
        public const string CompactVisualState = "Compact";

        /// <summary>
        /// Defines the name of the "Expanded" Visual State.
        /// </summary>
        public const string ExpandedVisualState = "Expanded";

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
