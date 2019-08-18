using Autin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutinPowerMain.Views
{
    /// <summary>
    /// Interaction logic for UI_Login.xaml
    /// </summary>
    public partial class UI_Login : UserControl
    {
        public Action<object[]> NotifyContainer { get; set; }

        public UI_Login()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginRequest();
        }

        public async void LoginRequest()
        {
            var url = "http://59.110.173.242/api/login";
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url, new Dictionary<string, string>() { { "email", $"{textboxUsername.Text}" }, { "password", $"{textboxPassword.Password}" } });
            try
            {
                if (SystemManager.Instance.CurrentUser != null)
                {
                    SystemManager.Instance.CurrentUser.ClientToken = ""; // clear ClientToken
                }
                SystemManager.Instance.CurrentUser = User.FromJson(response);
                NotifyContainer?.Invoke(new object[] { "success" });
                Console.WriteLine("Login successfully!");
            }
            catch
            {
                NotifyContainer?.Invoke(new object[] { "fails" });
                Console.WriteLine("Login fails!");
            }
        }
    }
}
