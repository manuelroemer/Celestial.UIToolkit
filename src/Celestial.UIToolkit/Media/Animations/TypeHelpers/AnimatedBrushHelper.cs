using Celestial.UIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using static System.Math;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Provides access to the brush helpers which are available.
    /// </summary>
    internal static class AnimatedBrushHelpers
    {

        /// <summary>
        /// A dictionary which maps <see cref="Brush"/> types to their corresponding
        /// animation helpers.
        /// </summary>
        public static Dictionary<Type, IAnimatedTypeHelper<Brush>> SupportedTypeHelpers
            = new Dictionary<Type, IAnimatedTypeHelper<Brush>>()
        {
            [typeof(SolidColorBrush)]     = AnimatedSolidColorBrushHelper.Instance,
            [typeof(LinearGradientBrush)] = AnimatedLinearGradientBrushHelper.Instance,
            [typeof(RadialGradientBrush)] = AnimatedRadialGradientBrushHelper.Instance
        };
        
    }

    /// <summary>
    /// A base class for an animation helper that animates a brush.
    /// This class already animates the brush's <see cref="Brush.Opacity"/>
    /// property.
    /// </summary>
    /// <typeparam name="TDeriving">
    /// The class deriving from this class.
    /// Used to setup a singleton pattern.
    /// </typeparam>
    /// <typeparam name="TBrush">
    /// The type of <see cref="Brush"/> which the deriving class is animating.
    /// </typeparam>
    internal abstract class AnimatedBrushHelper<TDeriving, TBrush> 
        : Singleton<TDeriving>, IAnimatedTypeHelper<Brush>
        where TBrush : Brush
    {
        
        private AnimatedDoubleHelper _doubleHelper = AnimatedDoubleHelper.Instance;
        
        public abstract Brush GetZeroValue();

        // For the animations to work, the brushes (each object really) must be cloned,
        // since working on reference types would yield incorrect results.
        // -> These methods clone the brushes, but delegate the actual value
        //    modification to the methods below.
        public Brush AddValues(Brush a, Brush b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));

            Brush result = a.Clone();
            this.AddValuesToResult((TBrush)result, (TBrush)a, (TBrush)b);
            return result;
        }

        public Brush SubtractValues(Brush a, Brush b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));

            Brush result = a.Clone();
            this.SubtractValuesFromResult((TBrush)result, (TBrush)a, (TBrush)b);
            return result;
        }

        public Brush ScaleValue(Brush value, double factor)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Brush result = value.Clone();
            this.ScaleResult((TBrush)result, factor);
            return result;
        }

        public Brush InterpolateValue(Brush from, Brush to, double progress)
        {
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (to == null) throw new ArgumentNullException(nameof(to));

            Brush result = from.Clone();
            this.InterpolateResult((TBrush)result, (TBrush)from, (TBrush)to, progress);
            return result;
        }
        
        protected virtual void AddValuesToResult(TBrush result, TBrush a, TBrush b)
        {
            result.Opacity = _doubleHelper.AddValues(a.Opacity, b.Opacity);
        }
        
        protected virtual void SubtractValuesFromResult(TBrush result, TBrush a, TBrush b)
        {
            result.Opacity = _doubleHelper.SubtractValues(a.Opacity, b.Opacity);
        }
        
        protected virtual void ScaleResult(TBrush result, double factor)
        {
            result.Opacity = _doubleHelper.ScaleValue(result.Opacity, factor);
        }

        protected virtual void InterpolateResult(TBrush result, TBrush from, TBrush to, double progress)
        {
            result.Opacity = _doubleHelper.InterpolateValue(from.Opacity, to.Opacity, progress);
        }
        
    }

    internal abstract class AnimatedGradientBrushHelper<TDeriving, TBrush> 
        : AnimatedBrushHelper<TDeriving, TBrush>
        where TBrush : GradientBrush
    {

        private AnimatedDoubleHelper _doubleHelper = AnimatedDoubleHelper.Instance;
        private AnimatedColorHelper _colorHelper = AnimatedColorHelper.Instance;

        protected override void AddValuesToResult(TBrush result, TBrush a, TBrush b)
        {
            base.AddValuesToResult(result, a, b);
            this.AddGradientStops(result.GradientStops, a.GradientStops, b.GradientStops);
        }

        protected override void SubtractValuesFromResult(TBrush result, TBrush a, TBrush b)
        {
            base.SubtractValuesFromResult(result, a, b);
            this.SubtractGradientStops(result.GradientStops, a.GradientStops, b.GradientStops);
        }

        protected override void ScaleResult(TBrush result, double factor)
        {
            base.ScaleResult(result, factor);
            this.ScaleGradientStops(result.GradientStops, factor);
        }

        protected override void InterpolateResult(TBrush result, TBrush from, TBrush to, double progress)
        {
            base.InterpolateResult(result, from, to, progress);
            this.InterpolateGradientStops(result.GradientStops, from.GradientStops, to.GradientStops, progress);
        }

        private void AddGradientStops(GradientStopCollection result, GradientStopCollection a, GradientStopCollection b)
        {
            for (int i = 0; i < Min(a.Count, b.Count); i++)
            {
                GradientStop gradientStop = result[i];
                gradientStop.Offset = _doubleHelper.AddValues(a[i].Offset, b[i].Offset);
                gradientStop.Color = _colorHelper.AddValues(a[i].Color, b[i].Color);
            }
        }

        private void SubtractGradientStops(GradientStopCollection result, GradientStopCollection a, GradientStopCollection b)
        {
            for (int i = 0; i < Min(a.Count, b.Count); i++)
            {
                GradientStop gradientStop = result[i];
                gradientStop.Offset = _doubleHelper.SubtractValues(a[i].Offset, b[i].Offset);
                gradientStop.Color = _colorHelper.SubtractValues(a[i].Color, b[i].Color);
            }
        }

        private void ScaleGradientStops(GradientStopCollection result, double factor)
        {
            for (int i = 0; i < result.Count; i++)
            {
                GradientStop gradientStop = result[i];
                gradientStop.Offset = _doubleHelper.ScaleValue(result[i].Offset, factor);
                gradientStop.Color = _colorHelper.ScaleValue(result[i].Color, factor);
            }
        }

        private void InterpolateGradientStops(
            GradientStopCollection result, GradientStopCollection from, GradientStopCollection to, double progress)
        {
            for (int i = 0; i < Min(from.Count, to.Count); i++)
            {
                GradientStop gradientStop = result[i];
                gradientStop.Offset = _doubleHelper.InterpolateValue(from[i].Offset, to[i].Offset, progress);
                gradientStop.Color = _colorHelper.InterpolateValue(from[i].Color, to[i].Color, progress);
            }
        }
        
    }

    internal sealed class AnimatedSolidColorBrushHelper 
        : AnimatedBrushHelper<AnimatedSolidColorBrushHelper, SolidColorBrush>
    {

        private static AnimatedColorHelper _colorHelper = AnimatedColorHelper.Instance;

        public AnimatedSolidColorBrushHelper() { }

        public override Brush GetZeroValue()
        {
            return new SolidColorBrush();
        }

        protected override void AddValuesToResult(SolidColorBrush result, SolidColorBrush a, SolidColorBrush b)
        {
            base.AddValuesToResult(result, a, b);
            result.Color = _colorHelper.AddValues(a.Color, b.Color);
        }

        protected override void SubtractValuesFromResult(SolidColorBrush result, SolidColorBrush a, SolidColorBrush b)
        {
            base.SubtractValuesFromResult(result, a, b);
            result.Color = _colorHelper.SubtractValues(a.Color, b.Color);
        }

        protected override void ScaleResult(SolidColorBrush result, double factor)
        {
            base.ScaleResult(result, factor);
            result.Color = _colorHelper.ScaleValue(result.Color, factor);
        }

        protected override void InterpolateResult(SolidColorBrush result, SolidColorBrush from, SolidColorBrush to, double progress)
        {
            base.InterpolateResult(result, from, to, progress);
            result.Color = _colorHelper.InterpolateValue(from.Color, to.Color, progress);
        }

    }

    internal sealed class AnimatedLinearGradientBrushHelper
        : AnimatedGradientBrushHelper<AnimatedLinearGradientBrushHelper, LinearGradientBrush>
    {

        private static AnimatedPointHelper _pointHelper = AnimatedPointHelper.Instance;

        public override Brush GetZeroValue() => new LinearGradientBrush();

        protected override void AddValuesToResult(LinearGradientBrush result, LinearGradientBrush a, LinearGradientBrush b)
        {
            base.AddValuesToResult(result, a, b);
            result.StartPoint = _pointHelper.AddValues(a.StartPoint, b.StartPoint);
            result.EndPoint = _pointHelper.AddValues(a.EndPoint, b.EndPoint);
        }

        protected override void SubtractValuesFromResult(LinearGradientBrush result, LinearGradientBrush a, LinearGradientBrush b)
        {
            base.SubtractValuesFromResult(result, a, b);
            result.StartPoint = _pointHelper.SubtractValues(a.StartPoint, b.StartPoint);
            result.EndPoint = _pointHelper.SubtractValues(a.EndPoint, b.EndPoint);
        }

        protected override void ScaleResult(LinearGradientBrush result, double factor)
        {
            base.ScaleResult(result, factor);
            result.StartPoint = _pointHelper.ScaleValue(result.StartPoint, factor);
            result.EndPoint = _pointHelper.ScaleValue(result.EndPoint, factor);
        }

        protected override void InterpolateResult(LinearGradientBrush result, LinearGradientBrush from, LinearGradientBrush to, double progress)
        {
            base.InterpolateResult(result, from, to, progress);
            result.StartPoint = _pointHelper.InterpolateValue(from.StartPoint, to.StartPoint, progress);
            result.EndPoint = _pointHelper.InterpolateValue(from.EndPoint, to.EndPoint, progress);
        }

    }
    
    internal sealed class AnimatedRadialGradientBrushHelper
        : AnimatedGradientBrushHelper<AnimatedRadialGradientBrushHelper, RadialGradientBrush>
    {

        private static AnimatedDoubleHelper _doubleHelper = AnimatedDoubleHelper.Instance;
        private static AnimatedPointHelper _pointHelper = AnimatedPointHelper.Instance;

        public override Brush GetZeroValue() => new RadialGradientBrush();

        protected override void AddValuesToResult(RadialGradientBrush result, RadialGradientBrush a, RadialGradientBrush b)
        {
            base.AddValuesToResult(result, a, b);
            result.RadiusX = _doubleHelper.AddValues(a.RadiusX, b.RadiusX);
            result.RadiusY = _doubleHelper.AddValues(a.RadiusY, b.RadiusY);
            result.Center = _pointHelper.AddValues(a.Center, b.Center);
            result.GradientOrigin = _pointHelper.AddValues(a.GradientOrigin, b.GradientOrigin);
        }

        protected override void SubtractValuesFromResult(RadialGradientBrush result, RadialGradientBrush a, RadialGradientBrush b)
        {
            base.SubtractValuesFromResult(result, a, b);
            result.RadiusX = _doubleHelper.SubtractValues(a.RadiusX, b.RadiusX);
            result.RadiusY = _doubleHelper.SubtractValues(a.RadiusY, b.RadiusY);
            result.Center = _pointHelper.SubtractValues(a.Center, b.Center);
            result.GradientOrigin = _pointHelper.SubtractValues(a.GradientOrigin, b.GradientOrigin);
        }

        protected override void ScaleResult(RadialGradientBrush result, double factor)
        {
            base.ScaleResult(result, factor);
            result.RadiusX = _doubleHelper.ScaleValue(result.RadiusX, factor);
            result.RadiusY = _doubleHelper.ScaleValue(result.RadiusY, factor);
            result.Center = _pointHelper.ScaleValue(result.Center, factor);
            result.GradientOrigin = _pointHelper.ScaleValue(result.GradientOrigin, factor);
        }

        protected override void InterpolateResult(RadialGradientBrush result, RadialGradientBrush from, RadialGradientBrush to, double progress)
        {
            base.InterpolateResult(result, from, to, progress);
            result.RadiusX = _doubleHelper.InterpolateValue(from.RadiusX, to.RadiusX, progress);
            result.RadiusY = _doubleHelper.InterpolateValue(from.RadiusY, to.RadiusY, progress);
            result.Center = _pointHelper.InterpolateValue(from.Center, to.Center, progress);
            result.GradientOrigin = _pointHelper.InterpolateValue(from.GradientOrigin, to.GradientOrigin, progress);
        }

    }

}