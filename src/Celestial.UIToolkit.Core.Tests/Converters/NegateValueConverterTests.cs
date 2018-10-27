using Celestial.UIToolkit.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class NegateValueConverterTests
    {

        [Fact]
        public void ConvertEqualsConvertBack()
        {
            var converter = new NegateValueConverter();
            int value = -123;
            Assert.Equal(
                converter.Convert(value, null, null, null),
                converter.ConvertBack(value, null, null, null));
        }

        [Fact]
        public void ConvertsBoolean()
        {
            var converter = new NegateValueConverter();
            var value = false;

            Assert.Equal(true, converter.Convert(value, null, null, null));
        }

        [Theory]
        [InlineData("-123", "123")]
        [InlineData(1d, -1d)]
        [InlineData(true, false)]
        [InlineData(1239f, -1239f)]
        public void ConvertsIConvertible(IConvertible value, IConvertible negated)
        {
            var converter = new NegateValueConverter();
            Assert.Equal(value, converter.Convert(negated, null, null, null));
        }

        [Fact]
        public void ConvertsThickness()
        {
            var converter = new NegateValueConverter();
            var value = new Thickness(3);
            var negated = (Thickness)converter.Convert(value, null, null, null);

            Assert.True(negated.Top == negated.Bottom &&
                        negated.Top == negated.Left &&
                        negated.Top == negated.Right &&
                        negated.Top == -3);
        }

        [Fact]
        public void ConvertsCornerRadius()
        {
            var converter = new NegateValueConverter();
            var value = new CornerRadius(3);
            var negated = (CornerRadius)converter.Convert(value, null, null, null);

            Assert.True(negated.BottomLeft == negated.TopLeft &&
                        negated.BottomLeft == negated.TopRight &&
                        negated.BottomLeft == negated.BottomRight &&
                        negated.BottomLeft == -3);
        }

        [Fact]
        public void ConvertsPoint()
        {
            var converter = new NegateValueConverter();
            var value = new Point(1, 2);
            var negated = (Point)converter.Convert(value, null, null, null);

            Assert.True(negated.X == -1 && negated.Y == -2);
        }

        [Fact]
        public void ThrowsNotSupportedExceptionForUnsupportedType()
        {
            var converter = new NegateValueConverter();
            Assert.Throws<NotSupportedException>(() =>
            converter.Convert(new NegateValueConverterTests(), null, null, null));
        }

    }

}
