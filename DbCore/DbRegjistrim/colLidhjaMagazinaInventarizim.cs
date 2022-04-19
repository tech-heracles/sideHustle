using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
  public  class colLidhjaMagazinaInventarizim: System.Collections.Generic.List<clsLidhjaMagazinaInventarizim >
    {
        #region Konstruktoret

        public colLidhjaMagazinaInventarizim()
        {
        }
        public colLidhjaMagazinaInventarizim(int iddokpaskonvertimi)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushLidhje(db.ktheSipasMagazines(iddokpaskonvertimi));
            db.Dispose();
        }
        #endregion

        #region Metoda Publike

        public new clsLidhjaMagazinaInventarizim this[int index]
        {
            get { return ((clsLidhjaMagazinaInventarizim)base[index]); }
        }

    

        #endregion

        #region Metoda Private

        private bool mbushLidhje(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKonvertimi konvertim = new clsKonvertimi();
                    //konvertim.mbushKonvertim(rreshti);
                    Add(new clsLidhjaMagazinaInventarizim(rreshti));
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
