using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.MbylljePeriudhe.Interfaces
{
    public interface IClosedPeriod
    {
        string ServerName { get; set; }
        int CompanyId { get; set; }
        Modul Modul { get; set; }
        DateTime LastClosedDate { get; set; }
    }
}
