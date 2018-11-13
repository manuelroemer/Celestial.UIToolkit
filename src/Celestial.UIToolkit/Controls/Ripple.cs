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
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = NormalVisualState)]
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = ExpandingVisualState)]
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = ExpandedVisualState)]
    [TemplateVisualState(GroupName = AnimationStatesVisualStateGroup, Name = FadingVisualState)]
    public class Ripple : Control
    {
        
        internal const string AnimationStatesVisualStateGroup = "AnimationStates";
        internal const string NormalVisualState = "Normal";
        internal const string ExpandingVisualState = "Expanding";
        internal const string ExpandedVisualState = "Expanded";
        internal const string FadingVisualState = "Fading";
        
        private static readonly DependencyPropertyKey AnimationOriginXPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationOriginX), typeof(double), typeof(Ripple), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationOriginYPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationOriginY), typeof(double), typeof(Ripple), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationPositionXPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationPositionX), typeof(double), typeof(Ripple), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationPositionYPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationPositionY), typeof(double), typeof(Ripple), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey AnimationDiameterPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AnimationDiameter), typeof(double), typeof(Ripple), new PropertyMetadata(0d));

        private static readonly DependencyPropertyKey IsExpandingPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsExpanding), typeof(bool), typeof(Ripple), new PropertyMetadata(false));

        private static readonly DependencyPropertyKey IsExpandedPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsExpanded), typeof(bool), typeof(Ripple), new PropertyMetadata(false));

        private static readonly DependencyPropertyKey IsFadingPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsFading), typeof(bool), typeof(Ripple), new PropertyMetadata(false));

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
        /// Identifies the <see cref="IsExpanding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsExpandingProperty = IsExpandingPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="IsExpanded"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty = IsExpandedPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="IsFading"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsFadingProperty = IsFadingPropertyKey.DependencyProperty;

        /// <summary>
        /// Identifies the <see cref="AnimationScale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationScaleProperty = DependencyProperty.Register(
            nameof(AnimationScale), typeof(double), typeof(Ripple), new PropertyMetadata(1d));
        
        /// <summary>
        /// Identifies the <see cref="RippleOrigin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RippleOriginProperty = DependencyProperty.Register(
            nameof(RippleOrigin), typeof(RippleOrigin), typeof(Ripple), new PropertyMetadata(RippleOrigin.MouseLocation));

        /// <summary>
        /// Identifies the <see cref="AllowFading"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AllowFadingProperty = DependencyProperty.Register(
            nameof(AllowFading), typeof(bool), typeof(Ripple), new PropertyMetadata(true, AllowFading_Changed));

        /// <summary>
        /// Identifies the <see cref="IsActiveTrigger"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveTriggerProperty = DependencyProperty.Register(
            nameof(IsActiveTrigger), typeof(bool), typeof(Ripple), new PropertyMetadata(false, IsActiveTrigger_Changed));

        /// <summary>
        /// Identifies the <see cref="IsAnimationExpanding"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAnimationExpandingProperty = DependencyProperty.Register(
            nameof(IsAnimationExpanding), typeof(bool), typeof(Ripple), new PropertyMetadata(false, IsAnimationExpanding_Changed));

        /// <summary>
        /// Identifies the <see cref="IsAnimationFading"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAnimationFadingProperty = DependencyProperty.Register(
            nameof(IsAnimationFading), typeof(bool), typeof(Ripple), new PropertyMetadata(false, IsAnimationFading_Changed));

        /// <summary>
        /// Gets the x-coordinate of the animation's origin point.
        /// </summary>
        public double AnimationOriginX
        {
            get { return (double)GetValue(AnimationOriginXProperty); }
            private set { SetValue(AnimationOriginXPropertyKey, value); }
        }

        /// <summary>
        /// Gets the y-coordinate of the animation's origin point.
        /// </summary>
        public double AnimationOriginY
        {
            get { return (double)GetValue(AnimationOriginYProperty); }
            private set { SetValue(AnimationOriginYPropertyKey, value); }
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
            private set { SetValue(AnimationPositionXPropertyKey, value); }
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
            private set { SetValue(AnimationPositionYPropertyKey, value); }
        }

        /// <summary>
        /// Gets the diameter of the animated component.
        /// </summary>
        public double AnimationDiameter
        {
            get { return (double)GetValue(AnimationDiameterProperty); }
            private set { SetValue(AnimationDiameterPropertyKey, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the animation is in the 'Expanding' state right now.
        /// </summary>
        public bool IsExpanding
        {
            get { return (bool)GetValue(IsExpandingProperty); }
            private set { SetValue(IsExpandingPropertyKey, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the animation is in the 'Expanded' state right now.
        /// This is the transition period between the 'Expanding' and 'Fading' state, when the animated
        /// component has its full size and does not grow larger anymore.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            private set { SetValue(IsExpandedPropertyKey, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the animation is in the 'Fading' state right now.
        /// </summary>
        public bool IsFading
        {
            get { return (bool)GetValue(IsFadingProperty); }
            private set { SetValue(IsFadingPropertyKey, value); }
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
        /// Gets or sets the <see cref="Celestial.UIToolkit.Controls.RippleOrigin"/> which
        /// is being used by this <see cref="Ripple"/>.
        /// </summary>
        public RippleOrigin RippleOrigin
        {
            get { return (RippleOrigin)GetValue(RippleOriginProperty); }
            set { SetValue(RippleOriginProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the animation will enter the
        /// 'Fading' state when it finishes.
        /// If this is <c>false</c>, the animated component will stay in place,
        /// when the animation is done.
        /// </summary>
        public bool AllowFading
        {
            get { return (bool)GetValue(AllowFadingProperty); }
            set { SetValue(AllowFadingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ripple animation is active.
        /// By setting this value to <c>true</c>, the animation is started.
        /// As long as this property is <c>true</c>, the animation will stay in its
        /// 'Expanded' state.
        /// If it gets set to <c>false</c>, the animation will be allowed to enter the 'Fading'
        /// state.
        /// </summary>
        public bool IsActiveTrigger
        {
            get { return (bool)GetValue(IsActiveTriggerProperty); }
            set { SetValue(IsActiveTriggerProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the animation which controls the expansion of the ripple
        /// is running at the moment.
        /// This property is introduced so that a ControlTemplate can notify this class, when
        /// an animation finishes. Afterwards, this class can switch the control's visual states.
        /// DO NOT use this property directly, except when defining a custom template in XAML.
        /// Instead, use the <see cref="IsExpanding"/> property, as it represents the current visual state of the control.
        /// </summary>
        public bool IsAnimationExpanding
        {
            get { return (bool)GetValue(IsAnimationExpandingProperty); }
            set { SetValue(IsAnimationExpandingProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the animation which controls the fading of the ripple
        /// is running at the moment.
        /// This property is introduced so that a ControlTemplate can notify this class, when
        /// an animation finishes. Afterwards, this class can switch the control's visual states.
        /// DO NOT use this property directly, except when defining a custom template in XAML.
        /// Instead, use the <see cref="IsFading"/> property, as it represents the current visual state of the control.
        /// </summary>
        public bool IsAnimationFading
        {
            get { return (bool)GetValue(IsAnimationFadingProperty); }
            set { SetValue(IsAnimationFadingProperty, value); }
        }

        /// <summary>
        /// Overrides the element's default style.
        /// </summary>
        static Ripple()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Ripple),
                new FrameworkPropertyMetadata(typeof(Ripple)));
            IsTabStopProperty.OverrideMetadata(
                typeof(Ripple),
                new FrameworkPropertyMetadata(false));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ripple"/> class.
        /// </summary>
        public Ripple()
        {
            SizeChanged += Ripple_SizeChanged;
        }

        /// <summary>
        /// Loads template parts for the animation control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EnterNormalVisualState();
        }

        /// <summary>
        /// Forces the (re-)start of the ripple animation,
        /// originating either from the control's center, or the current location of the
        /// user's mouse pointer. The decision will depend on the <see cref="RippleOrigin"/>
        /// property and some further conditions.
        /// </summary>
        public void StartAnimation()
        {
            if (RippleOrigin == RippleOrigin.MouseLocation)
            {
                // Even if RippleOrigin is set to use the cursor, we can only do so,
                // if the mouse is actually over this element.
                // If not, still show the animation from the center.
                // (This can, for instance, happen if the animation gets triggered via the keyboard).
                var clickCoordinates = Mouse.GetPosition(this);
                if (this.IsPointInControlBounds(clickCoordinates))
                {
                    StartAnimationFromPoint(clickCoordinates);
                }
                else
                {
                    StartAnimationFromCenter();
                }
            }
            else if (RippleOrigin == RippleOrigin.Center)
            {
                StartAnimationFromCenter();
            }
            else
            {
                throw new InvalidOperationException($"Unknown {nameof(Controls.RippleOrigin)} enumeration value.");
            }
        }

        /// <summary>
        /// Forces the (re-)start of the ripple animation,
        /// originating from the control's center.
        /// Calling this method will ignore the value of the
        /// <see cref="RippleOrigin"/> property.
        /// </summary>
        public void StartAnimationFromCenter()
        {
            StartAnimationFromPoint(this.GetCenter());
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
            if (IsEnabled)
            {
                UpdateAnimationProperties(rippleOrigin);
                EnterExpandingVisualState();
            }
        }

        private void UpdateAnimationProperties(Point rippleOrigin)
        {
            AnimationOriginX = rippleOrigin.X;
            AnimationOriginY = rippleOrigin.Y;

            AnimationDiameter = Sqrt(  // This will make the ripple touch the outer-most edge
                Pow(Max(rippleOrigin.X, ActualWidth - rippleOrigin.X), 2) +
                Pow(Max(rippleOrigin.Y, ActualHeight - rippleOrigin.Y), 2)) * 2;
            AnimationPositionX = AnimationOriginX - AnimationDiameter / 2;
            AnimationPositionY = AnimationOriginY - AnimationDiameter / 2;
        }

        private void Ripple_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // When the control is resized, while the ripple is visible, we need to re-calculate
            // its animation diameter and bounds.
            // The origin point is simply kept.
            if (IsAnimationFading || IsAnimationExpanding || IsExpanded)
            {
                UpdateAnimationProperties(new Point(
                    AnimationOriginX,
                    AnimationOriginY));
            }
        }

        private static void IsActiveTrigger_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (Ripple)d;
            bool isActiveTriggered = (bool)e.NewValue;

            if (isActiveTriggered)
            {
                self.StartAnimation();
            }
            else
            {
                self.TryEnterFadingVisualState();
            }
        }

        private static void IsAnimationExpanding_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (Ripple)d;
            bool isExpanding = (bool)e.NewValue;

            if (!isExpanding)
            {
                // Got a signal that the animation stopped expanding.
                // -> It is fully expanded now and might start fading, if
                //    the IsActiveTrigger allows it.
                self.EnterExpandedVisualState();
                self.TryEnterFadingVisualState();
            }
        }

        private static void IsAnimationFading_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (Ripple)d;
            bool isFading = (bool)e.NewValue;

            if (!isFading && !self.IsAnimationExpanding)
            {
                // Got a signal that the animation stopped fading.
                // -> The control is in its normal state again.
                self.EnterNormalVisualState();
            }
        }

        private static void AllowFading_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (Ripple)d;
            bool allowFading = (bool)e.NewValue;

            // If the anim is currently fading away and fading gets forbidden during this animation,
            // revert back to the Expanded state and act like the fading process didn't start.
            // This is very situational and might have to be put behind a Flag (enum, bool, ...).
            // Right now, it's implemented like this to address the following issue:
            // https://github.com/manuelroemer/Celestial.UIToolkit/issues/14
            if (self.IsFading && !allowFading)
            {
                self.EnterExpandedVisualState();
            }
            
            if (self.IsExpanded && allowFading)
            {
                self.TryEnterFadingVisualState();
            }
        }

        private void EnterNormalVisualState()
        {
            IsExpanding = false;
            IsExpanded = false;
            IsFading = false;
            VisualStateManager.GoToState(this, NormalVisualState, true);
        }

        private void EnterExpandingVisualState()
        {
            IsExpanding = true;
            IsExpanded = false;
            IsFading = false;
            VisualStateManager.GoToState(this, NormalVisualState, false); // Required to reset potentially running animations.
            VisualStateManager.GoToState(this, ExpandingVisualState, true);
        }

        private void EnterExpandedVisualState()
        {
            IsExpanding = false;
            IsExpanded = true;
            IsFading = false;
            VisualStateManager.GoToState(this, ExpandedVisualState, true);
        }

        private void EnterFadingVisualState()
        {
            IsExpanding = false;
            IsExpanded = false;
            IsFading = true;
            VisualStateManager.GoToState(this, FadingVisualState, true);
        }
        
        private void TryEnterFadingVisualState()
        {
            // The animation should only be allowed to fade if the following conditions are met:
            // - AllowFading is true
            // - It has reached the maximum size
            // - It is not being forced to stay expanded (e.g. if a button is long-pressed).
            if (AllowFading && 
                IsExpanded &&
                !IsActiveTrigger)
            {
                EnterFadingVisualState();
            }
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
        /// The animation starts from the center of the <see cref="Ripple"/>.
        /// </summary>
        Center

    }

}
