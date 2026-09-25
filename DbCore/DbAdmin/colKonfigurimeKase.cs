using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colKonfigurimeKase : List<clsKonfigurimKase>
    {
        #region Konstruktoret

        public colKonfigurimeKase()
        {
        }
        #endregion
        public new clsKonfigurimKase this[int index]
        {
            get { return ((clsKonfigurimKase)base[index]); }
        }

        #region Metoda Publike
        public static DataTable merrKonfigurimetSipasNdermarjesDT(int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKonfigurimKasashSipasNdermarjesDT(idNdermarrje);
            }
        }

        public static DataTable merrKonfigurimetSipasNdermarjesMeUrl(int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKonfigurimKasashSipasNdermarjesMeUrl(idNdermarrje);
            }
        }

        public static DataRow merrKonfigurimetSipasNdermarjesDR(int IdKonfigurimi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKonfigurimKasashSipasNdermarjesDR(IdKonfigurimi);
            }
        }

        #endregion

        #region metoda private

        #endregion
    }
}