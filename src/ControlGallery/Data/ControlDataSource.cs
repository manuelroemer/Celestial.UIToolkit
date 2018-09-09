using ControlGallery.Data.Categories;
using System;
using System.Collections.Generic;

namespace ControlGallery.Data
{

    public interface IControlDataSource
    {
        IEnumerable<ControlCategory> GetCategories();
    }
    
    public class ControlDataSource : IControlDataSource
    {

        public IEnumerable<ControlCategory> GetCategories()
        {
            yield return KnownCategories.Commanding;
            yield return KnownCategories.Input;
            yield return KnownCategories.Layout;
            yield return KnownCategories.Navigation;
        }

    }

}
