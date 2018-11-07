using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// An extension of the <see cref="Control"/> class, which implements common
    /// visual state changes.
    /// </summary>
    [TemplateVisualState(Name = NormalVisualState, GroupName = CommonStatesVisualStateGroup)]
    [TemplateVisualState(Name = MouseOverVisualState, GroupName = CommonStatesVisualStateGroup)]
    [TemplateVisualState(Name = DisabledVisualState, GroupName = CommonStatesVisualStateGroup)]
    public class CommonVisualStatesControl : Control
    {

        /// <summary>
        /// Defines the name of the "CommonStates" Visual State Group.
        /// </summary>
        public const string CommonStatesVisualStateGroup = "CommonStates";

        /// <summary>
        /// Defines the name of the "Normal" Visual State.
        /// </summary>
        public const string NormalVisualState = "Normal";

        /// <summary>
        /// Defines the name of the "MouseOver" Visual State.
        /// </summary>
        public const string MouseOverVisualState = "MouseOver";

        /// <summary>
        /// Defines the name of the "Disabled" Visual State.
        /// </summary>
        public const string DisabledVisualState = "Disabled";
        
        /// <summary>
        /// Initializes a new instance of the control.
        /// </summary>
        public CommonVisualStatesControl()
        {
            IsEnabledChanged += IsEnabled_Changed;
            MouseEnter += Mouse_Enter;
            MouseLeave += Mouse_Leave;
        }

        /// <summary>
        /// Enters the current common visual state, when called.
        /// If overridden, can be used to perform operations on the control's template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EnterCurrentCommonStatesVisualState(false);
        }

        private void IsEnabled_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.TraceVerbose("IsEnabled changed.");
            EnterCurrentCommonStatesVisualState();
        }

        private void Mouse_Enter(object sender, MouseEventArgs e)
        {
            this.TraceVerbose("Mouse entered.");
            EnterCurrentCommonStatesVisualState();
        }

        private void Mouse_Leave(object sender, MouseEventArgs e)
        {
            this.TraceVerbose("Mouse left.");
            EnterCurrentCommonStatesVisualState();
        }

        /// <summary>
        /// Changes the control's visual state in the <see cref="CommonStatesVisualStateGroup"/>
        /// to the appropriate state.
        /// This method uses visual transitions.
        /// </summary>
        protected void EnterCurrentCommonStatesVisualState()
        {
            EnterCurrentCommonStatesVisualState(true);
        }

        /// <summary>
        /// Changes the control's visual state in the <see cref="CommonStatesVisualStateGroup"/>
        /// to the appropriate state.
        /// </summary>
        /// <param name="useTransitions">
        /// A value indicating whether visual transitions should be used.
        /// </param>
        protected virtual void EnterCurrentCommonStatesVisualState(bool useTransitions)
        {
            string visualStateName = GetCurrentCommonVisualStateName();
            VisualStateManager.GoToState(this, visualStateName, useTransitions);
        }
        
        private string GetCurrentCommonVisualStateName()
        {
            if (!IsEnabled)
            {
                // Disabled takes precedence over everything else.
                return DisabledVisualState;
            }
            else if (IsMouseOver)
            {
                return MouseOverVisualState;
            }
            else
            {
                return NormalVisualState;
            }
        }

    }

}
