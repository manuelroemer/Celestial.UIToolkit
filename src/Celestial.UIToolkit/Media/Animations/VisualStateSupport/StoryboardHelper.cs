using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

    internal static class StoryboardHelper
    {

        /// <summary>
        /// Copies the <see cref="Storyboard.TargetProperty"/>,
        /// <see cref="Storyboard.TargetNameProperty"/> and 
        /// <see cref="Storyboard.TargetPropertyProperty"/>
        /// from the <paramref name="source"/> timeline to the
        /// <paramref name="destination"/> timeline,
        /// if none of them is null.
        /// </summary>
        /// <param name="rootContainer">
        /// A root element which contains the timelines.
        /// Used to find a target, if only its name is specified.
        /// </param>
        /// <param name="source">The source timeline.</param>
        /// <param name="destination">The destination timeline.</param>
        public static void CopyTargetProperties(FrameworkElement rootContainer, Timeline source, Timeline destination)
        {
            if (source == null || destination == null) return;
            var targetName = Storyboard.GetTargetName(source);
            var target = Storyboard.GetTarget(source);
            var targetProperty = Storyboard.GetTargetProperty(source);
            
            // If no target is set, try to locate it via its name.
            if (target == null && !string.IsNullOrEmpty(targetName))
                target = rootContainer.FindName(targetName) as DependencyObject;

            if (!string.IsNullOrEmpty(targetName))
                Storyboard.SetTargetName(destination, targetName);
            if (target != null)
                Storyboard.SetTarget(destination, target);
            if (targetProperty != null)
                Storyboard.SetTargetProperty(destination, targetProperty);
        }

    }

}
