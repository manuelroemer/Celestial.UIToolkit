using Celestial.UIToolkit.Extensions;
using Celestial.UIToolkit.Media.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class DoubleSegmentProvider : ISegmentLengthProvider
    {
        public double GetSegmentLength(object startValue, object currentValue)
        {
            return Math.Abs((double)currentValue - (double)startValue);
        }
    }

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
                frame.ResolvedKeyTime == frame.OriginalKeyTime));
        }

        [TestMethod]
        public void ResolvesPercentOnlyFrames()
        {
            var resolvedFrames = this.GetResolvedKeyFrames(
                KeyTime.FromPercent(0.10),
                KeyTime.FromPercent(0.00),
                KeyTime.FromPercent(0.65),
                KeyTime.FromPercent(0.05),
                KeyTime.FromPercent(0.20));

            // Each frame's ResolvedKeyTime must be x-% of the totalDuration.
            this.AssertSharedKeyFrameRules(resolvedFrames);
            foreach (var frame in resolvedFrames)
            {
                double expectedMSecs = _totalDuration.TotalMilliseconds * frame.OriginalKeyTime.Percent;
                Assert.AreEqual(expectedMSecs, frame.ResolvedKeyTime.TotalMilliseconds);
            }
        }

        [TestMethod]
        public void ResolvesUniformOnlyFrames()
        {
            var resolvedFrames = this.GetResolvedKeyFrames(
                KeyTime.Uniform, 
                KeyTime.Uniform, 
                KeyTime.Uniform,
                KeyTime.Uniform);

            this.AssertSharedKeyFrameRules(resolvedFrames);
            this.ValidateUniformKeyFrames(resolvedFrames);
        }

        [TestMethod]
        public void ResolvesSegmentedUniformFrames()
        {
            var resolvedFrames = this.GetResolvedKeyFrames(
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                KeyTime.Uniform,
                KeyTime.Uniform,
                KeyTime.Uniform,
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4)),
                KeyTime.Uniform,
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(8)));
            
            this.AssertSharedKeyFrameRules(resolvedFrames);
            this.ValidateUniformKeyFrames(resolvedFrames);
        }

        [TestMethod]
        public void ResolvesPacedOnlyFrames()
        {
            var resolvedFrames = this.GetResolvedKeyFrames(
                KeyTime.Paced,
                KeyTime.Paced,
                KeyTime.Paced,
                KeyTime.Paced);
            
            this.AssertSharedKeyFrameRules(resolvedFrames);
            this.ValidateUniformKeyFrames(resolvedFrames); // For paced only, the values should == Uniform values.
        }

        [TestMethod]
        public void ResolveSegmentedPacedFrames()
        {
            var resolvedFrames = this.GetResolvedKeyFrames(
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)),
                KeyTime.Paced,
                KeyTime.Paced,
                KeyTime.Uniform,
                KeyTime.Uniform,
                KeyTime.Paced,
                KeyTime.Paced,
                KeyTime.Paced,
                KeyTime.FromPercent(0.8),
                KeyTime.Paced);

            this.AssertSharedKeyFrameRules(resolvedFrames);
            // TODO: Add assertions that the key frames are really paced.
            //       I can't think of a good one right now.
            //       I did test it manually by comparing the results for many different inputs
            //       to a DoubleAnimationUsingKeyFrame's internal values, but that
            //       should not replace an automatic unit test.
        }

        private IReadOnlyList<ResolvedKeyFrame<DoubleKeyFrame>> GetResolvedKeyFrames(params KeyTime[] keyTimes)
        {
            var collection = this.BuildDoubleKeyFrameCollection(keyTimes);
            return KeyFrameResolver<DoubleKeyFrame>.ResolveKeyFrames(collection, _totalDuration, new DoubleSegmentProvider());
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

        private void AssertSharedKeyFrameRules(IEnumerable<ResolvedKeyFrame<DoubleKeyFrame>> frames)
        {
            this.AssertThatEachFrameIsResolved(frames);
            this.AssertThatFramesAreInOrder(frames);
        }

        private void AssertThatEachFrameIsResolved(IEnumerable<ResolvedKeyFrame<DoubleKeyFrame>> frames)
        {
            Assert.IsTrue(frames.All(frame => frame.IsResolved));
        }

        private void AssertThatFramesAreInOrder(IEnumerable<ResolvedKeyFrame<DoubleKeyFrame>> frames)
        {
            ResolvedKeyFrame<DoubleKeyFrame> previousFrame = null;
            foreach (var frame in frames)
            {
                if (previousFrame != null)
                {
                    Assert.IsTrue(frame.ResolvedKeyTime >= previousFrame.ResolvedKeyTime);
                }
            }
        }

        private void ValidateUniformKeyFrames(IEnumerable<ResolvedKeyFrame<DoubleKeyFrame>> frames)
        {
            // Grab each uniform segment and check if the increments are valid.
            var segments = frames.ToArray().GetGroupSegments(frame => 
                frame.OriginalKeyTime.Type == KeyTimeType.Uniform);

            foreach (var segment in segments)
            {
                TimeSpan? previousTimeStep = null;
                int i = segment.Offset == 0 ? 1 : segment.Offset;
                int upper = segment.Offset + segment.Count;
                if (!segment.IncludesLastArrayItem()) upper++;

                for (; i < upper; i++)
                {
                    var currentFrame = frames.ElementAt(i);
                    var previousFrame = frames.ElementAt(i - 1);
                    var currentTimeStep = currentFrame.ResolvedKeyTime - previousFrame.ResolvedKeyTime;

                    if (previousTimeStep != null)
                    {
                        Assert.AreEqual(previousTimeStep, currentTimeStep);
                    }
                    previousTimeStep = currentTimeStep;
                }
            }
        }

    }

}
