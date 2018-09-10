using ControlGallery.Data;
using ControlGallery.Xaml.SamplePages.Containers;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlGallery.Data.Categories
{

    public static partial class KnownCategories
    {

        private static ControlInformation CardInfo = new ControlInformation()
        {
            Name = "Card",
            Description = "A Card is a self-contained unit which displays content in an " +
                          "organized form.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.BorderOutside },
            SamplePageType = typeof(Card),
            DocumentationLinks = new ObservableCollection<LinkViewModel>()
            {
                new LinkViewModel("MD - Cards", "https://material.io/design/components/cards.html")
            }
        };

        public static ControlCategory Containers { get; } = new ControlCategory(
            "Containers",
            new PackIconModern() { Kind = PackIconModernKind.BorderOutside },
            CardInfo);

    }

}
