using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsParashikimiTeArdhura
    {
        #region atributet
        //private static int index = 0;
        public int IdAuto { get ; set; }
        public int PTaId { get; set; }

        public int RreshtiId { get; set; }

        public decimal TeArdhuraTotaleParaardhes { get; set; }

        public decimal ITakojneInstitucionitAktuale { get; set; }

        public decimal DerdhenNeBuxhetAktuale { get; set; }

        public decimal ITakojneInstitucionitPasardhes { get; set; }

        public decimal DerdhenNeBuxhetPasardhes { get; set; }

        public decimal ParashikimiPlus2 { get; set; }

        public decimal ParashikimiPlus3 { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public string Emertimi { get; set; }

        public string Kodi { get; set; }
        public int IdNdermVit { get; set; }

        #endregion atributet

        public clsParashikimiTeArdhura() { }

        public clsParashikimiTeArdhura(int ptaId, int rreshtiId, decimal teArdhuraTotaleParaardhes, decimal iTakojneInstitucionitAktuale, decimal derdhenNeBuxhetAktuale, decimal iTakojneInstitucionitPasardhes, decimal derdhenNeBuxhetPasardhes, decimal parashikimiPlus2, decimal parashikimiPlus3, int idStatusDok, int idNdermarrje, int idModifikuesi, int idNdermVit)
        {
            PTaId = ptaId;
            RreshtiId = rreshtiId;
            TeArdhuraTotaleParaardhes = teArdhuraTotaleParaardhes;
            ITakojneInstitucionitAktuale = iTakojneInstitucionitAktuale;
            DerdhenNeBuxhetAktuale = derdhenNeBuxhetAktuale;
            ITakojneInstitucionitPasardhes = ITakojneInstitucionitPasardhes;
            DerdhenNeBuxhetPasardhes = derdhenNeBuxhetPasardhes;
            ParashikimiPlus2 = parashikimiPlus2;
            ParashikimiPlus3 = parashikimiPlus3;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            IdModifikuesi = IdModifikuesi;
            IdNdermVit = idNdermVit;

        }

        /// <summary>
        /// krijon nje objekt  clsParashikimiTeArdhura,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsParashikimiTeArdhura Krijo(IDataRecord record)
        {
            return new clsParashikimiTeArdhura
            {
               IdAuto= !Convert.IsDBNull(record["IdAuto"]) ? Convert.ToInt32(record["IdAuto"]) : 0,
                PTaId = !Convert.IsDBNull(record["PTAID"]) ? Convert.ToInt32(record["PTAID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                TeArdhuraTotaleParaardhes = !Convert.IsDBNull(record["TE_ARDHURATOTALE_PARAARDHES"]) ? Convert.ToDecimal(record["TE_ARDHURATOTALE_PARAARDHES"]) : 0,
                ITakojneInstitucionitAktuale = !Convert.IsDBNull(record["ITAKOJNE_INSTITUCIONIT_AKTUALE"]) ? Convert.ToDecimal(record["ITAKOJNE_INSTITUCIONIT_AKTUALE"]) : 0,
                DerdhenNeBuxhetAktuale = !Convert.IsDBNull(record["DERDHEN_NE_BUXHET_AKTUALE"]) ? Convert.ToDecimal(record["DERDHEN_NE_BUXHET_AKTUALE"]) : 0,
                ITakojneInstitucionitPasardhes = !Convert.IsDBNull(record["ITAKOJNE_INSTITUCIONIT_PASARDHES"]) ? Convert.ToDecimal(record["ITAKOJNE_INSTITUCIONIT_PASARDHES"]) : 0,
                DerdhenNeBuxhetPasardhes = !Convert.IsDBNull(record["DERDHEN_NE_BUXHET_PASARDHES"]) ? Convert.ToDecimal(record["DERDHEN_NE_BUXHET_PASARDHES"]) : 0,
                ParashikimiPlus2 = !Convert.IsDBNull(record["PARASHIKIMI_PLUS2"]) ? Convert.ToDecimal(record["PARASHIKIMI_PLUS2"]) : 0,
                ParashikimiPlus3 = !Convert.IsDBNull(record["PARASHIKIMI_PLUS3"]) ? Convert.ToDecimal(record["PARASHIKIMI_PLUS3"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Emertimi = !Convert.IsDBNull(record["EMERTIMI"]) ? Convert.ToString(record["EMERTIMI"]) : "",
                Kodi = !Convert.IsDBNull(record["KODIEMERTIMI"]) ? Convert.ToString(record["KODIEMERTIMI"]) : "",
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,

            };
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit");

            try
            {
                dbAB.beginTransaksion();
                mesazhi = dbAB.fshiUpdateStatusDokParashikimiTeArdhura(PTaId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int pTaId = -1;
                    mesazhi = dbAB.RuajParashikimiTeArdhura(out pTaId, RreshtiId, TeArdhuraTotaleParaardhes, ITakojneInstitucionitAktuale,
                        DerdhenNeBuxhetAktuale, ITakojneInstitucionitPasardhes, DerdhenNeBuxhetPasardhes,
                        ParashikimiPlus2, ParashikimiPlus3, IdNdermarrje, IdModifikuesi, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        PTaId = pTaId;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te parashikimit te te ardhurave \n" + err.Message);
            }
            return mesazhi;
        }


        /// <summary>
        /// fshin nga tabela rreshtin e caktuar
        /// </summary>
        /// <param name="rreshtiID"></param>
        /// <param name="dbAB"></param>
        /// <returns></returns>
        internal static clsMesazh Fshi(int rreshtiID, clsDatabaseAnalizeBuxheti dbAB)
        {
            return dbAB.fshiRreshtNgaParashikimTeArdhura(rreshtiID);
        }

    }
}
