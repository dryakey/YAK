using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutinPower.WPF.Models
{
    public class TreeMenuModel : IUIModel
    {
        public TreeMenuModel()
        {
            this.SubMennus = new ObservableCollection<SubMenuModel>();
        }

        public string MenuName { get; set; }
        public ObservableCollection<SubMenuModel> SubMennus { get; set; }
    }
}
