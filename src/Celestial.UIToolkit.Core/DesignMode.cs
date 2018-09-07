using System.ComponentModel;
using System.Windows;

namespace Celestial.UIToolkit
{

    /// <summary>
    /// Provides static members for determining whether the application is currently in
    /// the designer mode.
    /// </summary>
    public static class DesignMode
    {

        private static readonly DependencyObject _depObj = new DependencyObject();

        /// <summary>
        /// Gets a value whether the design mode is enabled.
        /// </summary>
        public static bool IsEnabled => DesignerProperties.GetIsInDesignMode(_depObj);
        
    }

}
