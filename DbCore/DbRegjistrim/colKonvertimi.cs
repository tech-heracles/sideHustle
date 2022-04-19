using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colKonvertimi: System.Collections.Generic.List<clsKonvertimi >
    {
        #region Konstruktoret

        public colKonvertimi()
        {
        }
        public colKonvertimi(int iddokpaskonvertimi)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKonvertim(db.ktheDokEKonvertuarSipasIdDokPasKonvertimi(iddokpaskonvertimi));
            db.Dispose();
        }
        #endregion

        #region Metoda Publike

        public new clsKonvertimi this[int index]
        {
            get { return ((clsKonvertimi)base[index]); }
        }

    

        #endregion

        #region Metoda Private

        private bool mbushKonvertim(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKonvertimi konvertim = new clsKonvertimi();
                    //konvertim.mbushKonvertim(rreshti);
                    Add(new clsKonvertimi(rreshti));
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

