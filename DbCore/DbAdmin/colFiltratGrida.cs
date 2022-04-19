using System.Collections.Generic;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colFiltratGrida : List<clsFiltraGrida>
    {

        #region Konstruktoret

        public colFiltratGrida()
        {

        }

        public colFiltratGrida(int gridakoka, int idnderm)
        {
            using (var data = new clsDatabaseAdmin())
                MbushFiltratGrida(data.ktheGjitheFiltratGridaByGridaKoka(gridakoka, idnderm));
        }

        /// <summary>
        /// metode per marrjen e filtrave sipas konfigurimit
        /// </summary>
        /// <param name="idkonfigurim">id konfigurim</param>
        /// <param name="idnderm">id ndermarje</param>
        /// <returns> kthen nje coleksion me filtrat e grides te ketij konfigurimi</returns>
        public colFiltratGrida merrFiltraSipasKonfigurimit(int idkonfigurim, int idnderm)
        {
            using (var data = new clsDatabaseAdmin())
                MbushFiltratGrida(data.ktheGjitheFiltratGridaByKonfigurimi(idkonfigurim, idnderm));

            return this;
        }

        #endregion

        #region Metoda Publike

        public bool shtoFiltraGridaNgaKlonimi(int idKonfigurimPrind, int idGridaKokaRe)
        {
            using (var data = new clsDatabaseAdmin())
                return data.ruajGjitheFiltratGridaByKonfigurimiNgaKlonimi(idKonfigurimPrind, idGridaKokaRe);
        }

        public new clsFiltraGrida this[int index] => base[index];
        
        #endregion

        #region Metoda Private

        private void MbushFiltratGrida(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsFiltraGrida(rreshti));
            }
        }

        #endregion
    }
}