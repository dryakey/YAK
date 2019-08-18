using Autin.Enum;
using Autin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutinPower.WPF.Models
{
    public class UnitModel : IUIModel
    {
        public async Task<string> GetUnitParamRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_unit_param"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitParam = UnitParam.FromJson(response);
                Console.WriteLine("get UnitParam list successfully!");
                return "success";
            }
            catch(Exception ex)
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }

        public async Task<string> GetUnitInitialParamRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_unit_initial_param"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitInitialParam = UnitInitialParam.FromJson(response);
                Console.WriteLine("get UnitParam list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }

        public async Task<string> GetUnitPriceRequest(string unitId)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_unit_price"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "unit_id", unitId},
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitPrice = UnitPrice.FromJson(response);
                Console.WriteLine("get UnitPrice list successfully!");
                return response;
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }

        public async Task<string> GetUnitStartPriceRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_unit_start_price"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitStartPrice = UnitStartPrice.FromJson(response);
                Console.WriteLine("get UnitParam list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }
        
        public async Task<string> GetUnitNodeRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_unit_node"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitNode = UnitNode.FromJson(response);
                Console.WriteLine("get UnitParam list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }

        public async Task<string> GetElementListRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_element_list"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentElement == null)
                {
                    SystemManager.Instance.CurrentElement = new List<Element>();
                }
                SystemManager.Instance.CurrentElement = Element.FromJson(response);
                Console.WriteLine("get UnitParam list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }
        
        public async Task<string> GetUnitNfgLimitRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_nfg_limit"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitNfgLimt = NfgLimit.FromJson(response);
                Console.WriteLine("get NfgLimit list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }

        public async Task<string> GetSystemLoadPredictRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_system_load_predict"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                SystemManager.Instance.CurrentUnitList.UnitSystemLoadPredict = PowerLoad.FromJson(response);
                Console.WriteLine("get System Load Predict list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get UnitParam list fails!");
                return "fails";
            }
        }
        public async Task<string> GetEnergyCostRequest(string unitId)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_energy_cost"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "unit_id", unitId},
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" },
                    { "settlement_date", $"{SystemManager.Instance.CurrentSettlementDate}" }
                });
            if (response.Contains("Error")) return "fails";
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                Console.WriteLine("get EnergyCost list successfully!");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("get EnergyCost list fails!");
                return "fails";
            }
        }

        public async Task<string> SetUnitConfigRequest(string jsonData)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.set_unit_config_data"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostJsonRequest(url, jsonData);
            return response;
        }

        public async Task<string> SetUnitPriceRequest(long id, string unitId, long clientId, string caseId, string clientToken, DateTimeOffset settlementDate, string unitPrice, string unitStartPrice)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.set_unit_price_data"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{id}" },
                    { "unit_id", $"{unitId}" },
                    { "client_id", $"{clientId}" },
                    { "case_id", $"{caseId}" },
                    { "client_token", $"{clientToken}" },
                    { "settlement_date", $"{settlementDate}" },
                    { "unit_price", unitPrice },
                    { "unit_start_price", unitStartPrice }
                });
            return response;
        }

        public async Task<string> SetPowerLoadRequest(long id, string nodeId, string caseId, string clientToken, long clientId, DateTimeOffset settlementDate, string powerLoad)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.set-bus-load-predict-data"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "node_id", $"{nodeId}" },
                    { "user_id", $"{id}" },
                    { "client_id", $"{clientId}" },
                    { "case_id", $"{caseId}" },
                    { "client_token", $"{clientToken}" },
                    { "settlement_date", $"{settlementDate}" },
                    { "power_load", powerLoad },
                });
            return response;
        }

        public async Task<string> SetSystemLoadRequest(long id, string nodeId, string caseId, string clientToken, long clientId, DateTimeOffset settlementDate, string powerLoad)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.set_system_load_predict"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "node_id", $"{nodeId}" },
                    { "user_id", $"{id}" },
                    { "client_id", $"{clientId}" },
                    { "case_id", $"{caseId}" },
                    { "client_token", $"{clientToken}" },
                    { "settlement_date", $"{settlementDate}" },
                    { "power_load", powerLoad },
                });
            return response;
        }

        public async Task<string> SetConsumerDeclareRequest(long id, string nodeId, string caseId, string clientToken, long clientId, DateTimeOffset settlementDate, string powerLoad)
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.set_consumer_load"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "node_id", $"{nodeId}" },
                    { "user_id", $"{id}" },
                    { "client_id", $"{clientId}" },
                    { "case_id", $"{caseId}" },
                    { "client_token", $"{clientToken}" },
                    { "settlement_date", $"{settlementDate}" },
                    { "power_load", powerLoad },
                });
            return response;
        }
    }
}
