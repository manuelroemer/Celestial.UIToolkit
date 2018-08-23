using System;
using System.Windows;
using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Converters
{
    [TestClass]
    public class NegateValueConverterTests
    {

        private NegateValueConverter _converter = new NegateValueConverter();

        [TestMethod]
        public void ThrowsForUnsupportedType()
        {
            object unconvertableObj = new Exception(); // Can be anything, really
            Assert.ThrowsException<NotSupportedException>(() =>
                _converter.Convert(unconvertableObj, typeof(object), null, null));
        }

        [TestMethod]
        public void ConvertEqualsConvertBack()
        {
            const int value = 100;

            Assert.AreEqual(
                _converter.Convert(value, typeof(object), null, null),
                _converter.ConvertBack(value, typeof(object), null, null));
        }

        [TestMethod]
        public void NegatesConvertible()
        {
            // Test with one IConvertible object, since there are just too many.
            decimal convertible = 123.456M;
            TestNegation(convertible, -1 * convertible);
        }

        [TestMethod]
        public void NegatesThickness()
        {
            TestNegation(
                new Thickness(1, 2, 3, 4), 
                new Thickness(-1, -2, -3, -4));
        }

        [TestMethod]
        public void NegatesCornerRadius()
        {
            TestNegation(
                new CornerRadius(1, 2, 3, 4), 
                new CornerRadius(-1, -2, -3, -4));
        }
        
        [TestMethod]
        public void NegatesPoint()
        {
            TestNegation(new Point(3d, 3d), new Point(-3d, -3d));
        }

        private void TestNegation(object obj, object negatedObj)
        {
            object conversionResult = _converter.Convert(obj, typeof(object), null, null);
            Assert.AreEqual(negatedObj, conversionResult);
        }
        
    }
}
