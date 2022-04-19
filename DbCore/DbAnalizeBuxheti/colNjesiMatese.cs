using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colNjesiMatese:List<clsNjesiMatese>
    {
         public colNjesiMatese()
            : base(new clsDatabaseAnalizeBuxheti().MerrNjesiteMatese())
        {
        }

         public colNjesiMatese(clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrNjesiteMatese())
        {
        }
    }
}
