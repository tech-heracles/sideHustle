using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne tipet e funksioneve te makros
    ///  (Te dhenat  merren nga tabela : T_FUNKSIONMAKRO)
    /// </summary>
    /// <remarks> eshte nje tabele ndihmese per makrot</remarks>
    /// <example>sasi manuale, sasi konstante, shumefish, shtim, sasi bosh</example>
    public class clsFunksionMakro
    { 
        #region Atribute

        private int idFunksionMakro;
        private string pershkrimFunksionMakro;
        private DataRow rreshti;
       
        #endregion
       
        #region Kontruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idFunksionMakro"> id ritese e funksionit te makros</param>
        /// <param name="pershkrimFunksionMakro"> pershkrimi i funksionit te makros</param>
        public clsFunksionMakro(int idFunksionMakro, string pershkrimFunksionMakro)
        {
            this.idFunksionMakro =idFunksionMakro ;
            this.pershkrimFunksionMakro =pershkrimFunksionMakro ;
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsFunksionMakro()
        {
        }

        public clsFunksionMakro(DataRow rreshti)
        {
            
            mbushFunksionMakro(rreshti);
        }

        #endregion
       
        #region Metoda Publike

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFunksionMakro {
            get
            {
                return idFunksionMakro;
            }
            set
            {
                idFunksionMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos peshkrimi i funksionit te makros.
        /// </summary>
        public string  PershkrimFunksionMakro {
            get
            {
                return pershkrimFunksionMakro;
            }
            set
            {
                pershkrimFunksionMakro = value;
            }
        }
      
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush funksion makro te marre nga databaza
        /// </summary>
        /// <param name="dbDataRowFunksionMakro">Datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFunksionMakro(DataRow dbDataRowFunksionMakro)
        {
            if (dbDataRowFunksionMakro != null)
            {

                try
                {
                    int.TryParse(dbDataRowFunksionMakro["IDFUNKSIONMAKRO"].ToString(), out idFunksionMakro);
                    pershkrimFunksionMakro = dbDataRowFunksionMakro["PERSHKRIMFUNKSIONMAKRO"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se funksionit makro nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
