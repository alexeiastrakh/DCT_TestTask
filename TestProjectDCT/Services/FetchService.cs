using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using TestProjectDCT.Models;

namespace TestProjectDCT.Services
{
    public class FetchService
    {
        public async Task<ObservableCollection<Asset>> FetchCryptosAsync()
        {
            ObservableCollection<Asset> cryptos = new ObservableCollection<Asset>();

            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.coincap.io/v2/assets";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonresponse = await response.Content.ReadAsStringAsync();
                    CurrencyList r = JsonConvert.DeserializeObject<CurrencyList>(jsonresponse);

                    foreach (var a in r.Data)
                    {
                        Asset crypto = new Asset()
                        {
                            Name = a.Name,
                            Id = a.Id,
                            PriceUsd = $"{a.PriceUsd:F4} $",
                            Rank = a.Rank,
                            Supply = $"{a.Supply:F0}",
                            MaxSupply = a.MaxSupply?.Split('.')[0],
                            MarketCapUsd = $"{a.MarketCapUsd:F3} $",
                            VolumeUsd24Hr = $"{a.VolumeUsd24Hr:F3} $",
                            ChangePercent24Hr = $"{a.ChangePercent24Hr:F3} %",
                            Vwap24Hr = a.Vwap24Hr?.Split('.')[0],
                            Explorer = a.Explorer,
                            Symbol = a.Symbol
                        };

                        cryptos.Add(crypto);
                    }

                    UpdateAdditionalInfo(cryptos);
                }
                else
                {
                    Console.WriteLine($"Error fetching cryptocurrencies: {response.StatusCode}");
                }
            }

            return cryptos;
        }

        private void UpdateAdditionalInfo(ObservableCollection<Asset> cryptos)
        {
            foreach (var crypto in cryptos)
            {
                if (crypto.MaxSupply != null)
                {
                    crypto.MaxSupply = crypto.MaxSupply.Split('.')[0];
                }
                if (crypto.Vwap24Hr != null)
                {
                    crypto.Vwap24Hr = $"{crypto.Vwap24Hr:F4} %";
                }
            }
        }
    }
}
