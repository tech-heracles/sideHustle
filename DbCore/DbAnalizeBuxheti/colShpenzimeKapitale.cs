using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colShpenzimeKapitale:List<clsShpenzimeKapitale>
    {
         public colShpenzimeKapitale(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrShpenzimeKapitale(idNdermarrje, idNdermVit))
        {
        }

         public colShpenzimeKapitale(int idNdermarrje, int idndermVit, clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrShpenzimeKapitale(idNdermarrje, idndermVit))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultShpenzimeKapitale(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }
        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }
    }
}
