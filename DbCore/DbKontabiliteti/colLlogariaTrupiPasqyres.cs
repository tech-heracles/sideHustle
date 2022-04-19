using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlogariaTrupiPasqyres
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlogariaTrupiPasqyres : System.Collections.Generic.List<clsLlogariaTrupiPasqyres>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colLlogariaTrupiPasqyres()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e trupit te pasqyres</param>
        public colLlogariaTrupiPasqyres(int id)
        {
            clsDatabaseKontabilitet dbLlogariTrupiPasqyra = new clsDatabaseKontabilitet();
            mbushLlogariteTrupi(dbLlogariTrupiPasqyra.ktheLlogariPasqyraFinaciareTrupiSipasIdTrupi(id));
            dbLlogariTrupiPasqyra.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsLlogariaTrupiPasqyres"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlogariaTrupiPasqyres this[int index]
        {
            get { return ((clsLlogariaTrupiPasqyres)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsLlogariaTrupiPasqyres"/> 
        /// </summary>
        private bool mbushLlogariteTrupi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlogariaTrupiPasqyres llog = new clsLlogariaTrupiPasqyres();
                    //llog.mbushLlogariTrup(rreshti);
                    Add(new clsLlogariaTrupiPasqyres(rreshti));
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
        [Obsolete("Perdor: bool mbushLlogariteTrupi(DataTable dt)", true)]
        public colLlogariaTrupiPasqyres mbushArrayListLlogariaTrupi(DataSet ds)
        {
            colLlogariaTrupiPasqyres trupi = new colLlogariaTrupiPasqyres();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlogariaTrupiPasqyres llog = new clsLlogariaTrupiPasqyres();

                llog.IdLlogariaTrupi = int.Parse(rreshti[0].ToString());
                llog.IdTrupi = int.Parse(rreshti[1].ToString());
                llog.IdLlogaria = int.Parse(rreshti[2].ToString());
                llog.Emertimi = rreshti[3].ToString();
                llog.Gjendja = rreshti[4].ToString();
                llog.Shenja= rreshti[5].ToString();
                llog.IdPerdoruesi =int.Parse (rreshti[6].ToString ());
                llog.Lloji = rreshti[7].ToString();
                trupi.Add(llog);
            }
            return trupi;
        }
    }
}