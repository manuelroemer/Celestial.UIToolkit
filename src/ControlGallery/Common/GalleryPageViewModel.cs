namespace ControlGallery.Common
{

    public abstract class GalleryPageViewModel : NavigationAwareViewModel
    {

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

    }

}
