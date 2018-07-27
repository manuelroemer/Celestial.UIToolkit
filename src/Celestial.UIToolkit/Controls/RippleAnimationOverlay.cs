using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static System.Math;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A content control which provides a ripple animation effect to the underlying content,
    /// whenever the user clicks on it.
    /// The effect is based on the ripple effect by the Material Design language.
    /// </summary>
    [TemplateVisualState(GroupName = CommonStatesVisualStateGroup, Name = NormalVisualStateName)]
    [TemplateVisualState(GroupName = CommonStatesVisualStateGroup, Name = PressedVisualStateName)]
    public class RippleAnimationOverlay : ContentControl
    {
        
        internal const string CommonStatesVisualStateGroup = "CommonStates";
        internal const string NormalVisualStateName = "Normal";
        internal const string PressedVisualStateName = "Pressed";

        /// <summary>
        /// Identifies the <see cref="AnimationOriginX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationOriginXProperty = DependencyProperty.Register(
            nameof(AnimationOriginX), typeof(double), typeof(RippleAnimationOverlay), new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="AnimationOriginY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationOriginYProperty = DependencyProperty.Register(
            nameof(AnimationOriginY), typeof(double), typeof(RippleAnimationOverlay), new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="AnimationPositionX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationPositionXProperty = DependencyProperty.Register(
            nameof(AnimationPositionX), typeof(double), typeof(RippleAnimationOverlay), new PropertyMetadata(0d));
        
        /// <summary>
        /// Identifies the <see cref="AnimationPositionY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationPositionYProperty = DependencyProperty.Register(
            nameof(AnimationPositionY), typeof(double), typeof(RippleAnimationOverlay), new PropertyMetadata(0d));
        
        /// <summary>
        /// Identifies the <see cref="AnimationDiameter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationDiameterProperty = DependencyProperty.Register(
            nameof(AnimationDiameter), typeof(double), typeof(RippleAnimationOverlay), new PropertyMetadata(0d));

        /// <summary>
        /// Gets the x-coordinate of the animation's origin point.
        /// </summary>
        public double AnimationOriginX
        {
            get { return (double)GetValue(AnimationOriginXProperty); }
            protected set { SetValue(AnimationOriginXProperty, value); }
        }

        /// <summary>
        /// Gets the y-coordinate of the animation's origin point.
        /// </summary>
        public double AnimationOriginY
        {
            get { return (double)GetValue(AnimationOriginYProperty); }
            protected set { SetValue(AnimationOriginYProperty, value); }
        }

        /// <summary>
        /// Gets the y-coordinate of the top-left location which the animation
        /// will reach.
        /// This is equal to <c><see cref="AnimationOriginX"/> - <see cref="AnimationDiameter"/> / 2</c>
        /// and can be used to align an element which displays the animation.
        /// </summary>
        public double AnimationPositionX
        {
            get { return (double)GetValue(AnimationPositionXProperty); }
            protected set { SetValue(AnimationPositionXProperty, value); }
        }

        /// <summary>
        /// Gets the y-coordinate of the top-left location which the animation
        /// will reach.
        /// This is equal to <c><see cref="AnimationOriginY"/> - <see cref="AnimationDiameter"/> / 2</c>
        /// and can be used to align an element which displays the animation.
        /// </summary>
        public double AnimationPositionY
        {
            get { return (double)GetValue(AnimationPositionYProperty); }
            protected set { SetValue(AnimationPositionYProperty, value); }
        }

        /// <summary>
        /// Gets the diameter of the animated circle.
        /// </summary>
        public double AnimationDiameter
        {
            get { return (double)GetValue(AnimationDiameterProperty); }
            protected set { SetValue(AnimationDiameterProperty, value); }
        }
        
        static RippleAnimationOverlay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(RippleAnimationOverlay),
                new FrameworkPropertyMetadata(typeof(RippleAnimationOverlay)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RippleAnimationOverlay"/> class.
        /// </summary>
        public RippleAnimationOverlay()
        {
        }

        /// <summary>
        /// Loads template parts for the animation control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, NormalVisualStateName, true);
        }

        /// <summary>
        /// Called whenever the element's render size changes.
        /// This updates the <see cref="AnimationDiameter"/> property.
        /// </summary>
        /// <param name="sizeInfo">Information about the new render size.</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            // The max. required radius is the diagonal of this element.
            double width = sizeInfo.NewSize.Width;
            double height = sizeInfo.NewSize.Height;
            this.AnimationDiameter = Sqrt(Pow(width, 2) + Pow(Height, 2)) * 2;

            base.OnRenderSizeChanged(sizeInfo);
        }

        /// <summary>
        /// Called when the user clicks on this element (with the left mouse button).
        /// This sets the animation origin properties and starts the animation effect.
        /// </summary>
        /// <param name="e">Event args about the click.</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // The animation starts from a specific point (the mouse press location).
            var rippleOrigin = e.GetPosition(this);
            this.AnimationOriginX = rippleOrigin.X;
            this.AnimationOriginY = rippleOrigin.Y;
            this.AnimationPositionX = this.AnimationOriginX - this.AnimationDiameter / 2;
            this.AnimationPositionY = this.AnimationOriginY - this.AnimationDiameter / 2;

            VisualStateManager.GoToState(this, PressedVisualStateName, true);
            base.OnPreviewMouseLeftButtonDown(e);
        }
        
        /// <summary>
        /// Called when the user lifts the left mouse button again.
        /// This stops the animation, if it has finished.
        /// </summary>
        /// <param name="e">Event args about the mouse data.</param>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, NormalVisualStateName, true);
            base.OnPreviewMouseLeftButtonUp(e);
        }
        
    }

}
