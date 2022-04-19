using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
      
    public class colTheme : System.Collections.Generic.List<clsTheme>
    {
        #region Metoda Publike

        public new clsTheme this[int index]
        {
            get { return ((clsTheme)base[index]); }
        }

        public bool mbushGjitheTheme()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushThemat(data.merrGjitheTheme());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushThemat(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTheme theme = new clsTheme();
                    //theme.mbushTheme(rreshti);
                    Add(new clsTheme(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushThemat(DataTable dt)", true)]
        public colTheme mbushArrayListTheme(DataSet ds)
        {
            colTheme themes = new colTheme();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTheme theme = new clsTheme();
                theme.IdTheme = int.Parse(rreshti[0].ToString());
                theme.EmriTheme = rreshti[1].ToString();
                theme.PathTheme = rreshti[2].ToString();
                themes.Add(theme);
            }
            return themes;
        }
    }
}
