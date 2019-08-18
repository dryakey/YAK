using AutinPower.WPF.Models;
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
using Autin.Model;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageUnitParamsConfig.xaml
    /// </summary>
    public partial class PageUnitParamsConfig : Page, INotifyPropertyChanged
    {
        public PageUnitParamsConfig()
        {
            InitializeComponent();
            BindUnitList();
            comboBoxUnit.SelectedIndex = 0;
            ObservablePoints = new List<ObservablePoint>();
        }

        #region properties
        public Action<object[]> NotifyHost { get; set; }
        public string[] UnitLabels
        {
            get => _unitLabels;
            set
            {
                _unitLabels = value;
                NotifyPropertyChanged("UnitLabels");
            }
        }
        public string SeletedUnitLabel { get => _seletedUnitLabel; set => _seletedUnitLabel = value; }
        public UnitParamsConfigModel UnitParamsConfigModel { get => _unitParamsConfigModel;
            set {
                _unitParamsConfigModel = value;
                NotifyPropertyChanged("UnitParamsConfigModel");
            } }
        public List<ObservablePoint> ObservablePoints { get; set; }
        #endregion

        #region fields
        private UnitParamsConfigModel _unitParamsConfigModel;
        private string[] _unitLabels;
        public string _seletedUnitLabel;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void BindUnitParamsConfigModel()
        {
            if (_unitParamsConfigModel == null)
            {
                _unitParamsConfigModel = new UnitParamsConfigModel();
            }
            
            gridMain.DataContext = UnitParamsConfigModel;
            dataGridPowerCost.DataContext = UnitParamsConfigModel;
            BindPlot();
        }

        private async Task FillInUnitParamsConfigModelAsync(string unitLabel)
        {
            if (SystemManager.Instance.CurrentUnitList.UnitConfiguration == null)
            {
                SystemManager.Instance.CurrentUnitList.UnitConfiguration = new List<IAutinDataModel>();
            }

            var unitId = GetUnitId(unitLabel);
            if (SystemManager.Instance.CurrentUnitList.UnitConfiguration.Any(p => p.Key == unitId && p.DataDate == SystemManager.Instance.CurrentSettlementDate))
            {
                _unitParamsConfigModel = (UnitParamsConfigModel)SystemManager.Instance.CurrentUnitList.UnitConfiguration.Where(p => p.Key == unitId && p.DataDate== SystemManager.Instance.CurrentSettlementDate).FirstOrDefault();
                LoadEnergyCostToObservableArray(_unitParamsConfigModel.EnergyCost);
            }
            else
            {
                var unitParamsConfigModel = new UnitParamsConfigModel();
                unitParamsConfigModel.UserId = SystemManager.Instance.CurrentUser.Id;
                unitParamsConfigModel.ClientToken = SystemManager.Instance.CurrentUser.ClientToken;
                unitParamsConfigModel.Unit = unitLabel;
                unitParamsConfigModel.Key = unitId;
                unitParamsConfigModel.DataDate = SystemManager.Instance.CurrentSettlementDate;

                unitParamsConfigModel.ColdStartCost = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitCostColdStart).FirstOrDefault();
                unitParamsConfigModel.WarmStartCost = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitCostWarmStart).FirstOrDefault();
                unitParamsConfigModel.HotStartCost = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitCostHotStart).FirstOrDefault();
                unitParamsConfigModel.NoloadCost = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitCostAverage).FirstOrDefault();
                //unitParamsConfigModel.MaximumStartTimes = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.).FirstOrDefault();
                unitParamsConfigModel.MinimumRunningHours = (long)SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitMinOntime).FirstOrDefault();
                unitParamsConfigModel.LoadRate = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitRampUp).FirstOrDefault();

                unitParamsConfigModel.Connector = SystemManager.Instance.CurrentUnitList.UnitNode.Where(p => p.IsLinked && p.GeneratorId == unitId).Select(p => p.NodeId).FirstOrDefault();
                unitParamsConfigModel.UnitType = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitType).FirstOrDefault();
                unitParamsConfigModel.UnitTypeName = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitTypeName).FirstOrDefault();
                unitParamsConfigModel.RatedCapacity = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitCapacity).FirstOrDefault();
                unitParamsConfigModel.MaximumTechOutput = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitPmax).FirstOrDefault();
                unitParamsConfigModel.MinimumTechOutput = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitPmin).FirstOrDefault();
                //unitParamsConfigModel.TimeToOngridFromColdStar = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.).FirstOrDefault();
                //unitParamsConfigModel.TimeToOngridFromWarmStar = ;
                //unitParamsConfigModel.TimeToOngridFromHotStar =;
                unitParamsConfigModel.TimeToWarmFromHot = (long)SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitTimeToWarm).FirstOrDefault();
                unitParamsConfigModel.TimeToColdFromHot = (long)SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitTimeToCold).FirstOrDefault();
                //unitParamsConfigModel.StateChangeCost =;
                //unitParamsConfigModel.FixCost =;
                //unitParamsConfigModel.FmMaximumTechOutput =;
                //unitParamsConfigModel.FmMinimumTechOutput =;
                unitParamsConfigModel.PlantPowerConsumingRate = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitSelfUseFact).FirstOrDefault();
                unitParamsConfigModel.GridLossCoefficient = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p => p.UnitId == unitId).Select(p => p.UnitLossFact).FirstOrDefault();

                var data = new List<EnergyCost>();
                var unitModel = new UnitModel();
                NotifyHost?.Invoke(new object[] { "showLoading", $"读取{unitLabel} ({unitId}) {SystemManager.Instance.CurrentSettlementDate.Date.ToShortDateString()}的电能量成本数据..." });
                var response = await unitModel.GetEnergyCostRequest(unitId);
                NotifyHost?.Invoke(new object[] { "hideLoading" });
                if (response != "fails")
                {
                    var powerCosts = PowerCost.FromJson(response);
                    data = powerCosts.Select(p => new EnergyCost() { PeriodId = p.PeriodId, GenOutput = p.GenOutput, DeclarePrice = p.DeclarePrice }).ToList();
                }
                else
                {
                    var costNumObject = SystemManager.Instance.CurrentControlInfoList.Where(p => p.ParamName == "CostNum").FirstOrDefault();
                    if (costNumObject != null)
                    {
                        var costNum = Convert.ToInt32(costNumObject.ParamValue);
                        ObservablePoints.Clear();
                        //ObservablePoints.Add(new ObservablePoint() { X = 0, Y = 0 });
                        for (int i = 0; i < costNum; i++)
                        {
                            data.Add(new EnergyCost { PeriodId = $"{i + 1}", GenOutput = 0, DeclarePrice = 0 });
                            //ObservablePoints.Add(new ObservablePoint() { X = 0, Y = 0 });
                            //ObservablePoints.Add(new ObservablePoint() { X = 0, Y = 0 });
                        }
                    }
                }
                unitParamsConfigModel.EnergyCost = data;
                SystemManager.Instance.CurrentUnitList.UnitConfiguration.RemoveAll(p => p.Key == unitId && p.DataDate == SystemManager.Instance.CurrentSettlementDate);
                SystemManager.Instance.CurrentUnitList.UnitConfiguration.Add(unitParamsConfigModel);
                _unitParamsConfigModel = unitParamsConfigModel;
                LoadEnergyCostToObservableArray(_unitParamsConfigModel.EnergyCost);
            }
        }

        private void BindPlot()
        {
            var xyMapper = Mappers.Xy<ObservablePoint>().X(p => p.X).Y(p => p.Y);
            var setpLineSerise = new LineSeries(xyMapper) {
                LineSmoothness = 0
            };
            setpLineSerise.Values = new ChartValues<ObservablePoint>(ObservablePoints);
            uiChart.SeriesCollection.Clear();
            uiChart.SeriesCollection.Add(setpLineSerise);
        }

        private void LoadEnergyCostToObservableArray(List<EnergyCost> energyCosts)
        {
            ObservablePoints.Clear();
            ObservablePoints.Add(new ObservablePoint(0, energyCosts[0].DeclarePrice));
            //ObservablePoints.Add(new ObservablePoint(energyCosts[0].GenOutput, 0));
            int i = 0;
            for (i = 0; i < energyCosts.Count - 1; i++)
            {
                ObservablePoints.Add(new ObservablePoint(energyCosts[i].GenOutput, energyCosts[i].DeclarePrice));
                ObservablePoints.Add(new ObservablePoint(energyCosts[i].GenOutput, energyCosts[i+1].DeclarePrice));
            }
            ObservablePoints.Add(new ObservablePoint(energyCosts[i].GenOutput, energyCosts[i].DeclarePrice));
        }

        public void BindUnitList()
        {
            comboBoxUnit.DataContext = this;
            UnitLabels = SystemManager.Instance.CurrentElement.Where(p=>p.ElementType=="un").Select(p => p.ElementDescription).ToArray();
        }
        
        private string GetUnitId(string unitLabel)
        {
            return SystemManager.Instance.CurrentElement.Where(p => p.ElementDescription == unitLabel).Select(p => p.ElementId).FirstOrDefault().ToString();
        }

        private async void comboBoxUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // cache
            if (!string.IsNullOrEmpty(_seletedUnitLabel))
            {
                var unitId = GetUnitId(_seletedUnitLabel);
                SystemManager.Instance.CurrentUnitList.UnitConfiguration.RemoveAll(p => p.Key == unitId && p.DataDate == SystemManager.Instance.CurrentSettlementDate);
                SystemManager.Instance.CurrentUnitList.UnitConfiguration.Add(_unitParamsConfigModel);
            }
            // switch
            _seletedUnitLabel = comboBoxUnit.SelectedValue.ToString();
            await FillInUnitParamsConfigModelAsync(_seletedUnitLabel);
            BindUnitParamsConfigModel();
        }

        private void dataGridPowerCost_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var rowIndex = e.Row.GetIndex();
                var colIndex = e.Column.DisplayIndex;
                var t = e.EditingElement as TextBox;
                var value = Convert.ToInt64(t.Text);
                if (colIndex == 1)
                {
                    var ridx = 2 * rowIndex + 1;
                    if (ridx < ObservablePoints.Count - 1)
                    {
                        ObservablePoints[ridx + 1].X = value;
                    }
                    ObservablePoints[ridx].X = value;
                }
                else if(colIndex == 2)
                {
                    var ridx = 2 * rowIndex;
                    ObservablePoints[ridx].Y = value;
                    ObservablePoints[ridx + 1].Y = value;
                }
            }
        }

        private async void buttonSubmit_ClickAsync(object sender, RoutedEventArgs e)
        {
            buttonSubmit.IsEnabled = false;
            _unitParamsConfigModel.UserId = SystemManager.Instance.CurrentUser.Id;
            _unitParamsConfigModel.ClientId = SystemManager.Instance.CurrentUser.ClientId;
            _unitParamsConfigModel.ClientToken = SystemManager.Instance.CurrentUser.ClientToken;
            _unitParamsConfigModel.CaseId = SystemManager.Instance.CurrentCaseId;
            _unitParamsConfigModel.SettlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);
            _unitParamsConfigModel.UnitId = GetUnitId(_seletedUnitLabel);

            var data = _unitParamsConfigModel.ToJson();
            var unitModel = new UnitModel();
            try
            {
                var response = await unitModel.SetUnitConfigRequest(data);
                if (!response.Contains("success"))
                {
                    MessageBox.Show(response);
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
