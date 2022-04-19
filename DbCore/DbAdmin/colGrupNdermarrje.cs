using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colGrupNdermarje : System.Collections.Generic.List<clsGrupNdermarrje>
    {
        #region Konstruktoret

        public colGrupNdermarje()
        {

        }

        public colGrupNdermarje(int idlicenca)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushGrupeNdermarrje(data.ktheGjitheGrupetNdermarrjeSipasLicences(idlicenca));
        }

       

        #endregion

        #region Metoda Publike

        public new clsGrupNdermarrje this[int index]
        {
            get { return ((clsGrupNdermarrje)base[index]); }
        }

      


        #endregion

        #region Metoda Private

        private bool mbushGrupeNdermarrje(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupNdermarrje grupNdermarrje = new clsGrupNdermarrje();
                    //grupNdermarrje.mbushGrupNdermarrje(rreshti);
                    Add(new clsGrupNdermarrje(rreshti));
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