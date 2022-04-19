using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e konfigurimeve te list orari
    ///  (Te dhenat  merren nga tabela : T_TRUPIKONFIGLISTORARI)
    /// </summary>
    public class clsTrupiKonfigListOrari
    {
        #region Atributet

        private int idTrupi;
        private int idKoka;
        private string diteJave;
        private string diteJave_Eng;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTrupiKonfigListOrari(int idTrupi, int idKoka, string diteJave, string diteJave_Eng)
        {
            this.idTrupi = idTrupi;
            this.diteJave = diteJave;
            this.idKoka = idKoka;
            this.diteJave_Eng = diteJave_Eng;

        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTrupiKonfigListOrari()
        {
        }

        public clsTrupiKonfigListOrari(DataRow rreshti)
        {
            
            mbushTrupiKonfigListOrari(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dite jave per te cilat vlen konfigurimi
        /// </summary>
        public string DiteJave
        {
            get
            {
                return diteJave;
            }
            set
            {
                diteJave = value;
            }
        }

        public string DiteJave_ENG
        {
            get
            {
                return diteJave_Eng;
            }
            set
            {
                diteJave_Eng= value;
            }
        }

        #endregion



        #region Metoda Internal

        internal bool mbushTrupiKonfigListOrari(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    diteJave = dbDataRow["DITEJAVE"].ToString();
                    diteJave_Eng = dbDataRow["DITEJAVE_ENG"].ToString();
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupin e konfigurimit te list orarit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
