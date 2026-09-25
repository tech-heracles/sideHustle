using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace DbCore.DbCRM
{
    public class clsFotoAnkete
    {
        #region Atribute

        private int idFotoAnkete;
        private int idTrupiKlientAnketa;
        private System.Drawing.Image foto;
        private bool fotoLoaded = false;
       

        #endregion

        #region Properties
        public int IdFotoAnkete
        {
            get { return idFotoAnkete; }
            set { idFotoAnkete = value; }
        }

        public int IdTrupiKlientAnketa
        {
            get { return idTrupiKlientAnketa; }
            set { idTrupiKlientAnketa = value; }
        }

        //public byte[] Foto
        //    get
               
        //    set

        #endregion

        #region Konstruktoret

        public clsFotoAnkete()
        {

        }

        #endregion

        #region Metoda Publike


        public static byte[] merrFoto(int id)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            byte[] foto = dbCRM.merrFoto(id);
            dbCRM.Dispose();
            return foto;
        }
        #endregion

        #region Metoda Internal

        internal bool mbushFotoAnketa(DataRow dbDataRowTrupiAnketa)
        {
            if (dbDataRowTrupiAnketa != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupiAnketa["IDFOTOANKETE"].ToString(), out idFotoAnkete);
                    int.TryParse(dbDataRowTrupiAnketa["IDTRUPIKLIENTANKETE"].ToString(), out idTrupiKlientAnketa);
                   
                   
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te FOTOANKETE nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}