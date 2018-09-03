using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using static System.Math;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A control which adds a drop shadow effect to its content.
    /// This control uses the standard WPF 
    /// <see cref="System.Windows.Media.Effects.DropShadowEffect"/>,
    /// but simplifies its usage via more intuitive properties.
    /// </summary>
    public partial class DropShadowDecorator : Decorator
    {

        private DropShadowEffect _currentEffect;

        /// <summary>
        /// Gets the <see cref="OffsetX"/> and <see cref="OffsetY"/> coordinates
        /// as a <see cref="Vector"/>.
        /// </summary>
        public Vector Offset => new Vector(OffsetX, OffsetY);

        static DropShadowDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DropShadowDecorator), 
                new FrameworkPropertyMetadata(typeof(DropShadowDecorator)));
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DropShadowDecorator"/>.
        /// </summary>
        public DropShadowDecorator()
        {
            Loaded += (sender, e) =>
            {
                UpdateCurrentDropShadowEffect();
            };
        }

        private static void Shadow_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DropShadowDecorator decorator)
            {
                decorator.UpdateCurrentDropShadowEffect();
            }
        }

        /// <summary>
        /// Forces an update of the <see cref="DropShadowEffect"/> property by calling
        /// <see cref="CalculateDropShadowEffect"/> and switching the property, if
        /// necessary.
        /// </summary>
        protected void UpdateCurrentDropShadowEffect()
        {
            DropShadowEffect newEffect = CalculateDropShadowEffect();
            if (Effect != newEffect)
            {
                Effect = newEffect;
            }
        }

        /// <summary>
        /// Calculates the current values for the <see cref="DropShadowEffect"/> property.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Windows.Media.Effects.DropShadowEffect"/> which is supposed to
        /// be applied to the panel's content.
        /// </returns>
        /// <remarks>
        /// When overriding this method, try to create a cached shadow effect object whose values
        /// are changed when this method is called again.
        /// </remarks>
        protected virtual DropShadowEffect CalculateDropShadowEffect()
        {
            if (!IsShadowEnabled || BlurRadius == 0d)
            {
                // No need to take up resources when we don't need them.
                _currentEffect = null;
                return null;
            }
            
            if (_currentEffect == null)
            {
                _currentEffect = new DropShadowEffect();
            }

            SetRenderingBias();
            SetColor();
            SetOpacity();
            SetBlurRadius();
            SetDirection();
            SetShadowDepth();
            
            return _currentEffect;
        }

        private void SetRenderingBias()
        {
            _currentEffect.RenderingBias = RenderingBias;
        }

        private void SetColor()
        {
            _currentEffect.Color = ShadowColor;
        }

        private void SetOpacity()
        {
            _currentEffect.Opacity = ShadowOpacity;
        }

        private void SetBlurRadius()
        {
            _currentEffect.BlurRadius = BlurRadius;
        }

        private void SetDirection()
        {
            double radians = Atan2(-OffsetY, OffsetX);
            double degrees = radians * (180 / PI);
            _currentEffect.Direction = 360 - degrees;
        }

        private void SetShadowDepth()
        {
            _currentEffect.ShadowDepth = Offset.Length;
        }

        /// <summary>
        /// Returns a string representation of the panel and its shadow properties.
        /// </summary>
        /// <returns>A string representing the panel.</returns>
        public override string ToString()
        {
            return $"{nameof(DropShadowDecorator)}: " +
                   $"{nameof(Offset)}: {Offset}, " +
                   $"{nameof(BlurRadius)}: {BlurRadius}";
        }

    }

}
