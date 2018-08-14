using Celestial.UIToolkit.Common;
using System.Windows;

namespace Celestial.UIToolkit.Media.Animations
{

    internal sealed class AnimatedPointHelper : Singleton<AnimatedPointHelper>, IAnimatedTypeHelper<Point>
    {

        private AnimatedPointHelper() { }

        public Point GetZeroValue() => new Point(0, 0);

        public Point AddValues(Point a, Point b)
        {
            return new Point(
                AnimatedDoubleHelper.Instance.AddValues(a.X, b.X),
                AnimatedDoubleHelper.Instance.AddValues(a.Y, b.Y));
        }

        public Point SubtractValues(Point a, Point b)
        {
            return new Point(
                AnimatedDoubleHelper.Instance.SubtractValues(a.X, b.X),
                AnimatedDoubleHelper.Instance.SubtractValues(a.Y, b.Y));
        }

        public Point ScaleValue(Point value, double factor)
        {
            return new Point(
                AnimatedDoubleHelper.Instance.ScaleValue(value.X, factor),
                AnimatedDoubleHelper.Instance.ScaleValue(value.Y, factor));
        }

        public Point InterpolateValue(Point from, Point to, double progress)
        {
            return new Point(
                AnimatedDoubleHelper.Instance.InterpolateValue(from.X, to.X, progress),
                AnimatedDoubleHelper.Instance.InterpolateValue(from.Y, to.Y, progress));
        }

    }

}
