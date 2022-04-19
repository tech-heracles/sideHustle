using System;
using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colPlanifikimiProdukteve:List<clsPlanifikimiProdukteve>
    {
        public colPlanifikimiProdukteve(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrPlanifikimiProdukteve(idNdermarrje, idNdermVit))
        {
        }

        public colPlanifikimiProdukteve(int idNdermarrje, int idNdermVit, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrPlanifikimiProdukteve(idNdermarrje, idNdermVit))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultPlanifikimiProdukteve(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }
    }
}
