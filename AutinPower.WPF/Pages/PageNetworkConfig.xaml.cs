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

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageNetworkConfig.xaml
    /// </summary>
    public partial class PageNetworkConfig : Page
    {
        #region properties
        public bool SuspendNavigating { get => _suspendNavigating; set => _suspendNavigating = value; }
        #endregion

        #region field
        private bool _suspendNavigating;
        private PageNFGLimit _pageNFGLimit;
        private PageBusLoad _pageBusLoadPredict;
        private PageBusLoad _pageBusLoadActual;
        private PageBusLoad _pageSystemLoadPredict;
        private Page _currentPage;
        #endregion

        public PageNetworkConfig()
        {
            InitializeComponent();
            _pageNFGLimit = LoadPage<PageNFGLimit>();
            _currentPage = _pageNFGLimit;
            SetUpPage(_pageNFGLimit);
            buttonNFGLimit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonNFGLimit.Foreground = new SolidColorBrush(Colors.White);
        }
        
        private T LoadPage<T>()
        {
            _suspendNavigating = false;
            var page = (T)Activator.CreateInstance(typeof(T));
            SetUpPage<T>(page);
            frameNetworkContent.Navigate(page);
            return page;
        }

        private void SetUpPage<T>(T page, params string[] stringParams)
        {
            switch (typeof(T).Name)
            {
                case "PageNFGLimit":
                    {
                        var pageOp = page as PageNFGLimit;
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            switch (objs[0].ToString())
                            {
                                case "showLoading":
                                    {
                                        var info = objs[1].ToString();
                                        break;
                                    }
                                case "hideLoading":
                                    {
                                        break;
                                    }
                            }
                        });
                        break;
                    }
                case "PageBusLoad":
                    {
                        var pageOp = page as PageBusLoad;
                        if (stringParams.Length>0)
                        {
                            var uiTitle = stringParams[0].ToString();
                            pageOp.UITitle = uiTitle;
                        }
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            switch (objs[0].ToString())
                            {
                                case "showLoading":
                                    {
                                        var info = objs[1].ToString();
                                        break;
                                    }
                                case "hideLoading":
                                    {
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
            frameNetworkContent.Navigate(page);
            _currentPage = page;
        }
        
        private void buttonPredictLoad_Click(object sender, RoutedEventArgs e)
        {
            buttonPredictLoad.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonPredictLoad.Foreground = new SolidColorBrush(Colors.White);

            buttonActualLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonActualLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonPredictSystemLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictSystemLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonStableNFGInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonStableNFGInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonNFGLimit.Background = new SolidColorBrush(Colors.Transparent);
            buttonNFGLimit.Foreground = new SolidColorBrush(Colors.Black);

            if (_pageBusLoadPredict != null)
            {
                LoadPage(_pageBusLoadPredict);
            }
            else
            {
                _pageBusLoadPredict = LoadPage<PageBusLoad>();
                _pageBusLoadPredict.Title = "_pageBusLoadPredict";
                _currentPage = _pageBusLoadPredict;
                SetUpPage(_pageBusLoadPredict, "母线负荷预测");
            }
        }

        private void buttonActualLoad_Click(object sender, RoutedEventArgs e)
        {
            buttonPredictLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonActualLoad.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonActualLoad.Foreground = new SolidColorBrush(Colors.White);

            buttonPredictSystemLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictSystemLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonStableNFGInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonStableNFGInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonNFGLimit.Background = new SolidColorBrush(Colors.Transparent);
            buttonNFGLimit.Foreground = new SolidColorBrush(Colors.Black);


            if (_pageBusLoadActual != null)
            {
                LoadPage(_pageBusLoadActual);
            }
            else
            {
                _pageBusLoadActual = LoadPage<PageBusLoad>();
                _pageBusLoadActual.Title = "_pageBusLoadActual";
                _currentPage = _pageBusLoadActual;
                SetUpPage(_pageBusLoadActual, "母线实际负荷");
            }
        }

        private void buttonPredictSystemLoad_Click(object sender, RoutedEventArgs e)
        {
            buttonPredictLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonActualLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonActualLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonPredictSystemLoad.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonPredictSystemLoad.Foreground = new SolidColorBrush(Colors.White);

            buttonStableNFGInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonStableNFGInfo.Foreground = new SolidColorBrush(Colors.Black);
            
            buttonNFGLimit.Background = new SolidColorBrush(Colors.Transparent);
            buttonNFGLimit.Foreground = new SolidColorBrush(Colors.Black);
            

            if (_pageSystemLoadPredict != null)
            {
                LoadPage(_pageSystemLoadPredict);
            }
            else
            {
                _pageSystemLoadPredict = LoadPage<PageBusLoad>();
                _pageSystemLoadPredict.Title = "_pageSystemLoadPredict";
                _currentPage = _pageSystemLoadPredict;
                SetUpPage(_pageSystemLoadPredict, "系统负荷预测");
            }
        }

        private void buttonStableNFGInfo_Click(object sender, RoutedEventArgs e)
        {
            buttonPredictLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonActualLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonActualLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonPredictSystemLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictSystemLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonStableNFGInfo.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonStableNFGInfo.Foreground = new SolidColorBrush(Colors.White);
            
            buttonNFGLimit.Background = new SolidColorBrush(Colors.Transparent);
            buttonNFGLimit.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonNFGLimit_Click(object sender, RoutedEventArgs e)
        {
            buttonPredictLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonActualLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonActualLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonPredictSystemLoad.Background = new SolidColorBrush(Colors.Transparent);
            buttonPredictSystemLoad.Foreground = new SolidColorBrush(Colors.Black);

            buttonStableNFGInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonStableNFGInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonNFGLimit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonNFGLimit.Foreground = new SolidColorBrush(Colors.White);
            
            if (_pageNFGLimit != null)
            {
                LoadPage(_pageNFGLimit);
            }
            else
            {
                _pageNFGLimit = LoadPage<PageNFGLimit>();
                SetUpPage(_pageNFGLimit);
                _pageNFGLimit.Title = "_pageNFGLimit";
                _currentPage = _pageNFGLimit;
            }
        }

        private async void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            buttonSubmit.IsEnabled = false;
            if (_currentPage is PageBusLoad page)
            {
               await page.SubmitData();
            }
            buttonSubmit.IsEnabled = true;
        }
    }
}
