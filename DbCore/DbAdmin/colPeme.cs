using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colPeme : System.Collections.Generic.List<clsPeme>
    {
        #region Metoda Publike

        public new clsPeme this[int index]
        {
            get { return ((clsPeme)base[index]); }
        }

        public bool mbushPemen()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPemet(data.kthePemen());
            data.Dispose();
            return sukses;
        }

        public bool mbushPemenEPerdoruesve()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPemet(data.kthePemenEPerdoruesve());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushPemet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPeme peme = new clsPeme();
                    //peme.mbushPeme(rreshti);
                    Add(new clsPeme(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushPemet(DataTable dt)", true)]
        public colPeme mbushArrayListPeme(DataSet ds)
        {
            colPeme modulet = new colPeme();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsPeme peme = new clsPeme();

                peme.IdAti = int.Parse(rreshti[2].ToString());
                peme.IdBiri = int.Parse(rreshti[0].ToString());
                peme.EmriDege = rreshti[1].ToString();
                peme.TePlota = false;
                peme.Lexim = false;
                peme.Modifikim = false;
                peme.Fshirje = false;
                modulet.Add(peme);
            }
            return modulet;
        }
   
    }
}
