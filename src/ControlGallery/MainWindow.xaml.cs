using System.Windows;

namespace ControlGallery
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavView_BackRequested(object sender, System.EventArgs e)
        {
            MessageBox.Show("Back requested!");
        }
    }

}
