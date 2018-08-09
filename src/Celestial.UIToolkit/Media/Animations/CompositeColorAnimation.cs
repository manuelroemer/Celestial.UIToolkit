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
    ///     Even though the animation supports a set of different brushes,
    ///     it will typically not be able to do "inter-brush" animations.
    ///     One animation can only animate a single type of brush.
    ///     
    ///     In addition, when animating gradient brushes, the animation can
    ///     only deal with brush values which have a fixed number of
    ///     gradient stops.
    ///     Deviations from this number will cause the animation to fail.
    /// </remarks>
    internal class CompositeColorAnimation
    {
    }

}
