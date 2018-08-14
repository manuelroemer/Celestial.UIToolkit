using Celestial.UIToolkit.Common;
using System.Windows;

namespace Celestial.UIToolkit.Media.Animations
{

    internal sealed class PointAnimationHelper : Singleton<PointAnimationHelper>, IAnimationHelper<Point>
    {

        private PointAnimationHelper() { }

        public Point GetZeroValue() => new Point(0, 0);

        public Point AddValues(Point a, Point b)
        {
            return new Point(
                DoubleAnimationHelper.Instance.AddValues(a.X, b.X),
                DoubleAnimationHelper.Instance.AddValues(a.Y, b.Y));
        }

        public Point SubtractValues(Point a, Point b)
        {
            return new Point(
                DoubleAnimationHelper.Instance.SubtractValues(a.X, b.X),
                DoubleAnimationHelper.Instance.SubtractValues(a.Y, b.Y));
        }

        public Point ScaleValue(Point value, double factor)
        {
            return new Point(
                DoubleAnimationHelper.Instance.ScaleValue(value.X, factor),
                DoubleAnimationHelper.Instance.ScaleValue(value.Y, factor));
        }

        public Point InterpolateValue(Point from, Point to, double progress)
        {
            return new Point(
                DoubleAnimationHelper.Instance.InterpolateValue(from.X, to.X, progress),
                DoubleAnimationHelper.Instance.InterpolateValue(from.Y, to.Y, progress));
        }

    }

}
