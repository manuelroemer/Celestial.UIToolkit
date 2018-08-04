using Celestial.UIToolkit.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Math;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// A content control which provides a ripple animation effect to the underlying content,
    /// whenever the user clicks on it.
    /// The effect is based on the ripple effect by the Material Design language.
    /// </summary>
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = NormalVisualStateName)]
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = ExpandingVisualStateName)]
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = FadingVisualStateName)]
    public class RippleOverlay : ContentControl
    {
        
        internal const string AnimationStatesVisualStateGroup = "AnimationStates";
        internal const string NormalVisualStateName = "Normal";
        internal const string ExpandingVisualStateName = "Expanding";
        internal const string FadingVisualStateName = "Fading";
        
        private static readonly DependencyPropertyKey AnimationOriginXPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationOriginX), typeof(double), typeof(RippleOverlay), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationOriginYPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationOriginY), typeof(double), typeof(RippleOverlay), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationPositionXPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationPositionX), typeof(double), typeof(RippleOverlay), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationPositionYPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationPositionY), typeof(double), typeof(RippleOverlay), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationDiameterPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationDiameter), typeof(double), typeof(RippleOverlay), new PropertyMetadata(0d));
        
        /// <summary>
        /// Identifies the <see cref="AnimationOriginX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationOriginXProperty = AnimationOriginXPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="AnimationOriginY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationOriginYProperty = AnimationOriginYPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="AnimationPositionX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationPositionXProperty = AnimationPositionXPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="AnimationPositionY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationPositionYProperty = AnimationPositionYPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="AnimationDiameter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationDiameterProperty = AnimationDiameterPropertyKey.DependencyProperty;
        
        /// <summary>
        /// Identifies the <see cref="AnimationScale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationScaleProperty = DependencyProperty.Register(
            nameof(AnimationScale), typeof(double), typeof(RippleOverlay), new PropertyMetadata(1d));

        /// <summary>
        /// Identifies the <see cref="ActualAnimationScale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActualAnimationScaleProperty = DependencyProperty.Register(
            nameof(ActualAnimationScale), typeof(double), typeof(RippleOverlay), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, AnimationScale_Changed));

        /// <summary>
        /// Identifies the <see cref="RippleOrigin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RippleOriginProperty = DependencyProperty.Register(
            nameof(RippleOrigin), typeof(RippleOrigin), typeof(RippleOverlay), new PropertyMetadata(RippleOrigin.MouseLocation));

        /// <summary>
        /// Identifies the <see cref="AllowFading"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsFadingProperty = DependencyProperty.Register(
            nameof(AllowFading), typeof(bool), typeof(RippleOverlay), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsActive"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive), typeof(bool), typeof(RippleOverlay), new PropertyMetadata(false, IsActive_Changed));

        /// <summary>
        /// Gets the x-coordinate of the animation's origin point.
        /// </summary>
        public double AnimationOriginX
        {
            get { return (double)GetValue(AnimationOriginXProperty); }
            protected set { SetValue(AnimationOriginXPropertyKey, value); }
        }

        /// <summary>
        /// Gets the y-coordinate of the animation's origin point.
        /// </summary>
        public double AnimationOriginY
        {
            get { return (double)GetValue(AnimationOriginYProperty); }
            protected set { SetValue(AnimationOriginYPropertyKey, value); }
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
            protected set { SetValue(AnimationPositionXPropertyKey, value); }
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
            protected set { SetValue(AnimationPositionYPropertyKey, value); }
        }

        /// <summary>
        /// Gets the diameter of the animated circle.
        /// </summary>
        public double AnimationDiameter
        {
            get { return (double)GetValue(AnimationDiameterProperty); }
            protected set { SetValue(AnimationDiameterPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the target scale of the ripple animation
        /// (the scale which it will have, when it is fully expanded).
        /// </summary>
        public double AnimationScale
        {
            get { return (double)GetValue(AnimationScaleProperty); }
            set { SetValue(AnimationScaleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current scale of the animation.
        /// This value is only supposed to be used by animations.
        /// </summary>
        public double ActualAnimationScale
        {
            get { return (double)GetValue(ActualAnimationScaleProperty); }
            set { SetValue(ActualAnimationScaleProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the <see cref="Celestial.UIToolkit.Controls.RippleOrigin"/> which
        /// is being used by this <see cref="RippleOverlay"/>.
        /// </summary>
        public RippleOrigin RippleOrigin
        {
            get { return (RippleOrigin)GetValue(RippleOriginProperty); }
            set { SetValue(RippleOriginProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the animation will enter the
        /// Fading visual state when it finishes.
        /// If this is <c>false</c>, the animated component will stay in place,
        /// when the animation is done.
        /// </summary>
        public bool AllowFading
        {
            get { return (bool)GetValue(IsFadingProperty); }
            set { SetValue(IsFadingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ripple animation is active.
        /// By setting this value to <c>true</c>, the animation is started.
        /// As long as this property is <c>true</c>, the animation will stay in its
        /// 'Expanding' state.
        /// If it gets set to <c>false</c>, the animation will be allowed to enter the 'Fading'
        /// state.
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        
        /// <summary>
        /// Overrides the element's default style.
        /// </summary>
        static RippleOverlay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(RippleOverlay),
                new FrameworkPropertyMetadata(typeof(RippleOverlay)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RippleOverlay"/> class.
        /// </summary>
        public RippleOverlay()
        {
        }

        /// <summary>
        /// Forces the (re-)start of the ripple animation,
        /// originating either from the control's center, or the current location of the
        /// user's mouse pointer. The decision will depend on the <see cref="RippleOrigin"/>
        /// property and some further conditions.
        /// </summary>
        public void StartAnimation()
        {
            if (this.RippleOrigin == RippleOrigin.MouseLocation)
            {
                // Even if RippleOrigin is set to use the cursor, we can only do so,
                // if the mouse is actually over this element.
                // If not, still show the animation from the center.
                // (This can, for instance, happen if the animation gets triggered via the keyboard).
                var clickCoordinates = Mouse.GetPosition(this);
                if (this.IsPointInControlBounds(clickCoordinates))
                {
                    this.StartAnimationFromPoint(clickCoordinates);
                }
                else
                {
                    this.StartAnimationFromCenter();
                }
            }
            else if (this.RippleOrigin == RippleOrigin.Center)
            {
                this.StartAnimationFromCenter();
            }
            else
            {
                throw new InvalidOperationException($"Unknown {nameof(Controls.RippleOrigin)} enumeration value.");
            }
        }

        /// <summary>
        /// Forces the (re-)start of the ripple animation,
        /// originating from the specified <paramref name="rippleOrigin"/> point.
        /// Calling this method will ignore the value of the <see cref="RippleOrigin"/>
        /// property.
        /// </summary>
        /// <param name="rippleOrigin">The point from which the ripple animation originates.</param>
        public void StartAnimationFromPoint(Point rippleOrigin)
        {
            this.UpdateAnimationProperties(rippleOrigin);
            this.EnterActiveVisualState();
        }

        /// <summary>
        /// Forces the (re-)start of the ripple animation,
        /// originating from the control's center.
        /// Calling this method will ignore the value of the
        /// <see cref="RippleOrigin"/> property.
        /// </summary>
        public void StartAnimationFromCenter()
        {
            this.StartAnimationFromPoint(this.GetCenterPoint());
        }

        /// <summary>
        /// Loads template parts for the animation control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, NormalVisualStateName, true);
        }

        private static void AnimationScale_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RippleOverlay)d).TryEnterFadingVisualState();
        }
        
        private static void IsActive_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (RippleOverlay)d;
            bool isActive = (bool)e.NewValue;

            if (isActive)
            {
                self.StartAnimation();
            }
            else
            {
                // If IsActive gets set to false,
                // allow the disappearing of the animation.
                self.TryEnterFadingVisualState();
            }
        }

        private void EnterActiveVisualState()
        {
            VisualStateManager.GoToState(this, NormalVisualStateName, false);
            VisualStateManager.GoToState(this, ExpandingVisualStateName, true);
        }
        
        private void TryEnterFadingVisualState()
        {
            if (this.AllowFading && 
                this.ActualAnimationScale >= this.AnimationScale 
                && !this.IsActive)
            {
                VisualStateManager.GoToState(this, FadingVisualStateName, true);
            }
        }

        private void UpdateAnimationProperties(Point rippleOrigin)
        {
            this.AnimationOriginX = rippleOrigin.X;
            this.AnimationOriginY = rippleOrigin.Y;

            this.AnimationDiameter = Sqrt(  // This will make the ripple touch the outer-most edge
                Pow(Max(rippleOrigin.X, this.ActualWidth - rippleOrigin.X), 2) +
                Pow(Max(rippleOrigin.Y, this.ActualHeight - rippleOrigin.Y), 2)) * 2;
            this.AnimationPositionX = this.AnimationOriginX - this.AnimationDiameter / 2;
            this.AnimationPositionY = this.AnimationOriginY - this.AnimationDiameter / 2;
        }

    }

    /// <summary>
    /// Defines the available origin point types for a ripple animation.
    /// </summary>
    public enum RippleOrigin
    {

        /// <summary>
        /// The location of the mouse pointer, when the animation is started.
        /// </summary>
        MouseLocation,

        /// <summary>
        /// The animation starts from the center of the <see cref="RippleOverlay"/>.
        /// </summary>
        Center

    }

}
