using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e grupeve te dokumentave
    ///  (Te dhenat  merren nga tabela : T_GRUPIMDOKUMENTASHTRUPI)
    /// </summary>
    public class clsGrupimDokumentiTrupi
    { 
        #region Atributet

        private int idGrupimTrupi;
        private int idGrupimKoka;
        private int idKonfig;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupimDokumentiTrupi(int idGrupimTrupi, int idGrupimKoka, int idKonfig)
        {
            this.idGrupimTrupi = idGrupimTrupi;
            this.idKonfig = idKonfig;
            this.idGrupimKoka = idGrupimKoka;
          
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupimDokumentiTrupi()
        {
        }

        public clsGrupimDokumentiTrupi(DataRow rreshti)
        {
            
            mbushGrupimTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupimTrupi
        {
            get { return idGrupimTrupi; }
            set { idGrupimTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes
        /// </summary>
        public int IdGrupimKoka
        {
            get { return idGrupimKoka; }
            set { idGrupimKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
            }
        }

       
        #endregion

      

        #region Metoda Internal

        internal bool mbushGrupimTrupi(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDGRUPIMTRUPI"].ToString(), out idGrupimTrupi);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDGRUPIMKOKA"].ToString(), out idGrupimKoka);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupim dokumenti trupi nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
