using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Celestial.UIToolkit.Tests.Converters
{

    // These tests also cover the NullToVisibilityConverter.

    [TestClass]
    public class StringToVisibilityConverterTests
    {

        [TestMethod]
        public void ConvertsNullValues()
        {
            var converter = new StringToVisibilityConverter();
            Assert.AreEqual(
                converter.NullVisibility,
                converter.Convert(null, null, CultureInfo.CurrentCulture));
            Assert.AreEqual(
                converter.NotNullVisibility,
                converter.Convert(new object(), null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertsStringValues()
        {
            var converter = new StringToVisibilityConverter();
            converter.IncludeWhiteSpace = false;

            Assert.AreEqual(
                converter.NullVisibility,
                converter.Convert("", null, CultureInfo.CurrentCulture));
            Assert.AreEqual(
                converter.NotNullVisibility,
                converter.Convert("Not empty", null, CultureInfo.CurrentCulture));

            converter.IncludeWhiteSpace = true;
            Assert.AreEqual(
                converter.NullVisibility,
                converter.Convert("   \t", null, CultureInfo.CurrentCulture));
        }

    }
}
