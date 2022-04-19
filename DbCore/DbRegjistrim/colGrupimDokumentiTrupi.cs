using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colGrupimDokumentiTrupi: System.Collections.Generic.List<clsGrupimDokumentiTrupi >
    {
        #region Konstruktoret

        public colGrupimDokumentiTrupi()
        {
        }
        public colGrupimDokumentiTrupi(int idkoka)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushGrupimTrupi(db.ktheGrupimTrupiSipasIdKoka(idkoka));
            db.Dispose();
        }
        #endregion

        #region Metoda Publike

        public new clsGrupimDokumentiTrupi this[int index]
        {
            get { return ((clsGrupimDokumentiTrupi)base[index]); }
        }

    

        #endregion

        #region Metoda Private

        private bool mbushGrupimTrupi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupimDokumentiTrupi konvertim = new clsGrupimDokumentiTrupi();
                    //konvertim.mbushGrupimTrupi(rreshti);
                    Add(new clsGrupimDokumentiTrupi(rreshti));
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
