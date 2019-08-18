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
using AutinPower.WPF.Models;
using AutinPower.WPF.Pages;
using Autin.Model;
using System.ComponentModel;
using System.Windows.Resources;

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageMainSystem.xaml
    /// </summary>
    public partial class PageMainSystem : Page, IAutinPage, INotifyPropertyChanged
    {
        public Action<object[]> NotifyHost { get; set; }
        public LoginModel LoginModel { get; set; }
        public ControlInfoModel ControlInfoModel { get; set; }
        public UnitModel UnitModel { get; set; }
        public AnalysisCaseModel AnalysisCaseModel { get; set; }
        public string OperateDate { get=>_operateDate;
            set {
                _operateDate = value;
            }
        }
        public bool SuspendNavigating { get => _suspendNavigating; set => _suspendNavigating = value; }
        public DateTime SelectedSettlementDate {
            get => SystemManager.Instance.CurrentSettlementDate.Date;
            set {
                SystemManager.Instance.CurrentSettlementDate = new DateTimeOffset(value);
                NotifyPropertyChanged("SelectedSettlementDate");
            } }

        private string _operateDate;
        private bool _suspendNavigating;
        private bool _expandItem;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public PageMainSystem()
        {
            InitializeComponent();
            InitializeTreeViewMenu();
            BindCurrentUser();
            textboxOperateDate.DataContext = this;
            datePickerOperateTime.DataContext = this;
            OperateDate = DateTime.Now.ToString("yyyy年MM月dd日");
            _expandItem = true;
        }

        public void InitializeTreeViewMenu()
        {
            var treeViewMenus = new List<TreeMenuModel>() {
                new TreeMenuModel()
                {
                    MenuName = "模型参数维护",
                    SubMennus = new System.Collections.ObjectModel.ObservableCollection<SubMenuModel>()
                    {
                        new SubMenuModel(){ MenuName="电力用户参数维护" },
                        new SubMenuModel(){ MenuName="电网参数维护" }
                    }
                },
                new TreeMenuModel()
                {
                    MenuName = "信息发布",
                    SubMennus = new System.Collections.ObjectModel.ObservableCollection<SubMenuModel>()
                    {
                        new SubMenuModel(){ MenuName="预测信息" },
                        new SubMenuModel(){ MenuName="历史信息" }
                    }
                },
                new TreeMenuModel()
                {
                    MenuName = "日前市场",
                    SubMennus = new System.Collections.ObjectModel.ObservableCollection<SubMenuModel>()
                    {
                        new SubMenuModel(){ MenuName="发电侧日前申报" },
                        new SubMenuModel(){ MenuName="用电侧日前申报" },
                        new SubMenuModel(){ MenuName="日前市场运行" },
                        new SubMenuModel(){ MenuName="日前市场交易结果查询" },
                    }
                },
                new TreeMenuModel()
                {
                    MenuName = "实时市场",
                    SubMennus = new System.Collections.ObjectModel.ObservableCollection<SubMenuModel>()
                    {
                        new SubMenuModel(){ MenuName="实时市场信息" }
                    }
                }
            };
            treeviewMenu.ItemsSource = treeViewMenus;
        }

        public void BindCurrentUser()
        {
            uiUser.User = SystemManager.Instance.CurrentUser;
            uiUser.LogoutAction = new Action(async () => {
                var logoutResponse = await LoginModel.LogoutRequest();
                NotifyHost?.Invoke(new object[] {
                    logoutResponse
                });
            });
        }

        private async Task<string> GetAnalysisCaseInfo()
        {
            var response = await AnalysisCaseModel.GetAnalysisCaseRequest();
            return response;
        }

        private async Task<string> GetControlInfo()
        {
           var response = await ControlInfoModel.GetInfoRequest();
            return response;
        }

        private async Task<string> GetUnitInfo()
        {
            var response = await UnitModel.GetUnitParamRequest();

            response += await UnitModel.GetUnitInitialParamRequest();

            response += await UnitModel.GetUnitStartPriceRequest();

            response += await UnitModel.GetUnitNodeRequest();

            response += await UnitModel.GetElementListRequest();
            
            return response;
        }

        private T LoadPage<T>()
        {
            _suspendNavigating = false;
            var page = (T)Activator.CreateInstance(typeof(T));
            SetUpPage<T>(page);
            frameContent.Navigate(page);
            return page;
        }

        private void SetUpPage<T>(T page)
        {
            switch (typeof(T).Name)
            {
                case "PageUnitParamsConfig":
                    {
                        var pageOp = page as PageUnitParamsConfig;
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            switch (objs[0].ToString())
                            {
                                case "showLoading":
                                    {
                                        var info = objs[1].ToString();
                                        ShowLoading(info);
                                        break;
                                    }
                                case "hideLoading":
                                    {
                                        HideLoading();
                                        break;
                                    }
                            }
                        });
                        break;
                    }
                case "PageGeneratorSideDayDeclare":
                    {
                        var pageOp = page as PageGeneratorSideDayDeclare;
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            switch (objs[0].ToString())
                            {
                                case "showLoading":
                                    {
                                        var info = objs[1].ToString();
                                        ShowLoading(info);
                                        break;
                                    }
                                case "hideLoading":
                                    {
                                        HideLoading();
                                        break;
                                    }
                            }
                        });
                        break;
                    }
                case "PageRealTimeDayMarket":
                    {
                        var pageOp = page as PageRealTimeDayMarket;
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            switch (objs[0].ToString())
                            {
                                case "showLoading":
                                    {
                                        var info = objs[1].ToString();
                                        ShowLoading(info);
                                        break;
                                    }
                                case "hideLoading":
                                    {
                                        HideLoading();
                                        break;
                                    }
                            }
                        });
                        break;
                    }
            }
        }

        private void LoadPage(Page page)
        {
            _suspendNavigating = false;
            frameContent.Navigate(page);
        }

        private void LoadPageByMenuName(SubMenuModel menuModel)
        {
            Page page = null;
            switch (menuModel.MenuName)
            {
                case "电力用户参数维护":
                    {
                        page = LoadPage<PageUnitParamsConfig>();
                        break;
                    }
                case "电网参数维护":
                    {
                        page = LoadPage<PageNetworkConfig>();
                        break;
                    }
                case "预测信息":
                    {
                        page = LoadPage<PageInformationConfig>();
                        break;
                    }
                case "发电侧日前申报":
                    {
                        page = LoadPage<PageGeneratorSideDayDeclare>();
                        break;
                    }
                case "用电侧日前申报":
                    {
                        page = LoadPage<PageConsumerSideDayDeclare>();
                        break;
                    }
                case "日前市场运行":
                    {
                        page = LoadPage <PageRealTimeDayMarket>();
                        break;
                    }
            }
            menuModel.Page = page;
        }

        public void ShowLoading(string info)
        {
            uiLoadingInfo.Visibility = Visibility.Visible;
            uiLoadingInfo.LoadingInfo = info;
        }

        public void HideLoading()
        {
            uiLoadingInfo.Visibility = Visibility.Hidden;
        }

        private void OnTreeviewMenuItemClicked(object sender, MouseButtonEventArgs e)
        {
            var subItem = (sender as TreeViewItem).DataContext as SubMenuModel;
            if (subItem != null)
            {
                if (subItem.Page != null)
                {
                    LoadPage(subItem.Page);
                }
                else
                {
                    LoadPageByMenuName(subItem);
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowLoading("读取案例信息...");
                await GetAnalysisCaseInfo();
                ShowLoading("读取系统配置信息...");
                await GetControlInfo();
                ShowLoading("读取机组配置信息...");
                await GetUnitInfo();
                SelectedSettlementDate = SystemManager.Instance.CurrentSettlementDate.Date;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadPage<PageUnitParamsConfig>();
            HideLoading();
        }

        private void buttonTraingScenarioManagement_Click(object sender, RoutedEventArgs e)
        {
            buttonTraingScenarioManagement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonTraingScenarioManagement.Foreground = new SolidColorBrush(Colors.White);

            buttonSpotTrading.Background = new SolidColorBrush(Colors.Transparent);
            buttonSpotTrading.Foreground = new SolidColorBrush(Colors.Black);

            buttonContractTrading.Background = new SolidColorBrush(Colors.Transparent);
            buttonContractTrading.Foreground = new SolidColorBrush(Colors.Black);

            buttonMarketSettlement.Background = new SolidColorBrush(Colors.Transparent);
            buttonMarketSettlement.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void buttonSpotTrading_Click(object sender, RoutedEventArgs e)
        {
            buttonTraingScenarioManagement.Background = new SolidColorBrush(Colors.Transparent);
            buttonTraingScenarioManagement.Foreground = new SolidColorBrush(Colors.Black);

            buttonSpotTrading.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonSpotTrading.Foreground = new SolidColorBrush(Colors.White);

            buttonContractTrading.Background = new SolidColorBrush(Colors.Transparent);
            buttonContractTrading.Foreground = new SolidColorBrush(Colors.Black);

            buttonMarketSettlement.Background = new SolidColorBrush(Colors.Transparent);
            buttonMarketSettlement.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonContractTrading_Click(object sender, RoutedEventArgs e)
        {
            buttonTraingScenarioManagement.Background = new SolidColorBrush(Colors.Transparent);
            buttonTraingScenarioManagement.Foreground = new SolidColorBrush(Colors.Black);

            buttonSpotTrading.Background = new SolidColorBrush(Colors.Transparent);
            buttonSpotTrading.Foreground = new SolidColorBrush(Colors.Black);

            buttonContractTrading.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonContractTrading.Foreground = new SolidColorBrush(Colors.White);

            buttonMarketSettlement.Background = new SolidColorBrush(Colors.Transparent);
            buttonMarketSettlement.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonMarketSettlement_Click(object sender, RoutedEventArgs e)
        {
            buttonTraingScenarioManagement.Background = new SolidColorBrush(Colors.Transparent);
            buttonTraingScenarioManagement.Foreground = new SolidColorBrush(Colors.Black);

            buttonSpotTrading.Background = new SolidColorBrush(Colors.Transparent);
            buttonSpotTrading.Foreground = new SolidColorBrush(Colors.Black);

            buttonContractTrading.Background = new SolidColorBrush(Colors.Transparent);
            buttonContractTrading.Foreground = new SolidColorBrush(Colors.Black);

            buttonMarketSettlement.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonMarketSettlement.Foreground = new SolidColorBrush(Colors.White);
        }

        private void buttonCase_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void buttonStartCalculate_Click(object sender, RoutedEventArgs e)
        {
            buttonStartCalculate.IsEnabled = false;

            var userId = SystemManager.Instance.CurrentUser.Id;
            var clientId = SystemManager.Instance.CurrentUser.ClientId;
            var clientToken = SystemManager.Instance.CurrentUser.ClientToken;
            var caseId = SystemManager.Instance.CurrentCaseId;
            var settlementDate = new DateTimeOffset(SystemManager.Instance.CurrentSettlementDate.Date, TimeSpan.Zero);

            var settlementModel = new SettlementModel();

            ShowLoading("开始出清计算...");
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
            buttonStartCalculate.IsEnabled = true;
            HideLoading();
        }

        private void ImageExpand_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _expandItem = !_expandItem;
            if (_expandItem)
            {
                gridTreeView.Width = 220;
                gridTreeView.Visibility = Visibility.Visible;
                stackpanelMiniLogo.Visibility = Visibility.Collapsed;
                buttonExpand.Tag = "Left";
                var resourceUri = new Uri("/AutinPower.WPF;component/Images/indent-lef-none.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = temp;
                buttonExpand.Background = brush;
            }
            else
            {
                gridTreeView.Width = 0;
                gridTreeView.Visibility = Visibility.Collapsed;
                stackpanelMiniLogo.Visibility = Visibility.Visible;
                buttonExpand.Tag = "Right";
                var resourceUri = new Uri("/AutinPower.WPF;component/Images/indent-right-none.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = temp;
                buttonExpand.Background = brush;
            }
        }

        private void buttonExpand_MouseEnter(object sender, MouseEventArgs e)
        {
            var tag = buttonExpand.Tag.ToString();
            switch (tag)
            {
                case "Left":
                    {
                        var resourceUri = new Uri("/AutinPower.WPF;component/Images/indent-left.png", UriKind.Relative);
                        StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                        BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = temp;
                        buttonExpand.Background = brush;
                        break;
                    }
                case "Right":
                    {
                        var resourceUri = new Uri("/AutinPower.WPF;component/Images/indent-right.png", UriKind.Relative);
                        StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                        BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = temp;
                        buttonExpand.Background = brush;
                        break;
                    }
            }
        }

        private void buttonExpand_MouseLeave(object sender, MouseEventArgs e)
        {
            var tag = buttonExpand.Tag.ToString();
            switch (tag)
            {
                case "Left":
                    {
                        var resourceUri = new Uri("/AutinPower.WPF;component/Images/indent-lef-none.png", UriKind.Relative);
                        StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                        BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = temp;
                        buttonExpand.Background = brush;
                        break;
                    }
                case "Right":
                    {
                        var resourceUri = new Uri("/AutinPower.WPF;component/Images/indent-right-none.png", UriKind.Relative);
                        StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                        BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = temp;
                        buttonExpand.Background = brush;
                        break;
                    }
            }
        }
    }
}
