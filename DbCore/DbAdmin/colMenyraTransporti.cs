using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colMenyraTransporti : System.Collections.Generic.List<clsMenyreTransporti>
    {
        #region Konstruktoret

        public colMenyraTransporti()
        {
        }

        public colMenyraTransporti(int idNdermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushMenyratTransporti(data.ktheMenyratTransportit(idNdermarje));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsMenyreTransporti this[int index]
        {
            get { return ((clsMenyreTransporti)base[index]); }
        }

        #endregion

        #region Metoda Private

        private bool mbushMenyratTransporti(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsMenyreTransporti transporti = new clsMenyreTransporti();
                    //transporti.mbushMenyreTransport(rreshti);
                    Add(new clsMenyreTransporti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushMenyratTransporti(DataTable dt)", true)]
        public colMenyraTransporti mbushArrayListMenyraTransporti(DataSet ds)
        {
            colMenyraTransporti menyrat = new colMenyraTransporti();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsMenyreTransporti transporti = new clsMenyreTransporti();

                transporti.IdMenyreTransporti = int.Parse(rreshti[0].ToString());
                transporti.KodiMenyreTransporti = rreshti[1].ToString();
                transporti.PershkrimiMenyreTransporti = rreshti[2].ToString();
                transporti.IdNdermarje = int.Parse(rreshti[3].ToString());
                menyrat.Add(transporti);
            }
            return menyrat;
        }
    }
}
