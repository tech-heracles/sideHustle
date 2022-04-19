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
    ///  (Te dhenat  merren nga tabela : T_SHPERNDARJESHPENZIMELLOGITE)
    /// </summary>
    public class clsShperndarjeShpenzimeLlogarite
        {
        #region Atributet

        private int idShperndarjeShpenzimeLlog;
        private int idKoka;
        private int idLlogari;
        private double vlefta;
        private string nrLlogari;
        private string pershkrimiLlogari;
        private DataRow rreshti;

        #endregion

        #region konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idfatura">id e fatures</param>
        /// <param name="idkoka"> id e kokes se dokumentit te shperndarjes se shpenzimeve</param>
        /// <param name="idtruepi"> id e trupit te dokumentit te shperndarje se shpenzimeve</param>
        public clsShperndarjeShpenzimeLlogarite(int idshpshpll, int idkoka, int idllogari, double vlefta)
            {
            idShperndarjeShpenzimeLlog = idshpshpll;
            idKoka = idkoka;
            idLlogari = idllogari;
            this.vlefta = vlefta;
            }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsShperndarjeShpenzimeLlogarite()
            {
            }

        public clsShperndarjeShpenzimeLlogarite(DataRow rreshti)
        {
            
            mbushShperndarjeShpenzLlog(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e  shperndarje shpenzimesh   llogarite inkrementuese
        /// </summary>
        public int IdShperndarjeShpenzimeLLog
            {
            get { return idShperndarjeShpenzimeLlog; }
            set { idShperndarjeShpenzimeLlog = value; }
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
        /// Kthen/Vendos ID-ne e llogarise.
        /// </summary>
        public int IdLlogari
            {
            get { return idLlogari; }
            set { idLlogari = value; }
            }
        public double Vlefta
            {
            get
                {
                return vlefta;
                }
            set
                {
                vlefta = value;
                }
            }
        public string NrLlogari
            {
            get { return nrLlogari; }
            set
                {

                nrLlogari = value;
                }
            }
        public string PershkrimiLlogari
            {
            get { return pershkrimiLlogari; }
            set
                {

                pershkrimiLlogari = value;
                }
            }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llogarite e shperndarjes se shpenzimeve nga databaza
        /// </summary>
        /// <param name="dbDataRowShperndarjeShpenzLlog">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushShperndarjeShpenzLlog(DataRow dbDataRowShperndarjeShpenzLlog)
            {
            if (dbDataRowShperndarjeShpenzLlog != null)
                {
                try
                    {
                    int.TryParse(dbDataRowShperndarjeShpenzLlog["IDSHPERNDARJESHPENZIMESHLLOG"].ToString(), out idShperndarjeShpenzimeLlog);
                    int.TryParse(dbDataRowShperndarjeShpenzLlog["IDSHPERNDARJESHPEZKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowShperndarjeShpenzLlog["IDLLOGARI"].ToString(), out idLlogari);
                    double.TryParse(dbDataRowShperndarjeShpenzLlog["VLERA"].ToString(), out vlefta);
                    nrLlogari = dbDataRowShperndarjeShpenzLlog["NRLLOGARI"].ToString();
                    pershkrimiLlogari = dbDataRowShperndarjeShpenzLlog["EMERLLOGARI_1"].ToString(); 
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