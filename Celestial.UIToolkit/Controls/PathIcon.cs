using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Celestial.UIToolkit.Controls
{

    /// <summary>
    /// An <see cref="IconElement"/> which draws its shapes by using a <see cref="Geometry"/>.
    /// </summary>
    public class PathIcon : IconElement
    {

        /// <summary>
        /// Do not use this field directly.
        /// Access it via the <see cref="Path"/> property.
        /// </summary>
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
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                PathPropertyChanged));

        /// <summary>
        /// Identifies the <see cref="StrokeThickness"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            nameof(StrokeThickness), 
            typeof(double), 
            typeof(PathIcon), 
            new FrameworkPropertyMetadata(
                1d, 
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                PathPropertyChanged));

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
        /// Gets a potentially newly created <see cref="Path"/> object
        /// which is used by the <see cref="PathIcon"/> to render the icon.
        /// </summary>
        private Path Path
        {
            get
            {
                if (_path == null)
                {
                    // Mirror this class' properties into the path,
                    // so that the render-size calculations performed by the path
                    // will yield the right results.
                    _path = new Path()
                    {
                        Stretch = Stretch.Uniform,
                        Data = this.Data,
                        StrokeThickness = this.StrokeThickness
                    };
                }
                return _path;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PathIcon"/> class.
        /// </summary>
        public PathIcon()
        {
        }

        /// <summary>
        /// Measures the size required for the icon.
        /// </summary>
        /// <param name="availableSize">
        /// The available size that this element can give to child elements.
        /// </param>
        /// <returns>
        /// The size that this element determines it needs during layout, based on its calculations
        /// of child element sizes.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Data == null || this.Data == Geometry.Empty) return new Size(0d, 0d);

            // We let a Path do the work of measuring the geometry.
            // We need to take care of one point though:
            //
            // If we gave the path the default availableSize, it would
            // be drawn over the edges of the allowed rectangle (for whatever reason).
            // As a result, we need to artifically shrink the available area,
            // so that the path has the exact allowed bounds.
            // The calculation is basically:
            // 
            // pathOverflow = dataRenderBounds - dataNormalBounds
            // finalBounds  = availableSize - pathOverflow
            var normalBounds = this.Data.Bounds;
            var renderBounds = this.Data.GetRenderBounds(this.CreatePen()).Size;
            var diff = new Size(
                Math.Max(renderBounds.Width - normalBounds.Width, 0d),
                Math.Max(renderBounds.Height - normalBounds.Height, 0d));

            availableSize = new Size(
                availableSize.Width - diff.Width,
                availableSize.Height - diff.Height);
            
            this.Path.Measure(availableSize);
            return this.Path.DesiredSize;
        }

        /// <summary>
        /// Positions the child elements of the <see cref="PathIcon"/>.
        /// </summary>
        /// <param name="finalSize">
        /// The final area within the parent that this element should use to arrange itself
        /// and its children.
        /// </param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            // Again, let the path do the work.
            // Provide the correct size though - do not go over the max. allowed bounds.
            finalSize = new Size(
                Math.Min(finalSize.Width, this.Path.DesiredSize.Width),
                Math.Min(finalSize.Height, this.Path.DesiredSize.Height));
            this.Path.Arrange(new Rect(finalSize));
            return finalSize;
        }
        
        /// <summary>
        /// Renders the icon, based on the properties in this class.
        /// </summary>
        /// <param name="drawingContext">
        /// A <see cref="DrawingContext"/> provided by the layout system
        /// which is used to draw the icon.
        /// </param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawGeometry(this.Fill, this.CreatePen(), this.Path.RenderedGeometry);
        }
        
        /// <summary>
        /// Creates a new <see cref="Pen"/> which should be used for drawing
        /// the icon's path.
        /// If not overridden, the pen's values are based on the properties in this class.
        /// </summary>
        /// <returns>The newly created <see cref="Pen"/>.</returns>
        protected virtual Pen CreatePen()
        {
            return new Pen(this.Foreground, this.StrokeThickness);
        }

        /// <summary>
        /// Called whenever a property which is mirrored into the internal <see cref="Path"/> object 
        /// is changed.
        /// This will force the creation of a new path to reflect the results.
        /// /// </summary>
        /// <param name="depObj">An instance of <see cref="PathIcon"/>.</param>
        /// <param name="e">Event args.</param>
        private static void PathPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            // This will force the creation of a new path object, when it is accessed.
            ((PathIcon)depObj)._path = null;
        }

    }

}
