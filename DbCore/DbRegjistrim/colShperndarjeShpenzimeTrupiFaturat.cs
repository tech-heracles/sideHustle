using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsShperndarjeShpenzimeTrupiFaturat
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colShperndarjeShpenzimeTrupiFaturat : System.Collections.Generic.List<clsShperndarjeShpenzimeTrupiFaturat>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colShperndarjeShpenzimeTrupiFaturat()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi">id e trupit</param>
        public colShperndarjeShpenzimeTrupiFaturat(int idTrupi)
        {
            clsDatabaseRegjistrim dbShperndShpenTrupiFat = new clsDatabaseRegjistrim();
            mbushShperndarjeShpenzimeTrupiFaturat(dbShperndShpenTrupiFat.ktheShperndarjeShpenzimeshFaturatSipasTrupit(idTrupi));
            dbShperndShpenTrupiFat.Dispose();
        }

        public colShperndarjeShpenzimeTrupiFaturat(int idNdermarrje, Dictionary<string, object> rreshtDokuKlient, object shpenzimi)
        {
            object[] trup = (object[])(rreshtDokuKlient["OcolTrupiMagazina"]);
            object[] shpez = (object[])shpenzimi;
            for (int i = 0; i < trup.Length; i++)
            {
                if (trup[i] != null)
                {
                    clsShperndarjeShpenzimeTrupiFaturat fat = new clsShperndarjeShpenzimeTrupiFaturat((Dictionary<string, object>)trup[i], shpez[i+1]);
                    this.Add(fat);
                }
            }

            // ((Dictionary<string,object>)((object[])(rreshtDokuKlient["OcolTrupiMagazina"]))[0])["IdArtikulli"]
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeTrupiFaturat"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsShperndarjeShpenzimeTrupiFaturat this[int index]
        {
            get { return ((clsShperndarjeShpenzimeTrupiFaturat)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsShperndarjeShpenzimeTrupiFaturat"/> 
        /// </summary>
        private bool mbushShperndarjeShpenzimeTrupiFaturat(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsShperndarjeShpenzimeTrupiFaturat trupi = new clsShperndarjeShpenzimeTrupiFaturat();
                    //trupi.mbushShperndarjeShpenzTrupFat(rreshti);
                    Add(new clsShperndarjeShpenzimeTrupiFaturat(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushShperndarjeShpenzimeTrupiFaturat(DataTable dt)", true)]
        public colShperndarjeShpenzimeTrupiFaturat mbushArrayListShperndarjeShpenzimeTrupiFaturat(DataSet ds)
        {
            colShperndarjeShpenzimeTrupiFaturat trupat = new colShperndarjeShpenzimeTrupiFaturat();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsShperndarjeShpenzimeTrupiFaturat trupi = new clsShperndarjeShpenzimeTrupiFaturat();

                trupi.IdTrupiFaturat= int.Parse(rreshti[0].ToString());
                trupi.IdShperndarjeShpenzTrupi = int.Parse(rreshti[1].ToString());
                trupi.IdArtikull = int.Parse(rreshti[2].ToString());
                trupi.IdTrupiShitje = int.Parse(rreshti[3].ToString());
                trupi.Vlera = double.Parse(rreshti[4].ToString());
                trupat.Add(trupi);
            }
            return trupat;
        }


    }
}
