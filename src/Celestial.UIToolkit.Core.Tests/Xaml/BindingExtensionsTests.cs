using Celestial.UIToolkit.Xaml;
using System.Windows.Data;
using Xunit;

namespace Celestial.UIToolkit.Tests.Xaml
{

    public class BindingExtensionsTests
    {

        [Fact]
        public void SelfBindingExtensionHasCorrectRelativeSource()
        {
            var binding = new SelfBindingExtension();
            Assert.Equal(RelativeSource.Self, binding.RelativeSource);
        }

        [Fact]
        public void TemplatedParentBindingExtensionHasCorrectRelativeSource()
        {
            var binding = new TemplatedParentBindingExtension();
            Assert.Equal(RelativeSource.TemplatedParent, binding.RelativeSource);
        }

    }

}
