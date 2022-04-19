using System;
using System.Data;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsKokaEvidencaStatistikore
    {
        public int IdKokaDok { get; set; }
        public int TreMujori { get; set; }
        public int GjyqtarPlan { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdModifikuesi { get; set; }
        public int IdNdermarrje { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public int IdStatusDok { get; set; }

        public int IdNdermVit { get; set; }
        public colTrupiEvidencaStatistikore ColTrupi { get; set; }

        public static clsKokaEvidencaStatistikore Krijo(IDataRecord record)
        {
            return new clsKokaEvidencaStatistikore
            {
                IdKokaDok = !Convert.IsDBNull(record["IDKOKADOK"]) ? Convert.ToInt32(record["IDKOKADOK"]) : 0,
                TreMujori = !Convert.IsDBNull(record["TREMUJORI"]) ? Convert.ToInt32(record["TREMUJORI"]) : 0,
                GjyqtarPlan = !Convert.IsDBNull(record["GJYQTARPLAN"]) ? Convert.ToInt32(record["GJYQTARPLAN"]) : 0,
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0,
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0,
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null,
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null,
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0,
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0,

            };
        }

        public void Mbush(IDataRecord record)
        {
                IdKokaDok = !Convert.IsDBNull(record["IDKOKADOK"]) ? Convert.ToInt32(record["IDKOKADOK"]) : 0;
                TreMujori = !Convert.IsDBNull(record["TREMUJORI"]) ? Convert.ToInt32(record["TREMUJORI"]) : 0;
                GjyqtarPlan = !Convert.IsDBNull(record["GJYQTARPLAN"]) ? Convert.ToInt32(record["GJYQTARPLAN"]) : 0;
                IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
                IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
                IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
                DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
                DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
                IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0;
                IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0;
        }
        public clsKokaEvidencaStatistikore()
        {
            ColTrupi = new colTrupiEvidencaStatistikore();
        }
        public clsKokaEvidencaStatistikore(int idKoka)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                dbAB.MerrEvidencaStatistikoreSipasId(idKoka, this);
                ColTrupi = new colTrupiEvidencaStatistikore(idKoka);
            }
        }
        public clsMesazh Ruaj()
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                dbAB.beginTransaksion();
                clsMesazh mesazh = Ruaj(dbAB);
                if (mesazh.Status)
                    dbAB.commitTransaksion();

                return mesazh;
            }
        }
        public clsMesazh EkzistonNjeDokumentPerKetePeriudhe()
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {

                if (dbAB.EkzistonNjeDokumentPerKetePeriudhe(IdNdermarrje, TreMujori, IdNdermVit))
                    return new clsMesazh(false, "Ekziston nje dokument per kete periudhe!");
                else return new clsMesazh(true);
            }
        }
        public clsMesazh Ruaj(clsDatabaseAnalizeBuxheti dbAbB)
        {

            clsMesazh mesazhi = new clsMesazh();
            try
            {
                // dbAbB.beginTransaksion();

                int idKokaDok = -1;

                mesazhi = dbAbB.RuajKokenEvidencaStatistikore(out idKokaDok, TreMujori, GjyqtarPlan, IdKrijuesi, IdNdermarrje, IdNdermVit);
                IdKokaDok = idKokaDok;
                if (!mesazhi.Status)
                {
                    dbAbB.rollbackTransaksion();
                    return mesazhi;

                }
                foreach (clsTrupiEvidencaStatistikore trup in ColTrupi)
                {
                    trup.IdStatusDok = 1;
                    trup.IdKokaDok = IdKokaDok;
                    mesazhi = trup.Ruaj(dbAbB);
                    if (!mesazhi.Status)
                    {
                        dbAbB.rollbackTransaksion();
                        break;
                    }
                }
                
                return new clsMesazh(true, "Ruajta u krye me sukses!");
            }
            catch (Exception ex)
            {
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se evidencave statistikore! " + ex.Message);
                dbAbB.rollbackTransaksion();
            }
            return new clsMesazh();


        }

        public clsMesazh Fshi()
        {
            using (clsDatabaseAnalizeBuxheti dbAb = new clsDatabaseAnalizeBuxheti())
            {
                try
                {
                    clsMesazh mesazh = Fshi(dbAb);
                    if (mesazh.Status)
                        return new clsMesazh(true, "Fshirja u krye me sukses!");
                    else
                        return new clsMesazh(false, "Ndodhi nje gabim gjate fshirjes!");
                }
                catch (Exception ex)
                {
               
                    return new clsMesazh(false, "Ndodhi nje gabim gjate fshirjes! \n" + ex.Message);
                }

            }
        }
        public clsMesazh Fshi(clsDatabaseAnalizeBuxheti dbAb)
        {
            clsMesazh mesazh = dbAb.FshiUpdateStatusDokEvidencaStatistikore(IdKokaDok, IdModifikuesi);
            return mesazh;

        }
        public clsMesazh Fshi(int idKokaDok)
        {

            IdKokaDok = idKokaDok;
            return Fshi();

        }
        public clsMesazh Modifiko()
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                clsMesazh mesazh = new clsMesazh();
                try
                {

                    dbAB.beginTransaksion();
                    mesazh = Fshi(dbAB);
                    if (!mesazh.Status)
                    {
                        dbAB.rollbackTransaksion();
                        return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te evidences se vjeter");
                    }
                    mesazh = Ruaj(dbAB);
                    if (!mesazh.Status)
                    {
                        dbAB.rollbackTransaksion();
                        return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te evidences statistikore!\n");
                    }
                    dbAB.commitTransaksion();
                    return mesazh;
                }
                catch (Exception ex)
                {
                    dbAB.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te evidences statistikore!\n" + ex.Message);
                }
            }
        }

        public clsMesazh EkzistonNjeDokumentMeKetePlan(clsDatabaseAnalizeBuxheti dbAB)
        {
            if (dbAB.EkzistonKyDokumentEvidencaStatistikore(GjyqtarPlan, TreMujori, IdNdermarrje, IdNdermVit))
                return new clsMesazh(false, "Ekziston nje dokument me kete numer!");
            else return new clsMesazh(true);

        }
        public clsMesazh EkzistonNjeDokumentMeKetePlan()
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
                return EkzistonNjeDokumentMeKetePlan(dbAB);

        }

        internal static clsMesazh Fshi(int rreshtiID, clsDatabaseAnalizeBuxheti dbAB)
        {
            return dbAB.fshiRreshtNgaEvidencaStatistikore(rreshtiID);
        }
    }
}
