﻿using System;
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
using TestProjectDCT.ViewModels;

namespace TestProjectDCT
{
    /// <summary>
    /// Логика взаимодействия для CryptoDetails.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel.DetailsViewModel;
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
            e.Handled = true;
        }
    }
}
