using ControlGallery.Xaml.SamplePages.Input;
using MahApps.Metro.IconPacks;

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
            SamplePageType = typeof(TextBox)
        };

        public static ControlCategory Input { get; } = new ControlCategory(
            "Input",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderOutside },
            TextBoxInfo);

    }

}
