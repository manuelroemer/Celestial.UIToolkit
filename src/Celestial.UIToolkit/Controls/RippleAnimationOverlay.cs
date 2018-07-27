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

    [TemplateVisualState(GroupName = CommonStatesVisualStateGroup, Name = NormalVisualStateName)]
    [TemplateVisualState(GroupName = CommonStatesVisualStateGroup, Name = PressedVisualStateName)]
    [TemplatePart(Name = RippleAnimationTimelineTemplatePart, Type = typeof(Timeline))]
    public class RippleAnimationOverlay : ContentControl
    {

        internal const string RippleAnimationTimelineTemplatePart = "PART_RippleAnimationTimeline";
        internal const string CommonStatesVisualStateGroup = "CommonStates";
        internal const string NormalVisualStateName = "Normal";
        internal const string PressedVisualStateName = "Pressed";
        
        private Storyboard _animationTimeline;

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

        public RippleAnimationOverlay()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _animationTimeline = this.GetTemplateChild(RippleAnimationTimelineTemplatePart) as Storyboard;
            if (_animationTimeline != null)
            {
                _animationTimeline.Completed += this.AnimationTimeline_Completed;
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // The animation starts from a specific point (the mouse press location).
            var rippleOrigin = e.GetPosition(this);
            this.AnimationOriginX = rippleOrigin.X;
            this.AnimationOriginY = rippleOrigin.Y;
            this.AnimationPositionX = this.AnimationOriginX - this.AnimationDiameter / 2;
            this.AnimationPositionY = this.AnimationOriginY - this.AnimationDiameter / 2;

            VisualStateManager.GoToState(this, NormalVisualStateName, true);
            VisualStateManager.GoToState(this, PressedVisualStateName, true);

            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            // If we don't have access to the animation object itself,
            // stop the animation when the mouse is no longer down.
            if (_animationTimeline == null)
            {
                VisualStateManager.GoToState(this, NormalVisualStateName, true);
            }
            base.OnPreviewMouseLeftButtonUp(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            // The max. required radius is the diagonal of this element.
            double width = sizeInfo.NewSize.Width;
            double height = sizeInfo.NewSize.Height;
            this.AnimationDiameter = Sqrt(Pow(width, 2) + Pow(Height, 2)) * 2;

            base.OnRenderSizeChanged(sizeInfo);
        }
        
        private void AnimationTimeline_Completed(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(this, NormalVisualStateName, true);
        }

    }

}
