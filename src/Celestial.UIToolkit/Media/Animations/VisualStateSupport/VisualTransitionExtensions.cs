using System;
using System.Windows;

namespace Celestial.UIToolkit.Media.Animations
{

    internal static class VisualTransitionExtensions
    {
        
        private static readonly DependencyPropertyKey _dynamicStoryboardCompletedPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "DynamicStoryboardCompleted", typeof(bool), typeof(VisualTransitionExtensions), new PropertyMetadata(true));
        
        private static readonly DependencyPropertyKey _explicitStoryboardCompletedPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "ExplicitStoryboardCompleted", typeof(bool), typeof(VisualTransitionExtensions), new PropertyMetadata(true));
        
        public static bool GetDynamicStoryboardCompleted(this VisualTransition transition)
        {
            return (bool)transition.GetValue(_dynamicStoryboardCompletedPropertyKey.DependencyProperty);
        }
        
        public static void SetDynamicStoryboardCompleted(this VisualTransition transition, bool value)
        {
            transition.SetValue(_dynamicStoryboardCompletedPropertyKey, value);
        }
        
        public static bool GetExplicitStoryboardCompleted(this VisualTransition transition)
        {
            return (bool)transition.GetValue(_explicitStoryboardCompletedPropertyKey.DependencyProperty);
        }
        
        public static void SetExplicitStoryboardCompleted(this VisualTransition transition, bool value)
        {
            transition.SetValue(_explicitStoryboardCompletedPropertyKey, value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified <paramref name="transition"/>
        /// can be treated as a default transition inside a collection.
        /// This is the case, if its <see cref="VisualTransition.From"/> and
        /// <see cref="VisualTransition.To"/> properties are <c>null</c>.
        /// </summary>
        /// <param name="transition">The transition to be checked.</param>
        /// <returns>
        /// <c>true</c> if the transition can be treated as default; false if not.
        /// </returns>
        public static bool IsDefault(this VisualTransition transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            return transition.From == null && transition.To == null;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <paramref name="transition"/>
        /// or its underlying storyboard has a generated duration of 0.
        /// </summary>
        /// <param name="transition">The transition.</param>
        public static bool HasZeroDuration(this VisualTransition transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));
            return transition.GeneratedDuration == new Duration(TimeSpan.Zero) &&
                   (transition.Storyboard == null || transition.Storyboard.Duration == new Duration(TimeSpan.Zero));
        }

    }

}
