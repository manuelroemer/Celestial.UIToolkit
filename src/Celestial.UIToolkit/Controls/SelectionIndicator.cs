using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A basic control which is usually used within control templates.
    /// It indicates whether the hosting control is currently selected (or anything similar).
    /// </summary>
    [TemplateVisualState(Name = SelectedVisualState, GroupName = SelectionStatesVisualStateGroup)]
    [TemplateVisualState(Name = UnselectedVisualState, GroupName = SelectionStatesVisualStateGroup)]
    public class SelectionIndicator : Control
    {

        /// <summary>
        /// Defines the name of the "SelectionStates" Visual State Group.
        /// </summary>
        public const string SelectionStatesVisualStateGroup = "SelectionStates";

        /// <summary>
        /// Defines the name of the "Selected" Visual State.
        /// </summary>
        public const string SelectedVisualState = "Selected";

        /// <summary>
        /// Defines the name of the "Unselected" Visual State.
        /// </summary>
        public const string UnselectedVisualState = "Unselected";

        /// <summary>
        /// Identifies the <see cref="Orientation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                nameof(Orientation),
                typeof(Orientation),
                typeof(SelectionIndicator),
                new PropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// Identifies the <see cref="IsSelected"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(SelectionIndicator),
                new PropertyMetadata(
                    false,
                    IsSelected_Changed));

        /// <summary>
        /// Gets or sets the indicator's orientation.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the indicator is currently active,
        /// i.e. the hosting control is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        
        static SelectionIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SelectionIndicator),
                new FrameworkPropertyMetadata(typeof(SelectionIndicator)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionIndicator"/> class.
        /// </summary>
        public SelectionIndicator()
        {
        }

        /// <summary>
        /// Called when the template is applied.
        /// Transitions to the current visual state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EnterCurrentVisualState(false);
        }

        private static void IsSelected_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SelectionIndicator)d;
            self.EnterCurrentVisualState(true);
        }

        private void EnterCurrentVisualState(bool useTransitions)
        {
            string stateName = IsSelected ? SelectedVisualState : UnselectedVisualState;
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        /// <summary>
        /// Returns a string representation of this <see cref="SelectionIndicator"/>.
        /// </summary>
        /// <returns>
        /// A string representing this <see cref="SelectionIndicator"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(SelectionIndicator)}: " +
                   $"{nameof(IsSelected)}: {IsSelected}, " +
                   $"{nameof(Orientation)}: {Orientation}";
        }

    }

}
