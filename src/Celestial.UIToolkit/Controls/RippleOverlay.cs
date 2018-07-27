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

        private bool _isMousePressed = false;

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
        /// Identifies the <see cref="ActualAnimationScale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ActualAnimationScaleProperty = DependencyProperty.Register(
            nameof(ActualAnimationScale), typeof(double), typeof(RippleOverlay), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, AnimationScaleChanged));

        /// <summary>
        /// Identifies the <see cref="AnimationScale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationScaleProperty = DependencyProperty.Register(
            nameof(AnimationScale), typeof(double), typeof(RippleOverlay), new PropertyMetadata(1d));

        /// <summary>
        /// Identifies the <see cref="RippleOrigin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RippleOriginProperty = DependencyProperty.Register(
            nameof(RippleOrigin), typeof(RippleOrigin), typeof(RippleOverlay), new PropertyMetadata(RippleOrigin.ClickLocation));

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
        /// Loads template parts for the animation control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, NormalVisualStateName, true);
        }
        
        /// <summary>
        /// Called when the user clicks on this element (with the left mouse button).
        /// This sets the animation origin properties and starts the animation effect.
        /// </summary>
        /// <param name="e">Event args about the click.</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _isMousePressed = true;
            if (this.RippleOrigin == RippleOrigin.ClickLocation)
            {
                this.UpdateAnimationProperties(e.GetPosition(this));
            }
            else if (this.RippleOrigin == RippleOrigin.Center)
            {
                this.UpdateAnimationProperties(this.GetCenter());
            }
            else
            {
                throw new NotImplementedException("Unknown RippleOrigin enumeration value.");
            }
            
            // Always start expanding the animation when the element is pressed.
            VisualStateManager.GoToState(this, NormalVisualStateName, false);
            VisualStateManager.GoToState(this, ExpandingVisualStateName, true);

            this.CaptureMouse();

            base.OnPreviewMouseLeftButtonDown(e);
        }

        /// <summary>
        /// Called when the user lifts the left mouse button again.
        /// This stops the animation, if it has finished.
        /// </summary>
        /// <param name="e">Event args about the mouse data.</param>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.ReleaseMouseCapture();

            _isMousePressed = false;
            this.TryEnterFading();
            base.OnPreviewMouseLeftButtonUp(e);
        }

        /// <summary>
        /// Called when the <see cref="ActualAnimationScaleProperty"/>'s value is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> whose local value got changed.</param>
        /// <param name="e">Event data about the new value.</param>
        private static void AnimationScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RippleOverlay)d).TryEnterFading();
        }
        
        /// <summary>
        /// Called when either of the following events happens:
        /// <list type="bullet">
        ///     <item>
        ///         <description>The user stops pressing the element.</description>
        ///     </item>
        ///     <item>
        ///         <description>The <see cref="ActualAnimationScale"/> reaches the max. value.</description>
        ///     </item>
        /// </list>
        /// When both of these events have happened, it means that the animation can enter
        /// the 'Fading' state, meaning that it will disappear.
        /// </summary>
        private void TryEnterFading()
        {
            if (this.ActualAnimationScale >= this.AnimationScale && !_isMousePressed)
            {
                VisualStateManager.GoToState(this, FadingVisualStateName, true);
            }
        }

        /// <summary>
        /// Updates the several properties which define the animation's
        /// location and size, depending on the specified <paramref name="rippleOrigin"/>
        /// point.
        /// </summary>
        /// <param name="rippleOrigin">The point from which the ripple animation originates.</param>
        private void UpdateAnimationProperties(Point rippleOrigin)
        {
            this.AnimationOriginX = rippleOrigin.X;
            this.AnimationOriginY = rippleOrigin.Y;

            this.AnimationDiameter = Sqrt(  // This will make the ripple touch the out-most edge
                Pow(Max(rippleOrigin.X, this.ActualWidth - rippleOrigin.X), 2) +
                Pow(Max(rippleOrigin.Y, this.ActualHeight - rippleOrigin.Y), 2)) * 2;
            this.AnimationPositionX = this.AnimationOriginX - this.AnimationDiameter / 2;
            this.AnimationPositionY = this.AnimationOriginY - this.AnimationDiameter / 2;
        }

        /// <summary>
        /// Returns a pointer which represents the center of the element.
        /// </summary>
        /// <returns>The center <see cref="Point"/>.</returns>
        private Point GetCenter()
        {
            return new Point(
                this.ActualWidth / 2,
                this.ActualHeight / 2);
        }

    }

    /// <summary>
    /// Defines the available origin point types for a ripple animation.
    /// </summary>
    public enum RippleOrigin
    {

        /// <summary>
        /// The location where the user clicked the <see cref="RippleOverlay"/>.
        /// If it got triggered via an input which does not produce valid
        /// coordinates, <see cref="RippleOrigin.Center"/> will be used.
        /// </summary>
        ClickLocation,

        /// <summary>
        /// The animation starts from the center of the <see cref="RippleOverlay"/>.
        /// </summary>
        Center

    }

}
