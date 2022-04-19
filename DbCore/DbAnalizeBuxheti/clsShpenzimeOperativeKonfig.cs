using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsShpenzimeOperativeKonfig
    {
        #region ATRIBUTET

        public int ShokId { get; set; }

        public String Kodi { get; set; }

        public string Pershkrimi { get; set; }

        public int IdPrindi { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public int IdNdermarrje { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public string KodiPrindit { get; set; }

        public int Niveli { get; set; }

        public int NiveliPrindit { get; set; }

        public int IdNdermVit { get; set; }

        #endregion ATRIBUTET

        public clsShpenzimeOperativeKonfig()
        {
        }

        public clsShpenzimeOperativeKonfig(int shokId)
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            dbAB.MbushOperativeKonfigSipasID(this, shokId);
        }

        public static clsShpenzimeOperativeKonfig Krijo(IDataRecord record)
        {
            return new clsShpenzimeOperativeKonfig
            {
                ShokId = !Convert.IsDBNull(record["SHOKID"]) ? Convert.ToInt32(record["SHOKID"]) : 0,
                Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : string.Empty,
                Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : string.Empty,
                IdPrindi = !Convert.IsDBNull(record["IDPRINDI"]) ? Convert.ToInt32(record["IDPRINDI"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,
                KodiPrindit = !Convert.IsDBNull(record["KODIPRINDIT"]) ? Convert.ToString(record["KODIPRINDIT"]) : string.Empty,
                Niveli = !Convert.IsDBNull(record["NIVELI"]) ? Convert.ToInt32(record["NIVELI"]) : 0,
                NiveliPrindit = !Convert.IsDBNull(record["NIVELIPRINDIT"]) ? Convert.ToInt32(record["NIVELIPRINDIT"]) : 1,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null
            };
        }

        public clsMesazh Ruaj()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();

            clsMesazh mesazh = null;
            int shokId = -1;

            try
            {
                ///kontrollohet nese prindi qe i eshte caktuar ketij shpenzimi operativ eshte perdorur  ne regjistrim
                if (KaRegjistrimeMeKeteShpenzimOperativ(ShokId))
                {
                    return new clsMesazh(false, "Nuk mund te vendoset ky prind per  " + Kodi + " sepse eshte perdorur ne regjistrime!");
                }
                dbAB.beginTransaksion();

                mesazh = dbAB.RuajShpenzimeOperativeKonfig(out shokId, Kodi, Pershkrimi, IdPrindi, IdKrijuesi, IdNdermarrje, Niveli, IdNdermVit);
                if (mesazh.Status)
                {
                    dbAB.commitTransaksion();
                    ShokId = shokId;
                }
                else
                    dbAB.rollbackTransaksion();
            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se konfigurimit te shpenzimeve operative \n" + err.Message);
            }
            return mesazh;
        }

        public static clsMesazh Fshi(int shokId, int idModifikuesi)
        {
            clsMesazh mesazh = null;

            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            try
            {
                mesazh = dbAB.fshiKonfgurimShpenzimeOperativeUpdDel(shokId, idModifikuesi);
                if (mesazh.Status)
                    dbAB.commitTransaksion();
                else dbAB.rollbackTransaksion();
            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate fshirjes se konfigurimit te shpenzimeve operative \n" + err.Message);
            }
            return mesazh;
        }

        public clsMesazh Fshi()
        {
            return Fshi(ShokId, IdModifikuesi);
        }

        /// <summary>
        /// kontrollon nese ky shpenzimOperativ sherben si prind i nje shpenzimOperativ tjeter
        /// </summary>
        /// <param name="shokID"></param>
        /// <returns></returns>

        public static bool KaFemij(int shokID)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.kaFemijShpenzimiOperativ(shokID);
            }
        }
        /// <summary>
        /// kontrollon nese per kete shpenzim operativ jane vendosur vlera
        /// </summary>
        /// <param name="shokID"></param>
        /// <returns></returns>
        public static bool KaRegjistrimeMeKeteShpenzimOperativ(int shokID)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KaRegjistrimeMeKeteShpenzimOperativ(shokID);
            }
        }
        public clsMesazh Modifiko(colShpenzimeOperativeKonfig newCol)
        {



            if (IdPrindi == ShokId)
                return new clsMesazh(false, "Shpenzimit me kodin " + Kodi + "Nuk mund ti caktohet prind vetvetja!");

            if (KaRegjistrimeMeKeteShpenzimOperativ(IdPrindi))
                return new clsMesazh(false, "Prindi i shpenzimit operativ me kodin " + Kodi + " eshte perdorur ne regjistrim!");

            IEnumerable<clsShpenzimeOperativeKonfig> teGjitheVecKetij = newCol.Where(x => x.ShokId != ShokId);
            clsShpenzimeOperativeKonfig prindiPotencial = newCol.Find(x => x.ShokId == IdPrindi);

            if (AnalizeBuxheti.FormohetCikel(teGjitheVecKetij, this, prindiPotencial))
                return new clsMesazh(false, "Nuk mund te vendoset ky prind sepse formohet cikel!");

            return Modifiko(new clsDatabaseAnalizeBuxheti());
        }
        public clsMesazh Modifiko(clsDatabaseAnalizeBuxheti dbAB)
        {
            clsMesazh mesazh = null;
            try
            {
                mesazh = dbAB.ModifikoShpenzimeOperativeKonfig(ShokId, Kodi, Pershkrimi, IdPrindi, IdNdermarrje, IdModifikuesi, Niveli, IdNdermVit);
                if (mesazh.Status)
                    dbAB.commitTransaksion();
                else dbAB.rollbackTransaksion();
            }
            catch (Exception err)
            {
                dbAB.rollbackTransaksion();
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te konfigurimit te shpenzimeve operative \n" + err.Message);
            }
            return mesazh;
        }


        /// <summary>
        /// kontrollon nese ka prind kategoria e shpenzimit,nese nuk ka mund ti caktohet 
        /// </summary>
        /// <param name="shokID"></param>
        /// <returns></returns>
        public bool KaPrind(int shokID)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.KaPrindShpenzimiOperativ(shokID);
            }
        }


        public static int MerrNivelinSipasID(int idPrindi)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.MerrNivelShpenzimiOperativ(idPrindi);
            }

        }

        public static clsMesazh EkzistonKyKod(string kodi, int idNdermarrje)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                clsMesazh mesazhi = new clsMesazh(); // true nese kodi ekziston
                 mesazhi.Status = dbAB.EkzistonKyKodShpenzimeOperative(kodi, idNdermarrje);
                if (mesazhi.Status)
                    mesazhi.PershkrimMesazhi = "Ekziston nje shpenzim me kete kod!";
                return mesazhi;
            }
        }
    }
}