using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Celestial.UIToolkit.Controls
{

    public partial class DropShadowPanel
    {
		
        private static readonly DependencyPropertyKey DropShadowEffectPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(DropShadowEffect),
                typeof(DropShadowEffect),
                typeof(DropShadowPanel),
                new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="DropShadowEffect"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DropShadowEffectProperty =
            DropShadowEffectPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="ShadowColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register(
                nameof(ShadowColor),
                typeof(Color),
                typeof(DropShadowPanel),
                new PropertyMetadata(
					Colors.Black,
					Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="ShadowOpacity"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowOpacityProperty =
            DependencyProperty.Register(
                nameof(ShadowOpacity),
                typeof(double),
                typeof(DropShadowPanel),
                new PropertyMetadata(
                    1d,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="BlurRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register(
                nameof(BlurRadius),
                typeof(double),
                typeof(DropShadowPanel),
                new PropertyMetadata(
                    5d, Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="OffsetX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register(
                nameof(OffsetX),
                typeof(double),
                typeof(DropShadowPanel),
                new PropertyMetadata(
                    0d,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="OffsetY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register(
                nameof(OffsetY),
                typeof(double),
                typeof(DropShadowPanel),
                new PropertyMetadata(
                    0d,
                    Shadow_Changed));

        /// <summary>
        /// Gets the calculated <see cref="DropShadowEffect"/> which is applied to the
        /// panel's content.
        /// </summary>
        public DropShadowEffect DropShadowEffect
        {
            get { return (DropShadowEffect)GetValue(DropShadowEffectProperty); }
            private set { SetValue(DropShadowEffectPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the shadow's color.
        /// </summary>
        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the opacity of the drop shadow.
        /// </summary>
        public double ShadowOpacity
        {
            get { return (double)GetValue(ShadowOpacityProperty); }
            set { SetValue(ShadowOpacityProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the radius of the shadow's blur effect.
        /// </summary>
        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        /// <summary>
        /// Gets or sets the X-Offset of the drop shadow.
        /// </summary>
        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Y-Offset of the drop shadow.
        /// </summary>
        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

    }

}
