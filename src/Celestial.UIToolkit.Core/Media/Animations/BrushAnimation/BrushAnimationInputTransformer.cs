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
                from = Brushes.Transparent;
                TraceSources.AnimationSource.Warn(
                    "The animation's 'From' value was null. Replacing it with a transparent brush.");
            }
            if (to == null)
            {
                to = Brushes.Transparent;
                TraceSources.AnimationSource.Warn(
                    "The animation's 'To' value was null. Replacing it with a transparent brush.");
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
            }

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
