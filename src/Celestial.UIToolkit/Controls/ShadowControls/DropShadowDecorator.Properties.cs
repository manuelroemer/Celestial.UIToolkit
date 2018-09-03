using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Celestial.UIToolkit.Controls
{

    public partial class DropShadowDecorator
    {
		
        /// <summary>
        /// Identifies the <see cref="RenderingBias"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RenderingBiasProperty =
            DependencyProperty.Register(
                nameof(RenderingBias),
                typeof(RenderingBias),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
                    RenderingBias.Performance,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="ShadowColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register(
                nameof(ShadowColor),
                typeof(Color),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
					Colors.Black,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="ShadowOpacity"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowOpacityProperty =
            DependencyProperty.Register(
                nameof(ShadowOpacity),
                typeof(double),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
                    1d,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="BlurRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register(
                nameof(BlurRadius),
                typeof(double),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
                    5d,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="OffsetX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register(
                nameof(OffsetX),
                typeof(double),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
                    0d,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="OffsetY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register(
                nameof(OffsetY),
                typeof(double),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
                    0d,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));

        /// <summary>
        /// Identifies the <see cref="IsShadowEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsShadowEnabledProperty =
            DependencyProperty.Register(
                nameof(IsShadowEnabled),
                typeof(bool),
                typeof(DropShadowDecorator),
                new FrameworkPropertyMetadata(
                    true,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    Shadow_Changed));
        
        /// <summary>
        /// Gets or sets a <see cref="System.Windows.Media.Effects.RenderingBias"/> 
        /// to be used with the drop shadow effect.
        /// </summary>
        public RenderingBias RenderingBias
        {
            get { return (RenderingBias)GetValue(RenderingBiasProperty); }
            set { SetValue(RenderingBiasProperty, value); }
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
        /// An offset of (0;0) means that the cast shadow is directly under
        /// the element which casts it.
        /// </summary>
        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Y-Offset of the drop shadow.
        /// An offset of (0;0) means that the cast shadow is directly under
        /// the element which casts it.
        /// </summary>
        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the drop shadow effect is enabled at the
        /// moment.
        /// </summary>
        public bool IsShadowEnabled
        {
            get { return (bool)GetValue(IsShadowEnabledProperty); }
            set { SetValue(IsShadowEnabledProperty, value); }
        }

    }

}
