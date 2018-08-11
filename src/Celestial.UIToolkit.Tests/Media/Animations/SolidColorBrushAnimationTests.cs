using Celestial.UIToolkit.Media.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    [TestClass]
    public class SolidColorBrushAnimationTests
    {
        
        [TestMethod]
        public void ThrowsForNonSolidBrush()
        {
            var animation = new SolidColorBrushAnimation();

            Assert.ThrowsException<InvalidOperationException>(() =>
                animation.GetCurrentValue(
                    Brushes.Red, new LinearGradientBrush(), animation.CreateClock()));
            Assert.ThrowsException<InvalidOperationException>(() =>
                animation.GetCurrentValue(
                    new LinearGradientBrush(), Brushes.Red, animation.CreateClock()));
        }

    }

}
