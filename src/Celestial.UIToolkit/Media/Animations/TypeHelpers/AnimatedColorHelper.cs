using Celestial.UIToolkit.Common;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    internal class AnimatedColorHelper : Singleton<AnimatedColorHelper>, IAnimatedTypeHelper<Color>
    {

        private AnimatedColorHelper() { }

        public Color GetZeroValue()
        {
            return new Color();
        }

        public Color AddValues(Color a, Color b)
        {
            return a + b;
        }

        public Color SubtractValues(Color a, Color b)
        {
            return a - b;
        }

        public Color ScaleValue(Color value, double factor)
        {
            return value * (float)factor;
        }

        public Color InterpolateValue(Color from, Color to, double progress)
        {
            return from + (to - from) * (float)progress;
        }

    }

}
