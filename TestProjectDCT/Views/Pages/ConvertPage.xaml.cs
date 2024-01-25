using System.Windows.Controls;
using TestProjectDCT.ViewModels;

namespace TestProjectDCT
{
    public partial class ConvertPage : Page
    {
        public ConvertPage()
        {
            InitializeComponent();

            DataContext = new ConvertViewModel();
        }

    }
}
