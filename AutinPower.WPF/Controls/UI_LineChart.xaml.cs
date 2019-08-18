using LiveCharts;
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
    /// Interaction logic for UI_LineChart.xaml
    /// </summary>
    public partial class UI_LineChart : UserControl
    { // https://lvcharts.net/App/examples/v1/wpf/Basic%20Line%20Chart

        public string XTitle { get; set; }
        public string YTitle { get; set; }
        public string[] Labels { get; set; }
        public SeriesCollection SeriesCollection { get=> _seriesCollection; set=> _seriesCollection=value; }
        public Func<double, string> YFormatter { get; set; }

        private SeriesCollection _seriesCollection;

        public UI_LineChart()
        {
            InitializeComponent();
            _seriesCollection = new SeriesCollection();
            uiChart.DataContext = this;
        }
    }
}
