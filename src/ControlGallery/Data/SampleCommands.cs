using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace ControlGallery.Data
{

    public static class SampleCommands
    {

        public static ICommand ShowMessageBox { get; }

        static SampleCommands()
        {
            ShowMessageBox = new DelegateCommand<string>(ExecuteShowMessageBox);
        }

        private static void ExecuteShowMessageBox(string parameter)
        {
            string title = "Command Example";
            string text = string.IsNullOrEmpty(parameter) ? "No command parameter was specified."
                                                          : parameter;
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }

}
