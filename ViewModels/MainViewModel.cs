using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectDCT.ViewModels.Base;

namespace TestProjectDCT.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private CryptoViewModel cryptoViewModel;
        private DetailsViewModel detailsViewModel;
        public CryptoViewModel CryptoViewModel
        {
            get { return cryptoViewModel; }
            set
            {
                cryptoViewModel = value;
                OnPropertyChanged();
            }
        }

        public DetailsViewModel DetailsViewModel
        {
            get { return detailsViewModel; }
            set
            {
                detailsViewModel = value;
                OnPropertyChanged();
            }
        }
   
        public MainViewModel()
        {          
            CryptoViewModel = new CryptoViewModel(this);
            DetailsViewModel = new DetailsViewModel(this);
        }
    }

}
