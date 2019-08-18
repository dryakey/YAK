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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autin.Enum;

namespace AutinPower.WPF.Controls
{
    /// <summary>
    /// Interaction logic for UI_Generator.xaml
    /// </summary>
    public partial class UI_Generator : UserControl
    {
        #region property
        public SolidColorBrush StrokeColor
        {
            get
            {
                return (SolidColorBrush)GetValue(StrokeColorProperty);
            }
            set
            {
                SetValue(StrokeColorProperty, value);
            }
        }
        public SolidColorBrush LinkColor
        {
            get
            {
                return (SolidColorBrush)GetValue(LinkColorProperty);
            }
            set
            {
                SetValue(LinkColorProperty, value);
            }
        }
        public PowerSystem.GeneratorStatus GeneratorStatus { get => _generatorStatus; set => _generatorStatus = value; }
        public Action<object[]> NotifyUI { get; set; }
        #endregion

        #region fields
        private PowerSystem.GeneratorStatus _generatorStatus;
        private Storyboard _storyboard;
        #endregion

        public UI_Generator()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
            recLink.DataContext = this;
            ellGenerator.DataContext = this;
            _storyboard = (Storyboard)imageGenerator.FindResource("spin");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TurnOnGenerator();
        }

        public static readonly DependencyProperty StrokeColorProperty
            = DependencyProperty.Register("StrokeColor", typeof(SolidColorBrush), typeof(UI_Generator), new PropertyMetadata(new SolidColorBrush(Colors.Gray), new PropertyChangedCallback(OnStrokeColorChanged)));

        public static readonly DependencyProperty LinkColorProperty
            = DependencyProperty.Register("LinkColor", typeof(SolidColorBrush), typeof(UI_Generator), new PropertyMetadata(new SolidColorBrush(Colors.Gray), new PropertyChangedCallback(OnLinkColorChanged)));

        private static void OnStrokeColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UI_Generator;
            control.OnStrokeColorChanged(e);
        }

        private void OnStrokeColorChanged(DependencyPropertyChangedEventArgs e)
        {
            ellGenerator.Stroke = (SolidColorBrush)e.NewValue;
        }
        
        private static void OnLinkColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as UI_Generator;
            control.OnLinkColorChanged(e);
        }

        private void OnLinkColorChanged(DependencyPropertyChangedEventArgs e)
        {
            recLink.Fill = (SolidColorBrush)e.NewValue;
        }

        public void TurnOnGenerator()
        {
            try
            {
                _generatorStatus = PowerSystem.GeneratorStatus.GS_ON;
                if (_storyboard == null)
                {
                    _storyboard = (Storyboard)imageGenerator.FindResource("spin");
                }
                StrokeColor = new SolidColorBrush(Colors.Black);
                LinkColor = new SolidColorBrush(Colors.Black);
                BitmapImage image = new BitmapImage(new Uri("/AutinPower.WPF;component/Images/rotator.png", UriKind.Relative));
                imageGenerator.Source = image;
                _storyboard.Begin();
            }
            catch
            {
            }
        }
        
        public void TurnOffGenerator()
        {
            try
            {
                _generatorStatus = PowerSystem.GeneratorStatus.GS_OFF;
                if (_storyboard == null)
                {
                    _storyboard = (Storyboard)imageGenerator.FindResource("spin");
                }
                _storyboard.Stop();

                BitmapImage image = new BitmapImage(new Uri("/AutinPower.WPF;component/Images/cross.png", UriKind.Relative));
                imageGenerator.Source = image;
                StrokeColor = new SolidColorBrush(Colors.Gray);
                LinkColor = new SolidColorBrush(Colors.Gray);
            }
            catch
            {
            }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (_generatorStatus)
            {
                case PowerSystem.GeneratorStatus.GS_ON:
                    {
                        TurnOffGenerator();
                        break;
                    }
                case PowerSystem.GeneratorStatus.GS_OFF:
                    {
                        TurnOnGenerator();
                        break;
                    }
            }
        }
    }
}
