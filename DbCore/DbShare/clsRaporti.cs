using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using static System.Convert;

namespace DbCore.DbShare
{
    public class clsRaporti
    {
        #region Konstruktoret

        public clsRaporti() { }

        public clsRaporti(int idGjuha, int idRaporti)
        {
            using (var sharedb = new clsDatabaseShare())
                Mbush(idGjuha, sharedb.merrRaport(idRaporti));
        }

        public clsRaporti(int idGjuha, string emriReal)
        {
            using (var sharedb = new clsDatabaseShare())
                Mbush(idGjuha, sharedb.merrRaportSipasEmritReal(emriReal));
        }

        public clsRaporti(int idGjuha, DataRow rreshti)
        {
            Mbush(idGjuha, rreshti);
        }

        #endregion

        #region Properties

        public int IdRaporti { get; set; }

        public string RaportiEmri { get; set; }

        public string RaportiEmriReal { get; set; }

        public int IdModul { get; set; }

        public bool RapDefault { get; set; }

        public int IdSp { get; set; }

        public bool Visible { get; set; }

        public bool ExportPerTatime { get; set; }

        public bool StilEnabled { get; set; }

        public bool PageByPage { get; set; }

        public string EmerSp { get; set; }

        public colParameter ParametraSp { get; set; }

        #endregion

        #region Metoda Publike
        
        public SqlParameter[] CreateParameters(int idNdermarrje, int idFatura, int idDesign, DateTime dateDergimi)
        {
            var storedProcedureParameters = new colParameter(IdSp);
            SqlParameter[] sqlParameters;
            if (storedProcedureParameters.Count > 0)
            {
                sqlParameters = new SqlParameter[storedProcedureParameters.Count];
                for (int i = 0; i < storedProcedureParameters.Count; i++)
                {
                    //vlera do i jepet oSp.OColSpTrupi[i].Vlera qe mban vleren qe do marri parametri qe do i kalohet SP-se
                    string vlera;

                    //************************************************************************************//
                    //behet kontrolli nqs nuk eshte plotesuar filtri atehere do kalohet si bosh, pa vlere 
                    switch (storedProcedureParameters[i].Emri.ToLower())
                    {
                        case "idfatura":
                        case "idfletemagazina":
                        case "idkokaarkabanka":
                            vlera = Convert.ToString(idFatura);
                            break;
                        case "idraportdesign":
                            vlera = Convert.ToString(idDesign);
                            break;
                        case "filterdtdokdergimi":
                            vlera = dateDergimi.ToString(CultureInfo.InvariantCulture);
                            break;
                        case "test":
                            vlera = bool.FalseString;
                            break;
                        case "idndermarje":
                            vlera = Convert.ToString(idNdermarrje);
                            break;
                        case "ngahistoriku":
                            vlera = bool.FalseString;
                            break;
                        default:
                            vlera = "";
                            break;
                    }

                    sqlParameters[i] = new SqlParameter
                    {
                        ParameterName = storedProcedureParameters[i].Emri,
                        Value = vlera
                    };
                }
            }
            else
                sqlParameters = new SqlParameter[0];

            return sqlParameters;
        }

        public SqlParameter[] CreateParameters(Dictionary<string, object> parameters)
        {
            var storedProcedureParameters = new colParameter(IdSp);
            SqlParameter[] sqlParameters;
            if (storedProcedureParameters.Count > 0)
            {
                sqlParameters = new SqlParameter[storedProcedureParameters.Count];
                for (var i = 0; i < storedProcedureParameters.Count; i++)
                {
                    sqlParameters[i] = new SqlParameter
                    {
                        ParameterName = storedProcedureParameters[i].Emri,
                    };

                    if (parameters.TryGetValue(storedProcedureParameters[i].Emri, out var vlera))
                    {
                        sqlParameters[i].Value = string.IsNullOrEmpty(storedProcedureParameters[i].KolonaDb)
                            ? vlera
                            : $" {storedProcedureParameters[i].KolonaDb} = '{vlera}' AND ";
                    }
                    else
                        sqlParameters[i].Value = string.Empty;
                }
            }
            else
                sqlParameters = new SqlParameter[0];

            return sqlParameters;
        }

        public static int KtheIdRaporti(int idGjuha, int idRaportDesign)
        {
            using (var dbshare = new clsDatabaseShare())
                return dbshare.merrIdRaportSipasDesign(idRaportDesign);
        }

        public static int KtheIdSubRaporti(int idSuperRaporti)
        {
            using (var dbshare = new clsDatabaseShare())
            {
                return dbshare.ktheIdSubRaporti(idSuperRaporti);
            }
        }

