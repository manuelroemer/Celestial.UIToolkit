using ControlGallery.Xaml.SamplePages.Text;
using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation TextBlockInfo = new ControlInformation()
        {
            Name = "TextBlock",
            Description = "A TextBlock is used to display larger amounts of, potentially formatted, text.",
            Icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.NoteText },
            SamplePageType = typeof(TextBlock),
            DocumentationLinks = new ObservableCollection<LinkViewModel>()
            {
                new LinkViewModel("UWP Typography Type Ramp", "https://docs.microsoft.com/en-us/windows/uwp/design/style/typography#type-ramp"),
                new LinkViewModel("UWP TextBlock", "https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/text-block")
            }
        };

        private static ControlInformation LabelInfo = new ControlInformation()
        {
            Name = "Label",
            Description = "A Label is a control which displays a small text, typically labeling other controls",
            Icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.NoteText },
            SamplePageType = typeof(Label),
            DocumentationLinks = new ObservableCollection<LinkViewModel>()
            {
                new LinkViewModel("UWP Typography Type Ramp", "https://docs.microsoft.com/en-us/windows/uwp/design/style/typography#type-ramp"),
                new LinkViewModel("UWP Label", "https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/labels")
            }
        };

        private static ControlInformation ToolTipInfo = new ControlInformation()
        {
            Name = "ToolTip",
            Description = "A ToolTip gets displayed when the user hovers over an item for a longer time. It is supposed to display contextual information.",
            Icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.Information },
            SamplePageType = typeof(ToolTip),
            DocumentationLinks = new ObservableCollection<LinkViewModel>()
            {

            }
        };

        public static ControlCategory Text { get; } = new ControlCategory(
            "Text",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.NoteText },
            TextBlockInfo,
            LabelInfo,
            ToolTipInfo);

    }

}
