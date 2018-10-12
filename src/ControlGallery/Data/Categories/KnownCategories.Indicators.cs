using ControlGallery.Xaml.SamplePages.Indicators;
using MahApps.Metro.IconPacks;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation ProgressBarInfo = new ControlInformation()
        {
            Name = "ProgressBar",
            Description = "A ProgressBar provides feedback to the user that a long running operation is underway.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Ring },
            SamplePageType = typeof(ProgressBar)
        };
        
        public static ControlCategory Indicators { get; } = new ControlCategory(
            "Indicators",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderAll },
            ProgressBarInfo);

    }

}
