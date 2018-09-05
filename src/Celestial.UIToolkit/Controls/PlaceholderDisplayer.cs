using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Defines the different display types of a placeholder.
    /// </summary>
    public enum PlaceholderDisplayKind
    {

        /// <summary>
        /// The placeholder is always visible.
        /// If space is required, the placeholder moves to a place where it doesn't use up
        /// the content's space.
        /// </summary>
        Floating,

        /// <summary>
        /// When the content requires space, the placeholder disappears.
        /// </summary>
        Disappearing
        
    }

    /// <summary>
    /// A control which displays placeholder content over the actual content.
    /// </summary>
    [TemplateVisualState(Name = FloatingVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    [TemplateVisualState(Name = FloatingNotVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    [TemplateVisualState(Name = DisappearingVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    [TemplateVisualState(Name = DisappearingNotVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    public class PlaceholderDisplayer : ContentControl
    {

        internal const string PlaceholderStatesVisualStateGroup = "PlaceholderStates";
        internal const string FloatingVisibleState = "FloatingVisible";
        internal const string FloatingNotVisibleState = "FloatingNotVisible";
        internal const string DisappearingVisibleState = "DisappearingVisible";
        internal const string DisappearingNotVisibleState = "DisappearingNotVisible";

        /// <summary>
        /// Identifies the <see cref="Placeholder"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                nameof(Placeholder),
                typeof(object),
                typeof(PlaceholderDisplayer),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="IsPlaceholderVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsPlaceholderVisibleProperty =
            DependencyProperty.Register(
                nameof(IsPlaceholderVisible),
                typeof(bool),
                typeof(PlaceholderDisplayer),
                new PropertyMetadata(
                    true,
                    PlaceholderDisplayProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="PlaceholderDisplayKind"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderDisplayKindProperty =
            DependencyProperty.Register(
                nameof(PlaceholderDisplayKind),
                typeof(PlaceholderDisplayKind),
                typeof(PlaceholderDisplayer),
                new PropertyMetadata(
                    PlaceholderDisplayKind.Floating,
                    PlaceholderDisplayProperty_Changed));

        /// <summary>
        /// Gets or sets the placeholder content which is rendered by the control.
        /// </summary>
        [Bindable(true), Category("Content")]
        public object Placeholder
        {
            get { return GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the placeholder is currently displayed.
        /// </summary>
        [Bindable(true), Category("Content")]
        public bool IsPlaceholderVisible
        {
            get { return (bool)GetValue(IsPlaceholderVisibleProperty); }
            set { SetValue(IsPlaceholderVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Controls.PlaceholderDisplayKind"/> which is used
        /// for displaying the placeholder.
        /// </summary>
        [Bindable(true), Category("Content")]
        public PlaceholderDisplayKind PlaceholderDisplayKind
        {
            get { return (PlaceholderDisplayKind)GetValue(PlaceholderDisplayKindProperty); }
            set { SetValue(PlaceholderDisplayKindProperty, value); }
        }

        static PlaceholderDisplayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(PlaceholderDisplayer), 
                new FrameworkPropertyMetadata(typeof(PlaceholderDisplayer)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceholderDisplayer"/> class.
        /// </summary>
        public PlaceholderDisplayer()
        {
            Loaded += (sender, e) => EnterCurrentPlaceholderVisualState(false);
        }

        private static void PlaceholderDisplayProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (PlaceholderDisplayer)d;
            self.EnterCurrentPlaceholderVisualState();
        }

        private void EnterCurrentPlaceholderVisualState(bool useTransitions = true)
        {
            string stateName = null;
            if (PlaceholderDisplayKind == PlaceholderDisplayKind.Floating)
            {
                stateName = IsPlaceholderVisible ?
                            FloatingVisibleState :
                            FloatingNotVisibleState;
            }
            else if (PlaceholderDisplayKind == PlaceholderDisplayKind.Disappearing)
            {
                stateName = IsPlaceholderVisible ?
                            DisappearingVisibleState :
                            DisappearingNotVisibleState;
            }

            if (stateName == null)
                throw new NotImplementedException("Unknown visual state.");
            Debug.WriteLine(stateName, nameof(PlaceholderDisplayer));
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        /// <summary>
        /// Returns a string representation of the <see cref="PlaceholderDisplayer"/>.
        /// </summary>
        /// <returns>
        /// A string representing the current state of the <see cref="PlaceholderDisplayer"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(PlaceholderDisplayer)}: " +
                   $"{nameof(Placeholder)}: {Placeholder}, " +
                   $"{nameof(IsPlaceholderVisible)}: {IsPlaceholderVisible}, " +
                   $"{nameof(PlaceholderDisplayKind)}: {PlaceholderDisplayKind}";
        }

    }
    
}
