using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaWebReports.Common
{
    public interface IUpdateDetailKPF: IUpdateDetail
    {
        void UpdateDetailKPF(string detailID);
    }
}
