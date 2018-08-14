using Celestial.UIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Math;

namespace Celestial.UIToolkit.Media.Animations
{

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
        : Singleton<TDeriving>, IAnimatedTypeHelper<TBrush>
        where TBrush : Brush
    {

        private AnimatedDoubleHelper _doubleHelper = AnimatedDoubleHelper.Instance;

        // For the animations to work, the brushes (each object really) must be cloned,
        // since working on reference types would yield incorrect results.
        // -> These methods clone the brushes, but delegate the actual value
        //    modification to the methods below.
        public TBrush AddValues(TBrush a, TBrush b)
        {
            TBrush result = (TBrush)a.Clone();
            this.AddValuesToResult(result, a, b);
            return result;
        }

        public TBrush SubtractValues(TBrush a, TBrush b)
        {
            TBrush result = (TBrush)a.Clone();
            this.SubtractValuesFromResult(result, a, b);
            return result;
        }

        public TBrush ScaleValue(TBrush value, double factor)
        {
            TBrush result = (TBrush)value.Clone();
            this.ScaleResult(result, factor);
            return result;
        }

        public TBrush InterpolateValue(TBrush from, TBrush to, double progress)
        {
            TBrush result = (TBrush)from.Clone();
            this.InterpolateResult(result, from, to, progress);
            return result;
        }

        public abstract TBrush GetZeroValue();

        // These methods do the actual modification of the result brush.
        // Since each brush has an Opacity, we can already animate that.
        protected virtual void AddValuesToResult(TBrush result, TBrush a, TBrush b)
        {
            result.Opacity = ClamOpacity(
                _doubleHelper.AddValues(a.Opacity, b.Opacity));
        }

        protected virtual void SubtractValuesFromResult(TBrush result, TBrush a, TBrush b)
        {
            result.Opacity = ClamOpacity(
                _doubleHelper.SubtractValues(a.Opacity, b.Opacity));
        }

        protected virtual void ScaleResult(TBrush result, double factor)
        {
            result.Opacity = ClamOpacity(
                _doubleHelper.ScaleValue(result.Opacity, factor));
        }

        protected virtual void InterpolateResult(TBrush result, TBrush from, TBrush to, double progress)
        {
            result.Opacity = ClamOpacity(
                AnimatedDoubleHelper.Instance.InterpolateValue(
                    from.Opacity, to.Opacity, progress));
        }

        private static double ClamOpacity(double opacity)
        {
            // A brush's opacity must be between 0 and 1.
            return Max(Min(opacity, 1d), 0d);
        }

    }

    internal abstract class AnimatedGradientBrushHelper<TDeriving, TBrush> :
        AnimatedBrushHelper<TDeriving, TBrush>
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

        public override SolidColorBrush GetZeroValue()
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

        private static AnimatedPointHelper _pointHelper;

        public override LinearGradientBrush GetZeroValue() => new LinearGradientBrush();

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

}
