using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class colTrupiRealizimProkurimesh:List<clsTrupiRealizimProkurimesh>
    {
        public colTrupiRealizimProkurimesh(int idKoka, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrTrupinRealizimProkurimi(idKoka))
        {

        }
        public colTrupiRealizimProkurimesh(int idKoka)
            : base(new clsDatabaseAnalizeBuxheti().MerrTrupinRealizimProkurimi(idKoka))
        {

        }

        public colTrupiRealizimProkurimesh(IEnumerable<clsTrupiRealizimProkurimesh> trupi)
            : base(trupi)
        {

        }
        public colTrupiRealizimProkurimesh()
        {

        }


        public colTrupiRealizimProkurimesh MerrTrupDefault(int idNdermarrje, int idNdermVit)
        {
            AddRange(new clsDatabaseAnalizeBuxheti().MerrTrupDefaultRealizimProkurimesh(idNdermarrje, idNdermVit));
            return this;
        }
    }
}
