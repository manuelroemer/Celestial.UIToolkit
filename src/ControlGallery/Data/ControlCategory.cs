using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ControlGallery.Data
{

    /// <summary>
    /// Defines a category into which controls can be grouped.
    /// </summary>
    public class ControlCategory
    {
        
        public string Name { get; set; }

        public object Icon { get; set; }

        public ObservableCollection<ControlInformation> Controls { get; }

        public ControlCategory()
            : this(null) { }

        public ControlCategory(string name)
            : this(name, null) { }

        public ControlCategory(string name, object icon)
            : this(name, icon, (IEnumerable<ControlInformation>)null) { }

        public ControlCategory(string name, object icon, params ControlInformation[] controls)
            : this(name, icon, (IEnumerable<ControlInformation>)controls) { }

        public ControlCategory(string name, object icon, IEnumerable<ControlInformation> controls)
        {
            Name = name;
            Icon = icon;
            if (controls == null)
            {
                Controls = new ObservableCollection<ControlInformation>();
            }
            else
            {
                Controls = new ObservableCollection<ControlInformation>(controls);
            }
        }

    }

}
