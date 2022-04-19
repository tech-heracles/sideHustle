using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colInventariVite:List<clsInventariVite>
    {
        public colInventariVite(int idNdermarrje)
            : base(new clsDatabaseAnalizeBuxheti().MerrInventariVite(idNdermarrje))
        {
        }

        public colInventariVite(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrInventariVite(idNdermarrje))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultInventariVite(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }
    }
}
