using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colTrupiEvidencaStatistikore: List<clsTrupiEvidencaStatistikore>
    {
         public colTrupiEvidencaStatistikore(int idKoka, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrTrupinEvidencaStatistikore(idKoka))
        {

        }
        public colTrupiEvidencaStatistikore(int idKoka)
            : base(new clsDatabaseAnalizeBuxheti().MerrTrupinEvidencaStatistikore(idKoka))
        {

        }

        public colTrupiEvidencaStatistikore(IEnumerable<clsTrupiEvidencaStatistikore> trupi)
            : base(trupi)
        {

        }
        public colTrupiEvidencaStatistikore()
        {

        }

      
        public colTrupiEvidencaStatistikore MerrTrupDefault(int idNdermarrje, int idAmbjenti)
        {
            AddRange(new clsDatabaseAnalizeBuxheti().MerrTrupinEvidencaStatistikore(idNdermarrje, idAmbjenti));
            return this;
        }
    }
}
