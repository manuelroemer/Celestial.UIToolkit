using Celestial.UIToolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Celestial.UIToolkit.Tests.Controls.NavigationViewTests
{

    public class DisplayModeTests
    {

        [WpfFact]
        public void FiresDisplayModeChangedEvent()
        {
            var navView = new NavigationView();
            bool eventFired = false;
            navView.DisplayModeChanged += DisplayMode_Changed;

            navView.DisplayMode = NavigationViewDisplayMode.Compact;
            Assert.True(eventFired);

            void DisplayMode_Changed(object sender, NavigationViewDisplayModeChangedEventArgs e)
            {
                navView.DisplayModeChanged -= DisplayMode_Changed;
                eventFired = true;
            }
        }

        [WpfFact]
        public void DisplayModeChangedCarriesOldAndNewValues()
        {
            var navView = new NavigationView();
            NavigationViewDisplayModeChangedEventArgs eventArgs = null;
            navView.DisplayModeChanged += DisplayMode_Changed;

            navView.DisplayMode = NavigationViewDisplayMode.Expanded;

            Assert.NotNull(eventArgs);
            Assert.Equal(NavigationViewDisplayMode.Minimal, eventArgs.OldDisplayMode);
            Assert.Equal(NavigationViewDisplayMode.Expanded, eventArgs.NewDisplayMode);

            void DisplayMode_Changed(object sender, NavigationViewDisplayModeChangedEventArgs e)
            {
                navView.DisplayModeChanged -= DisplayMode_Changed;
                eventArgs = e;
            }
        }

    }

}
