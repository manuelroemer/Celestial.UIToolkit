using Celestial.UIToolkit.Tests.Media.Animations.Mocks;
using System.Windows.Media.Animation;
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
        
        [Theory]
        [MemberData(
            nameof(FromToByAnimationTestsDataSources.AllAnimationTypes),
            MemberType = typeof(FromToByAnimationTestsDataSources)
        )]
        public void AnimationRespectsIsAdditive(FromToByAnimationData animationData)
        {
            var animation = new FromToByDoubleAnimation(
                animationData.From,
                animationData.To,
                animationData.By
            );
            var clock = new ControllableAnimationClock();
            animation.IsAdditive = true;

            for (double progress = 0d; progress <= 1d; progress += 0.01)
            {
                clock.CurrentProgress = progress;

                double result = animation.GetCurrentValue(
                    animationData.DefaultOrigin,
                    animationData.DefaultDestination,
                    clock
                );
                double expectedResult = animationData.GetExpectedValueForProgress(clock.CurrentProgress.Value);

                // If the animation supports IsAdditive, we add the DefaultOrigin value to the expected result.
                // IsAdditive only gets supported, if both From and To/By are set.
                // See https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.animation.doubleanimation.isadditive?view=netframework-4.7.2
                // for details.
                if (animationData.From.HasValue &&
                    (animationData.To.HasValue || animationData.By.HasValue))
                {
                    expectedResult += animationData.DefaultOrigin;
                }

                Assert.Equal(expectedResult, result);
            }
        }
        
        [Theory]
        [MemberData(
            nameof(FromToByAnimationTestsDataSources.AllAnimationTypes),
            MemberType = typeof(FromToByAnimationTestsDataSources)
        )]
        public void AnimationRespectsIsCumulative(FromToByAnimationData animationData)
        {
            const int repeatCount = 10;

            var animation = new FromToByDoubleAnimation(
                animationData.From,
                animationData.To,
                animationData.By
            );
            var clock = new ControllableAnimationClock();
            animation.IsCumulative = true;
            animation.RepeatBehavior = new RepeatBehavior(repeatCount);

            // IsCumulative only works, if the animation repeats.
            // In each iteration, the previously calculated value gets set as the starting value
            // of the next iteration.
            // This basically means that an animation doesn't get reset after each repetition.
            //
            // WPF's CurrentIteration always starts from 1, not 0!
            for (int iteration = 1; iteration <= repeatCount; iteration++)
            {
                for (double progress = 0d; progress <= 1d; progress += 0.01)
                {
                    clock.CurrentProgress = progress;
                    clock.CurrentIteration = iteration;

                    double result = animation.GetCurrentValue(
                        animationData.DefaultOrigin,
                        animationData.DefaultDestination,
                        clock
                    );
                    double expectedResult = animationData.GetExpectedValueForProgress(clock.CurrentProgress.Value);
                    
                    // Calculate the value which should be added on top of the expected result for the current
                    // iteration.
                    double toFromDiff = animationData.ActualTo - animationData.ActualFrom;
                    double accumulatedValue = toFromDiff * (iteration - 1);
                    expectedResult += accumulatedValue;
                    
                    Assert.Equal(expectedResult, result);
                }
            }
        }


        public static class FromToByAnimationTestsDataSources
        {

            const double DefaultOrigin = 25d;
            const double DefaultDestination = 200d;
            const double From = 50d;
            const double To = 100d;
            const double By = 340d;

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
                    return new TheoryData<FromToByAnimationData>()
                    {
                        new AutomaticAnimationData(DefaultOrigin, DefaultDestination),
                        new FromAnimationData(DefaultOrigin, DefaultDestination, From),
                        new ToAnimationData(DefaultOrigin, DefaultDestination, To),
                        new ByAnimationData(DefaultOrigin, DefaultDestination, By),
                        new FromToAnimationData(DefaultOrigin, DefaultDestination, From, To),
                        new FromByAnimationData(DefaultOrigin, DefaultDestination, From, By)
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

            public double ActualFrom
            {
                get
                {
                    return From ?? DefaultOrigin;
                }
            }

            public double ActualTo
            {
                get
                {
                    if (To.HasValue)
                        return To.Value;
                    else if (By.HasValue)
                        return ActualFrom + By.Value;
                    else
                        return DefaultDestination;
                }
            }
            
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
