using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colTabelatEmerLogjik : System.Collections.Generic.List<clsTabeleEmerLogjik>
    {
        #region Metoda Publike

        public new clsTabeleEmerLogjik this[int index]
        {
            get { return ((clsTabeleEmerLogjik)base[index]); }
        }

        public bool mbushGjitheTabelat()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTabelatEmerLogjik(data.ktheGjitheTabelat());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushTabelatEmerLogjik(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTabeleEmerLogjik tabele = new clsTabeleEmerLogjik();
                    //tabele.mbushTabeleEmerLogjik(rreshti);
                    Add(new clsTabeleEmerLogjik(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushTabelatEmerLogjik(DataTable dt)", true)]
        public colTabelatEmerLogjik mbushArrayListTabelat(DataSet ds)
        {
            colTabelatEmerLogjik col = new colTabelatEmerLogjik();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTabeleEmerLogjik tabele = new clsTabeleEmerLogjik();

                tabele.IdTabele = int.Parse(rreshti[0].ToString());
                tabele.NrTabele = int.Parse(rreshti[1].ToString());
                tabele.EmerRealTabele = rreshti[2].ToString();
                tabele.EmerLogjikTabele = rreshti[3].ToString();
                col.Add(tabele);
            }
            return col;
        }
    }
}