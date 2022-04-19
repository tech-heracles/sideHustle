using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsLlojLicence
    { 
        #region Atribute

        private int idLlojLicence;
        private string kod;
        private string pershkrim;
        private DataRow rreshti;
       
        #endregion
       
        #region kontruktoret
      /// <summary>
      /// konstruktori me parametra
      /// </summary>
      /// <param name="idLlojLicence"> id ritese e llojit te taksave</param>
      /// <param name="pershkrim"> pershkrimi i llojit te taksave</param>
        public clsLlojLicence(    int idLlojLicence,string kod, string pershkrim)
        {
            this.idLlojLicence =idLlojLicence ;
            this.kod = kod;
            this.pershkrim =pershkrim ;
        }
      /// <summary>
      /// konstruktori pa parametra
      /// </summary>
        public clsLlojLicence()
        {
        }

        public clsLlojLicence(DataRow rreshti)
        {
            
            mbushLlojLicence(rreshti);
        }
        #endregion
       
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojLicence {
            get
            {
                return idLlojLicence;
            }
            set
            {
                idLlojLicence = value;
            }
        }
        public string Kod
        {
            get
            {
                return kod;
            }
            set
            {
                kod = value;
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
        /// <param name="dbDataRowLlojLicence">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlojLicence(DataRow dbDataRowLlojLicence)
        {
            if (dbDataRowLlojLicence != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojLicence["IDLLOJLICENCE"].ToString(), out idLlojLicence);
                    kod = dbDataRowLlojLicence["KOD"].ToString();
                    pershkrim = dbDataRowLlojLicence["PERSHKRIM"].ToString();
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
