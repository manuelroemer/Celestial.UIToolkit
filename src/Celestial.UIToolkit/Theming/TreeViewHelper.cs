using System.Windows;

namespace Celestial.UIToolkit.Theming
{

    internal static class TreeViewHelper
    {

        /// <summary>
        /// An attached dependency property which is used to get and set the depth of a
        /// TreeViewItem from a style.
        /// Setting this property works recursively.
        /// </summary>
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
