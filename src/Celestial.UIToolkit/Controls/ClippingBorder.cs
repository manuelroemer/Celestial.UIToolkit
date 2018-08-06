using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Celestial.UIToolkit.Extensions;
using static System.Math;

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

        private RectangleGeometry CreateClipGeometry()
        {
            return new RectangleGeometry()
            {
                RadiusX = this.CalculateClipRadiusX(),
                RadiusY = this.CalculateClipRadiusY(),
                Rect = new Rect(this.Child.RenderSize)
            };
        }

        /// <summary>
        /// Used to determine the <see cref="RectangleGeometry.RadiusX"/> property
        /// of the clip that is being applied to the underlying child.
        /// By overriding this method, you can specify your own CornerRadius to
        /// RadiusX handling.
        /// </summary>
        /// <returns>
        /// A <see cref="double"/> representing the x-axis radius of the resulting
        /// clip geometry.
        /// </returns>
        protected virtual double CalculateClipRadiusX() =>
            this.CalculateUniversalRadius();

        /// <summary>
        /// Used to determine the <see cref="RectangleGeometry.RadiusY"/> property
        /// of the clip that is being applied to the underlying child.
        /// By overriding this method, you can specify your own CornerRadius to
        /// RadiusY handling.
        /// </summary>
        /// <returns>
        /// A <see cref="double"/> representing the y-axis radius of the resulting
        /// clip geometry.
        /// </returns>
        protected virtual double CalculateClipRadiusY() =>
            this.CalculateUniversalRadius();

        private double CalculateUniversalRadius()
        {
            if (this.CornerRadius.HasUnifiedValues())
            {
                return this.CornerRadius.TopLeft;
            } 
            else
            {
                // This is where it gets hard:
                // The CornerRadius has different values. How to
                // translate them into the Rect's radii?
                // For now, take the largest of the 4 radius values.
                // This should be improved, but at the time of writing this,
                // there was no need for it.
                return Max(Max(this.CornerRadius.TopLeft, this.CornerRadius.TopRight),
                           Max(this.CornerRadius.BottomLeft, this.CornerRadius.BottomRight));
            }
        }
        
    }

}
