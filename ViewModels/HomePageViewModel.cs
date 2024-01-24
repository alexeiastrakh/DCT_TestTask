using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestProjectDCT.Infrastracture.Commands;
using TestProjectDCT.Services;

namespace TestProjectDCT.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private Asset selectedCrypto;

        public ObservableCollection<Asset> TopCryptos { get; set; }
        public ICommand HomeCommand { get; }
        public ICommand ListCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand SettingsCommand { get; }

        public Asset SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged();
            }
        }
        private bool isDetailedInfoVisible;

        public bool IsDetailedInfoVisible
        {
            get { return isDetailedInfoVisible; }
            set
            {
                if (isDetailedInfoVisible != value)
                {
                    isDetailedInfoVisible = value;
                    OnPropertyChanged(nameof(IsDetailedInfoVisible));
                }
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

            // Assign rank based on the index in the list
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
