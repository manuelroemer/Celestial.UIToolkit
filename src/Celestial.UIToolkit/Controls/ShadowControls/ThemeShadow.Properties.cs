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
                    ShadowProperty_Changed));

        /// <summary>
        /// Identifies the Elevation attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ElevationProperty =
            DependencyProperty.RegisterAttached(
                "Elevation",
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(
                    0d,
                    ShadowProperty_Changed));
        
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



        private static readonly DependencyPropertyKey BlurRadiusPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "BlurRadius",
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the BlurRadius attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            BlurRadiusPropertyKey.DependencyProperty;



        private static readonly DependencyPropertyKey OffsetXPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "OffsetX",
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the OffsetX attached dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty =
            OffsetXPropertyKey.DependencyProperty;



        private static readonly DependencyPropertyKey OffsetYPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "OffsetY",
                typeof(double),
                typeof(ThemeShadow),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the OffsetY attached dependency property.
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
        /// Gets the value of the <see cref="ElevationProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ElevationProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="ElevationProperty"/> attached dependency property,
        /// which is of type <see cref="double"/>.
        /// </returns>
        public static double GetElevation(DependencyObject obj) =>
            (double)obj.GetValue(ElevationProperty);

        /// <summary>
        /// Sets the value of the <see cref="ElevationProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="ElevationProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetElevation(DependencyObject obj, double value) =>
            obj.SetValue(ElevationProperty, value);



        /// <summary>
        /// Gets the value of the <see cref="BlurRadiusProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BlurRadiusProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BlurRadiusProperty"/> attached dependency property,
        /// which is of type <see cref="double"/>.
        /// </returns>
        public static double GetBlurRadius(DependencyObject obj) =>
            (double)obj.GetValue(BlurRadiusProperty);

        private static void SetBlurRadius(DependencyObject obj, double value) =>
            obj.SetValue(BlurRadiusPropertyKey, value);



        /// <summary>
        /// Gets the value of the <see cref="OffsetXProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="OffsetXProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="OffsetXProperty"/> attached dependency property,
        /// which is of type <see cref="double"/>.
        /// </returns>
        public static double GetOffsetX(DependencyObject obj) =>
            (double)obj.GetValue(OffsetXProperty);

        private static void SetOffsetX(DependencyObject obj, double value) =>
            obj.SetValue(OffsetXPropertyKey, value);



        /// <summary>
        /// Gets the value of the <see cref="OffsetYProperty"/> attached dependency property
        /// for a given <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="OffsetYProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="OffsetYProperty"/> attached dependency property,
        /// which is of type <see cref="double"/>.
        /// </returns>
        public static double GetOffsetY(DependencyObject obj) =>
            (double)obj.GetValue(OffsetYProperty);

        private static void SetOffsetY(DependencyObject obj, double value) =>
            obj.SetValue(OffsetYPropertyKey, value);

    }

}
