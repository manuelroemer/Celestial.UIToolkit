using Celestial.UIToolkit.Xaml;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Xunit;

namespace Celestial.UIToolkit.Tests.Xaml
{

    public class MultiStyleExtensionTests
    {

        [Fact]
        public void ReturnsEmptyStyleForNoStyleKeyParts()
        {
            var multiStyleExtension = new MultiStyleExtension(null);
            var style = multiStyleExtension.ProvideValue(null);
            Assert.NotNull(style);
            Assert.IsType<Style>(style);
        }

    }


    // We can't really test the Extension itself, since it relies on WPF resource retrieval.
    // That is difficult in a testing project, since we don't have a ResourceDicationary available.
    // We can, however, test the merging logic behind the extension.
    public class StyleMergingTests
    {

        [Theory]
        [InlineData(null, typeof(Button))]
        [InlineData(typeof(Button), null)]
        public void CopiesTargetTypeIfOneIsNull(Type targetType, Type srcType)
        {
            var target = targetType != null ? new Style(targetType) : new Style(); // Style will throw ArgumentNullEx
            var src =    srcType != null    ? new Style(srcType)    : new Style(); // if the targetType param is null.
            var expectedType = targetType ?? srcType;

            target.MergeWith(src);
            Assert.Equal(expectedType, target.TargetType);
        }
        
        [Fact]
        public void ThrowsForConflictingTargetTypes()
        {
            var target = new Style(typeof(Button));
            var src = new Style(typeof(ComboBox));

            var ex = Record.Exception(() => target.MergeWith(src));
            Assert.IsAssignableFrom<Exception>(ex);
        }

        [Fact]
        public void ChoosesMoreSpecificTargetType()
        {
            var target = new Style(typeof(ButtonBase));
            var src = new Style(typeof(Button));

            target.MergeWith(src);
            Assert.Equal(typeof(Button), target.TargetType);
        }

        [Fact]
        public void MergesSetters()
        {
            var target = new Style();
            var src = new Style();
            target.Setters.Add(new Setter(DependencyProperties.ThicknessProperty, new Thickness()));
            src.Setters.Add(new Setter(DependencyProperties.DoubleProperty, 1d));
            src.Setters.Add(new Setter(DependencyProperties.PointProperty, new Point()));

            target.MergeWith(src);
            Assert.All(src.Setters, setter => target.Setters.Contains(setter));
            Assert.True(target.Setters.Count > src.Setters.Count);
        }

        [Fact]
        public void MergesTriggers()
        {
            var target = new Style();
            var src = new Style();
            target.Triggers.Add(new Trigger());
            src.Triggers.Add(new Trigger());
            src.Triggers.Add(new Trigger());

            target.MergeWith(src);
            Assert.All(src.Triggers, trigger => target.Triggers.Contains(trigger));
            Assert.True(target.Triggers.Count > src.Triggers.Count);
        }

        [Fact]
        public void MergesResources()
        {
            var target = new Style();
            var src = new Style();
            src.Resources["Res1"] = 1;
            src.Resources["Res2"] = 2;
            target.Resources["Res1"] = 0; // Supposed to be overwritten.

            target.MergeWith(src);
            Assert.Equal(2, target.Resources.Count);
            Assert.Equal(1, target.Resources["Res1"]);
        }

        [Fact]
        public void MergesBaseStyles()
        {
            var target = new Style();
            var srcBase = new Style(typeof(Button));
            var src = new Style() { BasedOn = srcBase };
            srcBase.Resources["Resource"] = 1;
            srcBase.Setters.Add(new Setter());
            srcBase.Triggers.Add(new Trigger());

            target.MergeWith(src);
            Assert.Equal(1, target.Resources["Resource"]);
            Assert.Single(target.Setters);
            Assert.Single(target.Triggers);
        }

    }

}
