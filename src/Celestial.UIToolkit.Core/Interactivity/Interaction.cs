using System;
using System.Windows;

namespace Celestial.UIToolkit.Interactivity
{

    /// <summary>
    /// Provides attached dependency properties for attaching <see cref="Behavior"/> instances
    /// to <see cref="FrameworkElement"/> instances from XAML.
    /// </summary>
    public static class Interaction
    {

        /// <summary>
        /// Identifies the Behaviors attached dependency property which defines a set
        /// of <see cref="IBehavior"/> instances which are currently attached to an object.
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.RegisterAttached(
                "Behaviors",
                typeof(BehaviorCollection),
                typeof(Interaction),
                new PropertyMetadata(null));

        /// <summary>
        ///     Gets the value of the <see cref="BehaviorsProperty"/> attached dependency property,
        ///     which is always a list of behaviors that are going to be attached to the specified
        ///     <paramref name="obj"/>.
        ///     
        ///     If <paramref name="obj"/> is a <see cref="FrameworkElement"/>, the
        ///     <see cref="BehaviorCollection"/> will automatically be attached/detached,
        ///     whenever the element is loaded/unloaded.
        ///     In any other case, the attaching/detaching of the collection will have to be done
        ///     manually.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="DependencyObject"/> to which the behaviors are supposed to be
        ///     attached.
        /// </param>
        /// <returns>
        ///     The local value of the <see cref="BehaviorsProperty"/> attached dependency property,
        ///     which is always an instance of <see cref="BehaviorCollection"/>.
        /// </returns>
        public static BehaviorCollection GetBehaviors(DependencyObject obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));

            var currentCollection = (BehaviorCollection)obj.GetValue(BehaviorsProperty);
            if (currentCollection == null)
            {
                currentCollection = new BehaviorCollection();
                obj.SetValue(BehaviorsProperty, currentCollection);

                // Attach by default, and anytime a potential FrameworkElement changes its loaded state.
                currentCollection.Attach(obj);

                if (obj is FrameworkElement frameworkElement)
                {
                    // The idea for this comes from:
                    // https://www.pedrolamas.com/2015/10/23/how-to-prevent-memory-leaks-in-behaviors/
                    frameworkElement.Loaded += FrameworkElement_Loaded;
                    frameworkElement.Unloaded += FrameworkElement_Unloaded;
                }
            }

            return currentCollection;
        }
        
        private static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)sender;
            GetBehaviors(frameworkElement).Attach(frameworkElement);
        }

        private static void FrameworkElement_Unloaded(object sender, RoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)sender;
            GetBehaviors(frameworkElement).Detach();
        }
    }

}
