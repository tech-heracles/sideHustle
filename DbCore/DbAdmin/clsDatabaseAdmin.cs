using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Resources;
using System.Text;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.DbImporte;
using DbCore.DbListPagesat;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using IDataBaseReader = AlphaWeb.Core.Interfaces.Data.IDataBaseReader;

namespace DbCore.DbAdmin
{
    /// <remarks>
    ///  Kjo eshte klasa ma e rendesishme e ketij moduli. Ka nje klase te tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten nga te gjitha objektet brenda projektit DbCore.DbAdmin.
    ///  Secila metode permban thirrjet e Stored procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </remarks>

    public partial class clsDatabaseAdmin : DbData
    {
        public clsDatabaseAdmin() : base()
        {

        }
        public clsDatabaseAdmin(string connectionName) : base(connectionName)
        {

        }

        public clsDatabaseAdmin(DbData db) : base(db) { }

        #region konfigurime serveri 
        public DataRow ktheTeDhenaHelpServeri()
        {


            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SERVERSETTINGS_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Kthe versionin e fundit te programit
        /// </summary>
        /// <returns></returns>



        internal object LexoKonfigurimSipasKeyNgaDb(string key)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@key", key);
            return dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SERVER_CONFIGURATION_selSipasKey");
        }



        internal Dictionary<string, object> LexoGjitheKonfigurimetNgaDb()
        {
            dbManager.Open();
            return dbManager.GetDictionary("prc_T_SERVER_CONFIGURATION_selAll");
        }
        #endregion
        public bool eshteFormatLidhurMeSQL(int idFormati)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idFormati, ParameterDirection.Input);
            int numerLidhur = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_eshteFormatLidhurMeSQL"));
            if (numerLidhur > 0)
                return true;
            else
                return false;

        }
        internal List<string> MerrGjitheKomponentet()
        {
            dbManager.Open();
            return dbManager.GetList<string>("prc_T_KOMPONENTE_all");
        }

        internal DataTable ktheDataTable(string dtName, string querystring)
        {
            dbManager.Open();
            DataTable dt = new DataTable(dtName);
            SqlDataAdapter dAD = new SqlDataAdapter(querystring, dbManager.ConnectionString);
            dAD.Fill(dt);
            return dt;

        }

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsPerdorues dhe colPerdorues
        /// </summary>
        #region PERDORUESIT

        /// <summary>
        /// ekzekuton prc_T_PERDORUESI_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMEsazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="info"></param>
        internal clsMesazh ruajPerdorues(out int idperdorues, int idqyteti, int idgjuha, String emriperdorues, String mbiemriperdorues, bool perdoruesaktiv, String perdoruesusername, String perdoruespassword,
            String perdoruestel, String perdoruesfax, String perdoruesemail, String perdoruesadresa, int idperdoruesi, int idStilRaporti, int idstatusdok, float zoomFactor, bool info, int idambjent,
            string KerkeseResetPass, bool PassIPerkohshem, bool kycur, DateTime dateKrijimiPassword, bool kontrolloPasswordin, int exportFormat, int exportMode, bool shfaqdtprintimi, bool KycurMobile, bool shfaqPerdoruesMenu, bool shfaqMesazhePopup, int idambjentmobile, String shopcode, String shopname, String dealername, String usericrm, String userEtopuP, String iDEtopUp, String typeofDevice, String salesrepMobileNumber, String salesrepMPesaMSISDN, int gjini, DateTime salesrepStartDateVod, DateTime salesrepTrainingStart, DateTime salesrepStartDateShop, DateTime salesrepMaternityLeaveStart, DateTime leavedateVod, DateTime leavedateShop, DateTime maternityleaveEndDate, DateTime trainingendDate, string commentsretailSales, string accountexecutive, string idNumber, int isinsured, string commentsretailOpSpecialist, string regionalsupervisor, string retailsalesAccountExecutive, string retailsalesAreaManager, DateTime birthDate, string sitecode, string districti, string shopmainCode, string latitude, string longitude, int status, int leaveReason, int uniform, string shenime, int statusAprovimi, bool njoftimEmailAprovim, int idkonfigkase)
        {
            idperdorues = -1;

            dbManager.Open();
            dbManager.CreateParameters(66);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDGJUHA", idgjuha, ParameterDirection.Input);
            if (idqyteti == 0 || idqyteti == -1) dbManager.AddParameters(2, "@IDQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDQYTETI", idqyteti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERDORUESEMRI", emriperdorues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERDORUESMBIEMRI", mbiemriperdorues, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERDORUESAKTIV", perdoruesaktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERDORUESUSERNAME", perdoruesusername, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERDORUESPASSWORD", perdoruespassword, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERDORUESTEL", perdoruestel, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PERDORUESFAX", perdoruesfax, ParameterDirection.Input);
            dbManager.AddParameters(10, "@PERDORUESEMAIL", perdoruesemail, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PERDORUESADRESA", perdoruesadresa, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTILRAPORTI", idStilRaporti, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(15, "@ZOOMFACTOR", zoomFactor, ParameterDirection.Input);
            dbManager.AddParameters(16, "@INFOHAPUR", info, ParameterDirection.Input);
            if (idambjent == 0 || idambjent == -1) dbManager.AddParameters(17, "@IDAMBJENT", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(17, "@IDAMBJENT", idambjent, ParameterDirection.Input);
            dbManager.AddParameters(18, "@KerkeseResetPass", KerkeseResetPass, ParameterDirection.Input);
            dbManager.AddParameters(19, "@PASSWORDIPERKOHSHEM", PassIPerkohshem, ParameterDirection.Input);
            dbManager.AddParameters(20, "@KYCUR", kycur, ParameterDirection.Input);
            dbManager.AddParameters(21, "@DATEKRIJIMIPASSWORD", dateKrijimiPassword, ParameterDirection.Input);
            dbManager.AddParameters(22, "@KONTROLLOPASSWORD", kontrolloPasswordin, ParameterDirection.Input);
            dbManager.AddParameters(23, "@EXPORTFORMAT", exportFormat, ParameterDirection.Input);
            dbManager.AddParameters(24, "@EXPORTMODE", exportMode, ParameterDirection.Input);
            dbManager.AddParameters(25, "@SHFAQDTPRINTIMI", shfaqdtprintimi, ParameterDirection.Input);
            dbManager.AddParameters(26, "@KYCURMOBILE", KycurMobile, ParameterDirection.Input);
            dbManager.AddParameters(27, "@SHFAQPERDORUESMENU", shfaqPerdoruesMenu, ParameterDirection.Input);
            dbManager.AddParameters(28, "@SHFAQMESAZHEPOPUP", shfaqMesazhePopup, ParameterDirection.Input);
            if (idambjentmobile == 0 || idambjentmobile == -1) dbManager.AddParameters(29, "@IDAMBJENTMOBILE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(29, "@IDAMBJENTMOBILE", idambjentmobile, ParameterDirection.Input);
            dbManager.AddParameters(30, "@UseriCRM", usericrm, ParameterDirection.Input);
            dbManager.AddParameters(31, "@UserEtopUP", userEtopuP, ParameterDirection.Input);
            dbManager.AddParameters(32, "@IDETopUp", iDEtopUp, ParameterDirection.Input);
            dbManager.AddParameters(33, "@TypeOfDevice", typeofDevice, ParameterDirection.Input);
            dbManager.AddParameters(34, "@SalesRepMPesaMSISDN", salesrepMPesaMSISDN, ParameterDirection.Input);
            dbManager.AddParameters(35, "@SalesRepMobileNumber", salesrepMobileNumber, ParameterDirection.Input);
            dbManager.AddParameters(36, "@Gjinia", gjini, ParameterDirection.Input);
            dbManager.AddParameters(37, "@SalesRepStartDateVod", salesrepStartDateVod, ParameterDirection.Input);
            dbManager.AddParameters(38, "@SalesRepTrainingStart", salesrepTrainingStart, ParameterDirection.Input);
            dbManager.AddParameters(39, "@SalesRepStartDateShop", salesrepStartDateShop, ParameterDirection.Input);
            dbManager.AddParameters(40, "@SalesRepMaternityLeaveStart", salesrepMaternityLeaveStart, ParameterDirection.Input);
            dbManager.AddParameters(41, "@LeaveDateVod", leavedateVod, ParameterDirection.Input);
            dbManager.AddParameters(42, "@LeaveDateShop", leavedateShop, ParameterDirection.Input);
            dbManager.AddParameters(43, "@MaternityLeaveEndDate", maternityleaveEndDate, ParameterDirection.Input);
            dbManager.AddParameters(44, "@TrainingEndDate", trainingendDate, ParameterDirection.Input);
            dbManager.AddParameters(45, "@CommentsRetailSales", commentsretailSales, ParameterDirection.Input);
            dbManager.AddParameters(46, "@AccountExecutive", accountexecutive, ParameterDirection.Input);
            dbManager.AddParameters(47, "@IDNumber", idNumber, ParameterDirection.Input);
            dbManager.AddParameters(48, "@IsInsured", isinsured, ParameterDirection.Input);
            dbManager.AddParameters(49, "@CommentsRetailOpSpecialist", commentsretailOpSpecialist, ParameterDirection.Input);
            dbManager.AddParameters(50, "@RegionalSupervisor", regionalsupervisor, ParameterDirection.Input);
            dbManager.AddParameters(51, "@RetailSalesAccountExecutive", retailsalesAccountExecutive, ParameterDirection.Input);
            dbManager.AddParameters(52, "@RetailSalesAreaManager", retailsalesAreaManager, ParameterDirection.Input);
            dbManager.AddParameters(53, "@Birthdate", birthDate, ParameterDirection.Input);
            dbManager.AddParameters(54, "@SiteCode", sitecode, ParameterDirection.Input);
            dbManager.AddParameters(55, "@District", districti, ParameterDirection.Input);
            dbManager.AddParameters(56, "@ShopMainCode", shopmainCode, ParameterDirection.Input);
            dbManager.AddParameters(57, "@Latitude", latitude, ParameterDirection.Input);
            dbManager.AddParameters(58, "@Longitude", longitude, ParameterDirection.Input);
            if (status == 0 || status == -1) dbManager.AddParameters(59, "@IDHIERARKISTATUS", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(59, "@IDHIERARKISTATUS", status, ParameterDirection.Input);
            if (leaveReason == 0 || leaveReason == -1) dbManager.AddParameters(60, "@IDHIERARKILEAVEREASON", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(60, "@IDHIERARKILEAVEREASON", leaveReason, ParameterDirection.Input);
            if (uniform == 0 || uniform == -1) dbManager.AddParameters(61, "@IDHIERARKIUNIFORM", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(61, "@IDHIERARKIUNIFORM", uniform, ParameterDirection.Input);
            dbManager.AddParameters(62, "@Shenime", shenime, ParameterDirection.Input);
            dbManager.AddParameters(63, "@StatusAprovimi", statusAprovimi, ParameterDirection.Input);
            dbManager.AddParameters(64, "@NjoftimEmailAprovim", njoftimEmailAprovim, ParameterDirection.Input);
            if (idkonfigkase == 0)
                dbManager.AddParameters(65, "@IDKONFIGKASA", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(65, "@IDKONFIGKASA", idkonfigkase, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_ins");
            idperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");

        }

        internal string[] merrKursinFunditTeKonfigPerMonedhatNdermarrjesSipasDates(int idNdermarrja, int idPerdoruesi, int idKonfigAmbjent, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTE", idKonfigAmbjent, ParameterDirection.Input);
            dbManager.AddParameters("@DATAKURSIT", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_T_MONEDHA_KtheKursTeKonfigPerCdoMonedheTeNdermSipasDates");
            List<string> result = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
                result.Add(row.ItemArray[0].ToString());
            return result.ToArray();
        }

        internal DataTable ktheNdermarjeBijSipasNivelitDT(int idNdermarrje, int nivelStrukture)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@Raportuesi", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@NivelStrukture", nivelStrukture, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeRaportueseSipasNivelit");
            return ds.Tables[0];
        }


        /// <summary>
        /// Ruajtja e te drejtave te secilit perdorues 
        /// Nje objekt clsPerdorues ka nje colection me te drejta si property te veten, 
        /// ruajtja e perdoruesit imponon ruajtjen edhe te nje colection-i me te drejta
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe perdoruesi bashke me te drejtat konsideroeht si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje perdoruesi se bashku me bashkesine e te drejtave
        /// </summary>
        /// <param name="perdorues"></param>
        /// <param name="idndermarje"></param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>

        public string merrEmailSipasIdPerdoruesit(int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUES_ktheEmail"));
        }
        /// <summary>
        /// therritet procedura nga DB prc_T_PERDORUESI_upd pasi merr parametrat qe i kalohen nga objekti clsPerdorues
        /// </summary>
        /// <param name="perdorues"></param>
        /// <returns>kthen statusin e e ekzekutimit te SP-se nepermjet objektit clsMesazh</returns>
        /// <param name="info"></param>
        internal clsMesazh modifikoPerdorues(int idperdorues, int idqyteti, int idgjuha, String emriperdorues, String mbiemriperdorues, bool perdoruesaktiv, String perdoruesusername, String perdoruespassword,
            String perdoruestel, String perdoruesfax, String perdoruesemail, String perdoruesadresa, int idperdoruesi, int idStilRaporti, int idstatusdok, float zoomFactor, bool info, int idambjent,
            string KerkeseResetPass, bool PassIPerkohshem, bool kycur, DateTime dateKrijimiPassword, bool kontrolloPassword, int exportFormat, int exportMode, bool shfaqdtprintimi, bool KycurMobile, bool shfaqPerdoruesMenu, bool shfaqMesazhePopup, int idambjentmobile, String shopcode, String shopname, String dealername, String usericrm, String userEtopuP, String iDEtopUp, String typeofDevice, String salesrepMobileNumber, String salesrepMPesaMSISDN, int gjini, DateTime salesrepStartDateVod, DateTime salesrepTrainingStart, DateTime salesrepStartDateShop, DateTime salesrepMaternityLeaveStart, DateTime leavedateVod, DateTime leavedateShop, DateTime maternityleaveEndDate, DateTime trainingendDate, string commentsretailSales, string accountexecutive, string idNumber, int isinsured, string commentsretailOpSpecialist, string regionalsupervisor, string retailsalesAccountExecutive, string retailsalesAreaManager, DateTime birthDate, string sitecode, string districti, string shopmainCode, string latitude, string longitude, int status, int leaveReason, int uniform, string shenime, int statusAprovimi, bool njoftimEmailAprovim, int idkonfigkase)
        {

            dbManager.Open();
            dbManager.CreateParameters(66);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idgjuha, ParameterDirection.Input);
            if (idqyteti == 0 || idqyteti == -1)
                dbManager.AddParameters(2, "@IDQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDQYTETI", idqyteti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERDORUESEMRI", emriperdorues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERDORUESMBIEMRI", mbiemriperdorues, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERDORUESAKTIV", perdoruesaktiv, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERDORUESUSERNAME", perdoruesusername, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERDORUESPASSWORD", perdoruespassword, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERDORUESTEL", perdoruestel, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PERDORUESFAX", perdoruesfax, ParameterDirection.Input);
            dbManager.AddParameters(10, "@PERDORUESEMAIL", perdoruesemail, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PERDORUESADRESA", perdoruesadresa, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(13, "@IDTHEME", idTheme, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDSTILRAPORTI", idStilRaporti, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(15, "@ZOOMFACTOR", zoomFactor, ParameterDirection.Input);
            dbManager.AddParameters(16, "@INFOHAPUR", info, ParameterDirection.Input);
            if (idambjent == 0 || idambjent == -1)
                dbManager.AddParameters(17, "@IDAMBJENT", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(17, "@IDAMBJENT", idambjent, ParameterDirection.Input);
            dbManager.AddParameters(18, "@KerkeseResetPass", KerkeseResetPass, ParameterDirection.Input);
            dbManager.AddParameters(19, "@PASSWORDIPERKOHSHEM", PassIPerkohshem, ParameterDirection.Input);
            dbManager.AddParameters(20, "@KYCUR", kycur, ParameterDirection.Input);
            dbManager.AddParameters(21, "@DATEKRIJIMIPASSWORD", dateKrijimiPassword, ParameterDirection.Input);
            dbManager.AddParameters(22, "@KONTROLLOPASSWORD", kontrolloPassword, ParameterDirection.Input);
            dbManager.AddParameters(23, "@EXPORTFORMAT", exportFormat, ParameterDirection.Input);
            dbManager.AddParameters(24, "@EXPORTMODE", exportMode, ParameterDirection.Input);
            dbManager.AddParameters(25, "@SHFAQDTPRINTIMI", shfaqdtprintimi, ParameterDirection.Input);
            dbManager.AddParameters(26, "@KYCURMOBILE", KycurMobile, ParameterDirection.Input);
            dbManager.AddParameters(27, "@SHFAQPERDORUESMENU", shfaqPerdoruesMenu, ParameterDirection.Input);
            dbManager.AddParameters(28, "@SHFAQMESAZHEPOPUP", shfaqMesazhePopup, ParameterDirection.Input);
            if (idambjentmobile == 0 || idambjentmobile == -1)
                dbManager.AddParameters(29, "@IDAMBJENTMOBILE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(29, "@IDAMBJENTMOBILE", idambjentmobile, ParameterDirection.Input);
            dbManager.AddParameters(30, "@UseriCRM", usericrm, ParameterDirection.Input);
            dbManager.AddParameters(31, "@UserEtopUP", userEtopuP, ParameterDirection.Input);
            dbManager.AddParameters(32, "@IDETopUp", iDEtopUp, ParameterDirection.Input);
            dbManager.AddParameters(33, "@TypeOfDevice", typeofDevice, ParameterDirection.Input);
            dbManager.AddParameters(34, "@SalesRepMPesaMSISDN", salesrepMPesaMSISDN, ParameterDirection.Input);
            dbManager.AddParameters(35, "@SalesRepMobileNumber", salesrepMobileNumber, ParameterDirection.Input);
            dbManager.AddParameters(36, "@Gjinia", gjini, ParameterDirection.Input);
            dbManager.AddParameters(37, "@SalesRepStartDateVod", salesrepStartDateVod, ParameterDirection.Input);
            dbManager.AddParameters(38, "@SalesRepTrainingStart", salesrepTrainingStart, ParameterDirection.Input);
            dbManager.AddParameters(39, "@SalesRepStartDateShop", salesrepStartDateShop, ParameterDirection.Input);
            dbManager.AddParameters(40, "@SalesRepMaternityLeaveStart", salesrepMaternityLeaveStart, ParameterDirection.Input);
            dbManager.AddParameters(41, "@LeaveDateVod", leavedateVod, ParameterDirection.Input);
            dbManager.AddParameters(42, "@LeaveDateShop", leavedateShop, ParameterDirection.Input);
            dbManager.AddParameters(43, "@MaternityLeaveEndDate", maternityleaveEndDate, ParameterDirection.Input);
            dbManager.AddParameters(44, "@TrainingEndDate", trainingendDate, ParameterDirection.Input);
            dbManager.AddParameters(45, "@CommentsRetailSales", commentsretailSales, ParameterDirection.Input);
            dbManager.AddParameters(46, "@AccountExecutive", accountexecutive, ParameterDirection.Input);
            dbManager.AddParameters(47, "@IDNumber", idNumber, ParameterDirection.Input);
            dbManager.AddParameters(48, "@IsInsured", isinsured, ParameterDirection.Input);
            dbManager.AddParameters(49, "@CommentsRetailOpSpecialist", commentsretailOpSpecialist, ParameterDirection.Input);
            dbManager.AddParameters(50, "@RegionalSupervisor", regionalsupervisor, ParameterDirection.Input);
            dbManager.AddParameters(51, "@RetailSalesAccountExecutive", retailsalesAccountExecutive, ParameterDirection.Input);
            dbManager.AddParameters(52, "@RetailSalesAreaManager", retailsalesAreaManager, ParameterDirection.Input);
            dbManager.AddParameters(53, "@Birthdate", birthDate, ParameterDirection.Input);
            dbManager.AddParameters(54, "@SiteCode", sitecode, ParameterDirection.Input);
            dbManager.AddParameters(55, "@District", districti, ParameterDirection.Input);
            dbManager.AddParameters(56, "@ShopMainCode", shopmainCode, ParameterDirection.Input);
            dbManager.AddParameters(57, "@Latitude", latitude, ParameterDirection.Input);
            dbManager.AddParameters(58, "@Longitude", longitude, ParameterDirection.Input);
            if (status == 0 || status == -1) dbManager.AddParameters(59, "@IDHIERARKISTATUS", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(59, "@IDHIERARKISTATUS", status, ParameterDirection.Input);
            if (leaveReason == 0 || leaveReason == -1) dbManager.AddParameters(60, "@IDHIERARKILEAVEREASON", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(60, "@IDHIERARKILEAVEREASON", leaveReason, ParameterDirection.Input);
            if (uniform == 0 || uniform == -1) dbManager.AddParameters(61, "@IDHIERARKIUNIFORM", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(61, "@IDHIERARKIUNIFORM", uniform, ParameterDirection.Input);
            dbManager.AddParameters(62, "@Shenime", shenime, ParameterDirection.Input);
            dbManager.AddParameters(63, "@StatusAprovimi", statusAprovimi, ParameterDirection.Input);
            dbManager.AddParameters(64, "@NjoftimEmailAprovim", njoftimEmailAprovim, ParameterDirection.Input);
            if (idkonfigkase == 0)
                dbManager.AddParameters(65, "@IDKONFIGKASA", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(65, "@IDKONFIGKASA", idkonfigkase, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_upd");

            idperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true);
            
        }
        internal clsMesazh modifikoPerdoruesEmail(int idperdorues, String perdoruesemail)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            dbManager.AddParameters(1, "@PERDORUESEMAIL", perdoruesemail, ParameterDirection.Input);


            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_updEmail");
            idperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true);


        }

        public  clsMesazh modifikoPerdoruesOtp(int idperdorues, string otp)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            dbManager.AddParameters(1, "@OTP_Token", otp, ParameterDirection.Input);


            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_updOtp");
            idperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true);
        }

        internal String merrEmerPerdoruesi(int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            String emri = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_ktheEmerPerdoruesi").ToString();
            return emri;

        }
        internal String merrEmerMbiemerPerdoruesi(int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            String emri = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_ktheEmerMbiemerPerdoruesi").ToString();
            return emri;
        }

        public DataTable merrEmaileDheGjuhePerAprovimePerdoruesish()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrEmaileDheGjuhePerAprovuesit");
            return ds.Tables[0];
        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_PERDORUESI_del duke i kaluar id e perdoruesit qe e marrim nga objekti clsPerdorues qe i kalohet si parameter
        /// </summary>
        /// <param name="perdorues"></param>
        /// <returns>kthen true apo false ne varesi si eshte ekzekuatuar sp-ja</returns>
        internal clsMesazh fshiPerdorues(int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }
        internal clsMesazh fshiPerdoruesStatus(int idperdorues, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_upddel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }
        
        internal clsMesazh modifikoIdKonfigurimKaseTePerdoruesit(int idkonfigurimi, int idkonfigurimiVjeter)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGKASA", idkonfigurimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGURIMIVJETER", idkonfigurimiVjeter, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_updKonfigKasa");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses");
        }

        #endregion

        #region SHOPSHIERARKISTATUS


        public clsMesazh ruajShopsHierarkiStatus(string pershkrimStatusi, bool aktiv, int idkrijuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKRIMSTATUSI", pershkrimStatusi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_STATUS_ins");

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        public void merrShopsHierarkiStatusi(int idStatus, IDataBaseReader objekt)
        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDSTATUS", idStatus, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SHOPS_HIERARKI_STATUS_sel", objekt);

        }

        public void merrShopsHierarkiStatusiSipasPershkrimi(string pershkrimStatusi, IDataBaseReader objekt)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimStatusi, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SHOPS_HIERARKI_STATUS_merrStatusSipasPershkrimi", objekt);

        }

        internal void ktheStatuseShopsHierarki(IDataBaseReader objekt)

        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(0);
            dbManager.FillCollection("prc_KtheShopsHierarkiStatus", objekt);


        }

        public clsMesazh modifikoShopsHierarkiStatus(int idStatus, string pershkrimStatusi, int idPerdorues, bool aktiv)
        {//metoda per modifikimin e artikullZevendesues
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSTATUS", idStatus, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMSTATUSI", pershkrimStatusi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_STATUS_upd");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
        }


        public bool ekzistonStatusMeKetePershkrim(String pershkrimStatusi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMSTATUSI", pershkrimStatusi, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_STATUS_ekzistonStatusPershkrimi"));

        }
        internal int ktheIdStatusPerdoruesiSipasPershkrimit(string pershkimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@Pershkrimi", pershkimi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_STATUS_merrIdStatusPerdoruesiSipasPershkrimi"));
            return (pergjigje);
        }
        #endregion

        #region SHOPSHIERARKILEAVEREASON


        public clsMesazh ruajShopsHierarkiLeaveReason(string pershkrimLeaveReason, int idkrijuesi, bool aktiv)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKRIMLEAVEREASON", pershkrimLeaveReason, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_LEAVEREASON_ins");

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal void ktheLeaveReasonShopsHierarki(IDataBaseReader objekt)

        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(0);
            dbManager.FillCollection("prc_KtheShopsHierarkiLeaveReason", objekt);
        }

        public void merrShopsHierarkiLeaveReasonSipasPershkrimi(string pershkrimLeaveReason, IDataBaseReader objekt)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimLeaveReason, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SHOPS_HIERARKI_LEAVEREASON_merrSipasPershkrimi", objekt);
        }

        public void merrShopsHierarkiLeaveReason(int idLeaveReason, IDataBaseReader objekt)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDLEAVEREASON", idLeaveReason, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SHOPS_HIERARKI_LEAVEREASON_sel", objekt);
        }

        public clsMesazh modifikoShopsHierarkiLeaveReason(int idLeaveReason, string pershkrimLeaveReason, int idPerdorues, bool aktiv)
        {//metoda per modifikimin e artikullZevendesues
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDLEAVEREASON", idLeaveReason, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMLEAVEREASON", pershkrimLeaveReason, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_LEAVEREASON_upd");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
        }

        public bool ekzistonLeaveReasonMeKetePershkrim(String pershkrimLeaveReason)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMLEAVEREASON", pershkrimLeaveReason, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_LEAVEREASON_ekzistonLeaveReasonPershkrimi"));
        }


        #endregion

        #region SHOPSHIERARKIUNIFORM


        public clsMesazh ruajShopsHierarkiUniform(string pershkrimUniform, int idkrijuesi, bool aktiv)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PERSHKRIMUNIFORM", pershkrimUniform, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_UNIFORM_ins");

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        public void merrShopsHierarkiUniform(int idUniform, IDataBaseReader objekt)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDUNIFORM", idUniform, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SHOPS_HIERARKI_UNIFORM_sel", objekt);
        }

        public void merrShopsHierarkiUniformSipasPershkrimi(string pershkrimUniform, IDataBaseReader objekt)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimUniform, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SHOPS_HIERARKI_UNIFORM_merrUniformeSipasPershkrimi", objekt);
        }

        public clsMesazh modifikoShopsHierarkiUniform(int idUnifom, string pershkrimUniform, int idPerdorues, bool aktiv)
        {//metoda per modifikimin e artikullZevendesues
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDUNIFORM", idUnifom, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMUNIFORM", pershkrimUniform, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@AKTIV", aktiv, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_UNIFORM_upd");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
        }

        public bool ekzistonUniformMeKetePershkrim(String pershkrimUniform)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMUNIFORM", pershkrimUniform, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SHOPS_HIERARKI_UNIFORM_ekzistonUniformPershkrimi"));

        }

        internal void ktheUniformShopsHierarki(IDataBaseReader objekt)

        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(0);
            dbManager.FillCollection("prc_KtheShopsHierarkiUniform", objekt);
        }
        internal clsMesazh ndryshoInfo(int idperdorues, int idperdoruesi, bool info)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@INFOHAPUR", info, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "[prc_T_PERDORUESI_updinfo]");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }
        internal clsMesazh ndryshoInfoplus(int idperdorues, int idperdoruesi, bool info)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@INFOHAPUR", info, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "[prc_T_PERDORUESI_updinfo]");
            return new clsMesazh(true, "Infoja do të shfaqet në ambjentin e regjistrimit. ");

        }
        internal clsMesazh ndryshoInfominus(int idperdorues, int idperdoruesi, bool info)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@INFOHAPUR", info, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "[prc_T_PERDORUESI_updinfo]");
            return new clsMesazh(true, "Infoja nuk do të shfaqet në ambjentin e regjistrimit. ");


        }
        //[Obsolete("Perdor: clsMesazh fshiPerdorues(int idperdorues)", true)]
        //public clsMesazh fshiPerdorues(clsPerdorues perdorues)
        //{
        //    try
        //    {              
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDPERDORUES", perdorues.IdPerdorues, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_del");
        //        return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //    }
        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        /// <summary>
        /// kthen objektin perdorues sipas username, mqs presupozohet qe username edhe unik ne bashkesine e perdorueve te programit
        /// </summary>
        /// <param name="perdorues"></param>
        internal void merrPerdorues(String perdoruesusername)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesusername, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_sel");

        }

        /// <summary>
        /// fshin te gjitha te drejat e perdoruesit qe i perkasin moduleve te kaluara si parameter
        /// </summary>
        /// <param name="perdorues"></param>
        /// <param name="modulet"></param>
        /// <returns>kthen true apo false ne varesi si eshte ekzekuatuar sp-ja</returns>
        internal bool fshiTeDrejtaPerdorues(int idperdorues, ArrayList modulet)
        {

            String bashkesiaModuleve = "";
            foreach (int m in modulet)
                bashkesiaModuleve += m + ",";

            if (bashkesiaModuleve.Length > 0)
                bashkesiaModuleve = bashkesiaModuleve.Substring(0, bashkesiaModuleve.Length - 1);

            string sqlstr = "delete from  T_DREJTAT" +
                              " where IDPERDORUES =" + idperdorues +
                              " and PERDORUESAPOGRUP = 0 " +
                              " and IDMODUL in (" + bashkesiaModuleve + ")";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            return true;

        }

        internal DataTable mbushMetodaTransferimiPerSerialeUnike()
        {
            dbManager.Open();
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_SERIALEUNIKE_METODETRANSFERIMI_sel").Tables[0];
        }


        /// <summary>
        /// fshin te gjitha te drejtat e nje perdoruesi te dhene pavaresisht nga moduli
        /// kemi cilesi te overloading me metoden paraardhese
        /// </summary>
        /// <param name="perdorues"></param>
        /// <returns></returns>
        internal clsMesazh fshiTeDrejtaPerdorues(int idperdorues)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            string sqlstr = "delete from  T_DREJTAT" +
                              " where IDPERDORUES =" + idperdorues +
                              " and PERDORUESAPOGRUP = 0";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            return new clsMesazh(true);

        }


        /// <summary>
        /// ploteson obj perdorues me te gjitha atributet e tij te marra nga DB ne varesi te ID-se se ketij perdoruesi
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <param name="idnermarrjeviti"></param>
        /// <returns> kthen nje obj perdorues, pa plotesuar col e ndryshem qe ka ky obj si atribute</returns>
        internal DataRow merrPerdorues(int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_kthePerdorues");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, idnermarrjeviti);
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;


            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// ploteson obj perdorues me te gjitha atributet e tij te marra nga DB ne varesi te ID-se se ketij perdoruesi
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <param name="idnermarrjeviti"></param>
        /// <returns> kthen nje obj perdorues, pa plotesuar col e ndryshem qe ka ky obj si atribute</returns>

        /// <summary>
        /// kthen gjuhen e perdoruesit
        /// </summary>
        /// <param name="idperdorues"> id e perdoruesit qe do i marrim gjuhen</param>
        /// <returns> kthen e idgjuha e tipit integer </returns>
        internal int merrGjuhaPerdorues(int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            return (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_kthePerdorues");
        }

        /// <summary>
        ///  metoda e njejte me te mesipermen, eshte menduar per t'u ndryshuar me vone
        /// </summary>
        /// <param name="idperdorues"></param>
        internal DataTable merrPerdoruesGjitheNdermarrjet(int idperdorues)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_kthePerdorues");
            return ds.Tables[0];

        }


        internal DataTable ktheGjithePerdoruesit(int idperdorues)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open(); dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrGjithePerdoruesitE_Licences");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }
        internal DataTable ktheGjithePerdoruesitSipasAutorizimit(int idperdorues, int idautorizimi)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idautorizimi", idautorizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrGjithePerdoruesitE_LicencesSipasAutorizimit");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }
        internal DataTable ktheGjithePerdoruesitLike(int idperdorues, int idlicenca, string text)
        {


            dbManager.Open(); dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters(2, "@text", text, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrGjithePerdoruesitLike");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nej perdorues me nje username te marre si parameter
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen perdorues te ndryshem me te njejtin username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>true nqs gjendet perdorues ne DB me kete username dhe false ne te kundert</returns>
        /// 
        public bool ekzistonPerdoruesi(String username)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", username, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_ekzistonPerdoruesi");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;


        }
        /// <summary>
        /// perdoret per te kontrolluar nqs ka veprime me kete perdorues 

        /// </summary>
        /// <param name="idperdorues"></param>
        /// <returns>true nqs ka veprime</returns>
        /// 
        public bool kaVeprime(int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_kaVeprime");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        /// <summary>
        /// perdoret per te kontrolluar nqs perdoruesi ka licence per kete ndermarje
        /// </summary>
        /// <param name="idndermarje"></param>
        /// /// <param name="idperdorues"></param>
        /// <returns>true nqs ka veprime</returns>
        /// 
        public bool kaLicence(int idNdermarje, int idPerdorues)
        {
            int nrLicence;
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            nrLicence = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUES_kaLicencePerIdNdermarje");
            if (nrLicence > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// ben te njejtin kontroll me metoden me siper, por ndryshon menyra si e merr parametrin per username
        /// metode overloaded e metodes se mesiperme
        /// </summary>
        /// <param name="perdoruesi"></param>
        /// <returns></returns>
        internal bool ekzistonPerdoruesMeKeteKod(String perdoruesusername)
        {
            int nrPerdoruesish;

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesusername, ParameterDirection.Input);
            nrPerdoruesish = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_PerdoruesMeKeteKod");
            if (nrPerdoruesish > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Metode e krijuar per Vodafone. Meqenese do te ken nje prind, useri do te identifikohen ne baze te username qe vjen nga eTop up, keshtu qe rekordi do te jete gjithmone nje datarow.
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <returns> kthen nje obj perdorues, pa plotesuar col e ndryshem qe ka ky obj si atribute</returns>
        internal DataRow merrPerdoruesSipasUsernameTePrindi(string perdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_MERR_PERDORUES_MEUSERNAME");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Metode qe kthen id e perdoruesit sipas username dhe ne varesi te te drejtave qe ka ky user per ndermarrjen. 
        /// Ne qofte se ekziston dhe ka te drejta atehere kthen id e perdoruesit, perndryshe kthen 0.
        /// </summary>
        /// <param name="perdoruesi">username i perdoruesit</param>
        /// <param name="idNdermarrje">id e ndermarrjes se loguar</param>
        /// <returns> kthen id e perdoruesit </returns>
        internal int merrPerdoruesIdSipasUsername(string perdoruesi, int idNdermarrje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int idPerdorues = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_MERRIDPERDORUESSIPASUSERNAME"));
            return idPerdorues;

        }
        internal int merrPerdoruesIdSipasUsernamePateDrejta(string perdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesi, ParameterDirection.Input);
            int idPerdorues = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_MERRIDPERDORUESSIPASUSERNAMEPATEDREJTA"));
            return idPerdorues;

        }


        /// <summary>
        /// Metode qe kthen infohapur per perdoruesin
        /// </summary>
        /// <param name="perdoruesi">id e perdoruesit</param>
        /// <returns> kthen id e perdoruesit </returns>
        internal string merrInfoHapur(int perdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", perdoruesi, ParameterDirection.Input);
            string infoHapur = Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_MERRINFOHAPURSIPASID"));
            return infoHapur;
        }

        internal bool merrPerdoruesKycurMobile(int perdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", perdoruesi, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_kthePerdoruesKycurMobile"));
        }


        internal bool merrPerdoruesShfaqMesazhePopup(int perdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", perdoruesi, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_kthePerdoruesShfaqMesazhePopup"));
        }



        /// <summary>
        /// Metode qe kthen gjuhen e perdoruesit sipas username dhe ne varesi te te drejtave qe ka ky user per ndermarrjen. 
        /// Ne qofte se ekziston dhe ka te drejta atehere kthen id e perdoruesit, perndryshe kthen 0.
        /// </summary>
        /// <param name="perdoruesi">username i perdoruesit</param>
        /// <param name="idNdermarrje">id e ndermarrjes se loguar</param>
        /// <returns> kthen id e perdoruesit </returns>
        internal int merrIdGjuhaSipasIdPerdorues(int idPerdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            int idPerdorues = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_ktheIdGjuhePerdoruesi"));
            return idPerdorues;

        }
        internal DataTable merrPerdoruesPerImport(string emerTabKoka, string ndermarrjeKey, string ndermarjeKodi, bool merrTePaImportuara, bool riMerrTePaImportuara)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@T_TEMP_KOKASHITJE", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NDERMARRJEKEY", ndermarrjeKey, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RIMERRTEIMPORTUARA", riMerrTePaImportuara, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORTmerrTeDhenaDT");
            return ds.Tables[0];

        }

        //[Obsolete("Perdor: bool ekzistonPerdoruesMeKeteKod(String perdoruesusername)", true)]
        //private bool ekzistonPerdoruesMeKeteKod(clsPerdorues perdoruesi)
        //{
        //    int nrPerdoruesish;
        //    try
        //    {
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesi.PerdoruesUsername, ParameterDirection.Input);
        //        nrPerdoruesish = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_PerdoruesMeKeteKod");
        //        if (nrPerdoruesish > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return true;
        //    }
        //}

        /// <summary>
        /// //metoda per te marre te dhenat per te mbushur pemen e perdoruesve
        /// </summary>
        /// <returns></returns>
        internal DataTable kthePemenEPerdoruesve()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrPemenEPerdoruesve");
            return ds.Tables[0];

        }

        /// <summary>
        /// ekzekuton SP per marrjen e te gjithe perdoruesve nga DB dhe vendosje ne tyre ne obj te tipit clsPerdorues
        /// </summary>
        /// <returns>te gjithe rreshtat e regjistruara per perdoruesit ne DB, pavaresisht nga ndermarja te ciles i perkasin</returns>
        internal DataTable merrPerdoruesitSipasLicencesDT(int idperdorues, int idlicenca)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open(); dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrPerdoruesitSipasLicencesDT");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }

        internal int merrNrPerdoruesishSipasLicences(int idperdorues, int idlicenca)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);

            int numri = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrNumerPerdoruesishSipasLicences"));
            return numri;

        }

        internal DataTable merrPerdoruesitSipasLicencesDTJoSuper(int idperdorues, int idlicenca)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open(); dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrPerdoruesitSipasLicencesDTJoSuper");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }




        internal DataTable merrPerdoruesitSipasLicencesDTPerNdermaje(int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open(); dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);


            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrPerdoruesitSipasLicencesDTPerNdermarje");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }

        internal DataTable merrSipasLicencesDTPerExport(int idperdorues, int idlicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrSipasLicencesDT_Eksport");
            return ds.Tables[0];
        }
        /// <summary>
        /// ploteson obj perdorues me te gjitha atributet e tij te marra nga DB ne varesi te ID-se se ketij perdoruesi
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <param name="idnermarrjeviti"></param>
        /// <returns> kthen nje obj perdorues, pa plotesuar col e ndryshem qe ka ky obj si atribute</returns>
        internal DataRow merrPerdoruesDR(int idperdorues)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrPerdoruesDR");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, idnermarrjeviti);
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// vendos nje querystring te hashuar te kolona KerkeseResetPass qe do ti kalohet linkut per verifikimin e kerkeses per resetimin e password-it
        /// </summary>
        /// <param name="emriPerdoruesit">emri i perdoruesit qe ka bere kerkesen per resetim passwordi</param>
        /// <param name="queryStringHashuar">querystringu i linkut qe do sherbeje per verifikimin </param>
        /// <returns></returns>
        public bool shtoKerkeseResetPass(string emriPerdoruesit, string queryStringHashuar)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@Perdoruesi", emriPerdoruesit, ParameterDirection.Input);
            dbManager.AddParameters(1, "@queryStringHashuar", queryStringHashuar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_shtoKerkeseResetPass");
            return true;

        }

        /// <summary>
        /// ruan stilin e raportit per perdoruesin(idstili ne baze te stylename dhe zoom-in)
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="styleName"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        internal clsMesazh ruajStilRaporti(int idPerdoruesi, string styleName, float zoom, int exportFormat, int exportMode)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@styleName", styleName, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ZOOMFACTOR", zoom, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EXPORTFORMAT", exportFormat, ParameterDirection.Input);
            dbManager.AddParameters(4, "@EXPORTMODE", exportMode, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_ruajStilRaporti");
            return new clsMesazh(true, "Stili i perdoruesit u ruajt me sukses!");

        }

        /// <summary>
        /// modifikon password-in e perdoruesit
        /// </summary>
        /// <param name="idPerdoruesi">perdoruesi qe do i modifikohet password-i</param>
        /// <param name="newPass">password-i i ri</param>
        /// <param name="passIPerkohshem">tregon nese ky password eshte i perkohshem, pra qe perdoruesi duhet ta ndryshoje kur te logohet</param>
        /// <returns></returns>
        internal clsMesazh modifikoPassowrd(int idPerdoruesi, string newPass, bool passIPerkohshem)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@newPass", newPass, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PASSWORDIPERKOHSHEM", passIPerkohshem, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_updPassword");
            return new clsMesazh(true, "Fjalekalimi u modifikua me sukses!");

        }

        /// <summary>
        /// modifikon password-in e punonjesit
        /// </summary>
        /// <param name="idPerdoruesi">perdoruesi qe do i modifikohet password-i</param>
        /// <param name="newPass">password-i i ri</param>
        /// <param name="passIPerkohshem">tregon nese ky password eshte i perkohshem, pra qe perdoruesi duhet ta ndryshoje kur te logohet</param>
        /// <returns></returns>
        internal clsMesazh modifikoPassowrdPunonjes(int idPerdoruesi, string newPass, bool passIPerkohshem)
        {
            string salt = System.Web.Configuration.WebConfigurationManager.AppSettings["salt"];
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@newPass", newPass, ParameterDirection.Input);
            dbManager.AddParameters(2, "@salt", salt, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PUNONJES_updPassword");
            return new clsMesazh(true, "Fjalekalimi u modifikua me sukses!");

        }
        /// <summary>
        /// merr te dhena per perdoruesin qe ka vleren myQueryRF te kolona KerkeseResetPass, null nqs nuk ka
        /// </summary>
        /// <param name="myQueryRF">string i hashuar qe do i kalohet ne linkun per verifikimin e resetimit te passwordit me email</param>
        /// <returns>kthen te dhenat e perdoruesit qe ka kete querystring te hashuar</returns>
        internal DataRow merrQueryStringHashuar(string myQueryRF)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@queryStringHashuar", myQueryRF, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[prc_T_PERDORUESI_merrKerkeseResetPass]");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal bool krijoPerdoruesMeGmail(string email,string username,string name,string password,int roli)
        {
            string connectionString = dbManager.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"INSERT INTO T_PERDORUESI ([PERDORUESEMRI],[PERDORUESMBIEMRI],[PERDORUESAKTIV],[PERDORUESUSERNAME],[PERDORUESPASSWORD],[PERDORUESEMAIL],[IDPERDORUESI],[IDSTATUSDOK]) VALUES ('{name}','{name}','True','{username}','{password}','{email}','14466','1')" +
                    $"INSERT INTO T_ROLPERDORUES ([IDROLI],[IDPERDORUES])  SELECT RP.IDROLI,(SELECT top 1 IDPERDORUES FROM T_PERDORUESI WHERE PERDORUESUSERNAME = '{username}' and PERDORUESPASSWORD = '{password}' and PERDORUESEMAIL = '{email}' and IDSTATUSDOK = 1 and PERDORUESAKTIV = 1 order by IDPERDORUES desc) FROM  T_PERDORUESI P inner join  T_ROLPERDORUES RP on RP.IDPERDORUES = P.IDPERDORUES inner join T_ROLI R on R.IDROLI = RP.IDROLI where P.IDPERDORUES = {roli}"+
                    $"INSERT INTO T_THEMESAMBJENTE VALUES('IMB09', 'Metropolis Blue', 1, 1, 42, 42, 22, 163, (SELECT top 1 IDPERDORUES FROM T_PERDORUESI WHERE PERDORUESUSERNAME = '{username}' and PERDORUESPASSWORD = '{password}' and PERDORUESEMAIL = '{email}' and IDSTATUSDOK = 1 and PERDORUESAKTIV = 1 order by IDPERDORUES desc), 1, null, null)", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                    connection.Close();
                }
            }
            return true;


        }

        /// <summary>
        /// kyc perdoruesin te tabela T_PERDORUESI
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        internal bool kycPerdorues(int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERDORUESI_updKycur");
            return true;

        }

        #endregion

        #region ROLPERDORUES

        /// <summary>
        /// ekzekuton prc_T_ROLPERDORUES_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idPerdorues">idperdoruesi</param>
        ///<param name="idRoli">id roli</param>
        /// <returns> nje id e rolperdoruesit te krijuar.</returns>
        /// </summary>
        public int krijoRolPerdorues(int idRoli, int idPerdorues)
        {
            int idRolPerdorues = -1;
            if (ekzistonRolPerdorues(idRoli, idPerdorues) > 0) //ekziston rolperdoruesi me kete rol dhe perdorues
                return idRolPerdorues;

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDROLPERDORUES", idRolPerdorues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_ins");
            idRolPerdorues = Convert.ToInt32(dbManager.Parameters[0].Value.ToString());
            return idRolPerdorues;

        }

        /// <summary>
        /// ekzekuton prc_T_ROLPERDORUES_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        ///<param name="idPerdorues">idPerdorues</param>
        ///<param name="idRoli">idRoli</param>
        ///<param name="idRolPerdorues">idRolPerdorues</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        public clsMesazh modifikoRolPerdorues(int idRolPerdorues, int idRoli, int idPerdorues)
        {//metoda per modifikimin e artikullZevendesues
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDROLPERDORUES", idRolPerdorues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_ROLPERDORUES_del duke i kaluar id e ROL perdorues 
        /// </summary>
        ///<param name="idRolPerdorues">idRolPerdorues</param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiRolPerdorues(int idRolPerdorues)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDROLPERDORUES", idRolPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }

        /// <summary>
        /// kthen objektet rolperdorues sipas id
        /// </summary>
        ///<param name="idRolPerdorues"> id e rolit te perdoruesit</param>
        public DataRow merrRolPerdorues(int idRolPerdorues)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDROLPERDORUES", idRolPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_sel");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;

        }

        /// <summary>
        /// kthen nje datatable me te gjithe rolPerdoruesit te ketij roli
        /// </summary>
        ///<param name="idRoli">idRoli</param>
        ///<returns>nje objekt DataTable me te gjithe perdoruesit e rolit  </returns>
        public DataTable merrRolPerdoruesSipasIdRoli(int idRoli)
        {


            this.dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrSipasIdRoli");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;

        }

        /// <summary>
        /// kthen nje datatable me te gjithe rolPerdoruesit ee ketij roli
        /// </summary>
        ///<param name="idRoli">idRoli</param>
        ///<returns>nje objekt DataTable me te gjithe perdoruesit e rolit  </returns>
        public DataTable ktheRolePerdoruesishSipasIdRoliDT(int idRoli)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrPerdoruesitSipasIdRoliDT");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public int merrIdLicencePerdoruesi(int idPerdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            Object idLicence = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrSipasIdPerdoruesiIdLicence");
            if (idLicence != null)
                return Convert.ToInt32(idLicence);
            return -1;
        }

        /// <summary>
        /// kthen nje datatable me te gjithe rolPerdoruesit te ketij perdoruesi
        /// </summary>
        ///<param name="idperdorues">idperdorues</param>
        ///<returns>nje objekt DataTable me te gjithe rolet e perdoruesit  </returns>
        public DataTable merrRolPerdoruesSipasIdPerdoruesi(int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrSipasIdPerdoruesi");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;

        }

        /// <summary>
        /// kthen nje datatable me te gjithe rolPerdoruesit te ketij perdoruesi
        /// </summary>
        ///<param name="idperdorues">idperdorues</param>
        ///<returns>nje objekt DataTable me te gjithe rolet e perdoruesit  </returns>
        public DataTable merrKodeRoleshSipasPerdoruesit(string usernamePerdoruesi)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@USERNAMEPERDORUES", usernamePerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrKodRoleshSipasPerdoruesi");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;

        }

        public int merrRolPerdoruesSipasIdPerdoruesiDheKodRol(int idperdoruesi, String roli)
        {


            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ROLI", roli, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrNrRol"));



        }

        public string merrEmailPerRoletRASipasPerdoruesDheKodRol(int idperdoruesi, string roli)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ROLI", roli, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUES_ktheEmailSipasKodRolit"));
        }

        public string merrKodeteRolevesipasPerdoruesit(int idperdoruesi, int idLicenca)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idLicenca", idLicenca, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUES_ktheKodetRoleveTePerdoruesit"));
        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston  kjo lidhje midis perdoruesit dhe rolit 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te lidhet nje perdorues me te njejtin rol dy here
        /// </summary>
        ///<param name="idPerdorues">idPerdorues</param>
        ///<param name="idRoli">idRoli</param>
        /// <returns>nje id e rolperdorues nqs ekziston</returns>
        public int ekzistonRolPerdorues(int idRoli, int idPerdoruesi)
        {
            int idRolPerdoruesi = -1;

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            Object idRoliObject = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_ekzistonRolPerdorues");
            if (idRoliObject != null)
                idRolPerdoruesi = Convert.ToInt32(idRoliObject);
            return idRolPerdoruesi;


        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston  kjo lidhje midis perdoruesit dhe rolit 
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te lidhet nje perdorues me te njejtin rol dy here
        /// </summary>
        ///<param name="idPerdorues">idPerdorues</param>
        ///<param name="idRoli">idRoli</param>
        /// <returns>nje id e rolperdorues nqs ekziston</returns>
        public bool eshteRoliLidhurMePerdorues(int idRoli)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_eshteRoliLidhurMePerdorues"));
            return Convert.ToBoolean(pergjigje);

        }

        #endregion

        #region ROLET

        /// <summary>
        /// nese roli me kodRoli gjendet ne db atehere kthen id-ne e rolit nese jo kthen numer <= 0
        /// </summary>
        /// <param name="kodRoli">kodi me te cilin do kerkohet</param>
        /// <returns>kthen id-ne e rolit nese gjendet, perndryshe kthen numer <= 0</returns>
        //internal int ekzistonRol(String kodRoli, int idlicenca)
        //{
        //    int idRoli = -1;

        //    //if (this.dbManager == null)
        //    //{
        //    //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
        //    //}
        //    this.dbManager.Open();
        //    dbManager.CreateParameters(2);
        //    dbManager.AddParameters(0, "@KODIROLI", kodRoli, ParameterDirection.Input);
        //    dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
        //    Object idRoliObject = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLI_ktheIdMeKod");
        //    if (idRoliObject != null)
        //        idRoli = Convert.ToInt32(idRoliObject);
        //    return idRoli;

        //}
        internal int ekzistonRol(String kodRoli, int idlicenca)
        {
            int idRoli = -1;

            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIROLI", kodRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            Object idRoliObject = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLI_ktheIdMeKodDheLicence");
            if (idRoliObject != null)
                idRoli = Convert.ToInt32(idRoliObject);
            return idRoli;
        }
        internal DataTable ktheRolePervecSuperUser()
        {
            this.dbManager.Open();
            string queryString = "SELECT PERDORUESUSERNAME as PERDORUESI,P.IDPERDORUES FROM T_PERDORUESI P inner join T_ROLPERDORUES RP on RP.IDPERDORUES = P.IDPERDORUES inner join T_ROLI R on R.IDROLI = RP.IDROLI where P.IDSTATUSDOK =1 and PERDORUESAKTIV = 1 and KODIROLI != \'RSU\' and R.IDSTATUSDOK =1 group by PERDORUESUSERNAME,P.IDPERDORUES order by PERDORUESUSERNAME asc";
            string connectionString = dbManager.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }

        /// <summary>
        /// nese roli me kodRoli gjendet ne db atehere kthen id-ne e rolit nese jo kthen numer <= 0
        /// </summary>
        /// <param name="kodRoli">kodi me te cilin do kerkohet</param>
        /// <returns>kthen id-ne e rolit nese gjendet, perndryshe kthen numer <= 0</returns>
        internal int ekzistonRolSipasKodit(String kodRoli, int idlicenca, int idPerdoruesi)
        {
            int idRoli = -1;


            this.dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODIROLI", kodRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            Object idRoliObject = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLI_ktheIdMeKod");
            if (idRoliObject != null)
                idRoli = Convert.ToInt32(idRoliObject);
            return idRoli;
        }

        /// <summary>
        /// krijon nje rol me te dhenat dhe e ruan ne historik
        /// </summary>
        /// <param name="kodRoli">kodi i rolit nese ekziston ne db krijimi deshton</param>
        /// <param name="pershkrimRoli">pershkrimi i rolit</param>
        /// <param name="aktivRoli">eshte aktiv apo jo </param>
        /// <param name="dateKrijimi">data krijimit</param>
        /// <param name="dateModifikimi">data modifikimit</param>
        /// <param name="idKrijuesi">id-ja e perdoruesit qe e krijoi</param>
        /// <returns>kthen id-ne e rolit nese eshte krijuar, kthen zero perndryshe</returns>
        public clsMesazh krijoRol(out int idRoli, String kodRoli, String pershkrimRoli, bool aktivRoli, int idKrijuesi, int model, int idlicenca, int idstatusdok)
        {
            idRoli = -1;
            this.dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters("@IDROLI", idRoli, ParameterDirection.Output);
            dbManager.AddParameters("@KODIROLI", kodRoli, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKROLI", pershkrimRoli, ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVROLI", aktivRoli, ParameterDirection.Input);
            dbManager.AddParameters("@KRIJUESI", idKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@MODEL", model, ParameterDirection.Input);
            dbManager.AddParameters("@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLI_ins");
            idRoli = Convert.ToInt32(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// modifikon rolin idRoli
        /// </summary>
        /// <param name="idRoli">idRoli sherben per te gjetur rolin ne db</param>        
        /// <param name="pershkrimRoli">pershkrimi i ri</param>
        /// <param name="aktivRoli">aktiv apo jo</param>        
        /// <param name="dateModifikimi">data tanishme</param>        
        /// <returns>True nese modifikohet me sukses, False perndryshe</returns>
        public bool modifikoRol(int idRoli, String pershkrimRoli, bool aktivRoli, int idPerdoruesi, int idstatusdok)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKROLI", pershkrimRoli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@AKTIVROLI", aktivRoli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLI_upd");
            return true;
        }

        /// <summary>
        /// lexon nga db-ja rolin sipas id-se
        /// </summary>
        /// <param name="idRoli">id-ja rolit per tu lexuar</param>
        /// <returns>kthen nje DataRow roli</returns>
        public DataRow merrRolin(int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_selectMeId");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;
        }

        /* Denisa komentuar pasi ka kaluar ne sp gjate strukturimit dt:05/06/2015
        /// <summary>
        ///ruan rolet default
        /// </summary>
        /// <param name="idRoli">id-ja rolit per tu lexuar</param>
        /// <returns>kthen nje DataRow roli</returns>
        public bool ruajRoleDefault(int idndermarje, int idviti, int idperdoruesi, int idllojlicence)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idviti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDLLOJLICENCE", idllojlicence, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLI_shtoRoleDefault");
            return true;
        }*/

        public clsMesazh ruajRoleDefaultMeTeDrejtaRaportesh(int idndermarje, int idviti, int idperdoruesi, int idllojlicence, int idlicenca)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(6);
            this.dbManager.AddParameters(0, "@Return_Message", "", ParameterDirection.Output);
            this.dbManager.Parameters[0].Size = 100;
            this.dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idviti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDLLOJLICENCE", idllojlicence, ParameterDirection.Input);
            this.dbManager.AddParameters(5, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLI_Dhe_ROLDREJTAT_ins_NeNjeSpNgaDefault");
            string msg = dbManager.Parameters[0].Value.ToString();
            if (msg.StartsWith("Gabim"))
                return new clsMesazh(false, msg);
            else
                return new clsMesazh(true, msg);
        }

        /// <summary>
        /// Per shpejtesi ne ekzekutim i kalohen vetem nje here te dhenat sp dhe aty behet shtimi automatik sipas rolit ne tabelat e te drejtave te trupit dhe te kokes
        /// </summary>
        /// <param name="idndermarje">Ndermarrja e re</param>
        /// <param name="idviti">Viti i Ri i ndermarrjes</param>
        /// <param name="idperdoruesi">perdoruesi qe po ben shtimin</param>
        /// <param name="idRoli">Roli qe i perket</param>
        /// <returns>Kthen mesazh per shtimin</returns>
        public clsMesazh ShtoTeGjitheTeDrejtaBazeNeNjeMeTransaksionNeSp(int idndermarje, int idviti, int idperdoruesi, int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(5);
            this.dbManager.AddParameters(0, "@Return_Message", "", ParameterDirection.Output);
            this.dbManager.Parameters[0].Size = 100;
            this.dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idviti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTAT_ALL_ins_PerNdermarrjeTeRe");
            string msg = dbManager.Parameters[0].Value.ToString();
            if (msg.StartsWith("Gabim"))
                return new clsMesazh(false, msg);
            else
                return new clsMesazh(true, msg);
        }

        /// <summary>
        ///ruan rolet default
        /// </summary>
        /// <param name="idRoli">id-ja rolit per tu lexuar</param>
        /// <returns>kthen nje DataRow roli</returns>
        public bool ndryshoLlojLicence(int idlicenca, int idllojlicence)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@idlicence", idlicenca, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDLLOJLICENCE", idllojlicence, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLI_ndryshollojlicence");
            return true;
        }
        
        public DataTable merrGjitheRoletSipasIdPerdoruesi(int idperdorues)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_merrSipasIdPerdoruesi");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable merrGjitheRoletLike(int idlicenca, string text)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@text", text, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_selectLike");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// nje perdorues mund t'i perkase nje ose disa grupeve te perdoruesve
        /// </summary>
        /// <param name="grup"></param>
        /// <returns></returns>
        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajGrupPerdoruesishTeDrejta(clsGrupiPerdorues grup)", true)]
        //public clsMesazh ruajGrupPerdoruesishTeDrejta(clsGrupiPerdorues grup)
        //{
        //    clsMesazh mesazh;
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();

        //    try
        //    {
        //        if (!ekzistonGrupPerdoruesiMeKeteKod(grup.GrupiPerdoruesKodi))
        //        {
        //            int idG;
        //            dbManager.BeginTransaction();
        //            mesazh = ruajGrupPerdoruesish(out idG, grup.GrupiPerdoruesKodi, grup.GrupiPerdoruesPershkrimi, grup.GrupiAktivPerdorues, grup.GrupiPerdoruesData, grup.IdPerdoruesi);
        //            if (mesazh.Status)
        //            {
        //                grup.IdGrupiPerdorues = idG;
        //                foreach (clsTeDrejtat o in grup.OColTeDrejtat)
        //                {
        //                    if (mesazh.Status)
        //                    {
        //                        o.IdPerdorues = grup.IdGrupiPerdorues;
        //                        if (!ekzistonEDrejta(o.IdNderViti, o.IdKomponente, o.IdAmbjentiModuli, o.IdModul, o.IdDrejtaVeprim, o.IdPerdorues))
        //                            mesazh = ruajTeDrejte(o.IdDrejta, o.IdNderViti, o.IdKomponente, o.IdModul, o.IdPerdorues, o.IdDrejtaVeprim, o.PerdoruesApoGrup);
        //                    }
        //                    else { dbManager.Transaction.Rollback(); return mesazh; }
        //                }
        //                if (mesazh.Status)
        //                {
        //                    dbManager.CommitTransaction();
        //                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //                    return mesazh;
        //                }
        //                else
        //                {
        //                    dbManager.Transaction.Rollback();
        //                    return mesazh;
        //                }
        //            }
        //            else
        //            {
        //                mesazh = new clsMesazh(false, mesazh.PershkrimMesazhi);
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            return new clsMesazh(false, "Ekziston nje grup me te njejtin kod!");
        //        }
        //    }

        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh ruajGrupPerdoruesish(out int idgrupiperdorues, String grupiperdorueskodi, String grupiperdoruespershkrimi, bool grupiaktivperdorues, DateTime grupiperdoruesdata, int idperdoruesi)
        {
            idgrupiperdorues = -1;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDGRUPIPERD", idgrupiperdorues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@GRUPIKODI", grupiperdorueskodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPIPERDPERSHK", grupiperdoruespershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GRUPIPERDAKTIV", grupiaktivperdorues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GRUPIPERDDATA", grupiperdoruesdata, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_ins");
            idgrupiperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajGrupPerdoruesish(out int idgrupiperdorues, String grupiperdorueskodi, String grupiperdoruespershkrimi, bool grupiaktivperdorues, DateTime grupiperdoruesdata, int idperdoruesi)", true)]
        //public clsMesazh ruajGrupPerdoruesish(clsGrupiPerdorues grupi)
        //{
        //    try
        //    {

        //            dbManager.CreateParameters(6);
        //            dbManager.AddParameters(0, "@IDGRUPIPERD", grupi.IdGrupiPerdorues, ParameterDirection.Output);
        //            dbManager.AddParameters(1, "@GRUPIKODI", grupi.GrupiPerdoruesKodi, ParameterDirection.Input);
        //            dbManager.AddParameters(2, "@GRUPIPERDPERSHK", grupi.GrupiPerdoruesPershkrimi, ParameterDirection.Input);
        //            dbManager.AddParameters(3, "@GRUPIPERDAKTIV", grupi.GrupiAktivPerdorues, ParameterDirection.Input);
        //            dbManager.AddParameters(4, "@GRUPIPERDDATA", grupi.GrupiPerdoruesData, ParameterDirection.Input);
        //            dbManager.AddParameters(5, "@IDPERDORUESI", grupi.IdPerdoruesi, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_ins");
        //            grupi.IdGrupiPerdorues = int.Parse(dbManager.Parameters[0].Value.ToString());

        //            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        //[Obsolete("Perdor nga klasa perkatese: clsMesazh modifikoGrupTeDrejta(clsGrupiPerdorues grup)", true)]
        //public clsMesazh modifikoGrupTeDrejta(clsGrupiPerdorues grup)
        //{
        //    clsMesazh mesazh = new clsMesazh();
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    try
        //    {
        //        mesazh = modifikoGrupPerdoruesish(grup.IdGrupiPerdorues, grup.GrupiPerdoruesKodi, grup.GrupiPerdoruesPershkrimi, grup.GrupiAktivPerdorues, grup.GrupiPerdoruesData, grup.IdPerdoruesi);
        //       if (mesazh.Status)
        //       {
        //           mesazh = fshiTeDrejtaGrupPerdorues(grup.IdGrupiPerdorues);


        //           foreach (clsTeDrejtat o in grup.OColTeDrejtat)
        //           {
        //               if (mesazh.Status)
        //               {
        //                   o.IdPerdorues = grup.IdGrupiPerdorues;
        //                   if (!ekzistonEDrejta(o.IdNderViti, o.IdKomponente, o.IdAmbjentiModuli, o.IdModul, o.IdDrejtaVeprim, o.IdPerdorues))
        //                       mesazh = ruajTeDrejte(o.IdDrejta, o.IdNderViti, o.IdKomponente, o.IdModul, o.IdPerdorues, o.IdDrejtaVeprim, o.PerdoruesApoGrup);
        //               }
        //               else
        //               {
        //                   dbManager.Transaction.Rollback();

        //                   return mesazh;
        //               }
        //           }
        //           if (mesazh.Status)
        //           {
        //               dbManager.CommitTransaction();
        //               mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //               return mesazh;
        //           }
        //           else
        //           {
        //               dbManager.Transaction.Rollback();

        //               return mesazh;
        //           }
        //       }
        //       else
        //       {
        //           dbManager.Transaction.Rollback();
        //           return mesazh;
        //       }
        //    }

        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoGrupPerdoruesish(int idgrupiperdorues, String grupiperdorueskodi, String grupiperdoruespershkrimi, bool grupiaktivperdorues, DateTime grupiperdoruesdata, int idperdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDGRUPIPERD", idgrupiperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GRUPIKODI", grupiperdorueskodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRUPIPERDPERSHK", grupiperdoruespershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GRUPIPERDAKTIV", grupiaktivperdorues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GRUPIPERDDATA", grupiperdoruesdata, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_upd");
            idgrupiperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoGrupPerdoruesish(int idgrupiperdorues, String grupiperdorueskodi, String grupiperdoruespershkrimi, bool grupiaktivperdorues, DateTime grupiperdoruesdata, int idperdoruesi)", true)]
        //public clsMesazh modifikoGrupPerdoruesish(clsGrupiPerdorues grupi)
        //{
        //    try
        //    {
        //        //komentoi edi, sepse nuk do lejohet modifikimi i kodi te grupit te perdoruesit
        //        ////if (!ekzistonGrupPerdoruesiMeKeteKod(grupi))
        //        ////{
        //            dbManager.CreateParameters(6);
        //            dbManager.AddParameters(0, "@IDGRUPIPERD", grupi.IdGrupiPerdorues, ParameterDirection.Input);
        //            dbManager.AddParameters(1, "@GRUPIKODI", grupi.GrupiPerdoruesKodi, ParameterDirection.Input);
        //            dbManager.AddParameters(2, "@GRUPIPERDPERSHK", grupi.GrupiPerdoruesPershkrimi, ParameterDirection.Input);
        //            dbManager.AddParameters(3, "@GRUPIPERDAKTIV", grupi.GrupiAktivPerdorues, ParameterDirection.Input);
        //            dbManager.AddParameters(4, "@GRUPIPERDDATA", grupi.GrupiPerdoruesData, ParameterDirection.Input);
        //            dbManager.AddParameters(5, "@IDPERDORUESI", grupi.IdPerdoruesi, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_upd");
        //            grupi.IdGrupiPerdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
        //            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            return mesazh;
        //    }

        //    catch (Exception)
        //    {

        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        //internal clsMesazh fshiTeDrejtaGrupPerdorues(int idgrupiperdorues)
        //{
        //    string sqlstr = "";

        //    sqlstr = "delete from  T_DREJTAT" +
        //                      " where IDPERDORUES =" + idgrupiperdorues +
        //                      " and PERDORUESAPOGRUP = 1";

        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }

        //    dbManager.Open();
        //    DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //    clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //    return mesazh;

        //}

        internal clsMesazh fshiGrupPerdoruesish(int idgrupiperdorues)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            if (!ekzistonPerdoruesPerKeteGrup(idgrupiperdorues))
            {
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDGRUPIPERD", idgrupiperdorues, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_del");
                clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                return mesazh;
            }
            else
            {
                return new clsMesazh(false, "Ekziston nje perdorues per kete grup!");
            }

        }
        
        /// <summary>
        /// Perditeson IdStatusDok dhe DtModifikimi ne T_ROLI dhe shton rreshtin perkates te rolit ne T_ROLI_HISTORIK me IDPERDORUESI parametrin qe i kalohet
        /// </summary>
        /// <param name="idRoli"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public bool fshiRolStatus(int idRoli, int idPerdoruesi)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLI_upddel");
            return true;
        }


        internal DataTable merrGrupPerdoruesGjitheNdermarrjet(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPIPERD", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_ktheGrupPerdorues");
            return ds.Tables[0];

        }

        internal DataRow merrGrupPerdorues(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPIPERD", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_ktheGrupPerdorues");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheGjitheGrupetPerdoruesve()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_merrGjitheGrupetPerdoruesve");
            return ds.Tables[0];

        }

        internal DataTable ktheGjitheGrupetPerdoruesvePozitive()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_merrGjitheGrupetPerdoruesvePozitive");
            return ds.Tables[0];

        }
        internal bool ekzistonPerdoruesPerKeteGrup(int idgrupiperdorues)
        {
            int nrPerdoruesish;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPIPERD", idgrupiperdorues, ParameterDirection.Input);
            nrPerdoruesish = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_EkzistonPerdoruesPerKeteGrup");
            if (nrPerdoruesish > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal bool ekzistonGrupPerdoruesiMeKeteKod(String grupiperdorueskodi)
        {
            int nrGrupPerdoruesish;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@GRUPIKODI", grupiperdorueskodi, ParameterDirection.Input);
            nrGrupPerdoruesish = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_GRUPIPERDORUES_EkzistonGupPerdoruesiMeKeteKod");
            if (nrGrupPerdoruesish > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable merrRoletDT(int idlicenca, int idperdorues)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_merrRoletDT");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable merrRoletDTJoSuper(int idlicenca, int idperdorues)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_merrRoletDTJoSuper");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;

        }
        public DataTable merrRoletDTSipasLidhjes(int idlicenca, int idperdorues, int idperdoruesroli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDPERDORUESRoli", idperdoruesroli, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_merrRoletDTSipasLidhjes");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }
        /// <summary>
        /// lexon nga db-ja rolin sipas id-se
        /// </summary>
        /// <param name="idRoli">id-ja rolit per tu lexuar</param>
        /// <returns>kthen nje DataRow roli</returns>
        public DataRow merrRolDR(int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLI_merrRolDR");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;
        }
        #endregion

        #region USERTRACK

        /// <summary>
        /// Ruan objektin e veprimit ne tabelen T_USERTRACK ne databaze
        /// </summary>
        /// <param name="S"></param>
        /// <param name="userId">null ne rastin kur login-i nuk eshte i suksesshem</param>
        /// <param name="inUser"></param>
        /// <param name="ipAdr"></param>
        /// <param name="username">null ne rastin kur login-i eshte i suksesshem</param>
        /// <param name="arsyeLoginFail">null ne rastin kur login-i eshte i suksesshem</param>
        /// <returns>kthen true nqs ruajtja u krye me sukses, false perndryshe</returns>
        public bool ruajUserTrack(string SESSIONID, int userId, string inUser, string ipAdr, bool aktiv, string username, string arsyeLoginFail)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@SESSIONID", SESSIONID, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", userId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LOGINDATETIME", Convert.ToDateTime(inUser), ParameterDirection.Input);
            dbManager.AddParameters(3, "@IP", ipAdr, ParameterDirection.Input);
            dbManager.AddParameters(4, "@Aktiv", aktiv, ParameterDirection.Input);
            dbManager.AddParameters(5, "@username", username, ParameterDirection.Input);
            dbManager.AddParameters(6, "@arsyeLoginFail", arsyeLoginFail, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_USERTRACK_ins");
            return true;

        }

        /// <summary>
        /// merr te dhenat nga tabela T_USERTRACK per idPerdoruesin qe eshte aktiv
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        internal DataRow ktheUserTrack(int idperdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_USERTRACK_selSipasPerdoruesit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            if (ds.Tables[0].Rows.Count > 1)
                throw new MyException("Nuk duhet te kete me shume se nje perdorues");
            return ds.Tables[0].Rows[0];
            //return ds.Tables[0];

        }

        /// <summary>
        /// kontrollon nese ky perdorues eshte i loguar me pare ne sistem
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <returns></returns>
        internal bool perdoruesILoguar(int idPerdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            object nr = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_USERTRACK_selNrOnlineSipasIdPerdorues");
            if (Convert.ToInt32(nr) > 0) return true;
            else return false;

        }

        /// <summary>
        /// kontrollon nese perdoruesi po logohet per here te pare
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <returns></returns>
        internal bool perdoruesFirstLoggedIn(int idPerdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPEDORUES", idPerdorues, ParameterDirection.Input);
            object nr = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_USERTRACK_selNrOnlineSipasIdPerdorues");
            if (Convert.ToInt32(nr) > 0) return true;
            else return false;

        }

        /// <summary>
        /// update-on nxjerrjen logout te perdoruesit ne tabelen T_USERTRACK
        /// </summary>
        /// <param name="S">SessionID te perdoruesit qe eshte bere logout</param>
        /// <returns>kthen true nese update-i behet me sukses, false perndryshe</returns>
        internal bool modifikoOnlineUser(String S)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SESSIONID", S, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@LOGOUTDATETIME", Convert.ToDateTime(outUser), ParameterDirection.Input);
            //dbManager.AddParameters(2, "@IP", objOnlineUser.IpAdress, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_USERTRACK_upd");
            return true;


        }
        //[Obsolete("Perdor: bool modifikoOnlineUser(String S, DateTime outUser)", true)]
        //public bool modifikoOnlineUser(clsTrackUser objOnlineUser)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@SESSIONID", objOnlineUser.SessionID, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@LOGOUTDATETIME", objOnlineUser.LogoutDatetime, ParameterDirection.Input);
        //        //dbManager.AddParameters(2, "@IP", objOnlineUser.IpAdress, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_USERTRACK_upd");
        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal bool modifikoOnlineUserAllOffline(String outUser)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@LOGOUTDATETIME", Convert.ToDateTime(outUser), ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_USERTRACK_updAllOffline");
            return true;


        }
        //[Obsolete("Perdor: bool modifikoOnlineUserAllOffline(DateTime outUser)", true)]
        //public bool modifikoOnlineUserAllOffline(clsTrackUser objOnlineUser)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.CreateParameters(1);
        //    dbManager.AddParameters(0, "@LOGOUTDATETIME", Convert.ToDateTime(objOnlineUser.LogoutDatetime), ParameterDirection.Input);
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_USERTRACK_updAllOffline");
        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal int Gjendet(String perdoruesUsername, String perdoruesPassword)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesUsername, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERDORUESPASSWORD", perdoruesPassword, ParameterDirection.Input);
            return (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_selCount");

        }
        //[Obsolete("Perdor: Gjendet(String perdoruesUsername, String perdoruesPassword)", true)]
        //public int Gjendet(clsPerdorues perdorues)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdorues.PerdoruesUsername, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@PERDORUESPASSWORD", perdorues.PerdoruesPassword, ParameterDirection.Input);
        //        return (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_selCount");
        //    }
        //    catch (Exception)
        //    {
        //        return -1;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheUserNgaLogin(string perdoruesUsername)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesUsername, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@PERDORUESPASSWORD", perdorues.PerdoruesPassword, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_sel");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            
            return ds.Tables[0];

        }
        internal DataTable ktheUserNgaLoginGmail(string perdoruesUsername)
        {


            var dbManager = MyScopeDbManager;
            string queryString = "";
            dbManager.Open();
            queryString = $"SELECT * FROM T_PERDORUESI where PERDORUESUSERNAME = '{perdoruesUsername}' and PERDORUESAKTIV = 1 and IDSTATUSDOK = 1";
            CommandType commandType = CommandType.Text;
            DataSet ds = dbManager.ExecuteDataSet(commandType, queryString);
            return ds.Tables[0];

        }


        internal DataTable ktheUserNgaLoginMeUsernameOseEmail(string perdoruesUsername, string email)
        {
            var dbManager = MyScopeDbManager;
            string queryString = "";
            dbManager.Open();
            if (perdoruesUsername == "")
                queryString = $"SELECT top 1 * FROM T_PERDORUESI as p INNER JOIN T_STILRAPORTI  AS s ON  p.IDSTILRAPORTI = s.IDSTILI WHERE PERDORUESEMAIL = '{email}' and IDSTATUSDOK = 1 AND PERDORUESAKTIV = 1";
            else
                queryString = $"SELECT top 1 * FROM T_PERDORUESI as p INNER JOIN T_STILRAPORTI  AS s ON  p.IDSTILRAPORTI = s.IDSTILI WHERE PERDORUESUSERNAME = '{perdoruesUsername}' and PERDORUESEMAIL = '{email}' and IDSTATUSDOK = 1 AND PERDORUESAKTIV = 1";
            CommandType commandType = CommandType.Text;
            DataSet ds = dbManager.ExecuteDataSet(commandType, queryString);
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen te dhenat e perdoruesit ne baze te username. Te dhenat merren nga tabela T_PERDORUESI
        /// therret metoden: 
        /// <see cref="clsDatabaseAdmin.ktheUserNgaLogin"/>
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        internal DataRow kthePerdoruesSipasUsername(string username)
        {
            DataTable dt = ktheUserNgaLogin(username);
            if (dt == null)
                return null;
            if (dt.Rows.Count == 0 || dt.Rows.Count > 1)
                return null;
            return dt.Rows[0];
        }
        internal bool kthePerdoruesSipasUsername(string username, bool email)
        {
            DataTable dt = ktheUserNgaLoginGmail(username);
            if (dt == null)
                return false;
            if (dt.Rows.Count >= 1)
                return true;
            return false;
        }
        internal DataRow kthePerdoruesSipasUsernameOseEmail(string username,string email)
        {
            DataTable dt = ktheUserNgaLogin(username);
            if (dt == null)
                return null;
            if (dt.Rows.Count == 0 || dt.Rows.Count > 1)
                return null;
            return dt.Rows[0];
        }

        internal DataTable ktheUserNgaLoginAll(string perdoruesUsername)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdoruesUsername, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@PERDORUESPASSWORD", perdorues.PerdoruesPassword, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_selAll");
            //colPerdoruesit perdoruesit = new colPerdoruesit();
            //return perdoruesit.mbushArrayListPerdoruesit(ds, -1);
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheUserNgaLogin(clsPerdorues perdorues)", true)]
        //public colPerdoruesit merrUserNgaLogin(clsPerdorues perdorues)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@PERDORUESUSERNAME", perdorues.PerdoruesUsername, ParameterDirection.Input);
        //        //dbManager.AddParameters(1, "@PERDORUESPASSWORD", perdorues.PerdoruesPassword, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_sel");
        //        colPerdoruesit perdoruesit = new colPerdoruesit();
        //        return perdoruesit.mbushArrayListPerdoruesit(ds,-1);
        //    }
        //    catch (Exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ce.Message + ce.StackTrace);
        //        return new colPerdoruesit();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        public DataTable merrNdermarrjetEPerdoruesitDataTable(int id, bool merrVitet = false)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@MERRVITET", merrVitet, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERDORUESI_merrNdermarrjetPerdoruesit");
            return ds.Tables[0];

        }



        #endregion

        #region QYTETET

        internal clsMesazh ruajQytet(out int idqyteti, String kodiqyteti, String emriqyteti, int idndermarja, int idperdoruesi, int idstatusdok)
        {
            idqyteti = -1;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            if (ekzistonQytetMeKeteKod(kodiqyteti, idndermarja))
                return new clsMesazh(false, "Ekziston nje qytet me kete kod!");


            if (ekzistonQytetMeKeteEmer(emriqyteti, idndermarja))
                return new clsMesazh(false, "Ekziston nje qytet me kete emer!");
            //if (mesazh.Status)
            //{
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@QYTETI_ID", idqyteti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@QYTETI", emriqyteti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODI", kodiqyteti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE ", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_ins");
            idqyteti = Convert.ToInt32(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);



            //       {
            //         return new clsMesazh(false, mesazh.PershkrimMesazhi);
            //}

        }

        //[Obsolete("Perdor: clsMesazh ruajQytet(out int idqyteti, String kodiqyteti, String emriqyteti, int idndermarja, int idndermvit, int idperdoruesi)", true)]
        //public clsMesazh ruajQytet(clsQyteti qyteti)
        //    {


        //    {
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        clsMesazh mesazh = ekzistonQytetMeKeteKod(qyteti.KodiQyteti, qyteti.IdNdermarja);
        //        if (!mesazh.Status)
        //        {
        //            dbManager.CreateParameters(6);
        //            dbManager.AddParameters(0, "@QYTETI_ID", qyteti.IdQyteti, ParameterDirection.Output);
        //            dbManager.AddParameters(1, "@QYTETI", qyteti.EmriQyteti, ParameterDirection.Input);
        //            dbManager.AddParameters(2, "@KODI", qyteti.KodiQyteti, ParameterDirection.Input);
        //            dbManager.AddParameters(3, "@IDNDERMARJE ", qyteti.IdNdermarja, ParameterDirection.Input);
        //            dbManager.AddParameters(4, "@IDNDERVITI", qyteti.IdNderViti, ParameterDirection.Input);
        //            dbManager.AddParameters(5, "@IDPERDORUESI", qyteti.IdPerdoruesi, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_ins");
        //            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        //            return mesazh;
        //        }
        //        else
        //        {
        //            return new clsMesazh(false, mesazh.PershkrimMesazhi);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    //finally
        //    //{
        //    //    dbManager.Dispose();
        //    //}
        //}

        internal clsMesazh modifikoQytet(int idqyteti, String kodiqyteti, String emriqyteti, int idndermarja, int idperdoruesi, int idstatusdok)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@QYTETI_ID", idqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@QYTETI", emriqyteti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODI", kodiqyteti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE ", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        internal int ktheIdQytetiSipasEmritDheNdermarrjes(string emerQyteti, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@QYTETIEMRI", emerQyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@INDERMARJE", idndermarje, ParameterDirection.Input);
            object id = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ktheIdQytetiSipasEmritDheNdermarrjes");
            return (id == null) ? -1 : Convert.ToInt32(id);
        }
        //[Obsolete("Perdor: clsMesazh modifikoQytet(int idqyteti, String kodiqyteti, String emriqyteti, int idndermarja, int idndermvit, int idperdoruesi)", true)]
        //public clsMesazh modifikoQytet(clsQyteti qyteti)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(6);
        //        dbManager.AddParameters(0, "@QYTETI_ID", qyteti.IdQyteti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@QYTETI", qyteti.EmriQyteti, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@KODI", qyteti.KodiQyteti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE ", qyteti.IdNdermarja, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDNDERVITI", qyteti.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", qyteti.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }

        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh fshiQytet(int idqyteti)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@QYTETI_ID", idqyteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh fshiQytetStatus(int idqyteti, int idperdorues)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@QYTETI_ID", idqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh fshiQytet(int idqyteti)", true)]
        //public clsMesazh fshiQytet(clsQyteti qyteti)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@QYTETI_ID", qyteti.IdQyteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal void merrQytetPakthim(int idqyteti)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@QYTETI_ID", idqyteti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_ktheQytet");

        }
        //[Obsolete("Perdor: void merrQytet(int idqyteti)", true)]
        //public void merrQytet(clsQyteti qyteti)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@QYTETI_ID", qyteti.IdQyteti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_QYTETI_ktheQytet");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrQytet(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDQYTETI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_ktheQytet");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrQytetSipasPershkrimit(string pershkrimqyteti, int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@QYTETIEMRI", pershkrimqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@INDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_ktheQytetSipasPershkrimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrQytet(int id)", true)]
        //public colQytetet ktheQytet(int id)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDQYTETI", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_ktheQytet");
        //        colQytetet qytetet = new colQytetet();
        //        return qytetet.mbushArrayListQytetet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colQytetet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        //public colQytetet merrGjitheQytetet()
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_merrGjitheQytetet");
        //        colQytetet qytetet = new colQytetet();
        //        return qytetet.mbushArrayListQytetet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colQytetet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        //sherben per te evituar qe te merret nga databaza rreshti qe perdoret tek filtri i kolonave
        //si  (...)
        internal DataTable ktheGjitheQytetetPozitive(int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_merrGjitheQytetetPozitive");
            return ds.Tables[0];

        }

        internal DataTable ktheQytetePerEksport(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QytetePerEksport");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }
        internal DataTable ktheGjithePajisjet()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            //dbManager.CreateParameters(1);
            //dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PAJISJE_merrGjithePajisjet");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheQytetetPozitive(int idndermarje)", true)]
        //public colQytetet merrGjitheQytetetPozitive(int idndermarje)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure , "prc_T_QYTETI_merrGjitheQytetetPozitive");
        //        colQytetet qytetet = new colQytetet();
        //        return qytetet.mbushArrayListQytetet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colQytetet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        internal DataTable ktheQytetetNdermarrjes(int idNdermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_merrQytetetNdermarrjes");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheQytetetNdermarrjes(int idNdermarje)", true)]
        //public colQytetet merrQytetetNdermarrjes(int idNdermarje)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_merrQytetetNdermarrjes");
        //        colQytetet qytetet = new colQytetet();
        //        return qytetet.mbushArrayListQytetet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colQytetet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal bool ekzistonQytetMeKeteKod(String kodiqyteti, int idndermarja)
        {
            int nrQytetesh;

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@QYTETIKODI", kodiqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            nrQytetesh = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ekzistonQyteti");
            if (nrQytetesh > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        internal bool ekzistonQytetMeKeteEmer(String emerqyteti, int idndermarja)
        {
            int nrQytetesh;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@QYTETIEMER", emerqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            nrQytetesh = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ekzistonQytetiEmri");
            if (nrQytetesh > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //[Obsolete("Perdor: clsMesazh ekzistonQytetMeKeteKod(String kodiqyteti, int idndermarja)", true)]
        //private clsMesazh ekzistonQytetMeKeteKod(clsQyteti qyteti)
        //{
        //    int nrQytetesh;
        //    try
        //    {
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@QYTETIKODI", qyteti.KodiQyteti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", qyteti.IdNdermarja, ParameterDirection.Input);
        //        nrQytetesh = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ekzistonQyteti");
        //        if (nrQytetesh > 0)
        //        {
        //            return new clsMesazh(true, "Ekziston nje qytet me kete kod!");
        //        }
        //        else
        //        {
        //            return new clsMesazh(false);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(true, ce.Message);
        //    }
        //}


        /// <summary>
        /// Kjo metode kthen true nqs qyteti eshte i lidhur, pra ka nje ndermarrje apo klient/furnitor me kete qytet
        /// dhe false ne rast te kundert.
        /// </summary>
        /// <param name="idQyteti"></param>
        /// <returns></returns>
        internal bool eshteQytetILidhur(int idQyteti)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDQYTETI", idQyteti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_QYTETI_EshteILidhur");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        internal int ktheIdQytetMeKeteEmer(string emerQyteti, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@QYTETIEMER", emerQyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ktheIdQytetSipasEmritDheNdermarrjes").ToString());
        }

        public bool ekzistonQyteti(int idQyteti)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDQYTETI", idQyteti, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ekzistonQytetiSipasID"));
            return Convert.ToBoolean(pergjigje);

        }

        //idQytetSipasEmer


        //idQytetMeKeteEmer
        internal bool EkzistonQytetMeKetePershkrim(String emerqyteti, int idndermarja, bool shtim, int idqyteti)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@QYTETIEMER", emerqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@shtim", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idqyteti", idndermarja, ParameterDirection.Input);
            int pershkrimi = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ekzistonQytetiPershkrimi"));
            return Convert.ToBoolean(pershkrimi);


        }
        //idQytetMeKeteKod
        internal bool EkzistonQytetMeKeteKod(String kodqyteti, int idndermarja, bool shtim, int idqyteti)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@QYTETIKODI", kodqyteti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(2, "@shtim", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idqyteti", idndermarja, ParameterDirection.Input);
            int kodi = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_QYTETI_ekzistonQytetiKodi"));
            return Convert.ToBoolean(kodi);
        }



        #endregion

        #region MENYRAT E TRANSPORTIT

        internal clsMesazh ruajMenyreTransporti(int id, String kodi, String pershkrimi, int idnderm, int idperdoruesi, int idstatusdok)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            if (!ekzistonMenyreTransportiMeKeteKod(kodi, idnderm))
            {
                dbManager.CreateParameters(6);
                dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODIMENYRETRANSPORTI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@PERSHKRIMIMENYRETRANSPORTI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ins");
                clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            else return new clsMesazh(false, "Ekziston nje menyre transporti me kete kod");

        }
        //[Obsolete("Perdor: clsMesazh ruajMenyreTransporti(int id, String kodi, String pershkrimi, int idnderm)", true)]
        //public clsMesazh ruajMenyreTransporti(clsMenyreTransporti transporti)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        if (!ekzistonMenyreTransportiMeKeteKod(transporti.KodiMenyreTransporti, transporti.IdNdermarje))
        //        {                    
        //            dbManager.CreateParameters(4);
        //            dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", transporti.IdMenyreTransporti, ParameterDirection.Output);
        //            dbManager.AddParameters(1, "@KODIMENYRETRANSPORTI", transporti.KodiMenyreTransporti, ParameterDirection.Input);
        //            dbManager.AddParameters(2, "@PERSHKRIMIMENYRETRANSPORTI", transporti.PershkrimiMenyreTransporti, ParameterDirection.Input);
        //            dbManager.AddParameters(3, "@IDNDERMARJE", transporti.IdNdermarje, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ins");
        //            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            return mesazh;
        //        }
        //        else return new clsMesazh(false, "Ekziston nje menyre transporti me kete kod");
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoMenyreTransporti(int id, String kodi, String pershkrimi, int idnderm, int idperdoruesi, int idstatusdok)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIMENYRETRANSPORTI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIMENYRETRANSPORTI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoMenyreTransporti(int id, String kodi, String pershkrimi, int idnderm)", true)]
        //public clsMesazh modifikoMenyreTransporti(clsMenyreTransporti transporti)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", transporti.IdMenyreTransporti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODIMENYRETRANSPORTI", transporti.KodiMenyreTransporti, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMIMENYRETRANSPORTI", transporti.PershkrimiMenyreTransporti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", transporti.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }

        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh fshiMenyreTransporti(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh fshiMenyreTransportiStatus(int id, int idperdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh fshiMenyreTransporti(int id)", true)]
        //public clsMesazh fshiMenyreTransporti(clsMenyreTransporti transporti)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", transporti.IdMenyreTransporti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheMenyratTransportit(int idNdermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_merrGjitheMenyratTransportit");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheMenyratTransportit(int idNdermarje)", true)]
        //public colMenyraTransporti merrMenyratTransportit(int idNdermarje)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_merrGjitheMenyratTransportit");
        //        colMenyraTransporti transporti = new colMenyraTransporti();
        //        return transporti.mbushArrayListMenyraTransporti(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colMenyraTransporti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrMenyreTransporti(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            ImbLogger.LogTraceShitje($"Filloi metoda merr menyre Transporti me id: {id}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ktheMenyreTransporti");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            ImbLogger.LogTraceShitje($"Mbaroi metoda merr menyre Transporti me id: {id}");
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrMenyreTransporti(int id)", true)]
        //public colMenyraTransporti ktheMenyreTransporti(int id)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDMENYRETRANSPORTI", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ktheMenyreTransporti");
        //        colMenyraTransporti transporti = new colMenyraTransporti();
        //        return transporti.mbushArrayListMenyraTransporti(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colMenyraTransporti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrMenyreTransportiSipasKodit(string kodi, int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIMENYRETRANSPORTI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ktheMenyreTransportiSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrMenyreTransportiSipasKodit(string kodi, int idndermarje)", true)]
        //public colMenyraTransporti ktheMenyreTransportiSipasKodit(string kodi, int idndermarje)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODIMENYRETRANSPORTI", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE",idndermarje, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ktheMenyreTransportiSipasKodit");
        //        colMenyraTransporti transporti = new colMenyraTransporti();
        //        return transporti.mbushArrayListMenyraTransporti(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colMenyraTransporti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal bool ekzistonMenyreTransportiMeKeteKod(String kodi, int idnderm)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ekzistonMenyreTransportiMeKeteKod");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        //[Obsolete("Perdor: bool ekzistonMenyreTransportiMeKeteKod(String kodi, int idnderm)", true)]
        //private bool ekzistonMenyreTransportiMeKeteKod(clsMenyreTransporti menyre)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODI", menyre.KodiMenyreTransporti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", menyre.IdNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENYRATRANSPORTI_ekzistonMenyreTransportiMeKeteKod");
        //        if (ds.Tables[0].Rows.Count == 1)
        //            return true;
        //        else if (ds.Tables[0].Rows.Count == 0)
        //            return false;
        //        else return true;
        //    }
        //    catch (Exception)
        //    {
        //        return true;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region KUSHTET E DERGIMIT

        internal clsMesazh ruajKushtDergimi(int id, String kodi, String pershkrimi, int idnderm, int idperdoruesi, int idstatusdok)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            if (!ekzistonKushtDergimiMeKeteKod(kodi, idnderm))
            {
                dbManager.CreateParameters(6);
                dbManager.AddParameters(0, "@IDKUSHTDERGIMI", id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODIKUSHTDERGIMI", kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@PERSHKRIMIKUSHTDERGIMI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ins");
                clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            else return new clsMesazh(false, "Ekziston nje kusht dergimi me kete kod!");

        }
        //[Obsolete("Perdor: clsMesazh ruajKushtDergimi(int id, String kodi, String pershkrimi, int idnderm)", true)]
        //public clsMesazh ruajKushtDergimi(clsKushtDergimi kusht)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        if (!ekzistonKushtDergimiMeKeteKod(kusht.KodiKushtDergimi, kusht.IdNdermarje))
        //        {                    
        //            dbManager.CreateParameters(4);
        //            dbManager.AddParameters(0, "@IDKUSHTDERGIMI", kusht.IdKushtDergimi, ParameterDirection.Output);
        //            dbManager.AddParameters(1, "@KODIKUSHTDERGIMI", kusht.KodiKushtDergimi, ParameterDirection.Input);
        //            dbManager.AddParameters(2, "@PERSHKRIMIKUSHTDERGIMI", kusht.PershkrimiKushtDergimi, ParameterDirection.Input);
        //            dbManager.AddParameters(3, "@IDNDERMARJE", kusht.IdNdermarje, ParameterDirection.Input);
        //            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ins");
        //            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //            return mesazh;
        //        }
        //        else return new clsMesazh(false, "Ekziston nje kusht dergimi me kete kod!");
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoKushtDergimi(int id, String kodi, String pershkrimi, int idnderm, int idperdoruesi, int idstatusdok)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKUSHTDERGIMI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIKUSHTDERGIMI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIKUSHTDERGIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoKushtDergimi(int id, String kodi, String pershkrimi, int idnderm)", true)]
        //public clsMesazh modifikoKushtDergimi(clsKushtDergimi kusht)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDKUSHTDERGIMI", kusht.IdKushtDergimi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@KODIKUSHTDERGIMI", kusht.KodiKushtDergimi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMIKUSHTDERGIMI", kusht.PershkrimiKushtDergimi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", kusht.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }

        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh fshiKushtDergimi(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTDERGIMI", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh fshiKushtDergimiStatus(int id, int idperdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKUSHTDERGIMI", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        //[Obsolete("Perdor: clsMesazh fshiKushtDergimi(int id)", true)]
        //public clsMesazh fshiKushtDergimi(clsKushtDergimi kusht)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKUSHTDERGIMI", kusht.IdKushtDergimi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;

        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheKushtetDergimit(int idNdermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_merrGjitheKushtetDegimit");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheKushtetDergimit(int idNdermarje)", true)]
        //public colKushteDergimi merrKushtetDergimit(int idNdermarje)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_merrGjitheKushtetDegimit");
        //        colKushteDergimi kushte = new colKushteDergimi();
        //        return kushte.mbushArrayListKushteDergimi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKushteDergimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrKushtDergimi(int id)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            ImbLogger.LogTraceShitje($"Filloi metoda merr kusht dergimi me Id:{id}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTDERGIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ktheKushtDergimi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            ImbLogger.LogTraceShitje($"Mbaroi metoda merr kusht dergimi me Id:{id}");
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrKushtDergimi(int id)", true)]
        //public colKushteDergimi ktheKushtDergimi(int id)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKUSHTDERGIMI", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ktheKushtDergimi");
        //        colKushteDergimi kushte = new colKushteDergimi();
        //        return kushte.mbushArrayListKushteDergimi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKushteDergimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrKushtDergimiSipaKodit(string kodi, int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIKUSHTDERGIMI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ktheKushtDergimiSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrKushtDergimiSipaKodit(string kodi, int idndermarje)", true)]
        //public colKushteDergimi ktheKushtDergimiSipaKodit(string kodi, int idndermarje)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODIKUSHTDERGIMI", kodi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE",idndermarje, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ktheKushtDergimiSipasKodit");
        //        colKushteDergimi kushte = new colKushteDergimi();
        //        return kushte.mbushArrayListKushteDergimi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKushteDergimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal bool ekzistonKushtDergimiMeKeteKod(String kodi, int idnderm)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ekzistonKushtDergimiMeKeteKod");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        //[Obsolete("Perdor: bool ekzistonKushtDergimiMeKeteKod(String kodi, int idnderm)", true)]
        //private bool ekzistonKushtDergimiMeKeteKod(clsKushtDergimi kusht)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODI", kusht.KodiKushtDergimi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", kusht.IdNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEDERGIMI_ekzistonKushtDergimiMeKeteKod");
        //        if (ds.Tables[0].Rows.Count == 1)
        //            return true;
        //        else if (ds.Tables[0].Rows.Count == 0)
        //            return false;
        //        else return true;
        //    }
        //    catch (Exception)
        //    {
        //        return true;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region TE DREJTAT



        /// <summary>
        /// merr te drejten me id 
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <returns></returns>
        public DataRow merrTeDrejte(int idDrejta)
        {


            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDDREJTA", idDrejta, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_selectMeID");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;

        }

        public clsMesazh modifikoGridaTrupi(int idTrupi, int idKoka, int indexTrupi, bool visibleTrupi, string kodiTrupi, string pershkrimi_sq, string pershkrimi_en, string pershkrimi_fr, bool readonlyTrupi, int widthTrupi, int indexOrigjinal, int idKonfigambienteLupa, bool visiblecostumize, int idGjuha, int tipi, bool shfaqmobile, double renditjamobile, int llojformatfushe)
        {
            dbManager.Open();
            dbManager.CreateParameters(18);
            dbManager.AddParameters(0, "@GRIDATRUPIID", idTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GRIDAKOKAID", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRIDATRUPIINDEX", indexTrupi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GRIDATRUPIVISIBLE", visibleTrupi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GRIDATRUPIKODI", kodiTrupi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@GRIDATRUPIPERSHKRIMI", pershkrimi_sq, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GRIDATRUPIREADONLY", readonlyTrupi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@GRIDATRUPIWIDTH", widthTrupi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@GRIDATRUPIINDEXORIGJINAL", indexOrigjinal, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKONFIGLUPA", idKonfigambienteLupa, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VISIBLECOSTUMIZE", visiblecostumize, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(12, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@SHFAQMOBILE", shfaqmobile, ParameterDirection.Input);
            dbManager.AddParameters(14, "@RENDITJAMOBILE", renditjamobile, ParameterDirection.Input);
            dbManager.AddParameters(15, "@LLOJFORMATFUSHE", llojformatfushe, ParameterDirection.Input);
            dbManager.AddParameters(16, "@GRIDATRUPIPERSHKRIMI_en", pershkrimi_en, ParameterDirection.Input);
            dbManager.AddParameters(17, "@GRIDATRUPIPERSHKRIMI_fr", pershkrimi_fr, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_upd");
            idTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        public int merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(int idKonfigAmbjenti, string kodi)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@EMERFUSHE", kodi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_MerrKonfigurimLupeSipasKonfigAmbjentiDheKodit"));
        }

        /// <summary>
        /// merr te drejten me id 
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <returns></returns>
        public DataRow merrTeDrejteSipasRolNdermVitKomp(int idRoli, int idNderm, int idViti, int idKomp)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}

            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDKOMPONENTE", idKomp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtenSipasRolNdermVitKomp");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;

        }

        /// <summary>
        /// Merr nje koleksion te drejtash koka dhe behet ruajtja e te gjithe viteve te ndermarrjes per te
        /// Aktualisht menyra eshte qe cdo vit ruhet me vete ndaj count i koleksionit eshte nje
        /// Ruhet dhe ne historik
        /// </summary>
        /// <param name="dtTeDrejtatRolKoka">RolDrejtaKoka konvertuar ne datatable</param>
        /// <returns>Nese Modifikimi u krye me sukses ose jo</returns>
        public clsMesazh modifikoTeGjitheTeDrejtatKokeNeNje(DataTable dtTeDrejtatRolKoka)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@type", "prc_T_ROLDREJTAKOKA_DATATABLE_upd", ParameterDirection.Input);
            dbManager.AddParameters(1, "@drejtaRolKoka", dtTeDrejtatRolKoka, ParameterDirection.Input);
            int sukses = dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTAKOKA_DATATABLE_upd");
            return (sukses > 0) ? new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]) : new clsMesazh(false, "Ndodhi një gabim gjatë ruajtjes!");
        }
        /// <summary>
        /// Merr koleksionin e te gjithe te drejtave trup per cdo komponente/raport/tab dhe ben modifikimin e tyre
        /// Ruhet dhe ne historik
        /// </summary>
        /// <param name="dtTeDrejtatRolTrupi">RolDrejtaTrup konvertuar ne datatable</param>
        /// <returns>Nese Modifikimi u krye me sukses ose jo</returns>
        public clsMesazh modifikoTeGjitheTeDrejtatTrupNeNje(DataTable dtTeDrejtatRolTrupi)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(1);
            //dbManager.AddParameters(0, "@type", "prc_T_ROLDREJTATRUPI_DATATABLE_upd", ParameterDirection.Input);
            dbManager.AddParameters(0, "@drejtaRolTrupi", dtTeDrejtatRolTrupi, ParameterDirection.Input);
            int sukses = dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_DATATABLE_upd");
            return (sukses > 0) ? new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]) : new clsMesazh(false, "Ndodhi një gabim gjatë ruajtjes!");
        }

        public DataTable merrTeDrejtatRolTrupPerNiveleRegjistrimiTeNdryshme(int idNdermarrje, int idNdermarrjeDest, int idViti, int idVitiDest, int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(5);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJEDEST", idNdermarrjeDest, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDVITIDEST", idVitiDest, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDROLI", idRoli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtatRolTrupPerNiveleRegjistrimiTeNdryshme");
            return ds.Tables[0];
        }

        public bool kaTeDrejtaRoliPerNdermarrjeDheVit(int idRoli, int idNdermarrje, int idViti)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLDREJTA_kaTeDrejtaRoliPerNdermarrjeDheVit"));
            return Convert.ToBoolean(pergjigje);

        }
        
        /// <summary>
        /// Ruan Koken e tabeles se te drejtave
        /// </summary>
        /// <param name="idDrejta">parameter output</param>
        /// <param name="idNdermarrje">ndermarrja</param>
        /// <param name="idViti">viti</param>
        /// <param name="idRoli">roli</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh krijoTeDrejtaKokaNgaDefault(out int idDrejta, int idNdermarrje, int idViti, int idRoli)
        {
            idDrejta = -1;
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Output);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTAKOKA_ins_NgaDefault");
            idDrejta = Convert.ToInt32(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        /// <summary>
        /// Ruan trupin e tabeles se te drejtave vetem per komponentet qe i perkasin licenses qe i dergohet sp
        /// </summary>
        /// <param name="idDrejta">id e tabeles se kokes</param>
        /// <param name="licensa">id e llojit te licenses</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh krijoTeDrejtaKomponenteshNgaDefault(int idDrejta, int licensa)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDLLOJLICENCE", licensa, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERKOMP_NgaDefault");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        /// <summary>
        /// Ruan trupin e tabeles se te drejtave vetem per raportet, kapen automatikisht nga sp prindi i lidhur me to
        /// </summary>
        /// <param name="idDrejta">id e tabeles se kokes</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh krijoTeDrejtaRaporteshBazeNgaDefault(int idDrejta)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERRAPORTE_NgaDefault");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        /// <summary>
        /// Ruan trupin e tabeles se te drejtave vetem per tabet e listpageses, kapen automatikisht nga sp prindi i lidhur me to
        /// </summary>
        /// <param name="idDrejta">id e tabeles se kokes</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh krijoTeDrejtaTabeshBazeNgaDefault(int idDrejta)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERTABE_NgaDefault");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Ruan trupin e tabeles se te drejtave vetem per nivellet e regjistrimit
        /// </summary>
        /// <param name="idDrejta">id e tabeles se kokes</param>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh krijoTeDrejtaNivelRegjistrimiNgaDefault(int idDrejta, int idNdermarrje)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERNIVELEREGJISTRIMI_NgaDefault");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        /// <summary>
        /// Ruan trupin e tabeles se te drejtave vetem per kategorite e dokumentave
        /// </summary>
        /// <param name="idDrejta">id e tabeles se kokes</param>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh krijoTeDrejtaKategoriNgaDefault(int idDrejta, int idNdermarrje)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(2);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERKATEGORI_NgaDefault");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        /// <summary>
        /// Fshin koken tek e te drejtave tek ndermarrja per vitin dhe ne rolin e kerkuar
        /// Ruhet ne historik
        /// </summary>
        /// <param name="idNdermarrje">ndermarrja</param>
        /// <param name="idViti">viti</param>
        /// <param name="idRoli">roli</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh fshiTeDrejtatKoka(int idNdermarrje, int idViti, int idRoli)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTAKOKA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        /// <summary>
        /// Fshin trupin tek e te drejtave tek ndermarrja per vitin dhe ne rolin e kerkuar
        /// Ruhen ne historik
        /// </summary>
        /// <param name="idNdermarrje">ndermarrja</param>
        /// <param name="idViti">viti</param>
        /// <param name="idRoli">roli</param>
        /// <returns>Mesazh per suksesin apo mos suksesin e insert</returns>
        public clsMesazh fshiTeDrejtatKTrupi(int idNdermarrje, int idViti, int idRoli)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }


        /// <summary>
        /// Ruan koken e te drejtave per ndermarrjen e re qe po klonohet
        /// Ruhet ne historik
        /// </summary>
        /// <param name="idDrejta">kthen id e re te insert</param>
        /// <param name="idNdermarrje">ndermarrja e vjeter</param>
        /// <param name="idViti">viti i ndermarrjes se vjeter</param>
        /// <param name="idRoli">roli i ndermarrjes se vjeter</param>
        /// <param name="idNdermarjedest">id e ndermarrjes se re qe do te krijohen rolet</param>
        /// <param name="idVitidest">viti i ndermarrjes se re qe po i krijohen rolet</param>
        /// <returns></returns>
        public clsMesazh klonoTeDrejtaKokaNgaNdermarrje(out int idDrejta, int idNdermarrje, int idViti, int idRoli, int idNdermarjedest, int idVitidest)
        {
            idDrejta = -1;
            this.dbManager.Open();
            this.dbManager.CreateParameters(6);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Output);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDNDERMARRJEDEST", idNdermarjedest, ParameterDirection.Input);
            this.dbManager.AddParameters(5, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTAKOKA_ins_KlonoNgaNdermarrje");
            idDrejta = Convert.ToInt32(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ruan komponentet per ndermarrjen e klonuar
        /// </summary>
        /// <param name="idDrejta">id e kokes se ndermarrjes se re te insert</param>
        /// <param name="idNdermarrje">ndermarrja e vjeter</param>
        /// <param name="idViti">viti i ndermarrjes se vjeter</param>
        /// <param name="idRoli">roli i ndermarrjes se vjeter</param>
        /// <returns></returns>
        public clsMesazh klonoTeDrejtaKomponenteshNgaNdermarrje(int idDrejta, int idNdermarrje, int idViti, int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERKOMP_KlonoNgaNdermarrje");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ruan raportet per ndermarrjen e klonuar
        /// </summary>
        /// <param name="idDrejta">id e kokes se ndermarrjes se re te insert</param>
        /// <param name="idNdermarrje">ndermarrja e vjeter</param>
        /// <param name="idViti">viti i ndermarrjes se vjeter</param>
        /// <param name="idRoli">roli i ndermarrjes se vjeter</param>
        /// <returns></returns>
        public clsMesazh klonoTeDrejtaRaporteshNgaNdermarrje(int idDrejta, int idNdermarrje, int idViti, int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERRAPORTE_KlonoNgaNdermarrje");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ruan tabet per ndermarrjen e klonuar
        /// </summary>
        /// <param name="idDrejta">id e kokes se ndermarrjes se re te insert</param>
        /// <param name="idNdermarrje">ndermarrja e vjeter</param>
        /// <param name="idViti">viti i ndermarrjes se vjeter</param>
        /// <param name="idRoli">roli i ndermarrjes se vjeter</param>
        /// <returns></returns>
        public clsMesazh klonoTeDrejtaTabeshNgaNdermarrje(int idDrejta, int idNdermarrje, int idViti, int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERTABE_KlonoNgaNdermarrje");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        public clsMesazh klonoTeDrejtaKategorishNgaNdermarrje(int idDrejta, int idNdermarrje, int idViti, int idRoli)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERKATEGORI_KlonoNgaNdermarrje");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        public clsMesazh klonoTeDrejtaNivelRegjistrimeshNgaNdermarrje(int idDrejta, int idNdermarrje, int idViti, int idRoli, DataTable teDrejtaNiveleshRegjistrimi)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(5);
            this.dbManager.AddParameters(0, "@IDDREJTAKOKA", idDrejta, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDROLI", idRoli, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@drejtaRolTrupi", teDrejtaNiveleshRegjistrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_PERNIVELRREGJISTRIMI_KlonoNgaNdermarrje");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        //End  Ndryshuar Denisa gjate strukturimit dt:05/06/2015

        public Boolean hiqTeDrejtaBij(int idNdermarrje)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTA_hiqTeDrejtaBij");
            return true;
        }


        public DataTable merrTeDrejtaPerdoruesi(int idPerdoruesi, int idNdermarrje, int idViti)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaPerdoruesNderViti");
            return ds.Tables[0];

            //finally
            //{
            //    dbManager.Dispose();
            //}
        }

        public DataTable merrTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(int idPerdoruesi, int idNdermarrje, int idViti, string komponente)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaPerdoruesiNiveleRregjistrimiPerKomponente");
            return ds.Tables[0];
        }
        public DataTable merrTeDrejta(int roli, int idNdermarrje, int idViti)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}

            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDROLI", roli, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliNderViti");
            return ds.Tables[0];

        }

        public DataTable merrTeDrejtaMeRaporte(int roli, int idNdermarrje, int idViti, int idGjuha)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}

            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDROLI", roli, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliDheRaporteNderViti");
            return ds.Tables[0];

        }

        public DataTable merrTeDrejtaMeEmraKomponentesh(int idperdoruesi, int idNdermarrje, int idViti)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliNderVitiMeEmra");
            return ds.Tables[0];
        }

        public DataTable merrTeDrejtaRoliDheRaporteshMeEmraKomponentesh(int idperdoruesi, int idNdermarrje, int idViti)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliNderVitiMeEmraDheRaporte");
            return ds.Tables[0];
        }

        public DataTable merrVetemTeDrejtaAmbjete(int idperdoruesi, int idNdermarrje, int idViti)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_MERR_VETEM_TE_DREJTAT");
            return ds.Tables[0];
        }


        public DataTable merrTeDrejtaRoliPerCRM(int idperdoruesi, int idNdermarrje, int idViti)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliPerCRM");
            return ds.Tables[0];
        }

        public DataTable merrTeDrejtaRoliPerGIS(int idperdoruesi, int idNdermarrje, int idViti)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliPerGIS");
            return ds.Tables[0];
        }

        public DataRow merrTeDrejtaSipasKomponentes(int idperdoruesi, int idNdermarrje, int idViti, string komponente)
        {

            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliNderVitiKomponente");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// todo pati
        /// </summary>
        /// <param name="idperdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="komponente"></param>
        /// <returns></returns>
        public void merrTeDrejtaSipasKomponentesDheRaportet(int idperdoruesi, int idNdermarrje, int idViti, string komponente, IDataBaseReader objektiPErTuMbushur)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrTeDrejtaSipasKomponentesDheRaportet me parametra idperdoruesi:{idperdoruesi}, idNdermarrje:{idNdermarrje}, idViti:{idViti}, komponente:" + komponente);
            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTA_merrTeDrejtaRoliDheRaporteNderVitiKomponente", objektiPErTuMbushur);
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrTeDrejtaSipasKomponentesDheRaportet me parametra idperdoruesi:{idperdoruesi}, idNdermarrje:{idNdermarrje}, idViti:{idViti}, komponente:" + komponente);
        }

        public void merrTeDrejtaSipasKomponentesDheRaportetDheKategori(int idperdoruesi, int idNdermarrje, int idViti, string komponente, int idKategoria, IDataBaseReader objektiPErTuMbushur)
        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(5);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDKATEGORIA", idKategoria, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTA_merrTeDrejtaRoliKomponenteDheKategori", objektiPErTuMbushur);

        }

        public void merrTeDrejtaSipasKomponentesDheNivelRegjistrimi(int idperdoruesi, int idNdermarrje, int idViti, string komponente, int idNivelRregjistrimi, IDataBaseReader objektiPErTuMbushur)
        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(5);
            this.dbManager.AddParameters(0, "@IDpERDORUESI", idperdoruesi, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDNIVELRREGJISTRIMI", idNivelRregjistrimi, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTA_merrTeDrejtaRoliKomponenteDheNivelRregjistrimi", objektiPErTuMbushur);

        }

        public string merrKodeRoleshPerPerdoruesin(int idPerdoruesi)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(1);
            this.dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            return (string)this.dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLPERDORUES_merrKodetERolevePerPerdoruesin");
        }
        internal DataTable ktheTeDrejtePerdorues(int IdPerdorues, int idnermarrjeviti)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", IdPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idnermarrjeviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DREJTAT_merrTeDrejtaPerdorues");
            return ds.Tables[0];

        }

        internal DataTable ktheTeDrejtePerdoruesGjitheNdermarrjet(int IdPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", IdPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DREJTAT_merrTeDrejtaPerdoruesPerGjitheNdermarrjet");
            return ds.Tables[0];
        }

        internal DataTable ktheTeDrejteGrupPerdoruesGjitheNdermarrjet(int idGrupiPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPPERDORUES", idGrupiPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DREJTAT_merrTeDrejtaGrupPerdoruesPerGjitheNdermarrjet");
            return ds.Tables[0];
        }

        internal DataTable ktheTeDrejteGrupPerdorues(int idGrupiPerdorues, int idndermarrjeviti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDGRUPPERDORUES", idGrupiPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERVITI", idndermarrjeviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DREJTAT_merrTeDrejtaGrupPerdorues");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr te drejtat vetem per komponentet e raporteve sipar id se rolit, id se ndermarrjes dhe id te vitit.
        /// </summary>
        /// <param name="idroli"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <returns></returns>
        public DataTable merrTeDrejtatPerKompRaportesh(int idroli, int idNdermarrje, int idViti)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDROLI", idroli, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaRoliNderVitiPerKompRaportesh");
            return ds.Tables[0];
        }

        public bool ekzistonKonfigurimFtpMeKeteKodPerKeteNdermarje(String kodi, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_ekzistonKonfigurimMeKeteKodPerKeteNdermarje");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }
        public bool ekzistonWebhookMeKeteKodPerKeteNdermarje(String kodiwebhook, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIWEBHOOK", kodiwebhook, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_WEBHOOKS_ekzistonKonfigurimMeKeteKodPerKeteNdermarje");
            if (ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }

        #endregion

        #region TE DREJTA RAPORTET

        public DataTable merrTeDrejtaRaportesh(int roli, int idNdermarrje, int idViti)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}

            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDROLI", roli, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_merrTeDrejtaPerRaporteNderViti");
            return ds.Tables[0];

        }
        public bool kaTeDrejtaRaporteshPerNdermarrjeDheVit(int idRoli, int idNdermarrje, int idViti)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLDREJTARAPORTE_kaTeDrejtaRoliPerNdermarrjeDheVit"));
            return Convert.ToBoolean(pergjigje);

        }

        /// <summary>
        /// merr te drejten me id 
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <returns></returns>
        public void merrTeDrejteRaportiSipasPerdNdermarrjeViti(int idRaport, int idPerdorues, int idNdermarrje, int idViti, IDataBaseReader objekt)
        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDRAPORTI", idRaport, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTARAPORTE_selectTeDrejtaRaportiSipasIdRapDheIdPerd", objekt);
        }


        /// <summary>
        /// merr te drejten me id 
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <returns></returns>
        public void merrTeDrejteRaportiSipasPerdNdermarrjeVitiPerSubRaportet(int idRaport, int idPerdorues, int idNdermarrje, int idViti, IDataBaseReader objekt)
        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDRAPORTI", idRaport, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTARAPORTE_selectTeDrejtaRaportiSipasIdRapDheIdPerdPerSubRaportet", objekt);
        }
        /// <summary>
        /// merr te drejten me id 
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <returns></returns>
        public void merrTeDrejteRaportiSipasPerdNdermarrjeViti(string rapEmri, int idPerdorues, int idNdermarrje, int idViti, IDataBaseReader objekt)
        {

            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@RapEmriReal", rapEmri, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTARAPORTE_selectTeDrejtaRaportiSipasRapEmriDheIdPerd", objekt);
        }



        #endregion

        #region TE DREJTA TABEVE


        public bool kaTeDrejtaTabeshhPerNdermarrjeDheVitPerPerdorues(int idPerdoruesi, int idNdermarrje, int idViti, string tabi)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TABI", tabi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLDREJTATABE_kaTeDrejtaRoliPerNdermarrjeDheVitPerTab"));
            return Convert.ToBoolean(pergjigje);

        }


        public bool kaTeDrejtaTabeshPerNdermarrjeDheVit(int idRoli, int idNdermarrje, int idViti)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLDREJTATABE_kaTeDrejtaRoliPerNdermarrjeDheVit"));
            return Convert.ToBoolean(pergjigje);

        }

        /// <summary>
        /// merr te drejten me id 
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <returns></returns>
        public void merrTeDrejteTabiSipasPerdNdermarrjeViti(int idViti, int idPerdorues, int idNdermarrje, string tabi, IDataBaseReader idbObjekt)
        {


            this.dbManager.Open();
            this.dbManager.CreateParameters(4);
            this.dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@TABI", tabi, ParameterDirection.Input);
            dbManager.FillObject("prc_T_ROLDREJTATABE_selectTeDrejtaTabiSipasIdRapDheIdPerd", idbObjekt);



        }
        public DataTable merrTeDrejteTabi()
        {


            this.dbManager.Open();
            this.dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_rolDrejtaTabe_merrTabe");
            DataTable dt = ds.Tables[0];
            return dt;

        }

        /// <summary>
        /// Shtuar ne menyre qe gjate mbylljes se vitit te dhenat te klonohen nga viti paraardhes Tabela e Kokes per te gjithe rolet njeheresh
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja ku po kryhet mbyllja e vitit</param>
        /// <param name="idViti">Viti aktual</param>
        /// <param name="idVitidest">Viti i ri</param>
        /// <returns>Kthen mesazh per suksesin apo jo</returns>
        public clsMesazh klonoTeDrejtaKokaPerMbylljeViti(int idNdermarrje, int idViti, int idVitidest)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTAKOKA_ins_KlonoPerMbylljeViti");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        /// <summary>
        /// Shtuar ne menyre qe gjate mbylljes se vitit te dhenat te klonohen nga viti paraardhes per komponentet 
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja ku po kryhet mbyllja e vitit</param>
        /// <param name="idViti">Viti aktual</param>
        /// <param name="idVitidest">Viti i ri</param>
        /// <returns>Kthen mesazh per suksesin apo jo</returns>
        public clsMesazh klonoTeDrejtaKomponenteshPerMbylljeViti(int idNdermarrje, int idViti, int idVitidest)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_KOMP_KlonoPerMbylljeViti");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        /// <summary>
        /// Shtuar ne menyre qe gjate mbylljes se vitit te dhenat te klonohen nga viti paraardhes per raportet 
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja ku po kryhet mbyllja e vitit</param>
        /// <param name="idViti">Viti aktual</param>
        /// <param name="idVitidest">Viti i ri</param>
        /// <returns>Kthen mesazh per suksesin apo jo</returns>
        public clsMesazh klonoTeDrejtaRaporteshPerMbylljeViti(int idNdermarrje, int idViti, int idVitidest)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_RAPORTE_KlonoPerMbylljeViti");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        /// <summary>
        /// Shtuar ne menyre qe gjate mbylljes se vitit te dhenat te klonohen nga viti paraardhes per tabet 
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja ku po kryhet mbyllja e vitit</param>
        /// <param name="idViti">Viti aktual</param>
        /// <param name="idVitidest">Viti i ri</param>
        /// <returns>Kthen mesazh per suksesin apo jo</returns>
        public clsMesazh klonoTeDrejtaTabeshPerMbylljeViti(int idNdermarrje, int idViti, int idVitidest)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_TABE_KlonoPerMbylljeViti");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Shtuar ne menyre qe gjate mbylljes se vitit te dhenat te klonohen nga viti paraardhes per kategorite 
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja ku po kryhet mbyllja e vitit</param>
        /// <param name="idViti">Viti aktual</param>
        /// <param name="idVitidest">Viti i ri</param>
        /// <returns>Kthen mesazh per suksesin apo jo</returns>
        public clsMesazh klonoTeDrejtaKategorishPerMbylljeViti(int idNdermarrje, int idViti, int idVitidest)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_KATEGORIA_KlonoPerMbylljeViti");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Shtuar ne menyre qe gjate mbylljes se vitit te dhenat te klonohen nga viti paraardhes per nivelet e rregjistrimit 
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja ku po kryhet mbyllja e vitit</param>
        /// <param name="idViti">Viti aktual</param>
        /// <param name="idVitidest">Viti i ri</param>
        /// <returns>Kthen mesazh per suksesin apo jo</returns>
        /// 
        public clsMesazh klonoTeDrejtaNiveleRegjistrimiPerMbylljeViti(int idNdermarrje, int idViti, int idVitidest)
        {
            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIDEST", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATRUPI_ins_IDNIVELRREGJISTRIMI_KlonoPerMbylljeViti");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /* Denisa komentuar pasi ka kaluar ne sp gjate strukturimit dt:05/06/2015
        public clsMesazh klonoTeDrejtaTabeshPerVitinRi(int idNdermarrje, int idVitidest)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}

            this.dbManager.Open();
            this.dbManager.CreateParameters(3);
            this.dbManager.AddParameters(0, "@Return_Message", "", ParameterDirection.Output);
            this.dbManager.Parameters[0].Size = 100;
            this.dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIRI", idVitidest, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATABE_klonoTeDrejtaRaporteshTekVitiIRi");
            string msg = dbManager.Parameters[0].ToString();
            if (msg.StartsWith("Gabim"))
                return new clsMesazh(false, msg);
            else
                return new clsMesazh(true, msg);

        }

        public clsMesazh klonoTeDrejtaTabesh(int idNdermarrjevjeter, int idVitivjeter, int idNdermarrjeRe, int idVitiRi, int idRoli)
        {
            //if (this.dbManager == null)
            //{
            //    this.dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    this.dbManager.ConnectionString = this.dbManager.GetConnectionString();
            //}

            this.dbManager.Open();
            this.dbManager.CreateParameters(6);
            this.dbManager.AddParameters(0, "@Return_Message", "", ParameterDirection.Output);
            this.dbManager.Parameters[0].Size = 100;
            this.dbManager.AddParameters(1, "@IDNDERMARRJEVJETER", idNdermarrjevjeter, ParameterDirection.Input);
            this.dbManager.AddParameters(2, "@IDVITIVJETER", idVitivjeter, ParameterDirection.Input);
            this.dbManager.AddParameters(3, "@IDNDERMARRJERE", idNdermarrjeRe, ParameterDirection.Input);
            this.dbManager.AddParameters(4, "@IDVITIRI", idVitiRi, ParameterDirection.Input);
            this.dbManager.AddParameters(5, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ROLDREJTATABE_klonoTeDrejtaTabesh");
            string msg = dbManager.Parameters[0].ToString();
            if (msg.StartsWith("Gabim"))
                return new clsMesazh(false, msg);
            else
                return new clsMesazh(true, msg);
        }
        */
        #endregion

        #region TE DREJTAT - VEPRIMET

        internal clsMesazh ruajTeDrejteVeprim(int iddrejtaveprim, String kodidrejtaveprim, String pershkrimidrejtaveprim)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDDREJTAVEPRIM", iddrejtaveprim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@DREJTAVEPRIMKODI", kodidrejtaveprim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DREJTAVEPRIMPERSH", pershkrimidrejtaveprim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajTeDrejteVeprim(int iddrejtaveprim, String kodidrejtaveprim, String pershkrimidrejtaveprim)", true)]
        //public clsMesazh ruajTeDrejteVeprim(clsTeDrejtaVeprim veprim)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDDREJTAVEPRIM", veprim.IdDrejtaVeprim, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@DREJTAVEPRIMKODI", veprim.KodiDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@DREJTAVEPRIMPERSH", veprim.PershkrimiDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoTeDrejteVeprim(int iddrejtaveprim, String kodidrejtaveprim, String pershkrimidrejtaveprim)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDDREJTAVEPRIM", iddrejtaveprim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DREJTAVEPRIMKODI", kodidrejtaveprim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DREJTAVEPRIMPERSH", pershkrimidrejtaveprim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoTeDrejteVeprim(int iddrejtaveprim, String kodidrejtaveprim, String pershkrimidrejtaveprim)", true)]
        //public clsMesazh modifikoTeDrejteVeprim(clsTeDrejtaVeprim veprim)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDDREJTAVEPRIM", veprim.IdDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@DREJTAVEPRIMKODI", veprim.KodiDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@DREJTAVEPRIMPERSH", veprim.PershkrimiDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }

        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        //[Obsolete("Perdor: DataTable ktheAllTrackUser()", true)]
        //public colTrackUser merrOnlineUser(clsTrackUser objOnlineUser)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_USERTRACK_sel_perGRID");
        //        colTrackUser colTrack = new colTrackUser();
        //        return colTrack.mbushArrayListTrackUser(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrackUser();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        public int merrOnlineUserCount()
        {

            dbManager.Open();
            //dbManager.CreateParameters(1);
            //dbManager.AddParameters(0, "@nr", 0, ParameterDirection.Output);
            return (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_USERTRACK_selNrOnline");

        }

        //public int merrNrUserOnlineIP(string ip, int idperdorues)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@IP", ip, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

        //        return (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_USERTRACK_kontrolloIP");
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}

        internal DataTable ktheAllTrackUser(int idLicenca, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLICENCA", idLicenca, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_USERTRACK_sel_perGRID");
            return ds.Tables[0];

        }


        //[Obsolete("Perdor: DataTable ktheAllTrackUser()", true)]
        //public colTrackUser merrAllTrackUser()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_USERTRACK_sel_perGRID");
        //        colTrackUser colTrack = new colTrackUser();
        //        return colTrack.mbushArrayListTrackUser(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTrackUser();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh fshiTeDrejteVeprim(int iddrejtaveprim)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDREJTAVEPRIM", iddrejtaveprim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiTeDrejteVeprim(int iddrejtaveprim)", true)]
        //public clsMesazh fshiTeDrejteVeprim(clsTeDrejtaVeprim veprim)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDDREJTAVEPRIM", veprim.IdDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal void merrTeDrejteVeprim(int iddrejtaveprim)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDREJTAVEPRIM", iddrejtaveprim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_sel");

        }
        //[Obsolete("Perdor: void merrTeDrejteVeprim(int iddrejtaveprim)", true)]
        //public void merrTeDrejteVeprim(clsTeDrejtaVeprim veprim)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDDREJTAVEPRIM", veprim.IdDrejtaVeprim, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_sel");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheTeDrejtatVeprimet()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_merrGjitheTeDrejtatVeprimet");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheTeDrejtatVeprimet()", true)]
        //public colTeDrejtatVeprimet merrGjitheTeDrejtatVeprimet()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DREJTAVEPRIM_merrGjitheTeDrejtatVeprimet");
        //        colTeDrejtatVeprimet veprimet = new colTeDrejtatVeprimet();
        //        return veprimet.mbushArrayListVeprimet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTeDrejtatVeprimet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region KONFIGURIM KASE
        internal clsMesazh fshiKonfigurimKase(int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;



        }
        internal clsMesazh fshiKonfigurimKaseStatus(int idkonfigurimi, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idkonfigurimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        public bool ekzistonKonfigurimKaseMeKeteKodPerKeteNdermarje(int idNdermarrja, String kodi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            int nrRreshtash = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ekzistonKonfigurimPerKeteNdermarje"));
            return (nrRreshtash == 0 ? false : true);
            //if (nrRreshtash == 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }

        internal string ktheSkemeBarkodiPershoreje(int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ktheSkemeBarkodiPeshorejeSipasNdermarjes"));
        }


        internal clsMesazh ruajKonfigurimKase(out int idkonfigurimi, String pershkrimi, int idndermarje, int idstatusdok, int idperdorues, string kodi, int lloji, string skema)
        {
            idkonfigurimi = -1;
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idkonfigurimi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SKEMA", skema, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ins");
            idkonfigurimi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");
        }
        internal clsMesazh modifikoVlereDefaultKonfigKasa(int idkonfigurimi, int idkonfigurimiVjeter, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idkonfigurimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGURIMIVJETER", idkonfigurimiVjeter, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_updVleraDefaultKasa");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses");
        }
        internal clsMesazh modifikoKonfigurimKase(int idkonfigurimi, String pershkrimi, int idndermarje, int idstatusdok, int idperdorues, string kodi, int lloji, string skema)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idkonfigurimi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SKEMA", skema, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "Prc_t_konfigurimkasa_upd");
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");
        }

        internal DataRow merrKonfigurimKase(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ktheKonfigurimKase");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataRow merrKonfigurimKaseSipasNdermarjes(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ktheKonfigurimKaseSipasNdermarjes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrKonfigurimKasashSipasNdermarjesDT(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ktheKonfigKasashSipasNdermarjeDT").Tables[0];
        }

        internal DataTable merrKonfigurimKasashSipasNdermarjesMeUrl(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ktheKonfigKasashSipasNdermarjesMeUrl").Tables[0];
        }

        internal DataRow merrKonfigurimKasashSipasNdermarjesDR(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_ktheKonfigKasashSipasNdermarjeDR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        #endregion

        #region OPSIONE KONFIGURIMI KASE
        internal int merrIdOpsioni(string pershkrimi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            int id = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_OPSIONEKONFIGURIMIKASA_ktheIdOpsioni"));
            return id;

        }
        internal string merrVlereOpsioni(string pershkrimi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            string vlere = Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_OPSIONEKONFIGURIMIKASA_ktheVlereSipasOpsionit"));
            return vlere;

        }

        #endregion

        #region VLERA KONFIGURIMI KASE
        internal clsMesazh fshiVleraKonfigurimKase(int idkonfigurimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idkonfigurimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh ruajVleraKonfigurimKase(out int idvlera, int idkonfigurimi, int idopsioni, String vlera, int idtaksa)
        {
            idvlera = -1;

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDVLERAKONFIGURIMIKASA", idvlera, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKONFIGURIMI", idkonfigurimi, ParameterDirection.Input);
            if (idopsioni == 0) dbManager.AddParameters(2, "@IDOPSIONI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(2, "@IDOPSIONI", idopsioni, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERA", vlera, ParameterDirection.Input);
            if (idtaksa == 0) dbManager.AddParameters(4, "@IDTAKSA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(4, "@IDTAKSA", idtaksa, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_ins");

            idvlera = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");

        }
        internal clsMesazh updatePLU(int idndermarrje, int plu, int idKonfigurimiKases)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PLU", plu, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIKURIMI", idKonfigurimiKases, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_updatePLUperKonfigurim");
            clsMesazh mesazh = new clsMesazh(true, "Update perfundoi me sukses!");
            return mesazh;


        }
        internal DataRow merrPLUActualNumberPerKase(int idndermarrje, int idKonfigurimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGURIMI", idKonfigurimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_MerrPLUperKonfigurim");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrVleraSipasIdTaksa(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTAKSA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_ktheVleratSipasIdTakse");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrVleraSipasIdTaksaJoFshire(int id, int idKonfigurimi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTAKSA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGURIMI", idKonfigurimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_ktheVleratSipasIdTakseJoFshire");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrVleraSipasIdKonfigurimi(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_ktheVleratSipasKonfigurimit");
            if (ds == null)
                return null;

            return ds.Tables[0];

        }
        internal DataTable merrVleraSipasNiveleTaksa(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAKONFIGURIMIKASA_ktheVleratSipasKonfigurimitTeNiveleve");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        internal DataTable ktheLlojeKasashDhePeshoreshSipasLlojit(int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KASAPESHORE_sel");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        internal DataTable ktheLlojeKasashDhePeshoreshAll()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KASAPESHORE_selAll");
            if (ds == null)
                return null;

            return ds.Tables[0];
        }

        #endregion

        #region MONEDHAT

        internal clsMesazh ruajMonedhe(out int idmonedha, String kodimonedha, String pershkrimimonedha, bool aktivmonedha, int idperdoruesi, int idllogfitimi, int idlloghumbje, int idndermarje, int idstatusdok, int idFormatNrKursi)
        {
            idmonedha = -1;
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@MON", idmonedha, ParameterDirection.Output);
            dbManager.AddParameters(1, "@MONEDHAKOD", kodimonedha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MONEDHAPERSHK", pershkrimimonedha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MONEDHAAKTIV", aktivmonedha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            if (idllogfitimi == 0 || idllogfitimi == -1) dbManager.AddParameters(6, "@IDLLOGFITIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDLLOGFITIMI", idllogfitimi, ParameterDirection.Input);
            if (idlloghumbje == 0 || idlloghumbje == -1) dbManager.AddParameters(7, "@IDLLOGHUMBJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDLLOGHUMBJE", idlloghumbje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idFormatNrKursi == 0 || idFormatNrKursi == -1)
                dbManager.AddParameters(9, "@IDFORMATNRKURSI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDFORMATNRKURSI", idFormatNrKursi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MONEDHA_ins");
            idmonedha = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        public clsMesazh ruajKurs(out int idkursi, int llojkursi, DateTime datakursit, double vlerakursi, int idmonedha, int njesia, string pershkllojkursi)
        {

            idkursi = -1;
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDKURSI", idkursi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@LLOJIKURSIT", llojkursi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VLERAKURSIT", vlerakursi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATAKURSIT", datakursit, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NJESIA", njesia, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERSHKRIMLLOJKURSI", pershkllojkursi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KURSET_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiKurs(int llojkursi, DateTime datakursit, int idmonedha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@LLOJIKURSIT", llojkursi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", datakursit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KURSET_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        public bool ekzistonKurs(int llojkursi, DateTime datakursit, double vlerakursi, int idmonedha)
        {
            int nr;
            string sqlstr = "select COUNT(*) from T_KURSET " +
                            " where LLOJIKURSIT = '" + llojkursi + "'" +
                            " and IDMONEDHA = " + idmonedha +
                            " and DATAKURSIT = convert(datetime,'" + datakursit + "', 103)" +
                            " and VLERAKURSIT = " + vlerakursi;


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            DataRow rreshti = ds.Tables[0].Rows[0];
            nr = int.Parse(rreshti[0].ToString());
            if (nr >= 1)
                return true;
            else
                return false;

        }

        internal clsMesazh modifikoMonedhe(int idmonedha, String kodimonedha, String pershkrimimonedha, bool aktivmonedha, int idperdoruesi, int idllogfitimi, int idlloghumbje, int idndermarje, int idstatusdok, int idFormatNrKursi)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@MON", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@MONEDHAKOD", kodimonedha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MONEDHAPERSHK", pershkrimimonedha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MONEDHAAKTIV", aktivmonedha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            if (idllogfitimi == 0 || idllogfitimi == -1) dbManager.AddParameters(6, "@IDLLOGFITIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDLLOGFITIMI", idllogfitimi, ParameterDirection.Input);
            if (idlloghumbje == 0 || idlloghumbje == -1) dbManager.AddParameters(7, "@IDLLOGHUMBJE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDLLOGHUMBJE", idlloghumbje, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDFORMATNRKURSI", idFormatNrKursi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MONEDHA_upd");
            idmonedha = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;


        }

        internal clsMesazh fshiMonedhe(int idmonedha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MONEDHA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiMonedheStatus(int idmonedha, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MONEDHA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal DataRow ktheMonedhen(string kod, int IdNdermarrja)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheMonedhen sipas kod:{kod} dhe idNdermarrje:{IdNdermarrja}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODMONEDHA", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", IdNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheKod");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheMonedhen sipas kod:{kod} dhe idNdermarrje:{IdNdermarrja}");
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheMonedhenSipasLlogari(int idLlogari)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOGARI", idLlogari, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheSipasLlogari");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheMonedhePershk(string pershMonedha, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIMMONEDHA", pershMonedha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedhePershk");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrMonedhe(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedhe");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataTable merrMonedha(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idNdermarrje", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedhaNdermarrje").Tables[0];
        }
        internal string merrKodMonedhe(int id)
        {
            ImbLogger.LogTraceShitje("Kthimi i monedhes nga db sipas id: " + Convert.ToString(id));
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            ImbLogger.LogTraceShitje("Kthimi i monedhes nga db sipas id: " + Convert.ToString(id));
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheKodMonedheSipasId"));
        }

        internal int merrIdSipasKodMonedhe(string kod, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODMONEDHA", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheIdMonedheSipasKod"));
        }

        internal String merrFormatNrMonedhe(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheFormatKursiMonedheSipasId"));
        }

        internal string merrPershkrimMonedhe(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_kthePershkrMonedheSipasId"));
        }

        internal DataTable ktheGjitheMonedhat(int id, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrGjitheMonedhatSipasNdermarjesAndAutorizime");
            return ds.Tables[0];

        }

        internal DataTable ktheGjitheMonedhatAktive(int id, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrGjitheMonedhatAktiveSipasNdermarjesAndAutorizime");
            return ds.Tables[0];

        }
        internal DataTable ktheGjitheMonedhatAktiveDtSmall(int id, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrGjitheMonedhatAktiveSipasNdermarjesAndAutorizimeDtSmall");
            return ds.Tables[0];

        }

        internal DataTable ktheGjitheMonedhatPozitive(int id, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrGjitheMonedhatPozitiveSipasNdermarjesAndAutorizime");
            return ds.Tables[0];

        }

        internal DataTable ktheKursetFunditMonedhes(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursetFunditMonedhes");
            return ds.Tables[0];
        }

        internal DataTable ktheKursetFunditMonedhesSipasLlojit(int id, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJIKURSIT", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursetFunditMonedhesSipasLlojit");
            return ds.Tables[0];
        }

        internal DataTable ktheKursetMonedhesSipasLlojit(int id, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJIKURSIT", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursetMonedhesSipasLlojit");
            return ds.Tables[0];
        }

        internal double ktheKursinSipasMonedhesAndllojit(int idMonedhe, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMONEDHA", idMonedhe, ParameterDirection.Input);
            dbManager.AddParameters(1, "@lloji", lloji, ParameterDirection.Input);
            double kurs = Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KURSET_merrKursinSipasMonedhesLlojit"));
            if (kurs == 0)
                return -1;
            return kurs;
        }

        internal DataRow ktheKursinSipasMonedhesAndDates(int id, DateTime date)
        {
            ImbLogger.LogTraceShitje("Kthe kurs sipas id monedhes: " + Convert.ToString(id) + " dhe dates: " + Convert.ToString(date));
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATAKURSIT", date, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursinSipasMonedhesAndDates");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            ImbLogger.LogTraceShitje("U kthye kursi sipas  id monedhes: " + Convert.ToString(id) + " dhe dates: " + Convert.ToString(date));
            return ds.Tables[0].Rows[0];
        }
        internal DataRow ktheKursinSipasMonedhDatesAndIdkonfigurim(int idMon, DateTime date, int idKonfigurim, int idNdermarrje)
        {
            ImbLogger.LogTraceShitje("Kthe kurs sipas id monedhes: " + Convert.ToString(idMon) + " , dates: " + Convert.ToString(date) + " dhe idKonfigurim: " + Convert.ToString(idKonfigurim));
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDMONEDHA", idMon, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATAKURSIT", date, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGURIM", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursinSipasMonedhesDatesDheIdKonfigurimit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;

            ImbLogger.LogTraceShitje("U kthye kursi sipas  id monedhes: " + Convert.ToString(idMon) + " , dates: " + Convert.ToString(date) + " dhe idKonfigurim: " + Convert.ToString(idKonfigurim));
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheKursSipasMonedhesAndDatesDheLlojit(int id, DateTime date, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATAKURSIT", date, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKURSI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursinSipasMonedhesAndDatesDheLlojit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal double ktheKursinFunditPerMonedheDateDheLloj(int id, DateTime date, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATAKURSIT", date, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJKURSI", lloji, ParameterDirection.Input);
            double kurs = Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KURSET_merrKursFunditSipasMonedhesDatesDheLlojit"));
            if (kurs == 0)
                return 1;
            return kurs;

        }

        internal DataRow ktheKursetSipasKoditMonedhesAndDates(string kodi, string date, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODMONEDHA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATAKURSIT", date, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursinSipasKodMonedhesAndDates");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }

        internal double ktheKursetSipasKoditMonedhesDatesDheLlojit(string kodi, string date, int idndermarje, int lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODMONEDHA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATAKURSIT", date, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@LLOJI", lloji, ParameterDirection.Input);
            double kurs = Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KURSET_merrKursinSipasKodMonedhesDatesDheLlojit"));
            if (kurs == 0)
                return 1;
            return kurs;

        }


        internal DataTable ktheKursetMonedhes(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursetMonedhes");
            return ds.Tables[0];
        }

        internal DataTable ktheKursetMonedhesDt(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrKursetMonedhesDt");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheKurset(int idNdermarrje, DateTime data)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idNdermarrje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@data", data, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KURSET_merrGjitheKursetShitjes").Tables[0];
        }

        public bool ekzistonMonedha(string kodi, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@MONEDHAKOD", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            bool ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Monedha_EkzistonMonedheMeKeteKod"));
            return ekziston;
        }

        public clsMesazh ekzistonMonedhaPershkrim(string kodi, int idNdermarrja)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@MONEDHApershk", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            int nrRreshtash = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Monedha_EkzistonMonedheMeKetePershkrim"));
            if (nrRreshtash == 0)
                return new clsMesazh(false, "Nuk ekziston nje monedhe me kete pershkrim");
            else
                return new clsMesazh(true, "Ekziston nje monedhe me kete pershkrim");
        }

        public clsMesazh kaVeprimeMonedha(int idmon)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMONEDHA", idmon, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_Monedha_KaVeprime");
            if (ds.Tables[0].Rows.Count == 0)
            {
                return new clsMesazh(false);
            }
            else
            {
                return new clsMesazh(true, "Ka veprime me kete monedhe");
            }
        }

        internal DataRow merrMonedhaSipasNdermarjesDR(int idnderm, int idmonedha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMONEDHA", idmonedha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrMonedhaSipasNdermarjesDR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrMonedhaNdermarjeDT(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrMonedhaNdermarjeDT");
            return ds.Tables[0];
        }

        internal DataTable merrMonedhaNdermarjeDTAktiv(int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_merrMonedhaNdermarjeDTAktiv");
            return ds.Tables[0];
        }

        #endregion

        #region NDERMARRJET - VITET

        internal clsMesazh ruajNdermarrjeVit(out int idNderViti, int idViti, int viti, int idNdermarrje, bool ndermarrjeVitiMbyllur, DateTime ndermarrjeVitiFillim, DateTime ndermarrjeVitiFund, int idPerdoruesi)
        {
            idNderViti = -1;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDNDERVITI", idNderViti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NDERMVITMBYLLUR", ndermarrjeVitiMbyllur, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NDERMVITFILLIM", ndermarrjeVitiFillim, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NDERMVITFUND", ndermarrjeVitiFund, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_ins");

            idNderViti = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: ", true)]
        //public clsMesazh ruajNdermarrjeVit(clsNdermarrjeViti viti)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDNDERVITI", viti.IdNderViti, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDVITI", viti.IdViti, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@VITI", viti.NdermarrjeViti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", viti.IdNdermarrje, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@NDERMVITMBYLLUR", viti.NdermarrjeVitiMbyllur, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@NDERMVITFILLIM", viti.NdermarrjeVitiFillim, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@NDERMVITFUND", viti.NdermarrjeVitiFund, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDPERDORUESI", viti.IdPerdoruesi , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_ins");

        //        viti.IdNderViti = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoNdermarrjeVit(int idNderViti, int idViti, int viti, int idNdermarrje, bool ndermarrjeVitiMbyllur, DateTime ndermarrjeVitiFillim, DateTime ndermarrjeVitiFund, int idPerdoruesi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NDERMVITMBYLLUR", ndermarrjeVitiMbyllur, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NDERMVITFILLIM", ndermarrjeVitiFillim, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NDERMVITFUND", ndermarrjeVitiFund, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: ", true)]
        //public clsMesazh modifikoNdermarrjeVit(clsNdermarrjeViti viti)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(8);
        //        dbManager.AddParameters(0, "@IDNDERVITI", viti.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDVITI", viti.IdViti, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@VITI", viti.NdermarrjeViti, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERMARJE", viti.IdNdermarrje, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@NDERMVITMBYLLUR", viti.NdermarrjeVitiMbyllur, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@NDERMVITFILLIM", viti.NdermarrjeVitiFillim, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@NDERMVITFUND", viti.NdermarrjeVitiFund, ParameterDirection.Input);
        //        dbManager.AddParameters(7, "@IDPERDORUESI", viti.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh fshiNdermarrjeVit(int idNderViti)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITI", idNderViti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: ", true)]
        //public clsMesazh fshiNdermarrjeVit(clsNdermarrjeViti viti)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERVITI", viti.IdNderViti, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        //[Obsolete("Nuk perdoret me!",true)]
        //public colNdermarrjeVitet merrNdermarrjeVit(ArrayList ndermarrjet, ArrayList vitet)
        //{
        //    if (ndermarrjet.Count > 0 && vitet.Count > 0)
        //    {
        //        IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();

        //        String nderm = "(";
        //        foreach (Object i in ndermarrjet)
        //        {
        //            nderm += i + ",";
        //        }
        //        nderm = nderm.Remove(nderm.Length - 1);
        //        nderm += ")";

        //        String vt = "(";
        //        foreach (Object i in vitet)
        //        {
        //            vt += i + ",";
        //        }
        //        vt = vt.Remove(vt.Length - 1);
        //        vt += ")";
        //        String sqlstr = "select IDNDERVITI,IDVITI, VITI, IDNDERMARJE, NDERMVITMBYLLUR, NDERMVITFILLIM, NDERMVITFUND from T_NDERMARJEVITI where IDNDERMARJE in " + nderm + " and VITI in " + vt;
        //        try
        //        {
        //            dbManager.Open();
        //            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //            colNdermarrjeVitet ndermarrjeVitet = new colNdermarrjeVitet();
        //            return ndermarrjeVitet.mbushArrayListNdermarrjeVitet(ds);
        //        }
        //        catch (Exception)
        //        {
        //            return new colNdermarrjeVitet();
        //        }
        //        finally
        //        {
        //            dbManager.Dispose();
        //        }
        //    }
        //    else return new colNdermarrjeVitet();
        //}

        public byte[] merrLogoNderm(int idNdermarrje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheLogoNdermarrje");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            DataRow dbDataRowNdermarrja = ds.Tables[0].Rows[0];
            if (!String.IsNullOrEmpty(dbDataRowNdermarrja["NDERMARJELOGO"].ToString()))
                return (byte[])dbDataRowNdermarrja["NDERMARJELOGO"];
            return null;

        }

        internal int merrIdNdermarrje(string kodNdermarrje)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NDERMARJEKODI", kodNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdNdermarrjeSipasKodit");
            if (ds.Tables[0].Rows.Count == 0)
                throw new MyException($"Ndermarrja me kodin {kodNdermarrje} nuk ekziston!");
            DataRow rreshti = ds.Tables[0].Rows[0];
            return Convert.ToInt32(rreshti[0]);


        }

        internal bool merrMeArkive(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheMeArkive"));

        }

        internal int merrMaxSizeArkive(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            object result = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheMaxSizeArkive");
            if (result == null || result == DBNull.Value)
                return 10485760; //default 10 MB
            return Convert.ToInt32(result);

        }

        /// <summary>
        /// kthen id e ndermarjes meme te ndermarrjes bije
        /// </summary>
        /// <param name="idNdermarrjeBije"></param>
        /// <returns></returns>
        internal int merrIdNdermarrjeMeme(int idNdermarrjeBije)
        {
            int idndermarrje = -1;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrjeBije, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdNdermarrjeMEMESipasId");
            DataRow rreshti = ds.Tables[0].Rows[0];
            idndermarrje = Convert.ToInt32(rreshti[0]);
            return idndermarrje;

        }

        /// <summary>
        /// kthen id e ndermarjes meme te ndermarrjes bije
        /// </summary>
        /// <param name="idNdermarrjeBije"></param>
        /// <returns></returns>
        internal int merrIdNdermarrjeRaportimi(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            object idRaportuesi = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdRaportues");
            if (idRaportuesi == null || idRaportuesi == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(idRaportuesi);
        }
        /// <summary>
        /// kthen id e ndermarjes meme te ndermarrjes bije
        /// </summary>
        /// <param name="idNdermarrjeBije"></param>
        /// <returns></returns>
        internal int merrIdNdermarrjeRaportimiRoot(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            object idRaportuesi = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdRaportuesRoot");
            if (idRaportuesi == null || idRaportuesi == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(idRaportuesi);
        }

        internal int merrNivelStrukturePerIdNderm(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            object idRaportuesi = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNivelStruktureSipasIdNderm");
            if (idRaportuesi == null || idRaportuesi == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(idRaportuesi);
        }

        internal bool EshteNdermarrjeRaportuese(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int count = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_eshteNdermarrjeRaportuese"));
            return count > 0;
        }

        /// <summary>
        /// VODAFONE
        /// Metode qe perdoret vetem per integrimin me VODAFONE per marrjen e ndermarrjes meme sipas emrin te ndermarrjes meme (e vendosur direkt ne SP)
        /// </summary>
        /// <returns></returns>
        internal int merrIdNdermarrjeMeme()
        {
            int idndermarrje = -1;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdNdermarrjeMEME"); //SP vetem per VODAFONE
            DataRow rreshti = ds.Tables[0].Rows[0];
            idndermarrje = int.Parse(rreshti[0].ToString());
            return idndermarrje;

        }

        internal int merrIdNdermarrjeVit(int idndermarje, int viti)
        {
            int idndermarrjevit = -1;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            String sqlstr = "select nv.IDNDERVITI from T_NDERMARJEVITI nv where nv.IDNDERMARJE = " + idndermarje + " and IdVITI =" + viti;

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            DataRow rreshti = ds.Tables[0].Rows[0];
            idndermarrjevit = int.Parse(rreshti[0].ToString());
            return idndermarrjevit;

        }

        //[Obsolete("Nuk perdoret me!", true)]
        //public colNdermarrjeVitet ktheNdermarrjetVitet(ArrayList vitet)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();

        //    String vt = "(";
        //    foreach (Object i in vitet)
        //    {
        //        vt += i + ",";
        //    }
        //    vt = vt.Remove(vt.Length - 1);
        //    vt += ")";

        //    String sqlstr = "select * from T_NDERMARJEVITI where IDNDERVITI in " + vt;
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //        colNdermarrjeVitet ndermarrjeVitet = new colNdermarrjeVitet();
        //        return ndermarrjeVitet.mbushArrayListNdermarrjeVitet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNdermarrjeVitet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataSet ktheVitetNdermarrjes(String kodiNdermarrjes)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            String sqlstr = "select IDNDERVITI, viti from T_NDERMARJEVITI nv, T_NDERMARJE n where nv.IDNDERMARJE = n.IDNDERMARJE and NDERMARJEKODI='" + kodiNdermarrjes + "'";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            return ds;

        }

        //internal string merrKodNdermarrje(int idndermarrjevit)
        //{
        //    string kodNdermarrje;
        //   
        //    //{
        //    //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    //}
        //    String sqlstr = "select NDERMARJEKODI  from T_NDERMARJE n inner join T_NDERMARJEVITI nv " +
        //                    " on n.IDNDERMARJE = nv.IDNDERMARJE  where IDNDERVITI = " + idndermarrjevit;

        //    dbManager.Open();
        //    DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //    DataRow rreshti = ds.Tables[0].Rows[0];
        //    kodNdermarrje = rreshti[0].ToString();
        //    return kodNdermarrje;

        //}

        /// <summary>
        /// kthen pershkrimin e ndermarrjes
        /// </summary>
        /// <param name="idndermarrje"></param>
        /// <returns></returns>
        internal string merrPershkrimNdermarrje(int idndermarrje)
        {
            string pershkrimi;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            String sqlstr = "SELECT NDERMARJEPERSHK FROM dbo.T_NDERMARJE WHERE IDSTATUSDOK<>2 AND AKTIV = 'True' AND IDNDERMARJE = " + idndermarrje;

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            DataRow rreshti = ds.Tables[0].Rows[0];
            pershkrimi = rreshti[0].ToString();
            return pershkrimi;

        }

        internal int merrKodViti(int idndermarrjevit)
        {
            int kodviti;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            String sqlstr = "select viti  from T_NDERMARJEVITI where  IDNDERVITI =" + idndermarrjevit;

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            DataRow rreshti = ds.Tables[0].Rows[0];
            kodviti = int.Parse(rreshti[0].ToString());
            return kodviti;

        }

        /// <summary>
        /// Merr nga db-ja NdermarrjeVit duke u nisur nga id-ja ne input
        /// </summary>
        /// <param name="idNdermarrjeVit">id-ja e dhene ne input</param>
        /// <returns>Kthen DataRow te tipit NdermarrjeVit, null nese nuk ekziston</returns>
        internal DataRow merrNdermarrjeVit(int idNdermarrjeVit)
        {
            //string datefill;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermarrjeVit, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_select");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// Merr nga db-ja NdermarrjeVit duke u nisur nga id-ja ne input
        /// </summary>
        /// <param name="idNdermarrjeVit">id-ja e dhene ne input</param>
        /// <returns>Kthen DataRow te tipit NdermarrjeVit, null nese nuk ekziston</returns>
        internal DataRow merrNdermarrjeVitSipasNdermarjesDheVitit(int idNdermarrje, int idviti)
        {
            //string datefill;

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrNdermarjeVitSipasNdermarjesDheVitit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal int merrIdNdermarrjeVitSipasNdermarjesDheVitit(int idNdermarrje, int idviti)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idviti, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrIdNdermarjeVitSipasNdermarjesDheVitit"));

        }

        internal int merrIdNdermarrjeVitSipasNdermarjesDheKodVitit(int idNdermarrje, int idviti)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@VITI", idviti, ParameterDirection.Input);
            object idNderViti = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrIdNdermarjeVitSipasNdermarjesDheKodit");
            if (idNderViti == null || idNderViti == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(idNderViti);
        }

        internal int merrKodVitiSipasIdNdermViti(int idNdermViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERVITI", idNdermViti, ParameterDirection.Input);
            object kodViti = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrKodVitiSipasIdNderViti");
            if (kodViti == null || kodViti == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(kodViti);
        }

        /// <summary>
        /// merr vitin fillim dhe fund te ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal DataRow merrNdermVitFillimFundGjitheVitetSipasNdermarjes(int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrNdermVitFillimFundGjitheVitetSipasNdermarjes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(int idNdermarrje)
        {
            //string datefill;

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrNdermarjeVitSipasNdermarjesDheDatesAktuale");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Nuk perdoret me!", true)]
        //public colNdermarrjeVitet merrndermvitet(int idndermarrjevit)
        //{
        //    //string datefill;
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    String sqlstr = "select *  from T_NDERMARJEVITI where  IDNDERVITI =" + idndermarrjevit;
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //        colNdermarrjeVitet vitet = new colNdermarrjeVitet();
        //        return vitet.mbushArrayListNdermarrjeVitet(ds);
        //        //DataRow rreshti = ds.Tables[0].Rows[0];
        //        //datefill = rreshti[0].ToString();
        //        //return datefill;
        //    }
        //    catch (Exception)
        //    {
        //        return new colNdermarrjeVitet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheNdermarrjeVitet()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrGjitheNdermarrjeVitet");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheNdermarrjeVitet()", true)]
        //public colNdermarrjeVitet merrGjitheNdermarrjeVitet()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrGjitheNdermarrjeVitet");
        //        colNdermarrjeVitet vitet = new colNdermarrjeVitet();
        //        return vitet.mbushArrayListNdermarrjeVitet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNdermarrjeVitet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataTable merrVitetENdermarrjes(int ndermarrja)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", ndermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_merrVitetENdermarrjes");
            return ds.Tables[0];

        }

        internal DataTable merrVitetENdermarrjes(string ndermarrjet)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDNDERMARRJE", ndermarrjet);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_merrVitetENdermarrjesSipasIdve");
            return ds.Tables[0];
        }

        internal DataSet merrVitetEMundshme()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_merrGjitheVitetEMundshme");
            return ds;

        }

        internal DataTable ktheVitetENdermarrjeve(ArrayList ndermarrjet)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            String nderm = "(";
            foreach (Object i in ndermarrjet)
            {
                nderm += i + ",";
            }
            nderm = nderm.Remove(nderm.Length - 1);
            nderm += ")";
            String sqlstr = "select distinct 1 as  IDNDERVITI,IDVITI, VITI, 1 as IDNDERMARJE, NDERMVITMBYLLUR,'01/01/2009' as NDERMVITFILLIM,'01/01/2009' as NDERMVITFUND,1 as IDPERDORUESI from T_NDERMARJEVITI where IDNDERMARJE in " + nderm;

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheVitetENdermarrjeve(ArrayList ndermarrjet)", true)]
        //public colNdermarrjeVitet merrVitetENdermarrjeve(ArrayList ndermarrjet)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    String nderm = "(";
        //    foreach (Object i in ndermarrjet)
        //    {
        //        nderm += i + ",";
        //    }
        //    nderm = nderm.Remove(nderm.Length - 1);
        //    nderm += ")";


        //    String sqlstr = "select distinct 1 as  IDNDERVITI,IDVITI, VITI, 1 as IDNDERMARJE, NDERMVITMBYLLUR,'01/01/2009' as NDERMVITFILLIM,'01/01/2009' as NDERMVITFUND,1 as IDPERDORUESI from T_NDERMARJEVITI where IDNDERMARJE in " + nderm;
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //        colNdermarrjeVitet vitet = new colNdermarrjeVitet();
        //        return vitet.mbushArrayListNdermarrjeVitet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colNdermarrjeVitet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        #region NDERMARRJET

        internal clsMesazh shtoDokumentaDefaultNdermarje(int idNderm, int idndermnga)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMNGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NIVELREGJISTRIMI_shtoDokumentatDefault");
            clsMesazh mesazh = new clsMesazh(true, "Shtimi perfundoi me sukses!");
            return mesazh;
        }

        internal clsMesazh shtoRapDesignDefault(int idNderm, int idndermnga)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMNGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_shtoDesignDefault");
            clsMesazh mesazh = new clsMesazh(true, "Shtimi perfundoi me sukses!");
            return mesazh;
        }

        internal string merrEmailTeBijavePerNdermarrjen(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMPRINDI", idNdermarrje, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_merrEmailTeBijave"));
        }

        internal clsMesazh shtoVleratDefaultNdermarrjes(String emriTabeles, int idNderm, int idndermviti, int idndermnga)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@EMRITABELES", emriTabeles, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERVITI", idndermviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMNGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJE_shtoVleratDefaultNdermarrjes");
            clsMesazh mesazh = new clsMesazh(true, "Shtimi perfundoi me sukses!");
            return mesazh;
        }

        /// <summary>
        /// kthen vitin me ane te nje store procedure per nje ndermarrje te re
        /// </summary>
        /// <param name="id">merr si parameter id e vitit</param>
        /// <returns>kthen ne Datatable vitet</returns>
        internal DataTable merrVitPerNdermarrjenRe(int id)
        {
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_ktheVit");
            return ds.Tables[0];
        }

        public clsMesazh ruajNdermarrjeReVit(out int idNderViti, int idViti, int viti, int idNdermarrje, bool ndermarrjeVitiMbyllur, DateTime ndermarrjeVitiFillim, DateTime ndermarrjeVitiFund, int idPerdoruesi)
        {//kjo metode duhet te thirret brenda transaksionit prandaj nuk eshte perdorur metoda ekzistuese ruajNdermarrjeVit
            idNderViti = -1;
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDNDERVITI", idNderViti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NDERMVITMBYLLUR", ndermarrjeVitiMbyllur, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NDERMVITFILLIM", ndermarrjeVitiFillim, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NDERMVITFUND", ndermarrjeVitiFund, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_ins");
            idNderViti = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        public clsMesazh ruajNdermarrje(out int idndermarrje, String ndermarrjekodi, String ndermarrjepershkrimi, String ndermarrjevendi, String ndermarrjenipt, int ndermarrjemonedha, int ndermarrjeqyteti, String ndermarrjetel, String ndermarrjefax, String ndermarrjeemail, String ndermarrjelicenca, String ndermarrjekodifiskal, int idperdoruesi, int idviti, int llojndermarje, int idlicenca, int idstatusdok, int idtaksa, byte[] ndermarrjeLogo, string nrtvsh, int lloji, bool prind, int idprindi, int idgrupi, string kodFurnitori, string emerFurnitori, string nrLlogariFurnitori, double limitishitjes, bool ownshop, bool logu, int raportuesi, int nivelstrukture,bool fiskalizim,string kodbiznesi,string pathname,bool meTvsh, bool klientFiskalizimi)
        {
            idndermarrje = -1;
            dbManager.Open();
            if(klientFiskalizimi)
                dbManager.CreateParameters(36);
            else
                dbManager.CreateParameters(35);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NDERMARJEKODI", ndermarrjekodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARJEPERSHK", ndermarrjepershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDERMARJEVEND", ndermarrjevendi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NDERMARJENIPT", ndermarrjenipt, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NDERMARJEMON", ndermarrjemonedha, ParameterDirection.Input);
            if (ndermarrjeqyteti == 0 || ndermarrjeqyteti == -1)
                dbManager.AddParameters(6, "@NDERMARJAQYTETI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@NDERMARJAQYTETI", ndermarrjeqyteti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NDERMARJETEL", ndermarrjetel, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NDERMARJEFAX", ndermarrjefax, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NDERMARJEMAIL", ndermarrjeemail, ParameterDirection.Input);
            dbManager.AddParameters(10, "@NDERMARJELICEN", ndermarrjelicenca, ParameterDirection.Input);
            dbManager.AddParameters(11, "@NDERMARJEKODIFISK ", ndermarrjekodifiskal, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI ", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDVITI", idviti, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJNDERMARJE", llojndermarje, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idtaksa == 0)
                dbManager.AddParameters(17, "@NIVELTVSH", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(17, "@NIVELTVSH", idtaksa, ParameterDirection.Input);
            dbManager.AddParameters(18, "@LOGONDERMARRJE", ndermarrjeLogo, ParameterDirection.Input);
            dbManager.AddParameters(19, "@NRTVSH", nrtvsh, ParameterDirection.Input);
            dbManager.AddParameters(20, "@Lloji", lloji, ParameterDirection.Input);
            dbManager.AddParameters(21, "@PRIND", prind, ParameterDirection.Input);
            if (idprindi > 0)
                dbManager.AddParameters(22, "@IDPRINDI", idprindi, ParameterDirection.Input);
            else
                dbManager.AddParameters(22, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            if (idgrupi > 0)
                dbManager.AddParameters(23, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            else
                dbManager.AddParameters(23, "@IDGRUPI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(24, "@KODFURNITORI", kodFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(25, "@EMERFURNITORI", emerFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(26, "@NRLLOGARIFURNITORI", nrLlogariFurnitori, ParameterDirection.Input);
            dbManager.AddParameters(27, "@LIMITISHITJES", limitishitjes, ParameterDirection.Input);
            dbManager.AddParameters(28, "@OWNSHOP", ownshop, ParameterDirection.Input);
            dbManager.AddParameters(29, "@LOGU", logu, ParameterDirection.Input);
            if (raportuesi == 0 || raportuesi == -1)
                dbManager.AddParameters(30, "@RAPORTUESI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(30, "@RAPORTUESI", raportuesi, ParameterDirection.Input);
            dbManager.AddParameters(31, "@NIVELSTRUKTURE", nivelstrukture, ParameterDirection.Input);
            dbManager.AddParameters(32, "@FISKALIZIM", fiskalizim, ParameterDirection.Input);
            dbManager.AddParameters(33, "@KODBIZNESI", kodbiznesi, ParameterDirection.Input);
            dbManager.AddParameters(34, "@pathname", pathname, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(35, "@METVSH", meTvsh, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJE_ins");
            idndermarrje = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");
        }

        internal clsMesazh modifikoNder(int idndermarrje, String ndermarrjekodi, String ndermarrjepershkrimi, String ndermarrjevendi, String ndermarrjenipt, int ndermarrjemonedha, int ndermarrjeqyteti, String ndermarrjetel, String ndermarrjefax, String ndermarrjeemail, String ndermarrjelicenca, String ndermarrjekodifiskal, int idperdoruesi, int idviti, int llojndermarje, int idlicenca, int idstatusdok, int idtaksa, byte[] ndermarrjeLogo, string nrtvsh, bool prind, int idprindi, int idgrupi, double limitishitjes, bool ownshop, bool logu, int raportuesi, int nivelstrukture,bool fiskalizim,string kodbiznesi,string pathname, bool meTvsh,bool klientFiskalizimi)
        {
            dbManager.Open();
            if(klientFiskalizimi)
                dbManager.CreateParameters(32);
            else
                dbManager.CreateParameters(31);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NDERMARJEKODI", ndermarrjekodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARJEPERSHK", ndermarrjepershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDERMARJEVEND", ndermarrjevendi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NDERMARJENIPT", ndermarrjenipt, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NDERMARJEMON", ndermarrjemonedha, ParameterDirection.Input);
            if (ndermarrjeqyteti == 0 || ndermarrjeqyteti == -1)
                dbManager.AddParameters(6, "@NDERMARJAQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@NDERMARJAQYTETI", ndermarrjeqyteti, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NDERMARJETEL", ndermarrjetel, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NDERMARJEFAX", ndermarrjefax, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NDERMARJEMAIL", ndermarrjeemail, ParameterDirection.Input);
            dbManager.AddParameters(10, "@NDERMARJELICEN", ndermarrjelicenca, ParameterDirection.Input);
            dbManager.AddParameters(11, "@NDERMARJEKODIFISK ", ndermarrjekodifiskal, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDPERDORUESI ", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDVITI", idviti, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJNDERMARJE", llojndermarje, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (idtaksa == 0)
                dbManager.AddParameters(17, "@NIVELTVSH", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(17, "@NIVELTVSH", idtaksa, ParameterDirection.Input);
            dbManager.AddParameters(18, "@LOGONDERMARRJE", ndermarrjeLogo, ParameterDirection.Input);
            dbManager.AddParameters(19, "@NRTVSH", nrtvsh, ParameterDirection.Input);
            dbManager.AddParameters(20, "@PRIND", prind, ParameterDirection.Input);
            if (idprindi > 0)
                dbManager.AddParameters(21, "@IDPRINDI", idprindi, ParameterDirection.Input);
            else dbManager.AddParameters(21, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            if (idgrupi > 0)
                dbManager.AddParameters(22, "@IDGRUPI", idgrupi, ParameterDirection.Input);
            else dbManager.AddParameters(22, "@IDGRUPI", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(23, "@LIMITISHITJES", limitishitjes, ParameterDirection.Input);
            dbManager.AddParameters(24, "@OWNSHOP", ownshop, ParameterDirection.Input);
            dbManager.AddParameters(25, "@LOGU", logu, ParameterDirection.Input);
            if (raportuesi == 0 || raportuesi == -1)
                dbManager.AddParameters(26, "@RAPORTUESI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(26, "@RAPORTUESI", raportuesi, ParameterDirection.Input);
            dbManager.AddParameters(27, "@NIVELSTRUKTURE", nivelstrukture, ParameterDirection.Input);
            dbManager.AddParameters(28, "@FISKALIZIM", fiskalizim, ParameterDirection.Input);
            dbManager.AddParameters(29, "@KODBIZNESI", kodbiznesi, ParameterDirection.Input);
            dbManager.AddParameters(30, "@pathname", pathname, ParameterDirection.Input);
            if(klientFiskalizimi)
                dbManager.AddParameters(31, "@METVSH", meTvsh, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        internal clsMesazh RuajPath(int idndermarrje, string path)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PATHNAME", path, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJE_updPath");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        internal clsMesazh fshiNdermarrje(int idndermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJE_fshiNdermarje");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiNdermarrjeStatus(int idndermarrje, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NDERMARJE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataRow merrNdermarrje(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrje");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheNdermarjeTeRolit(int idRoli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_MerrNdermarjeTeRolit");
            return ds.Tables[0];
        }

        internal DataRow merrNdermarrje(string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NDERMARJEKODI", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrNdermarrjeMeme(int idlicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IdLicenca", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeMeme");
            return ds.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>      
        /// <returns></returns>
        public DataTable merrNdermarrjet(int idperdoruesi, int idlicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_merrGjitheNdermarrjet");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheNdermarrjet(int idperdoruesi, int idlicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_merrGjitheNdermarrjet");
            return ds.Tables[0];
        }

        internal DataTable merrNdermarrjeDefault()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeDefault");
            return ds.Tables[0];
        }

        /// <summary>
        /// Merr gjithe ndermarrjet e llojeve te licensave ''Zyre'' ose ''SAGIS''
        /// </summary>
        /// <param name="tePalidhura">True/False kerkon per te gjithe ndermarrjet apo vetem ato qe nuk kane akoma nje workspace te lidhur me te</param>
        /// <returns>Kthen nje DataTable me informacion IdNdermarrje/NdermarrjeKodi/NdermarrjePershkrimi </returns>
        internal DataTable merrNdermarrjePerGIS(bool tePalidhura)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@TEPALIDHURA", tePalidhura, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_merrSipasLicensesGIS");
            return ds.Tables[0];
        }

        //ben kontrollin nese ekziston nje ndermarrje tjeter me kete kod
        internal bool ekzistonNdermarrjeMeKeteKod(String ndermarrjekodi)
        {
            int nrNdermarrjesh;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NDERMARJEKODI", ndermarrjekodi, ParameterDirection.Input);
            nrNdermarrjesh = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_EkzistonNdermarrjeMeKeteKod"));
            if (nrNdermarrjesh > 0)
                return true;
            else
                return false;
        }

        internal bool ekzistonVitPerKeteNdermarrje(int idndermarrje, string kodViti)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODVITI", kodViti, ParameterDirection.Input);
            object vlere = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_kontrolloKaVitNdermarrjaSipasIdndermarrjeDheKodviti");
            if (vlere == null)
                return false;

            return Convert.ToInt32(vlere) > 0;
        }
        internal bool eshtePrindNdermarje(int idndermarje)
        {
            int nrNdermarrjesh;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            nrNdermarrjesh = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_eshtePrind"));
            if (nrNdermarrjesh > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal int ktheLicenceNdermarrje(int idndermarje)
        {
            int idLicenca;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            idLicenca = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheLicenceNdermarrje"));
            return idLicenca;
        }

        public bool ekzistonNdermarrjeSipasIdViti(int idViti)
        {
            int nrNdermarrjesh;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            nrNdermarrjesh = (int)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_EkzistonNdermarrjeSipasIdViti");
            if (nrNdermarrjesh > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal DataRow ktheMonedhenNdermarrjes(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheNderm");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheMonedhenKlientit(string kodKlientFurn, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKLIENTFURNITOR", kodKlientFurn, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheKlienti");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheMonedhenSipasKodArkaBanka(string kodArkaBanka, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARKABANKA", kodArkaBanka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheSipasKodArkeBanke");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal String ktheKodMonedheNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            String kodi = Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheNdermKod"));
            return kodi;
        }

        internal DataRow ktheNdermarjeDR(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarjeDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheNdermarjeBijSipasMemeDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idnderm, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeBijSipasMemes").Tables[0];
        }
        internal DataTable ktheNdermarjeBijSipasMemeDheOwnDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPRINDI", idnderm, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeBijSipasMemesDheOwn").Tables[0];
        }

        internal DataTable ktheNdermarjeBijSipasNdermRaportueseDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RAPORTUESI", idnderm, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarrjeBijSipasRaportueses").Tables[0];
        }

        internal DataTable ktheNdermarjetDT(int idperdorues, int idlicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarjetDT").Tables[0];
        }

        internal DataRow ktheNdermarjeDheViteDR(int idnderviti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERviti", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarjeDheViteDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable ktheNdermarjetDheViteDT(int idperdorues, int idlicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheNdermarjetDheViteDT");
            return ds.Tables[0];
        }

        /// <summary> kujdes metoda eshte e njejte tek DbCore.DbRegjistrim.DbListpagese.clsDatabazeListPagese por per shkak te mos aksesimit u vendos dhe ketu
        /// ekzekuton 	[prc_T_KOMPONENTEPAGE_insDefaultSipasLlojit] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        ///<see cref="clsDatabazeListPagesa.ruajDefaultKomponentePage"/>
        /// <param name="lloji">lloji komponente apo listpagese</param>
        /// <param name="data">data e aktivizimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajDefaultKomponentePage(bool lloji, DateTime data, int idPerdoruesi, int idnderm, int idndermnga)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_insDefaultSipasLlojit");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        internal clsMesazh ruajDefaultKomponentePageKlono(bool lloji, int idPerdoruesi, int idnderm, int idndermnga)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOMPONENTEPAGE_insDefaultSipasLlojitKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }
        internal clsMesazh ruajDefaultKlonoFushaShtese(int idPerdoruesi, int idnderm, int idndermnga)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_insKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }
        internal clsMesazh ruajDefaultKlonoFormatNr(int idPerdoruesi, int idnderm, int idndermnga)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_insKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }
        internal clsMesazh ruajDefaultKlonoShperndarjeQk(int idPerdoruesi, int idnderm, int idndermnga)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOGARISHPERNDARJEQK_insKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }
        internal clsMesazh ruajDefaultKlonoNrAuto(int idPerdoruesi, int idnderm, int idndermnga)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOM_insKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }
        internal clsMesazh ruajDefaultKlonoInfo(int idPerdoruesi, int idnderm, int idndermnga)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOKOKA_insKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }
        internal clsMesazh ruajDefaultKonfigurimeKasaKlono(int idPerdoruesi, int idnderm, int idndermnga)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJENGA", idndermnga, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMKASA_insKlono");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);


        }

        /// <summary> kujdes metoda eshte e njejte tek DbCore.DbRegjistrim.DbListpagese.clsDatabazeListPagese por per shkak te mos aksesimit u vendos dhe ketu
        /// ekzekuton 	[prc_T_SIGURIME_insDefault] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// shton sigurimet default per kete ndermarje 
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajDefaultSigurime(int idPerdoruesi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SIGURIME_insDefault");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>kujdes metoda eshte e njejte tek DbCore.DbRegjistrim.DbListpagese.clsDatabazeListPagese por per shkak te mos aksesimit u vendos dhe ketu
        /// ekzekuton 	[prc_T_TATIME_insDefault] ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// shton TATIMET default per kete ndermarje per kete lloj
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <returns>nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        internal clsMesazh ruajDefaultTatime(int idPerdoruesi, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TATIME_insDefault");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal DataTable merrNdermarrjetIdRoli(int idRoli, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@IDROLI", idRoli);
            dbManager.AddInputParameters("@IDPERDORUESI", idPerdoruesi);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTAKOKA_merrNdermarrjetIdRoli");
            return ds.Tables[0];
        }
        public DataTable merrNdermarrjeTePalidhuraMeRaportin(int idDesign)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idDesign", idDesign, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARRJE_merrNdermarrje_Design");
            return ds.Tables[0];
        }

        public void lidhNdermarjetMeFormatin(string ndermarrjet, int idDesign)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idDesign", idDesign, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KodetENdermarrjeve", ndermarrjet, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPXIDESIGN_lidhNdermarrjetMeFormatin_ins");
        }

        public DataTable MerrIdVitetMeKodPerNdermarrje(string ids)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDS", ids);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_MerrIdVitetMeKodPerNdermarrje");
            return ds.Tables[0];
        }

        #endregion

        #region AGJENTET E SHITJES

        internal clsMesazh ruajAgjentShitje(out int id, string kodi, string emri, string mbiemri, string tel, string fax, string email, int qyt, double perq, int llog, int idnderm, int idkonfig, int idstatusdok, int idperdorues, colLidhjetAutorizim oColLidhjetAutorizim, int idperdoruesmobile, int idDrejtori)
        {
            id = -1;


            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@IDAGJENTSHITJE", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIAGJENTSHITJE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMRIAGJENTSHITJE", emri, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMRIAGJENTSHITJE", mbiemri, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TELAGJENTSHITJE", tel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FAXAGJENTSHITJE", fax, ParameterDirection.Input);
            dbManager.AddParameters(6, "@EMAILAGJENTSHITJE", email, ParameterDirection.Input);
            if (qyt == 0) dbManager.AddParameters(7, "@IDQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDQYTETI", qyt, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERQINDJEAGJENTSHITJE", perq, ParameterDirection.Input);
            if (llog == 0 || llog == -1)
                dbManager.AddParameters(9, "@IDLLOGARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDLLOGARI", llog, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            if (idperdoruesmobile == 0)
                dbManager.AddParameters(14, "@IDPERDORUESMOBILE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDPERDORUESMOBILE", idperdoruesmobile, ParameterDirection.Input);
            if (idDrejtori == 0) dbManager.AddParameters(15, "@IDDREJTORI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDDREJTORI", idDrejtori, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");


        }

        internal clsMesazh modifikoAgjentShitje(int id, string kodi, string emri, string mbiemri, string tel, string fax, string email, int qyt, double perq, int llog, int idnderm, int idkonfig, int idstatusdok, int idperdorues, colLidhjetAutorizim oColLidhjetAutorizim, int idperdoruesmobile, int idDrejtori)
        {



            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@IDAGJENTSHITJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIAGJENTSHITJE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMRIAGJENTSHITJE", emri, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBIEMRIAGJENTSHITJE", mbiemri, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TELAGJENTSHITJE", tel, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FAXAGJENTSHITJE", fax, ParameterDirection.Input);
            dbManager.AddParameters(6, "@EMAILAGJENTSHITJE", email, ParameterDirection.Input);
            if (qyt == 0) dbManager.AddParameters(7, "@IDQYTETI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDQYTETI", qyt, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PERQINDJEAGJENTSHITJE", perq, ParameterDirection.Input);
            if (llog == 0 || llog == -1)
                dbManager.AddParameters(9, "@IDLLOGARI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDLLOGARI", llog, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            if (idperdoruesmobile == 0)
                dbManager.AddParameters(14, "@IDPERDORUESMOBILE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(14, "@IDPERDORUESMOBILE", idperdoruesmobile, ParameterDirection.Input);
            if (idDrejtori == 0) dbManager.AddParameters(15, "@IDDREJTORI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@IDDREJTORI", idDrejtori, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiAgjentShitje(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAGJENTSHITJE", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiAgjentShitjestatus(int id, int idperdorues)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAGJENTSHITJE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        internal DataRow merrAgjentShitje(int id)
        {

            ImbLogger.LogTraceShitje($"Filloi metoda merr agjent shitje me id:{id}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAGJENTSHITJE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_ktheAgjentShitje");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            ImbLogger.LogTraceShitje($"Mbaroi metoda merr agjent shitje me id:{id}");
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Therret sp prc_T_AGJENTSHITJE_merrPerqindjeAgjentiEdheSipasKlientit per te marre perqindjen e agjentit te lidhur me klientin e kerkuar. Ne qofte se ky agjent ka perqindje te kartela e klientit, merret perqindja e vendosur tek kartela e klientit, perndryshe merret perqindja e vendosur tek kartela e agjentit.
        /// </summary>
        /// <param name="idAgjenti">id e agjentit te kerkuar</param>
        /// <param name="idKlienti"></param>
        /// <param name="llojAgjenti"></param>
        /// <returns></returns>
        internal double merrPerqindjeAgjentiEdheSipasKlientit(int idAgjenti, int idKlienti, int llojAgjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDAGJENTSHITJE", idAgjenti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKLIENT", idKlienti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJAGJENTI", llojAgjenti, ParameterDirection.Input);
            return Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_merrPerqindjeAgjentiEdheSipasKlientit").ToString());
        }


        internal DataRow ktheAgjentShitjeSipasKodit(string kodi, int idnderm)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_merrAgjentShitjeSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheGjitheAgjentetShitjesNdermarrjeAutorizimDT(int idndermarje, int idperdorues)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_ktheAgjentNdermarrjesAndAutorizimeDT");
            return ds.Tables[0];

        }

        internal DataRow merrAgjentShitjeSipasIdPeroruesMobile(int idNdermarrje, int idPeroruesMobile)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESMOBILE", idPeroruesMobile, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_ktheAgjentSHitjeSipasIdPErdoruesMobile");
            if (ds.Tables[0].Rows.Count > 1)
                throw new MyException($"AGjenti me id perdoruesi {idPeroruesMobile} dhe id ndermarrje {idNdermarrje} u gjet me shume se njehere");
            return ds.Tables[0].Rows[0];
        }

        internal DataRow kthAgjentetNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idagj)
        {




            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDAGJENT", idagj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_ktheAgjentNdermarrjesAndAutorizimeDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheGjitheAgjentetShitjes(int idndermarje)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_merrGjitheAgjentetShitjes");
            return ds.Tables[0];

        }
        internal List<string> merrEmaileDrejtoreshDheAgjentiShitje(int idAgjentShitje)
        {
            ImbLogger.LogTraceShitje($"Filloi marrja e email te agjentit dhe drejtorit me id agjenti shitje :{idAgjentShitje}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDAGJENTI", idAgjentShitje);
            ImbLogger.LogTraceShitje($"Perfundoi marrja nga DB e email te agjentit dhe drejtorit me id agjenti shitje :{idAgjentShitje}");
            return dbManager.GetList<string>("prc_T_AGJENTSHITJE_merrEmaileAgjentiDheDrejtori");

        }
        internal bool ekzistonAgjentShitjeMeKeteKod(string kodi, int idnderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIAGJENTSHITJE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_EkzistonAgjentShitjeMeKeteKod");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// kthen IdAgjent ne baze te kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kodAgjenti"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal int ktheIdAgjentShitjeSipasKodDheNdermarrje(string kodAgjenti, int idNdermarrje)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIAGJENTSHITJE", kodAgjenti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_ktheIdAgjentShitje"));

        }

        internal clsMesazh fshiKonfigurimFtp(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMFTP", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiWebhook(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDWEBHOOK", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_WEBHOOKS_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: bool ekzistonAgjentShitjeMeKeteKod(string kodi, int idnderm)", true)]
        //private bool ekzistonAgjentShitjeMeKeteKod(clsAgjentShitje agjent)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODIAGJENTSHITJE", agjent.KodiAgjentShitje, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDNDERMARJE", agjent.IdNdermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AGJENTSHITJE_EkzistonAgjentShitjeMeKeteKod");
        //        if (ds.Tables[0].Rows.Count == 1)
        //            return true;
        //        else if (ds.Tables[0].Rows.Count == 0)
        //            return false;
        //        else return true;
        //    }
        //    catch (Exception)
        //    {
        //        return true;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheKonfigurimetFtpSipasNdermarrje(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_ktheKonfigurimFtpNdermarrjes");
            return ds.Tables[0];
        }
        internal DataTable ktheGjitheWebhookSipasNdermarrje(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_WEBHOOKS_ktheWebhookNdermarrjes");
            return ds.Tables[0];
        }

        internal DataRow ktheKonfigurimFtpSipasIdDR(int idKonfigurimFtp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMEFTP", idKonfigurimFtp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_selSipasIdDr");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheWebhookSipasIdDR(int idWebhook)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDWEBHOOK", idWebhook, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_WEBHOOKS_selSipasIdDr");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        #endregion

        #region GRIDAT

        internal DataTable ktheTrupin(int idGjuha, int idGrida)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idgjuha", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GRIDAKOKAID", idGrida, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_merrTrupin");
            return ds.Tables[0];

        }

        public clsMesazh fshiTrupin(int idGrida)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@GRIDAKOKAID", idGrida, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_fshiTrupin");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal void RuajGridaKoka(int idGridaKoka, int topRows, int menyreFiltrimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@GRIDAKOKAID", idGridaKoka);
            dbManager.AddInputParameters("@TOPROWS", topRows);
            dbManager.AddInputParameters("@MENYREFILTRIMI", menyreFiltrimi);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRIDAKOKA_upd");
        }
        public clsMesazh fshiKoka(int idGrida)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@GRIDAKOKAID", idGrida, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_fshiKoka");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //TOTEST getson
        internal void ktheGridaKokaByEmri(string emriGrida, string emriKomponente, int idNdermarrje, clsGridaKoka grida)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@GRIDKOKAEMRI", emriGrida, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KOMPONEMRI", emriKomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillObject<clsGridaKoka>("prc_T_GRIDAKOKA_merrGridaKokaByEmri", grida.mbushGridKok);


        }
        //TOTEST
        internal void ktheGridaKokaByEmrisipasIdKonfigurimi(string emriGrida, string emriKomponente, int idNdermarrje, int idkonf, clsGridaKoka grida)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@GRIDKOKAEMRI", emriGrida, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KOMPONEMRI", emriKomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIGAMBJENTE", idkonf, ParameterDirection.Input);
            dbManager.FillObject<clsGridaKoka>("prc_T_GRIDAKOKA_merrGridaKokaByEmriAndIdKonfigurimi", grida.mbushGridKok);

        }

        //TOTEST getson
        internal void ktheGridaKoka(int idkoka, clsGridaKoka grida)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@GRIDKOKAID", idkoka, ParameterDirection.Input);
            dbManager.FillObject<clsGridaKoka>("prc_T_GRIDAKOKA_merrGridaKoka", grida.mbushGridKok);

        }

        //TOTEST getson
        internal void ktheGridaKokaByKonfigurim(int idkonf, clsGridaKoka grida)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idkonf, ParameterDirection.Input);
            dbManager.FillObject<clsGridaKoka>("prc_T_GRIDAKOKA_merrGridaKokaByKonfigurim", grida.mbushGridKok);

        }

        internal clsMesazh ruajLupaMultiple(int idTrupi, int idKonfAmbLupa, int idKonfig)
        { //metoda per ruajtjen e filtrit



            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKONFIGMULTIPLE", 0, ParameterDirection.Output);
            dbManager.AddParameters(1, "@GRIDATRUPIID", idTrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIGAMBJENTELUPA", idKonfAmbLupa, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIGAMBJENTE", idKonfig, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LUPAMULTIPLE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;


        }

        internal clsMesazh fshiLupaMultiple(int idkonfigambjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LUPAMULTIPLE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataRow ktheLupaMultipleSipasGridaTrupi(int idTrupi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@GRIDATRUPIID", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LUPAMULTIPLE_selSipasIdGridaTrupi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal clsMesazh vendosVisibleKostoGjendje(int idkonfig, bool kostogjendje, String gridatrupikod)
        {//metoda per shfaqjen ose jo te kolonave kosto dhe gjendje te lupa e artikullit
            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GRIDATRUPIVISIBLE", kostogjendje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GRIDATRUPIKODI", gridatrupikod, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_updVisibleKostoGjendje");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }


        /// <summary>
        /// merr nje IEnumerable me konfigurimet e grides
        /// </summary>
        /// <param name="emriGrides"></param>
        /// <param name="idKomponente"></param>
        /// <param name="idKonfigurim"></param>
        /// <param name="idGjuha"></param>
        /// <returns></returns>
        public IEnumerable<clsGridaTrupi> merrGridenKonfigurimitKomponentesSipasGridesNew(string emriGrides, int idKomponente, int idKonfigurim, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMRIGRIDES", emriGrides, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentesSipasGrides", clsGridaTrupi.Create);
        }
        internal DataTable merrGridenKonfigurimitKomponentesSipasGjuhes(int idKomponente, int idKonfigurim, int idGjuha)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentesSipasGjuhes");
            return ds.Tables[0];

        }
        internal DataTable merrGridenKonfigurimitKomponentesSipasGjuhes(string emerKomponente, int idKonfigurim, int idGjuha)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@EMERKOMPONENTE", emerKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentesSipasGjuhesEmerKomp").Tables[0];
        }
        internal DataTable merrGridenKonfigurimitKomponentes(int idKomponente, int idKonfigurim, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentes");
            return ds.Tables[0];

        }

        internal DataTable merrGridenKonfigurimitKomponentesSipasGrides(string emriGrides, int idKomponente, int idKonfigurim, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMRIGRIDES", emriGrides, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGridenKonfigurimitKomponentesSipasGrides");
            return ds.Tables[0];
        }
        internal DataTable merrGridenKonfigurimit(int idKonfigurim, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGridatEKonfigurimit");
            return ds.Tables[0];
        }

        internal DataTable merrGrideTrupinSipasEmerGride(int idKonfigurim, string emriGrides, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);

            dbManager.AddParameters(0, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMRIGRIDES", emriGrides, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGrideTrupinSipasEmerGride");
            return ds.Tables[0];

        }

        internal DataTable merrGrideTrupinSipasEmerGrideDheKomponente(int idKonfigurim, string emriGrides, int idGjuha, string emerKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);

            dbManager.AddParameters(0, "@IDKONFIG", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMRIGRIDES", emriGrides, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERKOMP", emerKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ktheGrideTrupinSipasEmerGrideDheKomponente");
            return ds.Tables[0];

        }

        #endregion

        #region AMBIENT MODULET

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable merrAmbientModulet()
        {



            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AMBJENTIMODULI_merrAmbjentModul");
            return ds.Tables[0];


        }

        #endregion

        #region KLASA OBJEKTESH

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable merrKlaseObjektet()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KLASEOBJEKTESH_merrKlasat");
            return ds.Tables[0];


        }

        #endregion

        #region MODULET

        /// <summary>
        /// Kthen nje dataTable me gjihte modulet ne sistem.
        /// </summary>
        /// <returns>DataTable me te njejten form me cfare kthen storeprocedura</returns>
        internal DataTable merrModulet()
        {


            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrGjitheModulet");
            //colModulet modulet = new colModulet();
            return ds.Tables[0];

        }

        internal DataTable ktheGjitheModulet()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrGjitheModulet");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheModulet()", true)]
        //public colModulet merrGjitheModulet()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrGjitheModulet");
        //        colModulet modulet = new colModulet();
        //        return modulet.mbushArrayListModulet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colModulet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheModuleteRaporteve()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrGjitheModuleteRaporteve");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheModuleteRaporteve()", true)]
        //public colModulet merrGjitheModuleteRaporteve()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrGjitheModuleteRaporteve");
        //        colModulet modulet = new colModulet();
        //        return modulet.mbushArrayListModulet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colModulet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable kthePemen()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrPemen");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable kthePemen()", true)]
        //public colPeme merrPemen()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrPemen");
        //        colPeme peme = new colPeme();
        //        return peme.mbushArrayListPeme(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colPeme();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow kthePemenEModulit(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODULI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrPemenEModulit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Metoda ktheModulById kthen modulin me id vleren qe i kalohet kesaj metode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal DataRow ktheModulById(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@MODID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow kthePemenEModulit(int id)", true)]
        //public colPeme merrPemenEModulit(int id)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDMODULI", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrPemenEModulit");
        //        colPeme peme = new colPeme();
        //        return peme.mbushArrayListPeme(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colPeme();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal int merrAmbjentinKomponentes(int IdKomponente)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrAmbjentinKomponentes");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return int.Parse(dr[0].ToString());
            }
            else
            {
                return -1;
            }

        }

        internal int merrModulinKomponentes(int IdKomponente)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODULI_merrModulinKomponentes");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                return int.Parse(dr[0].ToString());
            }
            else
            {
                return -1;
            }

        }

        internal bool pastroMap()
        {

            string sql = "delete from t_raportet_sitemap where parentid>0";

            dbManager.Open();

            return bool.Parse(dbManager.ExecuteNonQuery(CommandType.Text, sql).ToString());


        }

        internal bool mapRaportetinsert(int id, int parentid, string navigateurl)
        {

            string sql = "insert into t_raportet_sitemap values (" + id + "," + parentid + ",'" + navigateurl + "','')";

            dbManager.Open();
            return bool.Parse(dbManager.ExecuteNonQuery(CommandType.Text, sql).ToString());

        }

        internal int merrModulinAmbjentit(int IdAmbjentiModuli)
        {

            string sql = "select IDMODULI from T_AMBJENTIMODULI where IDAMBJMODULI=" + IdAmbjentiModuli;

            dbManager.Open();
            //dbManager.CreateParameters(1);
            //dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            DataRow dr = ds.Tables[0].Rows[0];
            return int.Parse(dr[0].ToString());

        }

        internal DataTable ktheKomponentetAmbjentit(int idllojlicence)
        {

            string sql = " select T_KOMPONENTE.* from T_KOMPONENTE inner join T_KOMPONENTEPERLLOJLICENCE on IDKOMPONENTE=idkompon where IDLLOJLICENCE= " + idllojlicence;

            ArrayList arr = new ArrayList();
            dbManager.Open();
            //dbManager.CreateParameters(1);
            //dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheKomponentetAmbjentit()", true)]
        //public colKomponentet merrKomponentetAmbjentit()
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    string sql = " select * from T_KOMPONENTE ";
        //    try
        //    {
        //        ArrayList arr = new ArrayList();
        //       dbManager.Open();
        //        //dbManager.CreateParameters(1);
        //        //dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
        //        colKomponentet komp = new colKomponentet();

        //        return komp.mbushArrayListKomponentet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKomponentet();
        //    }
        //}

        internal DataTable ktheKomponentetAmbjentitSipasModulit(int idModuli, int idllojlicence)
        {

            string sql = " select T_KOMPONENTE.* from T_KOMPONENTE inner join T_KOMPONENTEPERLLOJLICENCE  on IDKOMPONENTE=idkompon where idmoduli= " + idModuli + " and IDLLOJLICENCE= " + idllojlicence;

            ArrayList arr = new ArrayList();
            dbManager.Open();
            //dbManager.CreateParameters(1);
            //dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheKomponentetAmbjentitSipasModulit(int idModuli)", true)]
        //public colKomponentet merrKomponentetAmbjentitSipasModulit(int idModuli)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    string sql = " select * from T_KOMPONENTE where idmoduli= " + idModuli;
        //    try
        //    {
        //        ArrayList arr = new ArrayList();
        //        dbManager.Open();
        //        //dbManager.CreateParameters(1);
        //        //dbManager.AddParameters(0, "@IDKOMPON", IdKomponente, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
        //        colKomponentet komp = new colKomponentet();

        //        return komp.mbushArrayListKomponentet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKomponentet();
        //    }
        //}

        internal String merrAmbjenteDheKomponente(int idllojlicence)
        {


            String ambj = "";
            colKomponentet komp = new colKomponentet();
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AMBJENTIMODULI_merrIdAmbjentModule");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                ambj += dr[0].ToString();
                komp.mbushKomponentetAmbjentit(idllojlicence);
                //komp = merrKomponentetAmbjentit();
                for (int j = 0; j < komp.Count; j++)
                    ambj += "," + komp[j].IdKomponente;
                ambj += ";";

            }
            return ambj;

        }

        #endregion

        #region FILTRAT E GRIDES
        /// <summary>
        /// metode per ruajtjen e filtrave ne db
        /// </summary>
        /// <param name="idfiltri">id e filtrit parameter output</param>
        /// <param name="filtrakodi">kodi</param>
        /// <param name="filtrashenime">pershkrimi</param>
        /// <param name="filtravlera">vlera e filtrit</param>
        /// <param name="gridakokaid">id e grides</param>
        /// <param name="filtrauniversal">universal per te gjitha gridat</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="kolrend">kolona nga e cila renditet grida</param>
        /// <param name="drejtrend">drejtimi i renditjes</param>
        /// <param name="idnderm">id e ndermarjes</param>
        /// <param name="idstatusdok">id e statusit te dokumentit</param>
        /// <returns>kthen nje objekt te tipit mesazh qe tregon nese ruajtja ka perfunduar me sukses</returns>
        internal clsMesazh ruajFiltraGrida(out int idfiltri, string filtrakodi, String filtrashenime, string filtravlera, int gridakokaid, bool filtrauniversal, int idperdoruesi, string kolrend, bool drejtrend, int idnderm, int idstatusdok)
        {
            idfiltri = -1;

            if (!ekzistonFilterNew(filtrakodi, idnderm, gridakokaid))
            {
                dbManager.Open();
                dbManager.CreateParameters(11);
                dbManager.AddParameters(0, "@IDFILTRI", idfiltri, ParameterDirection.Output);
                dbManager.AddParameters(1, "@FILTRIKODI", filtrakodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@FILTRISHENIME", filtrashenime, ParameterDirection.Input);
                dbManager.AddParameters(3, "@FILTRIVLERA", filtravlera, ParameterDirection.Input);
                dbManager.AddParameters(4, "@GRIDKOKAID", gridakokaid, ParameterDirection.Input);
                dbManager.AddParameters(5, "@FILTRIUNIVERSAL", filtrauniversal, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(7, "@KOLONERENDITJE", kolrend, ParameterDirection.Input);
                dbManager.AddParameters(8, "@DREJTIMRENDITJE", drejtrend, ParameterDirection.Input);
                dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_ins");
                idfiltri = int.Parse(dbManager.Parameters[0].Value.ToString());
                clsMesazh mesazh = new clsMesazh(true, "Filtri per kete gride u ruajt me sukses!");
                return mesazh;
            }
            else
            {
                return new clsMesazh(false, "Ekziston nje filter me te njejtin kod!");
            }
        }

        internal clsMesazh modifikoFiltraGrida(int idfiltri, string filtrakodi, String filtrashenime, string filtravlera, int gridakokaid, bool filtrauniversal, int idperdoruesi, string kolrend, bool drejtrend, int idnderm, int idstatusdok)
        {//metoda per modifikimin i filtrave


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDFILTRI", idfiltri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@FILTRIKODI", filtrakodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILTRISHENIME", filtrashenime, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILTRIVLERA", filtravlera, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GRIDKOKAID", gridakokaid, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FILTRIUNIVERSAL", filtrauniversal, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@KOLONERENDITJE", kolrend, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DREJTIMRENDITJE", drejtrend, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }


        public clsMesazh fshiFiltraGrida(int idfiltri)
        {//metoda per fshirjen e filtrave

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFILTRI", idfiltri, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTRIGRIDA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        public clsMesazh fshiFiltraGridaStatus(int idfiltri, int idperdorues)
        {//metoda per fshirjen e filtrave


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDFILTRI", idfiltri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal DataRow ktheFiltraGridaSipasId(int idFiltra)
        {//metoda per te marre te filtrin sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFILTER", idFiltra, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_merrFiltraGridaSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        internal DataTable ktheGjitheFiltratGridaByGridaKoka(int gridakoka, int idnderm)
        {//metoda per te marre te gjithe filtrat


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@gridkokaid", gridakoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_merrGjitheFiltratGridaByGridaKoka");
            return ds.Tables[0];

        }
        /// <summary>
        /// metode per marrjen e filtrave sipas id konfigurimit
        /// </summary>
        /// <param name="idkonfigurim">id e konfigurimit</param>
        /// <param name="idnderm">id e nderamrjes</param>
        /// <returns> data table me filtrat e ketij konfigurimi</returns>
        internal DataTable ktheGjitheFiltratGridaByKonfigurimi(int idkonfigurim, int idnderm)
        {//metoda per te marre te gjithe filtrat


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idkonfigurim", idkonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_merrGjitheFiltratGridaByKonfigurim");
            return ds.Tables[0];

        }
        internal bool ruajGjitheFiltratGridaByKonfigurimiNgaKlonimi(int idKonfigurimPrind, int idGridaKokaRe)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idKonfigurimPrind", idKonfigurimPrind, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idGridaKokaRe", idGridaKokaRe, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_ruajGjitheFiltratGridaByKonfigurimiNgaKlonimi"));
            return Convert.ToBoolean(pergjigje);

        }

        internal DataRow ktheFilterPerGrideSipasKodit(string filtrakodi, int idnderm, int idGridaKoka)
        {//metoda per te marre te filtrin sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@filtrikodi", filtrakodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGRIDAKOKA", idGridaKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_merrFiltraGridaSipasFiltraKodiNew");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        [Obsolete("Perdor: bool ekzistonFilterNew(String filtriKodi, int idnder, int idkoka)", false)]//pati
        public bool ekzistonFilter(String filtriKodi, int idnder)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@filtrikodi", filtriKodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_ekzistonFilter");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// Kthen nese ekziston nje filter tjeter me te njejtin kod per griden tek lupat
        /// </summary>
        /// <param name="filtriKodi">kodi i filtrit</param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="idkoka">id e grides</param>
        /// <returns>True nese ekziston filter tjeter me kete kod; False ne rast te kundert</returns>
        internal bool ekzistonFilterNew(String filtriKodi, int idnder, int idkoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@filtrikodi", filtriKodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@gridkokaid", idkoka, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_FILTRAGRIDA_ekzistonFilterNew"));
            return Convert.ToBoolean(pergjigje);
        }

        #endregion

        #region VITET - PERIUDHAT USHTRIMORE

        /// <summary>
        /// kthen gjithe vitet me ane te nje store procedure
        /// </summary>
        /// <returns>kthen nje datatable me te gjitha vitet</returns>
        internal DataTable ktheGjitheVitetENdermarjes(int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_merrGjitheVitetSipasNdermarjes");

            return ds.Tables[0];

        }


        /// <summary>
        /// Kthen vit sipas nje id te caktuar te vitit
        /// </summary>
        /// <param name="id">id e vitit</param>
        /// <returns>kthen serisht nje dataTable</returns>
        internal DataRow merrVit(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_ktheVit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }


        /// <summary>
        /// kthen vitet sipas ndermarjes dhe rolit.
        /// </summary>
        /// <param name="idNdermarje">idndermarje</param>
        /// <param name="idRoli"> idroli</param>
        /// <returns>kthen datatable</returns>
        internal DataTable ktheVitetTeNdermarjesDheRolit(int idRoli, int idNdermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDROLI", idRoli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ROLDREJTA_MerrViteTeNdermarjeTeRolit");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen vit sipas nje kod viti te caktuar.
        /// </summary>
        /// <param name="kodi">kodi i vitit</param>
        /// <returns>kthen datatable</returns>
        internal DataRow merrVit(string kodi, int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIVITI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_ktheVitSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        internal DataTable ktheGjitheViteELidhuraMeNdermarrje(int idNdermarrje)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NDERMARJEVITI_merrGjitheNdermarrjeVitetSipasNdermarjes");
            return ds.Tables[0];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal DataRow kthePeriudhen(DateTime date, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasDateNderm");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal int ktheIdPeriudheSipasDatesDheNdermarrjes(DateTime date, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            ImbLogger.LogTraceShitje("Kthimi i periudhes me sukses sipas dates: " + Convert.ToString(date) + " dhe idndermarrjes: " + Convert.ToString(idNdermarrje));
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selIdSipasDateNderm"));
        }

        internal bool eshteKycurPeriudheSipasDateDheNdermarrjes(DateTime date, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@DATE", date, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERIUDHAT_ktheKycurSipasDateNderm"));
        }

        internal DataTable kthePeriudhaSipasViti(int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasViti");
            return ds.Tables[0];

        }
        internal DataTable kthePeriudhatSipasVitiDheGjuhes(int idViti, int idgjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHA", idgjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasVitiDheGjuhes");
            return ds.Tables[0];
        }
        internal DataTable kthePeriudhaSipasViti(int idViti, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasVitiMeGjuhe");
            return ds.Tables[0];

        }
        internal DataTable kthePeriudhaSipasVitiEPaySlip(int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasVitiPostoEpaySlip");
            return ds.Tables[0];

        }
        internal DataTable kthePeriudhaSipasVitiEPaySlip(int idViti, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHA", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasVitiPostoEpaySlipSipasGjuhes");
            return ds.Tables[0];

        }
        internal DataRow kthePeriudhaSipasVititDheMuajit(int idViti, int nrPeriudhe, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NUMRIPERIUDHA", nrPeriudhe, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJUHA", gjuha, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasVitiDheMuaj").Tables[0].Rows[0];
        }


        internal DataRow kthePeriudhaSipasId(int idPeriudha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERIUDHA", idPeriudha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow kthePeriudhaSipasId(int idPeriudha, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERIUDHA", idPeriudha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHA", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selSipasIdMeGjuhe");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }



        internal DataTable kthePeriudhaAll()
        {




            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PERIUDHAT_selAll");
            return ds.Tables[0];

        }


        internal clsMesazh ruajVit(out int idviti, String kodiviti, DateTime fillimiviti, DateTime mbarimiviti, String periudhalloji, bool periudhahapjes, bool periudhambylljes, int idperdoruesi, int idKonfig, int idNdermarje, int idstatusdok, DateTime mbyllurme, int idllogmbylljeviti)
        {
            idviti = -1;


            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDVITI", idviti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIVITI", kodiviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILLIMIVITI", fillimiviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBARIMIVITI", mbarimiviti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERIUDHALLOJI", periudhalloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERIUDHAHAPJES", periudhahapjes, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERIUDHAMBYLLJES", periudhambylljes, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);

            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (mbyllurme == new DateTime())
                dbManager.AddParameters(11, "@MBYLLURME", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@MBYLLURME", mbyllurme, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDLLOGMBYLLJEVITI", idllogmbylljeviti, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VITET_ins");

            idviti = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        public string ktheKodVitSipasID(int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", idViti, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VITET_ktheKodSipasID"));
        }
        public int ktheIdVitPerNdermarrjenSipasKodit(int idNdermarje, string kodiviti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIVITI", kodiviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VITET_ktheIDSipasKodit"));
        }
        public clsMesazh ekzistonVit(String kodiviti, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIVITI", kodiviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            int nrRreshtash = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VITET_eksiston_Vit"));
            if (nrRreshtash == 0)
            {
                return new clsMesazh(true);
            }
            else
            {
                return new clsMesazh(false, "Ekziston nje vit me kete kod");
            }

        }


        internal clsMesazh fshiVit(int idviti)
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", idviti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VITET_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiVitStatus(int idviti, int idperdoruesi)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDVITI", idviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VITET_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        public clsMesazh modifikoVit(int idviti, String kodiviti, DateTime fillimiviti, DateTime mbarimiviti, String periudhalloji, bool periudhahapjes, bool periudhambylljes, int idperdoruesi, int idKonfig, int idNdermarje, int idstatusdok, DateTime mbyllurme, int idllogmbylljeviti)
        {


            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDVITI", idviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIVITI", kodiviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILLIMIVITI", fillimiviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MBARIMIVITI", mbarimiviti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERIUDHALLOJI", periudhalloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERIUDHAHAPJES", periudhahapjes, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERIUDHAMBYLLJES", periudhambylljes, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            if (mbyllurme == new DateTime())
                dbManager.AddParameters(11, "@MBYLLURME", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(11, "@MBYLLURME", mbyllurme, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDLLOGMBYLLJEVITI", idllogmbylljeviti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VITET_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        public clsMesazh modifikoKycurPeriudha(int idperiudha, bool ekycur, ResourceManager rm, CultureInfo ci)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERIUDHA", idperiudha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EKYCUR", ekycur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERIUDHAT_upd");
            return new clsMesazh(true, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci));
        }

        public clsMesazh modifikoPeriudha(int idperiudha, bool ekycur, int nrperiudha, DateTime datefillimi, DateTime datembarimi, string emerperiudha, bool epayslip, bool mebonus, bool annualdeclaration)
        {


            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDPERIUDHA", idperiudha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EKYCUR", ekycur, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NUMRIPERIUDHA", nrperiudha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATAFILLIMIT", datefillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATAMBARIMIT", datembarimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERPERIUDHA", emerperiudha, ParameterDirection.Input);
            dbManager.AddParameters(6, "@POSTOEPAYSLIP", epayslip, ParameterDirection.Input);
            dbManager.AddParameters(7, "@POSTOMEMOBONUS", mebonus, ParameterDirection.Input);
            dbManager.AddParameters(8, "@POSTOANNUALDECLARATION", annualdeclaration, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PERIUDHAT_updAll");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        internal clsMesazh shtoPeriudhat(string slqString)
        {



            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, slqString);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        internal clsMesazh fshiPeriudhatVitit(int idviti)
        {
            //fshin te gjitha periudhat ushtrimore te cilat i perkasin vitit ushtrimor te percaktuar

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVITI", idviti, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VITET_fshiPeriudhatVitit");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal DataRow merrViteSipasNdermarjesDR(int idnderm, int idviti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_merrViteSipasNdermarjesDR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }
        internal DataTable merrVitetNdermarjeDT(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VITET_merrVitetNdermarjeDT").Tables[0];
        }
        #endregion

        #region NUMRAT AUTOMATIKE

        internal DataTable ktheGjitheNumratAutomatike(int idndermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_merrGjitheNumratAutomatike");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheNumratAutomatikePerKonfigurim(int idndermarrje, string kodKonfigurimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODKONFIGURIMI", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_merrGjitheNumratAutomatikePerKonfigurim");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheNumratAutomatikeSipasKategorise(int idndermarrje, int idkatdok)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idkatdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_merrGjitheNumratAutomatikeSipasKategorise");
            return ds.Tables[0];

        }
       
        internal DataRow merrNrAutom(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTOM", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_ktheNrAutom");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }
       
        internal DataRow merrNrAutom(string kod, int idndermarje)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KodiNrAutom", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_ktheNrAutomKod");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
       
        internal DataTable merrNrAutomTeNdermarrjes(int idndermarrje)
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            // dbManager.AddParameters(0, "@IDNDERVITI", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_ktheNrAutomTeNdermarrjes");
            return ds.Tables[0];

        }

        internal clsMesazh ruajNrAutom(out int idnrautom, String kodinrautom, String emertiminrautom, Int64 fillonnrautom, Int64 mbaronnrautom, Int64 hapinrautom,
            Int64 drejtiminrautom, DateTime ngadatanrautom, DateTime derimenrautom, String majtasnrautom, String djathtasnrautom,
            int kategrianrautom, int periudhanrautom, int gjatesianrautom, int idndermarja, int vit, int idperdoruesi, int idstatusdok, int LajmeroPerparaNrFundit, Int64 interval)
        {
            idnrautom = -1;


            dbManager.Open();

            dbManager.CreateParameters(20);
            dbManager.AddParameters(0, "@IDNRAUTOM", idnrautom, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODINRAUTOM", kodinrautom, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMINRAUTOM", emertiminrautom, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILLONNRAUTOM", fillonnrautom, ParameterDirection.Input);
            dbManager.AddParameters(4, "@MBARONNRAUTOM", mbaronnrautom, ParameterDirection.Input);
            dbManager.AddParameters(5, "@HAPINRAUTOM", hapinrautom, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DREJTIMINRAUTOM", drejtiminrautom, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NGADATA", ngadatanrautom, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DERIME", derimenrautom, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MAJTASNRAUTOM ", majtasnrautom, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DJATHTASNRAUTOM", djathtasnrautom, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KATEGORIA", kategrianrautom, ParameterDirection.Input);
            dbManager.AddParameters(12, "@PERIUDHANRAUTOM", periudhanrautom, ParameterDirection.Input);
            dbManager.AddParameters(13, "@GJATESIANRAUTOM ", gjatesianrautom, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERMARJE ", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(15, "@VITI ", vit, ParameterDirection.Input);
            //dbManager.AddParameters(16, "@IDNDERVITI", idndermvit, ParameterDirection.Input);
            dbManager.AddParameters(16, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(17, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(18, "@LAJMEROPARANRFUNDIT", LajmeroPerparaNrFundit, ParameterDirection.Input);
            dbManager.AddParameters(19, "@INTERVALI", interval, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOM_ins");
            idnrautom = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh modifikoNrAutom(int idnrautom, String kodinrautom, String emertiminrautom, Int64 fillonnrautom, Int64 mbaronnrautom, Int64 hapinrautom,
            Int64 drejtiminrautom, DateTime ngadatanrautom, DateTime derimenrautom, String majtasnrautom, String djathtasnrautom,
            int kategorianrautom, int periudhanrautom, int gjatesianrautom, int idperdoruesi, int idstatusdok, int LajmeroPerparaNrFundit, Int64 interval)
        {



            dbManager.Open();
            dbManager.CreateParameters(18);
            dbManager.AddParameters(0, "@IDNRAUTOM", idnrautom, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODINRAUTOM", kodinrautom, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMINRAUTOM", emertiminrautom, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILLONNRAUTOM", fillonnrautom, ParameterDirection.Input);
            dbManager.AddParameters(4, "@MBARONNRAUTOM", mbaronnrautom, ParameterDirection.Input);
            dbManager.AddParameters(5, "@HAPINRAUTOM", hapinrautom, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DREJTIMINRAUTOM", drejtiminrautom, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NGADATA", ngadatanrautom, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DERIME", derimenrautom, ParameterDirection.Input);
            dbManager.AddParameters(9, "@MAJTASNRAUTOM ", majtasnrautom, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DJATHTASNRAUTOM", djathtasnrautom, ParameterDirection.Input);
            dbManager.AddParameters(11, "@KATEGORIA", kategorianrautom, ParameterDirection.Input);
            dbManager.AddParameters(12, "@PERIUDHANRAUTOM", periudhanrautom, ParameterDirection.Input);
            dbManager.AddParameters(13, "@GJATESIANRAUTOM ", gjatesianrautom, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(15, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(16, "@LAJMEROPARANRFUNDIT", LajmeroPerparaNrFundit, ParameterDirection.Input);
            dbManager.AddParameters(17, "@INTERVALI", interval, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOM_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiNrAutom(int idnrautom)
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTOM", idnrautom, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOM_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiNrAutomStatus(int idnrautom, int idperdorues)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNRAUTOM", idnrautom, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOM_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        
        public bool ekzistonNrAutomatik(String kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODINRAUTOM", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOM_eksiston");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool eshteILidhurMeAtributeTrupiPerMobile(int idNrAutomatik)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTO", idNrAutomatik, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NRAUTOM_EshteILidhurMeAtributeTrupiPerMobile"));
        }

        public int ktheIntervalSipasId(int idNrAutomatik)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTO", idNrAutomatik, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NRAUTOM_ktheIntervalSipasId"));
        }

        public string ktheKodNrAutoSipasId(int idNrAutomatik)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTO", idNrAutomatik, ParameterDirection.Input);
            return dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NRAUTOM_ktheKodNrAutoSipasId").ToString();
        }




        #endregion

        #region NUMARAT AUTOMATIKE FUNDIT

        /// <summary>
        /// kontrollon nese nje numer automatik ndodhet ne databaze me kodin e kaluar si parameter dhe i numeron rreshtat me kete 
        /// parameter me pas kthen numrin e ketyre rreshtave
        /// </summary>
        /// <param name="kodiNrAutomatik"></param>
        /// <returns></returns>
        public int kaNumraAutoTeFundit(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTO", id, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_kaNumraAutoTeFundit"));
        }

        internal clsMesazh ruajNumraAutomatikeFundit(out int idNrFunditAutomatik, int idNrAutom, DateTime data, string vlera, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            idNrFunditAutomatik = 0;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNRAUTOFUNDIT", idNrFunditAutomatik, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDNRAUTOM", idNrAutom, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_ins");
            idNrFunditAutomatik = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        internal clsMesazh modifikoNumraAutomatikeFundit(int idNrFunditAutomatik, int idNrAutom, DateTime data, string vlera, int idperdoruesi, int idndermarje, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNRAUTOFUNDIT", idNrFunditAutomatik, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNRAUTOM", idNrAutom, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_upd");
            clsMesazh mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
            return mesazh;

        }

        //internal clsMesazh fshiNumraAutomatikeFunditSipasID(int idNrFunditAutomatik)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNRAUTOFUNDIT", idNrFunditAutomatik, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_delSipasID");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}


        internal clsMesazh fshiGjithNrAutoFunditSipasNrAuto(int idNrAutomatik)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTOM", idNrAutomatik, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_delSipasIDNrAutomatik");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiNumraAutomatikeFunditStatus(int idNrFunditAutomatik, int idperdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNRAUTOFUNDIT", idNrFunditAutomatik, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal DataRow merrNumraAutomatikeFunditSipasID(int idNrFunditAutomatik)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTOFUNDIT", idNrFunditAutomatik, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable merrNumraAutomatikeFunditSipasIDNrAuto(int idNrAutomatik)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTOM", idNrAutomatik, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_merrSipasIdNrAutom");
            if (ds == null)
                return null;
            return ds.Tables[0];

        }
        //internal DataTable merrNumraAutomatikeFunditSipasIDNrAuto(int idNrAutomatik)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNRAUTOM", idNrAutomatik, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_merrSipasIdNrAutom");
        //        if (ds == null)
        //            return null;
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        internal DataRow merrNrAutomatikFunditIdDataPerdoruesiNdermarrja(int idNrAutom, DateTime data, int idNdermarrja)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNRAUTOM", idNrAutom, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_merrSipasIdData");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheGjitheNrAutomatikeFundit(int idNrAutomatik)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTOM", idNrAutomatik, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NRAUTOMATIKFUNDIT_merrSipasIdNrAutomatik");
            return ds.Tables[0];

        }

        internal bool eshteILidhurNrAutomatik(int idNrAutomatik)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNRAUTO", idNrAutomatik, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NRAUTOM_EshteILidhur"));
            return Convert.ToBoolean(pergjigje);

        }

        #endregion

        #region  AUDITIMI

        internal clsMesazh ruajAuditim(int idauditim, int idtabele, int idkolone, int idndermarrjevit, int idperdoruesi)
        {


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDAUDIT", idauditim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDTABELE", idtabele, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOLONE", idkolone, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERVITI", idndermarrjevit, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUDITIMI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajAuditim(int idauditim, int idtabele, int idkolone, int idndermarrjevit, int idperdoruesi)", true)]
        //public clsMesazh ruajAuditim(clsAuditim audit)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(5);
        //        dbManager.AddParameters(0, "@IDAUDIT", audit.IdAuditim, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDTABELE", audit.IdTabele, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDKOLONE", audit.IdKolone, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNDERVITI", audit.IdNderViti, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@IDPERDORUESI", audit.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUDITIMI_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheTabelat()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EMERLOGJIKTABELE_sel");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheTabelat()", true)]
        //public colTabelatEmerLogjik merrGjitheTabelat()
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();

        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EMERLOGJIKTABELE_sel");
        //        colTabelatEmerLogjik tabelat = new colTabelatEmerLogjik();
        //        return tabelat.mbushArrayListTabelat(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTabelatEmerLogjik();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheGrupetAuditimit()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMI_sel");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheGrupetAuditimit()", true)]
        //public colGrupeAuditimi merrGjitheGrupetAuditimit()
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();

        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMI_sel");
        //        colGrupeAuditimi grupet = new colGrupeAuditimi();
        //        return grupet.mbushArrayListGrupetAuditimit(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colGrupeAuditimi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable merrKolonat(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTABELE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EMERLOGJIKKOLONE_ktheKolonat");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrKolonat(int id)", true)]
        //public colKolonatEmerLogjik ktheKolonat(int id)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDTABELE", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EMERLOGJIKKOLONE_ktheKolonat");
        //        colKolonatEmerLogjik kolonat = new colKolonatEmerLogjik();
        //        return kolonat.mbushArrayListKolonat(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKolonatEmerLogjik();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        /// <summary>
        /// funksion qe kthen gjith kolonat qe do auditohen per nje tabele te caktuar.
        /// per nje ndermarrje te caktuar, per nje vit te caktuar
        /// </summary>
        /// <param name="kodtabele"></param>
        /// <param name="idNdermarrjeViti"></param>
        /// <returns></returns>
        internal DataTable merrKolonatPerAuditim(string kodtabele, int idNdermarrjeViti)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODTABELE", kodtabele, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NDERMARRJEVITI", idNdermarrjeViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUDITIMI_ktheKolonatPerAuditim");
            return ds.Tables[0];

        }
        ////funksion qe kthen gjith kolonat qe do auditohen per nje tabele te caktuar.
        ////per nje ndermarrje te caktuar, per nje vit te caktuar
        //[Obsolete("Perdor: DataTable ktheKolonatPerAuditim(string kodtabele, int idNdermarrjeViti)", true)]
        //public colAuditime ktheKolonatPerAuditim(string kodtabele, int indendermrrjeviti)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(2);
        //        dbManager.AddParameters(0, "@KODTABELE", kodtabele, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@NDERMARRJEVITI", indendermrrjeviti, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUDITIMI_ktheKolonatPerAuditim");
        //        colAuditime kolonat = new colAuditime();
        //        return kolonat.mbushArrayListAuditime(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colAuditime();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        public bool ekzistonAuditimi(int idTabela, int IdKolona)
        {
            // kontrollohet nese ka auditim per kete kolone te kesaj tabele)

            string sqlstr = "select IDAUDIT from T_AUDITIMI" +
                            " where IDTABELE = " + idTabela + " and IDKOLONE = " + IdKolona;

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTABELE", idTabela, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOLONE", IdKolona, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUDITIMI_ekzistonAuditimi");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;


        }

        internal clsMesazh fshiAuditim(int idTabela, int IdKolona)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTABELE", idTabela, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOLONE", IdKolona, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUDITIMI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        internal DataTable ktheTrupinGrupitAuditimit(int idgrupi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUPAUDIT", idgrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMITRUPI_merrTrupinGrupitAuditimit");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheTrupinGrupitAuditimit(int idgrupi)", true)]
        //public colGrupAuditimiTrupi merrTrupinGrupitAuditimit(clsGrupAuditimi grup)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDGRUPAUDIT", grup.IdGrupi, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMITRUPI_merrTrupinGrupitAuditimit");
        //        colGrupAuditimiTrupi trupi = new colGrupAuditimiTrupi();
        //        return trupi.mbushArrayListGrupetAuditimit(ds);
        //    }

        //    catch (Exception)
        //    {
        //        return new colGrupAuditimiTrupi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region  GRUP AUDITIMI

        //[Obsolete("Perdor nga klasa perkatese: clsMesazh ruajGrupAuditimiDheTrupin(clsGrupAuditimi gr)", true)]
        //public clsMesazh ruajGrupAuditimiDheTrupin(clsGrupAuditimi gr)
        //{
        //    clsMesazh mesazh = new clsMesazh();
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    try
        //    {
        //      int idGA;
        //      mesazh = ruajGrupAuditimi(out idGA, gr.NrGrupi,gr.PershkrimiGrupi, gr.IdPerdoruesi);
        //      if (mesazh.Status)
        //      {
        //          gr.IdGrupi = idGA;
        //          foreach (clsGrupAuditimiTrupi o in gr.OColTrupi)
        //          {
        //              if (mesazh.Status)
        //              {
        //                  o.IdGrupi = gr.IdGrupi;
        //                  mesazh = ruajTrupin(o.IdGrupi, o.IdTabele, o.IdKolone);
        //              }
        //              else
        //              {
        //                  dbManager.Transaction.Rollback();
        //                  return mesazh;
        //              }
        //          }
        //          if (mesazh.Status)
        //          {
        //              dbManager.CommitTransaction();
        //              mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //              return mesazh;
        //          }
        //          else
        //          {
        //              dbManager.Transaction.Rollback();
        //              return mesazh;
        //          }
        //      }
        //      else
        //      {
        //          dbManager.Transaction.Rollback();
        //          return mesazh;
        //      }
        //    }

        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh ruajGrupAuditimi(out int idgrupi, int nrgrupi, String pershkrimigrupi, int idperdoruesi)
        {
            idgrupi = -1;


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDGRUPAUDIT", idgrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRGRUPAUDIT", nrgrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKGRUPAUDIT", pershkrimigrupi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMI_ins");

            idgrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajGrupAuditimi(out int idgrupi, int nrgrupi, String pershkrimigrupi, int idperdoruesi)", true)]
        //public clsMesazh ruajGrupAuditimi(clsGrupAuditimi gr)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(4);
        //        dbManager.AddParameters(0, "@IDGRUPAUDIT", gr.IdGrupi, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@NRGRUPAUDIT", gr.NrGrupi, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKGRUPAUDIT", gr.PershkrimiGrupi, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDPERDORUESI", gr.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMI_ins");

        //        gr.IdGrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        internal clsMesazh ruajTrupin(int idgrupi, int idtabele, int idkolone)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDGRUPAUDIT", idgrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDTABELE", idtabele, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOLONE", idkolone, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMITRUPI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajTrupin(int idgrupi, int idtabele, int idkolone)", true)]
        //public clsMesazh ruajTrupin(clsGrupAuditimiTrupi trupi)
        //{
        //    try
        //    {
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDGRUPAUDIT", trupi.IdGrupi, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDTABELE", trupi.IdTabele, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDKOLONE", trupi.IdKolone, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPAUDITIMITRUPI_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        #endregion

        #region AUTORIZIME

        internal clsMesazh ruajAutorizimKoka(out int idAutorizimKoka, string kodiAutorizim, string pershkrimAutorizim, int idperdoruesi, int idndermarje, int idstatusdok)
        {//ruan ne database nje autorizim koka
            idAutorizimKoka = -1;


            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDAUTORIZIMIKOKA", idAutorizimKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIAUTORIZIMI", kodiAutorizim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMAUTORIZIMI", pershkrimAutorizim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ins");

            idAutorizimKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        internal clsMesazh ruajAutorizimTrupi(int idAutorizimTrupi, int idAutorizimKoka, int idPerdoruesi)
        {//ruan ne database nje autorizim trupi


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDAUTORIZIMITRUPI", idAutorizimTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDAUTORIZIMIKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMITRUPI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiAutorizimSipasPerdoruesPervecTeRinjve(int idperdorues, string idAutorizimeTeRi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAUTORIZIMETERI", idAutorizimeTeRi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMTRUPI_delSipasIdPerdoruesPervecListes");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajAutorizimTrupiNeseNukEkziston(int idAutorizimKoka, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAUTORIZIMIKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMITRUPI_insNeseNukEkziston");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }


        internal clsMesazh modifikoAutorizimKoka(int idAutorizimKoka, string kodiAutorizim, string pershkrimAutorizim, int idperdoruesi, int idndermarje, int idstatusdok)
        {//ben modifikimin e nje autorizim koke


            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDAUTORIZIMKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIAUTORIZIM", kodiAutorizim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMAUTORIZIM", pershkrimAutorizim, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMKOKA_upd");
            idAutorizimKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        internal clsMesazh modifikoAutorizimTrupi(int idAutorizimTrupi, int idAutorizimKoka, int idPerdoruesi)
        {//ben modifikimin e nje autorizim trupi


            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDAUTORIZIMTRUPI", idAutorizimTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAUTORIZIMKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMTRUPI_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        internal clsMesazh fshiAutorizimKoka(int idAutorizimKoka)
        {//ben fshirjen e nje autorizim koke


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTORIZIMKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMKOKA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh fshiAutorizimKokaStatus(int idAutorizimKoka, int idperdorues)
        {//ben fshirjen e nje autorizim koke

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAUTORIZIMKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMKOKA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }


        internal clsMesazh fshiAutorizimTrupi(int idAutorizimTrupi)
        {//ben fshirjen e nje autorizim trupi


            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTORIZIMTrupi", idAutorizimTrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMTRUPI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiAutorizimTrupi(int idAutorizimTrupi)", true)]
        //public clsMesazh fshiAutorizimTrupi(clsAutorizimTrupi autorizimTrupi)
        //{//ben fshirjen e nje autorizim trupi
        //    try
        //    {

        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDAUTORIZIMTrupi", autorizimTrupi.IdAutorizimTrupi, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMTRUPI_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        internal clsMesazh fshiAutorizimSipasPerdorues(int idperdorues)
        {//ben fshirjen e nje autorizim trupi


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMTRUPI_delSipasIdPerdorues");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiAutorizimSipasPerdorues(int idperdorues)", true)]
        //public clsMesazh fshiAutorizimSipasPerdorues(clsPerdorues oPerdorues)
        //{//ben fshirjen e nje autorizim trupi
        //    try
        //    {

        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDPERDORUES", oPerdorues.IdPerdorues, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMTRUPI_delSipasIdPerdorues");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }

        //}

        //internal void merrAutorizimKoka(int idAutorizimKoka)
        //{//merr nje autorizim koke 
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@idautorizimekoka", idAutorizimKoka, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizim");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //[Obsolete("Perdor: void merrAutorizimKoka(int idAutorizimKoka)", true)]
        //public void merrAutorizimKoka(clsAutorizimKoka autorizimKoka)
        //{//merr nje autorizim koke 
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@idautorizimekoka", autorizimKoka.IdAutorizimKoka, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizim");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheAutorizimKokaPerPerdorues(int idperdoruesi)
        {//merr nje autorizim koke 


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_merrAutorizimetPerPerdorues");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheAutorizimKokaPerPerdorues(int idperdoruesi)", true)]
        //public colAutorizimetKoka  merrAutorizimKokaPerPerdorues(int idperdoruesi)
        //{//merr nje autorizim koke 
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDPERDORUES", idperdoruesi , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_merrAutorizimetPerPerdorues");
        //        colAutorizimetKoka autorizimet = new colAutorizimetKoka();
        //        return autorizimet.mbushArrayListAutorizimKoka(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colAutorizimetKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        //[Obsolete("Perdor nga klasa perkatese: clsMesazh fshiAutorizimKokaAndTrupi(clsAutorizimKoka autorizimKoka)", true)]
        //public clsMesazh fshiAutorizimKokaAndTrupi(clsAutorizimKoka autorizimKoka)
        //{//transaksioni per te fshire nje autorizim koke dhe trupat
        //    colAutorizimetTrupi trupi = new colAutorizimetTrupi(autorizimKoka.IdAutorizimKoka);
        //    //colAutorizimetTrupi trupi = merrAutorizimTrupiNgaIdAutorizimKoka(autorizimKoka.IdAutorizimKoka);
        //    clsMesazh mesazh = new clsMesazh(true);
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    dbManager.Open();
        //    dbManager.BeginTransaction();
        //    try
        //    {

        //        foreach (clsAutorizimTrupi o in trupi)
        //        {
        //            if (mesazh.Status)
        //                mesazh = fshiAutorizimTrupi(o.IdAutorizimTrupi);
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        if (mesazh.Status)
        //        {
        //            mesazh = fshiAutorizimKoka(autorizimKoka.IdAutorizimKoka);
        //            if (mesazh.Status)
        //            {
        //                dbManager.CommitTransaction();
        //                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //                return mesazh;
        //            }
        //            else
        //            {
        //                dbManager.Transaction.Rollback();
        //                return mesazh;
        //            }
        //        }
        //        else
        //        {
        //            dbManager.Transaction.Rollback();
        //            return mesazh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        dbManager.Transaction.Rollback();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrAutorizim(int id)
        {//kthen nje autorizim koke sipas id


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idautorizimekoka", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizim");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrAutorizimDR(int id)
        {//kthen nje autorizim koke sipas id


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idautorizimekoka", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizimDR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal string merrKodAutorizim(int id)
        {//kthen nje autorizim koke sipas id


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idautorizimekoka", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizim");
            if (ds == null)
                return "";
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return "";
            string kodAutorizimi;
            kodAutorizimi = ds.Tables[0].Rows[0]["KODAUTORIZIME"].ToString();
            return kodAutorizimi;

        }
        //[Obsolete("Perdor: DataRow merrAutorizim(int id) ose string merrKodAutorizim(int id)", true)]
        //public colAutorizimetKoka ktheAutorizim(int id)
        //{//kthen nje autorizim koke sipas id
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@idautorizimekoka", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizim");
        //        colAutorizimetKoka autorizimet = new colAutorizimetKoka();
        //        return autorizimet.mbushArrayListAutorizimKoka(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colAutorizimetKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrAutorizim(string kod)
        {//kthen nje autorizim koke sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KodAutorizime", kod, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizimKod");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int merrIDAutorizim(string kod)
        {//kthen nje autorizim koke sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KodAutorizime", kod, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizimKod");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idAutorizim;
            int.TryParse(ds.Tables[0].Rows[0]["IDAUTORIZIMEKOKA"].ToString(), out idAutorizim);
            return idAutorizim;
        }

        internal bool kaAutorizimSipasKodDhePerdoruesi(string kod, int idPerdorues)
        {//kthen nje autorizim koke sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODAUTORIZIME", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            bool pergjigje = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ekzistonAutorizimSipasPerdoruesit"));
            return pergjigje;
        }

        //[Obsolete("Perdor: int merrIDAutorizim(string kod) ose DataRow merrAutorizim(string kod)", true)]
        //public colAutorizimetKoka ktheAutorizim(string kod)
        //{//kthen nje autorizim koke sipas kodit
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KodAutorizime", kod, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizimKod");
        //        colAutorizimetKoka autorizimet = new colAutorizimetKoka();
        //        return autorizimet.mbushArrayListAutorizimKoka(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colAutorizimetKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheAutorizimet(int idndermarje, int idperdoruesi)
        {//merr gjithe autorizim kokat ekzistuese


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_merrGjitheAutorizimet");
            return ds.Tables[0];

        }
        
        internal DataTable ktheGjitheAutorizimetDT(int idndermarje, int idperdorues)
        {//merr gjithe autorizim kokat ekzistuese


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_merrGjitheAutorizimetDT");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheAutorizimet()", true)]
        //public colAutorizimetKoka merrGjitheAutorizimet()
        //{//merr gjithe autorizim kokat ekzistuese
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_merrGjitheAutorizimet");
        //        colAutorizimetKoka autorizimet = new colAutorizimetKoka();
        //        return autorizimet.mbushArrayListAutorizimKoka(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colAutorizimetKoka();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheAutorizimTrupiNgaIdAutorizimKoka(int id)
        {//merr gjithe trupat sipas id se kokes


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idautorizimkoka", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMITRUPI_merrAutorizimTrupiNgaIdAutorizimKoka");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheAutorizimTrupiNgaIdAutorizimKoka(int id)", true)]
        //public colAutorizimetTrupi merrAutorizimTrupiNgaIdAutorizimKoka(int id)
        //{//merr gjithe trupat sipas id se kokes
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@idautorizimkoka", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMITRUPI_merrAutorizimTrupiNgaIdAutorizimKoka");
        //        colAutorizimetTrupi autorizimet = new colAutorizimetTrupi();
        //        return autorizimet.mbushArrayListAutorizimTrupi(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colAutorizimetTrupi();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        public bool ekzistonAutorizim(String kodi)
        {//kontrollon nese ekziston nje autorizim me kete kod

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODAUTORIZIME", kodi, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ekzistonAutorizim");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        public bool kaVeprimeAutorizim(int idautorizimi)
        {//kontrollon nese ekziston nje autorizim me kete kod


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDAUTORIZIMEKOKA", idautorizimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        #endregion

        #region LLOJIKODE

        internal DataTable ktheGjithellojKodi()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJKODI_sel");
            return ds.Tables[0];

        }


        internal int merrIDLlojKodi(String kodi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@LLOJKODIPERSHKRIMI", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJKODI_ktheLlojKodi");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idLlojKodi;
            int.TryParse(ds.Tables[0].Rows[0]["IDLLOJKODI"].ToString(), out idLlojKodi);
            return idLlojKodi;


        }


        internal DataTable ktheGjithellojKodiPozitive()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJKODI_sel_pozitive");
            return ds.Tables[0];

        }


        #endregion

        #region LLOJE MODELESH FUSHA SHTESE

        internal clsMesazh ruajLlojModeliFushaShtese(int idllojmodeliFushaShtese, String pershkrimillojmodeliFushaShtese)
        { //metoda per ruajtjen e LLOJIT TE Modelit FushaShtese 


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOJMODELIFUSHASHTESE", idllojmodeliFushaShtese, ParameterDirection.Output);
            dbManager.AddParameters(1, "@PERSHKRIMILLOJMODELIFUSHASHTESE", pershkrimillojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        internal clsMesazh modifikoLlojModeliFushaShtese(int idllojmodeliFushaShtese, String pershkrimillojmodeliFushaShtese)
        {//metoda per modifikimin e llojit te Modelit FushaShtese 


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOJMODELIFUSHASHTESE", idllojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PERSHKRIMILLOJMODELIFUSHASHTESE", pershkrimillojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }


        internal clsMesazh fshiLlojModeliFushaShtese(int idllojmodeliFushaShtese)
        {//metoda per fshirjen e llojit te Modelit FushaShtese 


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJMODELIFUSHASHTESE", idllojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        internal void merrLlojModeliFushaShtese(int idllojmodeliFushaShtese)
        {// metoda per te marre nje LLOJ Modeli  FushaShtese  NE BAZE TE ID


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJMODELIFUSHASHTESE", idllojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_sel");

        }


        internal DataTable ktheGjitheLlojModeleshFushaShtese()
        {//metoda per te marre te gjithe llojet e Modeleve FushaShtese 


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_merrGjitheLlojModeleshFushaShtese");
            return ds.Tables[0];

        }


        internal DataTable ktheGjitheLlojModeleshFushaShtesePozitive()
        {//metoda per te marre te gjithe llojet e Modeleve FushaShtese 


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_merrGjitheLlojModeleshFushaShtesePozitive");
            return ds.Tables[0];

        }


        internal DataRow ktheLlojModeliFushaShteseSipasKodit(String kodi)
        {//metoda per te marre te llojin e Modelit FushaShtese  sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMILLOJMODELIFUSHASHTESE", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_merrLlojModeliFushaShteseSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        #endregion

        #region  MODELI FUSHA SHTESE

        internal clsMesazh ruajModeliFushaShtese(out int idmodeliFushaShtese, string kodimodeliFushaShtese, String pershkrimmodeliFushaShtese, int idllojmodeliFushaShtese, int idperdoruesi, int idndermarje, int idstatusdok)
        { //metoda per ruajtjen e  Modelit
            idmodeliFushaShtese = -1;


            //shtimi i parametrave
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", idmodeliFushaShtese, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIMODELIFUSHASHTESE", kodimodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMMODELIFUSHASHTESE", pershkrimmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJMODELIFUSHASHTESE", idllojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_ins");
            idmodeliFushaShtese = int.Parse(dbManager.Parameters[0].Value.ToString());

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        internal clsMesazh modifikoModeliFushaShtese(int idmodeliFushaShtese, string kodimodeliFushaShtese, String pershkrimmodeliFushaShtese, int idllojmodeliFushaShtese, int idperdoruesi, int idndermarje, int idstatusdok)
        {//metoda per modifikimin e  ModelitFushaShtese 


            //shtimi i parametrave
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", idmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODIMODELIFUSHASHTESE", kodimodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMMODELIFUSHASHTESE", pershkrimmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLLOJMODELIFUSHASHTESE", idllojmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_upd");
            int.TryParse(dbManager.Parameters[0].Value.ToString(), out idmodeliFushaShtese);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        internal clsMesazh fshiModeliFushaShtese(int idmodeliFushaShtese)
        {//metoda per fshirjen e  ModelitFushaShtese 


            //shtimi i parametrave
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", idmodeliFushaShtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }
        internal clsMesazh fshiModeliFushaShteseStatus(int idmodeliFushaShtese, int idperdoruesi)
        {//metoda per fshirjen e  ModelitFushaShtese 


            //shtimi i parametrave
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", idmodeliFushaShtese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }

        internal void merrModeliFushaShtese(int idmodeliFushaShtese)
        {// metoda per te marre nje  Model NE BAZE TE ID


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", idmodeliFushaShtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_ktheModeletFushaShtese");

        }
        internal DataTable ktheGjitheModeletFushaShtese(int idndermarje, int idperdorues)
        {//metoda per te marre te gjithe  ModeletFushaShtese 


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_merrGjitheModeletFushaShtese");
            return ds.Tables[0];

        }
        internal DataTable ktheGjitheModeletFushaShteseDT(int idndermarje, int idperdorues)
        {//metoda per te marre te gjithe  ModeletFushaShtese 


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_merrGjitheModeletFushaShteseDT");
            return ds.Tables[0];

        }

        internal DataTable ktheGjitheModeletFushaShteseSipasLlojit(int idlloj, int idndermarje, int idperdorues)
        {//metoda per te marre te gjithe  ModeletFushaShtese sipas llojit


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idllojmodelifushashtese", idlloj, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_merrGjitheModeletFushaShteseSipasLlojit");
            return ds.Tables[0];

        }
        internal DataTable ktheGjitheModeletFushaShteseSipasLlojit(string lloj, int idndermarje, int idperdorues)
        {//metoda per te marre te gjithe  ModeletFushaShtese sipas llojit


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@llojmodelifushashtese", lloj, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_merrModeletFushaShteseSipasLlojit");
            return ds.Tables[0];

        }
        internal DataRow ktheModelinFushaShteseSipasKodit(String kodi, int idndermarje)
        {//metoda per te marre Modelin sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIMODELIFUSHASHTESE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJMODELIFUSHASHTESE_merrModelinFushaShteseSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        public bool ekzistonModelFushaShtese(String kodi, int idndermarje)
        {//kontrollon nese ekziston nje model me kete kod


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIMODELIFUSHASHTESE", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_ekzistonModelFushaShtese");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

            //beri edi koment, sepse ky eshe nje funksion qe thirret brenda nje funksioni tjeter
            //i cili e hap dhe e mbyll connectionin.
            ////finally
            ////{
            ////    dbManager.Dispose();
            ////}
        }

        internal DataRow merrModeletFushaShtese(int id)
        {//kthen nje model koke sipas id


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_ktheModeletFushaShtese");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrModeletFushaShteseDR(int id)
        {//kthen nje model koke sipas id


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_ktheModeletFushaShteseDR");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        public bool kaVeprimeModelFushaShtese(int id)
        {//kontrollon nese ekziston nje model me kete kod



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODELIFUSHASHTESE", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MODELIFUSHASHTESE_kaVeprimeModelFushaShtese");
            if (ds.Tables[0].Rows.Count >= 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

            //beri edi koment, sepse ky eshe nje funksion qe thirret brenda nje funksioni tjeter
            //i cili e hap dhe e mbyll connectionin.
        }


        #endregion

        #region  FUSHAT SHTESE

        internal clsMesazh ruajFushaShteseDt(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@VLERAT_NDRYSHUAR", dt);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_VLERAFUSHASHTESE_MERGEDT");
            return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
        }

        internal clsMesazh ruajFushaShtese(out int idfushashtese, int idmodelifushashtese, String pershkrimifushashtese, int tipifushashtese, int gjatesiafushashtese, int atitipifushashtese, bool shfaq, bool detyrueshme, bool lejueshme, string vlereDefault, string kodi, string pershkrimiEng, string shenime)
        {
            idfushashtese = -1;
            //shtimi i parametrave
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDFUSHASHTESE", idfushashtese, ParameterDirection.Output);
            dbManager.AddInputParameters("@IDMODELIFUSHASHTESE", idmodelifushashtese);
            dbManager.AddInputParameters("@PERSHKRIMIFUSHASHTESE", pershkrimifushashtese);
            dbManager.AddInputParameters("@TIPIFUSHASHTESE", tipifushashtese);
            dbManager.AddInputParameters("@GJATESIAFUSHASHTESE", gjatesiafushashtese);
            if (atitipifushashtese == 0) dbManager.AddInputParameters("@ATITIPIFUSHASHTESE", DBNull.Value);
            else dbManager.AddInputParameters("@ATITIPIFUSHASHTESE", atitipifushashtese);

            dbManager.AddInputParameters("@SHFAQ", shfaq);
            dbManager.AddInputParameters("@DETYRUESHME", detyrueshme);
            dbManager.AddInputParameters("@LEJUESHME", lejueshme);
            dbManager.AddInputParameters("@VLEREDEFAULT", vlereDefault);
            dbManager.AddInputParameters("@KODI", kodi);
            dbManager.AddInputParameters("@PERSHKRIMIENG", pershkrimiEng);
            dbManager.AddInputParameters("@SHENIME", shenime);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FUSHASHTESE_ins");
            idfushashtese = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);

            return mesazh;

        }
        internal clsMesazh modifikoFushaShtese(int idfushashtese, int idmodelifushashtese, String pershkrimifushashtese, int tipifushashtese, int gjatesiafushashtese, int atitipifushashtese, bool shfaq, bool detyrueshme, bool lejueshme, string vlereDefault, string kodi, string pershkrimiEng, string shenime)
        {
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDFUSHASHTESE", idfushashtese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODELIFUSHASHTESE", idmodelifushashtese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMIFUSHASHTESE", pershkrimifushashtese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TIPIFUSHASHTESE", tipifushashtese, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GJATESIAFUSHASHTESE", gjatesiafushashtese, ParameterDirection.Input);
            if (atitipifushashtese == 0) dbManager.AddParameters(5, "@ATITIPIFUSHASHTESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@ATITIPIFUSHASHTESE", atitipifushashtese, ParameterDirection.Input);
            dbManager.AddInputParameters("@SHFAQ", shfaq);
            dbManager.AddInputParameters("@DETYRUESHME", detyrueshme);
            dbManager.AddInputParameters("@LEJUESHME", lejueshme);
            if (string.IsNullOrEmpty(vlereDefault)) dbManager.AddInputParameters("@VLEREDEFAULT", DBNull.Value);
            else dbManager.AddInputParameters("@VLEREDEFAULT", vlereDefault);
            if (string.IsNullOrEmpty(kodi)) dbManager.AddInputParameters("@KODI", DBNull.Value);
            else dbManager.AddInputParameters("@KODI", kodi);
            if (string.IsNullOrEmpty(pershkrimiEng)) dbManager.AddInputParameters("@PERSHKRIMIENG", DBNull.Value);
            else dbManager.AddInputParameters("@PERSHKRIMIENG", pershkrimiEng);
            dbManager.AddInputParameters("@SHENIME", shenime);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FUSHASHTESE_upd");
            idfushashtese = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);


        }
        internal clsMesazh fshiFushaShtese(int idfushashtese)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFUSHASHTESE", idfushashtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FUSHASHTESE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal void merrFushaShtese(int idfushashtese)
        {

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFUSHASHTESE", idfushashtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FUSHASHTESE_sel");

        }


        internal DataTable ktheGjitheFushatShtese()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FUSHASHTESE_merrGjitheFushatShtese");
            return ds.Tables[0];

        }


        internal void ktheFushatShteseSipasKodit(String kodi, IDataBaseReader fushaShtese)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@Pershkrimifushashtese", kodi, ParameterDirection.Input);
            dbManager.FillObject("prc_T_FUSHASHTESE_merrFushatShteseSipasKodit", fushaShtese);
        }

        internal void ktheFushatShteseSipasAtit(int ati, IDataBaseReader colFushat)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@atitipifushashtese", ati, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_FUSHASHTESE_merrFushatShteseSipasAtit", colFushat);
        }


        internal void ktheFushatShteseSipasModelit(int idmodeli, IDataBaseReader col)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idmodeliFushaShtese", idmodeli, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_FUSHASHTESE_merrFushatShteseSipasModelit", col);

        }

        #endregion

        #region LLOJE PERIUDHASH

        internal DataTable ktheGjitheLlojPeriudhash()
        {

            string sqlstr = "prc_T_LLOJPERIUDHE_sel";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, sqlstr);
            return ds.Tables[0];

        }


        internal DataTable ktheGjitheLlojPeriudhashSipasSuperKategorise(int idSuperKat)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSUPERKAT", idSuperKat, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJPERIUDHE_selSipasSuperKategorise");
            return ds.Tables[0];

        }
        internal DataTable ktheGjitheLlojPeriudhashSipasIdKatNrAuto(int idkatnrauto)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATNRAuto", idkatnrauto, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJPERIUDHE_selSipasIdKategoriNrAuto");
            return ds.Tables[0];

        }

        internal DataRow ktheLlojPeriudheSipasId(int idLlojPeriudhe)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJPERIUDHE", idLlojPeriudhe, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJPERIUDHE_selSipasID");
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheGjitheLlojPeriudhashPozitive()
        {

            string sqlstr = "prc_T_LLOJPERIUDHE_sel_pozitive";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, sqlstr);
            return ds.Tables[0];

        }


        #endregion

        #region KOMPONENTET
        internal int MerrIdKomponenteSipasEmrit(string komponenteEmri)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KOMPONENTEEMRI", komponenteEmri, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOMPONENTE_merrKomponenteIdSipasEmrit"));
        }
        internal DataRow merrKomponenteSipasEmrit(string emerKomponente)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@KOMPONEMRI", emerKomponente);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTE_merrKomponenteSipasKodit");
            if (ds.Tables[0] == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrKomponenteSipasId(int idkomp)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPON", idkomp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTE_sel");
            if (ds.Tables[0] == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal string merrKomponenteDefaultPerdoruesi(int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            string pergjigje = Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOMPONENTE_merrKomponenteDefaultPerdoruesi"));
            if (pergjigje != "")
                if (pergjigje == "CRMDefault.aspx" || pergjigje == "GISDefault.aspx")
                    return pergjigje;
                else
                    return pergjigje + "&shtim_modifikim=shtim";
            return pergjigje;
        }

        internal string merrKomponenteDefaultPerdoruesiSipasLlojit(int idperdoruesi, bool ambjentMobile)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@AMBJENTMOBILE", ambjentMobile, ParameterDirection.Input);
            string pergjigje = Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOMPONENTE_merrKomponenteDefaultPerdoruesiSipasLlojit"));
            return pergjigje;
        }

        /// <summary>
        /// Kthen listen e konfigurimeve per nje ambjent te caktuar per te cilat ka autorizime perdoruesi.
        /// </summary>
        /// <param name="idKomponente">id e komponentes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        /// <param name="idKatDok">id e kategorise se nivelit te dokumentit</param>
        /// <returns>Kthen nje DataTable me listen e konfigurimeve qe ka autorizime perdoruesi</returns>
        internal DataTable ktheKonfigurimetMeAutorizimPerdoruesiSipasKomponentes(int idKomponente, int idPerdorues, int idNdermarrje, int idKatDok)
        {


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATDOK", idKatDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTE_merrTeDrejtaPerdPerKonfigurimKomponenteje");
            if (ds.Tables[0] == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0];

        }

        //[Obsolete("Perdor: DataRow merrKomponenteSipasEmrit(string emerKomponente)", true)]
        //public colKomponentet merrKomponenteSipasKodit(clsKomponente oKomponente)
        //{
        //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@KOMPONEMRI", oKomponente.EmriKomponente, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTE_merrKomponenteSipasKodit");
        //        colKomponentet colKomponentet = new colKomponentet();
        //        return colKomponentet.mbushArrayListKomponentet(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKomponentet();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region RAPORTET

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAdapter"></param>
        /// <param name="ds"></param>
        /// <param name="prc_name"></param>
        /// <param name="ekzekuto2here"> perdoret per te ekzekutuar query 2 here ne vend qe te beje union te dy query sepse unioni e ngadaleson me shume sesa kur ekzekutohen vecan</param>
        /// <param name="param_array"></param>
        /// <returns></returns>
        public clsMesazh GetReportDataAdapter(out SqlDataAdapter dataAdapter, out DataSet ds, string prc_name, params SqlParameter[] param_array)
        {
            dataAdapter = new SqlDataAdapter();
            ds = new DataSet();
            try
            {
                SqlCommand SqlCmd = new SqlCommand
                {
                    Transaction = (SqlTransaction)dbManager.Transaction,
                    Connection = (SqlConnection)dbManager.Connection,
                    CommandText = prc_name,
                    CommandType = CommandType.StoredProcedure
                };

                SqlCmd.Parameters.Clear();
                foreach (SqlParameter param in param_array)
                {
                    SqlCmd.Parameters.Add(param);
                }


                dataAdapter.SelectCommand = SqlCmd;
                dataAdapter.SelectCommand.CommandTimeout = 900;
                dataAdapter.Fill(ds);
                SqlCmd.Parameters.Clear();
                return new DbCore.clsMesazh(true);

            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 1205 || sqlEx.Number == 121 || sqlEx.Number == 1236)
                    return new DbCore.clsMesazh(Convert.ToInt32(IsolationLevel.Snapshot), false, "");
                return new DbCore.clsMesazh(false);
            }
            catch (Exception ex)
            {
                return new DbCore.clsMesazh(false, ex.Message);
            }

        }


        #endregion

        #region LISTE AMBJENTESH CELJE REGJISTRIM

        internal clsMesazh ruajListeAmbjenteCeljeRegjistrim(int idCR, string kodiCR, string pershkrimCR)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDCR", idCR, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODICR", kodiCR, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMICR", pershkrimCR, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajListeAmbjenteCeljeRegjistrim(int idCR, string kodiCR, string pershkrimCR)", true)]
        //public clsMesazh ruajListeAmbjenteCeljeRegjistrim(clsListeAmbjenteCeljeRegjistrim listeAmbjenteCeljeRegjistrim)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDCR", listeAmbjenteCeljeRegjistrim.IdCR, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@KODICR", listeAmbjenteCeljeRegjistrim.KodiCR, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@PERSHKRIMICR", listeAmbjenteCeljeRegjistrim.PershkrimCR, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoListeAmbjenteCeljeRegjistrim(int idCR, string kodiCR, string pershkrimCR)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDCR", idCR, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODICR", kodiCR, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMICR", pershkrimCR, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
       
        internal clsMesazh fshiListeAmbjenteCeljeRegjistrim(int idCR)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDCR", idCR, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
       
        internal void merrListeAmbjenteCeljeRegjistrim(int idCR)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDCR", idCR, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_ktheListeAmbjentiCeljeRegjistrim");

        }
        

        internal DataRow merrListeAmbjentiCeljeRegjistrim(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDCR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_ktheListeAmbjentiCeljeRegjistrim");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        

        internal int merrIDListeAmbjentiCeljeRegjistrim(string kod)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODICR", kod, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_ktheListeAmbjentiCeljeRegjistrimSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idCR;
            int.TryParse(ds.Tables[0].Rows[0]["IDCR"].ToString(), out idCR);
            return idCR;

        }
     

        internal DataTable ktheGjitheListeAmbjenteshCeljeRegjistrim()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LISTEAMBJENTECELJEREGJSTRIMI_merrGjitheListeAmbjenteshCeljeRegjitstrim");
            return ds.Tables[0];

        }
       
        #endregion

        #region ACR NUMRA AUTOMATIKE

        internal clsMesazh ruajACRNumraAutomatike(int idLidhjeNrAuto, int idLidhjeCR, int idLlojiLidhje, int idNumraAutoLidhje, string vleraFunditLidhje, int idperdoruesi, int idndermarje, int idstatusdok)
        {


            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDLIDHJENRAUTO", idLidhjeNrAuto, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDLIDHJECR", idLidhjeCR, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJILIDHJE", idLlojiLidhje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNUMRAAUTOLIDHJE", idNumraAutoLidhje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAFUNDITLIDHJE", vleraFunditLidhje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh ruajACRNumraAutomatike(int idLidhjeNrAuto, int idLidhjeCR, int idLlojiLidhje, int idNumraAutoLidhje, string vleraFunditLidhje, int idperdoruesi, int idndermarje)", true)]
        //public clsMesazh ruajACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(7);
        //        dbManager.AddParameters(0, "@IDLIDHJENRAUTO", ACRNumraAutomatike.IdLidhjeNrAuto, ParameterDirection.Output);
        //        dbManager.AddParameters(1, "@IDLIDHJECR", ACRNumraAutomatike.IdLidhjeCR, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDLLOJILIDHJE", ACRNumraAutomatike.IdLlojiLidhje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNUMRAAUTOLIDHJE", ACRNumraAutomatike.IdNumraAutoLidhje, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@VLERAFUNDITLIDHJE", ACRNumraAutomatike.VleraFunditLidhje, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", ACRNumraAutomatike.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERMARJE", ACRNumraAutomatike.IdNdermarje , ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_ins");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        //        return mesazh;


        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh modifikoACRNumraAutomatike(int idLidhjeNrAuto, int idLidhjeCR, int idLlojiLidhje, int idNumraAutoLidhje, string vleraFunditLidhje, int idperdoruesi, int idndermarje, int idstatusdok)
        {


            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDLIDHJENRAUTO", idLidhjeNrAuto, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLIDHJECR", idLidhjeCR, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJILIDHJE", idLlojiLidhje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNUMRAAUTOLIDHJE", idNumraAutoLidhje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAFUNDITLIDHJE", vleraFunditLidhje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh modifikoACRNumraAutomatike(int idLidhjeNrAuto, int idLidhjeCR, int idLlojiLidhje, int idNumraAutoLidhje, string vleraFunditLidhje, int idperdoruesi, int idndermarje)", true)]
        //public clsMesazh modifikoACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(7);
        //        dbManager.AddParameters(0, "@IDLIDHJENRAUTO", ACRNumraAutomatike.IdLidhjeNrAuto, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDLIDHJECR", ACRNumraAutomatike.IdLidhjeCR, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDLLOJILIDHJE", ACRNumraAutomatike.IdLlojiLidhje, ParameterDirection.Input);
        //        dbManager.AddParameters(3, "@IDNUMRAAUTOLIDHJE", ACRNumraAutomatike.IdNumraAutoLidhje, ParameterDirection.Input);
        //        dbManager.AddParameters(4, "@VLERAFUNDITLIDHJE", ACRNumraAutomatike.VleraFunditLidhje, ParameterDirection.Input);
        //        dbManager.AddParameters(5, "@IDPERDORUESI", ACRNumraAutomatike.IdPerdoruesi, ParameterDirection.Input);
        //        dbManager.AddParameters(6, "@IDNDERMARJE", ACRNumraAutomatike.IdNdermarje, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_upd");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        //        return mesazh;
        //    }

        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal clsMesazh fshiACRNumraAutomatike(int idLidhjeNrAuto)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLIDHJENRAUTO", idLidhjeNrAuto, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiACRNumraAutomatikeStatus(int idLidhjeNrAuto, int idperdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLIDHJENRAUTO", idLidhjeNrAuto, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        //[Obsolete("Perdor: clsMesazh fshiACRNumraAutomatike(int idLidhjeNrAuto)", true)]
        //public clsMesazh fshiACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());

        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLIDHJENRAUTO", ACRNumraAutomatike.IdLidhjeNrAuto, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_del");
        //        clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        return new clsMesazh(false, ce.Message);
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        //internal void merrACRNumraAutomatikeVoid(int idLidhjeNrAuto)
        //{
        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLIDHJENRAUTO", idLidhjeNrAuto, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_ktheACRNumraAutomatike");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //[Obsolete("Perdor: void merrACRNumraAutomatike(int idLidhjeNrAuto)", true)]
        //public void merrACRNumraAutomatike(clsACRNumraAutomatike ACRNumraAutomatike)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLIDHJENRAUTO", ACRNumraAutomatike.IdLidhjeNrAuto, ParameterDirection.Input);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_ktheACRNumraAutomatike");
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrACRNumraAutomatike(int id)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLIDHJENRAUTO", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_ktheACRNumraAutomatike");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrACRNumraAutomatike(int id)", true)]
        //public colACRNumratAutomatike ktheACRNumraAutomatike(int id)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLIDHJENRAUTO", id, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_ktheACRNumraAutomatike");
        //        colACRNumratAutomatike colACRNumratAutomatike = new colACRNumratAutomatike();
        //        return colACRNumratAutomatike.mbushArrayListeACRNumraAutomatike(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colACRNumratAutomatike();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheACRNumraAutomatike(int idndermarje)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_merrGjitheACRNumraAutomatike");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheACRNumraAutomatike(int idndermarje)", true)]
        //public colACRNumratAutomatike merrGjitheACRNumraAutomatike(int idndermarje)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje , ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_merrGjitheACRNumraAutomatike");
        //        colACRNumratAutomatike colACRNumratAutomatike = new colACRNumratAutomatike();
        //        return colACRNumratAutomatike.mbushArrayListeACRNumraAutomatike(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colACRNumratAutomatike();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        public bool ekzistonACRNumraAutomatike(int IDLIDHJECR, int IDLLOJILIDHJE, int idndermarje)
        {//kontrollon nese ekziston nje ACRNumra automatik per kete ambjent dhe kete lloj lidhje


            dbManager.Open(); dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDLIDHJECR", IDLIDHJECR, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJILIDHJE", IDLLOJILIDHJE, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_existon");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        public bool ekzistonACRNumraAutomatikeModifiko(int IDLIDHJENRAUTO, int IDLIDHJECR, int IDLLOJILIDHJE, int idndermarje)
        {//kontrollon nese ekziston nje ACRNumra automatik per kete ambjent dhe kete lloj lidhje


            dbManager.Open(); dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDLIDHJENRAUTO", IDLIDHJENRAUTO, ParameterDirection.Input);

            dbManager.AddParameters(1, "@IDLIDHJECR", IDLIDHJECR, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJILIDHJE", IDLLOJILIDHJE, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_existon_modifiko");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        internal DataRow ktheNrAutomatikPerKodin(int idcr, int idlloj, int idndermarje)
        {

            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDLIDHJECR", idcr, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJILIDHJE", idlloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_merrVlerenFundit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow ktheNrAutomatikPerKodin(int idcr, int idlloj, int idndermarje)", true)]
        //public colACRNumratAutomatike  merrNrAutomatikPerKodin(int idcr, int idlloj, int idndermarje)
        //{

        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDLIDHJECR", idcr, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@IDLLOJILIDHJE", idlloj, ParameterDirection.Input);
        //        dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ACRNUMRAAUTOMATIKE_merrVlerenFundit");
        //        DbCore.DbAdmin.colACRNumratAutomatike ACR = new DbCore.DbAdmin.colACRNumratAutomatike();
        //        return ACR.mbushArrayListeACRNumraAutomatike(ds);


        //    }
        //    catch (Exception)
        //    {
        //        return new colACRNumratAutomatike(); ;
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region VLERAT FUSHAT SHTESE

        public clsMesazh ruajVlera(int idvlerafushashtese, int idlidhese, int idfushashtese, String vlerafushashtese, int idmodelifushashtese)
        {//ruajtja e vlerave te fushave shtese


            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDVLERAFUSHASHTESE", idvlerafushashtese, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDLIDHESE", idlidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDFUSHASHTESE", idfushashtese, ParameterDirection.Input);
            if (vlerafushashtese == null) dbManager.AddParameters(3, "@VLERAFUSHASHTESE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@VLERAFUSHASHTESE", vlerafushashtese, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMODELIFUSHASHTESE", idmodelifushashtese, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        public clsMesazh ruajVleraPerLidhjetEkzistuese(int idvlerafushashtese, int idfushashtese, String vlerafushashtese, int idmodelifushashtese)
        {//ruajtja e vlerave te fushave shtese


            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDVLERAFUSHASHTESE", idvlerafushashtese, ParameterDirection.Output);
            dbManager.AddParameters("@IDFUSHASHTESE", idfushashtese, ParameterDirection.Input);
            if (vlerafushashtese == null)
                dbManager.AddParameters("@VLERAFUSHASHTESE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters("@VLERAFUSHASHTESE", vlerafushashtese, ParameterDirection.Input);
            dbManager.AddParameters("@IDMODELIFUSHASHTESE", idmodelifushashtese, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_ins_lidhjet_ekzistuese");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        public clsMesazh modifikoVlera(int idvlerafushashtese, int idlidhese, int idfushashtese, String vlerafushashtese, int idmodelifushashtese)
        {//modifikimi i vlerave te fushave shtese


            dbManager.Open();

            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDVLERAFUSHASHTESE", idvlerafushashtese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLIDHESE", idlidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDFUSHASHTESE", idfushashtese, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERAFUSHASHTESE", vlerafushashtese, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMODELIFUSHASHTESE", idmodelifushashtese, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        public clsMesazh fshiVlera(int idvlerafushashtese)
        {//fshirja e vlerave te fushave shtese


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVLERAFUSHASHTESE", idvlerafushashtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }


        internal void merrVlera(int idvlerafushashtese)
        {// metoda per te marre nje vleren ne base te id

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDVLERAFUSHASHTESE", idvlerafushashtese, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_sel");

        }


        internal DataTable ktheGjitheVlerat()
        {//metoda per te marre te gjithe  VLERAT    


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_merrGjitheVlerat");
            return ds.Tables[0];

        }


        internal void ktheVleratSipasIdLidhese(int id, DateTime? dtAktivizimi, IDataBaseReader objekti)
        {//metoda per te marre te gjithe vlerat sipas idlidhese 


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@IDLIDHESE", id);
            dbManager.AddInputParameters("@DT_AKTIVIZIMI", dtAktivizimi);
            dbManager.FillCollection("prc_T_VLERAFUSHASHTESE_merrVleratSipasIdLidhese", objekti);


        }

        internal void ktheVleratSipasIdLidheseAndIdModeli(int id, int idmod, DateTime? dtAktivizimi, IDataBaseReader objekti)
        {//metoda per te marre te gjithe vlerat sipas idlidhese dhe idmodeli

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDLIDHESE", id);
            dbManager.AddInputParameters("@IDMODELIFUSHASHTESE", idmod);
            dbManager.AddInputParameters("@DT_AKTIVIZIMI", dtAktivizimi);
            dbManager.FillCollection("prc_T_VLERAFUSHASHTESE_merrVleratSipasIdLidheseAndIdModeli", objekti);
        }


        /// <summary>
        /// fshin vlerat e vjetra per fushat shtese
        /// </summary>
        /// <param name="idLidhese"></param>
        /// <param name="pershkrimModeli"></param>
        /// <param name="idNdermarje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal clsMesazh fshiVleraFushaShtese(int idLidhese, string pershkrimModeli, int idNdermarje, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@PERSHKRIMILLOJMODELIFUSHASHTESE", pershkrimModeli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_fshiGjitheVlerat");

            return new clsMesazh(true, "vlerat u fshin me sukses");

        }
        internal List<DateTime> MerrDataAktivizimi(int idLidhese, int idmodeli)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODELI", idmodeli, ParameterDirection.Input);
            return dbManager.GetList<DateTime>("prc_T_VLERAFUSHASHTESE_MerrDataAktivizimi");
        }
        internal bool EshteDataMeEfundit(int idLidhese, int idmodeli, DateTime dtAktivizimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODELI", idmodeli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@dtAktivizimi", dtAktivizimi, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_EshteDataMeEfundit"));
        }
        #endregion

        #region LIDHJEAUTORIZIM

        public clsMesazh ruajLidhjeAutorizim(out int idLidhjeAutorizim, int idLidhese, int idLloji, int idAutorizimeKoka, int idstatusdok)
        {
            ImbLogger.LogWarningShitje($"Filloi metoda ruajLidhjeAutorizim sipas  idLidhese:{idLidhese} , idLloji:{idLloji}, idAutorizimeKoka:{idAutorizimeKoka}, idstatusdok:{idstatusdok}");
            idLidhjeAutorizim = -1;
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDLIDHJEAUTORIZIM", idLidhjeAutorizim, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJI", idLloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDAUTORIZIMEKOKA", idAutorizimeKoka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DTKRIJIMI", DateTime.Now, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_ins");
            idLidhjeAutorizim = Convert.ToInt32(dbManager.Parameters[0].Value);
            ImbLogger.LogWarningShitje($"Mbaroi metoda ruajLidhjeAutorizim sipas  idLidhese:{idLidhese} , idLloji:{idLloji}, idAutorizimeKoka:{idAutorizimeKoka}, idstatusdok:{idstatusdok}");
            ImbLogger.LogWarningShitje("Ruajtja përfundoi me sukses");
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");

        }
        public clsMesazh ruajLidhjeAutorizim(int idLidhjeAutorizim, int idLidhese, int idLloji, int idAutorizimeKoka, int idstatusdok)
        {
            idLidhjeAutorizim = -1;
            return ruajLidhjeAutorizim(out idLidhjeAutorizim, idLidhese, idLloji, idAutorizimeKoka, idstatusdok);

        }

        public clsMesazh modifikoLidhjeAutorizim(int idLidhjeAutorizim, int idLidhese, int idLloji, int idAutorizimeKoka, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDLIDHJEAUTORIZIM", idLidhjeAutorizim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJI", idLloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDAUTORIZIMEKOKA", idAutorizimeKoka, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_upd");
            return new clsMesazh(true, "Ruajtja përfundoi me sukses");

        }

        public clsMesazh fshiLidhjeAutorizim(int idLidhjeAutorizim)
        {
            ImbLogger.LogWarningShitje($"Filloi metoda fshiLidhjeAutorizim sipas idLidhjeAutorizim:{idLidhjeAutorizim}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLIDHJEAUTORIZIM", idLidhjeAutorizim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_upddel");
            ImbLogger.LogWarningShitje($"Mbaroi metoda fshiLidhjeAutorizim sipas idLidhjeAutorizim:{idLidhjeAutorizim}");
            ImbLogger.LogWarningShitje(MessagesResource.Messages["msgFshirjeMeSukses"]);
            return new clsMesazh(true, "Fshirja perfundoi me sukses");

        }

        public void merrLidhjeAutorizim(int idLidhjeAutorizim)
        {


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhjeAutorizim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_sel");

        }

        internal DataTable ktheLidhjeAutorizimSipasIdLidheseIdLloji(int id, int idlloj)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJI", idlloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_merrLidhjeAutorizimSipasIdLidheseIdLloji");
            return ds.Tables[0];

        }

        internal DataTable ktheLidhjeAutorizimSipasIdLidheseKodLloji(int id, string kodLLoji)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLIDHESE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODLLOJBUXHETI", kodLLoji, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_merrLidhjeAutorizimSipasIdLidheseKodLloji").Tables[0];

        }

        /// <summary>
        /// merr te gjitha lidhjet e autorizimit te selektuar 
        /// </summary>
        /// <param name="idAutorizimKoka"></param>
        /// <param name="idGjuha"></param>
        /// <returns></returns>
        internal DataTable ktheLidhjeAutorizimSipasIdKokesAutorizim(int idAutorizimKoka, int idGjuha)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDAUTORIZIMEKOKA", idAutorizimKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LIDHJEAUTORIZIM_merrLidhjeAutorizimSipasKokesAutorizimit");
            return ds.Tables[0];

        }


        #endregion

        #region LLOJE DOKUMENTASH

        internal DataTable ktheGjitheDokumentat(int moduli)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODULI", moduli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDOKUMENTI_sel");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheDokumentat(int moduli)", true)]
        //public colLlojDokumenti merrGjitheDokumentat(clsLlojDokumenti d)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDMODULI", d.IdModuli, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDOKUMENTI_sel");
        //        colLlojDokumenti colLlojDok = new colLlojDokumenti();
        //        return colLlojDok.mbushArrayListLlojiDokumenta(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colLlojDokumenti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataTable ktheGjitheDokumentatSipasKomponentes(string komponente)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE", komponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDOKUMENTI_merrSipasKomponente");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheDokumentatSipasKomponentes(string komponente)", true)]
        //public colLlojDokumenti merrGjitheDokumentatSipasKomponentes(string komponente)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDKOMPONENTE",komponente, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDOKUMENTI_merrSipasKomponente");
        //        colLlojDokumenti colLlojDok = new colLlojDokumenti();
        //        return colLlojDok.mbushArrayListLlojiDokumenta(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colLlojDokumenti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrDokumentin(int idLlojDok)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJDOK", idLlojDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDOKUMENTI_ktheDokumentin");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrDokumentin(int idLlojDok)", true)]
        //public colLlojDokumenti ktheDokumentin(int idLlojDok)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@IDLLOJDOK", idLlojDok, ParameterDirection.Input);
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJDOKUMENTI_ktheDokumentin");
        //        colLlojDokumenti colLlojDok = new colLlojDokumenti();
        //        return colLlojDok.mbushArrayListLlojiDokumenta(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colLlojDokumenti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        public String ktheIdLlojDokumenti(String kodiLlojDokumenti)
        {

            String sql = "select IDLLOJDOK, KODI, PERSHKRIMI, MODULI, IDKOMPONENTE from T_LLOJDOKUMENTI where KODI = '" + kodiLlojDokumenti + "'";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow rr = ds.Tables[0].Rows[0];
                return rr[0].ToString();
            }
            else return "";

        }

        internal DataTable ktheGjitheLlojetDokumentave()
        {

            String sql = "select IDLLOJDOK, KODI, PERSHKRIMI, MODULI,IDKOMPONENTE from T_LLOJDOKUMENTI";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheLlojetDokumentave()", true)]
        //public colLlojDokumenti merrGjitheLlojetDokumentave()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    String sql = "select IDLLOJDOK, KODI, PERSHKRIMI, MODULI,IDKOMPONENTE from T_LLOJDOKUMENTI";
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
        //        colLlojDokumenti col = new colLlojDokumenti();
        //        return col.mbushArrayListLlojiDokumenta(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colLlojDokumenti();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region ReportingServices

        public Byte[] ktheStruktRaporti()
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMERRAPORTI", "ditari", ParameterDirection.Input);
            Byte[] ds = (Byte[])dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_RS_T_Catalog_sel");
            //colLlojDokumenti colLlojDok = new colLlojDokumenti();
            return ds;

        }

        #endregion

        #region TIPEFUSHASH SHTESE

        internal DataTable ktheGjithetTipeFushashShtese()
        {


            dbManager.Open();

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPIFUSHASHTESE _merrGjitheTipet");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrGjithetTipeFushashShtese()", true)]
        //public colTipeFushaShtese  merrGjithetTipeFushashShtese()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TIPIFUSHASHTESE _merrGjitheTipet");
        //        colTipeFushaShtese colTipeFushaShtese = new colTipeFushaShtese();
        //        return colTipeFushaShtese.mbushArrayListTipeshFushaShtese(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colTipeFushaShtese();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}

        #endregion

        # region Themes template

        internal DataTable merrGjitheTheme()
        {

            dbManager.Open();

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEME_sel");
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable merrGjitheTheme()", true)]
        //public colTheme ktheGjitheTheme(clsTheme theme)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEME_sel");
        //        colTheme themes = new colTheme();
        //        return themes.mbushArrayListTheme(ds);
        //    }
        //    catch (Exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ce.Message + ce.StackTrace);
        //        return new colTheme();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrSipasEmri(String emri)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMRI", emri, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEME_selSipasEmri");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Kthen id e sfondit sipas emrit te tij.
        /// </summary>
        /// <param name="emerTheme">Emri i sfondit</param>
        /// <returns>Id e sfondit, ose 0 nqs ndodh ndonje gabim</returns>
        internal int ktheIdThemeSipasEmrit(string emer)
        {
            int id;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMRI", emer, ParameterDirection.Input);
            id = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEME_merrIdSipasEmri"));
            return id;

        }

        /// <summary>
        /// Kthen pathin e sfondit sipas emrit te tij.
        /// </summary>
        /// <param name="emerTheme">Emri i sfondit</param>
        /// <returns>pathin e sfondit, ose bosh nqs ndodh ndonje gabim</returns>
        internal string kthePathThemeSipasEmrit(string emer)
        {

            object pathTheme;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMRI", emer, ParameterDirection.Input);
            pathTheme = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEME_merrPathThemeSipasEmri");
            return pathTheme != null ? pathTheme.ToString() : "";

        }

        //[Obsolete("Perdor: DataRow merrSipasEmri(String emri)", true)]
        //public clsTheme ktheSipasEmri(clsTheme theme)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@EMRI", theme.EmriTheme, ParameterDirection.Input);               

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEME_selSipasEmri");
        //        colTheme themes = new colTheme();
        //        return themes.mbushArrayListTheme(ds)[0];
        //    }
        //    catch (Exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ce.Message + ce.StackTrace);
        //        return new clsTheme();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        internal DataRow merrSipasId(int idTheme)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idTheme, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEME_selSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        //[Obsolete("Perdor: DataRow merrSipasId(int idTheme)", true)]
        //public clsTheme ktheSipasId(clsTheme theme)
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(1);
        //        dbManager.AddParameters(0, "@ID", theme.IdTheme, ParameterDirection.Input);

        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEME_selSipasId");
        //        colTheme themes = new colTheme();
        //        return themes.mbushArrayListTheme(ds)[0];
        //    }
        //    catch (Exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ce.Message + ce.StackTrace);
        //        return new clsTheme();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }
        //}

        #endregion

        #region ThemesAmbjente

        internal clsMesazh ruajThemeAmbjente(out int id, string kodi, string pershkrimi, bool defaultTheme, bool zgjedhur, int idThemeFrames, int idThemeFrameKryesor, int idThemeJQuery, int idBgImage, int idPerdorues, int idStatusDok)
        {
            id = -1;

            dbManager.Open();
            //shtimi i parametrave

            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTHEMEAMBJENT", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODTHEME", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMTHEME", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DEFAULTTHEME", defaultTheme, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ZGJEDHUR", zgjedhur, ParameterDirection.Input);
            if (idThemeFrames == 0) dbManager.AddParameters(5, "@IDTHEMEFRAMES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDTHEMEFRAMES", idThemeFrames, ParameterDirection.Input);
            if (idThemeFrameKryesor == 0) dbManager.AddParameters(6, "@IDTHEMEFRAMEKRYESOR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDTHEMEFRAMEKRYESOR", idThemeFrameKryesor, ParameterDirection.Input);
            if (idThemeJQuery == 0) dbManager.AddParameters(7, "@IDTHEMEJQUERY", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDTHEMEJQUERY", idThemeJQuery, ParameterDirection.Input);
            if (idBgImage == 0) dbManager.AddParameters(8, "@IDBGIMAGE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDBGIMAGE", idBgImage, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            //dbManager.AddParameters(10, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_INSERT");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }
        /// <summary>
        /// Ky eshte funksioni qe krijon temat default per cdo perdorues. Thirret kur krijohet perdorues i ri.
        /// </summary>
        /// <param name="idPerd">Id e perdoruesit qe eshte krijuar</param>
        /// <returns></returns>
        internal clsMesazh krijoThemesDefaultPerPerdorues(int idPerd)
        {


            dbManager.Open();
            //shtimi i parametrave

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerd, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_KrijoThemesDefault");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        internal clsMesazh modifikoThemeAmbjente(int id, string kodi, string pershkrimi, bool defaultTheme, bool zgjedhur, int idThemeFrames, int idThemeFrameKryesor, int idThemeJQuery, int idBgImage, int idPerdorues, int idStatusDok)
        {


            dbManager.Open();

            //shtimi i parametrave

            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTHEMEAMBJENT", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODTHEME", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMTHEME", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DEFAULTTHEME", defaultTheme, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ZGJEDHUR", zgjedhur, ParameterDirection.Input);
            if (idThemeFrames == 0) dbManager.AddParameters(5, "@IDTHEMEFRAMES", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDTHEMEFRAMES", idThemeFrames, ParameterDirection.Input);
            if (idThemeFrameKryesor == 0) dbManager.AddParameters(6, "@IDTHEMEFRAMEKRYESOR", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDTHEMEFRAMEKRYESOR", idThemeFrameKryesor, ParameterDirection.Input);
            if (idThemeJQuery == 0) dbManager.AddParameters(7, "@IDTHEMEJQUERY", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDTHEMEJQUERY", idThemeJQuery, ParameterDirection.Input);
            if (idBgImage == 0) dbManager.AddParameters(8, "@IDBGIMAGE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(8, "@IDBGIMAGE", idBgImage, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            //dbManager.AddParameters(10, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_UPDATE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        internal clsMesazh modifikoZgjedhurThemeAmbjente(int id, bool zgjedhur, int idPerdorues)
        {

            dbManager.Open();

            //shtimi i parametrave

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDTHEMEAMBJENT", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ZGJEDHUR", zgjedhur, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_UPDATEZGJEDHUR");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

        }

        internal clsMesazh fshiThemeAmbjente(int idTheme)
        {


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_UPDDEL");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal void merrThemeAmbjenteSipasId(int idTheme, IDataBaseReader objektiPerTuMbushur)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
            dbManager.FillObject("prc_T_THEMESAMBJENTE_SelThemeSipasIdDR", objektiPerTuMbushur);


        }
        internal DataRow merrThemeAmbjenteSipasId(int idTheme)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
            using (var dt = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_SelThemeSipasIdDR").Tables[0])
                return dt.Rows?[0];


        }
        internal void merrThemeAmbjenteSipasKodit(string kodTheme, int idPerd, IDataBaseReader objektiPerTuMbushur)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODTHEME", kodTheme, ParameterDirection.Input);
            //dbManager.AddParameters(1, "@IDNDERM", idPerd, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERD", idPerd, ParameterDirection.Input);
            dbManager.FillObject("prc_T_THEMESAMBJENTE_SelThemeSipasKodNdermPerd", objektiPerTuMbushur);
        }

        public bool ekzistonThemeAmbjent(string kod, int idperd)
        {//kontrollon nqs ekziston nje theme me kete kod per perdoruesin


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERD", idperd, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_ekzistonThemeAmbjente"));
            return Convert.ToBoolean(pergjigje);



        }

        public bool eshteThemeDefault(int idTheme)
        {//kontrollon nqs theme me kete id eshte theme default


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_eshteDefault"));
            return Convert.ToBoolean(pergjigje);

        }

        public bool eshteThemeZgjedhur(int idTheme)
        {//kontrollon nqs theme me kete id eshte theme default


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_eshteZgjedhur"));
            return Convert.ToBoolean(pergjigje);

        }

        internal DataTable ktheGjitheThemesAmbjenteSipasNdermPerd(int idperd)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERD", idperd, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_SelSipasPerdoruesDT");
            return ds.Tables[0];

        }

        internal int ktheIdThemeZgjedhurPerdorues(int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERD", idPerdorues, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_KtheThemeIdZgjedhurPerd"));
        }

        internal void ktheThemeZgjedhurPerdorues(int idperd, IDataBaseReader objektiPerTuMbushur)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERD", idperd, ParameterDirection.Input);
            dbManager.FillObject("prc_T_THEMESAMBJENTE_KtheThemeZgjedhurPerd", objektiPerTuMbushur);


        }

        internal void ktheDataTableThemeZgjedhurPerdorues(int idperd, IDataBaseReader objektiPerTuMbushur)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERD", idperd, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_THEMESAMBJENTE_KtheThemeZgjedhurPerd", objektiPerTuMbushur);

        }


        //metoda qe merr procedurat per asistentin 
        public DataTable ktheGjitheSpAsistenti()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_Asistenti_SelectoTabelenAsistenti");
            return ds.Tables[0];
        }

        //metoda qe ekzekuton procedurat e kthyera nga sp qe selecton procedurat
        internal string ekzekutoSpAsistenti(string emerproc)
        {
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(0);
                return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, emerproc));
            }
            catch (Exception e)
            {
                ImbLogger.Error(e);
                return String.Empty;
            }
        }
        internal string ekzekutoSpAsistenti(string emerproc, int idndermarrje)
        {
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
                return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, emerproc));
            }
            catch (Exception e)
            {
                ImbLogger.Error(e);
                return String.Empty;
            }
        }
        public DataTable ktheSpRregullueseSipasId(string id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_Asistenti_ktheSpRregullueseSipasId");
            return ds.Tables[0];
        }

        public clsMesazh ekzekutoSpRregulluese(string emerproc)
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, emerproc);
            return new clsMesazh(true, String.Format("Ezkekutimi i sp rregulluese {0} perfundoi me sukses!", emerproc));
        }
        public clsMesazh ekzekutoSpRregulluese(string emerproc, int idndermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, emerproc);
            return new clsMesazh(true, String.Format("Ezkekutimi i sp rregulluese {0} perfundoi me sukses!", emerproc));
        }
        public clsMesazh ekzekutoSpRregulluese(string emerproc, int idndermarrje,int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, emerproc);
            return new clsMesazh(true, String.Format("Ezkekutimi i sp rregulluese {0} perfundoi me sukses!", emerproc));
        }
        //public clsMesazh eshteThemeZgjedhurOseDefault(int idTheme)
        //{
        //    bool themedefault = false;
        //    bool zgjedhur = false;

        //    if (dbManager == null)
        //    {
        //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //        dbManager.ConnectionString = dbManager.GetConnectionString();
        //    }
        //    try
        //    {
        //        dbManager.Open();
        //        dbManager.CreateParameters(3);
        //        dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
        //        dbManager.AddParameters(1, "@DEFAULTI", themedefault, ParameterDirection.Output);
        //        dbManager.AddParameters(2, "@ZGJEDHUR", zgjedhur, ParameterDirection.Output);
        //        dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_THEMESAMBJENTE_eshteDefaultOseZgjedhur");
        //        themedefault = bool.Parse(dbManager.Parameters["DEFAULTI"].Value.ToString());
        //        zgjedhur = bool.Parse(dbManager.Parameters["ZGJEDHUR"].Value.ToString());                
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        #endregion

        #region ThemesDevExpressJQuery

        internal DataTable ktheGjitheThemesDevExpressJQuery()
        {


            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEMESDEVEXPRESSJQUERY_SelAll");
            return ds.Tables[0];

        }

        internal DataTable ktheThemesDevExpressJQuerySipasLloji(bool lloji)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEMESDEVEXPRESSJQUERY_SelSipasLloji");
            return ds.Tables[0];

        }

        internal DataRow merrThemeDevExpressJQuerySipasId(int idTheme)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTHEME", idTheme, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEMESDEVEXPRESSJQUERY_SelSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrThemeDevExpressJQuerySipasEmri(string emri)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMERTHEME", emri, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_THEMESDEVEXPRESSJQUERY_SelSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal int merrThemeDevExpressJQueryNgaEmri(string emerTheme)
        {
            int id;


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@EMERTHEME", emerTheme, ParameterDirection.Input);
            id = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_THEMESDEVEXPRESSJQUERY_SelIDSipasKodit"));
            return id;

        }

        #endregion

        #region Konfigurime

        internal DataTable ktheGjitheKonfigurimet()
        {

            string sqlstr = "select * from T_KONFIGURIME";

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
            return ds.Tables[0];

        }
        //[Obsolete("Perdor: DataTable ktheGjitheKonfigurimet()", true)]
        //public colKonfigurime merrGjitheKonfigurimet()
        //{
        //    IDBManager dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
        //    dbManager.ConnectionString = dbManager.GetConnectionString();
        //    string sqlstr = "select * from T_KONFIGURIME";
        //    try
        //    {
        //        dbManager.Open();
        //        DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sqlstr);
        //        colKonfigurime konfigurimet = new colKonfigurime();
        //        return konfigurimet.mbushArrayListKonfigurime(ds);
        //    }
        //    catch (Exception)
        //    {
        //        return new colKonfigurime();
        //    }
        //    finally
        //    {
        //        dbManager.Dispose();
        //    }

        //}
        /// <summary>
        /// merr te dhenat e licences qe i perket perdoruesi 
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal DataRow merrLicenceSipasIdPerdoruesi(int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LICENCA_merrSipasIdPerdoruesi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrLicenceSipasId(int idLicenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLICENCA", idLicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LICENCA_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrLicenceAktive()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LICENCA_merrLicencaAktive");

            return ds.Tables[0];

        }
        #endregion

        #region KonfigurimEmail

        internal clsMesazh ruajKonfigurimEmail(out int id, string outgoingSmtp, string dergoEmailNga, string password, int portaSmtp, int idNdermarrje, int idPerdorues, bool enableSsl)
        {
            id = -1;


            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKONFIGURIMEMAIL", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@OUTGOINGSMTP", outgoingSmtp, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DERGOEMAILNGA", dergoEmailNga, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PASSWORD", password, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PORTASMTP", portaSmtp, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ENABLESSL", enableSsl, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh modifikoKonfigurimEmail(int id, string outgoingSmtp, string dergoEmailNga, string password, int portaSmtp, int idNdermarrje, int idPerdorues, bool enableSsl)
        {


            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@IDKONFIGURIMEMAIL", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@OUTGOINGSMTP", outgoingSmtp, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DERGOEMAILNGA", dergoEmailNga, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PASSWORD", password, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PORTASMTP", portaSmtp, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(7, "@ENABLESSL", enableSsl, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        public bool kaKonfigurimEmailPerNdermarrjen(int idNdermarrje)
        {//kontrollon nqs ka konfigurim emaili per ndermarrjen


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_kaKonfigSipasNdermarrje"));
            return Convert.ToBoolean(pergjigje);

        }

        public string merrEmailKonfigurimiSipasIdNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_merrEmailSipasNdermarrje"));
        }

        internal DataRow merrKonfigurimEmailSipasIdNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_selSipasNdermarrje");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataRow merrKonfigurimEmailSipasIdKonfigurimi(int idKonfigurim)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMEMAIL", idKonfigurim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_selSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal int merrIdKonfigurimEmailNgaNdermarrje(int idNderm)
        {
            int id;


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            id = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGURIMEMAIL_ktheIdKonfigSipasNdermarrje"));
            return id;

        }

        #endregion

        #region KonfigurimFtp



        internal void merrKonfigurimFtpSipasId(int idKonfigurimFtp, int idNdermarrje, IDataBase konfigurim)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMEFTP", idKonfigurimFtp, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KONFIGURIMEFTP_selSipasId", konfigurim);
        }
        internal void merrWebhookSipasId(int idWebhooks, int idNdermarrje, IDataBase konfigurim)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDWEBHOOKS", idWebhooks, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_WEBHOOKS_selSipasId", konfigurim);
        }

        internal clsMesazh ruajKonfigurimFtp(out int id, string kodi, string hostName, string username, string password, int port, int idNdermarrje, int idPerdorues, bool enableSSL, int metoda, bool eshteSFTP, string folderPath)
        {
            id = -1;


            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(13);
            dbManager.AddParameters("@IDKONFIGURIMEFTP", id, ParameterDirection.Output);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@HOSTNAME", hostName, ParameterDirection.Input);
            dbManager.AddParameters("@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters("@PASSWORD", password, ParameterDirection.Input);
            dbManager.AddParameters("@PORT", port, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@ENABLESSL", enableSSL, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", 1, ParameterDirection.Input);
            dbManager.AddParameters("@METODA", metoda, ParameterDirection.Input);
            dbManager.AddParameters("@ESHTE_SFTP", eshteSFTP, ParameterDirection.Input);
            dbManager.AddParameters("@FOLDER_PATH", folderPath, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
       
        internal clsMesazh modifikoKonfigurimFtp(int id, string kodi, string hostName, string username, string password, int port, int idNdermarrje, int idPerdorues, bool enableSSL, int metoda, bool eshteSFTP, string folderPath)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(12);
            dbManager.AddParameters("@IDKONFIGURIMEFTP", id, ParameterDirection.Input);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@HOSTNAME", hostName, ParameterDirection.Input);
            dbManager.AddParameters("@USERNAME", username, ParameterDirection.Input);
            dbManager.AddParameters("@PASSWORD", password, ParameterDirection.Input);
            dbManager.AddParameters("@PORT", port, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@ENABLESSL", enableSSL, ParameterDirection.Input);
            dbManager.AddParameters("@METODA", metoda, ParameterDirection.Input);
            dbManager.AddParameters("@ESHTE_SFTP", eshteSFTP, ParameterDirection.Input);
            dbManager.AddParameters("@FOLDER_PATH", folderPath, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        internal clsMesazh ruajWebhook(out int id, string kodi, string url, int idNdermarrje, int idPerdorues, bool aktive, int kategoria, int eventi)
        {
            id = -1;


            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(9);
            dbManager.AddParameters("@IDWEBHOOK", id, ParameterDirection.Output);
            dbManager.AddParameters("@KODIWEBHOOK", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@URLPRITESE", url, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", aktive, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", 1, ParameterDirection.Input);
            dbManager.AddParameters("@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.AddParameters("@EVENTI", eventi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_WEBHOOKS_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh modifikowebhook(int id, string kodi, string url, int idNdermarrje, int idPerdorues, bool aktive, int kategoria, int eventi)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(9);
            dbManager.AddParameters("@IDWEBHOOK", id, ParameterDirection.Input);
            dbManager.AddParameters("@KODIWEBHOOK", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@URLPRITESE", url, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", aktive, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", 1, ParameterDirection.Input);
            dbManager.AddParameters("@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.AddParameters("@EVENTI", eventi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_WEBHOOKS_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }
        public bool kaKonfigurimFtpPerNdermarrjen(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_kaKonfigSipasNdermarrje"));
            return pergjigje == 1;

        }

        public void mbushKonfigurimFtpSipasIdNdermarrje(int idNdermarrje, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KONFIGURIMEFTP_selSipasNdermarrje", objectToFill);
        }
        public void mbushWebhooksIdNdermarrje(int idNdermarrje, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_WEBHOOKS_selSipasNdermarrje", objectToFill);
        }

        /// <summary>
        /// Merr gjithe konfigurimet FTP te ndermarrjes sipas metodes se kerkuar
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idMetoda"></param>
        /// <param name="objectToFill"></param>
        public void mbushKonfigurimFtpSipasIdNdermarrjeDheMetode(int idNdermarrje, int idMetoda, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMETODA", idMetoda, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KONFIGURIMEFTP_selSipasNdermarrjeDheMetode", objectToFill);
        }
        /// <summary>
        /// Merr gjithe konfigurimet FTP te ndermarrjes sipas metodes se kerkuar qe i perkasin kategorise
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idMetoda"></param>
        /// <param name="kategoria"></param>
        /// <param name="objectToFill"></param>
        public void mbushKonfigurimFtpSipasIdNdermarrjeDheMetode(int idNdermarrje, int idMetoda, string kategoria, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMETODA", idMetoda, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KONFIGURIMEFTP_selSipasNdermarrjeMetodeKategorie", objectToFill);
        }
        internal int merrIDKonfigurmiFtp(string kod, int idNdermarje)
        {//kthen nje konfigurim ftp sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFTP_ktheKonfigurimFtpSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idKonfigurimFtp;
            int.TryParse(ds.Tables[0].Rows[0]["IDKONFIGURIMEFTP"].ToString(), out idKonfigurimFtp);
            return idKonfigurimFtp;
        }
        internal int merrIDWebhook(string kod, int idNdermarje)
        {//kthen nje konfigurim ftp sipas kodit

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_WEBHOOKS_ktheKonfigurimFtpSipasKodit");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idWebhook;
            int.TryParse(ds.Tables[0].Rows[0]["IDWEBHOOKS"].ToString(), out idWebhook);
            return  idWebhook;
            
        }

        internal void merrKonfiguriminEPareManualFtp(int idNdermarrje, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_KONFIGURIMEFTP_merrKonfiguriminEPareManual", objectToFill);
        }

        #endregion

        #region SerialeUnike


        internal DataTable merrGjendjeSerialeUnikeAparte(int idNdermarje, string data, string lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters("@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters("@GJENDJELLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_MERRGJENDJEPERAPARATE");
            return ds.Tables[0];
        }

        internal DataTable merrSerialeUnikePerTransferim(int idNdermarje, string idKategoriSeriali, string data, int idLlojDokumentMag, int idMetodeTransferimi, out bool gabim)
        {
            gabim = false;
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters("@KATEGORISERIALI", idKategoriSeriali, ParameterDirection.Input);
            dbManager.AddParameters("@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters("@IDLLOJDOKMAGAZINE", idLlojDokumentMag, ParameterDirection.Input);
            dbManager.AddParameters("@IDMETODETRANSFERIMI", idMetodeTransferimi, ParameterDirection.Input);
            dbManager.AddParameters("@MESAZHI", gabim, ParameterDirection.Output);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_MERRSERIALE_SIPASDITES");
            gabim = (bool)dbManager.Parameters[5].Value;
            return ds.Tables[0];
        }

        internal clsMesazh ruajLidhjeKategoriserialKonfigurimftp(out int idLidhje, int idKategori, int idKonfigurimFtp)
        {
            idLidhje = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDLIDHJE", idLidhje, ParameterDirection.Output);
            dbManager.AddParameters("@IDKATEGORI", idKategori, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGURIMFTP", idKonfigurimFtp, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", 1, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_X_KONFIGURIMEFTP_ins");
            idLidhje = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh modifikoLidhjeKategoriserialKonfigurimftp(int idLidhje, int idKategori, int idKonfigurimFtp)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDLIDHJE", idLidhje, ParameterDirection.Output);
            dbManager.AddParameters("@IDKATEGORI", idKategori, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGURIMFTP", idKonfigurimFtp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_X_KONFIGURIMEFTP_upd");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
        }

        internal clsMesazh fshiLidhjeKategoriserialKonfigurimftp(int idLidhje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDLIDHJE", idLidhje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_SERIALEUNIKE_KATEGORI_X_KONFIGURIMEFTP_del");
            return new clsMesazh(true, "Fshirja përfundoi me sukses!");
        }

        internal void mbushLidhjeKategoriserialiKonfigurimftpSipasIdKategori(int id, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATEGORI", id, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_SERIALEUNIKE_KATEGORI_X_KONFIGURIMEFTP_merrLidhjeSipasIdKategorie", objectToFill);
        }

        internal string merrEmerPerSkedarTransferimi(string kodi, string data, int nrRreshta, int idMetoda, out string emerSkedari)
        {
            emerSkedari = "";
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters("@NRRRESHTA", nrRreshta, ParameterDirection.Input);
            dbManager.AddParameters("@IDMETODETRANSFERIMI", idMetoda, ParameterDirection.Input);
            dbManager.AddParameters("@EMRISKEDARIT", emerSkedari, ParameterDirection.Output);
            dbManager.Parameters[4].Size = 250;
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_MERREMERSKEDARTRANSFERIMI");
            emerSkedari = dbManager.Parameters[4].Value.ToString();
            //return dbManager.Parameters[3].Value.ToString();
            return emerSkedari;
        }

        #endregion

        #region KonfigurimeFjalekalimi

        /// <summary>
        /// kthen konfigurimin e fjalekalimit per kete idPerdorues
        /// Te dhenat merren nga tabela T_KonfigurimeFjalekalimi
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal DataRow merrKonfigurimeFjalekalimiSipasIdPerdoruesi(int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KonfigurimeFjalekalimi_selSipasPerdoruesit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow merrKonfigurimeFjalekalimiSipasIdPunonjesi(int idPunonjes)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KonfigurimeFjalekalimi_selSipasPunonjesit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kontrollon nqs ka konfigurim fjalekalimi per licencen qe permban kete perdorues
        /// nepermjet idPerdoruesi, ne kete SP gjejme licencen perkatese
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns>kthen 0 nqs nuk ka konfigurim per licencen e ketij perdoruesit, kthen idLicencen e perdoruesit ne rastin kur ekziston konfigurimi i fjalekalimit  </returns>
        public int kaKonfigurimeFjalekalimiPerLicencen(int idPerdoruesi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KonfigurimeFjalekalimi_kaKonfigSipasLicences"));

        }

        /// <summary>
        /// shton nje konfigurim te ri fjalekalimi per licencen e perdoruesit aktual. Shtimi behet ne tabelen T_KONFIGURIMEFJALEKALIMI 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ruajHistorikunPass"></param>
        /// <param name="nrHereRuajHistorikPass"></param>
        /// <param name="komplexPassword"></param>
        /// <param name="ndryshimPasswordiDetyruar"></param>
        /// <param name="gjatesiaMinPassword"></param>
        /// <param name="diteSkadimiPassword"></param>
        /// <param name="bllokoPerdorues"></param>
        /// <param name="tentativaBllokPerdorues"></param>
        /// <param name="bllokoLogin"></param>
        /// <param name="idPERDORUES"></param>
        /// <param name="maxSessionPerPerdorues"></param>
        /// <param name="skadoPassword"></param>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        internal clsMesazh ruajKonfigurimFjalekalimi(out int id, bool ruajHistorikunPass, int nrHereRuajHistorikPass, bool komplexPassword, bool ndryshimPasswordiDetyruar, int gjatesiaMinPassword, int diteSkadimiPassword, bool bllokoPerdorues, int tentativaBllokPerdorues, bool bllokoLogin, int idPERDORUES, int maxSessionPerPerdorues, bool skadoPassword, bool resetPassword, bool gjeneroPassword, int specialChars, int uppercaseChars, int numbersChars,bool twofactorauth)
        {
            id = -1;
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(19);
            dbManager.AddParameters(0, "@IDKonfigurimePassword", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ruajHistorikunPass", ruajHistorikunPass, ParameterDirection.Input);
            dbManager.AddParameters(2, "@nrHereRuajHistorikPass", nrHereRuajHistorikPass, ParameterDirection.Input);
            dbManager.AddParameters(3, "@komplexPassword", komplexPassword, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ndryshimPasswordiDetyruar", ndryshimPasswordiDetyruar, ParameterDirection.Input);
            dbManager.AddParameters(5, "@gjatesiaMinPassword", gjatesiaMinPassword, ParameterDirection.Input);
            dbManager.AddParameters(6, "@diteSkadimiPassword", diteSkadimiPassword, ParameterDirection.Input);
            dbManager.AddParameters(7, "@bllokoPerdorues", bllokoPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(8, "@tentativaBllokPerdorues", tentativaBllokPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(9, "@bllokoLogin", bllokoLogin, ParameterDirection.Input);
            dbManager.AddParameters(10, "@idPERDORUES", idPERDORUES, ParameterDirection.Input);
            dbManager.AddParameters(11, "@MAXSESSIONxUSER", maxSessionPerPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SKADOPASSWORD", skadoPassword, ParameterDirection.Input);
            dbManager.AddParameters(13, "@RESETOPASSWORD", resetPassword, ParameterDirection.Input);
            dbManager.AddParameters(14, "@GJENEROPASSWORD", gjeneroPassword, ParameterDirection.Input);
            dbManager.AddParameters(15, "@NRSPECIALCHARSINPASS", specialChars, ParameterDirection.Input);
            dbManager.AddParameters(16, "@NRUPPERCASECHARSINPASS", uppercaseChars, ParameterDirection.Input);
            dbManager.AddParameters(17, "@NRNUMBERSCHARSINPASS", numbersChars, ParameterDirection.Input);
            dbManager.AddParameters(18, "@TWOFACTORAUTH", twofactorauth, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFJALEKALIMI_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// modifikon konfigurimin e fjalekalimit sipas licences
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ruajHistorikunPass"></param>
        /// <param name="nrHereRuajHistorikPass"></param>
        /// <param name="komplexPassword"></param>
        /// <param name="ndryshimPasswordiDetyruar"></param>
        /// <param name="gjatesiaMinPassword"></param>
        /// <param name="diteSkadimiPassword"></param>
        /// <param name="bllokoPerdorues"></param>
        /// <param name="tentativaBllokPerdorues"></param>
        /// <param name="bllokoLogin"></param>
        /// <param name="idPERDORUES"></param>
        /// <param name="maxSessionPerPerdorues"></param>
        /// <param name="skadoPassword"></param>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        internal clsMesazh modifikoKonfigurimFjalekalimi(int id, bool ruajHistorikunPass, int nrHereRuajHistorikPass, bool komplexPassword, bool ndryshimPasswordiDetyruar, int gjatesiaMinPassword, int diteSkadimiPassword, bool bllokoPerdorues, int tentativaBllokPerdorues, bool bllokoLogin, int idPERDORUES, int maxSessionPerPerdorues, bool skadoPassword, bool resetPassword, bool gjeneroPassword, int specialChars, int uppercaseChars, int numbersChars,bool twofactorauth)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(19);
            dbManager.AddParameters(0, "@IDLICENCA", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ruajHistorikunPass", ruajHistorikunPass, ParameterDirection.Input);
            dbManager.AddParameters(2, "@nrHereRuajHistorikPass", nrHereRuajHistorikPass, ParameterDirection.Input);
            dbManager.AddParameters(3, "@komplexPassword", komplexPassword, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ndryshimPasswordiDetyruar", ndryshimPasswordiDetyruar, ParameterDirection.Input);
            dbManager.AddParameters(5, "@gjatesiaMinPassword", gjatesiaMinPassword, ParameterDirection.Input);
            dbManager.AddParameters(6, "@diteSkadimiPassword", diteSkadimiPassword, ParameterDirection.Input);
            dbManager.AddParameters(7, "@bllokoPerdorues", bllokoPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(8, "@maxTentativaLoginXSession", tentativaBllokPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(9, "@bllokoLogin", bllokoLogin, ParameterDirection.Input);
            dbManager.AddParameters(10, "@idPERDORUES", idPERDORUES, ParameterDirection.Input);
            dbManager.AddParameters(11, "@MAXSESSIONxUSER", maxSessionPerPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SKADOPASSWORD", skadoPassword, ParameterDirection.Input);
            dbManager.AddParameters(13, "@RESETOPASSWORD", resetPassword, ParameterDirection.Input);
            dbManager.AddParameters(14, "@GJENEROPASSWORD", gjeneroPassword, ParameterDirection.Input);
            dbManager.AddParameters(15, "@NRSPECIALCHARSINPASS", specialChars, ParameterDirection.Input);
            dbManager.AddParameters(16, "@NRUPPERCASECHARSINPASS", uppercaseChars, ParameterDirection.Input);
            dbManager.AddParameters(17, "@NRNUMBERSCHARSINPASS", numbersChars, ParameterDirection.Input);
            dbManager.AddParameters(18, "@TWOFACTORAUTH ", twofactorauth, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFJALEKALIMI_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen konfigurimin e fjalekalimit per licencen e perdoruesit, ose licencen default ne rastin kur nuk ka konfigurim
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal DataRow merrKonfigurimFjalekalimiSipasPerdoruesit(int idPerdoruesi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGURIMEFJALEKALIMI_selSipasPerdoruesLicence");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// merr passwordet e fundit qe ruhen ne tabelen T_PASSWORD_HISTORY ne databaze per perdoruesin
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="nr">numrin e passwordeve te fundit per kete perdorues</param>
        /// <returns></returns>
        internal DataTable merrHistorikPassPerPerdoruesin(int idPerdorues, int nr)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@idPerdorues", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Nr", nr, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASSWORD_HISTORY_merrPasswordet");
            return ds.Tables[0];

        }

        #endregion

        #region STILE RAPORTI

        /// <summary>
        /// Kthen te gjitha stilet e raporteve
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<ReportStyle> GetReportSytles(int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDGJUHA", idGjuha);
            return dbManager.GetIEnumerbale("prc_T_STILRAPORTI_sel", ReportStyle.Create);
        }

        #endregion

        #region Lidhja Dokumentave

        /// <summary>
        /// kthen true ose false qe tregon nese dokumenti eshte i lidhur apo jo
        /// eshte krijuar nje store procedure universale qe te perdoret e njejta per te gjithe ambjentet e rregjistrimit
        /// ne te kontrollohen keto 3 raste
        /// 1 nese dokumenti eshte dokument i lidhur pra statusi 0 tek tabela e lidhjes se dokumentave dhe ndodhet tek trupi i lidhjes se dokumentit
        /// 2 nese dokumenti eshte i gjeneruar nga dokumenta te tjere per kete kontrollohet idgjenerues nese eshte != 0
        /// 3 nese dokumenti eshte i lidhur por nuk shfaqet tek lidhja e dokumentave per kete jane krijuar dy tabela  
        /// t_tabperkontroll qe mban tipet e kontrolleve dhe kontrollin qe behet ne databaze per kete tip dhe 
        /// t_lidhesperkontroll qe lidh nivelin e dokumentit me tipin e kontrollit
        /// </summary>        
        /// <param name="idDok">id e dokumentit</param>
        /// <param name="idNivel"> id e nivelit te dokumentit</param>
        /// <param name="emertabele"> emri i tabeles ku ndodhet ky dokument</param>
        /// <param name="emerkoloneid">emri i kolones qe mban id e kesaj tabele</param>
        ///<returns>nje objekt boolean  qe tregon nese dokumenti eshte i lidhur apo jo</returns>
        public bool eshteDokumentiILidhur(int idDok, int idNivel, string emertabele, string emerkoloneid)
        {
            if (MerrDokLidhur(idDok, idNivel, emertabele, emerkoloneid).Rows.Count == 0)
                return false;
            else
            {
                ImbLogger.LogWarningShitje("Dokumenti me idDok: " + Convert.ToString(idDok) + " ,me idNivel: " + Convert.ToString(idNivel) + ", emer tabele: " + Convert.ToString(emertabele) + " dhe emer kooloneid: " + Convert.ToString(emerkoloneid) + " eshte lidhur.");
                return true;
            }

        }
        /// <summary>
        /// Merr id-te e dokumentave te lidhur me dokumentin e dhene
        /// </summary>
        /// <param name="idDok"></param>
        /// <param name="idNivel"></param>
        /// <param name="emertabele"></param>
        /// <param name="emerkoloneid"></param>
        /// <returns></returns>
        public DataTable MerrDokLidhur(int idDok, int idNivel, string emertabele, string emerkoloneid)
        {
            ImbLogger.LogTraceShitje($"Merr id-te e dokumentave te lidhur me dokumentin e dhene me parametra idDok:{idDok}, idNivel:{idNivel}, emertabele:" + emertabele + $", emerkoloneid:" + emerkoloneid);
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDDOKUMENTI", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTAB", emertabele, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERKOLONEID", emerkoloneid, ParameterDirection.Input);
            ImbLogger.LogTraceShitje("Id-te e dokumentave te lidhur me dokumentin e dhene u moren me sukses.");
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TAB_eshteILidhur").Tables[0];
        }
        public bool eshteILidhur(int idShitjeKoka)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda eshteILidhur me parametrer idShitjeKoka:{idShitjeKoka}");
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSHITJEKOKA", idShitjeKoka, ParameterDirection.Input);
            ImbLogger.LogTraceShitje($"Mbaroi metoda eshteILidhur me parametrer idShitjeKoka:{idShitjeKoka}");
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKASHITJE_eshteILidhur"));
        }

        /// kthen konfigurimin e dok
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal string ktheKonfigurimDok(int idKoka, string lloji)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@lloji", lloji, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_VEPRIMBANKAKOKA_percaktoKomponenteSipasKOnfigurimit"));
        }
        /// <summary>
        /// kthen true ose false qe tregon nese dokumenti eshte i lidhur apo jo
        /// eshte krijuar nje store procedure universale qe te perdoret e njejta per te gjithe ambjentet e celjes
        /// ne te kontrollohen rasti
        /// nese dokumenti eshte i lidhur me ambjente te tjera per kete jane krijuar dy tabela  
        /// t_tabperkontroll qe mban tipet e kontrolleve dhe kontrollin qe behet ne databaze per kete tip dhe 
        /// t_lidhesperkontroll qe lidh nivelin e dokumentit me tipin e kontrollit
        /// </summary>
        /// <param name="iddok">id e dokumentit</param>
        /// <param name="idnivel"> id e nivelit te dokumentit</param>
        ///<returns>nje objekt boolean  qe tregon nese dokumenti eshte i lidhur apo jo</returns>
        public bool eshteDokumentiILidhurCelje(int iddok, int idnivel)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOKUMENTI", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idnivel, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TAB_eshteILidhurCelje");

            if (ds.Tables[0].Rows.Count == 1)
                return false;
            else return true;

        }
        #endregion

        #region INFO  KOKA

        internal DataTable merrGjitheInfoKokaNdermarrjes(int idndermarrje)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_ktheInfoNdermarrjes").Tables[0];

        }
        internal DataTable merrGjitheInfoKokaNdermarrjesSipasLlojit(int idndermarrje, int lloji)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_ktheInfoNdermarrjesDheLlojit");
            return ds.Tables[0];

        }
        internal DataTable merrInfoKokaNdermarrjesPerGride(int idNdermarje)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_ktheInfoNdermarrjesPerGride");
            return ds.Tables[0];

        }
        internal DataTable merrInfoKokaNdermarrjesPerGrideSipasLloji(int idNdermarje, int lloji)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@LLOJI", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_ktheInfoNdermarrjesPerGrideDheLlojit");
            return ds.Tables[0];

        }
        internal DataRow merrInfoKokaSipasID(int idInfoKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_ktheSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow merrInfoKokaSipasKodit(string kodInfoKoka, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIINFOKOKA", kodInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_ktheSipasKoditDheNdermarrjes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        public bool ekzistonInfoKokaSipasKodNdermarje(String kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODIINFOKOKA", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_eksiston");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaVeprimeInfo(int idInfo)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfo, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOKOKA_kaVeprime");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        internal clsMesazh fshiInfoKoka(int idInfoKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOKOKA_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiInfoStatus(int idInfoKoka, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOKOKA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajInfoKoka(out int idInfoKoka, String kodi, String pershkrimi, int periudha, int idndermarja, int idperdoruesi, int idstatusdok, int lloji, int IdFormatNumri)
        {
            idInfoKoka = -1;
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERIUDHA", periudha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDFORMATNUMRI", IdFormatNumri, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOKOKA_ins");
            idInfoKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }


        internal clsMesazh modifikoInfoKoka(int idInfoKoka, String kodi, String pershkrimi, int periudha, int idndermarja, int idperdoruesi, int idstatusdok, int lloji, int IdFormatNumri)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERIUDHA", periudha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDFORMATNUMRI", IdFormatNumri, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOKOKA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        #endregion

        #region INFO  TRUPI

        internal DataRow merrInfoTrupiSipasID(int idInfoTrupi)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOTRUPI", idInfoTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOTRUPI_ktheInfoSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }

        internal DataTable merrInfoTrupiSipasIdKoka(int idInfoKoka)
        {
            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOTRUPI_ktheInfoTrupiSipasIdKoka");
            return ds.Tables[0];
        }

        internal DataTable merrInfoTrupiSipasIdKokaDheVisible(int idInfoKoka, bool visible, int idndermarje)
        {
            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Visible", visible, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOTRUPI_ktheInfoTrupiSipasIdKokaDheVisible");
            return ds.Tables[0];

        }

        internal DataTable merrInfoTrupiSipasIdKokaDheVisibleNew(int idInfoKoka, bool visible, int idndermarje)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Visible", visible, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_INFOTRUPI_ktheInfoTrupiSipasIdKokaDheVisibleNew");
            return ds.Tables[0];

        }

        internal clsMesazh fshiInfoTrupi(int idInfoTrupi)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOTRUPI", idInfoTrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOTRUPI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh fshiInfoTrupiSipasIdKoka(int idInfoKoka)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOTRUPI_delSipasIdInfoKoka");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh ruajInfoTrupi(int idInfoKoka, String emerKolone, String pershkrimKolone, bool visibility, int rendi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();

            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMERKOLONE", emerKolone, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKOLONE", pershkrimKolone, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VISIBLITY", visibility, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RENDI", rendi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOTRUPI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        internal clsMesazh modifikoInfoTrupi(int idInfoTrupi, int idInfoKoka, String emerKolone, String pershkrimKolone, bool visibility, int rendi)
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();

            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDINFOTRUPI", idInfoTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDINFOKOKA", idInfoKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERKOLONE", emerKolone, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMKOLONE", pershkrimKolone, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VISIBLITY", visibility, ParameterDirection.Input);
            dbManager.AddParameters(5, "@RENDI", rendi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_INFOTRUPI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }


        #endregion

        #region LLOJLICENCE
        /// <summary>
        /// kthen gjithe objektet lloj takse 
        /// </summary>
        /// <returns> nje objekt colLlojTakse qe permban nje koleksion me te gjithe llojet e taksave</returns>
        internal DataTable ktheGjitheLlojeLicence()
        {

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJLICENCE_merrGjitheLlojLicence");
            return ds.Tables[0];

        }

        #endregion

        #region KOKA FORMAT IMPORTI

        internal DataTable merrGjitheFormatImportiNdermarrjes(int idndermarrje)
        {


            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheFormatImportiNdermarrjes");
            return ds.Tables[0];

        }
        internal DataTable merrGjitheFormatImportiNdermarjesDheKategorise(int idndermarrje, int idkategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheFormatSipasNdermarjeDheKategorise");
            return ds.Tables[0];
        }
        internal DataTable merrFormatImportiDTSipasTeDrejtave(int idNdermarje, int idViti, int idPerdoruesi, string komponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheFormatDT");
            return ds.Tables[0];

        }
        internal DataTable merrFormatImportiSipasKategoriseDT(int idNdermarje, int idkategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheFormatSipasNdermarjeDheKategoriseDT");
            return ds.Tables[0];
        }

        internal DataRow merrFormatImportiSipasID(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen nga db nese ekziston nje format importi sipas id-se qe i kalohet si parameter
        /// </summary>
        /// <param name="idKoka">id e kokes se formatit te importit</param>
        /// <returns>true nqs ekziston, false ne te kundert</returns>
        internal bool ekzistonFormatImportiSipasID(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ekzistonSipasId"));
            return Convert.ToBoolean(pergjigje);
        }

        internal DataRow merrFormatImportiSipasKodit(string kodi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheSipasKoditDheNdermarrjes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        public bool ekzistonFormatImportiSipasKodNdermarje(String kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_eksiston");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaVeprimeFormatImporti(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_kaVeprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        internal clsMesazh fshiFormatImporti(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFormatImportiStatus(int idKoka, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajFormatImportiKoka(out int idKoka, String kodi, String pershkrimi, int idkategori, int idndermarja, int idperdoruesi, int idstatusdok)
        {
            idKoka = -1;

            dbManager.Open();

            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ins");
            idKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoFormatImportiKoka(int idKoka, String kodi, String pershkrimi, int idkategori, int idndermarja, int idperdoruesi, int idstatusdok)
        {
            dbManager.Open();

            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajTeDhenaNeTabeleEksporti(int idNdermarrje)
        {
            dbManager.Open();

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTPAGESADEMESH_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal DataTable merrTeDhenatPerTabeleEksporti(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTPAGESADEMESH_select");
            return ds.Tables[0];
        }

        public int ktheIdKokaFormatiSipasKoditdheKategorise(int idNdermarrje, int idkategori, string kodi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODI", kodi, ParameterDirection.Input);
            object id = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFORMATIMPORTI_ktheIdSipasKoditdheKategorise"));
            if (id != null)
                return Convert.ToInt32(id);
            return -1;
        }


        public clsMesazh ruajTeDhenaNeTabeleEksportiDt(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateInsertParameters(10);
            dbManager.AddInsertParameters(0, "@IDNIVELDOKUMENT", DbType.Decimal, "IDNIVELDOKUMENT");
            dbManager.AddInsertParameters(1, "@IDDOKUMENTI", DbType.Decimal, "IDDOKUMENTI");
            dbManager.AddInsertParameters(2, "@NRDOK", DbType.String, "NRDOK");
            dbManager.AddInsertParameters(3, "@DATEDOK", DbType.DateTime, "DATEDOK");
            dbManager.AddInsertParameters(4, "@NRCESHTJE", DbType.String, "NRCESHTJE");
            dbManager.AddInsertParameters(5, "@MONEDHA", DbType.String, "MONEDHA");
            dbManager.AddInsertParameters(6, "@VLEFTAPAGUAR", DbType.Double, "VLEFTAPAGUAR");
            dbManager.AddInsertParameters(7, "@PERSHKRIMVEPRIMI", DbType.String, "PERSHKRIMVEPRIMI");
            dbManager.AddInsertParameters(8, "@FURNITOR", DbType.String, "FURNITOR");
            dbManager.AddInsertParameters(9, "@IDDOKNGA", DbType.String, "IDDOKNGA");
            dbManager.CreateParameters(0);//bejme clear koleksionin e parametrave te selektit
            dbManager.ExecuteInsert(dt, CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTPAGESADEMESH_selectPerInsert", CommandType.StoredProcedure, "prc_T_TEMP_EKSPORTPAGESADEMESH_insDt");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        #endregion

        #region TRUPI FORMAT IMPORTI

        internal DataRow merrFormatImportiTrupiSipasID(int idTrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_ktheSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrFormatImportiTrupiSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_ktheSipasIdKoka");
            return ds.Tables[0];
        }

        internal DataTable merrFormatImportiTrupiSipasIdKokaDetyrueshme(int idKoka, bool detyrueshme)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@detyrueshme", detyrueshme, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_ktheSipasIdKokaEDetyrueshme");
            return ds.Tables[0];
        }

        internal DataTable merrFormatImportiTrupiSipasIdKokaDheVisible(int idKoka, bool visible)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Visible", visible, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_ktheSipasIdKokaDheVisible");
            return ds.Tables[0];
        }

        internal clsMesazh fshiFormatImportiTrupi(int idTrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFormatImportiTrupiSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_delSipasIdKoka");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajFormatImportiTrupi(int idtrupi, int idKoka, int idkontroll, String emerimporti, string vleradefault, bool visibility, int rendi, bool detyrueshme, bool shfaq, int tipi, bool detyrueshmedefault, int fushekokeapotrupi, string fushetype)
        {
            dbManager.Open();

            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERIMPORTI", emerimporti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERADEFAULT", vleradefault, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VISIBLE", visibility, ParameterDirection.Input);
            dbManager.AddParameters(6, "@RENDI", rendi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DETYRUESHME", detyrueshme, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SHFAQ", shfaq, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DETYRUESHMEDEFAULT", detyrueshmedefault, ParameterDirection.Input);
            dbManager.AddParameters(11, "@FUSHEKOKEAPOTRUPI", fushekokeapotrupi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@FUSHETYPE", fushetype, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajFormatImportiDefault(int idndermarje, int idndermarjenga, int idperdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idndermarje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarjenga", idndermarjenga, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_KopjoDefault");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoFormatImportiTrupi(int idtrupi, int idKoka, String idkontroll, String emerimporti, string vleradefault, bool visibility, int rendi, bool detyrueshme, bool shfaq, int tipi, bool detyrueshmedefault)
        {
            dbManager.Open();

            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTRUPI", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERIMPORTI", emerimporti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERADEFAULT", vleradefault, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VISIBLE", visibility, ParameterDirection.Input);
            dbManager.AddParameters(6, "@RENDI", rendi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DETYRUESHME", detyrueshme, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SHFAQ", shfaq, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DETYRUESHMEDEFAULT", detyrueshmedefault, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIFORMATIMPORTI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        #endregion

        #region ERROR IMPORTI

        internal clsMesazh ruajErrorImportiKoka(out int idKoka, String pershkrimi, int idkategori, int idndermarja, int idperdoruesi)
        {
            idKoka = -1;
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID", idKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKATEGORIa", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAERRORIMPORTI_ins");
            idKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajErrorImportiTrupi(out int id, String gabimi, int idkoka, int rreshti, string kodi)
        {
            id = -1;
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@GABIMI", gabimi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RRESHTI", rreshti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPIERRORIMPORTI_ins");
            id = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal DataTable ktheErrorImportiSipasIdKoka(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPIERRORIMPORTI_SelSipasIdKoka");
            return ds.Tables[0];
        }

        #endregion

        #region KONFIG IMPORTI
        
        internal IEnumerable<clsKonfigImporti> merrGjitheKonfigurimImportiNdermarrjesSipasTeDrejtave(int idNdermarrje, int idViti, int idPerdoruesi, string komponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KONFIGIMPORTI_ktheNdermarrjesSipasTeDrejtave", clsKonfigImporti.Create);
        }
        public Queue<string> merrTabelatEImportit()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT EMERTABELEKOKA,EMERTABELETRUPI,EMERTABELEREC,EMERTABELEKOKA_HISTORIK,EMERTABELETRUPI_HISTORIK,EMERTABELEREC_HISTORIK FROM T_KONFIGIMPORTI";
            string connectionString = dbManager.ConnectionString;
            Queue<string> queue = new Queue<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if(reader["EMERTABELEKOKA"].ToString() != "")
                            queue.Enqueue(reader["EMERTABELEKOKA"].ToString());
                        if (reader["EMERTABELETRUPI"].ToString() != "")
                            queue.Enqueue(reader["EMERTABELETRUPI"].ToString());
                        if (reader["EMERTABELEREC"].ToString() != "")
                            queue.Enqueue(reader["EMERTABELEREC"].ToString());
                        if (reader["EMERTABELEKOKA_HISTORIK"].ToString() != "")
                            queue.Enqueue(reader["EMERTABELEKOKA_HISTORIK"].ToString());
                        if (reader["EMERTABELETRUPI_HISTORIK"].ToString() != "")
                            queue.Enqueue(reader["EMERTABELETRUPI_HISTORIK"].ToString());
                        if (reader["EMERTABELEREC_HISTORIK"].ToString() != "")
                            queue.Enqueue(reader["EMERTABELEREC_HISTORIK"].ToString());
                        //return queue;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return queue;
        }
        internal DataRow merrKonfigurimImportiSipasID(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_ktheSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow merrKonfigurimImportiSipasKodit(string kodi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMER", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_ktheSipasKoditDheNdermarrjes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        public bool ekzistonKonfigurimImportiSipasKodNdermarje(String kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMER", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_eksiston")) > 0;
        }
        
        internal void fshiKonfigurimImportiStatus(int id, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_upddel");
        }

        internal int ruajKonfigurimImportiKoka(string emer, string tipi, int idkategori, int idformati, string emersheet, int idndermarja, int idperdoruesi, int idstatusdok, string emertabkoka, string emertabtrupi, bool gjeneroFatPermb, string emerTabRec, bool transferoFatura, bool merrTePaImportuara, bool dergoMeEmail, string emerTabKokaHistorik, string emerTabTrupiHistorik, string emerTabRecHistorik, bool rimerrTeImportuara, int? nrDokumentash)
        {
            dbManager.Open();
            dbManager.CreateParameters(21);
            dbManager.AddParameters(0, "@ID", -1, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMER", emer, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KATEGORIA", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FORMATI", idformati, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERSHEET", emersheet, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@EMERTABELEKOKA", emertabkoka, ParameterDirection.Input);
            dbManager.AddParameters(10, "@EMERTABELETRUPI", emertabtrupi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@GJENEROFATUREPERMB", gjeneroFatPermb, ParameterDirection.Input);
            dbManager.AddParameters(12, "@EMERTABELEREC", emerTabRec, ParameterDirection.Input);
            dbManager.AddParameters(13, "@TRANSFEROFATURA", transferoFatura, ParameterDirection.Input);
            dbManager.AddParameters(14, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(15, "@DERGOMEEMAIL", dergoMeEmail, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMERTABELEKOKA_HISTORIK", emerTabKokaHistorik, ParameterDirection.Input);
            dbManager.AddParameters(17, "@EMERTABELETRUPI_HISTORIK", emerTabTrupiHistorik, ParameterDirection.Input);
            dbManager.AddParameters(18, "@EMERTABELEREC_HISTORIK", emerTabRecHistorik, ParameterDirection.Input);
            dbManager.AddParameters(19, "@RIMERRTEIMPORTUARA", rimerrTeImportuara, ParameterDirection.Input);
            dbManager.AddParameters(20, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_ins");

            return int.Parse(dbManager.Parameters[0].Value.ToString());
        }

        internal void modifikoKonfigurimImportiKoka(int id, string emer, string tipi, int idkategori, int idformati, string emersheet, int idndermarja, int idperdoruesi, int idstatusdok, string emertabkoka, string emertabtrupi, bool gjeneroFatPermb, string emerTabRec, bool transferoFatura, bool merrTePaImportuara, bool dergoMeEmail, string emerTabKokaHistorik, string emerTabTrupiHistorik, string emerTabRecHistorik, bool rimerrTeImportuara, int? nrDokumentash)
        {
            dbManager.Open();
            dbManager.CreateParameters(21);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMER", emer, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KATEGORIA", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FORMATI", idformati, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERSHEET", emersheet, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@EMERTABELEKOKA", emertabkoka, ParameterDirection.Input);
            dbManager.AddParameters(10, "@EMERTABELETRUPI", emertabtrupi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@GJENEROFATUREPERMB", gjeneroFatPermb, ParameterDirection.Input);
            dbManager.AddParameters(12, "@EMERTABELEREC", emerTabRec, ParameterDirection.Input);
            dbManager.AddParameters(13, "@TRANSFEROFATURA", transferoFatura, ParameterDirection.Input);
            dbManager.AddParameters(14, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(15, "@DERGOMEEMAIL", dergoMeEmail, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMERTABELEKOKA_HISTORIK", emerTabKokaHistorik, ParameterDirection.Input);
            dbManager.AddParameters(17, "@EMERTABELETRUPI_HISTORIK", emerTabTrupiHistorik, ParameterDirection.Input);
            dbManager.AddParameters(18, "@EMERTABELEREC_HISTORIK", emerTabRecHistorik, ParameterDirection.Input);
            dbManager.AddParameters(19, "@RIMERRTEIMPORTUARA", rimerrTeImportuara, ParameterDirection.Input);
            dbManager.AddParameters(20, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_upd");
        }


        internal void merrKonfigurimEmalSipasKonfigImporti(int idKonfigImporti, IDataBaseReader objekt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTEMPLATEIMPORTI", idKonfigImporti, ParameterDirection.Input);
            dbManager.FillCollection("PRC_T_KONFIGIMPORTI_EMAILKONFIG_selSipasIdKonfigImporti", objekt);
        }

        internal bool kaKonfigurimEmalPerKonfigImporti(int idKonfigImporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTEMPLATEIMPORTI", idKonfigImporti, ParameterDirection.Input);
            object kaEmail = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGIMPORTI_ekzistonKonfigEmail");
            return !(kaEmail == null);
        }
        internal clsMesazh ruajKonfigurimeEmailImportDT(DataTable dt)
        {
            try
            {
                dbManager.Open();
                dbManager.CreateParameters(1);
                dbManager.AddParameters(0, "@TABELA", dt, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_KONFIGIMPORTI_EMAILKONFIG_insDT");
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception e)
            {
                return new clsMesazh(false, e.Message);
            }
        }

        public DataTable merrStatusDheEmailSipasKonfigImporti(int idKonfigImporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTEMPLATEIMPORTI", idKonfigImporti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_KONFIGIMPORTI_EMAILKONFIG_ktheEmaileSipasIdtemplati");
            return ds.Tables[0];
        }

        internal bool ekzistonStoredProcedure(string emerSP)
        {
            bool ekziston = false;
            try
            {
                string query = "select COUNT(*) from sysobjects where type='P' and name='" + emerSP + "'";
                dbManager.Open();
                ekziston = Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.Text, query));
                return ekziston;
            }
            catch
            {
                ekziston = false;
                return ekziston;
            }
        }

        #endregion

        #region KONFIG EXPORTI
        
        internal IEnumerable<clsKonfigExporti> MerrGjitheKonfigurimExportiNdermarrjesSipasTeDrejtave(int idNdermarrje, int idViti, int idPerdoruesi, string komponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_KONFIGEXPORTI_ktheNdermarrjesSipasTeDrejtave", clsKonfigExporti.Create);
        }

        internal DataRow merrKonfigurimExportiSipasID(int id)
        {



            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGEXPORTI_ktheSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }

        internal DataRow merrKonfigurimExportiSipasKodit(string kodi, int idNdermarrje)
        {



            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMER", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGEXPORTI_ktheSipasKoditDheNdermarrjes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        public bool ekzistonKonfigurimExportiSipasKodNdermarje(string kodi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@EMER", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGEXPORTI_eksiston")) > 0;
        }

        internal void fshiKonfigurimExportiStatus(int id, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGEXPORTI_upddel");
        }

        internal int ruajKonfigurimExportiKoka(string emer, string tipi, int idkategori, int idformati, string emersheet, string emerskedari, int idfiltri, int idndermarja, int idperdoruesi, int idstatusdok, DateTime datePerFiltrim, string formatDest, string urlDest, string ndermDest, string emerTabKoka, string emerTabTrupi, int lloji, string emerTabRec, bool merrDokTeModifikuar, bool merrDokTeFshire)
        {
            dbManager.Open();
            dbManager.CreateParameters(21);
            dbManager.AddParameters(0, "@ID", -1, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMER", emer, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KATEGORIA", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FORMATI", idformati, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERSHEET", emersheet, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@EMERSKEDARI", emerskedari, ParameterDirection.Input);
            if (idfiltri == 0)
                dbManager.AddParameters(10, "@FILTRI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(10, "@FILTRI", idfiltri, ParameterDirection.Input);
            if (datePerFiltrim == DateTime.MinValue)
                dbManager.AddParameters(11, "@DATEPERFILTRIM", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(11, "@DATEPERFILTRIM", datePerFiltrim, ParameterDirection.Input);
            dbManager.AddParameters(12, "@FORMATDESTINACION", formatDest, ParameterDirection.Input);
            dbManager.AddParameters(13, "@URLDESTINACION", urlDest, ParameterDirection.Input);
            dbManager.AddParameters(14, "@NDERMARRJEDESTINACION", ndermDest, ParameterDirection.Input);
            dbManager.AddParameters(15, "@EMERTABELEKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMERTABELETRUPI", emerTabTrupi, ParameterDirection.Input);
            dbManager.AddParameters(17, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(18, "@EMERTABELEREC", emerTabRec, ParameterDirection.Input);
            dbManager.AddParameters(19, "@MERRDOKTEMODIFIKUAR", merrDokTeModifikuar, ParameterDirection.Input);
            dbManager.AddParameters(20, "@MERRDOKTEFSHIRE", merrDokTeFshire, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGEXPORTI_ins");

            return int.Parse(dbManager.Parameters[0].Value.ToString());
        }

        /// <summary>
        /// Funksion qe perdoret per te krijuar tabela ne sql. Merr si parameter skriptin e krijimit te tabelave.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal void krijoObjektSql(StringBuilder query)
        {
            dbManager.Open();
            dbManager.ClearParameters();
            dbManager.ExecuteNonQuery(CommandType.Text, query.ToString());
        }

        internal void modifikoKonfigurimExportiKoka(int id, string emer, string tipi, int idkategori, int idformati, string emersheet, string emerskedari, int idfiltri, int idndermarja, int idperdoruesi, int idstatusdok, DateTime datePerFiltrim, string formatDest, string urlDest, string ndermDest, string emerTabKoka, string emerTabTrupi, int lloji, string emerTabRec, bool merrDokTeModifikuar, bool merrDokTeFshire)
        {
            dbManager.Open();
            dbManager.CreateParameters(21);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EMER", emer, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TIPI", tipi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KATEGORIA", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FORMATI", idformati, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERSHEET", emersheet, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNDERMARJE", idndermarja, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(9, "@EMERSKEDARI", emerskedari, ParameterDirection.Input);
            if (idfiltri == 0)
                dbManager.AddParameters(10, "@FILTRI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(10, "@FILTRI", idfiltri, ParameterDirection.Input);
            if (datePerFiltrim == DateTime.MinValue)
                dbManager.AddParameters(11, "@DATEPERFILTRIM", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(11, "@DATEPERFILTRIM", datePerFiltrim, ParameterDirection.Input);
            dbManager.AddParameters(12, "@FORMATDESTINACION", formatDest, ParameterDirection.Input);
            dbManager.AddParameters(13, "@URLDESTINACION", urlDest, ParameterDirection.Input);
            dbManager.AddParameters(14, "@NDERMARRJEDESTINACION", ndermDest, ParameterDirection.Input);
            dbManager.AddParameters(15, "@EMERTABELEKOKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(16, "@EMERTABELETRUPI", emerTabTrupi, ParameterDirection.Input);
            dbManager.AddParameters(17, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(18, "@EMERTABELEREC", emerTabRec, ParameterDirection.Input);
            dbManager.AddParameters(19, "@MERRDOKTEMODIFIKUAR", merrDokTeModifikuar, ParameterDirection.Input);
            dbManager.AddParameters(20, "@MERRDOKTEFSHIRE", merrDokTeFshire, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGEXPORTI_upd");
        }

        #endregion

        #region FILTRAT  E EXPORTIT

        internal clsMesazh ruajFiltraExporti(int idfiltri, string filtrakodi, int formati, int idperdoruesi, int idnderm, int idstatusdok, string pershkrimi, string filterPerDataSet)
        { //metoda per ruajtjen e filtrit


            if (!ekzistonFilterExporti(filtrakodi, idnderm, formati))
            {
                dbManager.Open();
                //shtimi i parametrave
                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@ID", idfiltri, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODI", filtrakodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
                dbManager.AddParameters(3, "@FORMATI", formati, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
                dbManager.AddParameters(7, "@FILTERPERDATASET", filterPerDataSet, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_ins");
                clsMesazh mesazh = new clsMesazh(true, "Filtri per kete gride u ruajt me sukses!");
                return mesazh;
            }
            else
            {
                return new clsMesazh(false, "Ekziston nje filter me te njejtin kod!");
            }

        }

        internal clsMesazh modifikoFiltraExporti(int idfiltri, string filtrakodi, int formati, int idperdoruesi, int idnderm, int idstatusdok, string pershkrimi, string filterPerDataSet)
        {//metoda per modifikimin i filtrave

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", idfiltri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", filtrakodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FORMATI", formati, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@FILTERPERDATASET", filterPerDataSet, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        public clsMesazh fshiFiltraExporti(int idfiltri)
        {//metoda per fshirjen e filtrave


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idfiltri, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        public clsMesazh fshiFiltraExportiStatus(int id, int idperdorues)
        {//metoda per fshirjen e filtrave

            //{
            //    dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //    dbManager.ConnectionString = dbManager.GetConnectionString();
            //}

            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        internal DataRow ktheFiltraExportiSipasId(int idFiltra)
        {
            //{//metoda per te marre te filtrin sipas kodit
            //    if (dbManager == null)
            //    {
            //        dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //        dbManager.ConnectionString = dbManager.GetConnectionString();
            //    }

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idFiltra, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_merrFiltraGridaSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        internal DataTable ktheGjitheFiltratExportiSipasFormatit(int formati, int idnderm)
        {//metoda per te marre te gjithe filtrat


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@FORMATI", formati, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_merrGjitheSipasFormatit");
            return ds.Tables[0];

        }

        internal DataRow ktheFiltraExportiSipasFiltraKodi(string filtrakodi, int idnderm, int formati)
        {//metoda per te marre te filtrin sipas kodit


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", filtrakodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FORMATI", formati, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_merrSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Kthen nese ekziston nje filter tjeter me te njejtin kod per griden tek lupat
        /// </summary>
        /// <param name="filtriKodi">kodi i filtrit</param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <param name="idkoka">id e grides</param>
        /// <returns>True nese ekziston filter tjeter me kete kod; False ne rast te kundert</returns>
        internal bool ekzistonFilterExporti(String filtriKodi, int idnder, int formati)
        {
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //dbManager.ConnectionString = dbManager.GetConnectionString();

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", filtriKodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FORMATI", formati, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_ekzistonFilter"));
            return Convert.ToBoolean(pergjigje);

        }
        internal bool kaVeprimeFilterExporti(int id)
        {
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //dbManager.ConnectionString = dbManager.GetConnectionString();
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KOKAFILTRAEXPORTI_kaVeprime"));
            return Convert.ToBoolean(pergjigje);

        }

        internal String ktheFilterPerDataSetSipasId(int id)
        {
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //dbManager.ConnectionString = dbManager.GetConnectionString();

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            String ds = dbManager.ExecuteScalar(CommandType.StoredProcedure, "T_KOKAFILTRAEXPORTI_ktheFilterPerDataSetSipasId").ToString();
            if (String.IsNullOrEmpty(ds))
                return "";
            return ds;
        }

        #endregion

        #region FILTRA GIS
        internal clsMesazh ruajFiltraGIS(int Id, string Kodi, string IdLayerType, int IdPerdoruesi, int IdNdermarje, int idStatusDok, string Pershkrimi, string filterExpression)
        {
            if (!ekzistonFilterGIS(Kodi, IdNdermarje, IdLayerType))
            {
                dbManager.Open();
                dbManager.CreateParameters(8);
                dbManager.AddParameters(0, "@ID", Id, ParameterDirection.Output);
                dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
                dbManager.AddParameters(2, "@PERSHKRIMI", DBNull.Value, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDLAYERTYPE", IdLayerType, ParameterDirection.Input);
                dbManager.AddParameters(4, "@IDPERDORUESI", IdPerdoruesi, ParameterDirection.Input);
                dbManager.AddParameters(5, "@IDNDERMARJE", IdNdermarje, ParameterDirection.Input);
                dbManager.AddParameters(6, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
                dbManager.AddParameters(7, "@FILTERPERDATASET", filterExpression, ParameterDirection.Input);
                dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_FILTRAGRIDA_ins");
                clsMesazh mesazh = new clsMesazh(true, "Filtri per kete gride u ruajt me sukses!");
                return mesazh;
            }
            else
            {
                return new clsMesazh(false, "Ekziston nje filter me te njejtin kod!");
            }
        }
        internal clsMesazh modifikoFiltraGIS(int Id, string Kodi, string IdLayerType, int IdPerdoruesi, int IdNdermarje, int idStatusDok, string Pershkrimi, string filterExpression)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@ID", Id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLAYERTYPE", IdLayerType, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", IdPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARJE", IdNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERSHKRIMI", Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@FILTERPERDATASET", filterExpression, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_FILTRAGRIDA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        internal DataRow ktheFiltraGisSipasID(int idFiltra)
        {
            throw new NotImplementedException();
        }
        internal string ktheFilterPerGISSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_FILTRAGRIDA_ktheFilterSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return Convert.ToString(ds.Tables[0].Rows[0]["FILTERPERDATASET"]);
        }
        internal bool ekzistonFilterGIS(string kodi, int idNderm, string idLayerType)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLAYERTYPE", idLayerType, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_GIS_App_FILTRAGRIDA_exists"));
            return Convert.ToBoolean(pergjigje);
        }
        internal DataRow ktheFiltraGISSipasFiltraKodi(string filtrakodi, int idnderm, int idLayerType)
        {
            throw new NotImplementedException();
        }
        internal clsMesazh fshiFilterGisStatus(int Id, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", Id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_FILTRAGRIDA_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }
        internal DataTable ktheGjitheFiltratGISSipasLayerit(string idLayerType, int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLAYERTYPE", idLayerType, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_FILTRAGRIDA_selAllSipasIDLAYERTYPE");
            return ds.Tables[0];
        }
        #endregion

        #region KONTROLLE GRIDA

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idKomponente"></param>
        /// <returns></returns>
        internal DataTable merrKontrolletSipasKategorise(int idkatdok)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATDOK", idkatdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLEGRIDA_ktheKontrolletKategorie");
            return ds.Tables[0];

        }


        /// <summary>
        /// Lexon nga db-ja kontrollin me id idkontroll
        /// </summary>
        /// <param name="idkontroll">id-ja e kontrollit qe do lexohet</param>
        /// <returns>Kthen DataRow me te dhenat e kontrollit, null perndryshe</returns>
        internal DataRow merrKontrollin(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLEGRIDA_ktheKontroll");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }


        /// <summary>
        /// Lexon nga db-ja kontrollin me kodin kodKontroll dhe me idKomponente te dhene
        /// </summary>
        /// <param name="idkontroll">kodi i kontrollit qe do lexohet</param>
        /// <param name="idKomponente">id e komponentes se kontrollit qe do lexohet</param>
        /// <returns>Kthen DataRow me te dhenat e kontrollit, null perndryshe</returns>
        internal DataRow merrKontrollinSipasKodit(string kodi, int idkatdok)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idkatdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLEGRIDA_ktheKontrollSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen per ruajtjen e veprimeve te klienteve ne tabelen T_LOGU
        /// </summary>
        #region LOGU

        internal clsMesazh ruajLogun(out int idlogu, int idkomponente, int idndermarje, int idperdorues, DateTime dtveprimi, string iddokregj, int idfature)
        {
            idlogu = -1;


            dbManager.Open();

            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDLOGU", idlogu, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DTVEPRIMI", dtveprimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDDOKREGJ", iddokregj, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDFATURE", idfature, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LOGU_ins");
            idlogu = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        #endregion

        /// <summary>
        /// Metodat e meposhtme sherbejne per ruajtjen e veprimeve te perdoruesve qe kryhen ne ambjentin e rivleresimit te magazines ne tabelen T_LOGRIVLERESIMINVENTARI
        /// </summary>
        #region T_LOGRIVLERESIMINVENTARI

        public clsMesazh ruajLogunPerRivleresiminEInventarit(int idPerdoruesi, int idNdermarje, DateTime startTime, long idStartTime, int nrArtikujve, int nrMagazinave, DateTime periudhaNga, DateTime periudhaDeri)
        {

            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@STARTTIME", startTime, ParameterDirection.Input);
            //dbManager.AddParameters(4, "@STOPTIME", stopTime, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTARTTIME", idStartTime, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRARTIKUJVE", nrArtikujve, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRMAGAZINAVE", nrMagazinave, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERIUDHANGA", periudhaNga, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PERIUDHADERI", periudhaDeri, ParameterDirection.Input);
            dbManager.AddParameters(8, "@ARSYESTOPIMI", "", ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LOGRIVLERESIMINVENTARI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }
        public String getLastArtRivleresim(int idPerdoruesi, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            object obj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LOGRIVLERESIMINVENTARI_selLastArt");
            if (obj == null)
                return "";
            return obj.ToString();
        }
        public clsMesazh setLastArtRivleresim(long idStartTime, String kodArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSTARTTIME", idStartTime, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LOGRIVLERESIMINVENTARI_setLastArt");
            return new clsMesazh(true, "Artikulli i fundit per rivleresim u update me sukses");

        }
        /// <summary>
        /// modifikohet stop time per rivleresimin
        /// </summary>
        /// <param name="idStartTime">id unike e cila sherben per te nenkuptuar se per cilin startim rivleresimi do te modifikohet data/ora e stopimit</param>
        /// <param name="stopTime">data/ora e stopimit te rivleresimit</param>
        /// <returns></returns>
        public clsMesazh modifikoStopTime(long idStartTime, DateTime stopTime, string arsyeStopimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSTARTTIME", idStartTime, ParameterDirection.Input);
            dbManager.AddParameters(1, "@STOPTIME", stopTime, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ARSYESTOPIMI", arsyeStopimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LOGRIVLERESIMINVENTARI_updStopTime");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKategoriNrAuto dhe colKategoriNrAuto
        /// </summary>
        #region KATEGORI Nr Auto


        /// <summary>
        /// kthen objektin kategori nivel dokumenti sipas pershkrimit, mqs presupozohet qe pershkrimi edhe unik ne bashkesine e kategorive 
        /// </summary>
        /// <param name="pershk">pershkrimi i kategorise</param>
        ///<returns>nje datarow kategorite qe kane kete pershkrim </returns>
        internal DataRow ktheKategoriSipasPershkrimit(string pershk)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PERSHKRIMI", pershk, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORINRAUTO_selSipasKodi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen objektin kategori nivel dokumenti sipas idse
        /// </summary>
        /// <param name="idKategori">id e kategorise</param>
        ///<returns>nje datarow kategori qe kane kete id por pa plotesuar colNivelRegjistrimi qe ka si atribut</returns>
        internal DataRow mbushKategoriSipasId(int idKategori)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idKategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORINRAUTO_selSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen objektet kategori nivel dokumenti sipas ndermarjes dhe vitit
        /// </summary>
        /// <param name="idNdermVit"> id ndermarje viti </param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha kategorite qe bejne pjese ne kete ndermarje vit me kategorine (...)por pa plotesuar colNivelRegjistrimi qe ka si atribut</returns>
        internal DataTable ktheGjitheKategori()
        {


            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORINRAUTO_selAll");
            return ds.Tables[0];

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsTrupiSkemaWorkFlow dhe colTrupiSkemaWorkFlow
        /// </summary>
        #region TRUPI SKEMA WORK FLOW

        internal clsMesazh ruajTrupiSkemaWorkFlow(out int idTrupi, int idkoka, int lloji, int idperdrol, int niveli, double vleralimit, bool modifiko, int delegimi, int niveliapr, int llojgrupikf, decimal dite, string pershkrimi)
        {
            dbManager.Open();
            idTrupi = 0;

            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDROL", idperdrol, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERALIMIT", vleralimit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@MODIFIKO", modifiko, ParameterDirection.Input);
            if (delegimi <= 0) dbManager.AddParameters(7, "@IDDELEGIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDDELEGIMI", delegimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NIVELIAPROV", niveliapr, ParameterDirection.Input);
            dbManager.AddParameters(9, "@LLOJGRUPIKF", llojgrupikf, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DITE", dite, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_ins");
            idTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);




        }


        internal clsMesazh modifikoTrupiSkemaWorkFlow(int idTrupi, int idkoka, int lloji, int idperdrol, int niveli, double vleralimit, bool modifiko, int delegimi, int niveliapr, int llojgrupikf, decimal dite, string pershkrimi)
        {//metoda per modifikimin e TRUPIT KategoriZbritje

            dbManager.Open();


            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDROL", idperdrol, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VLERALIMIT", vleralimit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@MODIFIKO", modifiko, ParameterDirection.Input);
            if (delegimi <= 0) dbManager.AddParameters(7, "@IDDELEGIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDDELEGIMI", delegimi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@NIVELIAPROV", niveliapr, ParameterDirection.Input);
            dbManager.AddParameters(9, "@LLOJGRUPIKF", llojgrupikf, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DITE", dite, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPISKEMAWORKFLOW_del duke i kaluar id e trupit te SKEMA WORKFLOW 
        /// </summary>
        /// <param name="idTrupi"> id ritese e trupit </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiSkemaWorkFlow(int idTrupi)
        {//metoda per fshirjen e TRUPIT KategoriZbritje


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        /// <summary>
        /// ekzekutohet sp-ja prc_T_TRUPISKEMAWORKFLOW_del duke i kaluar id e kokes te SKEMA WORKFLOW 
        /// </summary>
        /// <param name="idKoka"> id kokes </param>
        /// <returns>nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiTrupiSkemaWorkFlowSipasKokes(int idKoka)
        {//metoda per fshirjen e TRUPIT KategoriZbritje


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_delSipasKokes");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;


        }

        /// <summary>
        /// kthen objektet trupi SKEMA WORKFLOW sipas idse
        /// </summary>
        ///<param name="idTrupi">trupi  qe do i merret id</param>
        internal DataRow merrTrupiSkemaWorkFlow(int idTrupi)
        {// metoda per te marre nje KategoriZbritje NE BAZE TE ID


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        /// <summary>
        /// kthen datarow trupi skema workflow sipas idse
        /// </summary>
        ///<param name="idTrupi"> id e trupit </param>
        ///<returns> nje datarow qe permban trupin me kete id</returns>
        internal DataRow merrTrupiSkemaWorkFlowSipasId(int idTrupi)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPI", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_ktheTrupSkemaWorkFlowSipasId");


            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datatable  trupi skema workflow sipas idse se kokes
        /// </summary>
        ///<param name="idKoka"> id e kokes </param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe trupat me kete id koke</returns>
        internal DataTable ktheTrupiSkemaWorkFlowSipasKokes(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_ktheTrupSipasKokes");
            return ds.Tables[0];

        }
        /// <summary>
        /// kthen datatable  trupi skema workflow sipas idse se kokes
        /// </summary>
        ///<param name="idKoka"> id e kokes </param>
        ///<returns> nje datatable qe permban nje koleksion me te gjithe trupat me kete id koke</returns>
        internal DataRow ktheTrupiSkemaWorkFlowSipasKokesDhePerdoruesit(int idKoka, int idperdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_merrTrupinSipasKokesDhePerdoruesit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheTrupiSkemaWorkFlowSipasKokesDhePerdoruesitDheNivelit(int idKoka, int idperdoruesi, int niveli)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@niveli", niveli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_merrTrupinSipasKokesDhePerdoruesitDheNivelit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataRow ktheTrupiSkemaWorkFlowSipasKokesDheDeleguesi(int idKoka, int idperdoruesi)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idperdoruesi", idperdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TRUPISKEMAWORKFLOW_merrTrupinSipasKokesDheDeleguesi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }
        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsKokaSkemaWorkFlow dhe colKokaSkemaWorkFlow
        /// </summary>
        #region KOKA Skema WorkFlow

        /// <summary>
        /// ekzekuton prc_T_KOKASKEMAWORKFLOW_ins ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// <param name="idKoka"> id ritese e kokes </param>
        /// <param name="kodi"> kod </param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="formula"> formula 1 javore,2 javore ose mujore</param>
        /// <param name="idkonfig">id konfig</param>
        /// <param name="idstatusdok">id e statusit te dokumentit</param>
        /// <param name="njoftim">njoftim me email ose jo</param>
        /// <returns> nje objekt boolean qe tregon nese ruajtja eshte kryer ne rregull apo jo.</returns>
        /// </summary>
        internal int ruajKokaSkemaWorkFlow(int idKoka, string kodi, string emertimi, int idPerdoruesi, bool njoftim, int formula, int idnderm, int idstatusdok, int idkonfig, int nr, bool dergoemailpasaprovimitfinal, string emailaprovimi)
        {//ruajtja e kokamakro

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NJOFTIM", njoftim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@FORMULA", formula, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NR", nr, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DERGOEMAILPASAPROVIMITFINAL", dergoemailpasaprovimitfinal, ParameterDirection.Input);
            dbManager.AddParameters(11, "@EMAILAPROVIMI", emailaprovimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_ins");
            idKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return idKoka;


        }

        /// <summary>
        /// ekzekuton prc_T_KOKASKEMAWORKFLOW_upd ne DB dhe kthen statusin e perfundimit te ekzekutimit te SP-se perkatese
        /// Shiko clsMesazh brenda ketij moduli per informacion te metejshem mbi vleren e kthimit te kesaj metode
        /// </summary>
        /// <param name="idKoka"> id ritese e kokes </param>
        /// <param name="kodi"> kod </param>
        /// <param name="emertimi"> emertimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="formula"> formula 1 javore,2 javore ose mujore</param>
        /// <param name="idkonfig">id konfig</param>
        /// <param name="idstatusdok">id e statusit te dokumentit</param>
        /// <param name="njoftim">njoftim me email ose jo</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo.</returns>

        internal clsMesazh modifikoKokaSkemaWorkFlow(int idKoka, string kodi, string emertimi, int idPerdoruesi, bool njoftim, int formula, int idnderm, int idstatusdok, int idkonfig, int nr, bool dergoemailpasaprovimitfinal, string emailaprovimi)
        {//modifikimi i kokamakro

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NJOFTIM", njoftim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDKONFIG", idkonfig, ParameterDirection.Input);
            dbManager.AddParameters(8, "@FORMULA", formula, ParameterDirection.Input);
            dbManager.AddParameters(9, "@NR", nr, ParameterDirection.Input);
            dbManager.AddParameters(10, "@DERGOEMAILPASAPROVIMITFINAL", dergoemailpasaprovimitfinal, ParameterDirection.Input);
            dbManager.AddParameters(11, "@EMAILAPROVIMI", emailaprovimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_upd");
            idKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// ekzekutohet sp-ja prc_T_KOKASKEMAWORKFLOW_del duke i kaluar id e kokes te kategori zbritje qe e marrim nga objekti clsKokaKategoriZbritje qe i kalohet si parameter
        /// </summary>
        /// <param name="idKoka"> id ritese e kokes </param>
        /// <returns>nje objekt boolean qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        internal clsMesazh fshiKokaSkemaWorkFlow(int idKoka)
        {//fshirja e KokaKategoriZbritje

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiKokaSkemaWorkFlowStatus(int idKoka, int idperdoruesi)
        {//fshirja e KokaKategoriZbritje


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// kthen objektet KOKA SKEMA sipas idse
        /// </summary>
        /// <param name="idKoka"> id ritese e kokes </param>
        internal DataRow merrKokaSkemaWorkFlow(int idKoka)
        {// metoda per te marre nje KokaKategoriZbritje ne baze te id te tij


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_sel");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen datatable koka SKEMA workflow sipas id ndermarjes
        /// </summary>
        ///<param name="idnderm">id e ndermarjes</param>
        ///<returns>nje datatable qe permban nje koleksion me te gjitha kokat e skema workflow te kesaj ndermarje  </returns>
        internal DataTable ktheKokaSkemaWorkFlowSipasNdermarrjes(int idnderm)
        {


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_merrSipasNdermarrjes");

            return ds.Tables[0];

        }

        /// <summary>
        /// kthen datarow  koka skema work flow sipas idse
        /// </summary>
        ///<param name="id"> id e kokes </param>
        ///<returns> nje datarow qe permban koken skema work flow sipas id-se</returns>
        internal DataRow merrKokaSkemaWorkFlowSipasId(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_ktheSkemaWorkflowSipasId");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];


        }

        /// <summary>
        /// kthen datarow  koka skema workflow sipas kodit
        /// </summary>
        ///<param name="kod"> kodi i kokes </param>
        ///<param name="idnder"> id e ndermarrjes</param>
        ///<returns> nje datarow qe permban koken e skema workflow me kete kod dhe id ndermarrje</returns>
        internal int merrIdKokaSkemaWorkFlowSipasKodit(string kod, int idnder)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_ktheSkemaWorkflowSipasKodit");

            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idKoka;
            int.TryParse(ds.Tables[0].Rows[0]["IDKOKA"].ToString(), out idKoka);
            return idKoka;


        }

        /// <summary>
        /// perdoret per te kontrolluar nqs ekziston nje SKEMA WOrkflow me kete kod ne kete ndermarje
        /// behet kontrolli per qellime konsistence te informacionit ne DB, nuk lejon te celen skema workflow te ndryshem me te njejtin kod
        /// </summary>
        ///<param name="idndermarje"> id e ndermarjes</param>
        ///<param name="kod"> kodi </param>
        /// <returns>nje objekt boolean qe tregon nese ekziston apo jo nje skema workflow me kete kod</returns>
        public bool ekzistonKokaSkemaWorkFlow(String kod, int idndermarje)
        {//kontrollon nqs ekziston nje KategoriZbritje me kete kod


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_ekziston");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }
        public bool kaDokumentaPerAprovimSkemaWorkFlow(int idkoka)
        {//kontrollon nqs ekziston nje KategoriZbritje me kete kod


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idkoka", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_kaDokumentaPerAprovim");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        internal DataRow merrSipasKokaSkemaWorkFlowDR(int idkoka)
        {




            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_merrSkemaWorkflowNdermarrjesDR");

            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        internal DataTable merrKokaSkemaWorkFlowNdermarrjesDT(int idnderm)
        {




            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_merrSkemaWorkFlowNdermarrjesDT");

            return ds.Tables[0];

        }

        #endregion

        /// <summary>
        /// kthen autorizimet e artikullit te ndara me presje
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <returns></returns>
        internal string merrAutorizimeArt(int idArtikulli) //TO CHECK SENADA - KJO NUK PUNON NUK I KTHEN NE RREGULL
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idArtikulli", idArtikulli, ParameterDirection.Input);
            object autorizimeObj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizimeArt");
            if (autorizimeObj == null)
                return "";
            else
            {
                string autorizime = autorizimeObj.ToString();
                return autorizime = autorizime.IndexOf(',') == -1 ? autorizime : autorizime.Substring(0, autorizime.Length - 1);
            }
        }

        /// <summary>
        /// kthen autorizimet e artikullit te ndara me presje
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <returns></returns>
        internal string merrAutorizimeSipasIdLidheseDheLloj(int idLidhese, string kodLloj,int idPerdorues)
        {


            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDLIDHESE", idLidhese, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODLLOJBUXHETI", kodLloj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            string autorizime = Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AUTORIZIMIKOKA_ktheAutorizimeSipasIdLlojDheIdLidhese"));
            autorizime = autorizime.IndexOf(',') == -1 ? autorizime : autorizime.Substring(0, autorizime.Length - 1);
            return autorizime;
        }

        internal DataTable merrAprovuesDokumenti(int idfatura)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFATURA", idfatura, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOKASKEMAWORKFLOW_merrAprovuesDokumenti");

            return ds.Tables[0];
        }
        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupeKFPerWorkFlow dhe colGrupeKFPerWorkFlow
        /// </summary>
        #region GRUPEKF PER WORKFLOW

        /// <summary>
        /// Ekzekuton prc_T_GRUPKFPERWORKFLOW_ins per te ruajtur nje objekt  ne DB.    
        /// </summary>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>

        internal clsMesazh ruajGrupeKfperWorkflow(out int id, int idtrupi, int idgrupi)
        {//ruajtja e kontaktit per klient furnitorin 
            id = -1;

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDTRUPISKEMAWORKFLOW", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGRUPKF", idgrupi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_ins");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }


        /// <summary>
        /// Ekzekuton prc_T_GRUPKFPERWORKFLOW_upd per te 
        /// nje objekt 
        /// </summary>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>

        internal clsMesazh modifikoGrupeKfperWorkflow(int id, int idtrupi, int idgrupi)
        {//modifikimi e kontaktit per klient furnitorin


            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDTRUPISKEMAWORKFLOW", idtrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGRUPKF", idgrupi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKFPERWORKFLOW_del per te fshire nje objekt .   
        /// </summary>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>

        internal clsMesazh fshiGrupeKfperWorkflow(int id)
        {//fshirja e kontaktit per klient furnitorin


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_del");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }
        internal clsMesazh fshiGrupeKfperWorkflowSipasIdTrupi(int idtrupi)
        {//fshirja e kontaktit per klient furnitorin

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPISKEMAWORKFLOW", idtrupi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_delSipasIdTrupi");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }
        internal clsMesazh fshiGrupeKfperWorkflowSipasIdKoka(int idkoka)
        {//fshirja e kontaktit per klient furnitorin


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_delSipasIdKoka");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPKFPERWORKFLOW_sel per te marre nje objekt 
        /// </summary>
        internal void merrGrupeKfperWorkFlow(int id)
        {// metoda per te marre nje kontaktin ne base te id


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_sel");

        }



        /// <summary>
        /// Ekzekuton prc_T_GRUPKFPERWORKFLOW_merrSipasIdTrupiSkema 
        /// <param name="id">ID e trupi</param>
        /// </summary>
        internal DataTable ktheGrupeKfPerWorkFlowSipasIdTrupi(int id)
        {//metoda per te marre te gjithe kontaktet sipas idklientfurnitor


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPISKEMAWORKFLOW", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPKFPERWORKFLOW_merrSipasIdTrupiSkema");
            return ds.Tables[0];

        }


        #endregion

        /// <summary>
        /// Metodat e meposhtme i perkasin veprimeve qe kryhen nga objektet clsGrupNdermarrje dhe colGrupNdermarrje
        /// </summary>
        #region GRUPE NDERMARRJE

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_ins per te ruajtur nje objekt clsGrupNdermarje ne DB.
        /// </summary>

        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>

        internal clsMesazh ruajGrupNdermarrje(int id, string kodi, string pershkrimi, int idperdoruesi, int idkrijuesi, int idlicenca, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_upd per te modifikuar nje objekt clsGrupNdermarrje ne DB.
        /// </summary>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>

        internal clsMesazh modifikoGrupNdermarrje(int id, string kodi, string pershkrimi, int idperdoruesi, int idkrijuesi, int idlicenca, int idstatusdok)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_del per te fshire nje objekt clsGrupBanke ne DB.  
        /// </summary>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te SP-se perkatese (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>

        internal clsMesazh fshiGrupNdermarrje(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }
        internal clsMesazh fshiGrupNdermarrjeStatus(int id, int idperdorues)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;

        }


        internal void merrGrupNdermarrje(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_sel");

        }
        /// <summary>
        /// Merr gjithe objektet clsGrupNdermarje ne DB.
        /// <param name="idlicence"> id e licence</param>
        /// <returns> Kthen nje collection me objekte clsGrupNdermarje</returns>
        /// </summary>
        internal DataTable ktheGjitheGrupetNdermarrjeSipasLicences(int idlicence)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLICENCA", idlicence, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_merrGrupSipasLicences");

            return ds.Tables[0];


        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_merrGrupArkeBankeSipasKodit per te marre nje objekt clsGrupNdermarrje ne DB duke filtruar .
        /// </summary>
        ///<param name="kodi">Numri(kodi) i grupit te bankes</param>
        ///<param name="idlicenca">id e idlicenca</param>
        /// <returns> Kthen nje collection me objekte clsGrupNdermarrje qe plotesojne kushtin</returns>

        internal DataRow ktheGrupNdermarrjeSipasKodit(string kodi, int idlicenca)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@Kodi", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_merrGrupArkeBankeSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_ktheGrupArkeBankeSipasId per te marre nje objekt clsGrupNdermarrje ne DB duke filtruar sipas id-se te grupit te bankes.
        ///<param name="id">ID e grupit te bankes</param>
        /// <returns> Kthen nje collection me objekte clsGrupNdermarrje qe plotesojne kushtin</returns>
        /// </summary>
        internal DataRow merrGrupNdermarrjeSipasId(int id)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_ktheGrupArkeBankeSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_ekzistonGrup per te kontrolluar nese ekziston nje objekt clsGrupNdermarrje ne DB duke filtruar.   
        /// </summary>
        ///<param name="nr">Numri(kodi) i grupit te ndermarrje</param>
        ///<param name="idnderm">id e LICENCA</param>
        /// <returns> Kthen true nese ekziston nje objekt clsGrupBanke qe ploteson kushtin</returns>

        public bool ekzistonGrupNdermarrje(string nr, int idlicenca)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", nr, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLICENCA", idlicenca, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_ekzistonGrup");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        /// <summary>
        /// Ekzekuton prc_T_GRUPENDERMARRJE_kaNdermarje per te kontrolluar nese ekziston nje grup ndermarrje i caktuar ka ndermarje apo jo.
        ///<param name="id">Id e grupit te ndermarjes</param>
        /// <returns> Kthen true nese ky grup ndermarje ka ndermarje</returns>
        /// </summary>
        public bool kaNdermarje(int id)
        {//kontrollon nese ky grup ka banke

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GRUPENDERMARRJE_kaNdermarje");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;

        }

        #endregion

        internal decimal ktheKoeficentArtikulli(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULI", idArtikulli, ParameterDirection.Input);
            return Convert.ToDecimal(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheKoeficent"));
        }

        internal int ktheMetodeKostoje(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULI", idArtikulli, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheMetodeKostoje"));
        }

        internal int ktheIdTvshSipasIdArtikullit(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULI", idArtikulli, ParameterDirection.Input);
            object idTvsh = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheIdTvsh");
            if (idTvsh == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(idTvsh);
        }

        internal string ktheCmimArtikulliNivelBaze(int idArtikulli, int idndermarje, int idnjesia)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNJESIA", idnjesia, ParameterDirection.Input);
            object cmimNivelBaze = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NIVELCMIMI_merrCmimArtikulliNivelCmimiBaze");
            if (cmimNivelBaze == null)
                return " ";

            else
                return cmimNivelBaze.ToString();
        }

        internal int ktheIdTvshSipasKodArtikullit(string kodArtikulli, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            object idTvsh = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Artikulli_ktheIdTvshSipasKodArt");
            if (idTvsh == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(idTvsh);
        }

        internal string ktheKodArtikulliSipasId(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULI", idArtikulli, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_ktheKodArtikulliSipasId"));
        }

        internal bool ktheKontrollCmimiPerDetajim(int idArtikulli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDARTIKULI", idArtikulli, ParameterDirection.Input);
            return Convert.ToBoolean(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_ktheKontrollCmimiPerDetajim"));
        }

        public int ktheIdArtRaportuesSipasKodit(string kodArtikullRaportuesi, int idNdermarrjeRaportuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", kodArtikullRaportuesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrjeRaportuesi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARTIKULLI_merrIdArtikullSipasKoditDheNdermarrjes"));

        }

        /// <summary>
        /// update-on daten e modifikimit me daten aktuale per artikullin Therret procedures: prc_T_ARTIKULLI_updDtModifikimi
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <returns></returns>
        internal clsMesazh updateDtModifikiArtikullit(int idArtikulli, int idPerdoruesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDARTIKULLI", idArtikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARTIKULLI_updDtModifikimi");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;

        }

        //shtuar nga Senada
        /// <summary>
        /// Merr nga db-ja numrin e diteve te mbetura te licences duke u nisur nga id-ja  e perdoruesit
        /// </summary>
        /// <param name="idPerdorues">id-ja e dhene ne input</param>
        /// <returns>Kthen DataRow, null nese nuk ekziston</returns>
        public DataRow merrDiteTembeturateLicencesSipasNdermarrjes(int idPerdorues)
        {
            //string datefill;


            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "SP_merrDitetEMbeturaTeLicencesPerPerdoruesin");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];

        }

        /// <summary>
        /// kthen numrin limit te diteve te mbetura te licencave nga tabela T_SERVERSETTINGS
        /// </summary>
        /// <returns></returns>
        internal int ktheLimitDiteTeMbeturaPerLicencen()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            object nr = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SERVERSETTINGS_merrLimitDiteTeMbetura");
            if (nr == DBNull.Value)
                return -1;
            else return Convert.ToInt32(nr);
        }

        /// <summary>
        /// kthen rreshtin me te dhenat true ose false ne tabelen T_NLOGLEVELS te kolonave te emertuara me emrat e niveleve te logut 
        /// </summary>
        /// <returns></returns>
        internal DataTable ktheNlogLevels()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_NLOGLEVELS_getAllLevels").Tables[0];
        }

        internal clsMesazh NdryshoNivelVerbosity(int niveli, string moduli)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@MODULI", moduli, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_NLOGLEVELS_upd");
            return new clsMesazh(true, "Niveli i verbosity u modifikua me sukses!");
        }

        internal string KtheMesazhPerPerdoruesin()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            object nr = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SERVERSETTINGS_merrMesazhPerdoruesi");
            if (nr == DBNull.Value)
                return "";
            else return nr.ToString();
        }

        /// <summary>
        /// Ekzekuton prc_T_AMBJENT_sel per te marre nje objekt
        /// </summary>
        internal void merrAmbjente(int id)
        {


            dbManager.Open();
            //shtimi i parametrave
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AMBJENT_sel");

        }

        internal DataTable ktheGjitheAmbjente(int idllojlicence, int idgjuha, bool ambjentpermobile)
        {



            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idllojlicence", idllojlicence, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idgjuha", idgjuha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@ambjentpermobile", ambjentpermobile, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_AMBJENTE_merrGjitheAmbjenteSipasLlojLicence");
            return ds.Tables[0];

        }

        /// <summary>
        /// merr IdRolin e Rolit Default me kod RA qe eshte celur i fundit
        /// </summary>
        /// <returns></returns>
        public int merrIdRolDefault()
        {


            dbManager.Open();
            dbManager.CreateParameters(0);
            object idRoli = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ROLI_merrIdRol");

            if (idRoli == null)
                return -1;
            return Convert.ToInt32(idRoli);

        }

        /// <summary>
        /// kthen idPerdoruesin qe perdoret per celjen e ndermarrjes nga programi i licencave
        /// </summary>
        /// <returns></returns>
        public int merrPerdoruesAdminlicence()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            object idPerdoruesiAdminLicenca = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_kthePerdoruesAdminLicence");

            if (idPerdoruesiAdminLicenca == null)
                return -1;
            return Convert.ToInt32(idPerdoruesiAdminLicenca);
        }
        /// <summary>
        /// kthen nje datatable me db dhe licencat perkatese (aktive)
        /// </summary>
        /// <returns></returns>
        public DataTable merrLicencaMeDb()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_LICENCA_merrLicencatMeDb");
            return ds.Tables[0];
        }
        public DataTable merrLicencaMeDb(string filter, long start, long end)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@Filter", filter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@startIndex", start, ParameterDirection.Input);
            dbManager.AddParameters(2, "@endIndex", end, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_LICENCA_merrLicencatMeDbMeFilter");
            return ds.Tables[0];
        }
        public DataTable merrLicencaMeDb(int kodi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@value", kodi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_LICENCA_merrLicencatMeDbSipasKodit");
            return ds.Tables[0];
        }

        #region T_PASSWORD_HISTORY
        /// <summary>
        /// kontrollon nese eshte password i perdorur me pare
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="nr">numri i passwordeve qe rhen te fundit</param>
        /// <param name="password">passwordi i cili do kontrollohet nese eshte perdorur me pare</param>
        /// <returns></returns>
        internal clsMesazh eshtePassVjeter(int idPerdorues, int nr, string password, ResourceManager rm, CultureInfo ci)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPEDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Nr", nr, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PASSWORD", password, ParameterDirection.Input);
            object nrPass = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PASSWORD_HISTORY_merrPasswordet");
            if (Convert.ToInt32(nrPass) > 0) return new clsMesazh(false, rm.GetString("msgFjalekalimPerdorur", ci));
            else return new clsMesazh(true);

        }
        internal clsMesazh eshtePassVjeterPunonjes(int idPunonjes, int nr, string password, ResourceManager rm, CultureInfo ci)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@Nr", nr, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PASSWORD", password, ParameterDirection.Input);
            object nrPass = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PASSWORD_HISTORY_merrPasswordet_Punonjes");
            if (Convert.ToInt32(nrPass) > 0) return new clsMesazh(false, rm.GetString("msgFjalekalimPerdorur", ci));
            else return new clsMesazh(true);

        }
        /// <summary>
        /// shton passwordin ne historikun e pass per perdoruesin
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="Pass">passwordi qe do shtohet</param>
        /// <returns></returns>
        public bool shtoPasswordNeHistorik(int idPerdoruesi, string Pass, int nrPassNeHistorik, int perdoruesiILoguar)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPEDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PASSWORD", Pass, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Nr", nrPassNeHistorik, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESILOGUAR", perdoruesiILoguar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASSWORD_HISTORY_ins");
            return true;

        }

        /// <summary>
        /// shton passwordin ne historikun e pass per punonjesin
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="Pass">passwordi qe do shtohet</param>
        /// <returns></returns>
        public bool shtoPasswordNeHistorikPunonjes(int idPunonjes, string Pass, int nrPassNeHistorik, int perdoruesiILoguar)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPUNONJES", idPunonjes, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PASSWORD", Pass, ParameterDirection.Input);
            dbManager.AddParameters(2, "@Nr", nrPassNeHistorik, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESILOGUAR", perdoruesiILoguar, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PASSWORD_HISTORY_PUNONJES_ins");
            return true;

        }


        /// <summary>
        /// kthen rreshtin e password history ne baze te idoerdoruesit dhe passwordit
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public DataRow merPassHistorySipasPassDhePerd(int idPerdoruesi, string pass)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PASSWORD", pass, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PASSWORD_HISTORY_merrSipasPerdoruesDhePassword");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];

        }
        #endregion
        public DataTable merrTeDhenaPerExportFlexCubeAlphaBank(int idNdermarrje, string kodiUnik, string data, bool drejteShikoGjitheDok, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NR_UNIK", kodiUnik, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTKRIJIMI", data, ParameterDirection.Input);
            dbManager.AddParameters(3, "@shikoGjitheDokumentat", drejteShikoGjitheDok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_RAP_EXPORT_PERFLEXCUBE_ALPHABANK");
            return ds.Tables[0];

        }

        public DataTable merrTeDhenaPerPrepaidExpensesAlphaBank(int idNdermarrje, string kodiUnik, DateTime data)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NR_UNIK", kodiUnik, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTKRIJIMI", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_RAP_EXPORT_PREPAID_EXPENSES_ALPHABANK");
            return ds.Tables[0];

        }

        /// <summary>
        /// kthen llojin e ndermarrjes ne baze te id-se se saj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal int ktheLlojNdermarrjeSipasID(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheLlojNdermarrjeSipasId"));
        }

        internal int merrIdNdermarrjeOwn()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_merrIdNdermarrjeOwn"));
        }

        /// <summary>
        /// kthen monedhen e ndermarrjes ne baze te id-se se saj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal int ktheidMonedheNdermSipasID(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_MONEDHA_IdktheMonedheNderm"));
        }
        /// <summary>
        /// kthen monedhen e ndermarrjes ne baze te id-se se saj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal DataRow ktheMonedheNdermSipasID(int idNdermarrje) //todo senada
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataRowCollection drs = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_ktheMonedheNderm").Tables[0].Rows;
            if (drs.Count > 1 || drs.Count == 0)
                return null;
            return drs[0];
        }
        /// <summary>
        /// kthen limitin e shitjes se ndermarrjes ne baze te id-se se saj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal double KtheLimitshitjeNdermarrjes(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return Convert.ToDouble(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheLimitShitje"));
        }
        /// <summary>
        /// Metode e cila sherben per te marre id e ndermarrjes nga e cila do merret konfigurimi i email.
        /// Te dhenat e konfigurimit te email do perdoren per te bere resetimin e pass te perdoruesit qe ka bere kerkese per resetim.
        /// Eshte menduar te merret id e pare ne liste nga te gjitha ndermarrjet e licences se perdoruesit duke qene se konfigurimi i email eshte ne nivel ndermarrjeje dhe jo licence.
        /// Kerkuar nga vodafone qe te merret nga konfigurimi i email adresa derguese qe dergon pass e ri per perdoruesin. 
        /// Me pare eshte marre nga web.config
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        internal int ktheIdNdermPareNeListe(int idPerdoruesi, bool eshtePunonjes)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@EshtePunonjes", eshtePunonjes, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdNdermarrjenEPareNeListeTeKonfigurimeEmail"));
        }
        internal int ktheIdNdermPareNeListeSipasLicences()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_NDERMARJE_ktheIdNdermarrjenEPareNeListeSipasLicences"));
        }

        #region Gjenerimi PIN

        /// <summary>
        /// Ruan objektin e PIN-it te gjeneruar
        /// </summary>
        /// <param name="idPIN">(int) Id e pinit</param>
        /// <param name="kodiPIN">(string) Kodi i pinit te gjeneruar</param>
        /// <param name="dataAktivizuar">(DateTime) Data e aktivizimit</param>
        /// <param name="dataPerfunduar">(DateTime) Data kur skadon PIN-i gjeneruaur</param>
        /// <param name="idPerdoruesi">(int) Id e perdorur</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes</param>
        /// <param name="perdorur">(bool) Eshte perdorur apo jo PIN-i</param>
        /// <param name="dataPerdorur">(DateTime) Nese eshte perdorur PIN-i data e perdorimit te tij</param>
        /// <returns>(clsMesazh) Kthen objektin e mesazhit ne varesi te ruajtjes</returns>
        internal clsMesazh ruajPINGjeneruar(out int idPIN, string kodiPIN, DateTime dataAktivizuar, DateTime dataPerfunduar, int idPerdoruesi, int idNdermarrja, bool perdorur, DateTime dataPerdorur)
        {
            idPIN = -1;


            dbManager.Open();

            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDPIN", idPIN, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODIPIN", kodiPIN, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA_AKTIVIZIMIT", dataAktivizuar, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA_PERFUNDIMIT", dataPerfunduar, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PERDORUR", perdorur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PIN_GJENERUAR_ins");
            idPIN = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;

        }

        /// <summary>
        /// Modifikimi i perdorimit te PIN-it
        /// </summary>
        /// <param name="idPIN">(int) Id e pinit</param>
        /// <param name="perdorur">(bool) Eshte perdorur apo jo PIN-i</param>
        /// <param name="dataPerdorur">(DateTime) Nese eshte perdorur PIN-i data e perdorimit te tij</param>
        /// <returns>(clsMesazh) Kthen objektin e mesazhit ne varesi te modifikimit</returns>
        internal clsMesazh modifikoPINGjeneruar(int idPIN, bool perdorur, DateTime dataPerdorur)
        {


            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPIN", idPIN, ParameterDirection.Output);
            dbManager.AddParameters(1, "@PERDORUR", perdorur, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DATA_PERDORIMIT", dataPerdorur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_PIN_GJENERUAR_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Kontrollon ne databaze vlefshemerin e nje PIN-i
        /// </summary>
        /// <param name="kodiPIN">(string) Kodi i pinit te gjeneruar</param>
        /// <param name="idPerdoruesi">(int) Id e perdorur</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes</param>
        /// <param name="dataPerdorur">(DateTime) Nese eshte perdorur PIN-i data e perdorimit te tij</param>
        /// <returns>(bool) Kontrollon nese PIN-i eshte i vlefshem apo jo. Kthen true nese eshte i vlefshem dhe anasjelltas</returns>
        internal bool kontrolloVlefshmeriPin(string kodiPIN, int idPerdoruesi, int idNdermarrja, DateTime dataPerdorur)
        {



            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@KODIPIN", kodiPIN, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATA_PERDORIMIT", dataPerdorur, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PIN_GJENERUAR_IVLEFSHEM"));
            return Convert.ToBoolean(pergjigje);
        }

        #endregion

        #region Logu i People Finder
        public bool fshiTabelaTemporareImporti(string tabelaKoka, string tabelaTrupi, string emerTabeleKokaHistorik, string emerTabeleTrupiHistorik)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@TABELAKOKA", tabelaKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TABELATRUPI", tabelaTrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TABELAKOKAHISTORIK", emerTabeleKokaHistorik, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TABELATRUPIHISTORIK", emerTabeleTrupiHistorik, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TABELATEMPORARE_del");
            return true;

        }
        internal clsMesazh ruajLogPeopleFinder(string ipKlient, DateTime kohaLogut, string pershkrimiLogut, string kodPershkrimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@ID", 0, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IPKLIENT", ipKlient, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOHALOG", kohaLogut, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PERSHKRIMI", pershkrimiLogut, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KOD_PERSHKRIMI", pershkrimiLogut, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LOG_PEOPLEFINDER_AUTENTIFIKIM_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal int ktheErrorLogimiNgaPeopleFinder()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LOG_PEOPLEFINDER_AUTENTIFIKIM_SEL_ERRLOGIMPEOPLEFINDER");
            return ds.Tables[0].Rows.Count;
        }





        #endregion

    }
}