using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using Celestial.UIToolkit.Media.Animations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Celestial.UIToolkit.Tests.Media.Animations
{

    [TestClass]
    public class BrushAnimationTests
    {
        
        [TestMethod]
        public void ThrowsForNullOrigin()
        {
            var animation = new TestableBrushAnimation()
            {
                From = null,
                To = Brushes.Transparent
            };

            Assert.ThrowsException<InvalidOperationException>(() =>
                animation.GetCurrentValue(null, null, animation.CreateClock()));
        }

        [TestMethod]
        public void ThrowsForNullDestination()
        {
            var animation = new TestableBrushAnimation()
            {
                From = Brushes.Transparent,
                To = null
            };

            Assert.ThrowsException<InvalidOperationException>(() =>
                animation.GetCurrentValue(null, null, animation.CreateClock()));
        }

        [TestMethod]
        public void ThrowsForDifferentBrushTypes()
        {
            var animation = new TestableBrushAnimation()
            {
                From = new SolidColorBrush(),
                To = new LinearGradientBrush()
            };

            Assert.ThrowsException<InvalidOperationException>(() =>
                animation.GetCurrentValue(null, null, animation.CreateClock()));
        }

        [TestMethod]
        public void ThrowsForDifferentTransforms()
        {
            var animation = new TestableBrushAnimation()
            {
                From = new SolidColorBrush(),
                To = new SolidColorBrush()
                {
                    Transform = new ScaleTransform()
                }
            };

            Assert.ThrowsException<InvalidOperationException>(() =>
                animation.GetCurrentValue(null, null, animation.CreateClock()));
        }

        [TestMethod]
        public void UsesFromPropertyInsteadOfOriginParam()
        {
            Brush from = Brushes.Red;
            var animation = new TestableBrushAnimation()
            {
                From = from,
                To = new SolidColorBrush(),
                ReturnOrigin = true
            };

            Assert.AreEqual(
                from,
                animation.GetCurrentValue(Brushes.Green, null, animation.CreateClock()));
        }

        [TestMethod]
        public void UsesToPropertyInsteadOfDestinationParam()
        {
            Brush to = Brushes.Red;
            var animation = new TestableBrushAnimation()
            {
                From = new SolidColorBrush(),
                To = to,
                ReturnOrigin = false
            };

            Assert.AreEqual(
                to,
                animation.GetCurrentValue(null, Brushes.Green, animation.CreateClock()));
        }

        private sealed class TestableBrushAnimation : BrushAnimation
        {

            /// <summary>
            /// If true, returns origin in GetCurrentBrush(..).
            /// Otherwise, destination.
            /// </summary>
            public bool ReturnOrigin { get; set; } = true;

            protected override Freezable CreateInstanceCore() => new TestableBrushAnimation();

            protected override Brush GetCurrentBrush(Brush origin, Brush destination, AnimationClock animationClock)
            {
                if (this.ReturnOrigin) return origin;
                return destination;
            }
            
        }

    }

}
