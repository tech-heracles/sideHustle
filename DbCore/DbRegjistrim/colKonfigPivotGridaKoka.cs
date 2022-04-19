using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colKonfigPivotGridaKoka : System.Collections.Generic.List<clsKonfigPivotGridaKoka>
    {
        #region Metoda Publike

        public new clsKonfigPivotGridaKoka this[int index]
        {
            get { return ((clsKonfigPivotGridaKoka)base[index]); }
        }

        public static colKonfigPivotGridaKoka ktheKonfigPGNdermarrjes(int idGjuha, int idNdermarja, int idModuli)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            colKonfigPivotGridaKoka konfigPG = new colKonfigPivotGridaKoka();
            konfigPG.mbushColKonfigPGKoka(idGjuha, dbRegj.merrKonfigPGKokaSipasNdermarrjeDheModulit(idNdermarja, idModuli));
            dbRegj.Dispose();
            return konfigPG;
        }


        #endregion


        #region Metoda Private

        private bool mbushColKonfigPGKoka(int idGjuha, DataTable konfigPGDataTable)
        {
            //try
            //{
                foreach (DataRow row in konfigPGDataTable.Rows)
                {
                    //clsKonfigPivotGridaKoka konfKoka = new clsKonfigPivotGridaKoka();
                    //konfKoka.mbushKonfigPivotGridaKoka(idGjuha, row);
                    Add(new clsKonfigPivotGridaKoka(idGjuha, row));
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
