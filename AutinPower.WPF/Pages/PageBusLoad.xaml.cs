using Autin.Model;
using AutinPower.WPF.Models;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageBusLoadPredict.xaml
    /// </summary>
    public partial class PageBusLoad : Page, INotifyPropertyChanged
    {
        #region properties
        public Action<object[]> NotifyHost { get; set; }
        public List<ObservablePoint> ObservablePoints { get; set; }
        public string UITitle
        {
            get => _uiTitle;
            set
            {
                _uiTitle = value;
                NotifyPropertyChanged("UITitle");
            }
        }
        public List<PowerLoad> PowerLoad {
            get => _powerLoad;
            set {
                _powerLoad = value;
                NotifyPropertyChanged("PowerLoad");
            }
        }
        public string[] NodeLabels
        {
            get => _nodeLabels;
            set
            {
                _nodeLabels = value;
                NotifyPropertyChanged("NodeLabels");
            }
        }
        public string SeletedNodeLabel { get => _seletedNodeLabel; set => _seletedNodeLabel = value; }
        #endregion

        #region fields
        private string _uiTitle;
        private string[] _nodeLabels;
        private string _seletedNodeLabel;
        private List<PowerLoad> _powerLoad;
        private PowerLoadModel _busLoadModel;
        #endregion

        public PageBusLoad()
        {
            InitializeComponent();
            gridMain.DataContext = this;
            InitializeNodeList();
            InitializePowerLoadModel();
            ObservablePoints = new List<ObservablePoint>();
            comboBoxBus.SelectedIndex = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void InitializeNodeList()
        {
            var nodeLabels = new string[] { "B", "C", "D" };
            NodeLabels = SystemManager.Instance.CurrentElement.Where(p => p.ElementType == "ld" && nodeLabels.Contains(p.ElementDescription)).Select(p => p.ElementId).ToArray();
            // UnitLabels = SystemManager.Instance.CurrentElement.Where(p => p.ElementType == "ld" && nodeLabels.Contains(p.ElementDescription)).Select(p => p.ElementId).ToArray();
            // NodeLabels = SystemManager.Instance.CurrentElement.Where(p => p.ElementType == "ld").Select(p => p.ElementId).ToArray();
        }

        private void InitializePowerLoadModel()
        {
            _busLoadModel = new PowerLoadModel();
            foreach (var label in _nodeLabels)
            {
                _busLoadModel.PowerLoadTable.Add(label, new List<PowerLoad>());
            }
        }

        private void LoadData(string nodeLabel)
        {
            if (_busLoadModel.PowerLoadTable.Keys.Contains(nodeLabel))
            {
                PowerLoad = _busLoadModel.PowerLoadTable[nodeLabel];
                if (_powerLoad.Count == 0)
                {
                    var dataCount = SystemManager.Instance.CurrentControlInfoList.Where(p => p.ParamName == "PrdNum").FirstOrDefault();
                    if (dataCount != null)
                    {
                        var count = Convert.ToInt32(dataCount.ParamValue);
                        for (int j = 0; j < count; j++)
                        {
                            PowerLoad.Add(new PowerLoad()
                            {
                                CaseId = SystemManager.Instance.CurrentCaseId,
                                ClientId = SystemManager.Instance.CurrentUser.ClientId,
                                NodeId = _seletedNodeLabel,
                                PeriodId = $"period-{(j + 1)}",
                                Load = 0,
                                SettlementDate = SystemManager.Instance.CurrentSettlementDate
                            });
                        }
                    }
                }
                dataGridBusLoad.Items.Refresh();
                var i = 1;
                ObservablePoints.Clear();
                foreach (var data in _powerLoad)
                {
                    ObservablePoints.Add(new ObservablePoint((double)i, data.Load));
                    i++;
                }
            }
        }
        
        private void BindPlot()
        {
            var xyMapper = Mappers.Xy<ObservablePoint>().X(p => p.X).Y(p => p.Y);
            var setpLineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0
            };
            setpLineSerise.Values = new ChartValues<ObservablePoint>(ObservablePoints);
            uiChart.SeriesCollection.Clear();
            uiChart.SeriesCollection.Add(setpLineSerise);
        }

        public async Task<string> SubmitData()
        {
            _busLoadModel.PowerLoadTable[_seletedNodeLabel] = _powerLoad;

            var userId = SystemManager.Instance.CurrentUser.Id;
            var clientId = SystemManager.Instance.CurrentUser.ClientId;
            var clientToken = SystemManager.Instance.CurrentUser.ClientToken;
            var caseId = SystemManager.Instance.CurrentCaseId;
            var settlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);

            var unitModel = new UnitModel();

            try
            {
                var response = "";
                switch (Title)
                {
                    case "_pageBusLoadPredict":
                        {
                            response = await unitModel.SetPowerLoadRequest(userId, _seletedNodeLabel, caseId, clientToken, clientId, settlementDate, _powerLoad.ToJson());
                            break;
                        }
                    case "_pageBusLoadActual":
                        {
                            break;
                        }
                    case "_pageSystemLoadPredict":
                        {
                            response = await unitModel.SetSystemLoadRequest(userId, _seletedNodeLabel, caseId, clientToken, clientId, settlementDate, _powerLoad.ToJson());
                            break;
                        }
                }
                if (!response.Contains("success"))
                {
                    MessageBox.Show(response);
                }
                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "fails";
        }

        private void dataGridBusLoad_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var rowIndex = e.Row.GetIndex();
                var colIndex = e.Column.DisplayIndex;
                var t = e.EditingElement as TextBox;
                var value = Convert.ToInt64(t.Text);
                if (colIndex == 1)
                {
                    ObservablePoints[rowIndex].Y = value;
                }
            }
        }

        private void buttonImport_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                var csvFileDir = fileDialog.FileName;
                var dataContent = File.ReadAllLines(csvFileDir).Skip(1).ToArray();
                var dataCount = SystemManager.Instance.CurrentControlInfoList.Where(p => p.ParamName == "PrdNum").FirstOrDefault();
                if (dataCount != null)
                {
                    var count = Convert.ToInt32(dataCount.ParamValue);
                    _busLoadModel.PowerLoadTable[_seletedNodeLabel].Clear();
                    for (int i = 0; i < count; i++)
                    {
                        _busLoadModel.PowerLoadTable[_seletedNodeLabel].Add(new PowerLoad() {
                            CaseId = SystemManager.Instance.CurrentCaseId,
                            ClientId = SystemManager.Instance.CurrentUser.ClientId,
                            NodeId = _seletedNodeLabel,
                            PeriodId = $"period-{(i+1)}",
                            Load = Convert.ToDouble(dataContent[i < dataContent.Length?i:(dataContent.Length-1)].Split(new string[] { "," }, StringSplitOptions.None)[1]),
                            SettlementDate = SystemManager.Instance.CurrentSettlementDate
                        });
                    }
                    LoadData(_seletedNodeLabel);
                    BindPlot();
                }
            }
        }

        private void comboBoxBus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _seletedNodeLabel = comboBoxBus.SelectedValue.ToString();
            LoadData(_seletedNodeLabel);
            BindPlot();
        }
    }
}
