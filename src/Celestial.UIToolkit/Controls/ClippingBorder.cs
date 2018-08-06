using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private object _oldChildClip;

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
                this.Child.SetValue(ClipProperty, _oldChildClip);
            }
        }

        private void SetOldChildClip()
        {
            _oldChildClip = this.Child?.ReadLocalValue(ClipProperty);
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
                RadiusX = this.DetermineClipRadiusX(),
                RadiusY = this.DetermineClipRadiusY(),
                Rect = new Rect(this.Child.RenderSize)
            };
        }

        private double DetermineClipRadiusX()
        {
            return this.CornerRadius.TopLeft;
        }

        private double DetermineClipRadiusY()
        {
            return this.CornerRadius.TopLeft;
        }

    }

}
