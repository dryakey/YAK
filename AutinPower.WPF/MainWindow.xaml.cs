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
using AutinPower.WPF.Pages;
using AutinPower.WPF.Models;
using System.Windows.Resources;

namespace AutinPower.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            MouseDown += Window_MouseDown;
        }

        public bool SuspendNavigating { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage<PageLogin>();
        }

        private void LoadPage<T>()
        {
            SuspendNavigating = false;
            mainFrame.NavigationService.RemoveBackEntry();

            var page = (T)Activator.CreateInstance(typeof(T));
            SetUpPage<T>(page);
            mainFrame.Navigate(page);
        }

        private void SetUpPage<T>(T page)
        {
            switch (typeof(T).Name)
            {
                case "PageLogin":
                    {
                        var pageOp = page as PageLogin;
                        pageOp.LoginModel = new LoginModel();
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            SwitchViewWithLoginResult(objs);
                        });
                        break;
                    }
                case "PageMainSystem":
                    {
                        var pageOp = page as PageMainSystem;
                        pageOp.LoginModel = new LoginModel();
                        pageOp.ControlInfoModel = new ControlInfoModel();
                        pageOp.UnitModel = new UnitModel();
                        pageOp.AnalysisCaseModel = new AnalysisCaseModel();
                        pageOp.NotifyHost = new Action<object[]>((objs) => {
                            SwitchViewWithLogoutResult(objs);
                        });
                        break;
                    }
            }
        }

        private void SwitchViewWithLoginResult(object[] objs)
        {
            var msg = objs[0].ToString();
            switch (msg)
            {
                case "success":
                    {
                        WindowState = WindowState.Maximized;
                        LoadPage<PageMainSystem>();
                        this.ResizeMode = ResizeMode.CanResizeWithGrip;
                        buttonMaxNormal.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void SwitchViewWithLogoutResult(object[] objs)
        {
            var msg = objs[0].ToString();
            switch (msg)
            {
                case "success":
                    {
                        this.Width = 365;
                        this.Height = 495;
                        this.WindowState = WindowState.Normal;
                        this.ResizeMode = ResizeMode.NoResize;
                        LoadPage<PageLogin>();
                        buttonMaxNormal.Visibility = Visibility.Collapsed;
                        break;
                    }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonNormal_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                var resourceUri = new Uri("/AutinPower.WPF;component/Images/opNormalize.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = temp;
                buttonMaxNormal.Background = brush;
            }
            else
            {
                WindowState = WindowState.Normal;
                var resourceUri = new Uri("/AutinPower.WPF;component/Images/opMaximize.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = temp;
                buttonMaxNormal.Background = brush;
            }
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
