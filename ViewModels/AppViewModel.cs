using Newtonsoft.Json;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestProjectDCT.Models;

namespace TestProjectDCT
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private Asset selectedCrypto;

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
        public ObservableCollection<Asset> TopCryptos { get; set; }
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

        public AppViewModel()
        {

            string url = "https://api.coincap.io/v2/assets";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonresponse;
            Asset dat = new Asset();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                jsonresponse = reader.ReadToEnd();
            }
            CurrencyList r = JsonConvert.DeserializeObject<CurrencyList>(jsonresponse);
            Cryptos = new ObservableCollection<Asset>() { };
            TopCryptos = new ObservableCollection<Asset>() { };
            foreach (var a in r.Data)
            {
                Cryptos.Add(new Asset() { Name = a.Name, Id = a.Id, PriceUsd = a.PriceUsd.Substring(0, a.PriceUsd.IndexOf('.')+4)+" $", Rank = a.Rank, Supply = a.Supply.Substring(0, a.Supply.IndexOf('.')), MaxSupply = a.MaxSupply, MarketCapUsd = a.MarketCapUsd.Substring(0,a.MarketCapUsd.IndexOf('.')+3)+" $", VolumeUsd24Hr = a.VolumeUsd24Hr.Substring(0, a.VolumeUsd24Hr.IndexOf('.')+3)+" $", ChangePercent24Hr = a.ChangePercent24Hr.Substring(0, a.ChangePercent24Hr.IndexOf('.')+3) + " %", Vwap24Hr = a.Vwap24Hr , Explorer=a.Explorer , Symbol = a.Symbol });
                if (Int32.Parse(a.Rank) <= 10)
                {
                    TopCryptos.Add(new Asset() { Name = a.Name, Id = a.Id, PriceUsd = a.PriceUsd, Rank = a.Rank, Supply = a.Supply, MaxSupply = a.MaxSupply, MarketCapUsd = a.MarketCapUsd, VolumeUsd24Hr = a.VolumeUsd24Hr, ChangePercent24Hr = a.ChangePercent24Hr, Vwap24Hr = a.Vwap24Hr , Explorer=a.Explorer });
                }
            }
            foreach (var a in Cryptos)
            {
                if (a.MaxSupply != null)
                {
                    a.MaxSupply = a.MaxSupply.Substring(0, a.MaxSupply.IndexOf('.'));
                }
                if (a.Vwap24Hr != null)
                {
                    a.Vwap24Hr = a.Vwap24Hr.Substring(0, a.Vwap24Hr.IndexOf('.')+4) + " %";
                }
            }
        }
        private List<HistoryChanges> historyChangesList;
        public List<HistoryChanges> HistoryChangesList
        {
            get { return historyChangesList; }
            set
            {
                historyChangesList = value;
                OnPropertyChanged(nameof(HistoryChangesList));

                // Update the plot when history changes are set
                UpdatePlot();
            }
        }

        // OxyPlot PlotModel property
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set
            {
                plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }

        // Update the OxyPlot based on historical changes
        private void UpdatePlot()
        {
            PlotModel = new PlotModel();
            LineSeries series = new LineSeries();

            // Assuming Date is sorted in ascending order in HistoryChangesList
            foreach (var historyChange in HistoryChangesList)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(historyChange.Date), historyChange.Price.GetValueOrDefault()));
            }

            PlotModel.Series.Add(series);
            PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Title = "Date" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Price" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
