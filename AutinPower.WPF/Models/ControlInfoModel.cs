using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autin.Enum;
using Autin.Model;

namespace AutinPower.WPF.Models
{
    public class ControlInfoModel
    {
        public async Task<string> GetInfoRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_control_info"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            try
            {
                SystemManager.Instance.CurrentControlInfoList = ControlInfo.FromJson(response);
                Console.WriteLine("get control info list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get control info list fails!");
                return "fails";
            }
        }
    }
}
