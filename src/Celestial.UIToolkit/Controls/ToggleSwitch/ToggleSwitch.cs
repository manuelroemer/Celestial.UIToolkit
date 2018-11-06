using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Represents a physical switch which allows users to turn something on or off.
    /// </summary>
    [ContentProperty(nameof(Header))]
    [TemplateVisualState(Name = OnVisualState, GroupName = ToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = OffVisualState, GroupName = ToggleStatesVisualStateGroup)]
    [TemplateVisualState(Name = DraggingVisualState, GroupName = ToggleStatesVisualStateGroup)]
    public partial class ToggleSwitch : Control
    {

        internal const string ToggleStatesVisualStateGroup = "ToggleStates";
        internal const string OnVisualState = "On";
        internal const string OffVisualState = "Off";
        internal const string DraggingVisualState = "Dragging";

        private bool _isDraggingViaKey;
        private bool _isDraggingViaMouse;

        /// <summary>
        /// Gets a value indicating whether the user is currently dragging the
        /// <see cref="ToggleSwitch"/>.
        /// </summary>
        protected bool IsDragging => !_isDraggingViaKey && !_isDraggingViaMouse;

        /// <summary>
        /// Identifies the <see cref="Toggled"/> routed event.
        /// </summary>
        public static readonly RoutedEvent ToggledEvent =
            EventManager.RegisterRoutedEvent(
                nameof(Toggled),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ToggleSwitch));

        /// <summary>
        /// Occurs when the <see cref="ToggleSwitch"/> is toggled on or off.
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Toggled
        {
            add { AddHandler(ToggledEvent, value); }
            remove { RemoveHandler(ToggledEvent, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleSwitch"/> class.
        /// </summary>
        public ToggleSwitch()
            : this(false) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleSwitch"/> class.
        /// </summary>
        /// <param name="isOn">
        /// A value indicating whether the switch is currently "On".
        /// </param>
        public ToggleSwitch(bool isOn)
        {
            IsOn = isOn;

            // These event handlers are required for dragging/toggling the switch.
            PreviewKeyDown += ToggleSwitch_PreviewKeyDown;
            PreviewKeyUp += ToggleSwitch_PreviewKeyUp;
            PreviewMouseLeftButtonDown += ToggleSwitch_PreviewMouseLeftButtonDown;
            PreviewMouseLeftButtonUp += ToggleSwitch_PreviewMouseLeftButtonUp;
        }

        /// <summary>
        /// Invoked when the <see cref="ToggleSwitch"/>'s template is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EnterCurrentToggledVisualState(false);
        }

        private void ToggleSwitch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsDragging && e.Key == Key.Space)
            {
                this.TraceVerbose("Dragging invoked via key.");
                _isDraggingViaKey = true;
                EnterCurrentToggledVisualState();
            }
        }

        private void ToggleSwitch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                // The user released the previously pressed Toggle-Key.
                this.TraceVerbose("Dragging via key stopped.");
                _isDraggingViaKey = false;
                IsOn = !IsOn;
            }
        }

        private void ToggleSwitch_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToggleSwitch_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void IsOn_Changed(DependencyPropertyChangedEventArgs e)
        {
            this.TraceInfo($"{nameof(IsOn)} changed to {0}.", IsOn);
            OnToggled();
            EnterCurrentToggledVisualState();
            ExecuteOnOffCommands();
        }

        /// <summary>
        /// Called whenever the <see cref="IsOnProperty"/> dependency property changes.
        /// If not overridden, raises the <see cref="Toggled"/> event.
        /// </summary>
        protected virtual void OnToggled()
        {
            var args = new RoutedEventArgs(ToggledEvent, this);
            RaiseEvent(args);
        }

        private void ExecuteOnOffCommands()
        {
            // Make sure to raise the right command, depending on the IsOn state.
            // Also ensure that each command is executable.
            if (IsOn && OnCommand != null && OnCommand.CanExecute(OnCommandParameter))
            {
                this.TraceVerbose("Executing OnCommand.");
                OnCommand.Execute(OnCommandParameter);
            }
            if (!IsOn && OffCommand != null && OffCommand.CanExecute(OffCommandParameter))
            {
                this.TraceVerbose("Executing OffCommand.");
                OffCommand.Execute(OffCommandParameter);
            }
        }
        
        private void EnterCurrentToggledVisualState(bool useTransitions = true)
        {
            // Dragging takes priority over On/Off.
            if (IsDragging)
            {
                VisualStateManager.GoToState(this, DraggingVisualState, useTransitions);
            }
            else
            {
                if (IsOn)
                {
                    VisualStateManager.GoToState(this, OnVisualState, useTransitions);
                }
                else
                {
                    VisualStateManager.GoToState(this, OffVisualState, useTransitions);
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the <see cref="ToggleSwitch"/>.
        /// </summary>
        /// <returns>A string representing the <see cref="ToggleSwitch"/>.</returns>
        public override string ToString()
        {
            return $"{nameof(ToggleSwitch)}: " +
                   $"{nameof(IsOn)}: {IsOn}";
        }

    }

}
