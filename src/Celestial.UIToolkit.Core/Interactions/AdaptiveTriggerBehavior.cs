using System;
using System.Linq;
using System.Windows;
using Celestial.UIToolkit.Extensions;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Interactions
{

    /// <summary>
    ///     A behavior which gets triggered when the parent window of its associated object
    ///     reaches a certain size.
    ///     
    ///     This is a stateful trigger and passes the <see cref="Behavior.AssociatedObject"/>
    ///     to its actions.
    /// </summary>
    public sealed class AdaptiveTriggerBehavior : StatefulTriggerBehavior<FrameworkElement>
    {

        private Window _window;
        
        /// <summary>
        /// Identifies the <see cref="MinWindowWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinWindowWidthProperty =
            DependencyProperty.Register(
                nameof(MinWindowWidth),
                typeof(double),
                typeof(AdaptiveTriggerBehavior),
                new FrameworkPropertyMetadata(
                    0d,
                    (d, e) => ((AdaptiveTriggerBehavior)d).MinSize_Changed(e),
                    CoerceMinSize));

        /// <summary>
        /// Identifies the <see cref="MinWindowHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinWindowHeightProperty =
            DependencyProperty.Register(
                nameof(MinWindowHeight),
                typeof(double),
                typeof(AdaptiveTriggerBehavior),
                new FrameworkPropertyMetadata(
                    0d,
                    (d, e) => ((AdaptiveTriggerBehavior)d).MinSize_Changed(e),
                    CoerceMinSize));

        /// <summary>
        /// Gets or sets the minimum window width which is required for the trigger to become
        /// active.
        /// </summary>
        public double MinWindowWidth
        {
            get { return (double)GetValue(MinWindowWidthProperty); }
            set { SetValue(MinWindowWidthProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the minimum window height which is required for the trigger to become
        /// active.
        /// </summary>
        public double MinWindowHeight
        {
            get { return (double)GetValue(MinWindowHeightProperty); }
            set { SetValue(MinWindowHeightProperty, value); }
        }

        private static object CoerceMinSize(DependencyObject d, object baseValue)
        {
            // Ensure that the Min-Sizes don't get <= 0.
            var value = (double)baseValue;
            return Math.Max(0d, value);
        }

        /// <summary>
        /// When attached to an object, tries to find its parent window and throws an exception,
        /// if none is found.
        /// If successful, attaches event handlers to the window so that the trigger can listen
        /// for window size changes.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        /// <summary>
        /// Removes previously set event handlers from a window.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            _window.SizeChanged -= Window_SizeChanged;
            _window = null;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            FindParentWindow();
            _window.SizeChanged += Window_SizeChanged;
        }

        /// <summary>
        /// Tries to find a <see cref="Window"/> object in the visual tree, starting from the
        /// associated object.
        /// If no window is found, this throws an exception.
        /// </summary>
        private void FindParentWindow()
        {
            // The associated object is either directly a window, or must have a window as parent
            // in the visual tree.
            // If that is not the case, this trigger won't work, since it requires a window.
            if (AssociatedObject is Window)
            {
                _window = (Window)AssociatedObject;
            }
            else
            {
                _window = AssociatedObject.GetVisualAncestor(ancestor => ancestor is Window) as Window;
            }

            if (_window == null)
            {
                throw new InvalidOperationException(
                    $"The {nameof(AdaptiveTriggerBehavior)} could not find a parent window of the " +
                    $"associated object {AssociatedObject}."
                );
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReevaluateTriggerState();
        }

        private void MinSize_Changed(DependencyPropertyChangedEventArgs e)
        {
            if (IsAttached)
                ReevaluateTriggerState();
        }
        
        private void ReevaluateTriggerState()
        {
            if (IsTriggeredByWindowSize() && IsStrongestAdaptiveTriggerInGroup())
            {
                OnTriggered(true, AssociatedObject);
            }
            else
            {
                OnTriggered(false, AssociatedObject);
            }
        }

        private bool IsTriggeredByWindowSize()
        {
            return _window.ActualWidth >= MinWindowWidth &&
                   _window.ActualHeight >= MinWindowHeight;
        }

        private bool IsStrongestAdaptiveTriggerInGroup()
        {
            // We need to check if this adaptive trigger is the strongest one in a collection
            // of behaviors.
            // We've got the OwningCollection property for that. Now we only need to look for other
            // adaptive triggers in this collection.
            if (OwningCollection is null)
                return true;

            var strongestTrigger = OwningCollection
                .OfType<AdaptiveTriggerBehavior>()
                .Where(trigger => trigger.IsTriggeredByWindowSize())
                .OrderByDescending(trigger => trigger.MinWindowWidth)
                .ThenByDescending(trigger => trigger.MinWindowHeight)
                .First();

            return strongestTrigger == this;
        }

    }

}
