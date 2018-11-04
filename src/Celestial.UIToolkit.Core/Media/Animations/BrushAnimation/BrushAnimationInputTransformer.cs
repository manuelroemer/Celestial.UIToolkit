using System;
using System.Windows.Media;

namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    ///     Modifies the two from/to input Brushes for the BrushAnimations.
    ///     This modification is done if one of the brushes is null, or if the types don't match
    ///     and one of the types can be converted to the type.
    /// 
    ///     An example is an animation from a SolidColorBrush to a LinearGradientBrush.
    ///     The SolidColorBrush can be converted to a LinearGradientBrush without losing any data.
    /// </summary>
    internal static class BrushAnimationInputTransformer
    {

        // We must use a custom transparent brush, since the WPF Transparent brush is defined as
        // 0x00FFFFFF. 
        // When adding brushes, this means that the RGB channels of other colors get "filled".
        // With this custom brush (0x00000000) that doesn't happen.
        private static readonly SolidColorBrush _transparent = new SolidColorBrush(new Color());

        public static Brush Transform(Brush brush)
        {
            // We can also transform a single brush (e.g. for Scale() methods).
            // We can however only do null checks.
            if (brush == null)
            {
                brush = _transparent;
            }
            return brush;
        }

        public static BrushAnimationInput Transform(Brush from, Brush to)
        {
            // If one of the two brushes is null, change them to Transparent.
            // This is better than throwing an exception, since WPF sometimes seems to pass
            // null instead of a real brush (I think that this can happen if a control is not fully
            // loaded).
            // It would be bad if an application crashed in such a case, even though the developer
            // didn't do anything wrong.
            if (from == null)
            {
                from = _transparent;
            }
            if (to == null)
            {
                to = _transparent;
            }

            
            // Furthermore, we can convert SolidColorBrushes to a GradientBrush.
            // The animation doesn't throw an exception for mismatching types if we do that.
            if (from.GetType() != to.GetType())
            {
                if (from is SolidColorBrush solidFrom && to is GradientBrush gradientTo)
                {
                    from = solidFrom.ToGradientBrush(gradientTo);
                }
                else if (to is SolidColorBrush solidTo && from is GradientBrush gradientFrom)
                {
                    to = solidTo.ToGradientBrush(gradientFrom);
                }
                else
                {
                    // We cannot match the types.
                    // This should have been handled by validation!
                    throw new InvalidOperationException(
                        $"Cannot create an animation between brushes of type " +
                        $"${from.GetType().FullName} and {to.GetType().FullName}.");
                }
            }

            // All good, the brushes now have a value and are of the same type.
            return new BrushAnimationInput(from, to);
        }

        private static TGradient ToGradientBrush<TGradient>(
            this SolidColorBrush solidBrush, TGradient mirror) where TGradient : GradientBrush
        {
            GradientBrush result = mirror.Clone();
            foreach (var gradientStop in result.GradientStops)
            {
                gradientStop.Color = solidBrush.Color;
            }
            return (TGradient)result;
        }

    }

    internal readonly struct BrushAnimationInput
    {

        public Brush From { get; }

        public Brush To { get; }
        
        public BrushAnimationInput(Brush from, Brush to)
        {
            From = from;
            To = to;
        }

    }

}