        public static int KtheIdRaportiSipasEmritReal(string emriReal)
        {
            using (var dbshare = new clsDatabaseShare())
            {
                return dbshare.merrIdRaportSipasEmerReal(emriReal);
            }
        }

        public static int KtheIdRaportPivotGridSipasIdModuli(int idModulPivotGrid)
        {
            using (var dbshare = new clsDatabaseShare())
                return dbshare.merrIdRaportPivotGridSipasIdModuli(idModulPivotGrid);
        }

        public static DataTable KtheListRaportesh(int idNdermarrje, int idPerdoruesi, int idGjuha, int idViti, int idModuli)
        {
            using (var dbshare = new clsDatabaseShare())
            {
                return dbshare.ktheListRaportesh(idNdermarrje, idPerdoruesi, idGjuha, idViti, idModuli);
            }
        }

        public static string KtheListKonfigRaportesh(int idNdermarrje, int idPerdoruesi, int idModuli)
        {
            using (var dbshare = new clsDatabaseShare())
            {
                return dbshare.ktheListKonfigRaportesh(idNdermarrje, idPerdoruesi, idModuli);
            }
        }

        public static clsMesazh RuajKonfigListRaportesh(int idPerdoruesi, int idNdermarrje, int idModuli, string konfigurimi)
        {
            using (var dbshare = new clsDatabaseShare())
            {
                return dbshare.ruajKonfigListRaportesh(idPerdoruesi, idNdermarrje, idModuli, konfigurimi);
            }
        }

        public void MbushParametra()
        {
            ParametraSp = new colParameter(IdSp);
            EmerSp = clsSP.GetStoredProcedureName(IdSp);
        }

        /// <summary>
        /// Kthen nese ka subraporte raporti me id=idRap.
        /// </summary>
        /// <returns>True nese ka, false ne te kundert.</returns>
        /// <param name="idRap">Id e raportit</param>
        public static bool KaSubRaporte(int idRap)
        {
            using (var db = new clsDatabaseShare())
                return db.kaSubRaporte(idRap);
        }

        /// <summary>
        /// Kthen nje raport financiar ka vetem format buxhetor apo jo
        /// </summary>
        /// <returns>True nese ka format jobuxhetor, false ne te kundert.</returns>
        /// <param name="idRap">Id e raportit</param>
        ///   /// <param name="idNder">Id e ndermarjes</param>
        public static bool KaFormatJoBuxhetor(int idRap, int idNder)
        {
            using (var db = new clsDatabaseShare())
                return db.kaFormatJoBuxhetor(idRap, idNder);
        }

        public static DataTable merrSipasKategorisePerMobile(int idkategori, int idNderm)
        {
            using (DbCore.DbShare.clsDatabaseShare db = new clsDatabaseShare())
            {
                return db.merrRaportDesignSipasKategorisePerMobile(idkategori, idNderm);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush objektin e raportit nga rreshti
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="rreshti">rreshti ne input</param>
        internal void Mbush(int idGjuha, DataRow rreshti)
        {
            if (rreshti == null) return;
            string kodGjuhe = MessagesResource.KtheKodGjuhe(idGjuha);
            RaportiEmri = rreshti["RAPEMRI_" + kodGjuhe].ToString();
            IdRaporti = !IsDBNull(rreshti["IDRAPORTI"])
                ? ToInt32(rreshti["IDRAPORTI"])
                : 0;
            RaportiEmriReal = rreshti["RAPEMRIREAL"].ToString();
            IdModul = !IsDBNull(rreshti["IDMODULI"])
                ? ToInt32(rreshti["IDMODULI"])
                : 0;
            RapDefault = !IsDBNull(rreshti["RAPDEFAULT"]) && ToBoolean(rreshti["RAPDEFAULT"]);
            IdSp = !IsDBNull(rreshti["IDSP"])
                ? ToInt32(rreshti["IDSP"])
                : 0;
            Visible = !IsDBNull(rreshti["RAPVISIBLE"]) && ToBoolean(rreshti["RAPVISIBLE"]);
            StilEnabled = !IsDBNull(rreshti["STIL_ENABLED"]) && ToBoolean(rreshti["STIL_ENABLED"]);
            PageByPage = !IsDBNull(rreshti["PAGE_BY_PAGE"]) && ToBoolean(rreshti["PAGE_BY_PAGE"]);
            ExportPerTatime = !IsDBNull(rreshti["EXPORT_PER_TATIME"]) && ToBoolean(rreshti["EXPORT_PER_TATIME"]);
        }

        #endregion
    }
}
