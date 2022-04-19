using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{

    public class colParashikimiTeArdhura: List<clsParashikimiTeArdhura>{

    
        public colParashikimiTeArdhura(int idNdermarrje, int idNdermVit)
            : base(new clsDatabaseAnalizeBuxheti().MerrParashikimiTeArdhura(idNdermarrje, idNdermVit))
        {
        }

        public colParashikimiTeArdhura(int idNdermarrje, int idNdermVit, clsDatabaseAnalizeBuxheti dbAB)
            : base(dbAB.MerrParashikimiTeArdhura(idNdermarrje,idNdermVit))
        {
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }

        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultParashikimiTeArdhura(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }
    }
}