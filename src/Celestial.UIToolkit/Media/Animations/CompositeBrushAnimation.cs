using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.UIToolkit.Media.Animations
{
    
    /// <summary>
    ///     An animation which is able to animate <see cref="SolidColorBrush"/>,
    ///     <see cref="LinearGradientBrush"/> and <see cref="RadialGradientBrush"/>
    ///     objects.
    /// </summary>
    /// <remarks>
    ///     This animation is internally choosing one of the <see cref="BrushAnimation"/>
    ///     derivations.
    ///     Their limitations apply to this class aswell, meaning that the animated brushes
    ///     have to be of the same type, need to have certain shared properties, ...
    /// </remarks>
    internal class CompositeBrushAnimation
    {
    }

}
