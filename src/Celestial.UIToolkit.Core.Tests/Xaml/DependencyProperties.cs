using System;
using System.Windows;

namespace Celestial.UIToolkit.Tests.Xaml
{

    /// <summary>
    /// Provides static dependency properties of various types to be used for testing.
    /// </summary>
    public static class DependencyProperties
    {

        public static readonly DependencyProperty DoubleProperty =
            DependencyProperty.Register(
                "DoubleProperty",
                typeof(Double),
                typeof(DependencyProperties));

        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register(
                "ThicknessProperty",
                typeof(Thickness),
                typeof(DependencyProperties));

        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register(
                "PointProperty",
                typeof(Point),
                typeof(DependencyProperties));

    }

}
