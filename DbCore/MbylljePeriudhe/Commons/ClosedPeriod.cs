using DbCore.MbylljePeriudhe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.MbylljePeriudhe
{
    public class ClosedPeriod : IClosedPeriod
    {
        public string ServerName { get; set; }
        public int CompanyId { get; set; }
        public Modul Modul { get; set; }
        public DateTime LastClosedDate { get; set; }
    }
}
