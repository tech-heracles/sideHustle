using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsRreshtaAmbjenti
    {
        #region atributet

        public int RreshtiId { get; set; }

        public string KodiRreshtit { get; set; }

        public string PershkrimiRreshtit { get; set; }

        public string NjesiaMatese { get; set; }

        public int IdAmbjenti { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdNdermVit { get; set; }

        #endregion atributet

        public clsRreshtaAmbjenti()
        {
        }

        public clsRreshtaAmbjenti(int rreshtiId, string kodiRreshtit, string pershkrimiRreshtit, int idAmbjenti, int idModifikuesi, int idNdermarrje, string njesiaMatese, int idNdermVit)
        {
            RreshtiId = rreshtiId;
            KodiRreshtit = kodiRreshtit;
            PershkrimiRreshtit = pershkrimiRreshtit;
            IdAmbjenti = idAmbjenti;
            IdModifikuesi = idModifikuesi;
            IdNdermarrje = idNdermarrje;
            NjesiaMatese = njesiaMatese;
            IdNdermVit = idNdermVit;
        }

        public clsRreshtaAmbjenti(int rreshtiId)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                dbAB.MerrRreshtAmbjenti(rreshtiId, this);
            }
        }


        /// <summary>
        /// krijon nje objekt  clsRreshtaAmbjenti,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsRreshtaAmbjenti Krijo(IDataRecord record)
        {
            clsRreshtaAmbjenti rreshti = new clsRreshtaAmbjenti();
            rreshti.Mbush(record);
            return rreshti;
            //return new clsRreshtaAmbjenti
            //{
            //    RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
            //    KodiRreshtit = !Convert.IsDBNull(record["KODI_RRESHTIT"]) ? Convert.ToString(record["KODI_RRESHTIT"]) : "",
            //    PershkrimiRreshtit = !Convert.IsDBNull(record["PERSHKRIMI_RRESHTIT"]) ? Convert.ToString(record["PERSHKRIMI_RRESHTIT"]) : "",
            //    IdAmbjenti = !Convert.IsDBNull(record["IDAMBJENTI"]) ? Convert.ToInt32(record["IDAMBJENTI"]) : 0,
            //    IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
            //    IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
            //    IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
            //    IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
            //    DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
            //    DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
            //};
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te rreshtave te ambjentit");

            try
            {
                dbAB.beginTransaksion();

                mesazhi = dbAB.ModifikoRreshtaAmbjenti(RreshtiId, KodiRreshtit, PershkrimiRreshtit, IdAmbjenti, IdNdermarrje, IdModifikuesi, NjesiaMatese, IdNdermVit);

                if (mesazhi.Status)
                {
                    dbAB.commitTransaksion();
                }
                else
                    dbAB.rollbackTransaksion();
            }
            catch (Exception er)
            {
                dbAB.rollbackTransaksion();
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerave \n" + er.Message);
            }
            return mesazhi;
        }

        public clsMesazh ModifikoRreshtaParashikimShpenzimesh(clsDatabaseAnalizeBuxheti dbAB)
        {
            
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te rreshtave te ambjentit");

            try
            {
                mesazhi = dbAB.ModifikoRreshtaAmbjenti(RreshtiId, KodiRreshtit, PershkrimiRreshtit, IdAmbjenti, IdNdermarrje, IdModifikuesi, NjesiaMatese, IdNdermVit);
            }
            catch (Exception er)
            {
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerave \n" + er.Message);
            }
            return mesazhi;
        }

        public clsMesazh Ruaj()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerave");

            try
            {
                dbAB.beginTransaksion();

                int rreshtiId = -1;
                mesazhi = dbAB.RuajRreshtaAmbjenti(out rreshtiId, KodiRreshtit, PershkrimiRreshtit, IdAmbjenti, IdNdermarrje, IdKrijuesi, NjesiaMatese, IdNdermVit);

                if (mesazhi.Status)
                {
                    RreshtiId = rreshtiId;
                    dbAB.commitTransaksion();
                }
                else
                    dbAB.rollbackTransaksion();
            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerave \n" + err.Message);
            }
            return mesazhi;
        }

        /// <summary>
        /// fshi nje rresht nga ambjenti
        /// </summary>
        /// <param name="rreshtiID"></param>
        /// <param name="idAmbjenti"></param>
        /// <returns></returns>
        public static clsMesazh Fshi(int rreshtiID, int idAmbjenti)
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate fshirjes se zerave");

            try
            {
                dbAB.beginTransaksion();

                mesazhi = AnalizeBuxheti.FshiRreshtinNgaAmbjenti(rreshtiID, idAmbjenti, dbAB);
                if (mesazhi.Status)
                {
                    mesazhi = dbAB.FshiRreshtaAmbjenti(rreshtiID);

                    if (mesazhi.Status)
                    {
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate fshirjes se zerave" + err.Message);
            }
            return mesazhi;
        }

        public static clsMesazh EkzistonKyKod(string kodi, int idAmbjenti, int idNdermarrje)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                clsMesazh mesazhi = new clsMesazh(); // true nese kodi ekziston
                mesazhi.Status = dbAB.EkzistonKyKodRreshtaAmbjenti(kodi, idAmbjenti, idNdermarrje);
                if (mesazhi.Status)
                    mesazhi.PershkrimMesazhi = "Ekziston nje ze me kete kod!";
                return mesazhi;
            }
        }

        internal void Mbush(IDataRecord record)
        {
            RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0;
            KodiRreshtit = !Convert.IsDBNull(record["KODI_RRESHTIT"]) ? Convert.ToString(record["KODI_RRESHTIT"]) : "";
            NjesiaMatese = !Convert.IsDBNull(record["NJESIAMATESE"]) ? Convert.ToString(record["NJESIAMATESE"]) : "";
            PershkrimiRreshtit = !Convert.IsDBNull(record["PERSHKRIMI_RRESHTIT"]) ? Convert.ToString(record["PERSHKRIMI_RRESHTIT"]) : "";
            IdAmbjenti = !Convert.IsDBNull(record["IDAMBJENTI"]) ? Convert.ToInt32(record["IDAMBJENTI"]) : 0;
            IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
            IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0;
        }
    }
}