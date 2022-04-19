using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFleteDoganoreTaksa
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFleteDoganoreTaksa : System.Collections.Generic.List<clsFleteDoganoreTaksa>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colFleteDoganoreTaksa()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se fletes doganore</param>
        public colFleteDoganoreTaksa(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreTaksa = new clsDatabaseRegjistrim();
            mbushFletaDoganoreTaksat(dbFleteDoganoreTaksa.ktheFleteDoganoreTaksaSipasKoka(id));
            dbFleteDoganoreTaksa.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsFleteDoganoreTaksa"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsFleteDoganoreTaksa this[int index]
        {
            get { return ((clsFleteDoganoreTaksa)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 

        ///  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreTaksa"/> 
        /// </summary>
        private bool mbushFletaDoganoreTaksat(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFleteDoganoreTaksa taksa = new clsFleteDoganoreTaksa();
                    //taksa.mbushFleteDoganoreTaksa(rreshti);
                    Add(new clsFleteDoganoreTaksa(rreshti));
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
        [Obsolete("Perdor: bool mbushFletaDoganoreTaksat(DataTable dt)", true)]
        public colFleteDoganoreTaksa mbushArrayListFleteDoganoreTaksa(DataSet ds)
        {
            colFleteDoganoreTaksa taksat = new colFleteDoganoreTaksa();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFleteDoganoreTaksa taksa = new clsFleteDoganoreTaksa();
                taksa.IdFleteDoganoreTaksa = int.Parse(rreshti[0].ToString());
                taksa.IdFleteDoganoreKoka = int.Parse(rreshti[1].ToString());
                taksa.IdTaksa = int.Parse(rreshti[2].ToString());
                taksa.Pershkrimi = rreshti[3].ToString();
                taksa.Vlefta = Decimal.Parse(rreshti[4].ToString());
                taksa.Tvsh = Boolean.Parse(rreshti[5].ToString());
                taksa.KodTaksa= " ";
                taksa.LlogariDebi = "632";
                taksa.LlogariKredi = "447";
                taksat.Add(taksa);
            }
            return taksat;
        }
    }
}
