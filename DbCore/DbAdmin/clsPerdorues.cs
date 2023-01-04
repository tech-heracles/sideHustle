using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.SessionState;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using System.Collections;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Security;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje perdorues.
    ///  (Te dhenat  merren nga tabela : T_PERDORUESI)
    /// </summary>
    public class clsPerdorues
    {
        #region Atribute

        private int idPerdorues;
        private int idQyteti;
        private int idGjuha;
        private String emriPerdorues;
        private String mbiemriPerdorues;
        private bool perdoruesAktiv;
        private String perdoruesUsername;
        private String perdoruesPassword;
        private string perdoruesTelefon;
        private string perdoruesFax;
        private string perdoruesEmail;
        private string perdoruesAdresa;
        private int idPerdoruesi;
        //private string backPath;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idStilRaporti;
        private string stilRaportiFileName;
        private colRolPerdorues oColRolPerdoruesi;
        private float zoomFactor;
        private int idAmbjent;
        private int idAmbjentMobile;
        private bool passwordIPerkohshem;
        private bool perdoruesIKycur;
        /// <summary>
        /// mban nese info kur hapet ambjenti do rine te hapura apo te mbyllura
        /// </summary>
        private bool infoHapur;
        private string kerkeseResetPass;
        private DateTime dateKrijimiPassword;
        private bool kontrollPassword;
        private bool shfaqDtPrintimi;
        private int exportFormat;
        private int exportMode;
        private bool kycurMobile;
        private bool shfaqPerdoruesMenu;
        private bool shfaqMesazhePopup;
        private DataRow rreshti;

        ///Ne kartelen e perdoruesit disa fushat te reja per Shops Hierarki
        /// </summary> 
        private string shopCode;
        private string shopName;
        private string dealerName;
        private string useriCRM;
        private string userEtopUP;
        private string iDETopUp;
        private string typeOfDevice;
        private string salesRepMobileNumber;
        private string salesRepMPesaMSISDN;
        private int gjinia;
        private DateTime salesRepStartDateVod;
        private DateTime salesRepTrainingStart;
        private DateTime salesRepStartDateShop;
        private DateTime salesRepMaternityLeaveStart;
        private DateTime leaveDateVod;
        private DateTime leaveDateShop;
        private DateTime maternityLeaveEndDate;
        private DateTime trainingEndDate;
        private string commentsRetailSales;
        private string accountExecutive;
        private string iDNumber;
        private int isInsured;
        private string commentsRetailOpSpecialist;
        private string regionalSupervisor;
        private string retailSalesAccountExecutive;
        private string retailSalesAreaManager;
        private DateTime birthdate;
        private string siteCode;
        private string district;
        private string shopMainCode;
        private string latitude;
        private string longitude;
        private int status;
        private int leaveReason;
        private int uniform;
        private string shenime;
        private int statusAprovimi;
        private bool njoftimEmailAprovim;
        private colAutorizimetTrupi autorizimet;
        private int idKonfigKasa;

        // otp for goalpha
        private string otp_token;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idperdorues">id e perdoruesit</param>
        public clsPerdorues(int idperdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushPerdorues(data.merrPerdorues(idperdorues));
            }
        }

        //public clsPerdorues(int idperdorues, bool meRole)
        //{
        //    using (clsDatabaseAdmin data = new clsDatabaseAdmin())
        //    {
        //        mbushPerdorues(data.merrPerdorues(idperdorues), meRole);
        //    }
        //}

        public clsPerdorues(int idperdorues, clsDatabaseAdmin data)
        {

            mbushPerdorues(data.merrPerdorues(idperdorues));

        }
        /// <summary>
        /// konstruktor qe mbush objektin e clsPerdorues ne baze te username
        /// </summary>
        /// <param name="userName"></param>
        public clsPerdorues(string userName)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushPerdorues(data.kthePerdoruesSipasUsername(userName));
            }
        }
        public clsPerdorues(string userName,string email,bool google)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushPerdorues(data.ktheUserNgaLoginMeUsernameOseEmail(userName,email).Rows[0]);
            }
        }
        public clsPerdorues(string userName,string connStringName)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin(connStringName))
            {
                mbushPerdorues(data.kthePerdoruesSipasUsername(userName));
            }
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsPerdorues()
        {
        }

        public clsPerdorues(DataRow rreshti)
        {
            
            mbushPerdorues(rreshti);
        }

        /// <summary>
        /// konstruktor qe mbush objektin e clsPerdorues ne baze te username
        /// </summary>
        /// <param name="userName"></param>
        public clsPerdorues(string userName, clsDatabaseAdmin dbAdmin)
        {
            mbushPerdorues(dbAdmin.TransCache.getPerdorues(userName, dbAdmin));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos tokenin per OTP.
        /// </summary>
        public string Otp_Token
        {
            get { return otp_token; }
            set { otp_token = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese perdoruesi duhet te lejohet te logohet ne aplikacionin mobile 
        /// </summary>
        public bool KycurMobile
        {
            get { return kycurMobile; }
            set { kycurMobile = value; }
        }
        /// <summary>
        /// Kthen/Vendos indexin e combos se menyres se exportit te stili i raportit
        /// </summary>
        public int ExportMode
        {
            get { return exportMode; }
            set { exportMode = value; }
        }

        /// <summary>
        /// Kthen/Vendos indexin e combos se formatit te exportit te stili i raportit
        /// </summary>
        public int ExportFormat
        {
            get { return exportFormat; }
            set { exportFormat = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit te passwordit per perdoruesin
        /// </summary>
        public DateTime DateKrijimiPassword
        {
            get { return dateKrijimiPassword; }
            set { dateKrijimiPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese perdoruesi ka arritur numrin maksimal te tentativave per login(pra nese eshte kycur ose jo)
        /// </summary>
        public bool PerdoruesIKycur
        {
            get { return perdoruesIKycur; }
            set { perdoruesIKycur = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese perdoruesi do te detyrohet te ndryshoje pass heren e pare qe do logohet
        /// </summary>
        public bool PasswordIPerkohshem
        {
            get { return passwordIPerkohshem; }
            set { passwordIPerkohshem = value; }
        }

        /// <summary>
        /// ambjenti default i perdoruesit
        /// </summary>
        public int IdAmbjent
        {
            get
            {
                return idAmbjent;
            }
            set
            {
                idAmbjent = value;
            }
        }

        /// <summary>
        /// ambjenti mobile default i perdoruesit
        /// </summary>
        public int IdAmbjentMobile
        {
            get
            {
                return idAmbjentMobile;
            }
            set
            {
                idAmbjentMobile = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }

        ///// <summary>
        ///// Kthen/Vendos ID-ne e theme default te ketij perdoruesi
        ///// </summary>
        //public int IdTheme
        //{
        //    get { return idTheme; }
        //    set { idTheme = value; }
        //}

        /// <summary>
        /// Kthen/Vendos vleren e Id-se se stilit default te raporteve per kete perdorues
        /// </summary>
        public int IdStilRaporti
        {
            get { return idStilRaporti; }
            set { idStilRaporti = value; }
        }

        /// <summary>
        /// mban nese info kur hapet ambjenti do rine te hapura apo te mbyllura
        /// </summary>
        public bool InfoHapur
        {
            get
            {
                return infoHapur;
            }
            set
            {
                infoHapur = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos filename-in e stilit default te raporteve per kete perdorues
        /// </summary>
        public String StilRaportiFileName
        {
            get { return stilRaportiFileName; }
            set { stilRaportiFileName = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e qytetit qe i eshte caktuar ketij perdoruesi.
        /// </summary>
        public int IdQyteti
        {
            get { return idQyteti; }
            set { idQyteti = value; }
        }

        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public int IdGjuha
        {
            get { return idGjuha; }
            set { idGjuha = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e perdoruesit.
        /// </summary>
        public String EmriPerdorues
        {
            get { return emriPerdorues; }
            set { emriPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos mbiemrin e perdoruesit.
        /// </summary>
        public String MbiemriPerdorues
        {
            get { return mbiemriPerdorues; }
            set { mbiemriPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin nese perdoruesi eshte aktiv apo jo.
        /// </summary>
        public bool PerdoruesAktiv
        {
            get { return perdoruesAktiv; }
            set { perdoruesAktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos username-in perdoruesit i cili duhet te jete unik.
        /// </summary>
        public String PerdoruesUsername
        {
            get { return perdoruesUsername; }
            set { perdoruesUsername = value; }
        }

        /// <summary>
        /// Kthen/Vendos passwordin e perdoruesit, i cili eshte i koduar.
        /// </summary>
        public String PerdoruesPassword
        {
            get { return perdoruesPassword; }
            set { perdoruesPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos telefonin e perdoruesit.
        /// </summary>
        public String PerdoruesTel
        {
            get { return perdoruesTelefon; }
            set { perdoruesTelefon = value; }
        }

        /// <summary>
        /// Kthen/Vendos Fax-in e perdoruesit.
        /// </summary>
        public String PerdoruesFax
        {
            get { return perdoruesFax; }
            set { perdoruesFax = value; }
        }

        /// <summary>
        /// Kthen/Vendos email-in e perdoruesit.
        /// </summary>
        public String PerdoruesEmail
        {
            get { return perdoruesEmail; }
            set { perdoruesEmail = value; }
        }

        /// <summary>
        /// Kthen/Vendos adresen e perdoruesit.
        /// </summary>
        public String PerdoruesAdresa
        {
            get { return perdoruesAdresa; }
            set { perdoruesAdresa = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me te drejtat e perdoruesit. Collectioni permban objekte te tipit
        /// <see cref="clsTeDrejtat"/>
        /// </summary>
        public colRolPerdorues OColRolPerdoruesi
        {
            get { return oColRolPerdoruesi; }
            set { oColRolPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Id-ne e perdoruesit i cili krijoi kete perdorues.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos queryString te hashuar qe i dergohet si link verifikimi perdoruesit qe ben kerkese per resetim password-i
        /// </summary>
        public string KerkeseResetPass
        {
            get { return kerkeseResetPass; }
            set { kerkeseResetPass = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }

        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }

        }

        /// <summary>
        /// Kthen/Vendos zoomfactor te raporteve per perdoruesin.
        /// </summary>
        public float ZoomFactor
        {
            get { return zoomFactor; }
            set { zoomFactor = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese ky perdorues do te testohet me password. Dmth nese useri do te logohet nga ambjenti i webit, apo do te logohet nga webServisi.
        /// </summary>
        public bool KontrollPassword
        {
            get { return kontrollPassword; }
            set { kontrollPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese ky perdorues do te testohet me password. Dmth nese useri do te logohet nga ambjenti i webit, apo do te logohet nga webServisi.
        /// </summary>
        public bool ShfaqPerdoruesMenu
        {
            get { return shfaqPerdoruesMenu; }
            set { shfaqPerdoruesMenu = value; }
        }
        /// <summary>
        /// Kthen/Vendos nese ketij perdoruesi do ti shfaqet data dhe ora e printimit ne raporte
        /// </summary>
        public bool ShfaqDtPrintimi
        {
            get { return shfaqDtPrintimi; }
            set { shfaqDtPrintimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos nese ketij perdoruesi do ti shfaqet njoftimet ku hyn ne ndermarje 
        /// </summary>
        public bool ShfaqMesazhePopup
        {
            get { return shfaqMesazhePopup; }
            set { shfaqMesazhePopup = value; }
        }

        public string ShopCode
        {
            get { return shopCode; }
            set { shopCode = value; }
        }

        public string ShopName
        {
            get { return shopName; }
            set { shopName = value; }
        }
        public string DealerName
        {
            get { return dealerName; }
            set { dealerName = value; }
        }
        public string UseriCRM
        {
            get { return useriCRM; }
            set { useriCRM = value; }
        }
        
        public string UserEtopUP
        {
            get { return userEtopUP; }
            set { userEtopUP = value; }
        }
        public string IDETopUp
        {
            get { return iDETopUp; }
            set { iDETopUp = value; }
        }
        public string TypeOfDevice
        {
            get { return typeOfDevice; }
            set { typeOfDevice = value; }
        }
        
        public string SalesRepMobileNumber
        {
            get { return salesRepMobileNumber; }
            set { salesRepMobileNumber = value; }
        }
        public string SalesRepMPesaMSISDN
        {
            get { return salesRepMPesaMSISDN; }
            set { salesRepMPesaMSISDN = value; }
        }
        public int Gjinia
        {
            get { return gjinia; }
            set { gjinia = value; }
        }
        public DateTime SalesRepStartDateVod
        {
            get { return salesRepStartDateVod; }
            set { salesRepStartDateVod = value; }
        }

        public DateTime SalesRepTrainingStart
        {
            get { return salesRepTrainingStart; }
            set { salesRepTrainingStart = value; }
        }

        public DateTime SalesRepStartDateShop
        {
            get { return salesRepStartDateShop; }
            set { salesRepStartDateShop = value; }
        }
        public DateTime SalesRepMaternityLeaveStart
        {
            get { return salesRepMaternityLeaveStart; }
            set { salesRepMaternityLeaveStart = value; }
        }
        
        public DateTime LeaveDateVod
        {
            get { return leaveDateVod; }
            set { leaveDateVod = value; }
        }
        public DateTime LeaveDateShop
        {
            get { return leaveDateShop; }
            set { leaveDateShop = value; }
        }
        public DateTime MaternityLeaveEndDate
        {
            get { return maternityLeaveEndDate; }
            set { maternityLeaveEndDate = value; }
        }
        public DateTime TrainingEndDate
        {
            get { return trainingEndDate; }
            set { trainingEndDate = value; }
        }

        public string CommentsRetailSales
        {
            get { return commentsRetailSales; }
            set { commentsRetailSales = value; }
        }
        public string AccountExecutive
        {
            get { return accountExecutive; }
            set { accountExecutive = value; }
        }
        
        public string IDNumber
        {
            get { return iDNumber; }
            set { iDNumber = value; }
        }
        
        public int IsInsured
        {
            get { return isInsured; }
            set { isInsured = value; }
        }
        public string CommentsRetailOpSpecialist
        {
            get { return commentsRetailOpSpecialist; }
            set { commentsRetailOpSpecialist = value; }
        }
        public string RegionalSupervisor
        {
            get { return regionalSupervisor; }
            set { regionalSupervisor = value; }
        }
        
        public string RetailSalesAccountExecutive
        {
            get { return retailSalesAccountExecutive; }
            set { retailSalesAccountExecutive = value; }
        }
        public string RetailSalesAreaManager
        {
            get { return retailSalesAreaManager; }
            set { retailSalesAreaManager = value; }
        }
        
        public DateTime Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }
        public string SiteCode
        {
            get { return siteCode; }
            set { siteCode = value; }
        }
        
        public string District
        {
            get { return district; }
            set { district = value; }
        }
        public string ShopMainCode
        {
            get { return shopMainCode; }
            set { shopMainCode = value; }
        }
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public int LeaveReason
        {
            get { return leaveReason; }
            set { leaveReason = value; }
        }
        public int Uniform
        {
            get { return uniform; }
            set { uniform = value; }
        }
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }
        
        public int StatusAprovimi
        {
            get { return statusAprovimi; }
            set { statusAprovimi = value; }
        }

        public bool NjoftimEmailAprovim
        {
            get { return njoftimEmailAprovim; }
            set { njoftimEmailAprovim = value; }
        }

        public int IdKonfigKasa
        {
            get { return idKonfigKasa; }
            set { idKonfigKasa = value; }
        }

        #endregion

        #region Metoda Publike
        internal static DataTable merrPerdoruesPerImport(string emerTabKoka, string ndermarrjeKey, string ndermarrjeKodi, bool merrTePaImportuara, bool riMerrTePaImportuara)
        {
            using (var db = new clsDatabaseAdmin())
                return db.merrPerdoruesPerImport(emerTabKoka, ndermarrjeKey, ndermarrjeKodi, merrTePaImportuara, riMerrTePaImportuara);
        }

        public clsMesazh ruaj(int idndermarje, int idNdermarjeVit, string idDokImporti = "", bool ngaImporti = false, string emerTabKoka = "", string ndermarrjeKey = "", string primarykey = "", string PassIRi = "true", string passwordiGjeneruar = "", bool kushtiSHTR = true)
        {
            //clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = ruajPerdoruesTeDrejta(this, idndermarje, idNdermarjeVit, idDokImporti, ngaImporti, emerTabKoka, ndermarrjeKey, primarykey, PassIRi, passwordiGjeneruar, kushtiSHTR);
            return u_ruajt;
        }
        public static string merrEmail(int idPerdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrEmailSipasIdPerdoruesit(idPerdorues);
            }
        }
        public static bool modifikoOtp(int idPerdorues, string otp)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.modifikoPerdoruesOtp(idPerdorues, otp).Status;
            }
        }
        /// <summary>
        /// modifikon passwordin e perdoruesit
        /// </summary>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public clsMesazh modifikoPassword(String newPass, int idPerdoruesiLoguar)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            db.beginTransaksion();
            clsMesazh mesazh = modifikoPassword(idPerdorues, newPass, db, this.PasswordIPerkohshem, idPerdoruesiLoguar, false);
            if (mesazh.Status)
            {
                db.commitTransaksion();
                return mesazh;
            }
            else
            {
                db.rollbackTransaksion();
                return mesazh;
            }
        }

        /// <summary>
        /// modifikon password-in e perdoruesit
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="newPass"></param>
        /// <param name="db"></param>
        /// <param name="PasswordIPerkohshem"></param>
        /// <returns></returns>
        public static clsMesazh modifikoPassword(int idPerdoruesi, String newPass, clsDatabaseAdmin db, bool PasswordIPerkohshem, int idPerdoruesiLoguar, bool punononjes)
        {
            clsMesazh mesazh = new clsMesazh();
            if (punononjes)
                mesazh = db.modifikoPassowrdPunonjes(idPerdoruesi, newPass, PasswordIPerkohshem);
            else mesazh = db.modifikoPassowrd(idPerdoruesi, newPass, PasswordIPerkohshem);
            bool suksesRuajPassHistorik = true;
            if (!mesazh.Status)
                return mesazh;
            else
            {
                DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi, db);
                if (konfigPass.RuajHistorikunPass)
                {
                    suksesRuajPassHistorik = konfigPass.shtoPassNeHistorik(idPerdoruesi, newPass, idPerdoruesiLoguar, db);
                }
                mesazh.Status = suksesRuajPassHistorik;
                if (mesazh.Status)
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["mesazhRuajtjeMeSukses"];
                else
                    mesazh.PershkrimMesazhi = "Ndodhi nje gabim gjate ruajtjes!";
                return mesazh;
            }
        }

        public clsMesazh modifiko(string PassIRi = "true", string passwordiGjeneruar = "", int idNdermarje = 0, bool kushtiMA = false, int idNdermarjeVit = 0, bool kushtiSHTR = true, string idDokImporti = "", bool ngaImporti = false, string emerTabKoka = "", string ndermarrjeKey = "", string primarykey = "")
        {
            return modifikoPerdoruesTeDrejta(this, PassIRi, passwordiGjeneruar, idNdermarje, kushtiMA, idNdermarjeVit, kushtiSHTR, idDokImporti, ngaImporti, emerTabKoka, ndermarrjeKey, primarykey);
        }
        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiPerdoruesStatus(this.idPerdorues, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        public static clsMesazh ndryshoInfo(int idperdorues, int idperdoruesveprimi, bool info)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh = data.ndryshoInfo(idperdorues, idperdoruesveprimi, info);
            data.Dispose();
            return mesazh;
        }

        public static clsMesazh ndryshoInfoplus(int idperdorues, int idperdoruesveprimi, bool info)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh = data.ndryshoInfoplus(idperdorues, idperdoruesveprimi, info);
            data.Dispose();
            return mesazh;
        }

        public static clsMesazh ndryshoInfominus(int idperdorues, int idperdoruesveprimi, bool info)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh = data.ndryshoInfominus(idperdorues, idperdoruesveprimi, info);
            data.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Metode e krijuar per Vodafonin. Duhet te ktheje perdoruesin sipas username-it
        /// </summary>
        /// <param name="perdoruesi"></param>
        /// <returns></returns>
        public void kthePerdoruesSipasUsername(string perdoruesi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool ekziston = mbushPerdoruesGjitheNder(data.merrPerdoruesSipasUsernameTePrindi(perdoruesi));
            data.Dispose();
        }

        /// <summary>
        /// Kthen nje collection me te gjithe perdoruest qe ndodhen ne databaze.Theret funksionin
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        public colPerdoruesit merriTeGjithe(int idperdorues)
        {
            colPerdoruesit data = new colPerdoruesit();
            data.mbushGjithePerdoruesit(idperdorues);
            return data;
        }

        public static string ktheEmerMbiemer(int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrEmerMbiemerPerdoruesi(idPerdoruesi);
            }
        }
        /// <summary>
        /// Kthen nje status qe tregon nese ekziston ne databaze nje user me kete username.Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ekzistonPerdoruesi"/> 
        /// </summary>
        public bool ekzistonPerdoruesi(String usrN)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool ekziston = data.ekzistonPerdoruesi(usrN);
            data.Dispose();
            return ekziston;
        }

        /// <summary>
        /// Kthen nje status qe tregon nese ka veprime me kete perdorues
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.kaVeprime"/> 
        /// </summary>
        public bool kaVeprime(int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool kaVeprime = data.kaVeprime(idperdorues);
            data.Dispose();
            return kaVeprime;
        }

        /// <summary>
        /// Kthen nje collection me nje objekt perdoruesi, te cilin e merr sipas username-it.Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ktheUserNgaLogin"/> 
        /// </summary>
        /// <param name="userName"></param>
        public static colPerdoruesit merrUserNgaLogin(String userName)
        {
            colPerdoruesit colPerd = new colPerdoruesit();
            colPerd.mbushUserNgaLogin(userName);
            return colPerd;
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
        public clsMesazh ruajPerdoruesTeDrejta(clsPerdorues perdorues, int idndermarje, int idNdermarjeVit, string idDokImporti = "", bool ngaImporti = false, string emerTabKoka = "", string ndermarrjeKey = "", string primarykey = "", string PassIRi = "true", string passwordiGjeneruar = "", bool kushtiSHTR = true)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int idregj = clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CP", dbAdmin);
            int idlloji = clsLlojKodi.ktheIDLlojKodi("Kod", dbAdmin);
            clsMesazh mesazh;

            using(var scope = new MyTransactionScope(dbAdmin))
            {
                try
                {
                    if (dbAdmin.ekzistonPerdoruesMeKeteKod(perdorues.PerdoruesUsername))
                        return new MesazhGabimi("Ekziston nje grup me te njejtin kod!");

                    int idP;
                    mesazh = dbAdmin.ruajPerdorues(out idP, perdorues.IdQyteti, perdorues.IdGjuha, perdorues.EmriPerdorues, perdorues.MbiemriPerdorues, perdorues.PerdoruesAktiv, perdorues.PerdoruesUsername, perdorues.PerdoruesPassword, perdorues.PerdoruesTel, perdorues.PerdoruesFax, perdorues.PerdoruesEmail, perdorues.PerdoruesAdresa, perdorues.IdPerdoruesi, perdorues.idStilRaporti, perdorues.idStatusDok, perdorues.zoomFactor, perdorues.InfoHapur, perdorues.idAmbjent, perdorues.kerkeseResetPass, perdorues.passwordIPerkohshem, perdorues.perdoruesIKycur, perdorues.dateKrijimiPassword, perdorues.KontrollPassword, perdorues.exportFormat, perdorues.exportMode, perdorues.ShfaqDtPrintimi, perdorues.KycurMobile, perdorues.ShfaqPerdoruesMenu, perdorues.ShfaqMesazhePopup, perdorues.idAmbjentMobile, perdorues.shopCode, perdorues.shopName, perdorues.dealerName, perdorues.useriCRM, perdorues.userEtopUP, perdorues.iDETopUp, perdorues.typeOfDevice, perdorues.salesRepMobileNumber, perdorues.salesRepMPesaMSISDN, perdorues.gjinia, perdorues.salesRepStartDateVod, perdorues.salesRepTrainingStart, perdorues.salesRepStartDateShop, perdorues.salesRepMaternityLeaveStart, perdorues.leaveDateVod, perdorues.leaveDateShop, perdorues.maternityLeaveEndDate, perdorues.trainingEndDate, perdorues.commentsRetailSales, perdorues.accountExecutive, perdorues.iDNumber, perdorues.isInsured, perdorues.commentsRetailOpSpecialist, perdorues.regionalSupervisor, perdorues.retailSalesAccountExecutive, perdorues.retailSalesAreaManager, perdorues.birthdate, perdorues.siteCode, perdorues.district, perdorues.shopMainCode, perdorues.latitude, perdorues.longitude, perdorues.status, perdorues.leaveReason, perdorues.uniform, perdorues.shenime, perdorues.statusAprovimi, perdorues.njoftimEmailAprovim, perdorues.idKonfigKasa);
                    if (!mesazh.Status)
                        return mesazh;

                    perdorues.idPerdorues = idP;
                    if (kushtiSHTR)
                    {
                        foreach (clsRolPerdorues o in perdorues.OColRolPerdoruesi)
                        {
                            int id = dbAdmin.krijoRolPerdorues(o.IdRoli, perdorues.IdPerdorues);
                            if (id == -1)
                                new MesazhGabimi("Gabim gjate krijimit te rolit!");
                        }
                        mesazh = DbDashboard.clsDashboard.LidhPerdoruesMeDashboardDefault(new DbDashboard.clsDatabaseDashboard(dbAdmin), perdorues.IdPerdorues);
                        if (!mesazh.Status)
                            return mesazh;
                    }

                    if (autorizimet != null && autorizimet.Any())
                    {
                        autorizimet.ForEach(auto => auto.IdPerdorues = this.IdPerdorues);
                        mesazh = autorizimet.Ruaj(this.IdPerdorues, dbAdmin);
                        if (!mesazh)
                            return mesazh;
                    }
                    mesazh = shtoPasswordNeHistorik(PassIRi, perdorues.IdPerdorues, perdorues.PerdoruesPassword, perdorues.IdPerdoruesi, dbAdmin);
                    if (!mesazh.Status)
                        return mesazh;

                    if (!String.IsNullOrEmpty(passwordiGjeneruar))
                    {
                        mesazh = EmailComposer.DergoEmailFjalekaliminEGjeneruar(perdorues.PerdoruesEmail, perdorues.perdoruesUsername, passwordiGjeneruar, perdorues.idPerdorues, dbAdmin);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                    if (ngaImporti && idDokImporti != string.Empty)
                    {
                        var dbRegj = new clsDatabaseRegjistrim(dbAdmin);
                        var statusi = 1;
                        mesazh = dbRegj.updateDokTabeleTemportal(idDokImporti, idndermarje, statusi, emerTabKoka, primarykey, ndermarrjeKey);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                    mesazh = clsThemesAmbjente.krijoThemesDefaultPerPerdorues(perdorues.IdPerdorues, dbAdmin);
                    if (!mesazh.Status)
                        return mesazh;

                    scope.Complete();
                    return new MesazhSuksesi(MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                }
                catch (Exception ce)
                {
                    return new MesazhGabimi(ce.Message);
                }
            }
        }

        /// <summary>
        /// Modifikimi i obj clsPerdorues konsiston ne modifikim e te gjithe atributeve te ketij objekti
        /// pra edhe te colectionit me te drejta, dmth na duhet te modifikojme disa objekte te vecanta por te grupuara si atribute te perdoruesit
        /// Perdoret transaksion brenda te cilit perfshijme:
        /// 1- modifikimin e perdorursit ne tabelen T_PERDORUES sipas prc prc_T_PERDORUESI_upd sipas ID se tij
        /// 2- fshirje te te gjitha te drejtave qe i takojne ketij perdoruesi (IDPERDORUESI) ne T_DREJTAT
        /// 3- shtim i te drejtave te reja qe merren nga colection i te drejava     
        /// </summary>
        /// <param name="perdorues"></param>
        /// <returns>kthen statusin e e ekzekutimit te SP-se nepermjet objektit clsMesazh</returns>
        public clsMesazh modifikoPerdoruesTeDrejta(clsPerdorues perdorues, string PassIRi = "true", string passwordiGjeneruar = "", int idNdermarje = 0, bool kushtiMA = false, int idNdermarjeVit = 0, bool kushtiSHTR = true, string idDokImporti = "", bool ngaImporti = false, string emerTabKoka = "", string ndermarrjeKey = "", string primarykey = "")
        {
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer, MyConnectionsManager.GetConNameServer());
            //dbManager.ConnectionString = dbManager.GetConnectionString();
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            using (var scope = new MyTransactionScope(dbAdmin))
            {
                try
                {
                    if (!dbAdmin.ekzistonPerdoruesMeKeteKod(perdorues.PerdoruesUsername))
                        return new MesazhGabimi("Nuk ekziston asnje perdorues me kete kod!");

                    mesazh = dbAdmin.modifikoPerdorues(perdorues.IdPerdorues, perdorues.IdQyteti, perdorues.IdGjuha, perdorues.EmriPerdorues, perdorues.MbiemriPerdorues, perdorues.PerdoruesAktiv, perdorues.PerdoruesUsername, perdorues.PerdoruesPassword, perdorues.PerdoruesTel, perdorues.PerdoruesFax, perdorues.PerdoruesEmail, perdorues.PerdoruesAdresa, perdorues.IdPerdoruesi, perdorues.idStilRaporti, perdorues.idStatusDok, perdorues.zoomFactor, perdorues.infoHapur, perdorues.idAmbjent, perdorues.kerkeseResetPass, perdorues.passwordIPerkohshem, perdorues.perdoruesIKycur, perdorues.dateKrijimiPassword, perdorues.KontrollPassword, perdorues.ExportFormat, perdorues.ExportMode, perdorues.ShfaqDtPrintimi, perdorues.KycurMobile, perdorues.ShfaqPerdoruesMenu, perdorues.ShfaqMesazhePopup, perdorues.idAmbjentMobile, perdorues.shopCode, perdorues.shopName, perdorues.dealerName, perdorues.useriCRM, perdorues.userEtopUP, perdorues.iDETopUp, perdorues.typeOfDevice, perdorues.salesRepMobileNumber, perdorues.salesRepMPesaMSISDN, perdorues.gjinia, perdorues.salesRepStartDateVod, perdorues.salesRepTrainingStart, perdorues.salesRepStartDateShop, perdorues.salesRepMaternityLeaveStart, perdorues.leaveDateVod, perdorues.leaveDateShop, perdorues.maternityLeaveEndDate, perdorues.trainingEndDate, perdorues.commentsRetailSales, perdorues.accountExecutive, perdorues.iDNumber, perdorues.isInsured, perdorues.commentsRetailOpSpecialist, perdorues.regionalSupervisor, perdorues.retailSalesAccountExecutive, perdorues.retailSalesAreaManager, perdorues.birthdate, perdorues.siteCode, perdorues.district, perdorues.shopMainCode, perdorues.latitude, perdorues.longitude, perdorues.status, perdorues.leaveReason, perdorues.uniform, perdorues.shenime, perdorues.statusAprovimi, perdorues.njoftimEmailAprovim, perdorues.idKonfigKasa);
                    if (!mesazh.Status)
                        throw new Exception(mesazh.PershkrimMesazhi);

                    clsMesazh msgShtimPassNeHistorik = shtoPasswordNeHistorik(PassIRi, perdorues.IdPerdorues, perdorues.PerdoruesPassword, perdorues.IdPerdoruesi, dbAdmin);
                    if (!msgShtimPassNeHistorik.Status)
                        return msgShtimPassNeHistorik;
                    if (!String.IsNullOrEmpty(passwordiGjeneruar))
                    {
                        mesazh = EmailComposer.DergoEmailFjalekaliminEGjeneruar(perdorues.PerdoruesEmail, perdorues.perdoruesUsername, passwordiGjeneruar, perdorues.idPerdorues, dbAdmin);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                    if (kushtiSHTR)
                    {
                        colRolPerdorues rolpereksistues = new colRolPerdorues();
                        rolpereksistues.mbushRolePerdoruesSipasPerdoruesi(perdorues.IdPerdorues);
                        foreach (clsRolPerdorues rp in rolpereksistues)
                        {
                            mesazh = dbAdmin.fshiRolPerdorues(rp.IdRolPerdorues);
                            if (!mesazh.Status)
                                return mesazh;
                        }

                        foreach (clsRolPerdorues o in perdorues.OColRolPerdoruesi)
                        {
                            int id = dbAdmin.krijoRolPerdorues(o.IdRoli, perdorues.IdPerdorues);
                            if (id == -1)
                                return new clsMesazh(false, "Gabim gjate modifikimit te rolit!");
                        }

                        mesazh = DbDashboard.clsDashboard.LidhPerdoruesMeDashboardDefault(new DbDashboard.clsDatabaseDashboard(dbAdmin),perdorues.IdPerdorues);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                    if (autorizimet != null && autorizimet.Any())
                    {
                        autorizimet.ForEach(auto => auto.IdPerdorues = this.IdPerdorues);
                        mesazh = autorizimet.Ruaj(this.IdPerdorues, dbAdmin);
                        if (!mesazh)
                            return mesazh;
                    }
                    /// dergimi me email tek aprovuesit
                    // 0. kontrollo nese statusi i perdoruesit eshte Per aprovim dhe kushti Me Aprovim eshte PO
                    if ((perdorues.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Per_Aprovim || perdorues.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Aprovuar
                        || perdorues.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Refuzuar) && kushtiMA)
                    {
                        clsFunksione.dergoKerkesePerAprovimPerdoruesi(perdorues.PerdoruesUsername, ktheEmerMbiemer(perdorues.IdPerdoruesi), idNdermarje, perdorues.IdPerdoruesi, idNdermarjeVit, ((StatusAprovimi)perdorues.StatusAprovimi).ToString().Replace('_', ' '));
                    }
                    if (ngaImporti && idDokImporti != string.Empty)
                    {
                        var dbRegj = new clsDatabaseRegjistrim(dbAdmin);
                        var statusi = 1;
                        mesazh = dbRegj.updateDokTabeleTemportal(idDokImporti, idNdermarje, statusi, emerTabKoka, primarykey, ndermarrjeKey);

                        if (!mesazh.Status)
                            return mesazh;
                    }
                    scope.Complete();
                    return new MesazhSuksesi(MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                }
                catch (Exception ce)
                {
                    return new MesazhGabimi(ce.Message);
                }
            }
        }
        public CultureInfo MerrCultureInfo()
        {
            return MessagesResource.KtheCultureInfo(idGjuha);
        }
        private clsMesazh shtoPasswordNeHistorik(string PassIRi, int idPerdoruesi, string password, int idPerdoruesiLoguar, clsDatabaseAdmin dbAdmin)
        {
            if (PassIRi.Equals("true"))
            {
                DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi, dbAdmin);
                if (konfigPass.RuajHistorikunPass)
                {
                    bool suksesRuajPassHistorik = konfigPass.shtoPassNeHistorik(idPerdoruesi, password, idPerdoruesiLoguar, dbAdmin);
                    if (!suksesRuajPassHistorik)
                    {
                        dbAdmin.rollbackTransaksion();
                        return new clsMesazh(false, "Ndodhi nje gabim. Ruajtja nuk u krye!");
                    }
                }
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// objekti perdorues permban atributet col per te drejtat dhe per autorizimet
        /// veprimet e fshirjes se nje bashkesie rreshtat ne DB ne tabela te ndryshme perfshihen brenda nje Transasioni
        /// </summary>
        /// <param name="perdorues"></param>
        /// <returns> statusi i ekzekutimit te SP-ve sipas objektit clsMesazh</returns>
        public clsMesazh fshiPerdoruesinDheTeDrejtatDheAutorizimet(clsPerdorues perdorues)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            using(var scope = new MyTransactionScope(dbAdmin))
            {
                try
                {
                    mesazh = dbAdmin.fshiAutorizimSipasPerdorues(perdorues.IdPerdorues);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                    colRolPerdorues rolpereksistues = new colRolPerdorues();
                    rolpereksistues.mbushRolePerdoruesSipasPerdoruesi(perdorues.IdPerdorues);
                    foreach (clsRolPerdorues rp in rolpereksistues)
                    {
                        mesazh = dbAdmin.fshiRolPerdorues(rp.IdRolPerdorues);
                        if (!mesazh.Status)
                            return mesazh;
                    }

                    mesazh = DbDashboard.clsDashboard.LidhPerdoruesMeDashboardDefault(new DbDashboard.clsDatabaseDashboard(dbAdmin), perdorues.IdPerdorues);
                    if (!mesazh.Status)
                        return mesazh;

                    mesazh = dbAdmin.fshiPerdorues(perdorues.IdPerdorues);
                    if (!mesazh.Status)
                        return mesazh;

                    scope.Complete();
                    return new MesazhSuksesi(MessagesResource.Messages["msgFshirjeMeSukses"]); ;
                }
                catch (Exception ce)
                {
                    return new MesazhGabimi(ce.Message);
                }
            }            
        }
        
        public static string ktheInfoHapur(int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrInfoHapur(idPerdoruesi);
            }
        }

        public static bool kthePerdoruesKycurMobile(int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrPerdoruesKycurMobile(idPerdoruesi);
            }
        }

        public static bool kthePerdoruesShfaqMesazhePopup(int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrPerdoruesShfaqMesazhePopup(idPerdoruesi);
            }
        }

        public static string ktheEmer(int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrEmerPerdoruesi(idPerdoruesi);
            }
        }

        /// <summary>
        /// merr te dhena per perdoruesin qe ka vleren myQueryRF te kolona KerkeseResetPass
        /// </summary>
        /// <param name="queryStringHashuarKerkeseResetPass"></param>
        /// <returns></returns>
        public static DataRow merrQueryStringHashuar(string queryStringHashuarKerkeseResetPass)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.merrQueryStringHashuar(queryStringHashuarKerkeseResetPass);
            }
        }

        /// <summary>
        /// ruan stilin e raportit per perdoruesin(idstili-n ne baze te stylename dhe zoom-in)
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="styleName"></param>
        /// <param name="zoom"></param>
        /// <param name="exportFormat"></param>
        /// <returns></returns>
        public static clsMesazh ruajStilRaporti(int idPerdoruesi, string styleName, float zoom, int exportFormat, int exportMode)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ruajStilRaporti(idPerdoruesi, styleName, zoom, exportFormat, exportMode);
            }
        }

        public static colRolPerdorues ruajRole(int idRoli)
        {
            colRolPerdorues roleper = new colRolPerdorues();
            clsRolPerdorues rp = new clsRolPerdorues();
            rp.IdRoli = idRoli;
            roleper.Add(rp);
            return roleper;
        }

        /// <summary>
        /// kyc perdoruesin qe ka kaluar numrin max te tentative te login
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static bool kycPerdorues(int idPerdoruesi)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.kycPerdorues(idPerdoruesi);
            }
        }

        public clsPerdorues krijoPerdoruesPerImport(bool dshtim, bool dmod, clsLicenca licenca, int nrPerdoruesishPerLicence, int idndermarje, string emriperdorues, string mbiemriperdorues, string perdoruesusername, string perdoruespassword, string perdoruespasswordkonfirmo, string kontrollPassword, string gjuha, string perdoruesaktiv, string rolet, string llojiVeprimit, string qyteti, string perdoruestel, string perdoruesfax, string perdoruesemail, string perdoruesadresa, string perdoruesiKycur, string passwordiPerkohshem, string fjalekalimiekzistues, string kodNdermarje, string kerkeseResetPass, string usericrm, string userEtopuP, string iDEtopUp, string typeofDevice, string salesrepMobileNumber, string salesrepTrainingStart, string salesrepMPesaMSISDN, string salesrepStartDateShop, string salesrepMaternityLeaveStart, string leavedateVod, string leavedateShop, string maternityleaveEndDate, string trainingendDate, string gjinia, string commentsretailSales, string accountexecutive, string idNumber, string isinsured, string commentsretailOpSpecialist, string regionalsupervisor, string retailsalesAccountExecutive, string retailsalesAreaManager, string birthDate, string sitecode, string districti, string shopmainCode, string longitude, string latitude, string status, string uniform, string leaveReason, string salesrepStartDateVod, string autorizime, int idperdoruesi, string statusAprovimi, string channel, bool boshLeaveDateShop)
        {
            string error = "";
            autorizimet = new colAutorizimetTrupi();
            clsPerdorues perdorues = new clsPerdorues();
            System.Resources.ResourceManager rm = MessagesResource.CurrentResourceManager;
            System.Globalization.CultureInfo ci = MessagesResource.Messages.CurrentCultureInfo;
            try
            {
                if (llojiVeprimit.ToLower() == "modifikim")
                {
                    if (!dmod)
                    {
                        error = MessagesResource.Messages["msgNukKeniTeDrejta"];
                        throw new Exception(error);
                    }
                    if (!ekzistonPerdoruesi(perdoruesusername)) { error = $"Perdoruesi nuk ekziston."; throw new Exception(error); }
                    perdorues = new clsPerdorues(perdoruesusername);
                    if (perdorues.PerdoruesAktiv == false && perdoruesaktiv.ToLower() == "true" && licenca.NrPerdoruesish < nrPerdoruesishPerLicence + 1) //ne rast se ai behet nga inaktiv ne aktiv, atehere duhet te kontrollojme nese tejkalohet nr i perdoruesve per licence
                    {
                        error = "Ju keni kaluar numrin e perdoruesve qe mund te regjistroni.";
                        throw new Exception(error);
                    }
                    if (mbiemriperdorues != "") perdorues.MbiemriPerdorues = mbiemriperdorues;
                    if (perdoruestel != "") { perdorues.perdoruesTelefon = perdoruestel; }
                    if (perdoruesfax != "") { perdorues.perdoruesFax = perdoruesfax; }
                    if (perdoruesadresa != "") { perdorues.perdoruesAdresa = perdoruesadresa; }
                    if (kerkeseResetPass != "") { perdorues.kerkeseResetPass = kerkeseResetPass; }
                    if (usericrm != "") { perdorues.useriCRM = usericrm; } //TO ASK: duhet shtuar kontroll ekzistence per userin 
                    if (userEtopuP != "") { perdorues.userEtopUP = userEtopuP; } //TO ASK: duhet shtuar kontroll ekzistence per userin 
                    if (iDEtopUp != "") { perdorues.iDETopUp = iDEtopUp; }
                    if (typeofDevice != "") { perdorues.typeOfDevice = typeofDevice; }
                    if (salesrepMobileNumber != "") { perdorues.salesRepMobileNumber = salesrepMobileNumber; }
                    if (salesrepMPesaMSISDN != "") { perdorues.salesRepMPesaMSISDN = salesrepMPesaMSISDN; }
                    if (commentsretailSales != "") { perdorues.commentsRetailSales = commentsretailSales; }
                    if (accountexecutive != "") { perdorues.accountExecutive = accountexecutive; }
                    if (idNumber != "") { perdorues.iDNumber = idNumber; }
                    if (commentsretailOpSpecialist != "") { perdorues.commentsRetailOpSpecialist = commentsretailOpSpecialist; }
                    if (regionalsupervisor != "") { perdorues.regionalSupervisor = regionalsupervisor; }
                    if (retailsalesAccountExecutive != "") { perdorues.retailSalesAccountExecutive = retailsalesAccountExecutive; }
                    if (retailsalesAreaManager != "") { perdorues.retailSalesAreaManager = retailsalesAreaManager; }
                    if (sitecode != "") { perdorues.siteCode = sitecode; }
                    if (districti != "") { perdorues.district = districti; }
                    if (shopmainCode != "") { perdorues.shopMainCode = shopmainCode; }
                    if (longitude != "") { perdorues.longitude = longitude; }
                    if (latitude != "") { perdorues.latitude = latitude; }
                }
                else
                {
                    if (!dshtim)
                    {
                        error = MessagesResource.Messages["msgNukKeniTeDrejta"];
                        throw new Exception(error);
                    }
                    if (licenca.NrPerdoruesish <= nrPerdoruesishPerLicence)
                    {
                        error = "Ju keni kaluar numrin e perdoruesve qe mund te regjistroni.";
                        throw new Exception(error);
                    }
                    if (ekzistonPerdoruesi(perdoruesusername))
                    {
                        error = $"Ekziston nje perdorues me kete username!";
                        throw new Exception(error);
                    }
                    if (perdoruespassword != perdoruespasswordkonfirmo)
                    {
                        error = $"Konfirmimi i fjalekalimit eshte i gabuar.";
                        throw new Exception(error);
                    }
                    perdorues.DateKrijimiPassword = DateTime.Now;
                    perdorues.PerdoruesPassword = PasswordHelper.HashLogin(perdoruesusername, perdoruespassword);
                    DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idperdoruesi);


                    clsMesazh mesazh = konfigPass.isValidPassword(perdoruespassword, perdorues.PerdoruesPassword, ci, rm, "shtim");
                    if (!mesazh.Status)
                    {
                        error = mesazh.PershkrimMesazhi;
                        throw new Exception(error);
                    }
                    perdorues.IdStilRaporti = 2;
                    perdorues.infoHapur = true;
                    perdorues.mbiemriPerdorues = mbiemriperdorues;
                    perdorues.perdoruesUsername = perdoruesusername;
                    perdorues.perdoruesTelefon = perdoruestel;
                    perdorues.perdoruesFax = perdoruesfax;
                    perdorues.perdoruesAdresa = perdoruesadresa;
                    perdorues.kerkeseResetPass = kerkeseResetPass;
                    perdorues.useriCRM = usericrm;  //TO ASK: duhet shtuar kontroll ekzistence per userin?
                    perdorues.userEtopUP = userEtopuP;  //TO ASK: duhet shtuar kontroll ekzistence per userin? 
                    perdorues.iDETopUp = iDEtopUp;
                    perdorues.typeOfDevice = typeofDevice;
                    perdorues.salesRepMPesaMSISDN = salesrepMPesaMSISDN;
                    perdorues.commentsRetailSales = commentsretailSales;
                    perdorues.accountExecutive = accountexecutive;
                    perdorues.commentsRetailOpSpecialist = commentsretailOpSpecialist;
                    perdorues.regionalSupervisor = regionalsupervisor;
                    perdorues.retailSalesAccountExecutive = retailsalesAccountExecutive;
                    perdorues.retailSalesAreaManager = retailsalesAreaManager;
                    perdorues.siteCode = sitecode;
                    perdorues.district = districti;
                    perdorues.shopMainCode = shopmainCode;
                    perdorues.longitude = longitude;
                    perdorues.latitude = latitude;
                    perdorues.idStatusDok = 1;
                }
                if (statusAprovimi != "") { perdorues.statusAprovimi = Convert.ToInt32((DbRegjistrim.StatusAprovimi)Enum.Parse(typeof(DbRegjistrim.StatusAprovimi), statusAprovimi)); }
                perdorues.IdPerdoruesi = idperdoruesi;
                perdorues.emriPerdorues = emriperdorues;
                perdorues.IdGjuha = (gjuha.ToLower() == "shqip") ? 0 : 1;
                Boolean.TryParse(perdoruesaktiv, out perdorues.perdoruesAktiv);
                Boolean.TryParse(kontrollPassword, out perdorues.kontrollPassword);
                if (perdoruesiKycur != "") { perdorues.perdoruesIKycur = (perdoruesiKycur.ToLower() == "false") ? false : true; }
                if (passwordiPerkohshem != "") Boolean.TryParse(passwordiPerkohshem, out perdorues.passwordIPerkohshem);
                if (gjinia != "") { perdorues.gjinia = (gjinia == "Femer") ? 1 : 2; }
                if (isinsured != "") { perdorues.isInsured = (isinsured == "Po") ? 1 : 2; }
                if (salesrepTrainingStart != "") { perdorues.salesRepTrainingStart = Convert.ToDateTime(salesrepTrainingStart); }
                if (salesrepStartDateShop != "") { perdorues.salesRepStartDateShop = Convert.ToDateTime(salesrepStartDateShop); }
                if (salesrepMaternityLeaveStart != "") { perdorues.salesRepMaternityLeaveStart = Convert.ToDateTime(salesrepMaternityLeaveStart); }
                if (leavedateVod != "") { perdorues.leaveDateVod = Convert.ToDateTime(leavedateVod); }
                if (leavedateShop != "") { perdorues.leaveDateShop = Convert.ToDateTime(leavedateShop); }
                if (boshLeaveDateShop)
                { perdorues.leaveDateShop = DateTime.MinValue; }
                if (maternityleaveEndDate != "") { perdorues.maternityLeaveEndDate = Convert.ToDateTime(maternityleaveEndDate); }
                if (trainingendDate != "") { perdorues.trainingEndDate = Convert.ToDateTime(trainingendDate); }
                if (birthDate != "") { perdorues.birthdate = Convert.ToDateTime(birthDate); }
                if (salesrepStartDateVod != "") { perdorues.SalesRepStartDateVod = Convert.ToDateTime(salesrepStartDateVod); }
                if (rolet != "")
                {
                    var role = rolet.Split(',');
                    colRolPerdorues rolePerPerdorues = new colRolPerdorues();
                    foreach (string rolKodi in role)
                    {
                        int idRoli = clsRoli.ktheIdRoliSipasKodit(rolKodi.Trim(), licenca.IdLicenca, idperdoruesi);
                        if (idRoli == -1) { error = $"Roli me kod {rolKodi.Trim()} nuk ekziston!"; throw new Exception(error); }
                        clsRolPerdorues roliPerPerdorues = new clsRolPerdorues();
                        roliPerPerdorues.IdRoli = idRoli;
                        roliPerPerdorues.IdPerdorues = perdorues.IdPerdorues;
                        rolePerPerdorues.Add(roliPerPerdorues);
                    }
                    perdorues.OColRolPerdoruesi = rolePerPerdorues;
                }
                if (qyteti != "")
                {
                    //int idQyteti =  clsQyteti.ktheIdQytetiSipasEmritDheNdermarrjes(qyteti, idndermarje);
                    int idQyteti = clsQyteti.ktheIdQytetiSipasEmritDheNdermarrjes(qyteti, -1);
                    if (idQyteti == -1) { error = $"Qyteti {qyteti} nuk ekziston!"; throw new Exception(error); }
                    perdorues.idQyteti = idQyteti;
                }
                if (status != "")
                {
                    if (!clsShopsHierarkiStatus.ekzistonStatusMeKetePershkrim(status)) { error = $"Statusi {status} nuk ekziston!"; throw new Exception(error); }
                    clsShopsHierarkiStatus hierarkiStatus = new clsShopsHierarkiStatus(status);
                    perdorues.status = hierarkiStatus.IdStatus;
                }
                if (uniform != "")
                {
                    if (!clsShopsHierarkiUniform.ekzistonUniformMeKetePershkrim(uniform)) { error = $"Uniforma {uniform} nuk ekziston!"; throw new Exception(error); }
                    clsShopsHierarkiUniform hierarkiUniform = new clsShopsHierarkiUniform(uniform);
                    perdorues.uniform = hierarkiUniform.IdUniform;
                }
                if (leaveReason != "")
                {
                    if (!clsShopsHierarkiLeaveReason.ekzistonLeaveReasonMeKetePershkrim(leaveReason)) { error = $"Arsyeja {leaveReason} nuk ekziston!"; throw new Exception(error); }
                    clsShopsHierarkiLeaveReason hierarkiLeaveReason = new clsShopsHierarkiLeaveReason(leaveReason);
                    perdorues.leaveReason = hierarkiLeaveReason.IdLeaveReason;
                }
                if (perdoruesemail != "")
                {
                    if (DbShare.clsAlternativaKushti.getAlternativa(Convert.ToInt32(DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("PER", idndermarje)), "VFE") == "Po" && !DbCore.clsFunksione.isValidEmail(out error, perdoruesemail)) { error = $"E-mail nuk eshte ne formatin e duhur!"; throw new Exception(error); }
                    perdorues.perdoruesEmail = perdoruesemail;
                }
                if (idNumber != "")
                {
                    if (!clsFunksione.isValidNrPersonal(idNumber, out error))
                        throw new Exception(error);
                    perdorues.IDNumber = idNumber;
                }
                if (salesrepMobileNumber != "")
                {
                    if (!clsFunksione.isValidMSISDN(salesrepMobileNumber, out error))
                        throw new Exception(error);
                    perdorues.salesRepMobileNumber = salesrepMobileNumber;
                }
                if (autorizime != "")
                {
                    if (autorizime.EndsWith(",")) { autorizime = autorizime.Remove(autorizime.Length - 1); }
                    var autorizimeString = autorizime.Split(',');
                    foreach (string kodi in autorizimeString)
                    {
                        int idakoka = clsAutorizimKoka.ktheIDAutorizim(kodi.Trim());
                        if (idakoka == -1) { error = $"Autorizimi me kod {kodi.Trim()} nuk ekziston!"; throw new Exception(error); }
                        clsAutorizimTrupi at = new clsAutorizimTrupi();
                        at.IdAutorizimKoka = idakoka;
                        at.IdPerdorues = perdorues.IdPerdorues;
                        at.PerdoruesUsername = perdorues.PerdoruesUsername;
                        autorizimet.Add(at);
                    }

                    perdorues.autorizimet = this.autorizimet;
                }
                return perdorues;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static int ktheIdPerdoruesSipasUsername(string perdoruesUsername, int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return ktheIdPerdoruesSipasUsername(perdoruesUsername, idNdermarrje, data);
            }
        }
        public static int ktheIdPerdoruesSipasUsername(string perdoruesUsername, int idNdermarrje, clsDatabaseAdmin data)
        {
            return data.merrPerdoruesIdSipasUsername(perdoruesUsername, idNdermarrje);
        }


        

        public static int ktheIdPerdoruesSipasUsernamePateDrejta(string perdoruesUsername, int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return ktheIdPerdoruesSipasUsernamePateDrejta(perdoruesUsername, idNdermarrje, data);
            }
        }

        public static int ktheIdPerdoruesSipasUsernamePateDrejta(string perdoruesUsername, int idNdermarrje, clsDatabaseAdmin data)
        {
            return data.merrPerdoruesIdSipasUsernamePateDrejta(perdoruesUsername);
        }




        public static int ktheGjuhePerdoruesi(int idPerdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrIdGjuhaSipasIdPerdorues(idPerdorues);
            }
        }





        public static string merrEmailPerRoletRASipasPerdoruesDheKodRol(int idPerdorues, string kodRoli) {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrEmailPerRoletRASipasPerdoruesDheKodRol(idPerdorues, kodRoli);
            }
        }

        public static string merrKodeteRolevesipasPerdoruesit(int idPerdorues, int idLicenca)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKodeteRolevesipasPerdoruesit(idPerdorues, idLicenca);
            }
        }

        /// <summary>
        /// Kthen nje status qe tregon nese ka perdoruesi ka licence per ndermarrjen
        /// </summary>
        public static bool kaLicencePerIdNdermarje(int idNdermarje, int idPerdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool kaLicence = data.kaLicence(idNdermarje, idPerdorues);
            data.Dispose();
            return kaLicence;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush perdoruesit nga databaza
        /// </summary>
        /// <param name="dbDataRowPerdorues">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushPerdorues(DataRow dbDataRowPerdorues)
        {
            if (dbDataRowPerdorues != null)
            {
                try
                {
                    int.TryParse(dbDataRowPerdorues["IDPERDORUES"].ToString(), out idPerdorues);
                    int.TryParse(dbDataRowPerdorues["IDGJUHA"].ToString(), out idGjuha);
                    int.TryParse(dbDataRowPerdorues["IDQYTETI"].ToString(), out idQyteti);
                    emriPerdorues = dbDataRowPerdorues["PERDORUESEMRI"].ToString();
                    mbiemriPerdorues = dbDataRowPerdorues["PERDORUESMBIEMRI"].ToString();
                    bool.TryParse(dbDataRowPerdorues["PERDORUESAKTIV"].ToString(), out perdoruesAktiv);
                    perdoruesUsername = dbDataRowPerdorues["PERDORUESUSERNAME"].ToString();
                    perdoruesPassword = dbDataRowPerdorues["PERDORUESPASSWORD"].ToString();
                    perdoruesTelefon = dbDataRowPerdorues["PERDORUESTEL"].ToString();
                    perdoruesFax = dbDataRowPerdorues["PERDORUESFAX"].ToString();
                    perdoruesEmail = dbDataRowPerdorues["PERDORUESEMAIL"].ToString();
                    perdoruesAdresa = dbDataRowPerdorues["PERDORUESADRESA"].ToString();
                    int.TryParse(dbDataRowPerdorues["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowPerdorues["IDTHEME"].ToString(), out idTheme);
                    int.TryParse(dbDataRowPerdorues["IDSTILRAPORTI"].ToString(), out idStilRaporti);
                    //backPath = dbDataRowPerdorues["PATH"].ToString();
                    int.TryParse(dbDataRowPerdorues["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowPerdorues["IDAMBJENT"].ToString(), out idAmbjent);
                    DateTime.TryParse(dbDataRowPerdorues["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowPerdorues["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    stilRaportiFileName = dbDataRowPerdorues["FILENAME"].ToString();
                    float.TryParse(dbDataRowPerdorues["ZOOMFACTOR"].ToString(), out zoomFactor);
                    bool.TryParse(dbDataRowPerdorues["INFOHAPUR"].ToString(), out infoHapur);
                    kerkeseResetPass = dbDataRowPerdorues["KerkeseResetPass"].ToString();
                    if (dbDataRowPerdorues["PASSWORDIPERKOHSHEM"] != DBNull.Value)
                        passwordIPerkohshem = Convert.ToBoolean(dbDataRowPerdorues["PASSWORDIPERKOHSHEM"]);
                    if (dbDataRowPerdorues["KYCUR"] != DBNull.Value)
                        perdoruesIKycur = Convert.ToBoolean(dbDataRowPerdorues["KYCUR"]);
                    if (!(dbDataRowPerdorues["DATEKRIJIMIPASSWORD"] is System.DBNull))
                        dateKrijimiPassword = Convert.ToDateTime(dbDataRowPerdorues["DATEKRIJIMIPASSWORD"]);
                    bool.TryParse(dbDataRowPerdorues["KONTROLLOPASSWORD"].ToString(), out kontrollPassword);
                    if (!(dbDataRowPerdorues["SHFAQDTPRINTIMI"] is System.DBNull))
                        shfaqDtPrintimi = Convert.ToBoolean(dbDataRowPerdorues["SHFAQDTPRINTIMI"]);
                    bool.TryParse(dbDataRowPerdorues["SHFAQDTPRINTIMI"].ToString(), out shfaqDtPrintimi);
                    if (dbDataRowPerdorues["EXPORTFORMAT"] != DBNull.Value)
                        exportFormat = Convert.ToInt32(dbDataRowPerdorues["EXPORTFORMAT"]);
                    if (dbDataRowPerdorues["EXPORTMODE"] != DBNull.Value)
                        exportMode = Convert.ToInt32(dbDataRowPerdorues["EXPORTMODE"]);
                    if (!(dbDataRowPerdorues["KYCURMOBILE"] is System.DBNull))
                        kycurMobile = Convert.ToBoolean(dbDataRowPerdorues["KYCURMOBILE"]);
                    bool.TryParse(dbDataRowPerdorues["SHFAQPERDORUESMENU"].ToString(), out shfaqPerdoruesMenu);
                    if (!(dbDataRowPerdorues["SHFAQMESAZHEPOPUP"] is System.DBNull))
                        shfaqMesazhePopup = Convert.ToBoolean(dbDataRowPerdorues["SHFAQMESAZHEPOPUP"]);
                    int.TryParse(dbDataRowPerdorues["IDAMBJENTMOBILE"].ToString(), out idAmbjentMobile);
                    userEtopUP = dbDataRowPerdorues["UserEtopUP"].ToString();
                    iDETopUp = dbDataRowPerdorues["IDETopUp"].ToString();
                    typeOfDevice = dbDataRowPerdorues["TypeOfDevice"].ToString();
                    salesRepMPesaMSISDN = dbDataRowPerdorues["SalesRepMPesaMSISDN"].ToString();
                    salesRepMobileNumber = dbDataRowPerdorues["SalesRepMobileNumber"].ToString();
                    int.TryParse(dbDataRowPerdorues["Gjinia"].ToString(), out gjinia);
                    DateTime.TryParse(dbDataRowPerdorues["SalesRepStartDateVod"].ToString(), out salesRepStartDateVod);
                    DateTime.TryParse(dbDataRowPerdorues["SalesRepTrainingStart"].ToString(), out salesRepTrainingStart);
                    DateTime.TryParse(dbDataRowPerdorues["SalesRepStartDateShop"].ToString(), out salesRepStartDateShop);
                    DateTime.TryParse(dbDataRowPerdorues["SalesRepMaternityLeaveStart"].ToString(), out salesRepMaternityLeaveStart);
                    DateTime.TryParse(dbDataRowPerdorues["LeaveDateVod"].ToString(), out leaveDateVod);
                    DateTime.TryParse(dbDataRowPerdorues["LeaveDateShop"].ToString(), out leaveDateShop);
                    DateTime.TryParse(dbDataRowPerdorues["MaternityLeaveEndDate"].ToString(), out maternityLeaveEndDate);
                    DateTime.TryParse(dbDataRowPerdorues["TrainingEndDate"].ToString(), out trainingEndDate);
                    commentsRetailSales = dbDataRowPerdorues["CommentsRetailSales"].ToString();
                    accountExecutive = dbDataRowPerdorues["AccountExecutive"].ToString();
                    iDNumber = dbDataRowPerdorues["IDNumber"].ToString();
                    int.TryParse(dbDataRowPerdorues["IsInsured"].ToString(), out isInsured);
                    commentsRetailOpSpecialist = dbDataRowPerdorues["CommentsRetailOpSpecialist"].ToString();
                    regionalSupervisor = dbDataRowPerdorues["RegionalSupervisor"].ToString();
                    retailSalesAccountExecutive = dbDataRowPerdorues["RetailSalesAccountExecutive"].ToString();
                    retailSalesAreaManager = dbDataRowPerdorues["RetailSalesAreaManager"].ToString();
                    DateTime.TryParse(dbDataRowPerdorues["Birthdate"].ToString(), out birthdate);
                    siteCode = dbDataRowPerdorues["SiteCode"].ToString();
                    district = dbDataRowPerdorues["District"].ToString();
                    shopMainCode = dbDataRowPerdorues["ShopMainCode"].ToString();
                    latitude = dbDataRowPerdorues["Latitude"].ToString();
                    longitude = dbDataRowPerdorues["Longitude"].ToString();
                    int.TryParse(dbDataRowPerdorues["IDHIERARKISTATUS"].ToString(), out status);
                    int.TryParse(dbDataRowPerdorues["IDHIERARKILEAVEREASON"].ToString(), out leaveReason);
                    int.TryParse(dbDataRowPerdorues["IDHIERARKIUNIFORM"].ToString(), out uniform);
                    shenime = dbDataRowPerdorues["Shenime"].ToString();
                    int.TryParse(dbDataRowPerdorues["StatusAprovimi"].ToString(), out statusAprovimi);
                    if (dbDataRowPerdorues["NjoftimEmailAprovim"] != DBNull.Value)
                        njoftimEmailAprovim = Convert.ToBoolean(dbDataRowPerdorues["NjoftimEmailAprovim"]);
                    oColRolPerdoruesi = new colRolPerdorues();
                    oColRolPerdoruesi.mbushRolePerdoruesSipasPerdoruesi(idPerdorues);
                    int.TryParse(dbDataRowPerdorues["IDKONFIGKASA"].ToString(), out idKonfigKasa);


                    // change some store procedure or this will always return null
                    if (dbDataRowPerdorues.Table.Columns.Contains("OTP_Token"))
                        otp_token = dbDataRowPerdorues["OTP_Token"]?.ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se perdoruesit nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// mbush perdoruesit e gjithe ndermarrjeve nga databaza
        /// </summary>
        /// <param name="dbDataRowPerdoruesGjitheNderm">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushPerdoruesGjitheNder(DataRow dbDataRowPerdoruesGjitheNderm)
        {
            if (dbDataRowPerdoruesGjitheNderm != null)
            {
                try
                {
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDPERDORUES"].ToString(), out idPerdorues);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDGJUHA"].ToString(), out idGjuha);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDQYTETI"].ToString(), out idQyteti);
                    emriPerdorues = dbDataRowPerdoruesGjitheNderm["PERDORUESEMRI"].ToString();
                    mbiemriPerdorues = dbDataRowPerdoruesGjitheNderm["PERDORUESMBIEMRI"].ToString();
                    bool.TryParse(dbDataRowPerdoruesGjitheNderm["PERDORUESAKTIV"].ToString(), out perdoruesAktiv);
                    perdoruesUsername = dbDataRowPerdoruesGjitheNderm["PERDORUESUSERNAME"].ToString();
                    perdoruesPassword = dbDataRowPerdoruesGjitheNderm["PERDORUESPASSWORD"].ToString();
                    perdoruesTelefon = dbDataRowPerdoruesGjitheNderm["PERDORUESTEL"].ToString();
                    perdoruesFax = dbDataRowPerdoruesGjitheNderm["PERDORUESFAX"].ToString();
                    perdoruesEmail = dbDataRowPerdoruesGjitheNderm["PERDORUESEMAIL"].ToString();
                    perdoruesAdresa = dbDataRowPerdoruesGjitheNderm["PERDORUESADRESA"].ToString();
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDSTILRAPORTI"].ToString(), out idStilRaporti);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    float.TryParse(dbDataRowPerdoruesGjitheNderm["ZOOMFACTOR"].ToString(), out zoomFactor);
                    if (dbDataRowPerdoruesGjitheNderm["PASSWORDIPERKOHSHEM"] != DBNull.Value)
                        passwordIPerkohshem = Convert.ToBoolean(dbDataRowPerdoruesGjitheNderm["PASSWORDIPERKOHSHEM"]);
                    if (dbDataRowPerdoruesGjitheNderm["KYCUR"] != DBNull.Value)
                        perdoruesIKycur = Convert.ToBoolean(dbDataRowPerdoruesGjitheNderm["KYCUR"]);
                    if (!(dbDataRowPerdoruesGjitheNderm["DATEKRIJIMIPASSWORD"] is System.DBNull))
                        dateKrijimiPassword = Convert.ToDateTime(dbDataRowPerdoruesGjitheNderm["DATEKRIJIMIPASSWORD"]);
                    bool.TryParse(dbDataRowPerdoruesGjitheNderm["KONTROLLOPASSWORD"].ToString(), out kontrollPassword);
                    if (!(dbDataRowPerdoruesGjitheNderm["SHFAQDTPRINTIMI"] is System.DBNull))
                        shfaqDtPrintimi = Convert.ToBoolean(dbDataRowPerdoruesGjitheNderm["SHFAQDTPRINTIMI"]);
                    bool.TryParse(dbDataRowPerdoruesGjitheNderm["SHFAQDTPRINTIMI"].ToString(), out shfaqDtPrintimi);
                    if (dbDataRowPerdoruesGjitheNderm["EXPORTFORMAT"] != DBNull.Value)
                        exportFormat = Convert.ToInt32(dbDataRowPerdoruesGjitheNderm["EXPORTFORMAT"]);
                    if (dbDataRowPerdoruesGjitheNderm["EXPORTMODE"] != DBNull.Value)
                        exportMode = Convert.ToInt32(dbDataRowPerdoruesGjitheNderm["EXPORTMODE"]);
                    if (!(dbDataRowPerdoruesGjitheNderm["KYCURMOBILE"] is System.DBNull))
                        kycurMobile = Convert.ToBoolean(dbDataRowPerdoruesGjitheNderm["KYCURMOBILE"]);
                    if (!(dbDataRowPerdoruesGjitheNderm["SHFAQMESAZHEPOPUP"] is System.DBNull))
                        shfaqMesazhePopup = Convert.ToBoolean(dbDataRowPerdoruesGjitheNderm["SHFAQMESAZHEPOPUP"]);
                    useriCRM = dbDataRowPerdoruesGjitheNderm["UseriCRM"].ToString();
                    userEtopUP = dbDataRowPerdoruesGjitheNderm["UserEtopUP"].ToString();
                    iDETopUp = dbDataRowPerdoruesGjitheNderm["IDETopUp"].ToString();
                    typeOfDevice = dbDataRowPerdoruesGjitheNderm["TypeOfDevice"].ToString();
                    salesRepMPesaMSISDN = dbDataRowPerdoruesGjitheNderm["SalesRepMPesaMSISDN"].ToString();
                    salesRepMobileNumber = dbDataRowPerdoruesGjitheNderm["SalesRepMobileNumber"].ToString();
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["Gjinia"].ToString(), out gjinia);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["SalesRepStartDateVod"].ToString(), out salesRepStartDateVod);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["SalesRepTrainingStart"].ToString(), out salesRepTrainingStart);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["SalesRepStartDateShop"].ToString(), out salesRepStartDateShop);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["SalesRepMaternityLeaveStart"].ToString(), out salesRepMaternityLeaveStart);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["LeaveDateVod"].ToString(), out leaveDateVod);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["LeaveDateShop"].ToString(), out leaveDateShop);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["MaternityLeaveEndDate"].ToString(), out maternityLeaveEndDate);
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["TrainingEndDate"].ToString(), out trainingEndDate);
                    commentsRetailSales = dbDataRowPerdoruesGjitheNderm["CommentsRetailSales"].ToString();
                    accountExecutive = dbDataRowPerdoruesGjitheNderm["AccountExecutive"].ToString();
                    iDNumber = dbDataRowPerdoruesGjitheNderm["IDNumber"].ToString();
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IsInsured"].ToString(), out isInsured);
                    commentsRetailOpSpecialist = dbDataRowPerdoruesGjitheNderm["CommentsRetailOpSpecialist"].ToString();
                    regionalSupervisor = dbDataRowPerdoruesGjitheNderm["RegionalSupervisor"].ToString();
                    retailSalesAccountExecutive = dbDataRowPerdoruesGjitheNderm["RetailSalesAccountExecutive"].ToString();
                    retailSalesAreaManager = dbDataRowPerdoruesGjitheNderm["RetailSalesAreaManager"].ToString();
                    DateTime.TryParse(dbDataRowPerdoruesGjitheNderm["Birthdate"].ToString(), out birthdate);
                    siteCode = dbDataRowPerdoruesGjitheNderm["SiteCode"].ToString();
                    district = dbDataRowPerdoruesGjitheNderm["District"].ToString();
                    shopMainCode = dbDataRowPerdoruesGjitheNderm["ShopMainCode"].ToString();
                    latitude = dbDataRowPerdoruesGjitheNderm["Latitude"].ToString();
                    longitude = dbDataRowPerdoruesGjitheNderm["Longitude"].ToString();
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDHIERARKISTATUS"].ToString(), out status);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDHIERARKILEAVEREASON"].ToString(), out leaveReason);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDHIERARKIUNIFORM"].ToString(), out uniform);
                    shenime = dbDataRowPerdoruesGjitheNderm["Shenime"].ToString();
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["StatusAprovimi"].ToString(), out statusAprovimi);
                    if (dbDataRowPerdoruesGjitheNderm["NjoftimEmailAprovim"] != DBNull.Value)
                        njoftimEmailAprovim = Convert.ToBoolean(dbDataRowPerdoruesGjitheNderm["NjoftimEmailAprovim"]);
                    int.TryParse(dbDataRowPerdoruesGjitheNderm["IDKONFIGKASA"].ToString(), out idKonfigKasa);


                    if (dbDataRowPerdoruesGjitheNderm.Table.Columns.Contains("OTP_Token"))
                        otp_token = dbDataRowPerdoruesGjitheNderm["OTP_Token"]?.ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se perdoruesit te te gjitha ndermarrjeve nga db-ja");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushPerdorues(clsPerdorues perdoruesi)
        {
            IdPerdorues = perdoruesi.IdPerdorues;
            IdGjuha = perdoruesi.IdGjuha;
            IdQyteti = perdoruesi.IdQyteti;
            EmriPerdorues = perdoruesi.EmriPerdorues;
            MbiemriPerdorues = perdoruesi.MbiemriPerdorues;
            PerdoruesAktiv = perdoruesi.PerdoruesAktiv;
            PerdoruesUsername = perdoruesi.PerdoruesUsername;
            PerdoruesPassword = perdoruesi.PerdoruesPassword;
            PerdoruesTel = perdoruesi.PerdoruesTel;
            PerdoruesFax = perdoruesi.PerdoruesFax;
            PerdoruesEmail = perdoruesi.PerdoruesEmail;
            PerdoruesAdresa = perdoruesi.PerdoruesAdresa;
            IdPerdoruesi = perdoruesi.IdPerdoruesi;
            IdStilRaporti = perdoruesi.IdStilRaporti;
            idStatusDok = perdoruesi.idStatusDok;
            IdAmbjent = perdoruesi.IdAmbjent;
            DtKrijimi = perdoruesi.DtKrijimi;
            DtModifikimi = perdoruesi.DtModifikimi;
            StilRaportiFileName = perdoruesi.StilRaportiFileName;
            ZoomFactor = perdoruesi.ZoomFactor;
            InfoHapur = perdoruesi.InfoHapur;
            KerkeseResetPass = perdoruesi.KerkeseResetPass;
            PasswordIPerkohshem = perdoruesi.PasswordIPerkohshem;
            PerdoruesIKycur = perdoruesi.PerdoruesIKycur;
            DateKrijimiPassword = perdoruesi.DateKrijimiPassword;
            KontrollPassword = perdoruesi.KontrollPassword;
            ShfaqDtPrintimi = perdoruesi.ShfaqDtPrintimi;
            ExportFormat = perdoruesi.ExportFormat;
            ExportMode = perdoruesi.ExportMode;
            KycurMobile = perdoruesi.KycurMobile;
            ShfaqPerdoruesMenu = perdoruesi.ShfaqPerdoruesMenu;
            ShfaqMesazhePopup = perdoruesi.ShfaqMesazhePopup;
            IdAmbjentMobile = perdoruesi.IdAmbjentMobile;
            ShopCode = perdoruesi.ShopCode;
            ShopName=perdoruesi.ShopName;
            DealerName=perdoruesi.DealerName;
            UseriCRM=perdoruesi.UseriCRM;
            UserEtopUP=perdoruesi.UserEtopUP;
            IDETopUp=perdoruesi.IDETopUp;
            TypeOfDevice=perdoruesi.TypeOfDevice;
            SalesRepMobileNumber=perdoruesi.SalesRepMobileNumber;
            SalesRepMPesaMSISDN=perdoruesi.SalesRepMPesaMSISDN;
            Gjinia=perdoruesi.Gjinia;
            SalesRepStartDateVod=perdoruesi.SalesRepStartDateVod;
            SalesRepTrainingStart=perdoruesi.SalesRepTrainingStart;
            SalesRepStartDateShop=perdoruesi.SalesRepStartDateShop;
            SalesRepMaternityLeaveStart=perdoruesi.SalesRepMaternityLeaveStart;
            LeaveDateVod=perdoruesi.LeaveDateVod;
            LeaveDateShop=perdoruesi.LeaveDateShop;
            MaternityLeaveEndDate=perdoruesi.MaternityLeaveEndDate;
            TrainingEndDate=perdoruesi.TrainingEndDate;
            CommentsRetailSales=perdoruesi.CommentsRetailSales;
            AccountExecutive=perdoruesi.AccountExecutive;
            IDNumber=perdoruesi.IDNumber;
            IsInsured=perdoruesi.IsInsured;
            CommentsRetailOpSpecialist=perdoruesi.CommentsRetailOpSpecialist;
            RegionalSupervisor=perdoruesi.RegionalSupervisor;
            RetailSalesAccountExecutive=perdoruesi.RetailSalesAccountExecutive;
            RetailSalesAreaManager=perdoruesi.RetailSalesAreaManager;
            Birthdate=perdoruesi.Birthdate;
            SiteCode=perdoruesi.SiteCode;
            District=perdoruesi.District;
            ShopMainCode=perdoruesi.ShopMainCode;
            Latitude=perdoruesi.Latitude;
            Longitude=perdoruesi.Longitude;
            Status=perdoruesi.Status;
            LeaveReason=perdoruesi.LeaveReason;
            Uniform=perdoruesi.Uniform;
            Shenime=perdoruesi.Shenime;
            StatusAprovimi=perdoruesi.StatusAprovimi;
            NjoftimEmailAprovim=perdoruesi.NjoftimEmailAprovim;
            IdKonfigKasa = perdoruesi.IdKonfigKasa;
            Otp_Token = perdoruesi.Otp_Token;

            return new clsMesazh(true, $"Mbushja e perdoruesit {perdoruesi.PerdoruesUsername} u be me sukses!");
        }

        #endregion
    }
}