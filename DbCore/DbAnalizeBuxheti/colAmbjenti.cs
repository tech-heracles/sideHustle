using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
  public  class colAmbjenti: List<clsAmbjenti>
    {
         public colAmbjenti()
            : base(new clsDatabaseAnalizeBuxheti().MerrAmbjentet())
        {
        }

        public colAmbjenti(clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrAmbjentet())
        {
        }
    }
}
