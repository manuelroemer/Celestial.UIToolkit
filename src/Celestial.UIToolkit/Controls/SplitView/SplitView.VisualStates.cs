using System;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    [TemplateVisualState(Name = DefaultVisualState,                 GroupName = DisplayModeStatesVisualStateGroup)]
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

        // This here looks rough, but by defining the individual visual state parts,
        // we can later dynamically build the current state via methods,
        // instead of checking every single possible combination.
        private const string Open = "Open";
        private const string Closed = "Closed";
        private const string Left = "Left";
        private const string Right = "Right";
        private const string Compact = "Compact";
        private const string Overlay = "Overlay";
        private const string Inline = "Inline";
        private const string CompactOverlay = Compact + Overlay;
        private const string CompactInline = Compact + Inline;

        /// <summary>
        /// Defines the name of the "DisplayModeStates" Visual State Group.
        /// </summary>
        public const string DisplayModeStatesVisualStateGroup = "DisplayModeStates";

        /// <summary>
        /// Defines the name of the "Default" Visual State.
        /// </summary>
        public const string DefaultVisualState = "Default";

        /// <summary>
        /// Defines the name of the "Closed" Visual State.
        /// </summary>
        public const string ClosedVisualState = Closed;

        /// <summary>
        /// Defines the name of the "ClosedCompactLeft" Visual State.
        /// </summary>
        public const string ClosedCompactLeftVisualState =  Closed + Compact + Left;

        /// <summary>
        /// Defines the name of the "ClosedCompactRight" Visual State.
        /// </summary>
        public const string ClosedCompactRightVisualState = Closed + Compact + Right;


        /// <summary>
        /// Defines the name of the "OpenOverlayLeft" Visual State.
        /// </summary>
        public const string OpenOverlayLeftVisualState =  Open + Overlay + Left;

        /// <summary>
        /// Defines the name of the "OpenOverlayRight" Visual State.
        /// </summary>
        public const string OpenOverlayRightVisualState = Open + Overlay + Right;


        /// <summary>
        /// Defines the name of the "OpenInlineLeft" Visual State.
        /// </summary>
        public const string OpenInlineLeftVisualState =  Open + Inline + Left;

        /// <summary>
        /// Defines the name of the "OpenInlineRight" Visual State.
        /// </summary>
        public const string OpenInlineRightVisualState = Open + Inline + Right;


        /// <summary>
        /// Defines the name of the "OpenCompactOverlayLeft" Visual State.
        /// </summary>
        public const string OpenCompactOverlayLeftVisualState =  Open + CompactOverlay + Left;

        /// <summary>
        /// Defines the name of the "OpenCompactOverlayRight" Visual State.
        /// </summary>
        public const string OpenCompactOverlayRightVisualState = Open + CompactOverlay + Right;


        /// <summary>
        /// Defines the name of the "OpenCompactInlineLeft" Visual State.
        /// </summary>
        public const string OpenCompactInlineLeftVisualState =  Open + CompactInline + Left;

        /// <summary>
        /// Defines the name of the "OpenCompactInlineRight" Visual State.
        /// </summary>
        public const string OpenCompactInlineRightVisualState = Open + CompactInline + Right;

        private void EnterDefaultDisplayModeVisualState()
        {
            VisualStateManager.GoToState(this, DefaultVisualState, false);
        }

        private void EnterCurrentDisplayModeVisualState()
        {
            EnterCurrentDisplayModeVisualState(true);
        }

        private void EnterCurrentDisplayModeVisualState(bool useTransitions)
        {
            string stateName = GetCurrentDisplayModeVisualStateName();
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        private string GetCurrentDisplayModeVisualStateName()
        {
            if (IsPaneOpen)
                return GetCurrentOpenDisplayModeVisualStateName();
            else
                return GetCurrentClosedDisplayModeVisualStateName();
        }

        private string GetCurrentOpenDisplayModeVisualStateName()
        {
            return Open + GetCurrentDisplayModeMiddleString() + GetLeftRightSuffix();
        }

        private string GetCurrentClosedDisplayModeVisualStateName()
        {
            if (DisplayMode == SplitViewDisplayMode.CompactOverlay ||
                DisplayMode == SplitViewDisplayMode.CompactInline)
            {
                return Closed + Compact + GetLeftRightSuffix();
            }
            else
            {
                return ClosedVisualState;
            }
        }

        private string GetCurrentDisplayModeMiddleString()
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
