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
    [TemplateVisualState(Name = OverlayVisualState, GroupName = DisplayModesVisualStateGroup)]
    [TemplateVisualState(Name = InlineVisualState, GroupName = DisplayModesVisualStateGroup)]
    [TemplateVisualState(Name = CompactOverlayVisualState, GroupName = DisplayModesVisualStateGroup)]
    [TemplateVisualState(Name = CompactInlineVisualState, GroupName = DisplayModesVisualStateGroup)]
    public partial class SplitView
    {

        internal const string PaneToggleStatesVisualStateGroup = "PaneToggleStates";
        internal const string PaneOpenVisualState = "Open";
        internal const string PaneClosedVisualState = "Closed";

        internal const string DisplayModesVisualStateGroup = "DisplayModes";
        internal const string OverlayVisualState = "Overlay";
        internal const string InlineVisualState = "Inline";
        internal const string CompactOverlayVisualState = "CompactOverlay";
        internal const string CompactInlineVisualState = "CompactInline";

        private void EnterCurrentPaneToggleVisualState()
        {
            EnterCurrentPaneToggleVisualState(true);
        }

        private void EnterCurrentPaneToggleVisualState(bool useTransitions)
        {
            if (IsPaneOpen)
            {
                VisualStateManager.GoToState(this, PaneOpenVisualState, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, PaneClosedVisualState, useTransitions);
            }
        }

        private void EnterCurrentDisplayModeVisualState()
        {
            EnterCurrentDisplayModeVisualState(true);
        }

        private void EnterCurrentDisplayModeVisualState(bool useTransitions)
        {
            switch (DisplayMode)
            {
                case SplitViewDisplayMode.Overlay:
                    VisualStateManager.GoToState(this, OverlayVisualState, useTransitions);
                    break;
                case SplitViewDisplayMode.Inline:
                    VisualStateManager.GoToState(this, InlineVisualState, useTransitions);
                    break;
                case SplitViewDisplayMode.CompactOverlay:
                    VisualStateManager.GoToState(this, CompactOverlayVisualState, useTransitions);
                    break;
                case SplitViewDisplayMode.CompactInline:
                    VisualStateManager.GoToState(this, CompactInlineVisualState, useTransitions);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

    }

}
