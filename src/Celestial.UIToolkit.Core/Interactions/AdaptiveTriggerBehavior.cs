using System;
using System.Windows;
using Celestial.UIToolkit.Extensions;
using Celestial.UIToolkit.Interactivity;

namespace Celestial.UIToolkit.Interactions
{

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
                new PropertyMetadata(
                    0d,
                    (d, e) => ((AdaptiveTriggerBehavior)d).MinSize_Changed(e)));

        /// <summary>
        /// Identifies the <see cref="MinWindowHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinWindowHeightProperty =
            DependencyProperty.Register(
                nameof(MinWindowHeight),
                typeof(double),
                typeof(AdaptiveTriggerBehavior),
                new PropertyMetadata(
                    0d,
                    (d, e) => ((AdaptiveTriggerBehavior)d).MinSize_Changed(e)));

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

        /// <summary>
        /// Checks if the trigger is active and invokes the actions, if so.
        /// </summary>
        private void ReevaluateTriggerState()
        {
            if (_window.ActualWidth >= MinWindowWidth && 
                _window.ActualHeight >= MinWindowHeight)
            {
                OnTriggered(true, AssociatedObject);
            }
            else
            {
                OnTriggered(false, AssociatedObject);
            }
        }

    }

}
