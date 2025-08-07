using PlatinumWeb;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// Kjo klase sherben per objektet qe perfaqesojne konfigurimin e politikave te fjalekalimit. Konfigurimi eshte ne nivel licence.
    /// Te dhenat merren nga tabela T_KonfigurimeFjalekalimi
    /// </summary>
    public class clsKonfigurimeFjalekalimi
    {
        #region Atributet

        private int idKonfigurimePassword;
        private bool ruajHistorikunPass;
        private int nrHereRuajHistorikPass;
        private bool komplexPassword;
        private bool ndryshimPasswordiDetyruar;
        private int gjatesiaMinPassword;
        private int diteSkadimiPassword;
        private bool bllokoPerdorues;
        private int maxTentativaLoginXSession;
        private bool bllokoLogin;
        private int idPERDORUES;
        private int idLicenca;
        private DateTime dateKrijimi;
        private DateTime dateModifikimi;
        private int maxSesioneXPerdorues;
        private bool skadoPassword;
        private bool resetPassword;
        private bool twofactorauth;
        private bool gjeneroPassword;
        private int numbersChars;
        private int uppercaseChars;
        private int specialChars;

        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos numrin e karaketereve numra qe do te permbaje passwordi i perdoruesit
        /// </summary>
        public int NumbersChars
        {
            get { return numbersChars; }
            set { numbersChars = value; }
        }
        /// <summary>
        /// Kthen/Vendos numrin e shrkonjave te meda qe do te permbaje passwordi i perdoruesit
        /// </summary>
        public int UppercaseChars
        {
            get { return uppercaseChars; }
            set { uppercaseChars = value; }
        }
        /// <summary>
        /// Kthen/Vendos numrin e karaketereve speciale qe do te permbaje passwordi i perdoruesit
        /// </summary>
        public int SpecialChars
        {
            get { return specialChars; }
            set { specialChars = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese perdoruesi do lejohet te resetoje fjalekalimin e tij ne faqen e login 
        /// </summary>
        public bool ResetPassword
        {
            get { return resetPassword; }
            set { resetPassword = value; }
        }
        public bool Twofacorauth
        {
            get { return twofactorauth; }
            set { twofactorauth = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese passwordi i perdoruesve te licences duhet te skadoje
        /// </summary>
        public bool SkadoPassword
        {
            get { return skadoPassword; }
            set { skadoPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin maksimal te sesioneve per nje perdorues 
        /// </summary>
        public int MaxSesioneXPerdorues
        {
            get { return maxSesioneXPerdorues; }
            set { maxSesioneXPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit te konfigurimit te fjalekalimit 
        /// </summary>
        public DateTime DateModifikimi
        {
            get { return dateModifikimi; }
            set { dateModifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit  te konfigurimit per licencen qe perfshin perdoruesin qe ben konfigurimet
        /// </summary>
        public DateTime DateKrijimi
        {
            get { return dateKrijimi; }
            set { dateKrijimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e licences se konfigurimit
        /// </summary>
        public int IdLicenca
        {
            get { return idLicenca; }
            set { idLicenca = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e perdoruesit qe ka bere konfigurimin
        /// </summary>
        public int IdPERDORUES
        {
            get { return idPERDORUES; }
            set { idPERDORUES = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese perdoruesi duhet te kete vetem nje login aktiv. Qe do te thote nese perdoruesi logohet ne dy kompjutera ose browsera te ndryshem, nese e ka te konfiguruar bllokoLogin true, ath ne momentin qe logohet ruhet informacioni ne kete menyre HttpContext.Current.Application[trackUser.SessionID] = "yes", me pas perdoruesi lejohet te logohet diku tjeter, por ne pc ose browserin e pare, ne kerkesen e pare qe ben ai behet logout(shiko ktheIdPerdoruesi te mySessionObjects)
        /// </summary>
        public bool BllokoLogin
        {
            get { return bllokoLogin; }
            set { bllokoLogin = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin maksimal te tentativave  per nje session qe nje perdorues te logohet 
        /// </summary>
        public int TentativaBllokUserXSession
        {
            get { return maxTentativaLoginXSession; }
            set { maxTentativaLoginXSession = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese perdoruesi duhet te kycet ne rast se arrin numrin max te hereve per t'u loguar sipas konfigurimit TentativaBllokUserXSession dhe MaxSesioneXPerdorues
        /// </summary>
        public bool BllokoPerdorues
        {
            get { return bllokoPerdorues; }
            set { bllokoPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e diteve qe nje password te jete i vlefshem duke nisur qe nga dita e krijimit te tij. 
        /// </summary>
        public int DiteSkadimiPassword
        {
            get { return diteSkadimiPassword; }
            set { diteSkadimiPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin minimal te karaktereve qe duhet te kete passwordi
        /// </summary>
        public int GjatesiaMinPassword
        {
            get { return gjatesiaMinPassword; }
            set { gjatesiaMinPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese duhet te detyrohet perdoruesi qe logohet per here te pare ne sistem, te ndryshoje fillimisht fjalekalimin.
        /// </summary>
        public bool NdryshimPasswordiDetyruar
        {
            get { return ndryshimPasswordiDetyruar; }
            set { ndryshimPasswordiDetyruar = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese passwordi duhet te kete te jete komplex, duke permbushur sipas konfigurimit numrin e karaktereve te medha, numrave dhe karaktereve speciale perkatesisht UppercaseChars, NumbersChars dhe SpecialChars
        /// </summary>
        public bool KomplexPassword
        {
            get { return komplexPassword; }
            set { komplexPassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrine hereve qe do ruhet historiku i passwordeve per perdoruesit e licences
        /// </summary>
        public int NrHereRuajHistorikPass
        {
            get { return nrHereRuajHistorikPass; }
            set { nrHereRuajHistorikPass = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese duhet te ruhet historiku i passwordeve per licencen
        /// </summary>
        public bool RuajHistorikunPass
        {
            get { return ruajHistorikunPass; }
            set { ruajHistorikunPass = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-nee konfigurimit qe gjenerohet automatikisht.
        /// </summary>
        public int IdKonfigurimePassword
        {
            get { return idKonfigurimePassword; }
            set { idKonfigurimePassword = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese passwordi do te gjenerohet automatikisht ose jo me celjen e nje perdoruesi te ri
        /// </summary>
        public bool GjeneroPassword
        {
            get { return gjeneroPassword; }
            set { gjeneroPassword = value; }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Kontruktori pa parametra i klases
        /// </summary>
        public clsKonfigurimeFjalekalimi()
        {
        }

        /// <summary>
        /// mbush konfigurimin e fjalekalimit sipas licences qe permban kete idPerdoruesi.
        /// </summary>
        /// 
        /// <param name="idPerdoruesi"></param>
        public clsKonfigurimeFjalekalimi(int idPerdoruesi)
        {
            clsDatabaseAdmin dbAdm = new clsDatabaseAdmin();
            bool sukses = true;
            sukses = mbushKonfigurimeFjalekalimi(dbAdm.merrKonfigurimeFjalekalimiSipasIdPerdoruesi(idPerdoruesi));
            dbAdm.Dispose();
            //if (!sukses) throw new MyException("Kjo licence nuk ka konfigurime per fjalekalimin!");
        }

        /// <summary>
        /// mbush konfigurimin e fjalekalimit sipas licences qe permban kete idPerdoruesi.
        /// </summary>
        /// 
        /// <param name="idPerdoruesi"></param>
        /// <param name="punonjes">true nese po thirret nga e payslip</param>
        public clsKonfigurimeFjalekalimi(int idPerdoruesi, bool punonjes)
        {
            clsDatabaseAdmin dbAdm = new clsDatabaseAdmin();
            bool sukses = true;
            if (punonjes)
                sukses = mbushKonfigurimeFjalekalimi(dbAdm.merrKonfigurimeFjalekalimiSipasIdPunonjesi(idPerdoruesi));
            else
                sukses = mbushKonfigurimeFjalekalimi(dbAdm.merrKonfigurimeFjalekalimiSipasIdPerdoruesi(idPerdoruesi));
            dbAdm.Dispose();
            //if (!sukses) throw new MyException("Kjo licence nuk ka konfigurime per fjalekalimin!");
        }

        /// <summary>
        /// mbush konfigurimin e fjalekalimit sipas perdoruesit
        /// perdoret ky konstruktor ne rastin kur mbushja e konfigurimit do behet ne transaksion 
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="dbAdm"></param>
        public clsKonfigurimeFjalekalimi(int idPerdoruesi, clsDatabaseAdmin dbAdm)
        {
            bool sukses = mbushKonfigurimeFjalekalimi(dbAdm.merrKonfigurimeFjalekalimiSipasIdPerdoruesi(idPerdoruesi));
        }
        /// <summary>
        /// mbush konfigurimin e fjalekalimit sipas perdoruesit
        /// perdoret ky konstruktor ne rastin kur mbushja e konfigurimit do behet ne transaksion 
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="dbAdm"></param>
        public clsKonfigurimeFjalekalimi(int idPunonjes, clsDatabaseAdmin dbAdm, bool punonjes)
        {
            bool sukses = mbushKonfigurimeFjalekalimi(dbAdm.merrKonfigurimeFjalekalimiSipasIdPunonjesi(idPunonjes));
        }

        /// <summary>
        /// Kontruktori me parametra i klases
        /// </summary>
        public clsKonfigurimeFjalekalimi(int idKonfigurimePassword, bool ruajHistorikunPass, int nrHereRuajHistorikPass, bool komplexPassword, bool ndryshimPasswordiDetyruar, int gjatesiaMinPassword, int diteSkadimiPassword, bool bllokoPerdorues, int tentativaBllokPerdorues, bool bllokoLogin, int idPERDORUES, int maxSession, bool skadoPassword, bool resetPassword, bool gjeneroPassword, int nrSpecialChars, int nrShkronjaTeMedha, int nrNumraNePass,bool twofactorauth)
        {
            this.idKonfigurimePassword = idKonfigurimePassword;
            this.ruajHistorikunPass = ruajHistorikunPass;
            this.nrHereRuajHistorikPass = nrHereRuajHistorikPass;
            this.komplexPassword = komplexPassword;
            this.ndryshimPasswordiDetyruar = ndryshimPasswordiDetyruar;
            this.gjatesiaMinPassword = gjatesiaMinPassword;
            this.diteSkadimiPassword = diteSkadimiPassword;
            this.bllokoPerdorues = bllokoPerdorues;
            this.maxTentativaLoginXSession = tentativaBllokPerdorues;
            this.bllokoLogin = bllokoLogin;
            this.idPERDORUES = idPERDORUES;
            this.maxSesioneXPerdorues = maxSession;
            this.skadoPassword = skadoPassword;
            this.resetPassword = resetPassword;
            this.twofactorauth = twofactorauth;
            this.gjeneroPassword = gjeneroPassword;
            this.specialChars = nrSpecialChars;
            this.uppercaseChars = nrShkronjaTeMedha;
            this.numbersChars = nrNumraNePass;
        }


        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbDataRowKonfigurimeFjalekalimi"></param>
        /// <returns></returns>
        internal bool mbushKonfigurimeFjalekalimi(DataRow dbDataRowKonfigurimeFjalekalimi)
        {
            if (dbDataRowKonfigurimeFjalekalimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["IDKONFIGURIMEPASSWORD"].ToString(), out idKonfigurimePassword);
                    ruajHistorikunPass = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["RUAJHISTORIKUNPASS"]);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["NRHERERUAJHISTORIKPASS"].ToString(), out nrHereRuajHistorikPass);
                    komplexPassword = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["KOMPLEXPASSWORD"]);
                    ndryshimPasswordiDetyruar = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["NDRYSHIMPASSWORDIDETYRUAR"]);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["GJATESIAMINPASSWORD"].ToString(), out gjatesiaMinPassword);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["DITESKADIMIPASSWORD"].ToString(), out diteSkadimiPassword);
                    bllokoPerdorues = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["BLLOKOPERDORUES"]);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["MAXTENTATIVALOGINxSESSION"].ToString(), out maxTentativaLoginXSession);
                    bllokoLogin = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["BLLOKOLOGIN"]);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["IDPERDORUES"].ToString(), out idPERDORUES);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["IDLICENCA"].ToString(), out idLicenca);
                    if (!(dbDataRowKonfigurimeFjalekalimi["DATEKRIJIMI"] is System.DBNull))
                        dateKrijimi = Convert.ToDateTime(dbDataRowKonfigurimeFjalekalimi["DATEKRIJIMI"]);
                    if (!(dbDataRowKonfigurimeFjalekalimi["DATEMODIFIKIMI"] is System.DBNull))
                        dateModifikimi = Convert.ToDateTime(dbDataRowKonfigurimeFjalekalimi["DATEMODIFIKIMI"]);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["MAXSESSIONxUSER"].ToString(), out maxSesioneXPerdorues);
                    if (!(dbDataRowKonfigurimeFjalekalimi["SKADOPASSWORD"] is System.DBNull))

                        skadoPassword = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["SKADOPASSWORD"]);
                    if (!(dbDataRowKonfigurimeFjalekalimi["RESETOPASSWORD"] is System.DBNull))
                        resetPassword = Convert.ToBoolean(dbDataRowKonfigurimeFjalekalimi["RESETOPASSWORD"]);
                        twofactorauth = false;
                    gjeneroPassword = false;
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["NRSPECIALCHARSINPASS"].ToString(), out specialChars);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["NRUPPERCASECHARSINPASS"].ToString(), out uppercaseChars);
                    int.TryParse(dbDataRowKonfigurimeFjalekalimi["NRNUMBERSCHARSINPASS"].ToString(), out numbersChars);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("ERROR: Gabim casti gjate marrjes se konfigurimit nga databaza!");
                }
                catch (Exception)
                {
                    throw new MyException("ERROR: Gabim gjate marrjes se konfigurimit nga databaza!");
                }
            }
            else
                return false;
        }

        #endregion

        #region Metoda Publike
        public bool mbushKonfigurime(int idPunonjes)
        {

            throw new NotImplementedException(); //TODO LORETA
        }
        /// <summary>
        /// Kontrollon nese ka konfigurim fjalekalimi per licencen qe perfshin kete perdorues
        /// </summary>
        /// <returns>kthen idLicencen e perdoruesi ne rastin kur ekziston konfigurimi, 0 perndryshe</returns>
        public static int kaKonfigurimPerLicencen(int idPerdoruesi)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int idLicenca = dbAdmin.kaKonfigurimeFjalekalimiPerLicencen(idPerdoruesi);
            dbAdmin.Dispose();
            return idLicenca;
        }

        /// <summary>
        /// Ruan objektin e konfigurimit te fjalekalimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajKonfigurimFjalekalimi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int id;
            clsMesazh u_ruajt = dbAdmin.ruajKonfigurimFjalekalimi(out id, ruajHistorikunPass, nrHereRuajHistorikPass, komplexPassword, ndryshimPasswordiDetyruar, gjatesiaMinPassword, diteSkadimiPassword, bllokoPerdorues, maxTentativaLoginXSession, bllokoLogin, idPERDORUES, maxSesioneXPerdorues, skadoPassword, resetPassword, gjeneroPassword, specialChars, uppercaseChars, numbersChars,twofactorauth);
            this.IdKonfigurimePassword = id;
            dbAdmin.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e konfigurimit te fjalekalimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoKonfigurimFjalekalimi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(int idLicenca)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh u_modifikua = dbAdmin.modifikoKonfigurimFjalekalimi(idLicenca, ruajHistorikunPass, nrHereRuajHistorikPass, komplexPassword, ndryshimPasswordiDetyruar, gjatesiaMinPassword, diteSkadimiPassword, bllokoPerdorues, maxTentativaLoginXSession, bllokoLogin, idPERDORUES, maxSesioneXPerdorues, skadoPassword, resetPassword, gjeneroPassword, specialChars, uppercaseChars, numbersChars,twofactorauth);
            dbAdmin.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// mbush konfigurimin e fjalekalimit sipas idPerdoruesit nqs ekziston konfigurimi per licencen e ketij perdoruesi
        /// mbush konfigurimin default perndryshe
        /// </summary>
        /// <param name="idPerdoruesit"></param>
        /// <returns></returns>
        public bool mbushKonfigurimSipasPerdoruesit(int idPerdoruesit)
        {
            clsDatabaseAdmin dbAdm = new clsDatabaseAdmin();
            bool sukses = mbushKonfigurimeFjalekalimi(dbAdm.merrKonfigurimFjalekalimiSipasPerdoruesit(idPerdoruesit));
            dbAdm.Dispose();
            return sukses;
        }

        /// <summary>
        /// kontrollon nese passwordi i perdoruesit eshte i perdorur me pare
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="password">pass i cili do kontrollohet nese eshte perdoret ne keto heret e fundit</param>
        /// <returns></returns>
        public clsMesazh ekzistonKyPassPerdoruesi(int idPerdorues, string password,ResourceManager rm,CultureInfo ci)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                return dbAdm.eshtePassVjeter(idPerdorues, this.nrHereRuajHistorikPass, password,rm,ci);
            }
        }
        public clsMesazh ekzistonKyPassPunonjesi(int idPunonjes, string password, ResourceManager rm, CultureInfo ci)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                return dbAdm.eshtePassVjeterPunonjes(idPunonjes, this.nrHereRuajHistorikPass, password,rm,ci);
            }
        
        }

        /// <summary>
        /// kontrollon nese perdoruesi eshte i loguar me pare ne sistem(nga nodnje kompjuter tjeter, ose browser tjeter, etj.)
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <returns>kthen true nqs ka perdorues me te njejtin username qe eshte i loguar, pra aktiv ne sistem</returns>
        private bool eshtePerdoruesILoguar(int idPerdorues)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                return dbAdm.perdoruesILoguar(idPerdorues);
            }
        }

        /// <summary>
        /// kontrollon nese passwordi i perdoruesit ka skaduar ose jo
        /// </summary>
        /// <param name="perdorues"></param>
        /// <returns></returns>
        public clsMesazh kontrolloSkadencenEPass(clsPerdorues perdorues)
        {
            //if (ekzistonKyPass(perdorues.IdPerdorues, password))
            //    return new clsMesazh(false, "Ky fjalekalim eshte perdorur me pare. Ju lutemi, zgjidhni nje fjalekalim te ri!");
            //if (this.komplexPassword)
            //return IsStrongPassword(password);
            //if(perdorues.PerdoruesIKycur)
            //    return new clsMesazh(false, "Ky perdorues eshte i kycur! Ju lutemi, kontaktoni me administratorin!");
            //if (this.bllokoLogin)
            //{
            //    if (eshtePerdoruesILoguar(perdorues.IdPerdorues))
            //        return new clsMesazh(false, "Ky perdorues eshte aktualisht i loguar ne sistem!");
            //}
            //DbCore.DbAdmin.clsPassword_History pass_Hist = new DbCore.DbAdmin.clsPassword_History();
            //TimeSpan diffTime = Convert.ToDateTime(pass.DateKrijimi.AddDays(this.DiteSkadimiPassword), new System.Globalization.CultureInfo("en-us")) - DateTime.Now;
            //if (diffTime.TotalDays > 0)
            if (this.skadoPassword)
            {
                //PlatinumWeb.clsPassword_History pass = new PlatinumWeb.clsPassword_History(perdorues.IdPerdorues, password);
                DateTime sot = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                //DateTime dateSkadimi = pass.DateKrijimi.AddDays(this.DiteSkadimiPassword);
                DateTime dateSkadimi = perdorues.DateKrijimiPassword.AddDays(this.DiteSkadimiPassword);
                int diteSkaduar = Convert.ToInt32(dateSkadimi.Subtract(sot).TotalDays);
                //TimeSpan diffTime = DateTime.Now - perdorues.DateKrijimiPassword.AddDays(this.DiteSkadimiPassword);
                if (diteSkaduar <= 0)
                //if (diffTime.TotalDays > 0)
                //if (DateTime.Now > Convert.ToDateTime(pass.DateKrijimi.AddDays(this.DiteSkadimiPassword), new System.Globalization.CultureInfo("en-us")))
                //    if (pass.DateKrijimi.AddDays(this.DiteSkadimiPassword) > DateTime.Now)
                {
                    return new clsMesazh(false, "Fjalekalimi juaj ka skaduar , ju duhet ta ndryshoni ate!");
                    //System.Web.HttpContext.Current.Response.Redirect("Default.aspx?SkaduarPass=true");
                }
                else
                    return new clsMesazh(true, "Passwordi eshte i vlefshem!");
            }
            return new clsMesazh(true);
        }

        /// <summary>
        /// ruan fjalekalimin e perdoruesit ne historik te tabela T_PASSWORD_HISTORY
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="password"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool shtoPassNeHistorik(int idPerdorues, string password, int idPerdoruesiLoguar, clsDatabaseAdmin db)
        {
            if (this.ruajHistorikunPass)
                return clsPassword_History.shtoPassNeHistorik(idPerdorues, password, this.nrHereRuajHistorikPass, idPerdoruesiLoguar, db);
            else return false;
        }
        /// <summary>
        /// ruan fjalekalimin e punonjesit ne historik te tabela T_PASSWORD_HISTORY_PUNONJES
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="password"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool shtoPassNeHistorikPunonjes(int idPunonjes, string password, int idPerdoruesiLoguar, clsDatabaseAdmin db)
        {
            if (this.ruajHistorikunPass)
                return clsPassword_History.shtoPassNeHistorikPunonjes(idPunonjes, password, this.nrHereRuajHistorikPass, idPerdoruesiLoguar, db);
            else return false;
        }

        /// <summary>
        /// Passwordi nuk duhet te korrespondoje me asnje nga password-et e meperashem te perdoruesit.
        /// Qe nje password te quhet i forte duhet te kete nje nga kombinimet e meposhtme:
        /// 1. gjatesia >nrMin, germe te madhe, germe te vogel, numer
        /// 2. gjatesia >nrMin, germe te madhe, germe te vogel, simbol
        /// 3. gjatesia >nrMin, germe te madhe, germe te vogel, simbol, numer
        /// 4. gjatesi  >nrMin,  numer,simbol
        /// </summary>
        /// <param name="password">Passwordi qe duam te kontrollojme</param>
        /// <returns></returns>
        public clsMesazh IsStrongPassword(string password, int nrSpecialChars, int nrUppercaseLetters, int nrNumberChars, System.Globalization.CultureInfo ci, System.Resources.ResourceManager rm)
        {
            string alphaCaps = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string numerics = "1234567890";
            string special = "~!@#$%^&*()_+|{}:\"<>?`-=\\[];',./";
            //bool numer = false;
            //bool germeKapitale = false;
            //bool germeEvogel = false;
            //bool simbol = false;
            //string mesazh = "Fjalëkalimi nuk plotëson minimumin e gjatësisë ose kompleksitetin e kërkuar!";
            string mesazh = rm.GetString("labelFjalekalimipermban", ci) + " " + this.gjatesiaMinPassword + " " + rm.GetString("labelKaraktere", ci) + " " + nrUppercaseLetters + " " + rm.GetString("labelShkronjaKapitale", ci) + " " + nrNumberChars + rm.GetString("labelNumra", ci) + " " + nrSpecialChars + " " + rm.GetString("labelKaraktereSpeciale", ci);
            //kur gjatesia eshte < se min i kerkuar nuk kontrollojme kushtet e tjera.
            if (password.Length < this.gjatesiaMinPassword)
                //return new clsMesazh(false,"Gjatesia e passwordit duhet te jene minimalisht " + this.gjatesiaMinPassword + "karaktere!");
                return new clsMesazh(false, mesazh);

            //Kontrollojme nqs passwordi korespondon me ndonje password te meparshem te perdoruesit.
            //if (oldPasswords.Any(c => password.Equals(c)))
            //    //return  new clsMesazh(false, "Ky password perputhet me nje password te meparshem");
            //    return false;

            if (password.Count(c => alphaCaps.Contains(c)) < nrUppercaseLetters)
                return new clsMesazh(false, mesazh);
            if (password.Count(c => numerics.Contains(c)) < nrNumberChars)
                return new clsMesazh(false, mesazh);
            if (password.Count(c => special.Contains(c)) < nrSpecialChars)
                return new clsMesazh(false, mesazh);
            return new clsMesazh(true);
            // Te pakten nje numer.
            //if (password.Any(c => char.IsDigit(c)))
            //    numer = true;

            //// Te pakten nje germe kapitale
            //if (password.Any(c => char.IsUpper(c)))
            //    germeKapitale = true;

            // Te pakten nje germe te vogel
            //if (password.Any(c => char.IsLower(c)))
            //    germeEvogel = true;

            // Te pakten nje simbol
            //if (password.Any(c => !char.IsLetterOrDigit(c)))
            //    simbol = true;

            //nqs plotesohet nje nga kushtet passwordi eshte i forte.
            //if (((germeEvogel && germeKapitale) && (numer || simbol)) || (numer && simbol))
            //    return new clsMesazh(true);
            //else
            //    //return new clsMesazh(false, "Password i dobet");
            //    return new clsMesazh(false, mesazh);
        }

        /// <summary>
        /// Metode qe kontrollon password policy
        /// kontrollon nese fjalekalimi eshte i forte dhe nese eshte perdorur me pare nga perdoruesi
        /// </summary>
        /// <param name="textPassword">passwordi ne text</param>
        /// <param name="passwordHashuar">passwordi i hashuar</param>
        /// <param name="idPerdoruesi">id e perdoruesit mbi te cilin po kryhen veprime</param>
        /// <param name="veprimiMbiPerdoruesin">tregon cfare veprimi po kryhet me perdoruesin(shtim, modifikim apo klonim)</param>
        /// <returns></returns>
        public clsMesazh isValidPassword(string textPassword, string passwordHashuar, int idPerdoruesi, System.Globalization.CultureInfo ci, System.Resources.ResourceManager rm, string veprimiMbiPerdoruesin = "")
        {
            clsMesazh mesazh = new clsMesazh(true);
            if (this.komplexPassword)
                mesazh = IsStrongPassword(textPassword, this.SpecialChars, this.UppercaseChars, this.NumbersChars,ci,rm);
            if (this.RuajHistorikunPass)//kontrollon nese eshte konfiguruar qe te ruhet historiku i passwordeve per kete perdorues
            {


                 clsMesazh mesazhHistoriku ;

                //kontrollon nese passwordi eshte i perdorur me pare
                if (!(veprimiMbiPerdoruesin == "shtim" || veprimiMbiPerdoruesin == "klonim")) //rasti kur po modifikohet perdoruesi
                {
                    
                if (veprimiMbiPerdoruesin == "epayslip")
                      mesazhHistoriku = this.ekzistonKyPassPunonjesi(idPerdoruesi, passwordHashuar,rm,ci);
                else
                     mesazhHistoriku = this.ekzistonKyPassPerdoruesi(idPerdoruesi, passwordHashuar,rm,ci);
                    if (!mesazhHistoriku.Status)
                        mesazh = mesazhHistoriku;
                }

            }
            return mesazh;
        }

        /// <summary>
        /// Metode qe kontrollon password policy
        /// kontrollon nese fjalekalimi eshte i forte. Kjo sherben ne rastin kur perdoruesi po shtohet i ri, dhe ska nevoje te kontrollohet historiku i passswordeve
        /// </summary>
        /// <param name="textPassword">passwordi ne text</param>
        /// <param name="passwordHashuar">passwordi i hashuar</param>
        /// <param name="veprimiMbiPerdoruesin">tregon cfare veprimi po kryhet me perdoruesin(shtim, modifikim apo klonim)</param>
        /// <returns></returns>
        public clsMesazh isValidPassword(string textPassword, string passwordHashuar, System.Globalization.CultureInfo ci, System.Resources.ResourceManager rm, string veprimiMbiPerdoruesin = "")
        {
            return isValidPassword(textPassword, passwordHashuar, -1, ci,rm, veprimiMbiPerdoruesin);
        }
        #endregion
    }
}
