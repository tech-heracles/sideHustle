using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsParashikimShpenzPersoneli
    {
        #region atribute

        public int PShPId { get; set; }

        public int FunksioniId { get; set; }

        public int NrPunonjesish { get; set; }

        public int VjetersiaMesatare { get; set; }

        public decimal ShtesaPuneJashteOrarit { get; set; }

        public decimal ShtesaPageTeRregulluara { get; set; }

        public decimal FondPagePerSigurimeShoqerore { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public int IdNdermarrje { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int RreshtiId { get; set; }

        public string Funksioni { get; set; }

        public decimal NiveliShteses { get; set; }

        public int IdNdermVit { get; set; }

        public string Prindi { get; set; }

        public clsParashikimShpenzPersoneli() { }
        #endregion atribute
        public clsParashikimShpenzPersoneli(int pShPId, int funksioniId , int nrPunonjesish,
            int vjetersiaMesatare, decimal shtesaPuneJashteOrarit, 
            decimal shtesaPageTeRregulluara, decimal fondPagePerSigurimeShoqerore, decimal niveliShteses,
            int idModifikuesi, int idNderrmarje, int idStatusDok, int idNdermVit)
        {
            PShPId = pShPId;
            FunksioniId = funksioniId;
            NrPunonjesish = nrPunonjesish;
            VjetersiaMesatare = vjetersiaMesatare;
            ShtesaPuneJashteOrarit = shtesaPuneJashteOrarit;
            ShtesaPageTeRregulluara = shtesaPageTeRregulluara;
            FondPagePerSigurimeShoqerore = fondPagePerSigurimeShoqerore;
            NiveliShteses = niveliShteses;
            IdModifikuesi = idModifikuesi;
            IdNdermarrje = idNderrmarje;
            IdStatusDok = idStatusDok;
            IdNdermVit = idNdermVit;

        }


        public static clsParashikimShpenzPersoneli Krijo(IDataRecord record)
        {
            return new clsParashikimShpenzPersoneli
            {
                PShPId = !Convert.IsDBNull(record["PSHPID"]) ? Convert.ToInt32(record["PSHPID"]) : 0,
                FunksioniId = !Convert.IsDBNull(record["FUNKSIONIID"]) ? Convert.ToInt32(record["FUNKSIONIID"]) : 0,
                NrPunonjesish = !Convert.IsDBNull(record["NRPUNONJESISH"]) ? Convert.ToInt32(record["NRPUNONJESISH"]) : 0,
                VjetersiaMesatare = !Convert.IsDBNull(record["VJETERSIA_MESATARE"]) ? Convert.ToInt32(record["VJETERSIA_MESATARE"]) : 0,
                ShtesaPuneJashteOrarit = !Convert.IsDBNull(record["SHTESA_PUNE_JASHTORARIT"]) ? Convert.ToDecimal(record["SHTESA_PUNE_JASHTORARIT"]) : 0,
                ShtesaPageTeRregulluara = !Convert.IsDBNull(record["SHTESA_PAGE_TE_RREGULLUARA"]) ? Convert.ToDecimal(record["SHTESA_PAGE_TE_RREGULLUARA"]) : 0,
                FondPagePerSigurimeShoqerore = !Convert.IsDBNull(record["FOND_PAGE_PER_SIGURIME_SHOQERORE"]) ? Convert.ToDecimal(record["FOND_PAGE_PER_SIGURIME_SHOQERORE"]) : 0,
                NiveliShteses = !Convert.IsDBNull(record["NIVELI_SHTESES"]) ? Convert.ToDecimal(record["NIVELI_SHTESES"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Funksioni = !Convert.IsDBNull(record["FUNKSIONI"]) ? Convert.ToString(record["FUNKSIONI"]) : string.Empty,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,
                Prindi = !Convert.IsDBNull(record["KODPRIND"]) ? Convert.ToString(record["KODPRIND"]) : "",
            };
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit");

            try
            {
                dbAB.beginTransaksion();
                mesazhi = dbAB.fshiUpdateStatusDokParashikimShpenzPersoneli(PShPId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int pShPId = -1;
                    mesazhi = dbAB.RuajParashikimShpenzPersoneli(out pShPId, FunksioniId, NrPunonjesish, VjetersiaMesatare,
                        ShtesaPuneJashteOrarit, ShtesaPageTeRregulluara, FondPagePerSigurimeShoqerore,NiveliShteses,
                        IdNdermarrje, IdModifikuesi, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        PShPId = pShPId;
                        dbAB.commitTransaksion();
                    }
                    else
                        dbAB.rollbackTransaksion();
                }
                else
                    dbAB.rollbackTransaksion();


            }
            catch (Exception err)
            {

                dbAB.rollbackTransaksion();
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te parashikimit te shpenzimeve te personelit \n" + err.Message);
            }
            return mesazhi;
        }
    }
}
