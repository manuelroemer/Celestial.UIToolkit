using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Celestial.UIToolkit.Interactivity;
using static Celestial.UIToolkit.TraceSources;

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
    ///     * Otherwise, the trigger which executes this action must pass a
    ///       <see cref="FrameworkElement"/> as a parameter. This element is then used.
    ///     
    ///     Now that the action has an element, it starts searching for the nearest control whose
    ///     <see cref="VisualStateManager.VisualStateGroupsProperty"/> is set (i.e. the nearest
    ///     element which provides visual states). This can be the element itself.
    ///     This search stops at certain points. For instance, if this action is defined in a 
    ///     <see cref="ControlTemplate"/>, it stops after reaching the template's root.
    ///     
    ///     If an element was found, the action changes the visual state of the element.
    ///     If not, it throws an <see cref="InvalidOperationException"/>.
    /// </remarks>
    public sealed class GoToStateAction : TriggerAction<FrameworkElement>
    {

        /// <summary>
        ///     Identifies the <see cref="UseTransitions"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UseTransitionsProperty =
            DependencyProperty.Register(
                nameof(UseTransitions),
                typeof(bool),
                typeof(GoToStateAction),
                new PropertyMetadata(true));

        /// <summary>
        ///     Identifies the <see cref="StateName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StateNameProperty =
            DependencyProperty.Register(
                nameof(StateName),
                typeof(string),
                typeof(GoToStateAction),
                new PropertyMetadata(""));

        /// <summary>
        ///     Identifies the <see cref="TargetElement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register(
                nameof(TargetElement),
                typeof(FrameworkElement),
                typeof(GoToStateAction),
                new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets a value indicating whether visual transitions should be used during
        ///     the state change.
        ///     The default value is <c>true</c>.
        /// </summary>
        public bool UseTransitions
        {
            get { return (bool)GetValue(UseTransitionsProperty); }
            set { SetValue(UseTransitionsProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the name of the visual state to which the control should transition.
        /// </summary>
        public string StateName
        {
            get { return (string)GetValue(StateNameProperty); }
            set { SetValue(StateNameProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a <see cref="FrameworkElement"/> whose visual state should be changed
        ///     by this action.
        ///     This can be null. In this case, the action automatically finds the most appropriate
        ///     element whose state should be changed.
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        /// <summary>
        ///     Gets a value indicating whether the action can be executed with a null parameter.
        ///     This is dependent on whether <see cref="TargetElement"/> is set or not.
        ///     If <see cref="TargetElement"/> is null, the target of this action must come from the
        ///     trigger.
        /// </summary>
        protected override bool AllowNullParameter => TargetElement != null;
        
        /// <summary>
        ///     Transitions the attached <paramref name="element"/> to the visual state defined
        ///     in <see cref="StateName"/>.
        /// </summary>
        /// <param name="element">
        ///     The element whose visual state should be changed.
        /// </param>
        protected override void Execute(FrameworkElement element)
        {
            if (StateName == null)
            {
                throw new ArgumentNullException(
                    $"The {nameof(StateName)} property must not be null."
                );
            }

            var actualTarget = GetActualVisualStateChangeTarget(element);
            InteractivitySource.Info(
                "Transitioning target element \"{0}\" to state \"{1}\".", actualTarget, StateName
            );
            VisualStateManager.GoToState(actualTarget, StateName, UseTransitions);
        }
        
        /// <summary>
        ///     Returns a <see cref="FrameworkElement"/> whose visual state should be switched.
        ///     This searches an element which has visual states, starting from either 
        ///     <see cref="TargetElement"/> or <paramref name="elementParameter"/>.
        ///     If the search fails, this throws an exception.
        /// </summary>
        /// <param name="elementParameter">
        ///     An element passed in by a trigger.
        /// </param>
        /// <exception cref="InvalidOperationException" />
        private FrameworkElement GetActualVisualStateChangeTarget(FrameworkElement elementParameter)
        {
            var actualTarget = TargetElement ?? FindNearestStatefulElement(elementParameter);

            // It can happen that we don't find an element to transition.
            // In that case, simply return.
            if (actualTarget == null)
            {
                throw new InvalidOperationException(
                    $"Couldn't find a target element which has visual states defined. " +
                    $"Not changing any states. Element: {TargetElement ?? elementParameter}"
                );
            }
            else
            {
                return actualTarget;
            }
        }

        /// <summary>
        ///     Finds the nearest parent element which has visual states defined.
        /// </summary>
        /// <param name="element">
        ///     The control from which to start looking.
        /// </param>
        /// <returns>
        ///     An element which has visual states defined, or null, if no such element exists.
        /// </returns>
        /// <exception cref="ArgumentNullException" />
        private static FrameworkElement FindNearestStatefulElement(FrameworkElement element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));

            FrameworkElement currentElement = element;
            FrameworkElement currentParent = element.Parent as FrameworkElement;

            // Try to find an element which has visual state groups. Walk up the visual tree to do this.
            while (!HasVisualStateGroupsDefined(element) &&
                   ShouldContinueTreeWalk(currentParent))
            {
                currentElement = currentParent;
                currentParent = currentElement.Parent as FrameworkElement;
            }

            // If we found an element whose VisualStateGroups property is set, we can return
            // its (Templated)Parent. This should be happening in most cases and covers most situations,
            // like templates.
            if (HasVisualStateGroupsDefined(currentElement))
            {
                var templatedParent = VisualTreeHelper.GetParent(currentElement) as FrameworkElement;

                if (templatedParent != null)
                {
                    return templatedParent;
                }
                else
                {
                    // No parent. At least return something, even though this is probably ineffective.
                    return element;
                }
            }
            else
            {
                return null;
            }
        }

        private static bool HasVisualStateGroupsDefined(FrameworkElement element)
        {
            return element != null && VisualStateManager.GetVisualStateGroups(element).Count > 0;
        }

        private static bool ShouldContinueTreeWalk(FrameworkElement element)
        {
            // We can't go up the tree if the element is null.
            // Furthermore, UserControls are root controls which can have visual states.
            // Don't go up any further in that case.
            if (element is null || element is UserControl)
                return false;

            // If the element's Parent is null, it could mean that the element is the root object
            // of a ControlTemplate or DataTemplate.
            // -> We will only stop going up, if that IS NOT the case, i.e. there is no parent,
            //    but the element also isn't the root of a template.
            //    This could, for instance, be the case for a root window of an application.
            if (element.Parent is null)
            {
                var templatedParent = VisualTreeHelper.GetParent(element) as FrameworkElement;
                if (templatedParent == null ||
                    (!(templatedParent is Control) && !(templatedParent is ContentPresenter)))
                {
                    return false;
                }
            }

            return true;
        }

    }

}
