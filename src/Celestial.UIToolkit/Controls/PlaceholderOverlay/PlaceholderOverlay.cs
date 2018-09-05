using System;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A control which displays placeholder content over the actual content.
    /// </summary>
    [TemplateVisualState(Name = FloatingVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    [TemplateVisualState(Name = FloatingNotVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    [TemplateVisualState(Name = DisappearingVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    [TemplateVisualState(Name = DisappearingNotVisibleState, GroupName = PlaceholderStatesVisualStateGroup)]
    public partial class PlaceholderOverlay : ContentControl
    {

        internal const string PlaceholderStatesVisualStateGroup = "PlaceholderStates";
        internal const string FloatingVisibleState = "FloatingVisible";
        internal const string FloatingNotVisibleState = "FloatingNotVisible";
        internal const string DisappearingVisibleState = "DisappearingVisible";
        internal const string DisappearingNotVisibleState = "DisappearingNotVisible";

        static PlaceholderOverlay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(PlaceholderOverlay), 
                new FrameworkPropertyMetadata(typeof(PlaceholderOverlay)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceholderOverlay"/> class.
        /// </summary>
        public PlaceholderOverlay()
        {
            Loaded += (sender, e) => EnterCurrentPlaceholderVisualState(false);
        }

        private static void PlaceholderDisplayProperty_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (PlaceholderOverlay)d;
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
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        /// <summary>
        /// Returns a string representation of the <see cref="PlaceholderOverlay"/>.
        /// </summary>
        /// <returns>
        /// A string representing the current state of the <see cref="PlaceholderOverlay"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(PlaceholderOverlay)}: " +
                   $"{nameof(Placeholder)}: {Placeholder}, " +
                   $"{nameof(IsPlaceholderVisible)}: {IsPlaceholderVisible}, " +
                   $"{nameof(PlaceholderDisplayKind)}: {PlaceholderDisplayKind}";
        }

    }
    
}
