using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne llojet e makrove
    ///  (Te dhenat  merren nga tabela : T_LLOJMAKRO)
    /// </summary>
    /// <remarks> eshte tabele ndihmese per makrot</remarks>
    /// <example> Artikull, Makro, Tekst</example>
    public  class clsLlojMakro
    {
        #region Atributet

        private int idLlojMakro;
        private string pershkrimLlojMakro;
        private DataRow rreshti;
       
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojMakro {
            get
            {
                return idLlojMakro;
            }
            set
            {
                idLlojMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e llojit te makros.
        /// </summary>
        public string  PershkrimLlojMakro {
            get
            {
                return pershkrimLlojMakro;
            }
            set
            {
                pershkrimLlojMakro = value;
            }
        }
      
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idLlojMakro"> id ritese e llojit te makros</param>
        /// <param name="pershkrimLlojMakro"> pershkrimi i llojit te makros</param>
        public clsLlojMakro(    int idLlojMakro, string pershkrimLlojMakro)
        {
            this.idLlojMakro =idLlojMakro ;
            this.pershkrimLlojMakro =pershkrimLlojMakro ;
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsLlojMakro()
        {
        }

        public clsLlojMakro(DataRow rreshti)
        {
            
            mbushLlojMakro(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e llojeve te makros nga databaza
        /// </summary>
        /// <param name="dbDataRowKokaMakro">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushLlojMakro(DataRow dbDataRowLlojMakro)
        {
            if (dbDataRowLlojMakro != null)
            {

                try
                {

                    int.TryParse(dbDataRowLlojMakro["IDLLOJMAKRO"].ToString(), out idLlojMakro);
                    pershkrimLlojMakro = dbDataRowLlojMakro["PERSHKRIMLLOJMAKRO"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojeve te makros nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
