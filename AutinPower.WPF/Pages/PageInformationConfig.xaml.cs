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
    /// Interaction logic for PageInformationConfig.xaml
    /// </summary>
    public partial class PageInformationConfig : Page, INotifyPropertyChanged
    {
        public PageInformationConfig()
        {
            InitializeComponent();
            comboboxUserList.DataContext = this;
        }

        public Visibility UserSelectorVisibility { get=>_userSelectorVisibility;
            set {
                _userSelectorVisibility = value;
                NotifyPropertyChanged("UserSelectorVisibility");
            } }
        
        private Visibility _userSelectorVisibility = Visibility.Hidden;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void buttonDayLoadPredict_Click(object sender, RoutedEventArgs e)
        {
            UserSelectorVisibility = Visibility.Hidden;
            textboxSelectorLabel.Text = "";
            textblockPlotTitle.Text = "日前负荷预测曲线";

            buttonDayLoadPredict.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.White);

            buttonUnitOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonTransformOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonBlockInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonExternalPowerPlan.Background = new SolidColorBrush(Colors.Transparent);
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUserLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonUnitOverhaul_Click(object sender, RoutedEventArgs e)
        {
            buttonDayLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUnitOverhaul.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.White);

            buttonTransformOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonBlockInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonExternalPowerPlan.Background = new SolidColorBrush(Colors.Transparent);
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUserLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonTransformOverhaul_Click(object sender, RoutedEventArgs e)
        {
            buttonDayLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUnitOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonTransformOverhaul.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.White);

            buttonBlockInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonExternalPowerPlan.Background = new SolidColorBrush(Colors.Transparent);
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUserLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonBlockInfo_Click(object sender, RoutedEventArgs e)
        {
            buttonDayLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUnitOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonTransformOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.Black); 

            buttonBlockInfo.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559")); 
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.White);

            buttonExternalPowerPlan.Background = new SolidColorBrush(Colors.Transparent);
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUserLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonExternalPowerPlan_Click(object sender, RoutedEventArgs e)
        {
            buttonDayLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUnitOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonTransformOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonBlockInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonExternalPowerPlan.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559")); 
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUserLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.Black);
        }
        
        private void buttonPowerLoadPredict_Click(object sender, RoutedEventArgs e)
        {
            UserSelectorVisibility = Visibility.Visible;
            textboxSelectorLabel.Text = "用户选择：";
            textblockPlotTitle.Text = "日前电力用户用电负荷预测";
            
            buttonDayLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUnitOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonTransformOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonBlockInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonExternalPowerPlan.Background = new SolidColorBrush(Colors.Transparent);
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.White);

            buttonUserLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void buttonUserLoadPredict_Click(object sender, RoutedEventArgs e)
        {
            UserSelectorVisibility = Visibility.Visible;
            textboxSelectorLabel.Text = "用户选择：";
            textblockPlotTitle.Text = "代理用户用电负荷预测";

            buttonDayLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonDayLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUnitOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonUnitOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonTransformOverhaul.Background = new SolidColorBrush(Colors.Transparent);
            buttonTransformOverhaul.Foreground = new SolidColorBrush(Colors.Black);

            buttonBlockInfo.Background = new SolidColorBrush(Colors.Transparent);
            buttonBlockInfo.Foreground = new SolidColorBrush(Colors.Black);

            buttonExternalPowerPlan.Background = new SolidColorBrush(Colors.Transparent);
            buttonExternalPowerPlan.Foreground = new SolidColorBrush(Colors.Black);

            buttonPowerLoadPredict.Background = new SolidColorBrush(Colors.Transparent);
            buttonPowerLoadPredict.Foreground = new SolidColorBrush(Colors.Black);

            buttonUserLoadPredict.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF034559"));
            buttonUserLoadPredict.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
