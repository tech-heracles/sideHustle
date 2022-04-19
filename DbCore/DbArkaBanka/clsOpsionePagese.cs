using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbArkaBanka
{
   public class clsOpsionePagese
    {
        #region Atribute

        private int id;
        private string pershkrimi;
       
        #endregion
       
        #region kontruktoret
      /// <summary>
      /// konstruktori me parametra
      /// </summary>
      /// <param name="idLlojLicence"> id ritese e llojit te taksave</param>
      /// <param name="pershkrim"> pershkrimi i llojit te taksave</param>
        public clsOpsionePagese(    int id,string pershkrim)
        {
            this.id =id;
           
            this.pershkrimi =pershkrim ;
        }
      /// <summary>
      /// konstruktori pa parametra
      /// </summary>
        public clsOpsionePagese()
        {
        }
        public clsOpsionePagese(DataRow dbDataRow)
        {
            mbushPagese(dbDataRow);
        }
        public clsOpsionePagese(string pershkrimi)
        {
            clsDatabaseArkaBanka db = new clsDatabaseArkaBanka();
            mbushPagese( db.merrOpsionePageseSipasPershkrimit(pershkrimi));
            db.Dispose();
        }
        #endregion
       
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
     
      
        /// <summary>
        /// Kthen/Vendos pershkrimin e llojit te taksave.
        /// </summary>
        public string  Pershkrimi {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }
      
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llojin e takses nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushPagese(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se opsione pagese nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
