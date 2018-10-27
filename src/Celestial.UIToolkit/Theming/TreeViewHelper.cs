using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Theming
{

    /// <summary>
    /// Provides members which are required for implementing the default style of a
    /// <see cref="TreeViewItem"/>.
    /// </summary>
    internal static class TreeViewHelper
    {

        /// <summary>
        /// An attached dependency property which is used to get and set the depth of a
        /// TreeViewItem from a style.
        /// Check the example for seeing how this can be implemented.
        /// </summary>
        /// <example>
        ///     &lt;Setter Property="theming:TreeViewHelper.DepthLevel"&gt;
        ///         &lt;Setter.Value&gt;
        ///             &lt;Binding Path="(theming:TreeViewHelper.DepthLevel)"
        ///                      RelativeSource="{RelativeSource AncestorType=TreeViewItem}"
        ///                      FallbackValue="0"
        ///                      TargetNullValue="0"
        ///                      ConverterParameter="1"&gt;
        ///                 &lt;Binding.Converter&gt;
        ///                     &lt;c:MathOperationConverter Operator="Add" /&gt;
        ///                 &lt;/Binding.Converter&gt;
        ///             &lt;/Binding&gt;
        ///         &lt;/Setter.Value&gt;
        ///     &lt;/Setter&gt;
        /// </example>
        public static readonly DependencyProperty DepthLevelProperty =
            DependencyProperty.RegisterAttached(
                "DepthLevel",
                typeof(int),
                typeof(TreeViewHelper),
                new PropertyMetadata(0));
        
        public static int GetDepthLevel(DependencyObject obj) =>
            (int)obj.GetValue(DepthLevelProperty);
        
        public static void SetDepthLevel(DependencyObject obj, int value) =>
            obj.SetValue(DepthLevelProperty, value);
        
    }

}
