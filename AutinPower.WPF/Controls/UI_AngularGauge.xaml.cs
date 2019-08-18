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
    /// Interaction logic for UI_AngularGauge.xaml
    /// </summary>
    public partial class UI_AngularGauge : UserControl, INotifyPropertyChanged
    {
        #region properties
        public double Value { get => _value; set { _value = value; } }

        public double FromValue0 { get => _fromValue0; set { _fromValue0 = value; NotifyPropertyChanged("FromValue0"); } }
        public double ToValue0 { get => _toValue0; set { _toValue0 = value; NotifyPropertyChanged("ToValue0"); } }
        public SolidColorBrush Fill0 { get => _fill0; set { _fill0 = value; NotifyPropertyChanged("Fill0"); } }
        
        public double ToValue1 { get => _toValue1; set { _toValue1 = value; NotifyPropertyChanged("ToValue1"); } }
        public SolidColorBrush Fill1 { get => _fill1; set { _fill1 = value; NotifyPropertyChanged("Fill1"); } }
        
        public SolidColorBrush BackgroundFill { get => _backgroundFill; set { _backgroundFill = value; NotifyPropertyChanged("BackgroundFill"); } }
        #endregion

        #region fields
        public double _value;
        public double _fromValue0;
        public double _toValue0;
        public SolidColorBrush _fill0;
        public double _toValue1;
        public SolidColorBrush _fill1;
        public SolidColorBrush _backgroundFill;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        public UI_AngularGauge()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
        }
    }
}
