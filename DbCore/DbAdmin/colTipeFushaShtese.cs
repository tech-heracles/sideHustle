using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colTipeFushaShtese : System.Collections.Generic.List<clsTipiFushaShtese>
    {
        #region Metoda Publike

        public new clsTipiFushaShtese this[int index]
        {
            get { return ((clsTipiFushaShtese)base[index]); }
        }

        public bool shtoTipiFushaShtese(clsTipiFushaShtese tipiFushaShtese)
        {
            base.Add(tipiFushaShtese);
            if (base.Contains(tipiFushaShtese))
                return true;
            else return false;
        }

        public bool fshiTipiFushaShtese(clsTipiFushaShtese tipiFushaShtese)
        {
            base.Remove(tipiFushaShtese);
            if (base.Contains(tipiFushaShtese))
                return false;
            else return true;
        }

        public bool fshiGjitheTipeFushaShtese()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteTipFushaShtese(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoTipiFushaShteseNeIndeksin(int index, clsTipiFushaShtese tipiFushaShtese)
        {
            base.Insert(index, tipiFushaShtese);
        }

        public int indeksiTipiFushaShteset(clsTipiFushaShtese tipiFushaShtese)
        {
            return base.IndexOf(tipiFushaShtese);
        }

        public bool ekzistonTipiFushaShtese(clsTipiFushaShtese tipiFushaShtese)
        {
            if (base.Contains(tipiFushaShtese))
                return true;
            else return false;
        }

        public int numriTipiFushaShtese()
        {
            return base.Count;
        }

        public bool mbushGjithetTipeFushashShtese()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTipeFushashShtese(data.ktheGjithetTipeFushashShtese());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushTipeFushashShtese(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTipiFushaShtese tipiFushaShtese = new clsTipiFushaShtese();
                    //tipiFushaShtese.mbushTipFushShtese(rreshti);
                    Add(new clsTipiFushaShtese(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushTipeFushashShtese(DataTable dt)", true)]
        public colTipeFushaShtese mbushArrayListTipeshFushaShtese(DataSet ds)
        {
            colTipeFushaShtese tipeFushash = new colTipeFushaShtese();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTipiFushaShtese tipiFushaShtese = new clsTipiFushaShtese();

                tipiFushaShtese.IdTipiFushaShtese = int.Parse(rreshti[0].ToString());

                tipiFushaShtese.PershkrimiTipiFushaShtese = rreshti[1].ToString();

                tipeFushash.Add(tipiFushaShtese);
            }
            return tipeFushash;
        }
    }
}


