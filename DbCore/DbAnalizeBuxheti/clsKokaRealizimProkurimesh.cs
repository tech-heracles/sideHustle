using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsKokaRealizimProkurimesh
    {
        #region atribute
        private int idKokaRp;
        private string nrDok;
        private int katerMujori;
        private int idKrijuesi;
        private int idModifikuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idNdermarrje;
        private int idNdermVit;
        private int idStatusDok;
        private bool parashikim;
        
        #endregion atribute

        #region properties
        public int IdKokaRp
        {
            get { return idKokaRp; }
            set { idKokaRp = value; }
        }
        public string NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }
        public int KaterMujori
        {
            get { return katerMujori; }
            set { katerMujori = value; }
        }
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        public int IdNdermVit
        {
            get { return idNdermVit; }
            set { idNdermVit = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }
        public bool Parashikim
        {
            get { return parashikim; }
            set { parashikim = value; }
        }

        public colTrupiRealizimProkurimesh ColTrupi { get; set; }

        #endregion properties

        #region konstruktoret
        public clsKokaRealizimProkurimesh()
        {
            ColTrupi = new colTrupiRealizimProkurimesh();
        }
        public clsKokaRealizimProkurimesh(int idKokaRp)
        {

            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                dbAB.MerrRealizimProkurimiSipasId(idKokaRp, this);
                ColTrupi = new colTrupiRealizimProkurimesh(idKokaRp);
            }
        }

        
        #endregion konstruktoret

        #region metoda publike
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

                if (dbAB.EkzistonNjeDokumentProkurimiPerKetePeriudhe(IdNdermarrje, KaterMujori, IdNdermVit))
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

                mesazhi = dbAbB.RuajKokenRealizimProkurimi(out idKokaDok, NrDok, KaterMujori, Parashikim, IdKrijuesi, IdNdermarrje, IdNdermVit);
                IdKokaRp = idKokaDok;
                if (!mesazhi.Status)
                {
                    dbAbB.rollbackTransaksion();
                    return mesazhi;

                }
                foreach (clsTrupiRealizimProkurimesh trup in ColTrupi)
                {
                    trup.IdStatusDok = 1;
                    trup.IdKokaRp = IdKokaRp;
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
                mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes! " + ex.Message);
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
            clsMesazh mesazh = dbAb.FshiUpdateStatusDokProkurimi(IdKokaRp, IdModifikuesi);
            return mesazh;

        }
        public clsMesazh Fshi(int idKokaRp)
        {

            IdKokaRp = idKokaRp;
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
                        throw new Exception();

                    mesazh = Ruaj(dbAB);

                    if (!mesazh.Status)
                        throw new Exception();

                    dbAB.commitTransaksion();

                    return mesazh;
                }
                catch (Exception ex)
                {
                    dbAB.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate modifikimit!\n" + ex.Message);
                }
            }
        }

        public clsMesazh EkzistonNjeDokumentMeKeteNrDok(bool eshteShtim, clsDatabaseAnalizeBuxheti dbAB)
        {
            if (dbAB.EkzistonKyDokumentProkurimi(eshteShtim ? -1 :IdKokaRp, NrDok, IdNdermarrje, IdNdermVit))
                return new clsMesazh(false, "Ekziston nje dokument me kete numer!");
            else return new clsMesazh(true);

        }
        public clsMesazh EkzistonNjeDokumentMeKeteNrDok(bool eshteShtim)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
                return EkzistonNjeDokumentMeKeteNrDok(eshteShtim, dbAB);

        }

        public static int merrIdParashikimi(int idNdermarrje, int idNdermVit)
        {
            using (clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti())
            {
                return dbAB.MerrIdProkurimiParashikim(idNdermarrje, idNdermVit);

            }
        }

        #endregion metoda publike
        #region metoda internal
        internal void Mbush(IDataRecord record)
        {

            try
            {
                int.TryParse(record["IDKOKARP"].ToString(), out idKokaRp);
                nrDok = record["NRDOK"].ToString();
                int.TryParse(record["IDKRIJUESI"].ToString(), out idKrijuesi);
                int.TryParse(record["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(record["IDNDERMVIT"].ToString(), out idNdermVit);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(record["KATERMUJORI"].ToString(), out katerMujori);
                DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtKrijimi);
                bool.TryParse(record["PARASHIKIM"].ToString(), out parashikim);
                
            }
            catch (InvalidCastException ex)
            {
                ImbLogger.Info($"Error ne marrjen e te dhenave nga databaza: {ex.Message}");
            }

        }

        internal static clsKokaRealizimProkurimesh Krijo(IDataRecord record)
        {
            clsKokaRealizimProkurimesh kRP = new clsKokaRealizimProkurimesh();
            kRP.Mbush(record);
            return kRP;

        }
        #endregion metoda internal
    }
}
