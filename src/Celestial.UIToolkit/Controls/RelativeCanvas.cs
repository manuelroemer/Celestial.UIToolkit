using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Celestial.UIToolkit.TraceSources;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// Defines an area within which you can position children based on coordinates,
    /// relative to the canvas element itself.
    /// </summary>
    public class RelativeCanvas : Panel
    {

        /// <summary>
        /// Identifies the Left attached dependency property.
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.RegisterAttached(
                "Left",
                typeof(double),
                typeof(RelativeCanvas),
                new PropertyMetadata(double.NaN, ChildPosition_Changed),
                ValidatePositionProperty);

        /// <summary>
        /// Identifies the Top attached dependency property.
        /// </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.RegisterAttached(
                "Top",
                typeof(double),
                typeof(RelativeCanvas),
                new PropertyMetadata(double.NaN, ChildPosition_Changed),
                ValidatePositionProperty);

        /// <summary>
        /// Identifies the Right attached dependency property.
        /// </summary>
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.RegisterAttached(
                "Right",
                typeof(double),
                typeof(RelativeCanvas),
                new PropertyMetadata(double.NaN, ChildPosition_Changed),
                ValidatePositionProperty);
        
        /// <summary>
        /// Identifies the Bottom attached dependency property.
        /// </summary>
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.RegisterAttached(
                "Bottom",
                typeof(double),
                typeof(RelativeCanvas),
                new PropertyMetadata(double.NaN, ChildPosition_Changed),
                ValidatePositionProperty);

        /// <summary>
        /// Gets the value of the <see cref="LeftProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="LeftProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="LeftProperty"/> attached dependency property.
        /// </returns>
        public static double GetLeft(DependencyObject obj) =>
            (double)obj.GetValue(LeftProperty);

        /// <summary>
        /// Sets the value of the <see cref="LeftProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="LeftProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetLeft(DependencyObject obj, double value) =>
            obj.SetValue(LeftProperty, value);

        /// <summary>
        /// Gets the value of the <see cref="TopProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TopProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="TopProperty"/> attached dependency property.
        /// </returns>
        public static double GetTop(DependencyObject obj) =>
            (double)obj.GetValue(TopProperty);

        /// <summary>
        /// Sets the value of the <see cref="TopProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="TopProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetTop(DependencyObject obj, double value) =>
            obj.SetValue(TopProperty, value);

        /// <summary>
        /// Gets the value of the <see cref="RightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="RightProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="RightProperty"/> attached dependency property.
        /// </returns>
        public static double GetRight(DependencyObject obj) =>
            (double)obj.GetValue(RightProperty);

        /// <summary>
        /// Sets the value of the <see cref="RightProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="RightProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetRight(DependencyObject obj, double value) =>
            obj.SetValue(RightProperty, value);

        /// <summary>
        /// Gets the value of the <see cref="BottomProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BottomProperty"/> attached dependency property
        /// should be retrieved.
        /// </param>
        /// <returns>
        /// The local value of the <see cref="BottomProperty"/> attached dependency property.
        /// </returns>
        public static double GetBottom(DependencyObject obj) =>
            (double)obj.GetValue(BottomProperty);

        /// <summary>
        /// Sets the value of the <see cref="BottomProperty"/> attached dependency property.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="DependencyObject"/> for which the local value of the
        /// <see cref="BottomProperty"/> attached dependency property
        /// should be set.
        /// </param>
        /// <param name="value">
        /// The new value for the dependency property.
        /// </param>
        public static void SetBottom(DependencyObject obj, double value) =>
            obj.SetValue(BottomProperty, value);

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeCanvas"/> class.
        /// </summary>
        public RelativeCanvas()
        {
            SizeChanged += RelativeCanvas_SizeChanged;
        }

        private static bool ValidatePositionProperty(object obj)
        {
            double value = (double)obj;
            return !double.IsInfinity(value);
        }
        
        private static void ChildPosition_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Re-arrange the canvas' elements whenever the layout property of one of its
            // children gets changed.
            if (d is UIElement child &&
                VisualTreeHelper.GetParent(child) is RelativeCanvas parentCanvas)
            {
                parentCanvas.TraceVerbose(
                    "Position property of child {0} changed. Rearranging the children...",
                    child);
                parentCanvas.InvalidateArrange();
            }
        }

        private void RelativeCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // In comparison to the normal canvas, we also have to re-arrange the children
            // when the canvas itself changes size.
            InvalidateArrange();
        }

        /// <summary>
        /// Measures the size of the panel's children.
        /// </summary>
        /// <param name="availableSize">
        /// The size which is available to the canvas.
        /// This value gets overwritten and thus doesn't get used.
        /// </param>
        /// <returns>
        /// An empty size (0;0).
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            // Override the availableSize, similar to the WPF Canvas.
            availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);

            foreach (var child in Children)
            {
                if (child is UIElement uiElement)
                {
                    ControlsSource.Verbose("Measuring child {0}.", uiElement);
                    uiElement.Measure(availableSize);
                }
            }
            return new Size();
        }

        /// <summary>
        /// Positions the canvas' children at their effective positions.
        /// </summary>
        /// <param name="finalSize">The final size of the canvas.</param>
        /// <returns>
        /// The final size which was used.
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var child in Children)
            {
                if (child is UIElement uiElementChild)
                {
                    var finalPosition = new Point(
                        CalculateEffectiveChildX(uiElementChild, finalSize),
                        CalculateEffectiveChildY(uiElementChild, finalSize));
                    uiElementChild.Arrange(new Rect(finalPosition, uiElementChild.DesiredSize));
                    this.TraceVerbose("Arranged child {0} at position {1}", uiElementChild, finalPosition);
                }
            }

            return finalSize;
        }

        private double CalculateEffectiveChildX(UIElement child, Size finalSize)
        {
            double left = GetLeft(child);
            double right = GetRight(child);
            double finalRelativeX = 0d;

            // Left wins over Right.
            if (!double.IsNaN(left))
            {
                finalRelativeX = left;
            }
            else if (!double.IsNaN(right))
            {
                double relativeChildWidth = child.DesiredSize.Width / finalSize.Width;
                finalRelativeX = 1d - right - relativeChildWidth;
            }

            return finalRelativeX * finalSize.Width;
        }

        private double CalculateEffectiveChildY(UIElement child, Size finalSize)
        {
            double top = GetTop(child);
            double bottom = GetBottom(child);
            double finalRelativeY = 0d;

            // Top wins over Bottom.
            if (!double.IsNaN(top))
            {
                finalRelativeY = top;
            }
            else if (!double.IsNaN(bottom))
            {
                double relativeChildHeight = child.DesiredSize.Height / finalSize.Height;
                finalRelativeY = 1d - bottom - relativeChildHeight;
            }

            return finalRelativeY * finalSize.Width;
        }

        /// <summary>
        /// Depending on <see cref="UIElement.ClipToBounds"/>, this method returns
        /// null or a clipping geometry for child elements.
        /// </summary>
        /// <param name="layoutSlotSize">Not used.</param>
        /// <returns>
        /// If <see cref="UIElement.ClipToBounds"/> is true, this returns a 
        /// <see cref="RectangleGeometry"/> which represents the canvas' size.
        /// Otherwise, this returns null.
        /// </returns>
        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            if (ClipToBounds)
            {
                return new RectangleGeometry(new Rect(RenderSize));
            }
            else
            {
                return null;
            }
        }

    }

}
