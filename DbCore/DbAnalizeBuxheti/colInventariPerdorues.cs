using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colInventariPerdorues:List<clsInventariPerdorues>
    {
        public colInventariPerdorues(int idNdermarrje)
            : base(new clsDatabaseAnalizeBuxheti().MerrInventariPerdorues(idNdermarrje))
        {
        }

        public colInventariPerdorues(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrInventariPerdorues(idNdermarrje))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultInventariPerdorues(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }
    }
}
