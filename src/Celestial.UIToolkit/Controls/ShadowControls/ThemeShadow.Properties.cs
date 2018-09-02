using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Celestial.UIToolkit.Controls
{

    public partial class ThemeShadow
    {

        /// <summary>
        /// Identifies the Shadow attached dependency property.
        /// When attached to an object, the specified shadow will be rendered on the control.
        /// </summary>
        public static readonly DependencyProperty ShadowProperty =
            DependencyProperty.RegisterAttached(
                "Shadow",
                typeof(ThemeShadow),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    null,
                    OnAttachedToElement));

        /// <summary>
        /// Gets the value of the <see cref="ShadowProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ShadowProperty"/> attached dependency property,
        /// which is of type <see cref="ThemeShadow"/>.
        /// </returns>
        public static ThemeShadow GetShadow(DependencyObject obj) =>
            (ThemeShadow)obj.GetValue(ShadowProperty);

        /// <summary>
        /// Sets the value of the <see cref="ShadowProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ShadowProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetShadow(DependencyObject obj, ThemeShadow value) =>
            obj.SetValue(ShadowProperty, value);

        /// <summary>
        /// Identifies the <see cref="RenderingBias"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RenderingBiasProperty =
            DependencyProperty.Register(
                nameof(RenderingBias),
                typeof(RenderingBias),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    RenderingBias.Performance));

        /// <summary>
        /// Identifies the <see cref="ShadowColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register(
                nameof(ShadowColor),
                typeof(Color),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    Colors.Black));

        /// <summary>
        /// Identifies the <see cref="ShadowOpacity"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowOpacityProperty =
            DependencyProperty.Register(
                nameof(ShadowOpacity),
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    1d));

        /// <summary>
        /// Identifies the <see cref="IsShadowEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsShadowEnabledProperty =
            DependencyProperty.Register(
                nameof(IsShadowEnabled),
                typeof(bool),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    true));

        /// <summary>
        /// Identifies the <see cref="Elevation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ElevationProperty =
            DependencyProperty.Register(
                nameof(Elevation),
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    1d,
                    ShadowElevation_Changed));

        /// <summary>
        /// Identifies the <see cref="ElevationShadowLengthMultiplier"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ElevationShadowLengthMultiplierProperty =
            DependencyProperty.Register(
                nameof(ElevationShadowLengthMultiplier),
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    4d,
                    ShadowElevation_Changed));

        private static readonly DependencyPropertyKey BlurRadiusPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(BlurRadius),
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="BlurRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            BlurRadiusPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey OffsetXPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(OffsetX),
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="OffsetX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty =
            OffsetXPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey OffsetYPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(OffsetY),
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="OffsetY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty =
            OffsetYPropertyKey.DependencyProperty;

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
        /// Gets or sets a value indicating whether the drop shadow effect is enabled at the
        /// moment.
        /// </summary>
        public bool IsShadowEnabled
        {
            get { return (bool)GetValue(IsShadowEnabledProperty); }
            set { SetValue(IsShadowEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current elevation of the shadow.
        /// </summary>
        public double Elevation
        {
            get { return (double)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value with which the <see cref="Elevation"/> is multiplied, before the
        /// shadow's final length is calculated.
        /// </summary>
        public double ElevationShadowLengthMultiplier
        {
            get { return (double)GetValue(ElevationShadowLengthMultiplierProperty); }
            set { SetValue(ElevationShadowLengthMultiplierProperty, value); }
        }

        /// <summary>
        /// Gets a drop shadow's blur radius.
        /// This blur radius is the result of a translation of this <see cref="ThemeShadow"/> to
        /// a common drop shadow.
        /// </summary>
        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            private set { SetValue(BlurRadiusPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets a drop shadow's X-Offset.
        /// This value is the result of a translation of this <see cref="ThemeShadow"/> to
        /// a common drop shadow.
        /// </summary>
        public double OffsetX
        {
            get { return (double)GetValue(OffsetXProperty); }
            private set { SetValue(OffsetXPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets a drop shadow's Y-Offset.
        /// This value is the result of a translation of this <see cref="ThemeShadow"/> to
        /// a common drop shadow.
        /// </summary>
        public double OffsetY
        {
            get { return (double)GetValue(OffsetYProperty); }
            private set { SetValue(OffsetYPropertyKey, value); }
        }

    }

}
