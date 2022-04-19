using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbListPagesat
{
    public class colTrupiKonfigListOrari : System.Collections.Generic.List<clsTrupiKonfigListOrari>
    {
        #region Konstruktoret

        public colTrupiKonfigListOrari()
        {
        }
        public colTrupiKonfigListOrari(int idkoka)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushTrupiKonfigListOrari(db.ktheTrupiKonfigListOrariSipasIdKoka(idkoka));
            db.Dispose();
        }
        #endregion

        #region Metoda Publike

        public new clsTrupiKonfigListOrari this[int index]
        {
            get { return ((clsTrupiKonfigListOrari)base[index]); }
        }


        public static DataTable ktheTrupiKonfigListOrariSipasDites(int idndermarje, string dita)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.ktheTrupiKonfigListOrariSipasDites(idndermarje, dita);
            }
        }
        #endregion

        #region Metoda Private

        private bool mbushTrupiKonfigListOrari(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiKonfigListOrari konvertim = new clsTrupiKonfigListOrari();
                    //konvertim.mbushTrupiKonfigListOrari(rreshti);
                    Add(new clsTrupiKonfigListOrari(rreshti));
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
