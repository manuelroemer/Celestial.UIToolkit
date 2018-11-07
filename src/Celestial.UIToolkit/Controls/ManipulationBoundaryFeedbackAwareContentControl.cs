using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Celestial.UIToolkit.Controls
{

    // This control's name is bad. Does anyone know something better?
    

    /// <summary>
    /// A <see cref="ContentControl"/> which reacts to the 
    /// <see cref="UIElement.ManipulationBoundaryFeedback"/> event of either the content,
    /// and/or an element directly specified via the <see cref="FeedbackProvider"/> property.
    /// </summary>
    public class ManipulationBoundaryFeedbackAwareContentControl : ContentControl
    {
        
        /// <summary>
        /// Identifies the <see cref="FeedbackProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FeedbackProviderProperty =
            DependencyProperty.Register(
                nameof(FeedbackProvider),
                typeof(UIElement),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(null, FeedbackProvider_Changed));

        /// <summary>
        /// Identifies the <see cref="PreventBubbling"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreventBubblingProperty =
            DependencyProperty.Register(
                nameof(PreventBubbling),
                typeof(bool),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="TranslationDeltaX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TranslationDeltaXProperty =
            DependencyProperty.Register(
                nameof(TranslationDeltaX),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="TranslationDeltaY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TranslationDeltaYProperty =
            DependencyProperty.Register(
                nameof(TranslationDeltaY),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="Rotation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register(
                nameof(Rotation),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ScaleDeltaX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleDeltaXProperty =
            DependencyProperty.Register(
                nameof(ScaleDeltaX),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ScaleDeltaY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleDeltaYProperty =
            DependencyProperty.Register(
                nameof(ScaleDeltaY),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ExpansionDeltaX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpansionDeltaXProperty =
            DependencyProperty.Register(
                nameof(ExpansionDeltaX),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ExpansionDeltaY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpansionDeltaYProperty =
            DependencyProperty.Register(
                nameof(ExpansionDeltaY),
                typeof(double),
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new PropertyMetadata(0d));

        /// <summary>
        /// Gets or sets the <see cref="UIElement"/> which provides manipulation boundary
        /// feedback via the <see cref="UIElement.ManipulationBoundaryFeedback"/> event
        /// to which this control listens.
        /// </summary>
        public UIElement FeedbackProvider
        {
            get { return (UIElement)GetValue(FeedbackProviderProperty); }
            set { SetValue(FeedbackProviderProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the
        /// <see cref="RoutedEventArgs.Handled"/> property of the received
        /// <see cref="ManipulationBoundaryFeedbackEventArgs"/> will be set to true,
        /// when such an event is received.
        /// </summary>
        public bool PreventBubbling
        {
            get { return (bool)GetValue(PreventBubblingProperty); }
            set { SetValue(PreventBubblingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the most recent manipulation's delta translation.
        /// </summary>
        public double TranslationDeltaX
        {
            get { return (double)GetValue(TranslationDeltaXProperty); }
            set { SetValue(TranslationDeltaXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the most recent manipulation's delta translation.
        /// </summary>
        public double TranslationDeltaY
        {
            get { return (double)GetValue(TranslationDeltaYProperty); }
            set { SetValue(TranslationDeltaYProperty, value); }
        }

        /// <summary>
        /// Gets or sets the rotation of the most recent manipulation in degrees.
        /// </summary>
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        /// <summary>
        /// Gets the x-coordinate of the most recent manipulation's delta scale.
        /// </summary>
        public double ScaleDeltaX
        {
            get { return (double)GetValue(ScaleDeltaXProperty); }
            set { SetValue(ScaleDeltaXProperty, value); }
        }

        /// <summary>
        /// Gets the y-coordinate of the most recent manipulation's delta scale.
        /// </summary>
        public double ScaleDeltaY
        {
            get { return (double)GetValue(ScaleDeltaYProperty); }
            set { SetValue(ScaleDeltaYProperty, value); }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the most recent manipulation's expansion delta.
        /// </summary>
        public double ExpansionDeltaX
        {
            get { return (double)GetValue(ExpansionDeltaXProperty); }
            set { SetValue(ExpansionDeltaXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the most recent manipulation's expansion delta.
        /// </summary>
        public double ExpansionDeltaY
        {
            get { return (double)GetValue(ExpansionDeltaYProperty); }
            set { SetValue(ExpansionDeltaYProperty, value); }
        }

        static ManipulationBoundaryFeedbackAwareContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ManipulationBoundaryFeedbackAwareContentControl),
                new FrameworkPropertyMetadata(
                    typeof(ManipulationBoundaryFeedbackAwareContentControl)));
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ManipulationBoundaryFeedbackAwareContentControl"/> class.
        /// </summary>
        public ManipulationBoundaryFeedbackAwareContentControl()
        {
        }

        private static void FeedbackProvider_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (ManipulationBoundaryFeedbackAwareContentControl)d;
            var oldElement = (UIElement)e.OldValue;
            var newElement = (UIElement)e.NewValue;

            // The FeedbackProvider got changed. Remove old/add new handlers.
            if (oldElement != null)
            {
                WeakEventManager<UIElement, ManipulationBoundaryFeedbackEventArgs>.RemoveHandler(
                    oldElement,
                    nameof(ManipulationBoundaryFeedback),
                    self.FeedbackProvider_ManipulationBoundaryFeedback);
                self.TraceVerbose("Removed FeedbackProvider {0}.", oldElement);
            }

            if (newElement != null)
            {
                WeakEventManager<UIElement, ManipulationBoundaryFeedbackEventArgs>.AddHandler(
                    newElement,
                    nameof(ManipulationBoundaryFeedback),
                    self.FeedbackProvider_ManipulationBoundaryFeedback);
                self.TraceVerbose("Added FeedbackProvider {0}.", oldElement);
            }
        }

        private void FeedbackProvider_ManipulationBoundaryFeedback(
            object sender, ManipulationBoundaryFeedbackEventArgs args)
        {
            this.TraceVerbose("Received manipulation boundary feedback from the FeedbackProvider.");
            OnManipulationBoundaryFeedbackReceived(args);
        }

        /// <summary>
        /// Called when the control's content emits the
        /// <see cref="UIElement.ManipulationBoundaryFeedback"/> event.
        /// </summary>
        /// <param name="e">The event args which define the manipulation boundary feedback.</param>
        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            // Also handle events if they come from the content.
            this.TraceVerbose("Received manipulation boundary feedback from the content.");
            OnManipulationBoundaryFeedbackReceived(e);
        }

        /// <summary>
        /// Called when either the control's content or the <see cref="FeedbackProvider"/>
        /// emits the <see cref="UIElement.ManipulationBoundaryFeedback"/> event.
        /// This method handles the event from either source.
        /// </summary>
        /// <param name="e">The event args which define the manipulation boundary feedback.</param>
        protected virtual void OnManipulationBoundaryFeedbackReceived(ManipulationBoundaryFeedbackEventArgs e)
        {
            if (PreventBubbling)
            {
                this.TraceVerbose("Preventing bubbling of the event.");
                e.Handled = true;
            }
            else
            {
                this.TraceVerbose("Not preventing bubbling of the event.");
            }

            // We copy the event data into (dependency) properties.
            // This allows a corresponding control template to visually represent the
            // manipulation boundary feedback data.
            TranslationDeltaX = e.BoundaryFeedback.Translation.X;
            TranslationDeltaY = e.BoundaryFeedback.Translation.Y;
            Rotation = e.BoundaryFeedback.Rotation;
            ScaleDeltaX = e.BoundaryFeedback.Scale.X;
            ScaleDeltaY = e.BoundaryFeedback.Scale.Y;
            ExpansionDeltaX = e.BoundaryFeedback.Expansion.X;
            ExpansionDeltaY = e.BoundaryFeedback.Expansion.Y;
        }

    }

}
