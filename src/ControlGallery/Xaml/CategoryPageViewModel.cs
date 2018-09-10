using ControlGallery.Common;
using ControlGallery.Data;
using Prism.Regions;

namespace ControlGallery.Xaml
{

    public class CategoryPageViewModel : GalleryPageViewModel
    {
        
        public const string CategoryParameterName = "Category";

        private ControlCategory _category;
        public ControlCategory Category
        {
            get { return _category; }
            set
            {
                SetProperty(ref _category, value);
                Title = value?.Name;
            }
        }

        private ControlInformation _selectedControl;
        public ControlInformation SelectedControl
        {
            get { return _selectedControl; }
            set
            {
                SetProperty(ref _selectedControl, value);
                NavigateToSelectedControl();
            }
        }
        
        public override void OnNavigatedTo(NavigationContext ctx)
        {
            base.OnNavigatedTo(ctx);

            var category = ctx.Parameters[CategoryParameterName] as ControlCategory;
            Category = category;
        }

        private void NavigateToSelectedControl()
        {
            if (SelectedControl != null)
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add(
                    ControlPageViewModel.ControlInformationParameterName,
                    SelectedControl);

                NavigationService.RequestNavigate(
                    typeof(ControlPage),
                    navigationParams);

                // Reset the selection, since we navigated away.
                // This is required, because the ListView couldn't be "Clicked" otherwise. 
                SelectedControl = null;
            }
        }

    }

}
