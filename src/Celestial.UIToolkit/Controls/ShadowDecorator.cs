using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Defines the different types of shadows that are available.
    /// </summary>
    public enum ShadowType
    {

        /// <summary>
        /// An ambient shadow is caused by ambient light.
        /// Shadows caused by this type of light appear at
        /// all angles of the control.
        /// </summary>
        Ambient,

        /// <summary>
        /// A directional shadow is caused by a light that
        /// comes from a specific direction.
        /// </summary>
        Directional

    }

    /// <summary>
    /// A decorator which applies a drop shadow effect to the underlying
    /// child element.
    /// In essence, this is a wrapper for the <see cref="DropShadowEffect"/>
    /// which simplifies the interacting with its properties.
    /// In addition though, this decorator is based on the elevation system
    /// used in the Material Design Language.
    /// </summary>
    public class ShadowDecorator : Decorator
    {

        private Lazy<DropShadowEffect> _dropShadowLazy = new Lazy<DropShadowEffect>(() => new DropShadowEffect());

        /// <summary>
        /// Identifies the <see cref="ShadowColor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(
            nameof(ShadowColor), 
            typeof(Color), 
            typeof(ShadowDecorator), 
            new FrameworkPropertyMetadata(
                Colors.Black,
                FrameworkPropertyMetadataOptions.AffectsRender, 
                ShadowProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="ShadowOpacity"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(
            nameof(ShadowOpacity), 
            typeof(double), 
            typeof(ShadowDecorator), 
            new FrameworkPropertyMetadata(
                1d, 
                FrameworkPropertyMetadataOptions.AffectsRender, 
                ShadowProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="ShadowType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowTypeProperty = DependencyProperty.Register(
            nameof(ShadowType), 
            typeof(ShadowType), 
            typeof(ShadowDecorator), 
            new FrameworkPropertyMetadata(
                ShadowType.Ambient, 
                FrameworkPropertyMetadataOptions.AffectsRender, 
                ShadowProperty_Changed));

        /// <summary>
        /// Identifies the <see cref="Elevation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ElevationProperty = DependencyProperty.Register(
            nameof(Elevation), 
            typeof(double), 
            typeof(ShadowDecorator), 
            new FrameworkPropertyMetadata(
                1d, 
                FrameworkPropertyMetadataOptions.AffectsRender, 
                ShadowProperty_Changed),
            (value) => (double)value >= 0);

        /// <summary>
        /// Identifies the <see cref="ShadowDirection"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowDirectionProperty = DependencyProperty.Register(
            nameof(ShadowDirection), 
            typeof(Dock), 
            typeof(ShadowDecorator), 
            new FrameworkPropertyMetadata(
                Dock.Bottom, 
                FrameworkPropertyMetadataOptions.AffectsRender, 
                ShadowProperty_Changed));

        /// <summary>
        /// Gets or sets the shadow's colors.
        /// </summary>
        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the shadow's opacity as a value between 0 and 1.
        /// </summary>
        public double ShadowOpacity
        {
            get { return (double)GetValue(ShadowOpacityProperty); }
            set { SetValue(ShadowOpacityProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the type of shadow which is currently displayed by this decorator.
        /// </summary>
        public ShadowType ShadowType
        {
            get { return (ShadowType)GetValue(ShadowTypeProperty); }
            set { SetValue(ShadowTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the elevation of the element which is being decorated with
        /// this <see cref="ShadowDecorator"/>.
        /// The elevation defines the strength of the shadow.
        /// The higher the elevation of the element, the stronger the shadow.
        /// </summary>
        public double Elevation
        {
            get { return (double)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the direction of the shadow, if <see cref="ShadowType"/>
        /// is set to <see cref="ShadowType.Directional"/>.
        /// </summary>
        public Dock ShadowDirection
        {
            get { return (Dock)GetValue(ShadowDirectionProperty); }
            set { SetValue(ShadowDirectionProperty, value); }
        }
        
        private static void ShadowProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs baseValue)
        {
            var self = (ShadowDecorator)d;
            self.UpdateDecoratorEffect();
        }
        
        private void UpdateDecoratorEffect()
        {
            if (Elevation == 0)
            {
                Effect = null;
            }
            else
            {
                UpdateDropShadow();
                Effect = _dropShadowLazy.Value;
            }
        }
        
        private void UpdateDropShadow()
        {
            var valueCalculator = CreateShadowEffectValueCalculator();
            _dropShadowLazy.Value.ShadowDepth = valueCalculator.CalculateShadowDepth();
            _dropShadowLazy.Value.Direction = valueCalculator.CalculateDirection();
            _dropShadowLazy.Value.BlurRadius = valueCalculator.CalculateBlurRadius();
            _dropShadowLazy.Value.Color = ShadowColor;
            _dropShadowLazy.Value.Opacity = ShadowOpacity;
        }

        private DropShadowValueCalculator CreateShadowEffectValueCalculator()
        {
            if (ShadowType == ShadowType.Ambient)
                return new AmbientDropShadowValueCalculator(Elevation, ShadowDirection);
            else if (ShadowType == ShadowType.Directional)
                return new DirectionalDropShadowValueCalculator(Elevation, ShadowDirection);
            else
                throw new NotImplementedException("Unknown ShadowType.");
        }
        
        private abstract class DropShadowValueCalculator
        {
            protected double Elevation { get; }
            protected Dock ShadowDirection { get; }

            public DropShadowValueCalculator(double elevation, Dock shadowDirection)
            {
                Elevation = elevation;
                ShadowDirection = shadowDirection;
            }

            public abstract double CalculateShadowDepth();

            public abstract double CalculateDirection();

            public virtual double CalculateBlurRadius()
            {
                // This value is chosen through observation and testing.
                // It looks the most real, when comparing different elevation levels.
                const int elevationLevelMultiplier = 3;
                return Elevation * elevationLevelMultiplier;
            }
        }

        private sealed class AmbientDropShadowValueCalculator : DropShadowValueCalculator
        {
            public AmbientDropShadowValueCalculator(double elevation, Dock shadowDirection)
                : base(elevation, shadowDirection) { }

            // Ambient shadows point in all directions.
            // For a DropShadowEffect, that is the case if
            // ShadowDepth and Direction are both 0.
            public override double CalculateShadowDepth()
            {
                return 0d;
            }

            public override double CalculateDirection()
            {
                return 0d;
            }
            
        }
        
        private sealed class DirectionalDropShadowValueCalculator : DropShadowValueCalculator
        {
            public DirectionalDropShadowValueCalculator(double elevation, Dock shadowDirection)
                : base(elevation, shadowDirection) { }

            public override double CalculateShadowDepth()
            {
                // ShadowDepth <-> Elevation can be treated on a 1-1 basis.
                return Elevation;
            }

            public override double CalculateDirection()
            {
                // The direction angle is mapped counter-clockwise,
                // starting from the right.
                switch (ShadowDirection)
                {
                    case Dock.Right:
                        return 0;
                    case Dock.Top:
                        return 90;
                    case Dock.Left:
                        return 180;
                    case Dock.Bottom:
                        return 270;
                    default:
                        throw new NotImplementedException(
                            "Unknown Dock enumeration value. " +
                            "This really shouldn't ever happen...");
                }
            }
            
        }

    }

}
