using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsParashikimShpenzPersoneliConfig
    {
        #region atributet
        public int IdAuto { get; set; }
        public int PShPConfigId { get; set; }

        public int RreshtiId { get; set; }

        public string KlasaKategoria { get; set; }

        public decimal PagaBaze { get; set; }

        public decimal PagaPozicion { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public string Emertimi { get; set; }

        public string Kodi { get; set; }

        public int IdNdermVit { get; set; }

        public bool Prind { get; set; }

        public int IdPrindi { get; set; }

        private clsRreshtaAmbjenti zerat;
        #endregion atributet

        public clsParashikimShpenzPersoneliConfig() {
            zerat = new clsRreshtaAmbjenti();
        }

        public clsParashikimShpenzPersoneliConfig(int pShPConfigId, int rreshtiId, string klasaKategoria, decimal pagaBaze, decimal pagaPozicion, int idStatusDok, int idNdermarrje, int idModifikuesi, int idNdermVit, bool prind, int idPrindi)
        {
            PShPConfigId = pShPConfigId;
            RreshtiId = rreshtiId;
            KlasaKategoria = klasaKategoria;
            PagaBaze = pagaBaze;
            PagaPozicion = pagaPozicion;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            IdModifikuesi = idModifikuesi;
            IdNdermVit = IdNdermVit;
            Prind = prind;
            IdPrindi = idPrindi;
            zerat = new clsRreshtaAmbjenti(rreshtiId);
        }

        /// <summary>
        /// krijon nje objekt  clsParashikimShpenzPersoneliConfig,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsParashikimShpenzPersoneliConfig Krijo(IDataRecord record)
        {
            return new clsParashikimShpenzPersoneliConfig
            {
                PShPConfigId = !Convert.IsDBNull(record["PSHPKONFIGID"]) ? Convert.ToInt32(record["PSHPKONFIGID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                KlasaKategoria = !Convert.IsDBNull(record["KLASA_KATEGORIA"]) ? Convert.ToString(record["KLASA_KATEGORIA"]) : "",
                PagaBaze = !Convert.IsDBNull(record["PAGA_BAZE"]) ? Convert.ToDecimal(record["PAGA_BAZE"]) : 0,
                PagaPozicion = !Convert.IsDBNull(record["PAGA_POZICION"]) ? Convert.ToDecimal(record["PAGA_POZICION"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdPrindi = !Convert.IsDBNull(record["IDPRINDI"]) ? Convert.ToInt32(record["IDPRINDI"]) : 0,
                Prind = !Convert.IsDBNull(record["PRIND"]) ? Convert.ToBoolean(record["PRIND"]) : false,
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Emertimi = !Convert.IsDBNull(record["EMERTIMI"]) ? Convert.ToString(record["EMERTIMI"]) : "",
                Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : "",
                IdAuto = !Convert.IsDBNull(record["IDAUTO"]) ? Convert.ToInt32(record["IDAUTO"]) : 0
            };
        }

        public clsMesazh Modifiko()
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit");

                try
                {
                    dbAB.beginTransaksion();

                    zerat.KodiRreshtit = Kodi;
                    zerat.PershkrimiRreshtit = Emertimi;
                    zerat.RreshtiId = RreshtiId;
                    zerat.IdModifikuesi = IdModifikuesi;
                    zerat.IdNdermarrje = IdNdermarrje;
                    zerat.IdAmbjenti = 3;
                    zerat.IdNdermVit = IdNdermVit;

                    mesazhi = zerat.ModifikoRreshtaParashikimShpenzimesh(dbAB);
                    if (!mesazhi.Status)
                    {
                        dbAB.rollbackTransaksion();
                    }
                    else
                    {

                        mesazhi = dbAB.updateParashikimShpenzPersoneliConfig(PShPConfigId, RreshtiId, KlasaKategoria, PagaBaze, PagaPozicion, IdNdermarrje, IdModifikuesi, IdNdermVit, Prind,IdPrindi);
                        if (mesazhi.Status)
                            dbAB.commitTransaksion();
                        else
                            dbAB.rollbackTransaksion();
                    }

                }
                catch (Exception err)
                {

                    dbAB.rollbackTransaksion();
                    mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te konfigurimit te parashikimit \n" + err.Message);
                }

                return mesazhi;
            }
        }



        public clsMesazh Ruaj()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerave");

            try
            {
                dbAB.beginTransaksion();

                int rreshtiId = -1;

                mesazhi = dbAB.RuajRreshtaAmbjenti(out rreshtiId, Kodi, Emertimi, 3, IdNdermarrje, IdKrijuesi, "", IdNdermVit);

                if (mesazhi.Status)
                {
                    RreshtiId = rreshtiId;
                    int pshpConfigId = -1;
                    mesazhi = dbAB.RuajParashikimShpenzPersoneliConfig(out pshpConfigId, RreshtiId, KlasaKategoria, PagaBaze, PagaPozicion, IdNdermarrje, IdKrijuesi, IdNdermVit, Prind, IdPrindi);

                    if (mesazhi.Status)
                    {
                        dbAB.commitTransaksion();
                        PShPConfigId = pshpConfigId;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te zerave \n" + err.Message);
            }
            return mesazhi;
        }

        /// <summary>
        /// fshin nga tabela rreshtin e caktuar
        /// </summary>
        /// <param name="rreshtiID"></param>
        /// <param name="dbAB"></param>
        /// <returns></returns>
        public static clsMesazh Fshi(int rreshtiID, clsDatabaseAnalizeBuxheti dbAB)
        {
            clsMesazh mesazh = dbAB.fshiRreshtNgaParashikimShpenzKonfig(rreshtiID);
            if (mesazh.Status)
                mesazh = dbAB.FshiRreshtaAmbjenti(rreshtiID);
            return mesazh;
        }

        public static clsMesazh Fshi(int rreshtiID)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return Fshi(rreshtiID, dbAB);
            }
        }
    }
}
