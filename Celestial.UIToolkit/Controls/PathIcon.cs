using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// An <see cref="IconElement"/> which draws its shapes by using a <see cref="Geometry"/>.
    /// </summary>
    public class PathIcon : IconElement
    {

        private Path _path;

        /// <summary>
        /// Identifies the <see cref="Data"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            nameof(Data),
            typeof(Geometry),
            typeof(IconElement),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="StrokeThickness"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            nameof(StrokeThickness), 
            typeof(double), 
            typeof(PathIcon), 
            new FrameworkPropertyMetadata(
                1d, 
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="Fill"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill), 
            typeof(Brush), 
            typeof(PathIcon), 
            new FrameworkPropertyMetadata(
                Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets a <see cref="Geometry"/> which defines the icon.
        /// </summary>
        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the stroke thickness which is used for drawing the icon's path..
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets a <see cref="Brush"/> which is used to fill the icon's path.
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PathIcon"/> class.
        /// </summary>
        public PathIcon()
        {
        }
        
        protected override Size MeasureOverride(Size availableSize)
        {
            var normalBounds = this.Data.Bounds;
            var renderBounds = this.Data.GetRenderBounds(this.CreatePen()).Size;
            var diff = new Size(
                Math.Max(renderBounds.Width - normalBounds.Width, 0d),
                Math.Max(renderBounds.Height - normalBounds.Height, 0d));

            availableSize = new Size(
                availableSize.Width - diff.Width,
                availableSize.Height - diff.Height);
            
            _path = new Path() { Stretch = Stretch.Uniform };
            _path.Data = this.Data;
            _path.StrokeThickness = this.StrokeThickness;

            _path.Measure(availableSize);
            return _path.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            finalSize = new Size(
                Math.Min(finalSize.Width, _path.DesiredSize.Width),
                Math.Min(finalSize.Height, _path.DesiredSize.Height));
            _path.Arrange(new Rect(finalSize));
            return finalSize;
        }
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawGeometry(this.Fill, this.CreatePen(), _path.RenderedGeometry);
        }
        
        /// <summary>
        /// Creates a new <see cref="Pen"/> which should be used for drawing
        /// the icon's path.
        /// </summary>
        /// <returns>The newly created <see cref="Pen"/>.</returns>
        protected Pen CreatePen()
        {
            return new Pen(this.Foreground, this.StrokeThickness);
        }
        
    }

}
