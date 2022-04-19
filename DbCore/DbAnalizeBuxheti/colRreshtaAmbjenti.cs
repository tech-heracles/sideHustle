using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
  public  class colRreshtaAmbjenti:List<clsRreshtaAmbjenti>
    {
        public colRreshtaAmbjenti(int idNdermarrje, int idAmbjenti)
            : base(new clsDatabaseAnalizeBuxheti().MerrRreshtaAmbjenti(idNdermarrje, idAmbjenti))
        {
        }

        public colRreshtaAmbjenti(int idNdermarrje, int idAmbjenti, clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrRreshtaAmbjenti(idNdermarrje, idAmbjenti))
        {
        }

       
    }
}
