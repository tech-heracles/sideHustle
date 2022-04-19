using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsPBuxhetPermbledhes
    {
        #region atributet

        public int PBuxhetId { get; set; }

        public int RreshtiId { get; set; }

        public decimal Pagat { get; set; }

        public decimal FondiVecante { get; set; }

        public decimal SigurimeShoqerore { get; set; }

        public decimal MallRadheSherbime { get; set; }

        public decimal Subvencione { get; set; }

        public decimal TransferimKorrBrendshme { get; set; }

        public decimal TransferimKorrHuaja { get; set; }

        public decimal ShpenzimeKapitalePaTrup { get; set; }

        public decimal ShpenzimeKapitaleTrup { get; set; }

        public decimal TransfertaKapitale { get; set; }

        public decimal Totali { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public string Emertimi { get; set; }

        public int IdNdermVit { get; set; }

        #endregion atributet

        public clsPBuxhetPermbledhes()
        {
        }

        public clsPBuxhetPermbledhes(int pbBuxhetId, int rreshtiId, decimal pagat, decimal fondiVecante, decimal sigurimeShoqerore, decimal mallRadheSherbime, decimal subvencione, decimal transferimKorrBrendshme, decimal transferimKorrHuaja, decimal shpenzimeKapitalePaTrup, decimal shpenzimeKapitaleTrup, decimal transfertaKapitale, int idStatusDok, int idNdermarrje, int idModifikuesi, int idNdermVit)
        {
            PBuxhetId = pbBuxhetId;
            RreshtiId = rreshtiId;
            Pagat = pagat;
            FondiVecante = fondiVecante;
            SigurimeShoqerore = sigurimeShoqerore;
            MallRadheSherbime = mallRadheSherbime;
            Subvencione = subvencione;
            TransferimKorrBrendshme = transferimKorrBrendshme;
            TransferimKorrHuaja = transferimKorrHuaja;
            ShpenzimeKapitalePaTrup = shpenzimeKapitalePaTrup;
            ShpenzimeKapitaleTrup = shpenzimeKapitaleTrup;
            TransfertaKapitale = transfertaKapitale;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            IdModifikuesi = IdModifikuesi;
            IdNdermVit = idNdermVit;
           
        }

        /// <summary>
        /// krijon nje objekt  clsPBuxhetPermbledhes,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsPBuxhetPermbledhes Krijo(IDataRecord record)
        {
            return new clsPBuxhetPermbledhes
            {
                PBuxhetId = !Convert.IsDBNull(record["PBUXHETID"]) ? Convert.ToInt32(record["PBUXHETID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                Pagat = !Convert.IsDBNull(record["PAGAT"]) ? Convert.ToDecimal(record["PAGAT"]) : 0,
                FondiVecante = !Convert.IsDBNull(record["FONDIVECANTE"]) ? Convert.ToDecimal(record["FONDIVECANTE"]) : 0,
                SigurimeShoqerore = !Convert.IsDBNull(record["SIGURIMESHOQERORE"]) ? Convert.ToDecimal(record["SIGURIMESHOQERORE"]) : 0,
                MallRadheSherbime = !Convert.IsDBNull(record["MALLRADHESHERBIME"]) ? Convert.ToDecimal(record["MALLRADHESHERBIME"]) : 0,
                Subvencione = !Convert.IsDBNull(record["SUBVENCIONIME"]) ? Convert.ToDecimal(record["SUBVENCIONIME"]) : 0,
                TransferimKorrBrendshme = !Convert.IsDBNull(record["TRANSFERIMKORRBRENDSHME"]) ? Convert.ToDecimal(record["TRANSFERIMKORRBRENDSHME"]) : 0,
                TransferimKorrHuaja = !Convert.IsDBNull(record["TRANSFERIMKORRHUAJA"]) ? Convert.ToDecimal(record["TRANSFERIMKORRHUAJA"]) : 0,
                ShpenzimeKapitalePaTrup = !Convert.IsDBNull(record["SHPENZIMEKAPITALEPATRUP"]) ? Convert.ToDecimal(record["SHPENZIMEKAPITALEPATRUP"]) : 0,
                ShpenzimeKapitaleTrup = !Convert.IsDBNull(record["SHPENZIMEKAPITALETRUP"]) ? Convert.ToDecimal(record["SHPENZIMEKAPITALETRUP"]) : 0,
                TransfertaKapitale = !Convert.IsDBNull(record["TRANSFERTAKAPITALE"]) ? Convert.ToDecimal(record["TRANSFERTAKAPITALE"]) : 0,
                Totali = !Convert.IsDBNull(record["TOTALI"]) ? Convert.ToDecimal(record["TOTALI"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Emertimi = !Convert.IsDBNull(record["EMERTIMI"]) ? Convert.ToString(record["EMERTIMI"]) : "",
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,
            };
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te projektbuxhetit");

            try
            {
                dbAB.beginTransaksion();

                mesazhi = dbAB.fshiUpdateStatusDokPBuxhetPermbledhes(PBuxhetId, IdModifikuesi, IdNdermVit);
                if (mesazhi.Status)
                {
                    int pbBuxhetID = -1;
                    mesazhi = dbAB.RuajPBuxhetPermbledhes(out pbBuxhetID, RreshtiId, Pagat, FondiVecante, SigurimeShoqerore, MallRadheSherbime, Subvencione, TransferimKorrBrendshme,TransferimKorrHuaja, ShpenzimeKapitalePaTrup, ShpenzimeKapitaleTrup, TransfertaKapitale, IdNdermarrje, IdModifikuesi, IdNdermVit);

                    if (mesazhi.Status)
                    {
                        PBuxhetId = pbBuxhetID;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te projekt buxhetit \n" + err.Message);
            }
            return mesazhi;
        }

        /// <summary>
        /// fshin nga tabela rreshtin e caktuar
        /// </summary>
        /// <param name="rreshtiID"></param>
        /// <param name="idAmbjenti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="dbAB"></param>
        /// <returns></returns>
        internal static clsMesazh Fshi(int rreshtiID, clsDatabaseAnalizeBuxheti dbAB)
        {
          return  dbAB.fshiRreshtNgaPbPermbledhes(rreshtiID);
        }
    }
}