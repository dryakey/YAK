using Autin.Enum;
using Autin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutinPower.WPF.Models
{
    public class AnalysisCaseModel : IUIModel
    {
        public async Task<string> GetAnalysisCaseRequest()
        {
            var url = Route.SERVER_PATH + Route.API_PATH + Route.URLDictionary["api.get_analysis_case"];
            var request = new Autin.Services.HttpRequest(); // wldevau@gmail.com, Mba287xd!
            var response = await request.PostRequest(url,
                new Dictionary<string, string>() {
                    { "user_id", $"{SystemManager.Instance.CurrentUser.Id}" },
                    { "client_id", $"{SystemManager.Instance.CurrentUser.ClientId}" },
                    { "case_id", $"{SystemManager.Instance.CurrentCaseId}" },
                    { "client_token", $"{SystemManager.Instance.CurrentUser.ClientToken}" }
                });
            try
            {
                if (SystemManager.Instance.CurrentUnitList == null)
                {
                    SystemManager.Instance.CurrentUnitList = new Unit();
                }
                if (SystemManager.Instance.CaseList == null)
                {
                    SystemManager.Instance.CaseList = new List<IAutinDataModel>();
                }
                SystemManager.Instance.CaseList.Clear();
                var caseInfoList = AnalysisCaseInfo.FromJson(response);
                SystemManager.Instance.CaseList.AddRange(caseInfoList);
                SystemManager.Instance.CurrentSettlementDate = caseInfoList.Where(p => p.CaseId == "default").Select(p => p.SettlementDate).FirstOrDefault();
                Console.WriteLine("get AnalysisCase list successfully!");
                return "success";
            }
            catch
            {
                Console.WriteLine("get AnalysisCase list fails!");
                return "fails";
            }
        }
    }
}
