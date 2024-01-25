using System.Windows;
using System.Windows.Controls;
using TestProjectDCT.Infrastracture.Commands;
using TestProjectDCT.ViewModels;

namespace TestProjectDCT
{

    public partial class MainWindow : Window
    {
        public RelayCommand HomeCommand { get; }
        public RelayCommand ListCommand { get; }
        public RelayCommand DetailsCommand { get; }
        public RelayCommand SettingsCommand { get; }

        public MainWindow()
        {
            InitializeComponent();

            HomeCommand = new RelayCommand(_ => NavigateTo(new HomePage()));
            ListCommand = new RelayCommand(_ => NavigateTo(new ConvertPage()));
            DetailsCommand = new RelayCommand(_ => NavigateTo(new DetailsPage()));
            SettingsCommand = new RelayCommand(_ => NavigateTo(new SettingsPage()));

            DataContext = this;

            frame.Navigate(new HomePage());
        }


        private void NavigateTo(Page page)
        {
            frame.Navigate(page);
        }
    }

}
