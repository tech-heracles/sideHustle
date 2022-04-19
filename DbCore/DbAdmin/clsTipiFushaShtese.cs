using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne tipin e fushes shtese.
    ///  (Te dhenat  merren nga tabela : T_TIPIFUSHASHTESE)
    /// </summary>
    public class clsTipiFushaShtese
    {
        #region Atribute

        private int idTipiFushaShtese;
        private string pershkrimiTipiFushaShtese;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTipiFushaShtese(int idTipiFushaShtese, string pershkrimiTipiFushaShtese)
        {
            this.idTipiFushaShtese = idTipiFushaShtese;
            this.pershkrimiTipiFushaShtese = pershkrimiTipiFushaShtese;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTipiFushaShtese()
        {
        }

        public clsTipiFushaShtese(DataRow rreshti)
        {
            
            mbushTipFushShtese(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTipiFushaShtese
        {
            get { return idTipiFushaShtese; }
            set { idTipiFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e ftipit te fushes shtese
        /// </summary>
        public string PershkrimiTipiFushaShtese
        {
            get { return pershkrimiTipiFushaShtese; }
            set { pershkrimiTipiFushaShtese = value; }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushTipFushShtese(DataRow dbDataRowTipFushShtese)
        {
            if (dbDataRowTipFushShtese != null)
            {
                try
                {
                    int.TryParse(dbDataRowTipFushShtese["IDTIPIFUSHASHTESE"].ToString(), out idTipiFushaShtese);
                    pershkrimiTipiFushaShtese = dbDataRowTipFushShtese["PERSHKRIMITIPIFUSHASHTESE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipit te fushes shtese nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

  