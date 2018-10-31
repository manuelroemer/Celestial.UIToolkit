using ControlGallery.Xaml.SamplePages.Navigation;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation NavigationViewInfo = new ControlInformation()
        {
            Name = "NavigationView",
            Description = "A NavigationView consists of two content ares: A pane, which can be visible or not, " +
                          "and an area which displays permanently visible content.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.LinesHorizontal4 },
            SamplePageType = typeof(NavigationView)
        };

        private static ControlInformation TabControlInfo = new ControlInformation()
        {
            Name = "TabControl",
            Description = "A TabControl displays multiple pages of controls. A user can switch between them via Tabs.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Table },
            SamplePageType = typeof(TabControl)
        };

        private static ControlInformation WindowInfo = new ControlInformation()
        {
            Name = "Window",
            Description = "Windows are typically the root of your application and host its content..",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Window },
            SamplePageType = typeof(Window)
        };

        public static ControlCategory Navigation { get; } = new ControlCategory(
            "Navigation",
            new PackIconModern() { Kind = PackIconModernKind.LinesHorizontal4 },
            WindowInfo,
            NavigationViewInfo,
            TabControlInfo);

    }

}
