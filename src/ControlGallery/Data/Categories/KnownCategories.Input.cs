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

        private static ControlInformation ComboBoxInfo = new ControlInformation()
        {
            Name = "ComboBox",
            Description = "A ComboBox allows the selection of an item from a list.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.BaseSelect },
            SamplePageType = typeof(ComboBox)
        };

        private static ControlInformation CheckBoxInfo = new ControlInformation()
        {
            Name = "CheckBox",
            Description = "A CheckBox allows the user to enter different check states, namely " +
                          "true, false or indeterminate.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Checkmark },
            SamplePageType = typeof(CheckBox)
        };

        private static ControlInformation RadioButtonInfo = new ControlInformation()
        {
            Name = "RadioButton",
            Description = "A RadioButton allows the user to enter different check states, like the CheckBox. " +
                          "The difference is that within a group of RadioButtons, only one can be checked.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Checkmark },
            SamplePageType = typeof(RadioButton)
        };

        private static ControlInformation ListBoxInfo = new ControlInformation()
        {
            Name = "ListBox",
            Description = "A ListBox presents multiple items in a box. One item can be selected by the user.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Box },
            SamplePageType = typeof(ListBox)
        };

        private static ControlInformation ListViewInfo = new ControlInformation()
        {
            Name = "ListView",
            Description = "The ListView control provides the infrastructure to display a set of data items in different layouts or views.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Box },
            SamplePageType = typeof(ListView)
        };

        private static ControlInformation SliderInfo = new ControlInformation()
        {
            Name = "Slider",
            Description = "A Slider allows the user to pick a value from a range of numbers.",
            Icon = new PackIconModern() { Kind = PackIconModernKind.Calculator },
            SamplePageType = typeof(Slider)
        };

        public static ControlCategory Input { get; } = new ControlCategory(
            "Input",
            new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.BorderOutside },
            CheckBoxInfo,
            RadioButtonInfo,
            SliderInfo,
            TextBoxInfo,
            PasswordBoxInfo,
            ComboBoxInfo,
            ListBoxInfo,
            ListViewInfo);

    }

}
