using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestProjectDCT.Models;

namespace TestProjectDCT.Services
{
    public class RequestService
    {
        private static readonly string CryptoApiUrl = "https://api.coincap.io/v2/assets";

        public async Task<List<Asset>> GetCryptosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(CryptoApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    CurrencyList root = JsonConvert.DeserializeObject<CurrencyList>(jsonResponse);
                    return root?.Data ?? new List<Asset>();
                }

                return new List<Asset>();
            }
        }

        public async Task<List<HistoryChanges>> GetHistoryChangesListAsync(string coinId)
        {
            string url = $"{CryptoApiUrl}/{coinId}/history?interval=h1";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    HistoryChanges historyChanges = JsonConvert.DeserializeObject<HistoryChanges>(jsonResponse);
                    return new List<HistoryChanges> { historyChanges };
                }

                return new List<HistoryChanges>();
            }
        }
    }
}
