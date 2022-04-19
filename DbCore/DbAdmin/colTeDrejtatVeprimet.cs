using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colTeDrejtatVeprimet : System.Collections.Generic.List<clsTeDrejtaVeprim>
    {
        #region Metoda Publike

        public new clsTeDrejtaVeprim this[int index]
        {
            get { return ((clsTeDrejtaVeprim)base[index]); }
        }

        public bool shtoVeprim(clsTeDrejtaVeprim veprimi)
        {
            base.Add(veprimi);
            if (base.Contains(veprimi))
                return true;
            else return false;
        }

        public bool fshiVeprim(clsTeDrejtaVeprim veprimi)
        {
            base.Remove(veprimi);
            if (base.Contains(veprimi))
                return false;
            else return true;
        }

        public bool fshiGjitheVeprimet()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteVeprim(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoVeprimNeIndeksin(int index, clsTeDrejtaVeprim veprimi)
        {
            base.Insert(index, veprimi);
        }

        public int indeksiVeprimit(clsTeDrejtaVeprim veprimi)
        {
            return base.IndexOf(veprimi);
        }

        public bool ekzistonVeprimi(clsTeDrejtaVeprim veprimi)
        {
            if (base.Contains(veprimi))
                return true;
            else return false;
        }

        public int numriVeprimeve()
        {
            return base.Count;
        }

        public bool mbushGjitheTeDrejtatVeprimet()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushVeprimet(data.ktheGjitheTeDrejtatVeprimet());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushVeprimet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTeDrejtaVeprim veprimi = new clsTeDrejtaVeprim();
                    //veprimi.mbushTeDrejtaVeprim(rreshti);
                    Add(new clsTeDrejtaVeprim(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushVeprimet(DataTable dt)", true)]
        public colTeDrejtatVeprimet mbushArrayListVeprimet(DataSet ds)
        {
            colTeDrejtatVeprimet veprimet = new colTeDrejtatVeprimet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTeDrejtaVeprim veprimi = new clsTeDrejtaVeprim();

                veprimi.IdDrejtaVeprim = int.Parse(rreshti[0].ToString());
                veprimi.KodiDrejtaVeprim = rreshti[1].ToString();
                veprimi.PershkrimiDrejtaVeprim = rreshti[2].ToString();

                veprimet.Add(veprimi);
            }

            return veprimet;
        }
    }
}
