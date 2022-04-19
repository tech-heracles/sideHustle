using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colGrupimeKolone_PivotGrid : List<clsGrupimeKolone_PivotGrid>
    {
        #region Metoda Publike 
        
        /// <summary>
        /// Konstruktori Default i klases
        /// </summary>
        public colGrupimeKolone_PivotGrid()
        {

        }

        /// <summary>
        /// Kthen te gjitha grupimet e kolonave te pivot grides sipas modulit qe i kalohet si parameter
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idModuli"></param>
        /// <returns></returns>
        public static colGrupimeKolone_PivotGrid ktheGrupimetKolonaveSipasIdModulit(int idGjuha, int idModuli)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            colGrupimeKolone_PivotGrid grupimet = new colGrupimeKolone_PivotGrid();
            grupimet.mbushColGrupimKolone(dbRegj.merrGrupimeKolonashSipasModulit(idGjuha, idModuli));
            dbRegj.Dispose();
            return grupimet;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Mbush te coleksionin e objekteve clsGrupimeKolone_PivotGrid me te dhenat e datatable qe merr si parameter
        /// </summary>
        /// <param name="grupimKoloneDataTable"></param>
        /// <returns></returns>
        private bool mbushColGrupimKolone(DataTable grupimKoloneDataTable)
        {
            //try
            //{
                foreach (DataRow row in grupimKoloneDataTable.Rows)
                {
                    //clsGrupimeKolone_PivotGrid grupimi = new clsGrupimeKolone_PivotGrid();
                    //grupimi.mbushGrupimKolone(row);
                    Add(new clsGrupimeKolone_PivotGrid(row));
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
