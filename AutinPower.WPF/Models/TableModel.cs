using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutinPower.WPF.Models;

namespace AutinPower.WPF.Models
{
    public class TableModel : IUIModel
    {
        public string TableTitle { get; set; }
        public TableColumn[] TableColumns { get; set; }
    }

    public class TableColumn
    {
        public string HeaderText { get; set; }
        public double HeaderWidth { get; set; }
        public string BindingPath { get; set; }
    }
}
