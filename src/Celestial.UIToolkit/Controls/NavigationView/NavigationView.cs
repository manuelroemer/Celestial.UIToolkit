using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    ///     A control which provides a central navigation structure to an application by providing
    ///     a pane for navigation commands and a central place for the content which is the current
    ///     navigation target.
    /// </summary>
    /// <remarks>
    ///     This control is built after the UWP navigation control.
    ///     As a result, the control behaves similarly and provides the same kind of properties.
    ///     While feature-parity is not guaranteed, having a closer look at the official 
    ///     documentation by Microsoft will be a good idea.
    /// </remarks>
    public partial class NavigationView : HeaderedContentControl
    {

        static NavigationView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NavigationView), new FrameworkPropertyMetadata(typeof(NavigationView)));
        }

    }

}
