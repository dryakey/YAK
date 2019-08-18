using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autin.Model;

namespace Autin.Model
{
    public class SystemManager
    {
        #region Constructor
        private SystemManager()
        {
        }

        public static SystemManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly SystemManager instance = new SystemManager();
        }
        #endregion

        #region properties
        public User CurrentUser { get; set; }
        public string CurrentCaseId { get => currentCaseId; set => currentCaseId = value; }
        public List<ControlInfo> CurrentControlInfoList { get; set; }
        public Unit CurrentUnitList { get; set; }
        public List<Element> CurrentElement { get; set; }
        public DateTimeOffset CurrentSettlementDate { get; set; }
        public List<IAutinDataModel> CaseList { get; set; }
        #endregion

        #region fields
        private string currentCaseId = "default";
        #endregion

    }
}
