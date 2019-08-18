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

namespace AutinPower.WPF.Controls
{
    /// <summary>
    /// Interaction logic for UI_LoadingInfo.xaml
    /// </summary>
    public partial class UI_LoadingInfo : UserControl, INotifyPropertyChanged
    {
        public string LoadingInfo { get => _loadingInfo; set { _loadingInfo = value; NotifyPropertyChanged("LoadingInfo"); } }
        private string _loadingInfo;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public UI_LoadingInfo()
        {
            InitializeComponent();
            textblockInfo.DataContext = this;
        }
    }
}
