using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            var mainViewModel = new MainViewModel();
            

            HomeCommand = new RelayCommand(_ => NavigateTo(new HomePage(mainViewModel)));
            ListCommand = new RelayCommand(_ => NavigateTo(new ListPage()));
            DetailsCommand = new RelayCommand(_ => NavigateTo(new DetailsPage(mainViewModel)));
            SettingsCommand = new RelayCommand(_ => NavigateTo(new SettingsPage()));

            DataContext = this;

            frame.Navigate(new HomePage(mainViewModel));
        }


        private void NavigateTo(Page page)
        {
            frame.Navigate(page);
        }
    }

}
