using Autin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutinPower.WPF.Models
{
    public class SettlementModel : IUIModel
    {
        public async Task<string> PrepareSettlement(long id, string clientToken, long clientId, string case_id, DateTimeOffset settlementDate)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.prepare_settlement"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{id}" },
                    { "client_id", $"{clientId}" },
                    { "client_token", $"{clientToken}" },
                    { "case_id", case_id },
                    { "settlement_date", $"{settlementDate}" },
                });
            return response;
        }

        public async Task<string> StartSettlement(long id, string clientToken, long clientId, string case_id, DateTimeOffset settlementDate)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.start_settlement"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{id}" },
                    { "client_id", $"{clientId}" },
                    { "client_token", $"{clientToken}" },
                    { "case_id", case_id },
                    { "settlement_date", $"{settlementDate}" },
                });
            return response;
        }

        public async Task<string> PostSettlement(long id, string clientToken, long clientId, string case_id, DateTimeOffset settlementDate)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.post_settlement"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{id}" },
                    { "client_id", $"{clientId}" },
                    { "client_token", $"{clientToken}" },
                    { "case_id", case_id },
                    { "settlement_date", $"{settlementDate}" },
                });
            return response;
        }
    }
}
