using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using System.Collections.Generic;
using Xunit;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    public class FromToByAnimationTests
    {
        
        [Theory]
        [MemberData(
            nameof(FromToByAnimationTestsDataSources.AllAnimationTypes), 
            MemberType = typeof(FromToByAnimationTestsDataSources)
        )]
        public void ReturnsExpectedValueWhenStarted(FromToByAnimationData animationData)
        {
            var animation = new FromToByDoubleAnimation(
                animationData.From,
                animationData.To,
                animationData.By
            );
            var clock = ControllableAnimationClock.NewStarted();

            double result = animation.GetCurrentValue(
                animationData.DefaultOrigin,
                animationData.DefaultDestination,
                clock
            );
            double expectedResult = animationData.GetExpectedValueForProgress(clock.CurrentProgress.Value);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(
            nameof(FromToByAnimationTestsDataSources.AllAnimationTypes),
            MemberType = typeof(FromToByAnimationTestsDataSources)
        )]
        public void ReturnsExpectedValueWhenFinished(FromToByAnimationData animationData)
        {
            var animation = new FromToByDoubleAnimation(
                animationData.From,
                animationData.To,
                animationData.By
            );
            var clock = ControllableAnimationClock.NewFinished();

            double result = animation.GetCurrentValue(
                animationData.DefaultOrigin,
                animationData.DefaultDestination,
                clock
            );
            double expectedResult = animationData.GetExpectedValueForProgress(clock.CurrentProgress.Value);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(
            nameof(FromToByAnimationTestsDataSources.AllAnimationTypes),
            MemberType = typeof(FromToByAnimationTestsDataSources)
        )]
        public void ReturnsExpectedIntermediaryValues(FromToByAnimationData animationData)
        {
            var animation = new FromToByDoubleAnimation(
                animationData.From,
                animationData.To,
                animationData.By
            );
            var clock = new ControllableAnimationClock();

            // We want to test for intermediary values, meaning that we loop the progress from
            // 0 to 1 in small steps.
            // In each step, we assert that the animation returns the correct value.

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;
                
                double result = animation.GetCurrentValue(
                    animationData.DefaultOrigin,
                    animationData.DefaultDestination,
                    clock
                );
                double expectedResult = animationData.GetExpectedValueForProgress(clock.CurrentProgress.Value);

                Assert.Equal(expectedResult, result);
            }
        }
        


        public static class FromToByAnimationTestsDataSources
        {

            /// <summary>
            ///     Returns a set of <see cref="FromToByAnimationData"/> objects which cover
            ///     all different animation types.
            ///     This includes:
            ///     - Automatic
            ///     - From
            ///     - To
            ///     - By
            ///     - FromTo
            ///     - FromBy
            /// </summary>
            public static TheoryData<FromToByAnimationData> AllAnimationTypes
            {
                get
                {
                    const double defaultOrigin = 25d;
                    const double defaultDestination = 200d;
                    const double from = 50d;
                    const double to = 100d;
                    const double by = 340d;

                    return new TheoryData<FromToByAnimationData>()
                    {
                        new AutomaticAnimationData(defaultOrigin, defaultDestination),
                        new FromAnimationData(defaultOrigin, defaultDestination, from),
                        new ToAnimationData(defaultOrigin, defaultDestination, to),
                        new ByAnimationData(defaultOrigin, defaultDestination, by),
                        new FromToAnimationData(defaultOrigin, defaultDestination, from, to),
                        new FromByAnimationData(defaultOrigin, defaultDestination, from, by)
                    };
                }
            }

        }

        /// <summary>
        /// Represents a data item for the <see cref="FromToByAnimationTests"/>.
        /// </summary>
        public abstract class FromToByAnimationData
        {
            
            public double DefaultOrigin { get; protected set; }

            public double DefaultDestination { get; protected set; }

            public double? From { get; protected set; }

            public double? To { get; protected set; }

            public double? By { get; protected set; }
            
            public FromToByAnimationData(double defaultOrigin, double defaultDestination)
            {
                DefaultOrigin = defaultOrigin;
                DefaultDestination = defaultDestination;
            }

            /// <summary>
            ///     Calculates the expected value of an animation at the specified 
            ///     <paramref name="progress"/>.
            /// </summary>
            /// <param name="progress">
            ///     The current progress of the animation as a double value between 0 and 1.
            /// </param>
            /// <returns>
            ///     The value which the animation should return when calculating the current value
            ///     with this class' data.
            /// </returns>
            public abstract double GetExpectedValueForProgress(double progress);

        }

        public sealed class AutomaticAnimationData : FromToByAnimationData
        {

            public AutomaticAnimationData(double defaultOrigin, double defaultDestination)
                : base(defaultOrigin, defaultDestination) { }

            public override double GetExpectedValueForProgress(double progress)
            {
                return DefaultOrigin + (DefaultDestination - DefaultOrigin) * progress;
            }

        }

        public sealed class FromAnimationData : FromToByAnimationData
        {
            
            public FromAnimationData(double defaultOrigin, double defaultDestination, double from)
                : base(defaultOrigin, defaultDestination)
            {
                From = from;
            }

            public override double GetExpectedValueForProgress(double progress)
            {
                double from = From.Value;
                return from + (DefaultDestination - from) * progress;
            }

        }

        public sealed class ToAnimationData : FromToByAnimationData
        {

            public ToAnimationData(double defaultOrigin, double defaultDestination, double to)
                : base(defaultOrigin, defaultDestination)
            {
                To = to;
            }

            public override double GetExpectedValueForProgress(double progress)
            {
                return DefaultOrigin + (To.Value - DefaultOrigin) * progress;
            }

        }

        public sealed class ByAnimationData : FromToByAnimationData
        {

            public ByAnimationData(double defaultOrigin, double defaultDestination, double by)
                : base(defaultOrigin, defaultDestination)
            {
                By = by;
            }

            public override double GetExpectedValueForProgress(double progress)
            {
                return DefaultOrigin + By.Value * progress;
            }

        }

        public sealed class FromToAnimationData : FromToByAnimationData
        {

            public FromToAnimationData(double defaultOrigin, double defaultDestination, double from, double to)
                : base(defaultOrigin, defaultDestination)
            {
                From = from;
                To = to;
            }

            public override double GetExpectedValueForProgress(double progress)
            {
                double from = From.Value;
                double to = To.Value;
                return from + (to - from) * progress;
            }

        }

        public sealed class FromByAnimationData : FromToByAnimationData
        {

            public FromByAnimationData(double defaultOrigin, double defaultDestination, double from, double by)
                : base(defaultOrigin, defaultDestination)
            {
                From = from;
                By = by;
            }

            public override double GetExpectedValueForProgress(double progress)
            {
                return From.Value + By.Value * progress;
            }

        }

    }

}
