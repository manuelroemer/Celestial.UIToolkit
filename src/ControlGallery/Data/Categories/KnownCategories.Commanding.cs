using ControlGallery.Xaml.SamplePages.Commanding;
using MahApps.Metro.IconPacks;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation ButtonInfo = new ControlInformation()
        {
            Name = "Button",
            Description = "A Button is a control which responds to user input and has a Click event. " +
                          "Buttons are typically used for invoking an action.",
            Icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderAll },
            SamplePageType = typeof(Button)
        };

        public static ControlCategory Commanding { get; } = new ControlCategory(
            "Commanding",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.Message },
            ButtonInfo);

    }

}
