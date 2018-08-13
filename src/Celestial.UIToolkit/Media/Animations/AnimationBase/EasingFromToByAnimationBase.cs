using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.UIToolkit.Media.Animations.AnimationBase
{

    /// <summary>
    /// Defines an abstract base class for a From/To/By animation,
    /// which, in comparison to the <see cref="FromToByAnimationBase{T}"/>,
    /// additionally uses an easing function.
    /// </summary>
    /// <typeparam name="T">The type which is being animated by the animation.</typeparam>
    public abstract class EasingFromToByAnimationBase<T> : FromToByAnimationBase<T>
    {



    }

}
