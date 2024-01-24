using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDCT.Services
{
    public class DataService
    {
        public ObservableCollection<Asset> GetCryptoData()
        {
            string url = "https://api.coincap.io/v2/assets";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonresponse;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                jsonresponse = reader.ReadToEnd();
            }

            CurrencyList r = JsonConvert.DeserializeObject<CurrencyList>(jsonresponse);

            ObservableCollection<Asset> cryptos = new ObservableCollection<Asset>();

            foreach (var a in r.Data)
            {
                cryptos.Add(new Asset()
                {
                    Name = a.Name,
                    Id = a.Id,
                    PriceUsd = a.PriceUsd.Substring(0, a.PriceUsd.IndexOf('.') + 4) + " $",
                    Rank = a.Rank,
                    Supply = a.Supply.Substring(0, a.Supply.IndexOf('.')),
                    MaxSupply = a.MaxSupply,
                    MarketCapUsd = a.MarketCapUsd.Substring(0, a.MarketCapUsd.IndexOf('.') + 3) + " $",
                    VolumeUsd24Hr = a.VolumeUsd24Hr.Substring(0, a.VolumeUsd24Hr.IndexOf('.') + 3) + " $",
                    ChangePercent24Hr = a.ChangePercent24Hr.Substring(0, a.ChangePercent24Hr.IndexOf('.') + 3) + " %",
                    Vwap24Hr = a.Vwap24Hr,
                    Explorer = a.Explorer,
                    Symbol = a.Symbol
                });
            }

            foreach (var crypto in cryptos)
            {
                if (crypto.MaxSupply != null)
                {
                    crypto.MaxSupply = crypto.MaxSupply.Substring(0, crypto.MaxSupply.IndexOf('.'));
                }
                if (crypto.Vwap24Hr != null)
                {
                    crypto.Vwap24Hr = crypto.Vwap24Hr.Substring(0, crypto.Vwap24Hr.IndexOf('.') + 4) + " %";
                }
            }

            return cryptos;
        }
    }

}
