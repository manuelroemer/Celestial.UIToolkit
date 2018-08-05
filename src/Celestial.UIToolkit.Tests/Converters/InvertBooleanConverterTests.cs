using Celestial.UIToolkit.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Converters
{
    [TestClass]
    public class InvertBooleanConverterTests
    {
        
        [TestMethod]
        public void InvertsValues()
        {
            Assert.IsFalse(InvertBooleanConverter.Default.Convert(true, null, null));
            Assert.IsTrue(InvertBooleanConverter.Default.Convert(false, null, null));
            Assert.IsFalse(InvertBooleanConverter.Default.ConvertBack(true, null, null));
            Assert.IsTrue(InvertBooleanConverter.Default.ConvertBack(false, null, null));
        }
        
    }
}
