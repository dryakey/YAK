using Autin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutinPower.WPF.Models
{
    public class PowerLoadModel : IUIModel
    {
        public Dictionary<string, List<PowerLoad>> PowerLoadTable { get; set; }

        public PowerLoadModel()
        {
            PowerLoadTable = new Dictionary<string, List<PowerLoad>>();
        }
    }
}
