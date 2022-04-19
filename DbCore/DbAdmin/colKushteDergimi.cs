using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colKushteDergimi : System.Collections.Generic.List<clsKushtDergimi>
    {
        #region Konstruktoret

        public colKushteDergimi()
        {
        }

        public colKushteDergimi(int idNdermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushKushteDergimi(data.ktheKushtetDergimit(idNdermarje));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsKushtDergimi this[int index]
        {
            get { return ((clsKushtDergimi)base[index]); }
        }

        #endregion

        #region Metoda Private

        private bool mbushKushteDergimi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKushtDergimi transporti = new clsKushtDergimi();
                    //transporti.mbushKushtDergimi(rreshti);
                    Add(new clsKushtDergimi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKushteDergimi(DataTable dt)", true)]
        public colKushteDergimi mbushArrayListKushteDergimi(DataSet ds)
        {
            colKushteDergimi kushtet = new colKushteDergimi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKushtDergimi transporti = new clsKushtDergimi();

                transporti.IdKushtDergimi = int.Parse(rreshti[0].ToString());
                transporti.KodiKushtDergimi = rreshti[1].ToString();
                transporti.PershkrimiKushtDergimi = rreshti[2].ToString();
                transporti.IdNdermarje = int.Parse(rreshti[3].ToString());
                kushtet.Add(transporti);
            }
            return kushtet;
        }
    }
}