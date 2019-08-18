using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AutinPower.WPF.Models;
using Autin.Model;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.Win32;
using System.IO;

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageConsumerSideDayDeclare.xaml
    /// </summary>
    public partial class PageConsumerSideDayDeclare : Page, INotifyPropertyChanged
    {

        #region properties
        public Action<object[]> NotifyHost { get; set; }
        public List<ObservablePoint> ObservablePoints { get; set; }
        public List<PowerLoad> PowerLoad
        {
            get => _powerLoad;
            set
            {
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
        private string[] _nodeLabels;
        private string _seletedNodeLabel;
        private List<PowerLoad> _powerLoad;
        private PowerLoadModel _busLoadModel;
        private string _currentOption;
        #endregion

        public PageConsumerSideDayDeclare()
        {
            InitializeComponent();
            gridMain.DataContext = this;
            _busLoadModel = new PowerLoadModel();
            InitializeUI();
            comboBoxUnit.SelectedIndex = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void SetEnergyDeclareTable()
        {
            if (PowerLoad == null)
            {
                PowerLoad = new List<PowerLoad>();
            }
            var table = new TableModel()
            {
                TableColumns = new Models.TableColumn[] {
                    new Models.TableColumn{ HeaderText="序号", HeaderWidth = 70, BindingPath="Id" },
                    new Models.TableColumn{ HeaderText="交易时段", HeaderWidth = 100, BindingPath="PeriodId" },
                    new Models.TableColumn{ HeaderText="申报电力 (MW)", HeaderWidth = 100, BindingPath="Load" },
                }
            };
            datagridTable.Columns.Clear();
            foreach (var tableColumn in table.TableColumns)
            {
                datagridTable.Columns.Add(new DataGridTextColumn()
                {
                    Header = tableColumn.HeaderText,
                    Binding = new Binding(tableColumn.BindingPath) { Mode = BindingMode.TwoWay },
                    Width = tableColumn.HeaderWidth
                });
            }
            textblockPlotTitle.Text = "日前市场申报";
            _currentOption = "日前市场申报";
        }

        private void SetContractQueryTable()
        {
            var table = new TableModel()
            {
                TableColumns = new Models.TableColumn[] {
                    new Models.TableColumn{ HeaderText="序号", HeaderWidth = 70, BindingPath="ID" },
                    new Models.TableColumn{ HeaderText="签约方", HeaderWidth = 100, BindingPath="Contractor" },
                    new Models.TableColumn{ HeaderText="合约价格 (元/MW)", HeaderWidth = 100, BindingPath="ContractPrice" },
                    new Models.TableColumn{ HeaderText="最大合约量（MW）", HeaderWidth = 100, BindingPath="MaximumContractPower" },
                    new Models.TableColumn{ HeaderText="最小合约量（MW）", HeaderWidth = 100, BindingPath="MinimumContractPower" },
                }
            };
            datagridTable.Columns.Clear();
            foreach (var tableColumn in table.TableColumns)
            {
                datagridTable.Columns.Add(new DataGridTextColumn()
                {
                    Header = tableColumn.HeaderText,
                    Binding = new Binding(tableColumn.BindingPath) { Mode = BindingMode.TwoWay },
                    Width = tableColumn.HeaderWidth
                });
            }
            textblockPlotTitle.Text = "售电公司合约曲线";
            _currentOption = "售电公司合约曲线";
        }

        private void SetAgentUserPredictTable()
        {
            var table = new TableModel()
            {
                TableColumns = new Models.TableColumn[] {
                    new Models.TableColumn{ HeaderText="序号", HeaderWidth = 70, BindingPath="ID" },
                    new Models.TableColumn{ HeaderText="调度时段", HeaderWidth = 100, BindingPath="TunePeriod" },
                    new Models.TableColumn{ HeaderText="用户1", HeaderWidth = 100, BindingPath="User1" },
                    new Models.TableColumn{ HeaderText="用户2", HeaderWidth = 100, BindingPath="User2" },
                    new Models.TableColumn{ HeaderText="合计（MW）", HeaderWidth = 100, BindingPath="TotalPower" },
                }
            };
            datagridTable.Columns.Clear();
            foreach (var tableColumn in table.TableColumns)
            {
                datagridTable.Columns.Add(new DataGridTextColumn()
                {
                    Header = tableColumn.HeaderText,
                    Binding = new Binding(tableColumn.BindingPath) { Mode = BindingMode.TwoWay },
                    Width = tableColumn.HeaderWidth
                });
            }
            textblockPlotTitle.Text = "运行日代理用户用电负荷预测";
            _currentOption = "运行日代理用户用电负荷预测";
        }

        private void InitializeUI()
        {
            ObservablePoints = new List<ObservablePoint>();
            buttonEnergyDeclare.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonEnergyDeclare.Foreground = new SolidColorBrush(Colors.White);
            NodeLabels = new string[] { "B", "C", "D" };
            SetEnergyDeclareTable();
            foreach (var nodelLabel in NodeLabels)
            {
                _busLoadModel.PowerLoadTable.Add(nodelLabel, new List<PowerLoad>());
            }
        }

        private void BindData(string nodeLabel)
        {
            switch (_currentOption)
            {
                case "日前市场申报":
                    {
                        if (_busLoadModel.PowerLoadTable.Keys.Contains(nodeLabel))
                        {
                            PowerLoad = _busLoadModel.PowerLoadTable[nodeLabel];
                            datagridTable.ItemsSource = PowerLoad;
                        }
                        break;
                    }
            }
        }

        private void LoadData(string nodeLabel)
        {
            switch (_currentOption)
            {
                case "日前市场申报":
                    {
                        if (_busLoadModel.PowerLoadTable.Keys.Contains(nodeLabel))
                        {
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
                                            Id = j + 1,
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
                            var i = 1;
                            ObservablePoints.Clear();
                            foreach (var data in _powerLoad)
                            {
                                ObservablePoints.Add(new ObservablePoint((double)i, data.Load));
                                i++;
                            }
                        }
                        break;
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

        private void buttonEnergyDeclare_Click(object sender, RoutedEventArgs e)
        {
            buttonAgentUserPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonAgentUserPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonContractQuery.Background = new SolidColorBrush(Colors.Transparent);
            buttonContractQuery.Foreground = new SolidColorBrush(Colors.Black);

            buttonEnergyDeclare.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonEnergyDeclare.Foreground = new SolidColorBrush(Colors.White);

            SetEnergyDeclareTable();
        }

        private void buttonContractQuery_Click(object sender, RoutedEventArgs e)
        {

            buttonAgentUserPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonAgentUserPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonContractQuery.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonContractQuery.Foreground = new SolidColorBrush(Colors.White);

            buttonEnergyDeclare.Background = new SolidColorBrush(Colors.Transparent);
            buttonEnergyDeclare.Foreground = new SolidColorBrush(Colors.Black);

            SetContractQueryTable();
        }

        private void buttonAgentUserPredict_Click(object sender, RoutedEventArgs e)
        {
            buttonAgentUserPredict.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonAgentUserPredict.Foreground = new SolidColorBrush(Colors.White);

            buttonContractQuery.Background = new SolidColorBrush(Colors.Transparent);
            buttonContractQuery.Foreground = new SolidColorBrush(Colors.Black);

            buttonEnergyDeclare.Background = new SolidColorBrush(Colors.Transparent);
            buttonEnergyDeclare.Foreground = new SolidColorBrush(Colors.Black);

            SetAgentUserPredictTable();
        }

        private void datagridTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var rowIndex = e.Row.GetIndex();
                var colIndex = e.Column.DisplayIndex;
                var t = e.EditingElement as TextBox;
                var value = Convert.ToInt64(t.Text);
                if (colIndex == 2)
                {
                    ObservablePoints[rowIndex].Y = value;
                }
            }
        }

        private void comboBoxUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _seletedNodeLabel = comboBoxUnit.SelectedValue.ToString();
            // var nodeLabel = SystemManager.Instance.CurrentElement.Where(p => p.ElementType == "ld" && _seletedNodeLabel == p.ElementDescription).Select(p => p.ElementId).FirstOrDefault();
            BindData(_seletedNodeLabel);
            LoadData(_seletedNodeLabel);
            BindPlot();
        }

        private void buttonImport_Click(object sender, RoutedEventArgs e)
        {
            try
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
                            _busLoadModel.PowerLoadTable[_seletedNodeLabel].Add(new PowerLoad()
                            {
                                Id = i + 1,
                                CaseId = SystemManager.Instance.CurrentCaseId,
                                ClientId = SystemManager.Instance.CurrentUser.ClientId,
                                NodeId = _seletedNodeLabel,
                                PeriodId = $"period-{(i + 1)}",
                                Load = Convert.ToDouble(dataContent[i < dataContent.Length ? i : (dataContent.Length - 1)].Split(new string[] { "," }, StringSplitOptions.None)[1]),
                                SettlementDate = SystemManager.Instance.CurrentSettlementDate
                            });
                        }
                        // _busLoadModel.PowerLoadTable[_seletedNodeLabel]
                        // BindData(_seletedNodeLabel);
                        LoadData(_seletedNodeLabel);
                        datagridTable.Items.Refresh();
                        BindPlot();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            buttonSubmit.IsEnabled = false;
            var response = "";
            var userId = SystemManager.Instance.CurrentUser.Id;
            var clientId = SystemManager.Instance.CurrentUser.ClientId;
            var clientToken = SystemManager.Instance.CurrentUser.ClientToken;
            var caseId = SystemManager.Instance.CurrentCaseId;
            var settlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);
            var unitModel = new UnitModel();
            try
            {
                switch (_currentOption)
                {
                    case "日前市场申报":
                        {
                            _busLoadModel.PowerLoadTable[_seletedNodeLabel] = _powerLoad;
                            response = await unitModel.SetConsumerDeclareRequest(userId, _seletedNodeLabel, caseId, clientToken, clientId, settlementDate, _powerLoad.ToJson());
                            if (!response.Contains("success"))
                            {
                                MessageBox.Show(response);
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonSubmit.IsEnabled = true;
        }
    }
}
