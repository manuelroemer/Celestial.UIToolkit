using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ControlGallery.Data
{

    /// <summary>
    /// Provides information about a specific type of control.
    /// This information is then displayed by the gallery application.
    /// </summary>
    public class ControlInformation
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public object Icon { get; set; }
        
        public Type SamplePageType { get; set; }

        public ObservableCollection<ControlInformation> RelatedControls { get; set; } =
            new ObservableCollection<ControlInformation>();

        public ObservableCollection<LinkViewModel> DocumentationLinks { get; set; } =
            new ObservableCollection<LinkViewModel>();

    }

    public class LinkViewModel
    {

        public string Title { get; }

        public string UriString { get; }
        
        public ICommand OpenInBrowserCommand { get; }

        public LinkViewModel(string title, string uriString)
        {
            Title = title;
            UriString = uriString;

            OpenInBrowserCommand = new DelegateCommand(OpenInBrowser);
        }

        public void OpenInBrowser()
        {
            try
            {
                 Process.Start(UriString);
            }
            catch { } // It doesn't really matter if this fails. No ex. handling required.
        }

    }

}
