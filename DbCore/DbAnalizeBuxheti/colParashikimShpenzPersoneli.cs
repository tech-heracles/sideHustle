using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colParashikimShpenzPersoneli:List<clsParashikimShpenzPersoneli>
    {
         public colParashikimShpenzPersoneli(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrParashikimShpenzPersoneli(idNdermarrje, idNdermVit))
        {
        }

         public colParashikimShpenzPersoneli(int idNdermarrje, int idNdermVit, clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrParashikimShpenzPersoneli(idNdermarrje, idNdermVit))
        {
        }

     
    }
}
