using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class colKokaRealizimProkurimesh:List<clsKokaRealizimProkurimesh>
    {
        public colKokaRealizimProkurimesh()
        {

        }
        public colKokaRealizimProkurimesh(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrListProkurimePublike(idNdermarrje, idNdermVit))
        {

        }
    }
}
