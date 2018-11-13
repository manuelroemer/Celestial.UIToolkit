using System;
using System.Windows;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

        /// <summary>
        /// Occurs when the back button of the <see cref="NavigationView"/> is pressed.
        /// </summary>
        public event EventHandler BackRequested;

        /// <summary>
        /// Identifies the <see cref="IsToggleButtonEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsToggleButtonEnabledProperty =
            DependencyProperty.Register(
                nameof(IsToggleButtonEnabled),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsToggleButtonVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsToggleButtonVisibleProperty =
            DependencyProperty.Register(
                nameof(IsToggleButtonVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="PaneToggleButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneToggleButtonStyleProperty =
            DependencyProperty.Register(
                nameof(PaneToggleButtonStyle),
                typeof(Style),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="IsBackButtonEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBackButtonEnabledProperty =
            DependencyProperty.Register(
                nameof(IsBackButtonEnabled),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsBackButtonVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsBackButtonVisibleProperty =
            DependencyProperty.Register(
                nameof(IsBackButtonVisible),
                typeof(bool),
                typeof(NavigationView),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="PaneBackButtonStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PaneBackButtonStyleProperty =
            DependencyProperty.Register(
                nameof(PaneBackButtonStyle),
                typeof(Style),
                typeof(NavigationView),
                new PropertyMetadata(null));
        
        /// <summary>
        /// Identifies the <see cref="BackButtonCommand"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BackButtonCommandProperty =
            DependencyProperty.Register(
                nameof(BackButtonCommand),
                typeof(ICommand),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="BackButtonCommandParameter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BackButtonCommandParameterProperty =
            DependencyProperty.Register(
                nameof(BackButtonCommandParameter),
                typeof(object),
                typeof(NavigationView),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether the pane's toggle button is enabled or
        /// disabled.
        /// </summary>
        public bool IsToggleButtonEnabled
        {
            get { return (bool)GetValue(IsToggleButtonEnabledProperty); }
            set { SetValue(IsToggleButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pane's toggle button is currently
        /// visible.
        /// </summary>
        public bool IsToggleButtonVisible
        {
            get { return (bool)GetValue(IsToggleButtonVisibleProperty); }
            set { SetValue(IsToggleButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a style which defines the look of the pane's toggle button.
        /// </summary>
        public Style PaneToggleButtonStyle
        {
            get { return (Style)GetValue(PaneToggleButtonStyleProperty); }
            set { SetValue(PaneToggleButtonStyleProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the pane's back button is enabled or 
        /// disabled.
        /// </summary>
        public bool IsBackButtonEnabled
        {
            get { return (bool)GetValue(IsBackButtonEnabledProperty); }
            set { SetValue(IsBackButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pane's back button is visible.
        /// </summary>
        public bool IsBackButtonVisible
        {
            get { return (bool)GetValue(IsBackButtonVisibleProperty); }
            set { SetValue(IsBackButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a style which defines the look of the pane's back button.
        /// </summary>
        public Style PaneBackButtonStyle
        {
            get { return (Style)GetValue(PaneBackButtonStyleProperty); }
            set { SetValue(PaneBackButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a command which is bound to the back button of the
        /// <see cref="NavigationView"/> and thus gets executed, whenever the back button is
        /// pressed.
        /// </summary>
        public ICommand BackButtonCommand
        {
            get { return (ICommand)GetValue(BackButtonCommandProperty); }
            set { SetValue(BackButtonCommandProperty, value); }
        }

        /// <summary>
        /// Gets a command parameter accompanying the <see cref="BackButtonCommand"/>.
        /// </summary>
        public object BackButtonCommandParameter
        {
            get { return (object)GetValue(BackButtonCommandParameterProperty); }
            set { SetValue(BackButtonCommandParameterProperty, value); }
        }
        
        /// <summary>
        /// Raises the <see cref="BackRequested"/> event.
        /// </summary>
        protected virtual void OnBackRequested()
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

    }

}
