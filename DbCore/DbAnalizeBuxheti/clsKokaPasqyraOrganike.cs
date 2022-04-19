using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsKokaPasqyraOrganike
    {

        public int IdKokaDok { get; set; }
        public string NrDok { get; set; }
        public DateTime? DtDok { get; set; }
        public int Muaji { get; set; }
        public int TotaliFemra { get; set; }
        public int TotaliMeshkuj { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdModifikuesi { get; set; }
        public int IdNdermarrje { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public int IdStatusDok { get; set; }

        public int IdNdermVit { get; set; }
        public colTrupiPasqyraOrganike ColTrupi { get; set; }

        public static clsKokaPasqyraOrganike Krijo(IDataRecord record)
        {
            return new clsKokaPasqyraOrganike
            {
                IdKokaDok = !Convert.IsDBNull(record["IDKOKADOK"]) ? Convert.ToInt32(record["IDKOKADOK"]) : 0,
                NrDok = !Convert.IsDBNull(record["NRDOK"]) ? Convert.ToString(record["NRDOK"]) : string.Empty,
                DtDok = !Convert.IsDBNull(record["DTDOK"]) ? Convert.ToDateTime(record["DTDOK"]) : (DateTime?)null,
                Muaji = !Convert.IsDBNull(record["MUAJI"]) ? Convert.ToInt32(record["MUAJI"]) : -1,
                TotaliFemra = !Convert.IsDBNull(record["TOTALIFEMRA"]) ? Convert.ToInt32(record["TOTALIFEMRA"]) : 0,
                TotaliMeshkuj = !Convert.IsDBNull(record["TOTALIMESHKUJ"]) ? Convert.ToInt32(record["TOTALIMESHKUJ"]) : 0,
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
            NrDok = !Convert.IsDBNull(record["NRDOK"]) ? Convert.ToString(record["NRDOK"]) : string.Empty;
            DtDok = !Convert.IsDBNull(record["DTDOK"]) ? Convert.ToDateTime(record["DTDOK"]) : (DateTime?)null;
            Muaji = !Convert.IsDBNull(record["MUAJI"]) ? Convert.ToInt32(record["MUAJI"]) : -1;
            TotaliFemra = !Convert.IsDBNull(record["TOTALIFEMRA"]) ? Convert.ToInt32(record["TOTALIFEMRA"]) : 0;
            TotaliMeshkuj = !Convert.IsDBNull(record["TOTALIMESHKUJ"]) ? Convert.ToInt32(record["TOTALIMESHKUJ"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdNdermVit = !Convert.IsDBNull(record["IDNDERMVIT"]) ? Convert.ToInt32(record["IDNDERMVIT"]) : 0;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
            IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0;
        }
        public clsKokaPasqyraOrganike()
        {
            ColTrupi = new colTrupiPasqyraOrganike();
        }
        public clsKokaPasqyraOrganike(int idKoka)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
              
                dbAB.MerrPasqyraOrganikeSipasId(idKoka, this);
                ColTrupi = new colTrupiPasqyraOrganike(idKoka);
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

        public clsMesazh Ruaj(clsDatabaseAnalizeBuxheti dbAbB)
        {

            clsMesazh mesazhi = new clsMesazh();
            try
            {
                // dbAbB.beginTransaksion();

                int idKokaDok = -1;
                foreach(clsTrupiPasqyraOrganike trup in ColTrupi)
                {
                    TotaliFemra += trup.TotaliFemra;
                    TotaliMeshkuj += trup.TotaliMeshkuj;
                }
                mesazhi = dbAbB.RuajKokenPasqyraOrganike(out idKokaDok, NrDok, DtDok, Muaji, TotaliFemra, TotaliMeshkuj, IdKrijuesi, IdModifikuesi, IdNdermarrje, DtKrijimi, DtModifikimi,IdStatusDok, IdNdermVit);
                IdKokaDok = idKokaDok;
                if (!mesazhi.Status)
                {
                    dbAbB.rollbackTransaksion();
                    return mesazhi;

                }
                foreach (clsTrupiPasqyraOrganike trup in ColTrupi)
                {
                    trup.IdKokaDok = IdKokaDok;
                    trup.IdStatusDok = 1;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se pasqyrave organike! " + ex.Message);
                dbAbB.rollbackTransaksion();
            }
            return new clsMesazh();


        }
        public clsMesazh EkzistonNjeDokumentMeKeteNumer(clsDatabaseAnalizeBuxheti dbAB)
        {
            if (dbAB.EkzistonKyNumerDokumentiPasqyraOrganike(NrDok, IdNdermarrje, IdNdermVit))
                return new clsMesazh(false, "Ekziston nje dokument me kete numer!");
            else return new clsMesazh(true);

        }
        public clsMesazh EkzistonNjeDokumentMeKeteNumer()
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
                return EkzistonNjeDokumentMeKeteNumer(dbAB);

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
           
            clsMesazh mesazh = dbAb.FshiUpdateStatusDokPasqyraOrganike(IdKokaDok, IdModifikuesi);
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
                        return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te pasqyres se vjeter");
                    }
                    mesazh = Ruaj(dbAB);
                    if (!mesazh.Status)
                    {
                        dbAB.rollbackTransaksion();
                        return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te pasqyres organike!\n");
                    }
                    dbAB.commitTransaksion();
                    return mesazh;
                }
                catch (Exception ex)
                {
                    dbAB.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit te pasqyres organike!\n" + ex.Message);
                }
            }
        }

    }

    
}
