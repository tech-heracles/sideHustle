using System.Collections.Generic;

namespace DbCore.DbAnalizeBuxheti
{
    public class colParashikimShpenzPersoneliConfig: List<clsParashikimShpenzPersoneliConfig>
    {
         public colParashikimShpenzPersoneliConfig(int idNdermarrje,bool prind = false)
            : base(new clsDatabaseAnalizeBuxheti().MerrParashikimShpenzPersoneliConfig(idNdermarrje, prind))
        {
        }
        
        
        public static clsMesazh KrijoDokumentDefault(int idNdermarrje, clsDatabaseAnalizeBuxheti dbAB)
        {
            dbAB.KrijoDokumentDefaultParashikimShpenzPersoneliConfig(idNdermarrje);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }
        public static clsMesazh KrijoDokumentDefault(int idNdermarrje)
        {
            return KrijoDokumentDefault(idNdermarrje, new clsDatabaseAnalizeBuxheti());
        }

       
  
    }
}
