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
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne konfigurimin qe i behet aplikcacionit
    ///  ne lidhje te dhenat e nevojshme per hapjen e raporteve.(Te dhenat  merren nga tabela : T_KONFIGURIME)
    /// </summary>
    public class clsKonfigurime
    {
        #region Atribute

        private int ndermarjeId; //NDERMID 
        private String emri; //NDERMEMRI
        private String path; //RAPPATH
        private String username; //RAPUSERNAME
        private String password; //RAPPASSWORD
        private String domain; //RAPDOMAIN
        private String folder; //RAPREPFOLDER
        private Boolean multiple;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKonfigurime(int id, String emriperd,
                               String rapPath, String user, String pw, String dom, String fold, bool mult)
        {
            ndermarjeId = id;
            emri = emriperd;
            path = rapPath;
            username = user;
            password = pw;
            domain = dom;
            folder = fold;
            multiple = mult;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsKonfigurime()
        {            
        }

        public clsKonfigurime(DataRow rreshti)
        {
            
            mbushKonfigurime(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes qe i perket ky konfigurim.
        /// </summary>
        public int IdNdermarje
        {
            get { return ndermarjeId; }
            set { ndermarjeId = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e ndermarrjes qe i perket ky konfigurim.
        /// </summary>
        public String EmriNdermarje
        {
            get { return emri; }
            set { emri = value; }
        }

        /// <summary>
        /// Kthen/Vendos path-in ku ruhen raportet.
        /// </summary>
        public String RaportPath
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// Kthen/Vendos usernamen e perdoruesit qe perdoret per te hapur raportet(nga ana teknike, jo perdoruesin e AlphaWeb-it).
        /// </summary>
        public String Perdorues
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Kthen/Vendos passwordin e perdoruesit qe perdoret per te hapur raportet.
        /// (nga ana teknike, jo perdoruesin e AlphaWeb-it).
        /// </summary>
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e domain-it per serverin e raporteve.
        /// </summary>
        public String RaportDomain
        {
            get { return domain; }
            set { domain = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e folderit ku do ruhen raportet.
        /// </summary>
        public String RaportFolder
        {
            get { return folder; }
            set { folder = value; }
        }

        /// <summary>
        /// Nuk dihet pse ?!.
        /// </summary>
        public Boolean MultipleUser
        {
            get { return multiple; }
            set { multiple = value; }
        }

        #endregion

        #region Metoda Publike

        ///// <summary>
        ///// Nuk perdoret.
        ///// </summary>
        //public bool ruaj()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    //bool u_ruajt = data.ruajKonfigurim(this);
        //    return true;
        //}

        ///// <summary>
        ///// Nuk perdoret.
        ///// </summary>
        //public bool modifiko()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    //bool u_modifikua = data.modifikoKonfigurim(this);
        //    return true;
        //}

        ///// <summary>
        ///// Nuk perdoret.
        ///// </summary>
        //public bool fshi()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    //bool u_fshi = data.fshiKonfigurim(this);
        //    return true;
        //}

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsKonfigurime"/>, te cilat i merr nga
        /// tabela perkatese ne databaze.
        /// </summary>
        public colKonfigurime merr()
        {
            colKonfigurime data = new colKonfigurime();
            data.mbushGjitheKonfigurimet();            
            return data;
            //return new colKonfigurime();
        }

        ///// <summary>
        ///// Nuk perdoret.
        ///// </summary>
        //public int ekzistonKonfigurimMultiple()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    //return data.ekzistonKonfigurimMultiple();
        //    return 1;
        //}

        #endregion

        #region Metoda Internal

        internal bool mbushKonfigurime(DataRow dbDataRowKonfigurime)
        {
            if (dbDataRowKonfigurime != null)
            {
                try
                {
                    int.TryParse(dbDataRowKonfigurime["NDERMID"].ToString(), out ndermarjeId);
                    emri = dbDataRowKonfigurime["NDERMEMRI"].ToString();
                    path = dbDataRowKonfigurime["RAPPATH"].ToString();
                    username = dbDataRowKonfigurime["RAPUSERNAME"].ToString();
                    password = dbDataRowKonfigurime["RAPPASSWORD"].ToString();
                    domain = dbDataRowKonfigurime["RAPDOMAIN"].ToString();
                    folder = dbDataRowKonfigurime["RAPREPFOLDER"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konfigurimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
