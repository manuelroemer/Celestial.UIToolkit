using Celestial.UIToolkit.Converters;
using System;
using System.Globalization;
using System.Windows;
using Xunit;

namespace Celestial.UIToolkit.Tests.Converters
{

    public class BooleanToVisibilityConverterTests
    {

        [Fact]
        public void ConvertsBoolToVisibility()
        {
            var converter = new BooleanToVisibilityConverter();
            Assert.Equal(
                converter.TrueVisibility,
                converter.Convert(true, null, CultureInfo.CurrentCulture));
            Assert.Equal(
                converter.FalseVisibility,
                converter.Convert(false, null, CultureInfo.CurrentCulture));

            Assert.True(converter.ConvertBack(converter.TrueVisibility, null, CultureInfo.CurrentCulture));
            Assert.False(converter.ConvertBack(converter.FalseVisibility, null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ThrowsForSameVisibilityProperties()
        {
            var converter = new BooleanToVisibilityConverter(Visibility.Visible, Visibility.Visible);
            Assert.Throws<InvalidOperationException>(() =>
                converter.ConvertBack(Visibility.Visible, null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ThrowsForUnknownVisibility()
        {
            var converter = new BooleanToVisibilityConverter(Visibility.Visible, Visibility.Collapsed);
            Assert.Throws<InvalidOperationException>(() =>
                converter.ConvertBack(Visibility.Hidden, null, CultureInfo.CurrentCulture));
        }

    }

}
