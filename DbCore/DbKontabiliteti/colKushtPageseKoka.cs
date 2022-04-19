using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKushtPageseKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKushtPageseKoka : System.Collections.Generic.List<clsKushtPageseKoka>
    {

        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKushtPageseKoka()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        public colKushtPageseKoka(int idNderm)
        {
            clsDatabaseKontabilitet dbKushtPagesKoka = new clsDatabaseKontabilitet();
            mbushKushtetPagesaKoka(dbKushtPagesKoka.ktheGjitheKushtetPageses(idNderm));
            dbKushtPagesKoka.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsKushtPageseKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKushtPageseKoka this[int index]
        {
            get { return ((clsKushtPageseKoka)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="DbCore.DbKontabiliteti.clsKushtPageseKoka"/> 
        /// </summary>
        private bool mbushKushtetPagesaKoka(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKushtPageseKoka koka = new clsKushtPageseKoka();
                    //koka.mbushKushtPagesaKoka(rreshti);
                    Add(new clsKushtPageseKoka(rreshti));
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
        [Obsolete("Perdor: bool mbushKushtetPagesaKoka(DataTable dt)", true)]
        public colKushtPageseKoka mbushArrayListKushtePageseKoka(DataSet ds)
        {
            colKushtPageseKoka col = new colKushtPageseKoka();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKushtPageseKoka koka = new clsKushtPageseKoka();

                koka.IdKoka = int.Parse(rreshti[0].ToString());
                koka.KodiKushtPagese = rreshti[1].ToString();
                koka.EmertimiKushtPagese = rreshti[2].ToString();
                koka.LlojiKushtPagese = rreshti[3].ToString();
                koka.IdAutorizim = int.Parse(rreshti[4].ToString());
                koka.Afati = int.Parse(rreshti[5].ToString());
                koka.Ndarja = rreshti[6].ToString();
                koka.IntervaliMidisNdarjeve = rreshti[7].ToString();
                koka.Numri = int.Parse(rreshti[8].ToString());
                koka.NumriNdarjeve = int.Parse(rreshti[9].ToString());
                koka.IdNdermarje = int.Parse(rreshti[10].ToString());
                col.Add(koka);
            }
            return col;
        }
    }
}
