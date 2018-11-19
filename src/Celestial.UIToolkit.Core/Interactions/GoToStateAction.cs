using System.Windows;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Interactions
{

    /// <summary>
    ///     A trigger action which, once executed, changes the visual state of an element.
    ///     See Remarks for details on which element's state gets changed.
    /// </summary>
    /// <remarks>
    ///     This action uses the following logic for finding the element whose visual state gets
    ///     changed:
    ///     
    ///     * If the <see cref="TargetElement"/> property is set, it will always be used.
    /// </remarks>
    public sealed class GoToStateAction : TriggerAction<FrameworkElement>
    {

        /// <summary>
        /// Identifies the <see cref="UseTransitions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UseTransitionsProperty =
            DependencyProperty.Register(
                nameof(UseTransitions),
                typeof(bool),
                typeof(GoToStateAction),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="StateName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StateNameProperty =
            DependencyProperty.Register(
                nameof(StateName),
                typeof(string),
                typeof(GoToStateAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="TargetElement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register(
                nameof(TargetElement),
                typeof(FrameworkElement),
                typeof(GoToStateAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether visual transitions should be used during
        /// the state change.
        /// The default value is <c>true</c>.
        /// </summary>
        public bool UseTransitions
        {
            get { return (bool)GetValue(UseTransitionsProperty); }
            set { SetValue(UseTransitionsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the visual state to which the control should transition.
        /// </summary>
        public string StateName
        {
            get { return (string)GetValue(StateNameProperty); }
            set { SetValue(StateNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="FrameworkElement"/> whose visual state should be changed
        /// by this action.
        /// This can be null. In this case, the action automatically finds the most appropriate
        /// element whose state should be changed.
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the action can be executed with a null parameter.
        /// This is dependent on whether <see cref="TargetElement"/> is set or not.
        /// If <see cref="TargetElement"/> is null, the target of this action must come from the
        /// trigger.
        /// </summary>
        protected override bool ExecuteWithNullParameter => TargetElement != null;
        
        /// <summary>
        /// Transitions the attached <paramref name="element"/> to the visual state defined
        /// in <see cref="StateName"/>.
        /// </summary>
        /// <param name="element">
        /// The element whose visual state should be changed.
        /// </param>
        protected override void Execute(FrameworkElement element)
        {
            var actualTarget = TargetElement ?? element;

            VisualStateManager.GoToState(actualTarget, StateName, UseTransitions);
        }
        
    }

}
