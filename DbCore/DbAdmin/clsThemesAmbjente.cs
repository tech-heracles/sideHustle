using CacheLayer;
using System;
using System.Data;
using System.Web.SessionState;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje template te temave te frameve, gridave dhe backgroundit te programit.
    ///  (Te dhenat  merren nga tabela : T_THEMESAMBJENTE)
    /// </summary>
    public class clsThemesAmbjente : IDataBase
    {
        #region Atributet
        //public const string mySessionKey = "clsThemesAmbjente";
        private int idThemeAmbjente;
        private string kodTheme;
        private string pershkrimTheme;
        private bool defaultTheme; //true nese eshte default
        private bool zgjedhur; //true nese eshte i zgjedhur, pra nese eshte theme qe perdoret nga perdoruesi
        private int idThemeFrames;
        private int idThemeFrameKryesor;
        private int idThemeJQuery;
        private int idBgImage;
        private int idPerdorues;
        //private int idNdermarrje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string pathDevExpress;
        private string pathJquery;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Kontruktori pa parametra i klases
        /// </summary>
        public clsThemesAmbjente()
        {
        }

        /// <summary>
        /// Kontruktori me te gjitha parametrat i klases
        /// </summary>
        public clsThemesAmbjente(int idtheme, string kodtheme, string pershkrimtheme, bool defaulti, bool zgjedhur, int idframes, int idframekryesor, int idThemeJQuery, int idbgimg, int idperd, int idstatusdok, DateTime dtkrijimi, DateTime dtmodifikimi)
        {
            idThemeAmbjente = idtheme;
            kodTheme = kodtheme;
            pershkrimTheme = pershkrimtheme;
            defaultTheme = defaulti;
            this.zgjedhur = zgjedhur;
            idThemeFrames = idframes;
            idThemeFrameKryesor = idframekryesor;
            this.idThemeJQuery = idThemeJQuery;
            idBgImage = idbgimg;
            idPerdorues = idperd;
            //this.idNdermarrje = idnderm;
            idStatusDok = idstatusdok;
            dtKrijimi = dtkrijimi;
            dtModifikimi = dtmodifikimi;
        }

        /// <summary>
        /// Kontruktori i klases sipas id 
        /// </summary>
        public clsThemesAmbjente(int idTheme)
        {

            GlobalCacheManager.MySessionCache.FillObjectFromCache( x => x.IdThemeAmbjente == idTheme,
                () =>
                {
                    using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                    {
                        data.merrThemeAmbjenteSipasId(idTheme, this);
                    }
                    return this;
                }, this);

        }



        /// <summary>
        /// Kontruktori i klases sipas kodit, perdoruesit dhe ndermarrjes
        /// </summary>
        public clsThemesAmbjente(string kodTheme, int idPerd)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                data.merrThemeAmbjenteSipasKodit(kodTheme, idPerd, this);
            }

        }

        public clsThemesAmbjente(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos id-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdThemeAmbjente
        {
            get
            {
                return idThemeAmbjente;
            }
            set
            {
                idThemeAmbjente = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin
        /// </summary>
        public string KodTheme
        {
            get
            {
                return kodTheme;
            }
            set
            {
                kodTheme = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin
        /// </summary>
        public string PershkrimTheme
        {
            get
            {
                return pershkrimTheme;
            }
            set
            {
                pershkrimTheme = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nese tema eshte default apo jo
        /// </summary>
        public bool DefaultTheme
        {
            get
            {
                return defaultTheme;
            }
            set
            {
                defaultTheme = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nese tema eshte e zgjedhur apo jo
        /// </summary>
        public bool Zgjedhur
        {
            get
            {
                return zgjedhur;
            }
            set
            {
                zgjedhur = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e temes se zgjedhur per framet lart, majtas dhe posht
        /// </summary>
        public int IdThemeFrames
        {
            get
            {
                return idThemeFrames;
            }
            set
            {
                idThemeFrames = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e temes se zgjedhur per gridat dhe komponentet tek frame kryesor
        /// </summary>
        public int IdThemeFrameKryesor
        {
            get
            {
                return idThemeFrameKryesor;
            }
            set
            {
                idThemeFrameKryesor = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e temes se zgjedhur per gridat jquery
        /// </summary>
        public int IdThemeJQuery
        {
            get
            {
                return idThemeJQuery;
            }
            set
            {
                idThemeJQuery = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e imazhit te zgjedhur si background
        /// </summary>
        public int IdBgImage
        {
            get
            {
                return idBgImage;
            }
            set
            {
                idBgImage = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e perdoruesit
        /// </summary>
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

        /// <summary>
        /// Kthen/Vendos statusin e temes: nese eshte e fshire, ose jo
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen daten e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// Kthen daten e modifikimit te fundit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public string PathDevExpress
        {
            get { return pathDevExpress; }
            set { pathDevExpress = value; }
        }

        public string PathJquery
        {
            get { return pathJquery; }
            set { pathJquery = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e theme ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajThemeAmbjente"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Ruaj()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                int id;
                clsMesazh u_ruajt = dbAdmin.ruajThemeAmbjente(out id, KodTheme, PershkrimTheme, DefaultTheme, Zgjedhur, IdThemeFrames, IdThemeFrameKryesor, IdThemeJQuery, IdBgImage, IdPerdorues, IdStatusDok);
                idThemeAmbjente = id;
                return u_ruajt;
            }
        }

        public static clsMesazh zgjidhMotiv(int idMotivPerTeZgjedhur, int idPerdorues)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh = new clsMesazh(false);
            dbAdmin.beginTransaksion();
            try
            {
                colThemesAmbjente themeVjeter = new colThemesAmbjente();
                bool sukses = themeVjeter.merrGjitheThemesAmbjenteVjeterZgjedhurPerPerd(idPerdorues, dbAdmin);
                if (!sukses)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate zgjedhjes se motivit!");
                }

                for (int i = 0; i < themeVjeter.Count; i++) //nqs ka ndodhur ndonje gabim dhe jane selektuar disa njeheresh
                {
                    mesazh = zgjidh(themeVjeter[i].IdThemeAmbjente, false, idPerdorues, dbAdmin);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }

                mesazh = zgjidh(idMotivPerTeZgjedhur, true, idPerdorues, dbAdmin);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }

                dbAdmin.commitTransaksion();
                return mesazh;
            }
            catch
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate zgjedhjes se motivit!");
            }
        }

        /// <summary>
        /// Modifikon objektin e theme ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoThemeAmbjente"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Modifiko()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.modifikoThemeAmbjente(IdThemeAmbjente, KodTheme, PershkrimTheme, DefaultTheme, Zgjedhur, IdThemeFrames, IdThemeFrameKryesor, IdThemeJQuery, IdBgImage, IdPerdorues, IdStatusDok);
            }
        }

        public static clsMesazh zgjidh(int idThemeAmb, bool zgjidh, int idPerd, clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.modifikoZgjedhurThemeAmbjente(idThemeAmb, zgjidh, idPerd);
        }

        /// <summary>
        /// Fshin objektin e theme ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiThemeAmbjente"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.fshiThemeAmbjente(IdThemeAmbjente);

        }

        public static DataRow merrThemeAmbjenteDR(int id)
        {
            try
            {
                using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
                {
                    return dbAdmin.merrThemeAmbjenteSipasId(id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Kontrollon nese ekziston nje theme me kodin e dhene per perdoruesin       
        /// </summary>
        /// <returns>true nqs ekziston dhe false nqs nuk ekziston</returns>
        public static bool ekzistonThemeAmbjent(string kodTheme, int idPerd)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool ekziston = dbAdmin.ekzistonThemeAmbjent(kodTheme, idPerd);
            dbAdmin.Dispose();
            return ekziston;
        }

        /// <summary>
        /// Kontrollon nese theme me id-ne e dhene eshte theme default       
        /// </summary>
        /// <returns>true nqs eshte theme default dhe false nese jo</returns>
        public static bool eshteThemeDefault(int idTheme)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool eshteThemeDefault = dbAdmin.eshteThemeDefault(idTheme);
            dbAdmin.Dispose();
            return eshteThemeDefault;
        }

        /// <summary>
        /// Kontrollon nese theme me id-ne e dhene eshte theme qe perdoret nga perdoruesi
        /// </summary>
        /// <returns>true nqs perdoret dhe false nese jo</returns>
        public static bool eshteThemeZgjedhur(int idTheme)
        {
            if (idTheme == 0) return false;
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.eshteThemeZgjedhur(idTheme);

        }

        public bool ktheThemeZgjedhurPerdorues(int idperd)
        {
            if (idperd == 0) return false;
            GlobalCacheManager.MySessionCache.FillObjectFromCache(x => x.IdPerdorues == idperd,
                () =>
                {
                    using (var dbAdm = new clsDatabaseAdmin())
                    {
                        dbAdm.ktheThemeZgjedhurPerdorues(idperd, this);
                    }
                    return this;
                }, this);
            return true;
        }


        public void mbushThemeSipasID(int idja)
        {

            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                dbAdm.merrThemeAmbjenteSipasId(idja, this);
            }

        }

        public void mbushThemeSipasID(int idja, clsDatabaseAdmin dbAdm)
        {
            dbAdm.merrThemeAmbjenteSipasId(idja, this);
        }

        public static clsMesazh krijoThemesDefaultPerPerdorues(int idperd)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh u_krijuan = dbAdmin.krijoThemesDefaultPerPerdorues(idperd);
            dbAdmin.Dispose();
            return u_krijuan;
        }

        public static int ktheIdTheme(int idperd)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                return dbAdm.ktheIdThemeZgjedhurPerdorues(idperd);
            }
        }

        public static clsMesazh krijoThemesDefaultPerPerdorues(int idperd, clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.krijoThemesDefaultPerPerdorues(idperd);

        }
        #endregion

        #region Metoda Internal
        public void Mbush(IDataRecord dbDataRowThemeAmbjente)
        {
            int.TryParse(dbDataRowThemeAmbjente["IDTHEMEAMBJENTE"].ToString(), out idThemeAmbjente);
            kodTheme = dbDataRowThemeAmbjente["KODTHEME"].ToString();
            pershkrimTheme = dbDataRowThemeAmbjente["PERSHKRIMTHEME"].ToString();
            bool.TryParse(dbDataRowThemeAmbjente["DEFAULTTHEME"].ToString(), out defaultTheme);
            bool.TryParse(dbDataRowThemeAmbjente["ZGJEDHUR"].ToString(), out zgjedhur);
            int.TryParse(dbDataRowThemeAmbjente["IDTHEMEFRAMES"].ToString(), out idThemeFrames);
            int.TryParse(dbDataRowThemeAmbjente["IDTHEMEFRAMEKRYESOR"].ToString(), out idThemeFrameKryesor);
            int.TryParse(dbDataRowThemeAmbjente["IDTHEMEJQUERY"].ToString(), out idThemeJQuery);
            int.TryParse(dbDataRowThemeAmbjente["IDBGIMAGE"].ToString(), out idBgImage);
            int.TryParse(dbDataRowThemeAmbjente["IDPERDORUES"].ToString(), out idPerdorues);
            int.TryParse(dbDataRowThemeAmbjente["IDSTATUSDOK"].ToString(), out idStatusDok);
            DateTime.TryParse(dbDataRowThemeAmbjente["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(dbDataRowThemeAmbjente["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            pathDevExpress = dbDataRowThemeAmbjente["pathDevExpress"].ToString();
            pathJquery = dbDataRowThemeAmbjente["pathJquery"].ToString();
        }
        #endregion
    }
}
