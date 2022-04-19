using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne cdo faqe aspx te projektit.
    ///  (Te dhenat  merren nga tabela : T_KOMPONENTE)
    /// </summary>
    public class clsKomponente
    {
        #region Atribute

        private int idKomponente;
        private int idModuli;
        private string emriKomponente;
        private string pershkrimiKomponente_sq;
        private string pershkrimiKomponente_en;
        private string pershkrimiKomponente_fr;
        private string urlHelpSuffix;
        private int tipi;
        private string imageUrl;
        private int idKompRegjistrimi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKomponente(int idkomponente, int idmoduli, string emrikomponente, string pershkrimikomponente_sq, string pershkrimikomponente_en, string pershkrimikomponente_fr, int tipi, string imageUrl)
        {
            idKomponente = idkomponente;
            idModuli = idmoduli;
            emriKomponente = emrikomponente;
            pershkrimiKomponente_sq = pershkrimikomponente_sq;
            pershkrimiKomponente_en = pershkrimikomponente_en;
            pershkrimiKomponente_fr = pershkrimikomponente_fr;
            this.tipi = tipi;
            this.imageUrl = imageUrl;
        }
        /// <summary>
        /// Merr komponente sipas kodit
        /// </summary>
        /// <param name="emerKomponente"></param>
        public clsKomponente(string emerKomponente)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushKomponente(data.merrKomponenteSipasEmrit(emerKomponente));
            }
        } public clsKomponente(int idkomp)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushKomponente(data.merrKomponenteSipasId(idkomp));
            data.Dispose();
        }

        ///// <summary>
        ///// Konstruktori i klases
        ///// </summary>
        //public clsKomponente(int idmoduli, String emrikomponente)
        //{
        //    idModuli = idmoduli;
        //    emriKomponente = emrikomponente;
        //}

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsKomponente()
        { 
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe identifikon komponenten ne databaze.
        /// </summary>
        //metodat per marrjen e te dhenave
        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modulit te cilit i perkete kjo komponente. Cdo komponente i perket vetem nje
        /// modulif, ku modulet jane Administrim, Kontabilitet,Konfigurime etj..
        /// </summary>
        public int IdModuli
        {
            get { return idModuli; }
            set { idModuli = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e komponentes qe esht emri i file-t fizik  .aspx.
        /// </summary>
        public string EmriKomponente
        {
            get { return emriKomponente; }
            set { emriKomponente = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin llogjik  te komponentes e cila perdoret si emer i faqes ne momentin
        /// qe i shfaqet perdoruesit.
        /// </summary>
        public string PershkrimiKomponente_sq
        {
            get { return pershkrimiKomponente_sq; }
            set { pershkrimiKomponente_sq = value; }
        }
        public string PershkrimKomponente_en
        {
            get { return pershkrimiKomponente_en; }
            set { pershkrimiKomponente_en = value; }
        }
        public string PershkrimKomponente_fr
        {
            get { return pershkrimiKomponente_fr; }
            set { pershkrimiKomponente_fr = value; }
        }
        public String UrlHelpSuffix
        {
            get
            {
                return urlHelpSuffix;
            }
        }
        public int Tipi
        {
            get
            {
                return tipi;
            }
            set
            {
                tipi = value;
            }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public int IdKompRegjistrimi
        {
            get { return idKompRegjistrimi; }
            set { idKompRegjistrimi = value; }
        }

        #endregion

        #region Metoda Internal 

        internal bool mbushKomponente(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOMPON"].ToString(), out idKomponente);
                    int.TryParse(rreshti["IDMODULI"].ToString(), out idModuli);
                    emriKomponente = rreshti["KOMPONEMRI"].ToString();
                    pershkrimiKomponente_sq = rreshti["KOMPPERSHKRIMI_sq"].ToString();
                    pershkrimiKomponente_en = rreshti["KOMPPERSHKRIMI_en"].ToString();
                    pershkrimiKomponente_fr = rreshti["KOMPPERSHKRIMI_fr"].ToString();
                    string katHelpi = rreshti["KATEGORIHELPI"].ToString();
                    string detHelpi = rreshti["DETAJIMHELPI"].ToString();
                     int.TryParse(rreshti["TIPI"].ToString(), out tipi);
                    if (detHelpi != "")
                        this.urlHelpSuffix = formoUrlSuffix(katHelpi, detHelpi);
                    else
                        this.urlHelpSuffix = "";
                    imageUrl = Convert.ToString(rreshti["IMAGEURL"]);
                    int.TryParse(rreshti["IDKOMPREGJISTRIMI"].ToString(), out idKompRegjistrimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se komponentes nga db-ja");
                }
            }
            else
                return false;
        }
        private static String formoUrlSuffix(string kategoriHelpi, string detajimHelpi) {
            kategoriHelpi = kategoriHelpi.Replace(" ", "");
            kategoriHelpi = kategoriHelpi.Replace("?", "");
            detajimHelpi = detajimHelpi.Replace(" ", "_");
            detajimHelpi = detajimHelpi.Replace("/", "_");
            string urlHelpSuffix = kategoriHelpi.Length > 0 ? kategoriHelpi + "/" + detajimHelpi : detajimHelpi;
            return "#" + urlHelpSuffix + ".htm";
        }
        #endregion
        
        #region Metoda Publike

        public bool kaTeDrejtaAmbjenti(colTeDrejtaRoli teDrejtaRoliPerdoruesi, int idndermarje, int idViti)
        {                          
                clsTeDrejtaRoli teDrejtaRoli = teDrejtaRoliPerdoruesi.filtroTeDrejtaRoli(this.idModuli, this.idKomponente);
                if (teDrejtaRoli != null)
                    if (teDrejtaRoli.DAmb)
                        return true;
            
            return false;
        }

        //public bool kaTeDrejtaAmbjenti(int idperdorues, int idndermarje, int idnderviti)
        //{
        //    colRolPerdorues colRolPer = new colRolPerdorues();
        //    colRolPer.mbushRolePerdoruesSipasPerdoruesi(idperdorues);
        //    int idviti = new clsNdermarrjeViti(idnderviti).IdViti;

        //    foreach (clsRolPerdorues rp in colRolPer)
        //    {
        //        clsRoli roli = new clsRoli(rp.IdRoli);
        //        colTeDrejtaRoli colTedrejta = new colTeDrejtaRoli(roli.IdRoli, idndermarje, idviti);
        //        if (colTedrejta.filtroTeDrejtaRoli(this.idModuli, this.idKomponente) != null)
        //            if (colTedrejta.filtroTeDrejtaRoli(this.idModuli, this.idKomponente).DAmb)
        //                return true;
        //    }
        //    return false;
        //}

        [Obsolete("Duhet te perdoret merrKomponenteDefaultPerdoruesiSipasLlojit", false)]
        public static string merrKomponenteDefaultPerdoruesi(int idperdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKomponenteDefaultPerdoruesi(idperdoruesi);
            }
        }

        public static string merrKomponenteDefaultPerdoruesiSipasLlojit(int idperdoruesi, bool ambjentMobile)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKomponenteDefaultPerdoruesiSipasLlojit(idperdoruesi, ambjentMobile);
            }
        }

        public static int MerrIdKomponenteSipasEmrit(string komponenteEmri)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                
               return dbAdmin.MerrIdKomponenteSipasEmrit(komponenteEmri);
            }
        }
        #endregion
    }
}
