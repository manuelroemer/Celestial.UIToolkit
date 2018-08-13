using Celestial.UIToolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celestial.UIToolkit.Media.Animations
{
    
    internal static class ResolvedKeyFrameCollectionExtensions
    {
        
        /// <summary>
        /// Goes through the sequence of resolved key frames and finds the frame,
        /// which is supposed to be used for an animation at the specified time.
        /// </summary>
        /// <param name="resolvedKeyFrames">
        /// A sequence of <see cref="ResolvedKeyFrame"/> instances.
        /// </param>
        /// <param name="currentTime">The animation's current time.</param>
        /// <returns>
        /// The index in the <paramref name="resolvedKeyFrames"/> sequence, which
        /// points to the key frame which is supposed to be played.
        /// </returns>
        public static int FindCurrentKeyFrameIndex(
            this IEnumerable<ResolvedKeyFrame> resolvedKeyFrames, TimeSpan currentTime)
        {
            if (resolvedKeyFrames == null) throw new ArgumentNullException(nameof(resolvedKeyFrames));
            if (resolvedKeyFrames.Count() == 0)
            {
                throw new InvalidOperationException(
                    "The resolved key frames sequence did not contain any frames. " +
                    "Unable to find a correct frame for the specified time.");
            }

            int frameCount = resolvedKeyFrames.Count();

            // Find the first frame whose time is >= currentTime, but
            // choose the last frame of a set which have the same time.
            for (int i = 0; i < frameCount; i++)
            {
                var currentFrame = resolvedKeyFrames.ElementAt(i);
                var nextFrame = resolvedKeyFrames.ElementAfterOrDefault(i);
                bool isFrameAfterCurrentTime = currentFrame.ResolvedKeyTime >= currentTime;
                bool nextFrameEqualsCurrent = nextFrame != null &&
                                              nextFrame.ResolvedKeyTime == currentFrame.ResolvedKeyTime;

                if (isFrameAfterCurrentTime && !nextFrameEqualsCurrent)
                    return i;
            }
            return frameCount - 1;
        }


    }

}
