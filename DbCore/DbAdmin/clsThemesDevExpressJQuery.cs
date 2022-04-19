using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne temat e devexpressit (per komponentet e devexpressit) dhe temat e jquery
    ///  (Te dhenat  merren nga tabela : T_THEMESDEVEXPRESSJQUERY)
    /// </summary>
    public class clsThemesDevExpressJQuery
    {
        #region Atributet
        public static int defaultJQueryTheme = ktheIdThemeNgaEmri("Redmond");
        public static int defaultFramesTheme = ktheIdThemeNgaEmri("Black Glass");
        public static int defaultAmbjentTheme = ktheIdThemeNgaEmri("Aqua");
        private int idTheme;
        private string emriTheme;
        private string urlImgPreview;
        private bool lloji; //true eshte devexpress; false eshte jquery
        private string path; //pathi ku ndodhet theme ne projekt (per jqeury)
        private DataRow rreshti;

        #endregion

        #region Kontruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsThemesDevExpressJQuery()
        {
        }

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idTheme"> id ritese e temes</param>
        /// <param name="emerDev"> emri i temes</param>
        ///  /// <param name="urlImg"> url e imazhit preview</param>
        public clsThemesDevExpressJQuery(int idTheme, string emerTheme, string urlImgPreview, bool lloji, string path)
        {
            this.idTheme = idTheme;
            this.emriTheme = emerTheme;
            this.urlImgPreview = urlImgPreview;
            this.lloji = lloji;
            this.path = path;
        }

        public clsThemesDevExpressJQuery(int id)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushThemeDevExpressJQuery(data.merrThemeDevExpressJQuerySipasId(id));
            }
        }

        public clsThemesDevExpressJQuery(string emri)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushThemeDevExpressJQuery(data.merrThemeDevExpressJQuerySipasEmri(emri));
            data.Dispose();
        }

        public clsThemesDevExpressJQuery(DataRow rreshti)
        {
            
            mbushThemeDevExpressJQuery(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos id-ne
        /// </summary>
        public int IdTheme
        {
            get
            {
                return idTheme;
            }
            set
            {
                idTheme = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos emrin e temes
        /// </summary>
        public string EmriTheme
        {
            get
            {
                return emriTheme;
            }
            set
            {
                emriTheme = value;
            }
        }
                
        /// <summary>
        /// Kthen/Vendos url e imazhit te preview te temes
        /// </summary>
        public string UrlImgPreview
        {
            get
            {
                return urlImgPreview;
            }
            set
            {
                urlImgPreview = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos llojin
        /// </summary>
        public bool Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos url e imazhit te preview te temes
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        #endregion       

        #region Metoda Publike

        public static int ktheIdThemeNgaEmri(string emerTheme)
        {
            using (DbCore.DbAdmin.clsDatabaseAdmin dbAdm = new DbCore.DbAdmin.clsDatabaseAdmin())
            {
                int idTheme = dbAdm.merrThemeDevExpressJQueryNgaEmri(emerTheme);
                return idTheme;
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush theme nga databaza
        /// </summary>
        /// <param name="dbDataRowThemes">datarow qe duhet plotesuar nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kunder false</returns>
        internal bool mbushThemeDevExpressJQuery(DataRow dbDataRowThemes)
        {

            if (dbDataRowThemes != null)
            {
                try
                {
                    int.TryParse(dbDataRowThemes["IDTHEME"].ToString(), out idTheme);
                    emriTheme = dbDataRowThemes["EMRITHEME"].ToString();
                    urlImgPreview = dbDataRowThemes["URLIMGPREVIEW"].ToString();
                    bool.TryParse(dbDataRowThemes["LLOJI"].ToString(), out lloji);
                    path = dbDataRowThemes["PATHI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se theme nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
