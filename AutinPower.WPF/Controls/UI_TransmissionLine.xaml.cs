using Autin.Enum;
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
    /// Interaction logic for UI_TransmissionLine.xaml
    /// </summary>
    public partial class UI_TransmissionLine : UserControl, INotifyPropertyChanged
    {
        #region properties
        public PowerSystem.TransmitLineStatus TransmitLineStatus { get => _transmitLineStatus; set => _transmitLineStatus = value; }
        public string AnimationStart
        {
            get => _animationStart;
            set
            {
                _animationStart = value;
                NotifyPropertyChanged("AnimationStart");
            }
        }
        public Action<object[]> NotifyUI { get; set; }
        #endregion

        #region fields
        private PowerSystem.TransmitLineStatus _transmitLineStatus;
        private string _animationStart;
        #endregion

        public UI_TransmissionLine()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void LineMovingToNomal()
        {
            try
            {
                _transmitLineStatus = PowerSystem.TransmitLineStatus.TLS_NORMAL;
                var style = (Style)this.Resources["normalStyle"];
                polygonMoving.Style = style;
                polygonMoving.Tag = "normal";
                NotifyUI?.Invoke(new object[] { "normal" });
            }
            catch
            {
            }
        }

        public void LineMovingToBlock()
        {
            try
            {
                _transmitLineStatus = PowerSystem.TransmitLineStatus.TLS_BLOCKED;
                var style = (Style)this.Resources["cutoffStyle"];
                polygonMoving.Style = style;
                polygonMoving.Tag = "block";
                NotifyUI?.Invoke(new object[] { "block" });
            }
            catch
            {
            }
        }

        public void LineMovingToHighLoad()
        {
            try
            {
                _transmitLineStatus = PowerSystem.TransmitLineStatus.TLS_HIGHLOAD;
                var style = (Style)this.Resources["highloadStyle"];
                polygonMoving.Style = style;
                polygonMoving.Tag = "highload";
                NotifyUI?.Invoke(new object[] { "highload" });
            }
            catch
            {
            }
        }
    }
}
