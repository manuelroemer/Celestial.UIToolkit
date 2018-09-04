using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Extends the <see cref="Decorator"/> class with additionalattached members.
    /// </summary>
    public static class DecoratorExtensions
    {

        /// <summary>
        /// Identifies the Child attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ChildProperty =
            DependencyProperty.RegisterAttached(
                "Child",
                typeof(UIElement),
                typeof(DecoratorExtensions),
                new PropertyMetadata(
                    null,
                    Child_Changed));

        /// <summary>
        /// Gets the value of the <see cref="ChildProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ChildProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ChildProperty"/> attached dependency property,
        /// which is of type <see cref="UIElement"/>.
        /// </returns>
        public static UIElement GetChild(DependencyObject obj) =>
            (UIElement)obj.GetValue(ChildProperty);

        /// <summary>
        /// Sets the value of the <see cref="ChildProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ChildProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetChild(DependencyObject obj, UIElement value) =>
            obj.SetValue(ChildProperty, value);

        private static void Child_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Decorator decorator)
            {
                decorator.Child = (UIElement)e.NewValue;
            }
        }

    }

}
