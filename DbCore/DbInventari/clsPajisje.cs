using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbInventari
{
    public class clsPajisje
    {
        #region Atributet

        private int idPajisje;
        private string kodi;
        private string emertimi;
        private string fjalekalimi;
        private int idKlienti;
        private bool regjistruar;
        private bool aktive;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idStatusdok;
        private int idPerdorues;
        private double koeficentKonsumi;
        private double koeficentFitimi;
        private int idNdermarrje;
        private int idLlojKonvertimi;

        #endregion

        #region Properties

        public int IdPajisje
        {
            get
            {
                return idPajisje;
            }
            set
            {
                idPajisje = value;
            }
        }

        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }

        public string Emertimi
        {
            get
            {
                return emertimi;
            }
            set
            {
                emertimi = value;
            }
        }

        public string Fjalekalimi
        {
            get
            {
                return fjalekalimi;
            }
            set
            {
                fjalekalimi = value;
            }
        }

        public int IdKlienti
        {
            get
            {
                return idKlienti;
            }
            set
            {
                idKlienti = value;
            }
        }

        public int IdStatusdok
        {
            get
            {
                return idStatusdok;
            }
            set
            {
                idStatusdok = value;
            }
        }

        public int IdPerdorues
        {
            get
            {
                return idPerdorues;
            }
            set
            {
                idPerdorues = value;
            }
        }

        public bool Regjistruar
        {
            get
            {
                return regjistruar;
            }
            set
            {
                regjistruar = value;
            }
        }

        public bool Aktive
        {
            get
            {
                return aktive;
            }
            set
            {
                aktive = value;
            }
        }

        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
            set
            {
                dtKrijimi = value;
            }
        }

        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
            set
            {
                dtModifikimi = value;
            }
        }

        public double KoeficentKonsumi
        {
            get
            {
                return koeficentKonsumi;
            }
            set
            {
                koeficentKonsumi = value;
            }
        }

        public double KoeficentFitimi
        {
            get
            {
                return koeficentFitimi;
            }
            set
            {
                koeficentFitimi = value;
            }
        }

        public int IdNdermarrje
        {
            get
            {
                return idNdermarrje;
            }
            set
            {
                idNdermarrje = value;
            }
        }

        public int IdLlojKonvertimi
        {
            get
            {
                return idLlojKonvertimi;
            }
            set
            {
                idLlojKonvertimi = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsPajisje()
        {

        }

        public clsPajisje(int idPajisje)
        {
            mbushPajisje(kthePajisjeSipasId(idPajisje));
        }

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        public clsPajisje(string kodi, string emertimi, string fjalekalimi, int idKlienti, bool regjistruar, bool aktive, int idStatusdok, int idPerdorues, double koeficentFitimi, double koeficentKonsumi, int idNdermarrje)
        {
            this.kodi = kodi;
            this.emertimi = emertimi;
            this.fjalekalimi = fjalekalimi;
            this.idKlienti = idKlienti;
            this.regjistruar = regjistruar;
            this.aktive = aktive;           
            this.idStatusdok = IdStatusdok;
            this.idPerdorues = idPerdorues;
            this.koeficentFitimi = koeficentFitimi;
            this.koeficentKonsumi = koeficentKonsumi;
            this.idNdermarrje = idNdermarrje;
        }


        public clsPajisje(DataRow rreshti)
        {
            mbushPajisje(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsMesazh kontrollo(bool kontrolloEkzistenceKlientiPerPajisje, clsDatabaseInventari dbInv)
        {
            if (kontrolloEkzistenceKlientiPerPajisje && ekzistonPajisjeAktiveDheRegjistruarSipasKoditDheKlientit(kodi, idKlienti, idNdermarrje, dbInv))
                return new clsMesazh(false, "Ekziston nje pajisje e regjistruar me kodin " + this.kodi + " per kete klient!");

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        public static bool ekzistonPajisjeAktiveDheRegjistruarSipasKoditDheKlientit(string kodi, int idKlienti, int idNdermarrje, clsDatabaseInventari dbInv)
        {
            return dbInv.ekzistonPajisjeAktiveDheRegjistruarSipasKoditDheKlientit(kodi, idKlienti, idNdermarrje);
        }

        public static bool ekzistonPajisjeAktiveDheRegjistruarSipasKoditDheKlientit(string kodi, int idKlienti, int idNdermarrje)
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return ekzistonPajisjeAktiveDheRegjistruarSipasKoditDheKlientit(kodi, idKlienti, idNdermarrje);
            }
        }

        /// <summary>
        /// kthe nje datarow me automjetin e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static DataRow kthePajisjeSipasId(int idPajisje)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                DataRow dr = dbInventari.merrPajisjeSipasId(idPajisje);
                return dr;
            }
        }

        public clsMesazh modifikoPajisjeDheDergoEmail(bool ndryshuarFjalekalimi, string fjalekalimJoEnkriptuar, bool ndryshuarKlienti)//, string emailKlienti)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            try
            {
                dbInventar.beginTransaksion();
                clsMesazh mesazh = kontrollo(ndryshuarKlienti, dbInventar);
                if (!mesazh.Status)
                {
                    dbInventar.rollbackTransaksion();
                    return mesazh;
                }

                mesazh = modifiko(dbInventar);
                if (!mesazh.Status)
                {
                    dbInventar.rollbackTransaksion();
                    return mesazh;
                }
                if (ndryshuarFjalekalimi)
                {
                    string emailKlienti = DbCore.DbKontabiliteti.clsKlientFurnitor.KtheEmailPerPajisje(this.idKlienti, new DbKontabiliteti.clsDatabaseKontabilitet(dbInventar));
                    if (String.IsNullOrEmpty(emailKlienti))
                    {
                        dbInventar.rollbackTransaksion();
                        return new clsMesazh(false, "Percaktoni adresen email per pajisje te kartela e klientit!");
                    }

                    mesazh = dergoEmail(dbInventar, fjalekalimJoEnkriptuar, emailKlienti);
                    if (!mesazh.Status)
                    {
                        dbInventar.rollbackTransaksion();
                        return mesazh;
                    }
                }

                dbInventar.commitTransaksion();
                return mesazh;
            }
            catch (Exception e)
            {
                dbInventar.rollbackTransaksion();
                ImbLogger.Error(e.Message);
                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te emailit!");
            }
        }

        public clsMesazh dergoEmail(clsDatabaseInventari dbInventar, string fjalekalimJoEnkriptuar, string emailPerdoruesi)
        {
            try
            {
                DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin(dbInventar);
                clsMesazh mesazh = EmailComposer.DergoEmailFjalekaliminERi(emailPerdoruesi, this.kodi, fjalekalimJoEnkriptuar, this.idPerdorues, dbAdmin);
                return mesazh;
            }
            catch (Exception e)
            {
                ImbLogger.Error(e.Message);
                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te emailit!");
            }
        }

        public clsMesazh modifiko(clsDatabaseInventari dbInventar)
        {
            try
            {
                clsMesazh mesazh = dbInventar.modifikoPajisje(this.idPajisje, this.fjalekalimi, this.idKlienti, this.regjistruar, this.aktive, this.idPerdorues, this.koeficentFitimi, this.koeficentKonsumi);

                return mesazh;
            }
            catch (Exception e)
            {
                ImbLogger.Error(e.Message);
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        #endregion

        #region Metoda Internal

        internal clsMesazh mbushPajisje(DataRow dbRowPajisje)
        {
            if (dbRowPajisje != null)
            {
                try
                {
                    int.TryParse(dbRowPajisje["IDPAJISJE"].ToString(), out idPajisje);
                    kodi = dbRowPajisje["UUID"].ToString();
                    emertimi = dbRowPajisje["KODI"].ToString();
                    fjalekalimi = dbRowPajisje["FJALEKALIMI"].ToString();
                    int.TryParse(dbRowPajisje["IDKLIENTI"].ToString(), out idKlienti);
                    bool.TryParse(dbRowPajisje["REGJISTRUAR"].ToString(), out regjistruar);
                    bool.TryParse(dbRowPajisje["AKTIVE"].ToString(), out aktive);
                    DateTime.TryParse(dbRowPajisje["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbRowPajisje["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbRowPajisje["IDSTATUSDOK"].ToString(), out idStatusdok);
                    int.TryParse(dbRowPajisje["IDPERDORUES"].ToString(), out idPerdorues);
                    double.TryParse(dbRowPajisje["KOEFICENTKONSUMI"].ToString(), out koeficentKonsumi);
                    double.TryParse(dbRowPajisje["KOEFICENTFITIMI"].ToString(), out koeficentFitimi);
                    int.TryParse(dbRowPajisje["IDNDERMARJE"].ToString(), out idNdermarrje);
                    return new clsMesazh(true, "Mbushja e pajisjes u krye me sukses");
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se pajisjes nga db-ja");
                }
            }
            else
                return new clsMesazh(false, "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja");
        }

        #endregion

    }
}