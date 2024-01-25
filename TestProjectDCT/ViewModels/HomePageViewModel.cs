using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TestProjectDCT.Models;
using TestProjectDCT.Services;

namespace TestProjectDCT.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private Asset selectedCrypto;

        public ObservableCollection<Asset> TopCryptos { get; set; }

        public Asset SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged();
            }
        }
   
        public HomePageViewModel()
        {
            InitializeData();
        }

        private async void InitializeData()
        {
            RequestService cryptoDataService = new RequestService();
            List<Asset> cryptos = await cryptoDataService.GetCryptosAsync();

            for (int i = 0; i < cryptos.Count; i++)
            {
                int counter = i + 1;
                cryptos[i].Rank = counter.ToString();
            }

            TopCryptos = new ObservableCollection<Asset>(cryptos.Take(10));
            OnPropertyChanged(nameof(TopCryptos));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
