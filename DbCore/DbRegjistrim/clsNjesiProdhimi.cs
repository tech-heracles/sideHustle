using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  dege administrative
    ///  (Te dhenat  merren nga tabela : T_NJESIPRODHIMI)
    /// </summary>
    public class clsNjesiProdhimi
    {
        #region Atribute

        private int idNjesiProdhimi;
        private string kodi;
        private string pershkrimi;
        private DateTime dtRegjistrimi;
        private int idDegeAdministrative;
        private string adresa;
        private bool aktiv;
        private string shenime;
        private DateTime dtModifikimi;
        private int idPerdoruesi;
        private int idKrijuesi;
        private int idNdermarje;
        private int idStatusdok;
        private int idKonfig;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        public clsNjesiProdhimi()
        {
        }

        public clsNjesiProdhimi(int idNjesiProdhimi, string kodi, string pershkrimi, DateTime dtRegjistrimi, int idDegeAdministrative, string adresa, bool aktiv, string shenime, DateTime dtModifikimi, int idPerdoruesi, int idKrijuesi, int idNdermarje, int idStatusdok, int idKonfig)
        {
            this.idNjesiProdhimi = idNjesiProdhimi;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.dtRegjistrimi = dtRegjistrimi;
            this.idDegeAdministrative = idDegeAdministrative;
            this.adresa = adresa;
            this.aktiv = aktiv;
            this.shenime = shenime;
            this.dtModifikimi = dtModifikimi;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.idNdermarje = idNdermarje;
            this.idStatusdok = idStatusdok;
            this.idKonfig = idKonfig;
        }

        public clsNjesiProdhimi(int idNjesiProdhimi, string kodi, string pershkrimi, DateTime dtRegjistrimi, int idDegeAdministrative, string adresa, bool aktiv, string shenime, int idPerdoruesi, int idKrijuesi, int idNdermarje, int idStatusDok, string kodDegeAdmin, bool shtim, int idKonfig, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                this.idNjesiProdhimi = idNjesiProdhimi;
                this.kodi = kodi;
                this.pershkrimi = pershkrimi;
                this.dtRegjistrimi = dtRegjistrimi;
                this.idDegeAdministrative = idDegeAdministrative;
                this.adresa = adresa;
                this.aktiv = aktiv;
                this.shenime = shenime;
                this.idPerdoruesi = idPerdoruesi;
                this.idKrijuesi = idKrijuesi;
                this.idNdermarje = idNdermarje;
                this.idStatusdok = idStatusDok;
                this.idKonfig = idKonfig;
                clsMesazh mesazh = this.kontrolloNjesiProdhimi(shtim, kodDegeAdmin, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public clsNjesiProdhimi(DataRow rreshti)
        {
            
            mbushNjesiProdhimi(rreshti);
        }

        private clsMesazh kontrolloNjesiProdhimi(bool shtim, string kodDegeAdmin, ResourceManager rm, CultureInfo ci)
        {
            if (kodi == "")
                return new clsMesazh(false, "Plotesoni kodin!");
            clsMesazh kontrollkodi = clsFunksione.kontrolloKaraktereMeMesazh(kodi, FusheKontrolli.Kodi, false);
            if (!kontrollkodi.Status)
                return kontrollkodi;
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            if (shtim)
            {
                if (db.ekzistonNjesiProdhimiSipasKodit(kodi, idNdermarje))
                {
                    db.Dispose();
                    return new clsMesazh(false, "Ekziston nje njesi prodhimi me kete kod!");
                }
            }
            if (pershkrimi == "")
                return new clsMesazh(false, "Plotesoni pershkrimin!");
            clsMesazh kontrollpershkrimi = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimi, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimi.Status)
                return kontrollpershkrimi;

            if (kodDegeAdmin != "" && idDegeAdministrative <= 0)
                return new clsMesazh(false, "Dega administrative nuk ekziston!");
            return new clsMesazh(true, "Kontrollet e artikullit u kaluan me sukses");
        }

        #endregion

        #region Properties


        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNjesiProdhimi
        {
            get { return idNjesiProdhimi; }
            set { idNjesiProdhimi = value; }
        }

        public int IdDegeAdministrative
        {
            get { return idDegeAdministrative; }
            set { idDegeAdministrative = value; }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        public int IdStatusdok
        {
            get { return idStatusdok; }
            set { idStatusdok = value; }
        }

        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        public DateTime DtRegjistrimi
        {
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(clsDatabaseRegjistrim data)
        {
            int id;
            clsMesazh u_ruajt = data.ruajNjesiProdhimi(out id, this.kodi, this.pershkrimi, this.dtRegjistrimi, this.idDegeAdministrative, this.adresa, this.aktiv, this.shenime, this.idKrijuesi, this.idNdermarje, this.idStatusdok, this.idKonfig);
            this.IdNjesiProdhimi = id;
            return u_ruajt;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt = ruaj(data);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(clsDatabaseRegjistrim data)
        {
            clsMesazh u_modifikua = data.modifikoNjesiProdhimi(this.idNjesiProdhimi, this.kodi, this.pershkrimi, this.dtRegjistrimi, this.idDegeAdministrative, this.adresa, this.aktiv, this.shenime, this.idPerdoruesi, this.idNdermarje, this.idStatusdok);
            return u_modifikua;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = modifiko(data);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public static clsMesazh fshi(int idNjesiProdhimi, int idPerdoruesi)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiNjesiProdhimi(idNjesiProdhimi, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }
        

        public static bool ekzistonSipasKodit(string kodi, int indermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool ekziston = ekzistonSipasKodit(kodi, indermarje, db);
            db.Dispose();
            return ekziston;
        }

        public static bool ekzistonSipasKodit(string kodi, int indermarje, clsDatabaseRegjistrim db)
        {
            return db.ekzistonNjesiProdhimiSipasKodit(kodi, indermarje);
        }

        public static DataRow merrNjesiProdhimiSipasIdDR(int idNjesiProdhimi)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataRow dr = dbRegj.merrNjesiProdhimiSipasIdDR(idNjesiProdhimi);
            dbRegj.Dispose();
            return dr;
        }

        public static int ktheIdKrijuesNjesiProdhimiSipasId(int idNjesiProdhimi)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            int idKrijues = dbRegj.ktheIdKrijuesNjesiProdhimiSipasId(idNjesiProdhimi);
            dbRegj.Dispose();
            return idKrijues;
        }

        public static string ktheKodNjesiProdhimiSipasId(int idNjesiProdhimi)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            string kodi = dbRegj.ktheKodNjesiProdhimiSipasId(idNjesiProdhimi);
            dbRegj.Dispose();
            return kodi;
        }

        public static int ktheIdNjesiProdhimiSipasKodit(string kodNjesiProdhimi, int idNderm)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            int id = dbRegj.ktheIdNjesiProdhimiSipasKodit(kodNjesiProdhimi, idNderm);
            dbRegj.Dispose();
            return id;
        }

        public static bool eshteAktiveNjesiProdhimiSipasKodit(string kodNjesiProdhimi, int idNderm)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            bool aktiv = dbRegj.eshteAktiveNjesiProdhimiSipasKodit(kodNjesiProdhimi, idNderm);
            dbRegj.Dispose();
            return aktiv;
        }

        public bool mbushNjesiProdhimi(int idNjesiProdhimi)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            bool sukses = mbushNjesiProdhimi(dbRegj.merrNjesiProdhimiSipasIdDR(idNjesiProdhimi));
            dbRegj.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush njesine e prodhimit nga databaza
        /// </summary>
        /// <param name="dbDataRowNjesiProdhimi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNjesiProdhimi(DataRow dbDataRowNjesiProdhimi)
        {
            if (dbDataRowNjesiProdhimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowNjesiProdhimi["IDNJESIPRODHIMI"].ToString(), out idNjesiProdhimi);
                    kodi = dbDataRowNjesiProdhimi["KODI"].ToString();
                    pershkrimi = dbDataRowNjesiProdhimi["PERSHKRIMI"].ToString();
                    adresa = dbDataRowNjesiProdhimi["ADRESA"].ToString();
                    shenime = dbDataRowNjesiProdhimi["SHENIME"].ToString();
                    int.TryParse(dbDataRowNjesiProdhimi["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    bool.TryParse(dbDataRowNjesiProdhimi["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNjesiProdhimi["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowNjesiProdhimi["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowNjesiProdhimi["IDKRIJUESI"].ToString(), out idKrijuesi);
                    DateTime.TryParse(dbDataRowNjesiProdhimi["DTREGJISTRIMI"].ToString(), out dtRegjistrimi);                    
                    int.TryParse(dbDataRowNjesiProdhimi["IDSTATUSDOK"].ToString(), out idStatusdok);                    
                    DateTime.TryParse(dbDataRowNjesiProdhimi["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se njesise se prodhimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}