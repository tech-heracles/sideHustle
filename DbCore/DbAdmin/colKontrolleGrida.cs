using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colKontrolleGrida : System.Collections.Generic.List<clsKontrolleGrida>
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public colKontrolleGrida()
        { 
            
        }

        #endregion

        #region Metoda Publike

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idKomponente"></param>
        /// <returns></returns>
        public bool merrKontrolletKomponentes(int idkatdok)
        {
            clsDatabaseAdmin shareDb = new clsDatabaseAdmin();
            bool mbush = mbushKontrollet(shareDb.merrKontrolletSipasKategorise(idkatdok));
            shareDb.Dispose();
            return mbush;
        }


        #endregion

        #region Metoda Private

        private bool mbushKontrollet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKontrolleGrida kontrolli = new clsKontrolleGrida();
                    //kontrolli.mbushKontrollin(rreshti);
                    this.Add(new clsKontrolleGrida(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
       
    }
}
