using Autin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autin.Enum;

namespace AutinPower.WPF.Models
{
    public class LoginModel : IUIModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task<string> LoginRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.login"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "email", $"{UserName}" },
                    { "password", $"{Password}" }
                });
            try
            {
                if (SystemManager.Instance.CurrentUser != null)
                {
                    SystemManager.Instance.CurrentUser.ClientToken = ""; // clear ClientToken
                }
                SystemManager.Instance.CurrentUser = User.FromJson(response);
                Console.WriteLine("Login successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("Login fails!");
                return "fails";
            }
        }

        public async Task<string> LogoutRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.logout"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "email", $"{UserName}" },
                    { "password", $"{Password}" }
                });
            try
            {
                if (response.Contains("User logged out"))
                {
                    Console.WriteLine("Logout successfully!");
                }
                return "success";
            }
            catch
            {
                Console.WriteLine("Login fails!");
                return "fails";
            }
        }

    }
}
