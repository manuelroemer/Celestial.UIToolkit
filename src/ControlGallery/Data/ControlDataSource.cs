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
        }

    }

}
