using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    /// <summary>
    /// A special <see cref="AnimationClock"/> which can be controlled from the outside.
    /// </summary>
    public class ControllableAnimationClock : AnimationClock
    {

        /// <summary>
        /// Gets a clock which has just been started and whose progress is thus 0.
        /// </summary>
        public static ControllableAnimationClock Started { get; }

        /// <summary>
        /// Gets a clock which has finished and whose progress is thus 1.
        /// </summary>
        public static ControllableAnimationClock Finished { get; }
        
        public ControllableAnimationClock(AnimationTimeline animation)
            : base(animation) { }
        
    }
    
}
