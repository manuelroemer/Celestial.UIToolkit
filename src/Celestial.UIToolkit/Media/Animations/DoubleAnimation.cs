﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Media.Animations
{

#if DEBUG

    public class DoubleAnimation : EasingFromToByAnimationBase<double>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new DoubleAnimation();
        }

        protected override double InterpolateValueCore(double from, double to, double progress)
        {
            return from + (to - from) * progress;
        }

        protected override double AddValues(double a, double b)
        {
            return a + b;
        }

        protected override double ScaleValue(double value, double factor)
        {
            return value * factor;
        }

        protected override double SubtractValues(double a, double b)
        {
            return a - b;
        }
    }

#endif

}
