using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colShpenzimeOperative:List<clsShpenzimeOperative>
    {
        public colShpenzimeOperative(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrShpenzimeOperative(idNdermarrje,idNdermVit))
        {
        }

        public colShpenzimeOperative(int idNdermarrje,int idNdermVit, clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrShpenzimeOperative(idNdermarrje, idNdermVit))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultShpenzimeOperative(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }
        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }
    }
}
