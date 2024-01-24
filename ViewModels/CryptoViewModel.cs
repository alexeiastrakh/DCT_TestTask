using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectDCT.Services;
using TestProjectDCT.ViewModels.Base;

namespace TestProjectDCT.ViewModels
{
    public class CryptoViewModel : ViewModelBase
    {
        private Asset selectedCrypto;
        private MainViewModel mainViewModel;

        public CryptoViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public ObservableCollection<Asset> Cryptos { get; set; }
        public Asset SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged();
            }
        }

        public CryptoViewModel()
        {
            DataService dataService = new DataService();
            Cryptos = dataService.GetCryptoData();
        }
    }
}
