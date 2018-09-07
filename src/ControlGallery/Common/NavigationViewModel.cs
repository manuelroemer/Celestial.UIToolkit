using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ControlGallery.Common
{

    /// <summary>
    /// A base class for view models which are involved in navigation and thus
    /// implement the <see cref="INavigationAware"/> interface.
    /// </summary>
    public abstract class NavigationAwareViewModel : BindableBase, INavigationAware
    {

        private IRegionNavigationService _navigationService;
        /// <summary>
        /// Gets the navigation service which is associated with this view model's
        /// region.
        /// </summary>
        protected IRegionNavigationService NavigationService
        {
            get { return _navigationService; }
            set
            {
                // Before/after changing our local navigation service,
                // register the event which updates the GoForward/Back-Command's CanExecute()
                // when navigation happens.
                if (_navigationService != null)
                {
                    _navigationService.Navigated -= NavigationService_Navigated;
                }

                SetProperty(ref _navigationService, value);

                if (_navigationService != null)
                {
                    _navigationService.Navigated += NavigationService_Navigated;
                }
            }
        }

        /// <summary>
        /// A command which navigates to the most recent entry in the back navigation history.
        /// </summary>
        public DelegateCommand GoBackCommand { get; }

        /// <summary>
        /// A command which navigates to the most recent entry in the forward navigation history.
        /// </summary>
        public DelegateCommand GoForwardCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationAwareViewModel"/> class.
        /// </summary>
        public NavigationAwareViewModel()
        {
            GoBackCommand = new DelegateCommand(
                () => NavigationService.Journal.GoBack(),
                () => NavigationService?.Journal.CanGoBack ?? false);
            GoForwardCommand = new DelegateCommand(
                () => NavigationService.Journal.GoForward(),
                () => NavigationService?.Journal.CanGoForward ?? false);
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// Returns true by default.
        /// </summary>
        /// <param name="ctx">The navigation context.</param>
        /// <returns>true if this instance accepts the navigation request; otherwise false.</returns>
        public virtual bool IsNavigationTarget(NavigationContext ctx)
        {
            return true;
        }

        /// <summary>
        /// Called when the view model is being navigated away from.
        /// </summary>
        /// <param name="ctx">The navigation context</param>
        public virtual void OnNavigatedFrom(NavigationContext ctx)
        {
        }

        /// <summary>
        /// Called when the view model is being navigated to.
        /// </summary>
        /// <param name="ctx">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext ctx)
        {
            NavigationService = ctx.NavigationService;
        }

        /// <summary>
        /// Handles the <see cref="IRegionNavigationService.Navigated"/> event.
        /// It is used to update the <see cref="GoBackCommand"/>'s and <see cref="GoForwardCommand"/>'s
        /// <see cref="DelegateCommand.CanExecute"/> state.
        /// </summary>
        /// <param name="sender"><see cref="NavigationService"/>.</param>
        /// <param name="e">Navigation event args.</param>
        private void NavigationService_Navigated(object sender, RegionNavigationEventArgs e)
        {
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

    }

}
