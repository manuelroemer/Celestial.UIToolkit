using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Celestial.UIToolkit.Tests.Converters
{

    [TestClass]
    public class BooleanToVisibilityConverterTests
    {

        [TestMethod]
        public void CorrectConversions()
        {
            var converter = new BooleanToVisibilityConverter();
            Assert.AreEqual(
                converter.TrueVisibility, 
                converter.Convert(true, null, CultureInfo.CurrentCulture));
            Assert.AreEqual(
                converter.FalseVisibility,
                converter.Convert(false, null, CultureInfo.CurrentCulture));

            Assert.IsTrue(converter.ConvertBack(converter.TrueVisibility, null, CultureInfo.CurrentCulture));
            Assert.IsFalse(converter.ConvertBack(converter.FalseVisibility, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ThrowsForSameVisibilityProperties()
        {
            var converter = new BooleanToVisibilityConverter(Visibility.Visible, Visibility.Visible);
            Assert.ThrowsException<InvalidOperationException>(() => 
                converter.ConvertBack(Visibility.Visible, null, CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ThrowsForUnknownVisibility()
        {
            var converter = new BooleanToVisibilityConverter(Visibility.Visible, Visibility.Collapsed);
            Assert.ThrowsException<InvalidOperationException>(() =>
                converter.ConvertBack(Visibility.Hidden, null, CultureInfo.CurrentCulture));
        }

    }

}
