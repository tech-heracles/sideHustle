using DbCore.IMBUtils.Messages;
using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    public class clsTrupiPolitikeKarta
    {
        #region Properties

        public int IdKategoria { get; set; }

        public string Kategoria { get; set; }

        public int IdPolitike { get; set; }

        public double Pike { get; set; }

        #endregion

        #region Konstruktoret

        public clsTrupiPolitikeKarta(int idPolitike, string kategoria, double pike)
        {
            IdPolitike = IdPolitike;
            Kategoria = kategoria;
            Pike = pike;
        }

        public clsTrupiPolitikeKarta(DataRow rreshti)
        {
            Mbush(rreshti);
        }

        #endregion
        
        #region Metoda Internal

        internal void Ruaj(clsDatabaseRegjistrim dbRegj)
        {
            IdKategoria = dbRegj.RuajTrupiPolitike(Kategoria, Pike, IdPolitike);
        }

        internal static void FshiTrupiPolitikeKartaSipasIdPolitike(clsDatabaseRegjistrim dbRegj, int idPolitike)
        {
            dbRegj.FshiTrupiPolitikeKartaSipasIdPolitike(idPolitike);
        }

        /// <summary>
        /// Metode per mbushjen e trupit te politikave te kartave.
        /// </summary>
        /// <param name="dataRow">Si parameter merr nje DataRow.</param>
        internal void Mbush(DataRow dataRow)
        {
            if (dataRow == null)
                return;
            try
            {
                IdKategoria = !IsDBNull(dataRow["IDKATEGORIA"])
                    ? ToInt32(dataRow["IDKATEGORIA"])
                    : 0;
                IdPolitike = !IsDBNull(dataRow["IDPOLITIKE"])
                    ? ToInt32(dataRow["IDPOLITIKE"])
                    : 0;
                Kategoria = dataRow["KATEGORIA"].ToString();
                Pike = !IsDBNull(dataRow["PIKE"])
                    ? ToDouble(dataRow["PIKE"])
                    : 0;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se automjetit nga db-ja");
            }
        }

        #endregion
    }
}
