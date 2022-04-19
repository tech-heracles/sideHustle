using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colFiltraGIS : System.Collections.Generic.List<clsFiltraGIS>
    {

        #region Konstruktoret

        public colFiltraGIS()
        {

        }

        public colFiltraGIS(string layeri, int idnderm)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushFiltratGrida(data.ktheGjitheFiltratGISSipasLayerit(layeri, idnderm));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsFiltraGIS this[int index]
        {
            get { return ((clsFiltraGIS)base[index]); }
        }

   
        #endregion

        #region Metoda Private

        private bool mbushFiltratGrida(DataTable dt)
        {
           
                foreach (DataRow rreshti in dt.Rows)
                {
                 
                    Add(new clsFiltraGIS(rreshti));
                }

            return true;
        }

        #endregion


        public static string MerrFilterExpressionSipasID(int id)
        {
            return new clsDatabaseAdmin().ktheFilterPerGISSipasId(id);
        }
    }
}
