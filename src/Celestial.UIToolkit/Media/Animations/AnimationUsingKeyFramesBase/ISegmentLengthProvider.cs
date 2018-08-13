namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Used by the <see cref="KeyFrameResolver"/> to calculate
    /// the distance between two elements for a paced animation.
    /// </summary>
    internal interface ISegmentLengthProvider
    {
        double GetSegmentLength(object from, object to);
    }

}
