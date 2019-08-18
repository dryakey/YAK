using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutinPower.WPF.Pages
{
    interface IAutinPage
    {
        Action<object[]> NotifyHost { get; set; }
    }
}
