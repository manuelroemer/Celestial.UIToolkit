using System;
using System.Collections.ObjectModel;

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

        public ObservableCollection<ControlInformation> RelatedControls { get; } =
            new ObservableCollection<ControlInformation>();

        public ObservableCollection<DocumentationLinkInfo> DocumentationLinks { get; } =
            new ObservableCollection<DocumentationLinkInfo>();

    }

    public class DocumentationLinkInfo
    {

        public string Title { get; }

        public string UriString { get; }

        public Uri Uri { get; }

        public DocumentationLinkInfo(string title, string uriString)
        {
            Title = title;
            UriString = uriString;
            Uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
        }

    }

}
