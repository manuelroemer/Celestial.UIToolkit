﻿namespace Celestial.UIToolkit.Media.Animations
{

    /// <summary>
    /// Used by the <see cref="KeyFrameResolver{TKeyFrame}"/> to calculate
    /// the distance between two elements for a paced animation.
    /// </summary>
    internal interface ISegmentLengthProvider
    {
        double GetDistanceBetween(object from, object to);
    }

}
