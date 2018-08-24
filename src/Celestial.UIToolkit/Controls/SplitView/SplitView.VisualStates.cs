using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{
    
    [TemplateVisualState(Name = ClosedVisualState,                  GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = ClosedCompactLeftVisualState,       GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = ClosedCompactRightVisualState,      GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenOverlayLeftVisualState,         GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenOverlayRightVisualState,        GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenInlineLeftVisualState,          GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenInlineRightVisualState,         GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenCompactOverlayLeftVisualState,  GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenCompactOverlayRightVisualState, GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenCompactInlineLeftVisualState,   GroupName = DisplayModeStatesVisualStateGroup)]
    [TemplateVisualState(Name = OpenCompactInlineRightVisualState,  GroupName = DisplayModeStatesVisualStateGroup)]
    public partial class SplitView
    {

        // These first few strings are here to simplify our lives later on.
        private const string Open = "Open";
        private const string Closed = "Closed";
        private const string Left = "Left";
        private const string Right = "Right";
        private const string Compact = "Compact";
        private const string Overlay = "Overlay";
        private const string Inline = "Inline";
        private const string CompactOverlay = Compact + Overlay;
        private const string CompactInline = Compact + Inline;
        
        internal const string DisplayModeStatesVisualStateGroup = "DisplayModeStates";

        internal const string ClosedVisualState = Closed;
        internal const string ClosedCompactLeftVisualState =  Closed + Compact + Left;
        internal const string ClosedCompactRightVisualState = Closed + Compact + Right;

        internal const string OpenOverlayLeftVisualState =  Open + Overlay + Left;
        internal const string OpenOverlayRightVisualState = Open + Overlay + Right;

        internal const string OpenInlineLeftVisualState =  Open + Inline + Left;
        internal const string OpenInlineRightVisualState = Open + Inline + Right;

        internal const string OpenCompactOverlayLeftVisualState =  Open + CompactOverlay + Left;
        internal const string OpenCompactOverlayRightVisualState = Open + CompactOverlay + Right;

        internal const string OpenCompactInlineLeftVisualState =  Open + CompactInline + Left;
        internal const string OpenCompactInlineRightVisualState = Open + CompactInline + Right;

        private void EnterCurrentDisplayModeVisualState()
        {
            EnterCurrentDisplayModeVisualState(true);
        }

        private void EnterCurrentDisplayModeVisualState(bool useTransitions)
        {
            if (IsPaneOpen)
                EnterOpenState();
            else
                 EnterClosedState();
            
            void EnterOpenState()
            {
                string stateName = Open + GetDisplayModeVisualStateString() + GetLeftRightSuffix();
                VisualStateManager.GoToState(this, stateName, useTransitions);
            }

            void EnterClosedState()
            {
                if (DisplayMode == SplitViewDisplayMode.CompactOverlay ||
                    DisplayMode == SplitViewDisplayMode.CompactInline)
                {
                    string stateName = Closed + Compact + GetLeftRightSuffix();
                    VisualStateManager.GoToState(this, stateName, useTransitions);
                }
                else
                {
                    VisualStateManager.GoToState(this, ClosedVisualState, useTransitions);
                }
            }
        }

        private string GetDisplayModeVisualStateString()
        {
            switch (DisplayMode)
            {
                case SplitViewDisplayMode.Overlay:
                    return Overlay;
                case SplitViewDisplayMode.Inline:
                    return Inline;
                case SplitViewDisplayMode.CompactOverlay:
                    return CompactOverlay;
                case SplitViewDisplayMode.CompactInline:
                    return CompactInline;
                default:
                    throw new NotImplementedException();
            }
        }

        private string GetLeftRightSuffix()
        {
            if (PanePlacement == SplitViewPanePlacement.Left)
                return Left;
            else if (PanePlacement == SplitViewPanePlacement.Right)
                return Right;
            else
                throw new NotImplementedException();
        }

    }

}
