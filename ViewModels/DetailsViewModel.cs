using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectDCT.Models;
using TestProjectDCT.ViewModels.Base;

namespace TestProjectDCT.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;

        public DetailsViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        private List<HistoryChanges> historyChangesList;
        public List<HistoryChanges> HistoryChangesList
        {
            get { return historyChangesList; }
            set
            {
                historyChangesList = value;
                OnPropertyChanged(nameof(HistoryChangesList));

            
                UpdatePlot();
            }
        }

      
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

     
        private void UpdatePlot()
        {
            PlotModel = new PlotModel();
            LineSeries series = new LineSeries();

         
            foreach (var historyChange in HistoryChangesList)
            {
                series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(historyChange.Date), historyChange.Price.GetValueOrDefault()));
            }

            PlotModel.Series.Add(series);
            PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Title = "Date" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Price" });
        }
    }

}
