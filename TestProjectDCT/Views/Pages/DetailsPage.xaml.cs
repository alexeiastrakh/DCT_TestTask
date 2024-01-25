using System.Windows.Controls;
using System.Windows.Navigation;
using TestProjectDCT.ViewModels;

namespace TestProjectDCT
{
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
            DataContext = new DetailsViewModel();
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
            e.Handled = true;
        }
    }
}
