using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsInventariVite
    {
        public int InventariId { get; set; }

        public int RreshtiId { get; set; }

        public int Viti2003 { get; set; }

        public int Viti2004 { get; set; }

        public int Viti2005 { get; set; }

        public int Viti2006 { get; set; }

        public int Viti2007 { get; set; }

        public int Viti2008 { get; set; }

        public int Viti2009 { get; set; }

        public int Viti2010 { get; set; }

        public int Viti2011 { get; set; }

        public int Viti2012 { get; set; }

        public int Viti2013 { get; set; }

        public int Viti2014 { get; set; }

        public int Viti2015 { get; set; }

        public int Viti2016 { get; set; }

        public int Viti2017 { get; set; }

        public int Viti2018 { get; set; }

        public int Viti2019 { get; set; }

        public int Viti2020 { get; set; }

        public DateTime? DtKrijimi { get; set; }

        public DateTime? DtModifikimi { get; set; }

        public int IdStatusDok { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdKrijuesi { get; set; }

        public int IdModifikuesi { get; set; }

        public string Artikulli { get; set; }

        public clsInventariVite() { }
        public clsInventariVite(int inventariId, int rreshtiId, int viti2003, int viti2004, int viti2005, int viti2006,
             int viti2007, int viti2008, int viti2009, int viti2010, int viti2011, int viti2012,
             int viti2013, int viti2014, int viti2015, int viti2016, int viti2017, int viti2018,
             int viti2019, int viti2020, int idStatusDok, int idNdermarrje, int idModifikuesi)
        {
            InventariId = inventariId;
            RreshtiId = rreshtiId;
            Viti2003 = viti2003;
            Viti2004 = viti2004;
            Viti2005 = viti2005;
            Viti2006 = viti2006;
            Viti2007 = viti2007;
            Viti2008 = viti2008;
            Viti2009 = viti2009;
            Viti2010 = viti2010;
            Viti2011 = viti2011;
            Viti2012 = viti2012;
            Viti2013 = viti2013;
            Viti2014 = viti2014;
            Viti2015 = viti2015;
            Viti2016 = viti2016;
            Viti2017 = viti2017;
            Viti2018 = viti2018;
            Viti2019 = viti2019;
            Viti2020 = viti2020;
            IdModifikuesi = idModifikuesi;
            IdNdermarrje = idNdermarrje;
            IdStatusDok = idStatusDok;
        }

        /// <summary>
        /// krijon nje objekt  clsInventariVite,
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static clsInventariVite Krijo(IDataRecord record)
        {
            return new clsInventariVite
            {
                InventariId = !Convert.IsDBNull(record["INVENTARIID"]) ? Convert.ToInt32(record["INVENTARIID"]) : 0,
                RreshtiId = !Convert.IsDBNull(record["RRESHTIID"]) ? Convert.ToInt32(record["RRESHTIID"]) : 0,
                Viti2003 = !Convert.IsDBNull(record["Viti2003"]) ? Convert.ToInt32(record["VITI2003"]) : 0,
                Viti2004 = !Convert.IsDBNull(record["Viti2004"]) ? Convert.ToInt32(record["Viti2004"]) : 0,
                Viti2005 = !Convert.IsDBNull(record["Viti2005"]) ? Convert.ToInt32(record["Viti2005"]) : 0,
                Viti2006 = !Convert.IsDBNull(record["Viti2006"]) ? Convert.ToInt32(record["VITI2006"]) : 0,
                Viti2007 = !Convert.IsDBNull(record["Viti2007"]) ? Convert.ToInt32(record["VITI2007"]) : 0,
                Viti2008 = !Convert.IsDBNull(record["Viti2008"]) ? Convert.ToInt32(record["VITI2008"]) : 0,
                Viti2009 = !Convert.IsDBNull(record["Viti2009"]) ? Convert.ToInt32(record["VITI2009"]) : 0,
                Viti2010 = !Convert.IsDBNull(record["Viti2010"]) ? Convert.ToInt32(record["VITI2010"]) : 0,
                Viti2011 = !Convert.IsDBNull(record["Viti2011"]) ? Convert.ToInt32(record["VITI2011"]) : 0,
                Viti2012 = !Convert.IsDBNull(record["Viti2012"]) ? Convert.ToInt32(record["VITI2012"]) : 0,
                Viti2013 = !Convert.IsDBNull(record["Viti2013"]) ? Convert.ToInt32(record["VITI2013"]) : 0,
                Viti2014 = !Convert.IsDBNull(record["Viti2014"]) ? Convert.ToInt32(record["VITI2014"]) : 0,
                Viti2015 = !Convert.IsDBNull(record["Viti2015"]) ? Convert.ToInt32(record["VITI2015"]) : 0,
                Viti2016 = !Convert.IsDBNull(record["Viti2016"]) ? Convert.ToInt32(record["VITI2016"]) : 0,
                Viti2017 = !Convert.IsDBNull(record["Viti2017"]) ? Convert.ToInt32(record["VITI2017"]) : 0,
                Viti2018 = !Convert.IsDBNull(record["Viti2018"]) ? Convert.ToInt32(record["VITI2018"]) : 0,
                Viti2019 = !Convert.IsDBNull(record["Viti2019"]) ? Convert.ToInt32(record["VITI2019"]) : 0,
                Viti2020 = !Convert.IsDBNull(record["Viti2020"]) ? Convert.ToInt32(record["VITI2020"]) : 0,
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
                mesazhi = dbAB.fshiUpdateStatusDokInventariVite(InventariId, IdModifikuesi);
                if (mesazhi.Status)
                {
                    int inventariId = -1;
                    mesazhi = dbAB.RuajInventariVite(out inventariId, RreshtiId, Viti2003, Viti2004, Viti2005, Viti2006,
                        Viti2007, Viti2008, Viti2009, Viti2010, Viti2011, Viti2012, Viti2013, Viti2014, Viti2015, Viti2016,
                        Viti2017, Viti2018, Viti2019, Viti2020,IdNdermarrje, IdModifikuesi);

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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te Inventarit sipas viteve \n" + err.Message);
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
            return dbAB.fshiRreshtNgaInventariVite(rreshtiID);
        }
    }
}
