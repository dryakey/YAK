using System;
using System.Collections.Generic;
using System.Text;

namespace Autin.Model
{
    public interface IAutinDataModel
    {
        string Key { get; set; }
        DateTimeOffset DataDate { get; set; }
    }
}
