using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;
using Celestial.UIToolkit.Media.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    [TestClass]
    public class KeyFrameResolverTests
    {

        TimeSpan _totalDuration = TimeSpan.FromSeconds(10);

        [TestMethod]
        public void ResolvesTimeSpanOnlyFrames()
        {
            // Create the KeyFrames in disorder, to see how the resolver handles that.
            var resolvedFrames = this.GetResolvedKeyFrames(
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.0)),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.0)),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5)),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4.5)),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0)));

            // The final frames must all have their original KeyTime - TimeSpan.
            this.AssertSharedKeyFrameRules(resolvedFrames);
            Assert.IsTrue(resolvedFrames.All(frame => 
                frame.ResolvedKeyTime == frame.KeyTime));
        }

        [TestMethod]
        public void ResolvesPercentOnlyFrames()
        {
            var collection = this.BuildDoubleKeyFrameCollection(
                KeyTime.FromPercent(0.10),
                KeyTime.FromPercent(0.00),
                KeyTime.FromPercent(0.65),
                KeyTime.FromPercent(0.05),
                KeyTime.FromPercent(0.20));
            var resolvedFrames = new KeyFrameResolver().ResolveKeyFrames(collection, _totalDuration);

            // Each frame's ResolvedKeyTime must be x-% of the totalDuration.
            this.AssertSharedKeyFrameRules(resolvedFrames);
            foreach (var frame in resolvedFrames)
            {
                double expectedMSecs = _totalDuration.TotalMilliseconds * frame.KeyTime.Percent;
                Assert.AreEqual(expectedMSecs, frame.ResolvedKeyTime.TotalMilliseconds);
            }
        }

        [TestMethod]
        public void ResolvesUniformOnlyFrames()
        {
            var collection = this.BuildDoubleKeyFrameCollection(
                KeyTime.Uniform, KeyTime.Uniform, KeyTime.Uniform, KeyTime.Uniform);
            var resolvedFrames = new KeyFrameResolver().ResolveKeyFrames(collection, _totalDuration);

            // Each frame must have the same value.
            this.AssertSharedKeyFrameRules(resolvedFrames);
            for (int i = 1; i < resolvedFrames.Length; i++)
            {
                double msecsStep = _totalDuration.TotalMilliseconds / resolvedFrames.Length;
                double currentDiff = (resolvedFrames[i].ResolvedKeyTime - 
                                      resolvedFrames[i - 1].ResolvedKeyTime)
                                     .TotalMilliseconds;
                Assert.AreEqual(
                    (resolvedFrames[i].ResolvedKeyTime - resolvedFrames[i - 1].ResolvedKeyTime).TotalMilliseconds,
                    msecsStep);
            }
        }

        [TestMethod]
        public void ResolvesSegmentedUniformFrames()
        {
            var collection = this.BuildDoubleKeyFrameCollection(
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                KeyTime.Uniform,
                KeyTime.Uniform,
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5)),
                KeyTime.Uniform,
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8)));
            var resolvedFrames = new KeyFrameResolver().ResolveKeyFrames(collection, _totalDuration);
            
            this.AssertSharedKeyFrameRules(resolvedFrames);
        }

        private ResolvedKeyFrame[] GetResolvedKeyFrames(params KeyTime[] keyTimes)
        {
            var collection = this.BuildDoubleKeyFrameCollection(keyTimes);
            return new KeyFrameResolver().ResolveKeyFrames(collection, _totalDuration);
        }

        private DoubleKeyFrameCollection BuildDoubleKeyFrameCollection(
            params KeyTime[] keyTimes)
        {
            var collection = new DoubleKeyFrameCollection();
            for (int i = 0; i < keyTimes.Length; i++)
            {
                collection.Add(new LinearDoubleKeyFrame(i, keyTimes[i]));
            }
            return collection;
        }

        private void AssertSharedKeyFrameRules(IEnumerable<ResolvedKeyFrame> frames)
        {
            this.AssertThatEachFrameIsResolved(frames);
            this.AssertThatFramesAreInOrder(frames);
        }

        private void AssertThatEachFrameIsResolved(IEnumerable<ResolvedKeyFrame> frames)
        {
            Assert.IsTrue(frames.All(frame => frame.IsResolved));
        }

        private void AssertThatFramesAreInOrder(IEnumerable<ResolvedKeyFrame> frames)
        {
            ResolvedKeyFrame previousFrame = null;
            foreach (var frame in frames)
            {
                if (previousFrame != null)
                {
                    Assert.IsTrue(frame.ResolvedKeyTime >= previousFrame.ResolvedKeyTime);
                }
            }
        }

    }

}
