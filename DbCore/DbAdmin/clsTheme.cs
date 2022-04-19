using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje template te theme per konfigurimin grafik te programit.
    ///  (Te dhenat  merren nga tabela : T_THEME)
    /// </summary>
    public class clsTheme
    {
        #region Atributet

        private int idTheme;   
        private String emriTheme;
        private String pathTheme;
        public static int defaultTheme = ktheIdThemeSipasEmrit("Modeli 248");
        public static string defaultThemeEmri = "Modeli 248";
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTheme(int id, String emri, String path)
        {
            idTheme = id;
            emriTheme = emri;
            pathTheme = path;           
        }

        public clsTheme(int idTheme)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushTheme(data.merrSipasId(idTheme));
            data.Dispose();
        }

        public clsTheme(String emri)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushTheme(data.merrSipasEmri(emri));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTheme()
        {
           
        }

        public clsTheme(DataRow rreshti)
        {
            
            mbushTheme(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTheme
        {
            get { return idTheme; }
            set { idTheme = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e theme.
        /// </summary>
        public String EmriTheme
        {
            get { return emriTheme; }
            set { emriTheme = value; }
        }

        /// <summary>
        /// Kthen/Vendos path e imazhit per kete theme.
        /// </summary>
        public String PathTheme
        {
            get { return pathTheme; }
            set { pathTheme = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kthen nje collection me te gjithe themes ekzustuese ne DB
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.merrGjitheTheme"/> 
        /// </summary>
        public colTheme merrGjitheTheme()
        {
            colTheme data = new colTheme();
            data.mbushGjitheTheme();
            return data;
        }

        /// <summary>
        /// Kthen te gjitha propertite e theme-s me emrin e dhene
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.merrGjitheTheme"/> 
        /// </summary>
        public clsTheme merrSipasEmri()
        {
            clsTheme data = new clsTheme(this.EmriTheme);
            return data;
        }

        public static int ktheIdThemeSipasEmrit(string emri)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            int id = data.ktheIdThemeSipasEmrit(emri);            
            data.Dispose();
            return id;
        }

        public static string kthePathThemeSipasEmrit(string emri)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.kthePathThemeSipasEmrit(emri);
            }
        }

        /// <summary>
        /// Kthen te gjitha propertite e theme-s me ID e dhene
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.merrGjitheTheme"/> 
        /// </summary>
        public clsTheme merrSipasID()
        {
            clsTheme data = new clsTheme(this.IdTheme);
            return data;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushTheme(DataRow dbDataRowTheme)
        {
            if (dbDataRowTheme != null)
            {
                try
                {
                    int.TryParse(dbDataRowTheme["IDTHEME"].ToString(), out idTheme);
                    emriTheme = dbDataRowTheme["EMRITHEME"].ToString();
                    pathTheme = dbDataRowTheme["PATH"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se theme-ave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
