using System;
using System.Windows;
using System.Windows.Controls;
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
        
        static PathIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(PathIcon), new FrameworkPropertyMetadata(typeof(PathIcon)));
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PathIcon"/> class.
        /// </summary>
        public PathIcon()
        {
            CreatePath();
        }

        private void CreatePath()
        {
            _path = new Path()
            {
                Stretch = Stretch.Uniform
            };
            _path.SetBinding(Path.FillProperty, CreateSelfBinding(ForegroundProperty));
            _path.SetBinding(Path.StrokeProperty, CreateSelfBinding(ForegroundProperty));
            _path.SetBinding(Path.DataProperty, CreateSelfBinding(DataProperty));
            _path.SetBinding(Path.StrokeThicknessProperty, CreateSelfBinding(StrokeThicknessProperty));

            SetChildInViewbox(_path);
        }

        private Binding CreateSelfBinding(DependencyProperty toProperty)
        {
            return new Binding()
            {
                Path = new PropertyPath(toProperty),
                Source = this,
                Mode = BindingMode.OneWay
            };
        }
        
    }

}
