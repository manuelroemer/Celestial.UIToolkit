using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Celestial.UIToolkit.Converters;
using System.Windows.Media;
using System.Globalization;

namespace Celestial.UIToolkit.Tests.Converters
{

    [TestClass]
    public class ColorToSolidColorBrushConverterTests
    {

        [TestMethod]
        public void TestConversions()
        {
            var converter = new ColorToSolidColorBrushConverter();

            // Very pretty, I know..
            Assert.AreEqual(
                Colors.Black,
                converter.ConvertBack(
                    converter.Convert(
                        Colors.Black,
                        null, 
                        CultureInfo.CurrentCulture), 
                    null, CultureInfo.CurrentCulture));
        }

    }
}
