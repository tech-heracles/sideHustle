using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  faturat e shperndarjes se shpenzimeve
    ///  (Te dhenat  merren nga tabela : T_SHPERNDARJESHPENZIMEFATURAT)
    /// </summary>
    public class clsShperndarjeShpenzimeFaturat
    {
        #region Atributet

        private int idTrupi;
        private int idKoka;
        private int idFatura;
        private DataRow rreshti;

        #endregion

        #region konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idfatura">id e fatures</param>
        /// <param name="idkoka"> id e kokes se dokumentit te shperndarjes se shpenzimeve</param>
        /// <param name="idtruepi"> id e trupit te dokumentit te shperndarje se shpenzimeve</param>
        public clsShperndarjeShpenzimeFaturat(int idtruepi, int idkoka, int idfatura)
        {
            idTrupi = idtruepi;
            idKoka = idkoka;
            idFatura = idfatura;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsShperndarjeShpenzimeFaturat()
        {
        }

        public clsShperndarjeShpenzimeFaturat(DataRow rreshti)
        {
            
            mbushShperndarjeShpenzFaturat(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e trupit te dokumentit shperndarje shpenzimesh
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit shperndarje shpenzimesh.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e fatures.
        /// </summary>
        public int IdFatura
        {
            get { return idFatura; }
            set { idFatura = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush faturen e shperndarjes se shpenzimeve nga databaza
        /// </summary>
        /// <param name="dbDataRowShperndarjeShpenzFaturat">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushShperndarjeShpenzFaturat(DataRow dbDataRowShperndarjeShpenzFaturat)
        {
            if (dbDataRowShperndarjeShpenzFaturat != null)
            {
                try
                {
                    int.TryParse(dbDataRowShperndarjeShpenzFaturat["IDSHPERNDARJESHPENZFATURAT"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowShperndarjeShpenzFaturat["IDSHPERNDARJESHPENZKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowShperndarjeShpenzFaturat["IDFATURA"].ToString(), out idFatura);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fatures se shperndarjes se shpenzimeve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}