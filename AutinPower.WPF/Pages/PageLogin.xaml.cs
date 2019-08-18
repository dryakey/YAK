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

namespace AutinPower.WPF.Pages
{
    /// <summary>
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page, IAutinPage
    {
        public LoginModel LoginModel { get; set; }
        public Action<object[]> NotifyHost { get; set; }

        public PageLogin()
        {
            InitializeComponent();
        }

        private void AfterLoginFail()
        {
            textblockInfo.Foreground = Brushes.Red;
            textblockInfo.Text = "Login Fails!";
        }

        private void AfterLoginSuccess()
        {
            textblockInfo.Foreground = Brushes.DodgerBlue;
            textblockInfo.Text = "Login successed!";
            NotifyHost?.Invoke(new object[] { "success" });
        }

        private async void LoginAction()
        {
            ctrlLoadingSpinner.Visibility = Visibility.Visible;

            LoginModel.UserName = textboxUserName.Text;
            LoginModel.Password = textboxPassword.Password;
            var result = await LoginModel.LoginRequest();
            if (result == "success")
            {
                AfterLoginSuccess();
            }
            else
            {
                AfterLoginFail();
            }
            ctrlLoadingSpinner.Visibility = Visibility.Hidden;
        }

        private void textboxPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginAction();
            }
        }
        
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginAction();
        }
    }
}
