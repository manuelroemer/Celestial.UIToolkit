using System;
using System.Windows;
using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Converters
{

    [TestClass]
    public class ThicknessSidePickerConverterTests
    {

        [TestMethod]
        public void ChangesAllSides()
        {
            var converter = new ThicknessSidePickerConverter()
            {
                Top = 5,
                Left = 5,
                Right = 5,
                Bottom = 5
            };

            var thickness = new Thickness(1, 2, 3, 4);
            var expectedThickness = new Thickness(5);

            Assert.AreEqual(
                expectedThickness,
                converter.Convert(thickness, null, null));
        }

        [TestMethod]
        public void KeepsAllSides()
        {
            var converter = new ThicknessSidePickerConverter();
            var thickness = new Thickness(1, 2, 3, 4);
            Assert.AreEqual(
                thickness,
                converter.Convert(thickness, null, null));
        }

    }

}
