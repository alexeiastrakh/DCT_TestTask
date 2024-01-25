using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestProjectDCT.Models;
using TestProjectDCT.Services;

namespace TestProjectDCT
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        private Asset selectedCrypto;
        private ObservableCollection<Asset> cryptos;
        private readonly FetchService fetchService;

        public ObservableCollection<Asset> Cryptos
        {
            get { return cryptos; }
            set
            {
                cryptos = value;
                OnPropertyChanged();
            }
        }

        public Asset SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged();
            }
        }

        public DetailsViewModel()
        {
            fetchService = new FetchService();
            FetchCryptosAsync();
        }

        private async void FetchCryptosAsync()
        {
            Cryptos = await fetchService.FetchCryptosAsync();
            OnPropertyChanged(nameof(Cryptos));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
