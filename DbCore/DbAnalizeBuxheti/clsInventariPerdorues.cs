using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsInventariPerdorues
    {
        #region atributet

        public int InventariId { get; set; }

        public int RreshtiId { get; set; }

        public int Gjyqtare { get; set; }

        public int Ndihmes { get; set; }

        public int Sekretare { get; set; }

        public int Administrata { get; set; }

        public int SallaCivile { get; set; }

        public int SallaPenale { get; set; }

        public int Sherbimi { get; set; }

        public int Tjeter { get; set; }
      
        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public string Artikulli { get; set; }

        #endregion atributet

        public clsInventariPerdorues() { }

        public clsInventariPerdorues(int inventariId, int rreshtiId, int gjyqtare, int ndihmes, int sekretare, int administrata, int sallaCivile, int sallaPenale, int sherbimi, int tjeter, int idStatusDok, int idNdermarrje, int idModifikuesi)
        {
            InventariId = inventariId;
            RreshtiId = rreshtiId;
            Gjyqtare = gjyqtare;
            Ndihmes = ndihmes;
            Administrata = administrata;
            SallaCivile = sallaCivile;
            SallaPenale = sallaPenale;
            Sherbimi = sherbimi;
            Tjeter = tjeter;
            IdModifikuesi = idModifikuesi;
            IdNdermarrje = idNdermarrje;
            IdStatusDok = idStatusDok;
        }

        /// <summary>
        /// krijon nje objekt  clsInventariPerdorues,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsInventariPerdorues Krijo(IDataRecord record)
        {
            return new clsInventariPerdorues
            {
                InventariId = !Convert.IsDBNull(record["INVENTARIID"]) ? Convert.ToInt32(record["INVENTARIID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                Gjyqtare = !Convert.IsDBNull(record["GJYQTARE"]) ? Convert.ToInt32(record["GJYQTARE"]) : 0,
                Ndihmes = !Convert.IsDBNull(record["NDIHMES"]) ? Convert.ToInt32(record["NDIHMES"]) : 0,
                Sekretare = !Convert.IsDBNull(record["SEKRETARE"]) ? Convert.ToInt32(record["SEKRETARE"]) : 0,
                Administrata = !Convert.IsDBNull(record["ADMNISTRATA"]) ? Convert.ToInt32(record["ADMNISTRATA"]) : 0,
                SallaCivile = !Convert.IsDBNull(record["SALLA_CIVILE"]) ? Convert.ToInt32(record["SALLA_CIVILE"]) : 0,
                SallaPenale = !Convert.IsDBNull(record["SALLA_PENALE"]) ? Convert.ToInt32(record["SALLA_PENALE"]) : 0,
                Sherbimi = !Convert.IsDBNull(record["SHERBIMI"]) ? Convert.ToInt32(record["SHERBIMI"]) : 0,
                Tjeter = !Convert.IsDBNull(record["TJETER"]) ? Convert.ToInt32(record["TJETER"]) : 0,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                Artikulli = !Convert.IsDBNull(record["ARTIKULLI"]) ? Convert.ToString(record["ARTIKULLI"]) : String.Empty


            };
        }

        public clsMesazh Modifiko()
        {
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit");

            try
            {
                dbAB.beginTransaksion();
                mesazhi = dbAB.fshiUpdateStatusDokInventariPerdorues(InventariId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int inventariId = -1;
                    mesazhi = dbAB.RuajInventariPerdorues(out inventariId, RreshtiId, Gjyqtare, Ndihmes,
                        Sekretare, Administrata, SallaCivile,
                        SallaPenale, Sherbimi, Tjeter, IdNdermarrje, IdModifikuesi);

                    if (mesazhi.Status)
                    {
                        InventariId = inventariId;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te Inventarit sipas perdoruesit \n" + err.Message);
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
            return dbAB.fshiRreshtNgaInventariPerdorues(rreshtiID);
        }
    }
}
