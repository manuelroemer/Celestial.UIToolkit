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

        public static ControlCategory Layout { get; } = new ControlCategory(
            "Layout",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderAll },
            SplitViewInfo,
            ExpanderInfo);

    }

}
