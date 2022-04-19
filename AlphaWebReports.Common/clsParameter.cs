using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbShare
{
    public class clsParameter
    {
        #region Properties

        public int IdParametri { get; set; }

        public string Emri { get; set; }

        public string Pershkrimi { get; set; }

        public string KolonaDb { get; set; }

        public string Vlera { get; set; }

        public int IdGrupKontroll { get; set; }

        #endregion

        #region Ctor

        public clsParameter()
        {

        }

        public clsParameter(DataRow row)
        {
            Mbush(row);
        } 

        #endregion

        #region Metoda Private

        private void Mbush(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    IdParametri = !IsDBNull(rreshti["IDPARAMETRI"])
                        ? ToInt32(rreshti["IDPARAMETRI"])
                        : 0;
                    Emri = rreshti["SPPARAMETEREMRI"].ToString();
                    Pershkrimi = rreshti["SPPARAMETERPERSHK"].ToString();
                    KolonaDb = rreshti["SPCOLDBEMERLOGJIK"].ToString();
                    IdGrupKontroll = !IsDBNull(rreshti["IDGRUPKONTROLL"])
                        ? ToInt32(rreshti["IDGRUPKONTROLL"])
                        : 0;
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
