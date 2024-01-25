using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TestProjectDCT.Infrastracture.Commands;
using TestProjectDCT.Models;
using TestProjectDCT.Services;
using TestProjectDCT.ViewModels.Services;

namespace TestProjectDCT.ViewModels
{
    public class ConvertViewModel : INotifyPropertyChanged
    {
        private Asset fromCrypto;
        private Asset toCrypto;
        private double amount;
        private double result;
        private List<Asset> allCryptos;
        public RelayCommand ConvertCommand { get; }
        private readonly ConversionService _conversionService;
        public Asset FromCrypto
        {
            get { return fromCrypto; }
            set
            {
                fromCrypto = value;
                OnPropertyChanged();
            }
        }

        public Asset ToCrypto
        {
            get { return toCrypto; }
            set
            {
                toCrypto = value;
                OnPropertyChanged();
            }
        }

        public double Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        public List<Asset> AllCryptos
        {
            get { return allCryptos; }
            set
            {
                allCryptos = value;
                OnPropertyChanged();
            }
        }

        public ConvertViewModel()
        {
            _conversionService = new ConversionService();

            ConvertCommand = new RelayCommand(async _ => await Convert());

            FetchAllCryptos();
        }

        private async void FetchAllCryptos()
        {
            try
            {
                RequestService requestService = new RequestService();
                AllCryptos = await requestService.GetCryptosAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cryptocurrencies: {ex.Message}");
            }
        }

        private async Task Convert()
        {
            Result = await _conversionService.ConvertAsync(FromCrypto, ToCrypto, Amount);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
