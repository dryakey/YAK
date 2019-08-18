using System;
using System.Collections.Generic;
using System.Text;

namespace Autin.Model
{
    public class Unit
    {
        public List<UnitInitialParam> UnitInitialParam { get; set; }
        public List<UnitParam> UnitParam { get; set; }
        public List<UnitPrice> UnitPrice { get; set; }
        public List<UnitStartPrice> UnitStartPrice { get; set; }
        public List<UnitNode> UnitNode { get; set; }
        public List<NfgLimit> UnitNfgLimt { get; set; }
        public List<PowerLoad> UnitSystemLoadPredict { get; set; }

        public List<IAutinDataModel> UnitConfiguration { get; set; }
        public List<IAutinDataModel> UnitGeneratorSidePriceDeclare { get; set; }
        public List<IAutinDataModel> UnitUserSidePriceDeclare { get; set; }
    }
}
