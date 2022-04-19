using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colModeletFushaShtese : System.Collections.Generic.List<clsModeliFushaShtese>
    {
  
        #region Konstruktoret

        public colModeletFushaShtese()
        {
        }

        public colModeletFushaShtese(int idndermarje, int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushModeleFushashShtese(data.ktheGjitheModeletFushaShtese(idndermarje, idperdorues));
            data.Dispose();
        }

        public colModeletFushaShtese(int idlloj, int idndermarje, int idperdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                mbushModeleFushashShtese(data.ktheGjitheModeletFushaShteseSipasLlojit(idlloj, idndermarje, idperdorues));
  
        } 
        public colModeletFushaShtese(int idlloj, int idndermarje, int idperdorues, clsDatabaseAdmin data)
        {
            mbushModeleFushashShtese(data.ktheGjitheModeletFushaShteseSipasLlojit(idlloj, idndermarje, idperdorues));
        }

        public colModeletFushaShtese(string kodLlojModeliFushaShtese, int idNdermarrje, int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                mbushModeleFushashShtese(data.ktheGjitheModeletFushaShteseSipasLlojit(kodLlojModeliFushaShtese, idNdermarrje, idPerdoruesi));
        }

        #endregion

        #region Metoda Publike

        public new clsModeliFushaShtese this[int index]
        {
            get { return ((clsModeliFushaShtese)base[index]); }
        }
        public static DataRow merrModeletFushaShteseDR(int idmodeli)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataRow rreshti = dbartikuj.merrModeletFushaShteseDR(idmodeli);
            dbartikuj.Dispose();
            return rreshti;
        }

        public static DataTable ktheGjitheModeletFushaShteseDT(int idnderm, int idperdorues)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.ktheGjitheModeletFushaShteseDT(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }
        #endregion

        #region Metoda Private

        private bool mbushModeleFushashShtese(DataTable dt)
        {       
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsModeliFushaShtese(rreshti));
                }
            return true;
        }

        #endregion
      

    }
}