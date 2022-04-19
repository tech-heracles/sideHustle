using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class colLlojElementiPerIntegrim: List<clsLlojElementiPerIntegrim>
    {
        public colLlojElementiPerIntegrim():base(new clsDatabaseInventari().merrLlojElementiPerIntegrim())
        {

        }
    }
}
