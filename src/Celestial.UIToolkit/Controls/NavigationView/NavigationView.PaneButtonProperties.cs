using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Controls
{

    public partial class NavigationView
    {

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
        
    }

}
