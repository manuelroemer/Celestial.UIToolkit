using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// An extension of the <see cref="Border"/> class which
    /// applies a clip to the underlying child, so that the
    /// child will not be rendered over the border's edges,
    /// if a corner radius is set.
    /// </summary>
    public class ClippingBorder : Border
    {

        private Geometry _oldChildClip;

        /// <summary>
        /// Gets or sets the single child of the clipping border.
        /// </summary>
        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (this.Child == value) return;
                this.RestoreCurrentChildClip();
                base.Child = value;
                this.SetOldChildClip();
            }
        }

        private void RestoreCurrentChildClip()
        {
            if (this.Child != null)
            {
                this.Child.Clip = _oldChildClip;
            }
        }

        private void SetOldChildClip()
        {
            _oldChildClip = this.Child?.Clip;
        }

        /// <summary>
        /// Draws the contents of a <see cref="DrawingContext"/> object during the
        /// render pass of a System.Windows.Controls.Border.
        /// </summary>
        /// <param name="dc"> The <see cref="DrawingContext"/> that defines the object to be drawn.</param>
        protected override void OnRender(DrawingContext dc)
        {
            this.ApplyCornerRadiusClipToChild();
            base.OnRender(dc);
        }

        private void ApplyCornerRadiusClipToChild()
        {
            if (this.Child != null)
            {
                this.Child.Clip = this.CreateClipGeometry();
            }
        }

        private Geometry CreateClipGeometry()
        {
            return GeometryHelper.CreateRectGeometryWithCornerRadius(
                this.CornerRadius, new Rect(this.Child.RenderSize));
        }
        


        private class GeometryHelper
        {
            
            private const double RotationAngle = 90d;
            private const bool IsLargeArc = false;
            private const bool IsStroked = false;
            private const bool IsSmoothJoin = true;

            private Rect _boundsRect;
            private CornerRadius _cornerRadius;
            private StreamGeometry _geometry;
            private StreamGeometryContext _context;

            private GeometryHelper(CornerRadius cornerRadius, Rect bounds)
            {
                // The CornerRadius must never be larger than the size. Otherwise, the geometry will
                // look totally off, since (for instance) an arc is drawn the next line segment's destination.
                // To avoid this, shrink a too-large CornerRadius to the exact size of the bounds rect.
                _cornerRadius = CornerRadiusHelper.ShrinkToSize(cornerRadius, bounds.Size);
                _boundsRect = bounds;
            }
            
            public static Geometry CreateRectGeometryWithCornerRadius(
                CornerRadius cornerRadius, Rect bounds)
            {
                return new GeometryHelper(cornerRadius, bounds).CreateGeometry();
            }

            private Geometry CreateGeometry()
            {
                this.InitializeGeometry();
                this.DrawTopLeftCorner();
                this.DrawTopLine();
                this.DrawTopRightCorner();
                this.DrawRightLine();
                this.DrawBottomRightCorner();
                this.DrawBottomLine();
                this.DrawBottomLeftCorner();
                this.DrawLeftLine();

                _context.Close();
                return _geometry;
            }

            private void InitializeGeometry()
            {
                const bool isFilled = true;
                const bool isClosed = true;

                _geometry = new StreamGeometry();
                _context = _geometry.Open();
                _context.BeginFigure(
                    new Point(
                        _boundsRect.TopLeft.X, 
                        _boundsRect.TopLeft.Y + _cornerRadius.TopLeft), 
                    isFilled, isClosed);
            }

            private void DrawTopLeftCorner()
            {
                this.DrawArc(new Point(
                    _boundsRect.TopLeft.X + _cornerRadius.TopLeft, 
                    _boundsRect.TopLeft.Y),
                    new Size(
                        _cornerRadius.TopLeft, 
                        _cornerRadius.TopLeft));
            }

            private void DrawTopLine()
            {
                this.DrawLine(new Point(
                    _boundsRect.TopRight.X - _cornerRadius.TopRight, 
                    _boundsRect.TopRight.Y));
            }

            private void DrawTopRightCorner()
            {
                this.DrawArc(new Point(
                    _boundsRect.TopRight.X,  
                    _boundsRect.TopRight.Y + _cornerRadius.TopRight),
                    new Size(
                        _cornerRadius.TopRight, 
                        _cornerRadius.TopRight));
            }

            private void DrawRightLine()
            {
                this.DrawLine(new Point(
                    _boundsRect.BottomRight.X, 
                    _boundsRect.BottomRight.Y - _cornerRadius.BottomRight));
            }

            private void DrawBottomRightCorner()
            {
                this.DrawArc(new Point(
                    _boundsRect.BottomRight.X - _cornerRadius.BottomRight,
                    _boundsRect.BottomRight.Y),
                    new Size(
                        _cornerRadius.BottomRight,
                        _cornerRadius.BottomRight));
            }
            
            private void DrawBottomLine()
            {
                this.DrawLine(new Point(
                    _boundsRect.BottomLeft.X + _cornerRadius.BottomLeft,
                    _boundsRect.BottomLeft.Y));
            }

            private void DrawBottomLeftCorner()
            {
                this.DrawArc(new Point(
                    _boundsRect.BottomLeft.X,
                    _boundsRect.BottomLeft.Y - _cornerRadius.BottomLeft),
                    new Size(
                        _cornerRadius.BottomLeft, _cornerRadius.BottomLeft));
            }

            private void DrawLeftLine()
            {
                this.DrawLine(new Point(
                    _boundsRect.TopLeft.X,
                    _boundsRect.TopLeft.Y + _cornerRadius.TopLeft));
            }

            private void DrawArc(Point destination, Size size)
            {
                _context.ArcTo(
                    destination,
                    size,
                    RotationAngle,
                    IsLargeArc,
                    SweepDirection.Clockwise,
                    IsStroked,
                    IsSmoothJoin);
            }

            private void DrawLine(Point destination)
            {
                _context.LineTo(
                    destination,
                    IsStroked,
                    IsSmoothJoin);
            }
            
        }

        private static class CornerRadiusHelper
        {

            /// <summary>
            /// Shrinks the values of the corner radius to the specified bounds.
            /// For instance, if <see cref="CornerRadius.TopLeft"/> + <see cref="CornerRadius.TopRight"/>
            /// is larger than the bound's width, they are both shrunk down, to that their sum equals said width.
            /// </summary>
            /// <param name="cornerRadius">The corner radius.</param>
            /// <param name="maxSize">
            /// The bounds to which the corner radius should be shrunk.
            /// </param>
            /// <returns>
            /// The resulting <see cref="CornerRadius"/> of the operation.
            /// </returns>
            public static CornerRadius ShrinkToSize(CornerRadius cornerRadius, Size maxSize)
            {
                if (maxSize.Width < 0 || maxSize.Height < 0)
                    throw new ArgumentException("The size must have positive values.", nameof(maxSize));
                
                double topLeft = cornerRadius.TopLeft;
                double topRight = cornerRadius.TopRight;
                double bottomLeft = cornerRadius.BottomLeft;
                double bottomRight = cornerRadius.BottomRight;

                ShrinkEdge(ref topLeft, ref topRight, maxSize.Width);
                ShrinkEdge(ref bottomLeft, ref bottomRight, maxSize.Width);
                ShrinkEdge(ref topLeft, ref bottomLeft, maxSize.Height);
                ShrinkEdge(ref topRight, ref bottomRight, maxSize.Height);

                return new CornerRadius(
                    topLeft, topRight, bottomRight, bottomLeft);
            }

            private static void ShrinkEdge(ref double part1, ref double part2, double maxSize)
            {
                if (part1 + part2 > maxSize)
                {
                    double divisor = (part1 + part2) / maxSize;
                    part1 /= divisor;
                    part2 /= divisor;
                }
            }

        }

    }

}
