using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne llojet e taksave
    ///  (Te dhenat  merren nga tabela : T_LLOJTAKSE)
    /// </summary>
    /// <remarks> eshte tabele ndihmese per taksat</remarks>
    /// <example> nivel tvsh, tvsh, taksa doganore</example>
    public class clsLlojTakse
    {
        #region Atribute

        private int idLlojTakse;
        private string pershkrim;
        private DataRow rreshti;
       
        #endregion
       
        #region kontruktoret
      /// <summary>
      /// konstruktori me parametra
      /// </summary>
      /// <param name="idLlojTakse"> id ritese e llojit te taksave</param>
      /// <param name="pershkrim"> pershkrimi i llojit te taksave</param>
        public clsLlojTakse(    int idLlojTakse, string pershkrim)
        {
            this.idLlojTakse =idLlojTakse ;
            this.pershkrim =pershkrim ;
        }
      /// <summary>
      /// konstruktori pa parametra
      /// </summary>
        public clsLlojTakse()
        {
        }

        public clsLlojTakse(DataRow rreshti)
        {
            
            mbushLlojTakse(rreshti);
        }
        #endregion
       
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojTakse {
            get
            {
                return idLlojTakse;
            }
            set
            {
                idLlojTakse = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e llojit te taksave.
        /// </summary>
        public string  Pershkrim {
            get
            {
                return pershkrim;
            }
            set
            {
                pershkrim = value;
            }
        }
      
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llojin e takses nga databaza
        /// </summary>
        /// <param name="dbDataRowLlojTakse">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlojTakse(DataRow dbDataRowLlojTakse)
        {
            if (dbDataRowLlojTakse != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojTakse["IDLLOJTAKSE"].ToString(), out idLlojTakse);
                    pershkrim = dbDataRowLlojTakse["PERSHKRIM"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojeve te takses nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
