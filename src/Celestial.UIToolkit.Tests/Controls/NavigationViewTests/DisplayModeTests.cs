using Celestial.UIToolkit.Controls;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Xunit;

namespace Celestial.UIToolkit.Tests.Controls.NavigationViewTests
{

    public class DisplayModeTests
    {

        [WpfTheory]
        [InlineData(100, 200, 50, NavigationViewDisplayMode.Minimal)]   // Default threshold ranges
        [InlineData(100, 200, 150, NavigationViewDisplayMode.Compact)]  //  -> (Compact < Expanded)
        [InlineData(100, 200, 300, NavigationViewDisplayMode.Expanded)]
        [InlineData(200, 100, 50, NavigationViewDisplayMode.Minimal)]   // Compact > Expanded
        [InlineData(200, 100, 150, NavigationViewDisplayMode.Expanded)]
        [InlineData(200, 100, 300, NavigationViewDisplayMode.Expanded)]
        [InlineData(200, 200, 50, NavigationViewDisplayMode.Minimal)]   // Compact == Expanded
        [InlineData(200, 200, 200, NavigationViewDisplayMode.Expanded)]
        public void DisplayModeIsAdaptive(
            double compactThresholdWidth,
            double expandedThresholdWidth,
            double newWidth,
            NavigationViewDisplayMode expectedDisplayMode)
        {
            var navView = new NavigationView()
            {
                CompactModeThresholdWidth = compactThresholdWidth,
                ExpandedModeThresholdWidth = expandedThresholdWidth
            };

            navView.ChangeActualSizeAndRaiseEvent(newWidth, newWidth);
            Assert.Equal(expectedDisplayMode, navView.DisplayMode);
        }

        [WpfFact]
        public void FiresDisplayModeChangedEvent()
        {
            var navView = new NavigationView();

            Assert.Raises<NavigationViewDisplayModeChangedEventArgs>(
                (handler) => navView.DisplayModeChanged += handler,
                (handler) => navView.DisplayModeChanged -= handler,
                () => navView.DisplayMode = NavigationViewDisplayMode.Compact);
        }

        [WpfTheory]
        [InlineData(NavigationViewDisplayMode.Minimal, NavigationViewDisplayMode.Compact)]
        [InlineData(NavigationViewDisplayMode.Compact, NavigationViewDisplayMode.Expanded)]
        [InlineData(NavigationViewDisplayMode.Expanded, NavigationViewDisplayMode.Minimal)]
        public void DisplayModeChangedCarriesOldAndNewValues(
            NavigationViewDisplayMode oldDisplayMode,
            NavigationViewDisplayMode newDisplayMode)
        {
            var navView = new NavigationView();
            navView.DisplayMode = oldDisplayMode;
            
            var raisedEvent = Assert.Raises<NavigationViewDisplayModeChangedEventArgs>(
                (handler) => navView.DisplayModeChanged += handler,
                (handler) => navView.DisplayModeChanged -= handler,
                () => navView.DisplayMode = newDisplayMode);
            var eventArgs = raisedEvent.Arguments;
            
            Assert.Equal(oldDisplayMode, eventArgs.OldDisplayMode);
            Assert.Equal(newDisplayMode, eventArgs.NewDisplayMode);
        }

    }

}
