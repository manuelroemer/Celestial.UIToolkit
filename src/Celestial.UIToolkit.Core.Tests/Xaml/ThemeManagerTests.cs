using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Celestial.UIToolkit.Xaml;
using Xunit;

namespace Celestial.UIToolkit.Core.Tests.Xaml
{

    public class ThemeManagerTests
    {

        [Fact]
        public void ReturnsCachedInstance()
        {
            var manager1 = ThemeManager.Current;
            var manager2 = ThemeManager.Current;
            Assert.Same(manager1, manager2);
        }

        [Fact]
        public void RaisesThemeChangedEvent()
        {
            Assert.Raises<ThemeChangedEventArgs>(
                (handler) => ThemeManager.Current.ThemeChanged += handler,
                (handler) => ThemeManager.Current.ThemeChanged -= handler,
                () => ThemeManager.Current.ChangeTheme("Theme")
            );
        }

    }

}
