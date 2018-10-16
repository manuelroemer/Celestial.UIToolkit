using ControlGallery.Xaml.SamplePages.Layout;
using MahApps.Metro.IconPacks;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation SplitViewInfo = new ControlInformation()
        {
            Name = "SplitView",
            Description = "A SplitView consists of two content ares: A pane, which can be visible or not, " +
                          "and an area which displays permanently visible content.",
            Icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.CommentText },
            SamplePageType = typeof(SplitView)
        };

        private static ControlInformation ExpanderInfo = new ControlInformation()
        {
            Name = "Expander",
            Description = "An Expander is a control which displays content that can be collapsed in " +
                          "different directions.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.ArrowDown },
            SamplePageType = typeof(Expander)
        };

        private static ControlInformation MenuInfo = new ControlInformation()
        {
            Name = "Menu",
            Description = "An Menu organizes menu items which can be used to invoke commands.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.ListSelect },
            SamplePageType = typeof(Menu)
        };

        private static ControlInformation StatusBarInfo = new ControlInformation()
        {
            Name = "StatusBar",
            Description = "A StatusBar displays information about the application's status in a horizontal bar.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.GraphBar },
            SamplePageType = typeof(StatusBar)
        };

        private static ControlInformation ToolBarInfo = new ControlInformation()
        {
            Name = "ToolBar",
            Description = "A ToolBar usually displays multiple small controls that fall into a certain category and execute commands.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.ListSelect },
            SamplePageType = typeof(ToolBar)
        };

        private static ControlInformation RelativeCanvasInfo = new ControlInformation()
        {
            Name = "RelativeCanvas",
            Description = "A RelativeCanvas provides the same functionality as the default Canvas, but works with relative coordinates.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.BorderOutside },
            SamplePageType = typeof(RelativeCanvas)
        };

        public static ControlCategory Layout { get; } = new ControlCategory(
            "Layout",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderAll },
            SplitViewInfo,
            MenuInfo,
            StatusBarInfo,
            ToolBarInfo,
            ExpanderInfo,
            RelativeCanvasInfo);

    }

}
