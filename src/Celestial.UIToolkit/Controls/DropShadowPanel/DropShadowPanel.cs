using Celestial.UIToolkit.Theming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Effects;
using static System.Math;

namespace Celestial.UIToolkit.Controls
{

    public partial class DropShadowPanel : ContentControl
    {

        private DropShadowEffect _currentEffect;

        /// <summary>
        /// Gets the <see cref="OffsetX"/> and <see cref="OffsetY"/> coordinates
        /// as a <see cref="Vector"/>.
        /// </summary>
        public Vector Offset => new Vector(OffsetX, OffsetY);

        static DropShadowPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DropShadowPanel), new FrameworkPropertyMetadata(typeof(DropShadowPanel)));
        }

        public override void OnApplyTemplate()
        {
            UpdateCurrentDropShadowEffect();
            base.OnApplyTemplate();
        }

        private static void Shadow_Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DropShadowPanel panel)
            {
                panel.UpdateCurrentDropShadowEffect();
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
            if (_currentEffect == null)
            {
                _currentEffect = new DropShadowEffect();
            }

            SetColor();
            SetOpacity();
            SetBlurRadius();
            SetDirection();
            SetShadowDepth();
            
            return _currentEffect;
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
            double radians = Atan2(OffsetY, OffsetX);
            double degrees = radians * (180 / PI);
            _currentEffect.Direction = 360 - degrees;
            Debug.WriteLine(degrees);
        }

        private void SetShadowDepth()
        {
            _currentEffect.ShadowDepth = Offset.Length;
        }

    }

}
