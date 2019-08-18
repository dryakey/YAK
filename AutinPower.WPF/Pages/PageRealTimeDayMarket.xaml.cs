using Autin.Model;
using AutinPower.WPF.Models;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
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

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageRealTimeDayMarket.xaml
    /// </summary>
    public partial class PageRealTimeDayMarket : Page, INotifyPropertyChanged
    {
        #region properties
        public Action<object[]> NotifyHost { get; set; }
        // 发电出清结果
        public List<GcrJson> GcrData
        {
            get => _gcrData;
            set
            {
                _gcrData = value;
                NotifyPropertyChanged("GcrData");
            }
        }
        // 发电价格
        public List<GpJson> GpData
        {
            get => _gpData;
            set
            {
                _gpData = value;
                NotifyPropertyChanged("GpData");
            }
        }
        //断面电能
        public List<NfgpJson> NfgpData
        {
            get => _nfgpData;
            set
            {
                _nfgpData = value;
                NotifyPropertyChanged("NfgpData");
            }
        }
        // 节点电价
        public List<ObservablePoint> ObservablePointsUnit1SettlePrice { get; set; }
        public List<ObservablePoint> ObservablePointsUnit2SettlePrice { get; set; }
        public List<ObservablePoint> ObservablePointsUnit3SettlePrice { get; set; }
        public List<ObservablePoint> ObservablePointsUnit4SettlePrice { get; set; }
        public List<ObservablePoint> ObservablePointsUnit5SettlePrice { get; set; }

        public List<ObservablePoint> ObservablePointsUser1SystemLoadPredict { get; set; }
        public List<ObservablePoint> ObservablePointsUser2SystemLoadPredict { get; set; }
        public List<ObservablePoint> ObservablePointsUser3SystemLoadPredict { get; set; }
        #endregion

        #region fields
        private PageCircuitPlan _pageCircuitPlan;
        private bool _suspendNavigating;
        private List<GcrJson> _gcrData { get; set; }
        private List<GpJson> _gpData { get; set; }
        private List<NfgpJson> _nfgpData { get; set; }
        #endregion

        public PageRealTimeDayMarket()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
            InitializeUI();

            var unitInfo = SystemManager.Instance.CurrentElement.Where(p => p.ElementType == "un").Select(p =>new string[] { p.ElementId, p.ElementDescription }).ToArray();

            var unitNameList = new List<string>();
            var unitCapacityList = new List<double>();
            var unitTypeList = SystemManager.Instance.CurrentUnitList.UnitParam.GroupBy(g => g.UnitTypeName).Select(p => new { name = p.Key, cnt = p.Count() }).ToList();

            for (int i = 0; i < unitInfo.Length; i++)
            {
                var id = unitInfo[i][0];
                var name = unitInfo[i][1];
                unitNameList.Add(name);
                var capacity = SystemManager.Instance.CurrentUnitList.UnitParam.Where(p=>p.UnitId==id)
                    .Select(p => p.UnitCapacity).FirstOrDefault();
                unitCapacityList.Add(capacity);
                var nodeId = SystemManager.Instance.CurrentUnitList.UnitNode.Where(p => p.IsLinked && p.GeneratorId == id)
                    .Select(p=>p.NodeId).FirstOrDefault();
            }

            //UpdateCapacityInfo(new string[] { "华能玉环", "浙能绍兴", "浙能嘉华", "华电半山燃机", "国电北仑" }, new double[] { 520, 110, 600, 100, 200 });
            //UpdateUnitTypeInfo(new string[] { "火电", "水电", "核电", "燃机", "风电", "光伏", "其他"}, new double[] { 4, 0, 1, 0, 0, 0, 0 });
            //UpdatePowerUserInfo(new string[] { "B地区", "C地区", "D地区" }, new double[] { 1,1,1 });
            UpdateCapacityInfo( unitNameList.ToArray(), unitCapacityList.ToArray());
            UpdateUnitTypeInfo(unitTypeList.Select(p=>p.name).ToArray(), unitTypeList.Select(p => (double)p.cnt).ToArray());
            UpdatePowerUserInfo(new string[] { "B地区", "C地区", "D地区" }, new double[] { 1, 1, 1, 1 });

            frameCircuit.DataContext = this;
            dataGridNfgpData.DataContext = this;
            _pageCircuitPlan = LoadPage();

            uiChart_UnitSettlePrice.DataContext = this;
            ObservablePointsUnit1SettlePrice = new List<ObservablePoint>();
            ObservablePointsUnit2SettlePrice = new List<ObservablePoint>();
            ObservablePointsUnit3SettlePrice = new List<ObservablePoint>();
            ObservablePointsUnit4SettlePrice = new List<ObservablePoint>();
            ObservablePointsUnit5SettlePrice = new List<ObservablePoint>();
            BindUnitSettlePricePlot();

            uiChart_LoadPredict.DataContext = this;
            ObservablePointsUser1SystemLoadPredict = new List<ObservablePoint>();
            ObservablePointsUser2SystemLoadPredict = new List<ObservablePoint>();
            ObservablePointsUser3SystemLoadPredict = new List<ObservablePoint>();
            BindLoadPredictPlot();

        }

        public void InitializeUI()
        {
            gaugeGeneratePrice.BackgroundFill = new SolidColorBrush(Colors.Gold);

            gaugeGeneratePrice.FromValue0 = 0;
            gaugeGeneratePrice.ToValue0 = 30;
            gaugeGeneratePrice.Fill0 = new SolidColorBrush(Colors.GreenYellow);

            gaugeGeneratePrice.ToValue1 = 70;
            gaugeGeneratePrice.Fill1 = new SolidColorBrush(Colors.DodgerBlue);

            gaugeGeneratePrice.Value = 10.5;


            gaugeParamsClaim.BackgroundFill = new SolidColorBrush(Colors.DarkGreen);

            gaugeParamsClaim.FromValue0 = 0;
            gaugeParamsClaim.ToValue0 = 45;
            gaugeParamsClaim.Fill0 = new SolidColorBrush(Colors.OrangeRed);

            gaugeParamsClaim.ToValue1 = 80;
            gaugeParamsClaim.Fill1 = new SolidColorBrush(Colors.DodgerBlue);

            gaugeParamsClaim.Value = 20.5;


            gaugeUserSideClaim.BackgroundFill = new SolidColorBrush(Colors.Gold);

            gaugeUserSideClaim.FromValue0 = 0;
            gaugeUserSideClaim.ToValue0 = 20;
            gaugeUserSideClaim.Fill0 = new SolidColorBrush(Colors.DodgerBlue);

            gaugeUserSideClaim.ToValue1 = 60;
            gaugeUserSideClaim.Fill1 = new SolidColorBrush(Colors.MediumPurple);

            gaugeUserSideClaim.Value = 80.5;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void UpdateCapacityInfo(string[] titles, double[] values)
        {
            doughnutCapacity.SetUpSeries(titles, values);
        }

        public void UpdateUnitTypeInfo(string[] titles, double[] values)
        {
            doughnutUnitType.SetUpSeries(titles, values);
        }

        public void UpdatePowerUserInfo(string[] titles, double[] values)
        {
            doughnutUser.SetUpSeries(titles, values);
        }
        
        private void BindLoadPredictPlot()
        {
            var nodeLabels = new string[] { "B", "C", "D" };
            var nodeList = SystemManager.Instance
                                        .CurrentElement.Where(p => p.ElementType == "ld" && nodeLabels.Contains(p.ElementDescription))
                                        .Select(u => new string[] { u.ElementId, u.ElementDescription+" 地区系统负荷预测（MW）" })
                                        .ToArray();
            var xyMapper = Mappers.Xy<ObservablePoint>().X(p => p.X).Y(p => p.Y);
            var user1LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[0][1]

            };
            var user2LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[1][1]
            };
            var user3LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[2][1]
            };

            user1LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUser1SystemLoadPredict);
            user2LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUser2SystemLoadPredict);
            user3LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUser3SystemLoadPredict);

            uiChart_LoadPredict.SeriesCollection.Clear();
            uiChart_LoadPredict.SeriesCollection.Add(user1LineSerise);
            uiChart_LoadPredict.SeriesCollection.Add(user2LineSerise);
            uiChart_LoadPredict.SeriesCollection.Add(user3LineSerise);
        }

        //private void BindSettlePricePlot()
        //{
        //    var xyMapper = Mappers.Xy<ObservablePoint>().X(p => p.X).Y(p => p.Y);
        //    var setpLineSerise = new LineSeries(xyMapper)
        //    {
        //        LineSmoothness = 0
        //    };
        //    setpLineSerise.Values = new ChartValues<ObservablePoint>(ObservablePoints);
        //    uiChart_SettlePrice.SeriesCollection.Clear();
        //    uiChart_SettlePrice.SeriesCollection.Add(setpLineSerise);
        //}

        private void BindUnitSettlePricePlot()
        {
            var nodeList = SystemManager.Instance
                                        .CurrentElement.Where(p => p.ElementType == "un")
                                        .Select(u => new string[] { u.ElementId, u.ElementDescription })
                                        .ToArray();
            var xyMapper = Mappers.Xy<ObservablePoint>().X(p => p.X).Y(p => p.Y);
            var unit1LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[0][1]

            };
            var unit2LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[1][1]
            };
            var unit3LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[2][1]
            };
            var unit4LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[3][1]
            };
            var unit5LineSerise = new LineSeries(xyMapper)
            {
                LineSmoothness = 0,
                Title = nodeList[4][1]
            };
            unit1LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUnit1SettlePrice);
            unit2LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUnit2SettlePrice);
            unit3LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUnit3SettlePrice);
            unit4LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUnit4SettlePrice);
            unit5LineSerise.Values = new ChartValues<ObservablePoint>(ObservablePointsUnit5SettlePrice);

            uiChart_UnitSettlePrice.SeriesCollection.Clear();
            uiChart_UnitSettlePrice.SeriesCollection.Add(unit1LineSerise);
            uiChart_UnitSettlePrice.SeriesCollection.Add(unit2LineSerise);
            uiChart_UnitSettlePrice.SeriesCollection.Add(unit3LineSerise);
            uiChart_UnitSettlePrice.SeriesCollection.Add(unit4LineSerise);
            uiChart_UnitSettlePrice.SeriesCollection.Add(unit5LineSerise);
        }

        private async void UpdateLoadPredictData()
        {
            await Task.Run(() => {
                var systemLoadPredict = SystemManager.Instance.CurrentUnitList.UnitSystemLoadPredict;
                var nodeLabels = new string[] { "B", "C", "D" };
                var nodeList = SystemManager.Instance
                                            .CurrentElement.Where(p => p.ElementType == "ld" && nodeLabels.Contains(p.ElementDescription))
                                            .Select(u => new string[] { u.ElementId, u.ElementDescription + " 地区" })
                                            .ToArray();

                for (int i = 0; i < nodeList.Length; i++)
                {
                    var unitData = systemLoadPredict.Where(p => p.NodeId.Contains(nodeList[i][0]))
                                         .OrderBy(p => Convert.ToInt32(p.PeriodId.Replace("'", "").Replace("period-", "")))
                                         .Select(p => new ObservablePoint(Convert.ToInt32(p.PeriodId.Replace("'", "").Replace("period-", "")), p.Load))
                                         .ToArray();
                    switch (i)
                    {
                        case 0:
                            {
                                ObservablePointsUser1SystemLoadPredict.Clear();
                                ObservablePointsUser1SystemLoadPredict.AddRange(unitData);
                                break;
                            }
                        case 1:
                            {
                                ObservablePointsUser2SystemLoadPredict.Clear();
                                ObservablePointsUser2SystemLoadPredict.AddRange(unitData);
                                break;
                            }
                        case 2:
                            {
                                ObservablePointsUser3SystemLoadPredict.Clear();
                                ObservablePointsUser3SystemLoadPredict.AddRange(unitData);
                                break;
                            }
                    }
                }

            });
        }

        private async void UpdateNfgPowerData(List<NfgpJson> nfgpData)
        {
            await Task.Run(() => {
                for (int i = 0; i < nfgpData.Count; i++)
                {
                    nfgpData[i].T = nfgpData[i].T.Replace("'", "").Replace(" ","");
                    var nfgLimit = SystemManager.Instance.CurrentUnitList.UnitNfgLimt
                                                .Where(n=>n.PeriodId == nfgpData[i].T && n.NfgId == nfgpData[i].Nfg)
                                                .FirstOrDefault();
                    if (nfgLimit != null)
                    {
                        nfgpData[i].Limt = nfgLimit.NfgLimitValue;
                        nfgpData[i].LoadRate = Math.Round(nfgpData[i].Power*100 / nfgpData[i].Limt, 3);
                    }
                }
                NfgpData = nfgpData;
            });
        }

        private async void UpdateGpData(List<GpJson> gpData)
        {
            await Task.Run(() => {
                var nodeList = SystemManager.Instance
                                            .CurrentElement.Where(p => p.ElementType == "un")
                                            .Select(u => new string[] { u.ElementId, u.ElementDescription })
                                            .ToArray();

                for (int i = 0; i < nodeList.Length; i++)
                {
                    var unitData = gpData.Where(p => p.UnitId.ToString().Contains(nodeList[i][0]))
                                         .OrderBy(p=>Convert.ToInt32(p.T.Replace("'","").Replace("period-","")))
                                         .Select(p => new ObservablePoint(Convert.ToInt32(p.T.Replace("'", "").Replace("period-", "")), p.Lmp))
                                         .ToArray();
                    // 
                    switch (i)
                    {
                        case 0: {
                                ObservablePointsUnit1SettlePrice.Clear();
                                ObservablePointsUnit1SettlePrice.AddRange(unitData);
                                //for (int j = 0; j < unitData.Length; j++)
                                //{
                                //    ObservablePointsUnit1SettlePrice.Add(new ObservablePoint(j, unitData[j].Lmp));
                                //}
                                break;
                            }
                        case 1:
                            {
                                ObservablePointsUnit2SettlePrice.Clear();
                                ObservablePointsUnit2SettlePrice.AddRange(unitData);
                                break;
                            }
                        case 2:
                            {
                                ObservablePointsUnit3SettlePrice.Clear();
                                ObservablePointsUnit3SettlePrice.AddRange(unitData);
                                break;
                            }
                        case 3:
                            {
                                ObservablePointsUnit4SettlePrice.Clear();
                                ObservablePointsUnit4SettlePrice.AddRange(unitData);
                                break;
                            }
                        case 4:
                            {
                                ObservablePointsUnit5SettlePrice.Clear();
                                ObservablePointsUnit5SettlePrice.AddRange(unitData);
                                break;
                            }
                    }
                }
                GpData = gpData;

            });
        }

        private async void UpdateGcrData(List<GcrJson> gcrData)
        {
            await Task.Run(() => GcrData = gcrData);
        }

        private PageCircuitPlan LoadPage()
        {
            _suspendNavigating = false;
            var page = new PageCircuitPlan();
            SetUpPage(page);
            frameCircuit.Navigate(page);
            return page;
        }

        private void SetUpPage(PageCircuitPlan page, params string[] stringParams)
        {
        }

        private void LoadPage(Page page)
        {
            _suspendNavigating = false;
            frameCircuit.Navigate(page);
        }
        
        private async void buttonPrepareSettlement_ClickAsync(object sender, RoutedEventArgs e)
        {
            buttonPrepareSettlement.IsEnabled = false;

            var userId = SystemManager.Instance.CurrentUser.Id;
            var clientId = SystemManager.Instance.CurrentUser.ClientId;
            var clientToken = SystemManager.Instance.CurrentUser.ClientToken;
            var caseId = SystemManager.Instance.CurrentCaseId;
            var settlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);

            var settlementModel = new SettlementModel();
            NotifyHost?.Invoke(new object[] { "showLoading", $"开始准备{caseId} 在 {settlementDate} 出清计算所需数据..." });
            try
            {
                var response = await settlementModel.PrepareSettlement(userId, clientToken, clientId, caseId, settlementDate);
                if (!response.Contains("success"))
                {
                    MessageBox.Show(response);
                }
                else
                {
                    MessageBox.Show($"案例 {caseId} 在 {settlementDate} 的出清计算数据准备完成");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonPrepareSettlement.IsEnabled = true;
            NotifyHost?.Invoke(new object[] { "hideLoading" });
        }

        private async void buttonStartSettlement_ClickAsync(object sender, RoutedEventArgs e)
        {
            buttonStartSettlement.IsEnabled = false;

            var userId = SystemManager.Instance.CurrentUser.Id;
            var clientId = SystemManager.Instance.CurrentUser.ClientId;
            var clientToken = SystemManager.Instance.CurrentUser.ClientToken;
            var caseId = SystemManager.Instance.CurrentCaseId;
            var settlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);

            var settlementModel = new SettlementModel();
            NotifyHost?.Invoke(new object[] { "showLoading", $"开始{caseId} 在 {settlementDate} 的出清计算..." });
            try
            {
                var response = await settlementModel.StartSettlement(userId, clientToken, clientId, caseId, settlementDate);
                if (!response.Contains("success"))
                {
                    MessageBox.Show(response);
                }
                else
                {
                    MessageBox.Show($"案例 {caseId} 在 {settlementDate} 的出清计算结束");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonStartSettlement.IsEnabled = true;
            NotifyHost?.Invoke(new object[] { "hideLoading" });
        }

        private async void buttonPostSettlement_ClickAsync(object sender, RoutedEventArgs e)
        {
            buttonPostSettlement.IsEnabled = false;

            var userId = SystemManager.Instance.CurrentUser.Id;
            var clientId = SystemManager.Instance.CurrentUser.ClientId;
            var clientToken = SystemManager.Instance.CurrentUser.ClientToken;
            var caseId = SystemManager.Instance.CurrentCaseId;
            var settlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);
            SystemManager.Instance.CurrentSettlementDate = settlementDate;

            var settlementModel = new SettlementModel();
            NotifyHost?.Invoke(new object[] { "showLoading", $"正在提取{caseId} 在 {settlementDate} 出清计算结果..." });
            try
            {
                var response = await settlementModel.PostSettlement(userId, clientToken, clientId, caseId, settlementDate);
                if (!response.Contains("success"))
                {
                    MessageBox.Show(response);
                }
                else
                {
                    var settlementResult = SettlementResult.FromJson(response);
                    if (settlementResult.Result == "success")
                    {
                        NotifyHost?.Invoke(new object[] { "showLoading", $"正在获取{caseId}断面信息..." });
                        var unitModel = new UnitModel();
                        var nfglimitresponse = await unitModel.GetUnitNfgLimitRequest();

                        if (!nfglimitresponse.Contains("success"))
                        {
                            MessageBox.Show(nfglimitresponse);
                            buttonPostSettlement.IsEnabled = true;
                            NotifyHost?.Invoke(new object[] { "hideLoading" });
                            return;
                        }
                        else
                        {
                            await this.Dispatcher.BeginInvoke(new Action<List<NfgpJson>>(UpdateNfgPowerData), new object[] { settlementResult.Data.NfgpJson });
                        }

                        NotifyHost?.Invoke(new object[] { "showLoading", $"正在获取{caseId} 在 {settlementDate}的系统负荷预测..." });
                        var systemloadresponse = await unitModel.GetSystemLoadPredictRequest();

                        if (!systemloadresponse.Contains("success"))
                        {
                            MessageBox.Show(systemloadresponse);
                            buttonPostSettlement.IsEnabled = true;
                            NotifyHost?.Invoke(new object[] { "hideLoading" });
                            return;
                        }
                        else
                        {
                            BindLoadPredictPlot();
                            await this.Dispatcher.BeginInvoke(new Action(UpdateLoadPredictData));
                        }

                        BindUnitSettlePricePlot();
                        await this.Dispatcher.BeginInvoke(new Action<List<GpJson>>(UpdateGpData), new object[] { settlementResult.Data.GpJson });

                        //GcrData = settlementResult.Data.GcrJson;
                        //GpData = settlementResult.Data.GpJson;

                        MessageBox.Show($"成功获取案例 {caseId} 在 {settlementDate} 的出清计算结果");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonPostSettlement.IsEnabled = true;
            NotifyHost?.Invoke(new object[] { "hideLoading" });
        }
    }
}
