using ControlGallery.Xaml.SamplePages.Animations;
using MahApps.Metro.IconPacks;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static readonly ControlInformation BrushAnimationInfo = new ControlInformation()
        {
            Name = "BrushAnimation",
            Description = "The BrushAnimation works similar to WPF's ColorAnimation, but animates Brushes.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.DrawBrush },
            SamplePageType = typeof(BrushAnimation)
        };
		
        public static ControlCategory Animations { get; } = new ControlCategory(
            "Animations",
            new PackIconModern() { Kind = PackIconModernKind.CursorMove },
            BrushAnimationInfo);
		
    }

}
