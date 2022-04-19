using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colKonfigPivotGridaTrupi : System.Collections.Generic.List<clsKonfigPivotGridaTrupi>
    {
        #region Metoda Publike

        public new clsKonfigPivotGridaTrupi this[int index]
        {
            get { return ((clsKonfigPivotGridaTrupi)base[index]); }
        }


        public static colKonfigPivotGridaTrupi ktheKonfigPGSipasIDKoka(int idGjuha, int idKonfigPGKoka)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            colKonfigPivotGridaTrupi colKonfigPGTrupi = new colKonfigPivotGridaTrupi();
            colKonfigPGTrupi.mbushColKonfigPGTrupi(dbRegj.merrKonfigPGTrupiSipasIdKoka(idGjuha, idKonfigPGKoka));
            dbRegj.Dispose();
            return colKonfigPGTrupi;
        }

        public static colKonfigPivotGridaTrupi ktheKonfigShto(int idGjuha, int idModuli)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            colKonfigPivotGridaTrupi colKonfigTrupi = new colKonfigPivotGridaTrupi();
            colKonfigTrupi.mbushColKonfigPGTrupi(dbRegj.merrKonfigDefaultShtim(idGjuha, idModuli));
            dbRegj.Dispose();
            return colKonfigTrupi;
        }

        /// <summary>
        /// Modifikon te dhenat e trupave te nje konfigurimi te caktuar raporti
        /// </summary>
        /// <param name="idTrupi">id e trupit te konfigurimit qe do modifikohet</param>
        /// <param name="visibility"></param>
        /// <param name="zona"></param>
        /// <param name="rendi"></param>
        /// <param name="width"></param>
        public void modifikoTrupKonfigSipasIDKolona(int idKolona, bool visibility, string zona, int rendi, string width,int llojGrupimi)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].IdKolonaPG == idKolona)
                {
                    this[i].Visibility = visibility;
                    this[i].Zona = zona;
                    this[i].Rendi = rendi;
                    this[i].LlojGrupimi = llojGrupimi;
                    if (!width.Equals(String.Empty))
                        this[i].Width = Convert.ToInt32(width);
                }
            }
        }

        #endregion

        #region Metoda Private

        private bool mbushColKonfigPGTrupi(DataTable konfigPGTrupiDataTable)
        {
            //try
            //{
                foreach (DataRow row in konfigPGTrupiDataTable.Rows)
                {
                    //clsKonfigPivotGridaTrupi konfigTrupi = new clsKonfigPivotGridaTrupi();
                    //konfigTrupi.mbushKonfigPGTrupi(row);
                    Add(new clsKonfigPivotGridaTrupi(row));
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