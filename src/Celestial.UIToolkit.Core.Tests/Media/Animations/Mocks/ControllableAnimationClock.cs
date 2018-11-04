using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations.Mocks
{

    /// <summary>
    /// A special <see cref="AnimationClock"/> which can be controlled from the outside.
    /// </summary>
    public class ControllableAnimationClock : AnimationClock
    {
        
        public ControllableAnimationClock(AnimationTimeline animation)
            : base(animation) { }

        protected override TimeSpan GetCurrentTimeCore()
        {
            return TimeSpan.FromSeconds(1.0d);
        }

    }
    
}
