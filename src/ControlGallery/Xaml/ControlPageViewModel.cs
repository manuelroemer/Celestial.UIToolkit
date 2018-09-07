using System;
using System.Diagnostics;
using ControlGallery.Common;
using ControlGallery.Data;
using Prism.Regions;

namespace ControlGallery.Xaml
{

    public class ControlPageViewModel : GalleryPageViewModel
    {

        public const string ControlInformationParameterName = "ControlInformation";
        
        private ControlInformation _controlInformation;
        public ControlInformation ControlInformation
        {
            get { return _controlInformation; }
            set
            {
                SetProperty(ref _controlInformation, value);
                Title = value?.Name;
            }
        }

        private object _samplePageContent;
        public object SamplePageContent
        {
            get { return _samplePageContent; }
            set { SetProperty(ref _samplePageContent, value); }
        }
        
        public override void OnNavigatedTo(NavigationContext ctx)
        {
            base.OnNavigatedTo(ctx);
            
            ControlInformation = ctx.Parameters[ControlInformationParameterName] as ControlInformation;
            CreateSamplePageContent();
        }

        private void CreateSamplePageContent()
        {
            // The ControlInfo only defines a type for the SamplePage.
            // We want a content, so try to instanciate it.
            if (ControlInformation != null && ControlInformation.SamplePageType != null)
            {
                try
                {
                    SamplePageContent = Activator.CreateInstance(ControlInformation.SamplePageType);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(
                        $"Failed to create a sample page instance. " +
                        $"ControlInfo: {0}, Ex: {1}",
                        ControlInformation.Name,
                        ex.ToString());
                }
            }
            else
            {
                SamplePageContent = null;
            }
        }
    }

}