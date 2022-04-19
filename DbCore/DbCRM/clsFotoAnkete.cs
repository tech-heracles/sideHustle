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
        //{
        //    get
        //    {
        //        if (foto == null && fotoLoaded)
        //            return null;
               
        //        MemoryStream ms = null;
        //        if (foto == null && !fotoLoaded)
        //        {
        //            byte[] logoja; //= merrFoto();
        //            if (logoja != null && ((byte[])logoja).Length != 0)
        //            {
        //                ms = new MemoryStream(logoja);
        //                foto = Image.FromStream(ms);
        //            }
        //            else return null;
        //        }
        //        if (ms == null)
        //        {
        //            ms = new MemoryStream();
        //            foto.Save(ms, foto.RawFormat);
        //        }
        //        return ms.ToArray();
        //    }
        //    set
        //    {
        //        if (value != null)
        //            foto = Image.FromStream(new MemoryStream(value));
        //        else foto = null;
        //        fotoLoaded = true;
        //    }
        //}

        #endregion

        #region Konstruktoret

        public clsFotoAnkete()
        {

        }

        #endregion

        #region Metoda Publike

        //public clsMesazh ruaj()
        //{
        //    clsDatabaseCRM data = new clsDatabaseCRM();
        //    clsMesazh u_ruajt = ruaj(data);
        //    data.Dispose();
        //    return u_ruajt;
        //}

        //public clsMesazh ruaj(clsDatabaseCRM data)
        //{
        //    int id;
        //    clsMesazh u_ruajt = data.ruajFotoAnkete(out id, this.IdTrupiKlientAnketa,this.Foto);
        //    this.IdFotoAnkete = id;
        //    return u_ruajt;
        //}

      
       
      

        public static byte[] merrFoto(int id)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            byte[] foto = dbCRM.merrFoto(id);
           // fotoLoaded = true;
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