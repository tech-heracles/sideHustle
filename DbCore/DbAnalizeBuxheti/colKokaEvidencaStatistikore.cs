using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colKokaEvidencaStatistikore:List<clsKokaEvidencaStatistikore>
    {
        public colKokaEvidencaStatistikore()
        {

        }
        public colKokaEvidencaStatistikore(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrListEvidencaStatistikore(idNdermarrje, idNdermVit))
        {

        }
    }
}
