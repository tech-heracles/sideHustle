using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKushtPageseTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKushtPageseTrupi : System.Collections.Generic.List<clsKushtPageseTrupi>
    {

        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKushtPageseTrupi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKoka">id e kokes</param>
        public colKushtPageseTrupi(int idKoka)
        {
            clsDatabaseKontabilitet dbKushtPagTrupi = new clsDatabaseKontabilitet();
            mbushKushtetPagesaTrup(dbKushtPagTrupi.ktheTrupatKushtevePagesesSipasKokes(idKoka));
            dbKushtPagTrupi.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKushtPageseTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKushtPageseTrupi this[int index]
        {
            get { return ((clsKushtPageseTrupi)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsKushtPageseTrupi"/> 
        /// </summary>
        private bool mbushKushtetPagesaTrup(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKushtPageseTrupi trupi = new clsKushtPageseTrupi();
                    //trupi.mbushKushtPagTrup(rreshti);
                    Add(new clsKushtPageseTrupi(rreshti));
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
        [Obsolete("Perdor: bool mbushKushtetPagesaTrup(DataTable dt)", true)]
        public colKushtPageseTrupi mbushArrayListKushtePageseKoka(DataSet ds)
        {
            colKushtPageseTrupi col = new colKushtPageseTrupi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKushtPageseTrupi koka = new clsKushtPageseTrupi();

                koka.IdTrupi = int.Parse(rreshti[0].ToString());
                koka.IdKoka = int.Parse(rreshti[1].ToString());
                koka.Intervali = rreshti[2].ToString();
                koka.Periudha = rreshti[3].ToString();
                koka.Dite = int.Parse(rreshti[4].ToString());
                koka.Zbritje = int.Parse(rreshti[5].ToString());
                koka.KushtPagese = int.Parse(rreshti[6].ToString());
                col.Add(koka);
            }
            return col;
        }
    }
}
