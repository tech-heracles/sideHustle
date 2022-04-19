using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbShare
{
    public class clsAtributeTrupi
    {
        #region Atribute

        private int idKontroll;
        private int idKonfigAmbjente;
        private string vlereDefault;
        private bool visible;
        private bool enabled;
        private int idKonfigAmbjenteLupa;
        private string kodKonfigLupa;
        private string pershkrimKontroll;
        private int identifikues;
        private int rreshti;
        private int kolona;
        private bool detyrueshme;
        private int idNrAutomatik;
        private string vlereDefaultEng;
        private string vlereDefault_fr;
        private bool shfaqMobile;
        private double renditjaMobile;
        private bool unike;

        #endregion

        #region Konstruktoret

        public clsAtributeTrupi(int idkontroll, int idkonfigambjente, string vleredefault, bool vis, bool enab, int lupa, string kodLupa, string pershkrimkontroll, int identif, int rresht, int kolon, bool detyruesh, int idnrautomatik, string vleredefaulteng, string vleredefault_fr, bool shfaqmobile, double renditjamobile, bool unike)
        {
            idKontroll = idkontroll;
            idKonfigAmbjente = idkonfigambjente;
            vlereDefault = vleredefault;
            visible = vis;
            enabled = enab;
            idKonfigAmbjenteLupa = lupa;
            kodKonfigLupa = kodLupa;
            pershkrimKontroll = pershkrimkontroll;
            identifikues = identif;
            rreshti = rresht;
            kolona = kolon;
            detyrueshme = detyruesh;
            idNrAutomatik = idnrautomatik;
            vlereDefaultEng = vleredefaulteng;
            vlereDefault_fr = vleredefault_fr;
            shfaqMobile = shfaqmobile;
            renditjaMobile = renditjamobile;
            this.unike = unike;
        }

        public clsAtributeTrupi()
        {
        }

        public clsAtributeTrupi(DataRow dbDataRowAtributTrupi)
        {
            mbushAtributTrupi(dbDataRowAtributTrupi);
        }

        #endregion

        #region Properties

        public int IdKontroll
        {
            get { return idKontroll; }
            set { idKontroll = value; }
        }

        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        public string VlereDefault
        {
            get { return vlereDefault; }
            set { vlereDefault = value; }
        }

        public string VlereDefaultEng
        {
            get { return vlereDefaultEng; }
            set { vlereDefaultEng = value; }
        }

        public string VlereDefault_fr
        {
            get { return vlereDefault_fr; }
            set { vlereDefault_fr = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public bool Detyrueshme
        {
            get { return detyrueshme; }
            set { detyrueshme = value; }
        }
        
        public int IdKonfigAmbjenteLupa
        {
            get { return idKonfigAmbjenteLupa; }
            set { idKonfigAmbjenteLupa = value; }
        }

        public string KodLupa
        {
            get { return kodKonfigLupa; }
            set { kodKonfigLupa = value; }
        }

        public string PershkrimKontroll
        {
            get { return pershkrimKontroll; }
            set { pershkrimKontroll = value; }
        }

        public int Identifikues
        {
            get { return identifikues; }
            set { identifikues = value; }
        }

        public int Rreshti
        {
            get { return rreshti; }
            set { rreshti = value; }
        }

        public int Kolona
        {
            get { return kolona; }
            set { kolona = value; }
        }

        public int IdNrAutomatik
        {
            get { return idNrAutomatik; }
            set { idNrAutomatik = value; }
        }

        public bool ShfaqMobile
        {
            get { return shfaqMobile; }
            set { shfaqMobile = value; }
        }

        public double RenditjaMobile
        {
            get { return renditjaMobile; }
            set { renditjaMobile = value; }
        }

        public bool Unike
        {
            get { return unike; }
            set { unike = value; }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            return new clsMesazh();
        }

        public clsMesazh modifiko()
        {
            return new clsMesazh();
        }

        public clsMesazh fshi()
        {
            return new clsMesazh();
        }

        public void KrijoArtibut(Dictionary<string, object> rresht, int idndermarje)
        {
            IdKontroll = int.Parse(rresht["IdKontroll"].ToString());
            IdKonfigAmbjente = int.Parse(rresht["IdKonfigAmbjente"].ToString());
            VlereDefault = rresht["VlereDefault"] != null ? rresht["VlereDefault"].ToString() : "";
            VlereDefaultEng = rresht["VlereDefaultEng"] != null ? rresht["VlereDefaultEng"].ToString() : "";
            VlereDefault_fr = rresht["VlereDefault_fr"] != null ? rresht["VlereDefault_fr"].ToString() : "";
            if (rresht["Visible"].ToString() == "Checked" || rresht["Visible"].ToString() == "True")
                Visible = true;
            else Visible = false;

            if (rresht["Enabled"].ToString() == "Checked" || rresht["Enabled"].ToString() == "True")
                Enabled = true;
            else Enabled = false;
            clsKonfigurimAmbjenti oKonfig = new clsKonfigurimAmbjenti();
            KodLupa = rresht["KodLupa"].ToString();
            if (!String.IsNullOrEmpty(KodLupa))
                oKonfig.mbushKonfigAmbjSipasKod(KodLupa, idndermarje);
            IdKonfigAmbjenteLupa = oKonfig.IdKonfigAmbjente;
            PershkrimKontroll = rresht["PershkrimKontroll"].ToString();
            if (rresht["Identifikues"] != null)
                Identifikues = int.Parse(rresht["Identifikues"].ToString());
            else Identifikues = 3;
            Rreshti = int.Parse(rresht["Rreshti"].ToString());
            Kolona = int.Parse(rresht["Kolona"].ToString());
            if (rresht["Detyrueshme"].ToString() == "Checked" || rresht["Detyrueshme"].ToString() == "True")
                Detyrueshme = true;
            else Detyrueshme = false;
            if (rresht["IdNrAutomatik"] != null)
                int.TryParse(rresht["IdNrAutomatik"].ToString(), out idNrAutomatik);
            if (rresht["ShfaqMobile"].ToString() == "Checked" || rresht["ShfaqMobile"].ToString() == "True")
                ShfaqMobile = true;
            else ShfaqMobile = false;
            RenditjaMobile = double.Parse(rresht["RenditjaMobile"].ToString());
            if (rresht["Unike"].ToString() == "Checked" || rresht["Unike"].ToString() == "True")
                unike = true;
            else unike = false;
        }

        public bool mbushAtributSipasKompKonfDheKontrollit(int idGjuha, int idKonfigurim, string kodKontroll, int idKompon)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return mbushAtributTrupi(data.merrAtributetKontrolleveSipasKonfigurimitDheKontrollit(idGjuha, idKonfigurim, kodKontroll, idKompon));
            }
        }

        public static int merrNrAutomatikSipasKontrollitDheKonfigurimit(int idKonfigurim, string kodKontroll, int idKompon)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrNrAutomatikSipasKontrollitDheKonfigurimit(idKonfigurim, idKompon, kodKontroll);
            }
        }

        public static int merrNrAutomatikSipasKontrollitDheKonfigurimit(int idKonfigurim, string kodKontroll, int idKompon, clsDatabaseShare data)
        {
            return data.merrNrAutomatikSipasKontrollitDheKonfigurimit(idKonfigurim, idKompon, kodKontroll);
        }

        public static int merrIdentifikuesSipasKontrollitDheKonfigurimit(int idKonfigurim, string kodKontroll, int idKompon)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrIdentifikuesSipasKontrollitDheKonfigurimit(idKonfigurim, idKompon, kodKontroll);
            }
        }

        public static string merrVleredefaultSipasKontrollitDheKonfigurimit(int idKonfigurim, string kodKontroll, int idKompon)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, idKompon, kodKontroll);
            }
        }

        public static string merrVleredefaultSipasKontrollitKodKonfigDheNderm(string kodKonfigambjente, int idNdermarrje, string kodkontroll)
        {
            using (clsDatabaseShare data = new clsDatabaseShare())
            {
                return data.merrVleredefaultSipasKontrollitKodKonfigDheNderm(kodKonfigambjente, idNdermarrje, kodkontroll);
            }
        }

        public static T merrVleredefaultSipasKontrollitKodKonfigDheNderm<T>(string kodKonfigambjente, int idNdermarrje, string kodkontroll)
        {
            string value = merrVleredefaultSipasKontrollitKodKonfigDheNderm(kodKonfigambjente, idNdermarrje, kodkontroll);

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception x)
            {
                return default(T);
            }
        }

        public static string merrVleredefaultSipasKontrollitDheKonfigurimit(int idKonfigurim, string kodKontroll, int idKompon, clsDatabaseShare data)
        {

            return data.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, idKompon, kodKontroll);
        }

        public static Dictionary<string, string> merrVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL(int idKonfigAmbjente)
        {
            using (var db = new clsDatabaseShare())
                return db.ktheVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL(idKonfigAmbjente);
        }

        public static bool IsRequiredField(int idKonfigurimi, string kodKontrolli, int idKomponente)
        {
            using (var databaseShare = new clsDatabaseShare())
                return databaseShare.IsRequiredField(idKonfigurimi, kodKontrolli, idKomponente);
        }

        public void VlereDefaultSipasGjuhes(int idgjuha)
        {
            if (idgjuha == 0)
            {
                VlereDefaultEng = VlereDefault;
                VlereDefault_fr = VlereDefault;
            }
            else if (idgjuha == 1)
            {
                VlereDefault = VlereDefaultEng;
                VlereDefault_fr = VlereDefaultEng;
            }
            else
            {
                VlereDefault = VlereDefault_fr;
                VlereDefaultEng = VlereDefault_fr;
            }
        }
        #endregion

        #region Metoda Internal

        internal bool mbushAtributTrupi(DataRow dbDataRowAtributTrupi)
        {
            if (dbDataRowAtributTrupi != null)
            {
                try
                {
                    clsKonfigurimAmbjenti konfigLupa = new clsKonfigurimAmbjenti();
                    int.TryParse(dbDataRowAtributTrupi["IDKONTROLL"].ToString(), out idKontroll);
                    int.TryParse(dbDataRowAtributTrupi["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    vlereDefault = dbDataRowAtributTrupi["VLEREDEFAULT"].ToString();
                    bool.TryParse(dbDataRowAtributTrupi["VISIBLE"].ToString(), out visible);
                    bool.TryParse(dbDataRowAtributTrupi["ENABLED1"].ToString(), out enabled);
                    int.TryParse(dbDataRowAtributTrupi["IDKONFIGLUPA"].ToString(), out idKonfigAmbjenteLupa);
                    kodKonfigLupa = Convert.ToString(dbDataRowAtributTrupi["KODKONFIGLUPA"]);
                    int.TryParse(dbDataRowAtributTrupi["IDENTIFIKUES"].ToString(), out identifikues);
                    int.TryParse(dbDataRowAtributTrupi["RRESHTI"].ToString(), out rreshti);
                    int.TryParse(dbDataRowAtributTrupi["KOLONA"].ToString(), out kolona);
                    int.TryParse(dbDataRowAtributTrupi["IDNRAUTOMATIK"].ToString(), out idNrAutomatik);
                    bool.TryParse(dbDataRowAtributTrupi["DETYRUESHME"].ToString(), out detyrueshme);
                    pershkrimKontroll = dbDataRowAtributTrupi["PERSHKRIMKONTROLL"].ToString();
                    bool.TryParse(dbDataRowAtributTrupi["SHFAQMOBILE"].ToString(), out shfaqMobile);
                    double.TryParse(dbDataRowAtributTrupi["RENDITJAMOBILE"].ToString(), out renditjaMobile);
                    bool.TryParse(dbDataRowAtributTrupi["UNIKE"].ToString(), out unike);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se atributit te trupit nga db-ja");
                }
            }
            else
                return false;
        }

        internal bool mbushAtributTrupiEng(DataRow dbDataRowAtributTrupi)
        {
            if (dbDataRowAtributTrupi != null)
            {
                try
                {
                    clsKonfigurimAmbjenti konfigLupa = new clsKonfigurimAmbjenti();
                    int.TryParse(dbDataRowAtributTrupi["IDKONTROLL"].ToString(), out idKontroll);
                    int.TryParse(dbDataRowAtributTrupi["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    vlereDefault = dbDataRowAtributTrupi["VLEREDEFAULT_sq"].ToString();
                    bool.TryParse(dbDataRowAtributTrupi["VISIBLE"].ToString(), out visible);
                    bool.TryParse(dbDataRowAtributTrupi["ENABLED1"].ToString(), out enabled);
                    int.TryParse(dbDataRowAtributTrupi["IDKONFIGLUPA"].ToString(), out idKonfigAmbjenteLupa);
                    kodKonfigLupa = Convert.ToString(dbDataRowAtributTrupi["KODKONFIGLUPA"]);
                    int.TryParse(dbDataRowAtributTrupi["IDENTIFIKUES"].ToString(), out identifikues);
                    int.TryParse(dbDataRowAtributTrupi["RRESHTI"].ToString(), out rreshti);
                    int.TryParse(dbDataRowAtributTrupi["KOLONA"].ToString(), out kolona);
                    int.TryParse(dbDataRowAtributTrupi["IDNRAUTOMATIK"].ToString(), out idNrAutomatik);
                    bool.TryParse(dbDataRowAtributTrupi["DETYRUESHME"].ToString(), out detyrueshme);
                    pershkrimKontroll = dbDataRowAtributTrupi["PERSHKRIMKONTROLL"].ToString();
                    vlereDefaultEng = dbDataRowAtributTrupi["VLEREDEFAULT_en"].ToString();
                    vlereDefault_fr = dbDataRowAtributTrupi["VLEREDEFAULT_fr"].ToString();
                    bool.TryParse(dbDataRowAtributTrupi["SHFAQMOBILE"].ToString(), out shfaqMobile);
                    double.TryParse(dbDataRowAtributTrupi["RENDITJAMOBILE"].ToString(), out renditjaMobile);
                    bool.TryParse(dbDataRowAtributTrupi["UNIKE"].ToString(), out unike);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se atributit te trupit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}