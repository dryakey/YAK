using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutinPower.WPF.Controls
{
    /// <summary>
    /// Interaction logic for UI_DoughnutChart.xaml
    /// </summary>
    public partial class UI_DoughnutChart : UserControl
    {

        public SeriesCollection SeriesCollection { get => _seriesCollection; set => _seriesCollection = value; }
        public double Width { get=>_width; set=>_width=value; }

        private SeriesCollection _seriesCollection;
        private double _width;

        public UI_DoughnutChart()
        {
            InitializeComponent();
            _seriesCollection = new SeriesCollection();
            mainGrid.DataContext = this;
            uiChart.DataContext = this;
        }

        public void SetUpSeries(string[] titles, double[] values)
        {
            var lstSeries = new List<PieSeries>();
            for(int i=0; i<titles.Length; i++)
            {
                lstSeries.Add(new PieSeries
                {
                    Title = titles[i],
                    Values = new ChartValues<ObservableValue> { new ObservableValue(values[i]) },
                    DataLabels = false
                });
            }
            SeriesCollection.Clear();
            SeriesCollection.AddRange(lstSeries);
        }
    }
}
