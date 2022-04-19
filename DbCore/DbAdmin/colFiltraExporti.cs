using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colFiltraExporti : System.Collections.Generic.List<clsFiltraExporti>
    {

        #region Konstruktoret

        public colFiltraExporti()
        {

        }

        public colFiltraExporti(int formati, int idnderm)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushFiltratGrida(data.ktheGjitheFiltratExportiSipasFormatit(formati, idnderm));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsFiltraExporti this[int index]
        {
            get { return ((clsFiltraExporti)base[index]); }
        }

   
        #endregion

        #region Metoda Private

        private bool mbushFiltratGrida(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFiltraExporti filtri = new clsFiltraExporti();
                    //filtri.mbushFilterGrid(rreshti);
                    Add(new clsFiltraExporti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
   
    }
}
