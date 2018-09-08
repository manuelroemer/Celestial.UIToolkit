using ControlGallery.Xaml.SamplePages.Input;
using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation TextBoxInfo = new ControlInformation()
        {
            Name = "TextBox",
            Description = "A TextBox allows the user to enter textual content into a designated field. " +
                          "It allows both single-line and multi-line input.",
            Icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.CommentText },
            SamplePageType = typeof(TextBox),
            DocumentationLinks = new ObservableCollection<LinkViewModel>()
            {
                new LinkViewModel("MD TextBox", "https://material.io/design/components/text-fields.html"),
                new LinkViewModel("UWP TextBox Guide", "https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/text-box")
            }
        };

        private static ControlInformation PasswordBoxInfo = new ControlInformation()
        {
            Name = "PasswordBox",
            Description = "A PasswordBox is a variation of the TextBox that allows the user to enter passwords.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Key },
            SamplePageType = typeof(PasswordBox)
        };

        public static ControlCategory Input { get; } = new ControlCategory(
            "Input",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderOutside },
            TextBoxInfo,
            PasswordBoxInfo);

    }

}
