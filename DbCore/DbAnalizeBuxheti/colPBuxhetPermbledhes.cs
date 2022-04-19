using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colPBuxhetPermbledhes : List<clsPBuxhetPermbledhes>
    {
        public colPBuxhetPermbledhes(int idNdermarrje, int idNdermvit)
            : base(new clsDatabaseAnalizeBuxheti().MerrPBuxhetPermbledhes(idNdermarrje, idNdermvit))
        {
        }

        public colPBuxhetPermbledhes(int idNdermarrje, int idNdermVit, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrPBuxhetPermbledhes(idNdermarrje, idNdermVit))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());

        }
        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultPBuxhetPermbledhes(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }



    
    }
}