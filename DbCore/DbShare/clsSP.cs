using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbShare
{
    public class clsSP
    {
        #region Ctor
        
        /// <summary>
        /// Mbush klasen me te dhenat nga db-ja per idSp-ne te dhene ne input
        /// </summary>
        /// <param name="idSp">id-ja e dhene ne input</param>
        public clsSP(int idSp)
        {
            using (var data = new clsDatabaseShare())
                Mbush(data.merrSP(idSp));
        }

        #endregion

        #region Properties

        public int IdSp { get; set; }

        public string SpEmri { get; set; }

        #endregion

        #region Metoda Publike

        public static string GetStoredProcedureName(int idSp)
        {
            using (var data = new clsDatabaseShare())
            {
                return data.merrEmerSP(idSp);
            }
        }

        #endregion 

        #region Metoda Private 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rreshti"></param>
        /// <returns></returns>
        private void Mbush(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    IdSp = !IsDBNull(rreshti["IDSP"])
                        ? ToInt32(rreshti["IDSP"])
                        : 0;
                    SpEmri = rreshti["SPEMRI"].ToString();
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se parametrave nga db-ja");
                }
            }
        }

        #endregion
    }
}
