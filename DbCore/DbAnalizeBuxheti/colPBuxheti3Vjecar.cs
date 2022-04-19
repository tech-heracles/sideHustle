using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colPBuxheti3Vjecar:List<clsPBuxheti3Vjecar>
    {
         public colPBuxheti3Vjecar(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrPBuxheti3Vjecar(idNdermarrje, idNdermVit))
        {
        }

         public colPBuxheti3Vjecar(int idNdermarrje, int idNdermVit, clsDatabaseAnalizeBuxheti dbAB)
             : base(dbAB.MerrPBuxheti3Vjecar(idNdermarrje, idNdermVit))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultPBuxheti3Vjecar(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }
    }
}
