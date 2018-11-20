using System;
using System.Windows;
using System.Windows.Data;

namespace Celestial.UIToolkit.Extensions
{

    /// <summary>
    /// Provides extension methods for the <see cref="Setter"/> class.
    /// </summary>
    internal static class SetterExtensions
    {

        /// <summary>
        ///     Applies the setter to an element which gets located via the 
        ///     <paramref name="rootElement"/>.
        /// </summary>
        /// <param name="setter">
        ///     The setter.
        /// </param>
        /// <param name="rootElement">
        ///     An element which is used to locate the setter target via its
        ///     <see cref="FrameworkElement.FindName(string)"/> method.
        ///     If the setter doesn't have a target name, this element is used as a target.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public static void ApplyToElement(this Setter setter, FrameworkElement rootElement)
        {
            if (setter is null)
                throw new ArgumentNullException(nameof(setter));
            if (rootElement is null)
                throw new ArgumentNullException(nameof(rootElement));
            if (setter.Property is null)
                throw new ArgumentException("The setter's Property must not be null.");

            var setterTarget = setter.FindSetterTarget(rootElement);
            if (setter.Value is Binding binding)
            {
                BindingOperations.SetBinding(setterTarget, setter.Property, binding);
            }
            else
            {
                // We are dealing with a "normal" value.
                // SetCurrentValue doesn't change the property source, but changes the value until
                // reset.
                // This is ideal, as long as the property gets reset again (which can be done
                // via the other extension methods).
                setterTarget.SetCurrentValue(setter.Property, setter.Value);
            }
        }

        /// <summary>
        ///     Removes the effect of the setter that has previously been applied to an element
        ///     via <see cref="ApplyToElement(Setter, FrameworkElement)"/>.
        /// </summary>
        /// <param name="setter">
        ///     The setter.
        /// </param>
        /// <param name="rootElement">
        ///     An element which is used to locate the setter target via its
        ///     <see cref="FrameworkElement.FindName(string)"/> method.
        ///     If the setter doesn't have a target name, this element is used as a target.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public static void RemoveFromElement(this Setter setter, FrameworkElement rootElement)
        {
            if (setter is null)
                throw new ArgumentNullException(nameof(setter));
            if (rootElement is null)
                throw new ArgumentNullException(nameof(rootElement));
            if (setter.Property is null)
                throw new ArgumentException("The setter's Property must not be null.");

            var setterTarget = FindSetterTarget(setter, rootElement);
            if (setter.Value is Binding setterBinding)
            {
                // Is the current value of the target the setter's binding? If not, don't clear it.
                var elementBinding = BindingOperations.GetBinding(setterTarget, setter.Property);
                if (elementBinding == setterBinding)
                {
                    BindingOperations.ClearBinding(setterTarget, setter.Property);
                }
            }
            else
            {
                // Only invalidate the property, if the value actually equals the value of the
                // setter. Otherwise, it might have been modified in between.
                if (setterTarget.GetValue(setter.Property) == setter.Value)
                {
                    setterTarget.InvalidateProperty(setter.Property);
                }
            }
        }

        /// <summary>
        ///     Tries to find the target element of the setter.
        ///     This searches for the <see cref="Setter.TargetName"/> in the 
        ///     <paramref name="rootElement"/>'s name scope, or directly returns the
        ///     <paramref name="rootElement"/>, if no <see cref="Setter.TargetName"/> is provided.
        /// </summary>
        /// <param name="setter">
        ///     The setter.
        /// </param>
        /// <param name="rootElement">
        ///     The root element from which to start looking.
        /// </param>
        /// <param name="target">
        ///     A <see cref="DependencyObject"/> which will hold the result of the operation.
        /// </param>
        /// <returns>
        ///     true if an element was found; false if not.
        /// </returns>
        public static bool TryFindSetterTarget(
            this Setter setter, 
            FrameworkElement rootElement,
            out DependencyObject target)
        {
            if (setter is null) throw new ArgumentNullException(nameof(setter));
            if (rootElement is null) throw new ArgumentNullException(nameof(rootElement)); 
            
            // If the setter doesn't have a TargetName, assume that it means the element from
            // which we start searching.
            if (string.IsNullOrEmpty(setter.TargetName))
            {
                target = rootElement;
                return true;
            }
            
            // Try to locate the target in the template part, or in the control itself.
            target = rootElement.FindName(setter.TargetName) as DependencyObject;
            return target != null;
        }

        /// <summary>
        ///     Tries to find the target element of the setter.
        ///     If no such element is found, this throws an <see cref="InvalidOperationException"/>.
        ///     This searches for the <see cref="Setter.TargetName"/> in the 
        ///     <paramref name="rootElement"/>'s name scope, or directly returns the
        ///     <paramref name="rootElement"/>, if no <see cref="Setter.TargetName"/> is provided.
        /// </summary>
        /// <param name="setter">
        ///     The setter.
        /// </param>
        /// <param name="rootElement">
        ///     The root element from which to start looking.
        /// </param>
        /// <returns>
        ///     The setter's target element.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if no target element was found.
        /// </exception>
        public static DependencyObject FindSetterTarget(this Setter setter, FrameworkElement rootElement)
        {
            if (TryFindSetterTarget(setter, rootElement, out var target))
            {
                return target;
            }
            else
            {
                throw new InvalidOperationException(
                    $"Could not locate an element with the TargetName \"{setter.TargetName}\"."
                );
            }
        }

    }

}
