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

        /// <summary>
        /// Defines the name of the "PlaceholderStates" Visual State Group.
        /// </summary>
        public const string PlaceholderStatesVisualStateGroup = "PlaceholderStates";

        /// <summary>
        /// Defines the name of the "FloatingVisible" Visual State.
        /// </summary>
        public const string FloatingVisibleState = "FloatingVisible";

        /// <summary>
        /// Defines the name of the "FloatingNotVisible" Visual State.
        /// </summary>
        public const string FloatingNotVisibleState = "FloatingNotVisible";

        /// <summary>
        /// Defines the name of the "DisappearingVisible" Visual State.
        /// </summary>
        public const string DisappearingVisibleState = "DisappearingVisible";

        /// <summary>
        /// Defines the name of the "DisappearingNotVisible" Visual State.
        /// </summary>
        public const string DisappearingNotVisibleState = "DisappearingNotVisible";

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
            if (PlaceholderDisplayType == PlaceholderDisplayType.Floating)
            {
                stateName = IsPlaceholderVisible ?
                            FloatingVisibleState :
                            FloatingNotVisibleState;
            }
            else if (PlaceholderDisplayType == PlaceholderDisplayType.Disappearing)
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
                   $"{nameof(PlaceholderDisplayType)}: {PlaceholderDisplayType}";
        }

    }
    
}
