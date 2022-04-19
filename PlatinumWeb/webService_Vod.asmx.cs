using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using System.Data;

using NLog;
using DbCore.Integrime;
using System.Globalization;
using DbCore.DbInventari;
using System.Resources;
using DbCore.DbImporte;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    /// <summary>
    /// Summary description for webService_Vod
    /// </summary>
    [WebService(Namespace = "http://alphaweb.al/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class webService_Vod : System.Web.Services.WebService
    {
        private static Logger logu = LogManager.GetCurrentClassLogger();
        private enum statusVeprimi
        {
            Aktive = 0, //ri
            Inaktive = 1, //modifikim
            Deleted = 2 //fshirje
        }

        private const string PARAMETER_NAME = "enc=";
        [WebMethod]
        public string merrInfo()
        {

            var selfCare = new PromocioneAdapter();
            var pike = 0;
            var msisdn = "355692201968";
            var info = selfCare.MerrInfoPerNumrin(msisdn, out pike);


            return "OK";
        }
        [WebMethod]
        public string DergoDhurate(string kodArtikulli)
        {

            var selfCare = new PromocioneAdapter();
            var pike = 0;
            var msisdn = "355692201968";
            var info = selfCare.DergoDhuratenEZgjedhur(msisdn, kodArtikulli);
            return "OK";
        }

        #region Metoda Publike

        /// <summary>
        /// Menaxhon krijimin, update dhe fshirjen e userave nga integrimi me eSales, VODAFONE
        /// </summary>
        /// <param name="useraMenaxhim">List me userat qe duhet te menaxhohen</param>
        /// <param name="loginAutentikim">Useri qe duhet te jete i krijuar ne fillin ne WEB qe eshte pergjegjes per transferimin</param>
        /// <param name="enc">Koha e hyrjes e enkriptuar me klasen clsEnDecVodafone</param>
        /// <returns>Kthen nje list mesazhesh ku ka qene i suksesshem dhe ku ka deshtuar</returns>
        [System.Web.Services.WebMethod(EnableSession = false)]
        public List<clsMesazh> menaxhimUserash(List<clsShtoUserVodafone> useraMenaxhim, string loginAutentikim, string enc)
        {
            bool mesazhSuksesi = true;
            List<DbCore.clsMesazh> listaMesazheve = new List<clsMesazh>();
            clsMesazh pergjigjaAlphaWEB = Autentifiko(loginAutentikim,"", enc);
            if (!pergjigjaAlphaWEB.Status)
            {
                listaMesazheve.Add(pergjigjaAlphaWEB);
                mesazhSuksesi = false;
                return listaMesazheve;
            }
            //Marrja e userit
            //Duhet specifikuar kush do te jete useri qe do te kryej keto transferimet
            DbCore.DbAdmin.clsPerdorues userKrijuesi = new DbCore.DbAdmin.clsPerdorues();
            userKrijuesi.kthePerdoruesSipasUsername(loginAutentikim);
            //Marrja e ndermarrjes
            int idNdermarrje = DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrjeMeme();
            if (idNdermarrje == -1 || userKrijuesi.IdPerdorues <= 0)
            {
                listaMesazheve.Add(new clsMesazh(false, "Nuk ekziston ndermarrja meme ne kete server ose useri pergjegjes per transferimet!"));
                mesazhSuksesi = false;
                return listaMesazheve;
            }

            foreach (DbCore.clsShtoUserVodafone user in useraMenaxhim)
            {
                //Deklarimi i objekteve per ruajtje
                DbCore.DbAdmin.clsPerdorues userNeWEB = new DbCore.DbAdmin.clsPerdorues();
                //Merr perdorues nese ekziston
                userNeWEB.kthePerdoruesSipasUsername(user.PerdoruesUsername);

                pergjigjaAlphaWEB = kaloTeDhenatNePerdoruesWEB(userNeWEB, user, userKrijuesi.IdPerdorues, idNdermarrje);
                if (!pergjigjaAlphaWEB.Status)
                {
                    listaMesazheve.Add(pergjigjaAlphaWEB);
                    mesazhSuksesi = false;
                }

                if (!String.IsNullOrEmpty(user.Dyqani))
                {
                    pergjigjaAlphaWEB = krijoAutorizimPerDyqane(user.Dyqani, user, userKrijuesi.IdPerdorues);
                    if (!pergjigjaAlphaWEB.Status)
                    {
                        listaMesazheve.Add(pergjigjaAlphaWEB);
                        mesazhSuksesi = false;
                    }
                }
            }
            if (mesazhSuksesi)
                listaMesazheve.Add(new clsMesazh(true, "Menaxhimi i kerkuar per userat u krye me sukses!"));
            return listaMesazheve;
        }

        public List<DbCore.clsMesazh> menaxhimDyqanesh(List<DbCore.clsShtoDyqanVodafone> dyqaneMenaxhim, string loginAutentikim, string enc)
        {
            bool mesazhsSuksesi = true;
            List<DbCore.clsMesazh> listaMesazheve = new List<clsMesazh>();
            clsMesazh pergjigjaAlphaWEB = Autentifiko(loginAutentikim,"", enc);
            if (!pergjigjaAlphaWEB.Status)
            {
                listaMesazheve.Add(pergjigjaAlphaWEB);
                mesazhsSuksesi = false;
                return listaMesazheve;
            }
            //Marrja e perdoruesit
            DbCore.DbAdmin.clsPerdorues userKrijuesi = new DbCore.DbAdmin.clsPerdorues();
            userKrijuesi.kthePerdoruesSipasUsername(loginAutentikim);
            //Marrja ndermarrjes

            foreach (DbCore.clsShtoDyqanVodafone dyqani in dyqaneMenaxhim)
            {
                int idNdermarrjeLokale = DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrje(dyqani.Ndermarrja);
                if (idNdermarrjeLokale == -1 || userKrijuesi.IdPerdorues < 1)
                {
                    pergjigjaAlphaWEB.PershkrimMesazhi = "Nuk ekziston ndermarrja me kod " + dyqani.Ndermarrja + " ose useri pergjegjes per transferimet ne ate ndermarrje!";
                    pergjigjaAlphaWEB.Status = false;
                    mesazhsSuksesi = false;
                    listaMesazheve.Add(pergjigjaAlphaWEB);
                    continue;
                }
                //Deklarimi i objekteve per ruajtje
                //Kontrolli nese ekziston i autorizimit
                DbCore.DbAdmin.clsAutorizimKoka autorizimWEB = new DbCore.DbAdmin.clsAutorizimKoka(dyqani.Kodi);
                DbCore.DbRegjistrim.clsDegeAdministrative degaAdministrativeWEB = new DbCore.DbRegjistrim.clsDegeAdministrative(dyqani.Kodi, idNdermarrjeLokale);
                //Ruajtja
                pergjigjaAlphaWEB = krijoTeDhenaAutorizime(autorizimWEB, dyqani, userKrijuesi.IdPerdorues, idNdermarrjeLokale);
                if (!pergjigjaAlphaWEB.Status)
                {
                    listaMesazheve.Add(pergjigjaAlphaWEB);
                    mesazhsSuksesi = false;
                    pergjigjaAlphaWEB = new clsMesazh();
                }
                pergjigjaAlphaWEB = krijoTeDhenaDegeAdministrative(degaAdministrativeWEB, dyqani, userKrijuesi.IdPerdorues, idNdermarrjeLokale);
                if (!pergjigjaAlphaWEB.Status)
                {
                    listaMesazheve.Add(pergjigjaAlphaWEB);
                    mesazhsSuksesi = false;
                    pergjigjaAlphaWEB = new clsMesazh();
                }
                for (int i = 0; i < 2; i++)
                {
                    //Per cdo dyqan qe krijohet nga Vodafoni, do te krijohet nje magazine dyqani dhe nje magazine per ekspositor
                    //Kur i == 1, pra ne rastin kur do te ruhet magazina e ekspositorit do te ndryshoje kodi dhe pershkrimi i kesaj magazine
                    if (i == 1)
                    {
                        dyqani.Kodi = dyqani.Kodi + "EXP";
                        dyqani.Pershkrimi = dyqani.Pershkrimi + " EXPOSITOR";
                    }
                    DbCore.DbRegjistrim.clsNjesiAdministrative njesiAdministrativeWEB = new DbCore.DbRegjistrim.clsNjesiAdministrative()
                    {
                        /*Kontrolli nese ekziston i njesise administrative*/
                        IdNjesiAdministrative = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(dyqani.Kodi, idNdermarrjeLokale)
                    };
                    pergjigjaAlphaWEB = krijoTeDhenaNjesiAdministrative(njesiAdministrativeWEB, dyqani, userKrijuesi.IdPerdorues, idNdermarrjeLokale);
                    if (!pergjigjaAlphaWEB.Status)
                    {
                        listaMesazheve.Add(pergjigjaAlphaWEB);
                        mesazhsSuksesi = false;
                        pergjigjaAlphaWEB = new clsMesazh();
                    }
                }
                //Ruajtja e mesazheve
                listaMesazheve.Add(pergjigjaAlphaWEB);
            }
            if (mesazhsSuksesi)
                listaMesazheve.Add(new clsMesazh(true, "Menaxhimi i kerkuar per dyqanet u krye me sukses!"));
            return listaMesazheve;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsMesazh anulloRezervimet(string loginAutentikim, string passAutentikim)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                clsMesazh pergjigjaAlphaWEB = Autentifiko(loginAutentikim, passAutentikim);
                if (!pergjigjaAlphaWEB.Status)
                    return pergjigjaAlphaWEB;
                int rezervimet = colKokaShitje.hiqRezervimetLlotariKupon();
                if (rezervimet != 0)
                    pergjigjaAlphaWEB = new clsMesazh(true, String.Format("U anulluan {0} rezervime qe kane kaluar 10 dite qe nga porosia!", rezervimet));
                else
                    pergjigjaAlphaWEB = new clsMesazh(true, "Nuk ka asnje porosi qe ka kaluar 10 dite per tu anulluar!");
                return pergjigjaAlphaWEB;
            }
            return new clsMesazh(false, "Kerkesa eshte kryer nga nje vend i pa autorizuar!");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<clsMesazh> dergoArketimetNeBRM(string loginAutentikim, string passAutentikim)
        {
            logu.Info("Po dergohet nje kerkese per te derguar faturat ne web service-n e BRM-se... ");
            List<clsMesazh> pergjigjet = new List<clsMesazh>();
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                BrmAdapter brmService;

                clsMesazh pergjigjaAlphaWEB = Autentifiko(loginAutentikim, passAutentikim);
                if (!pergjigjaAlphaWEB.Status)
                {
                    logu.Error("Nje gabim ndodhi gjate autentifikimit te skedulerit me web service-n Alpha WEB!");
                    pergjigjet.Add(pergjigjaAlphaWEB);
                    return pergjigjet;
                }

                logu.Info("Thirrja e web service-t me URL {0}...", Convert.ToString(ConfigurationManager.AppSettings["brmUrl"]));

                try
                {
                    brmService = new BrmAdapter();
                }
                catch (Exception e)
                {
                    var messazhi = new clsMesazh(false, "Gabim gjate krijmit lidhjes me brm " + e);
                    logu.Error(messazhi.PershkrimMesazhi);
                    pergjigjet.Add(messazhi);
                    return pergjigjet;
                }

                pergjigjet = brmService.DergoArketimeBrm();
                return pergjigjet;
            }
            logu.Error("Kerkesa eshte kryer nga nje vend i pa autorizuar!");
            pergjigjet.Add(new clsMesazh(false, "Kerkesa eshte kryer nga nje vend i pa autorizuar!"));
            return pergjigjet;
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<clsMesazh> anulloArketimetBRM(string loginAutentikim, string passAutentikim)
        {//GTOCHECK ta shtoj ridi tek scheduleri si job 
            logu.Info("Po dergohet nje kerkese per te anulluar arketimet ne web service-n e BRM-se... ");
            List<clsMesazh> pergjigjet = new List<clsMesazh>();
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                clsMesazh pergjigjaAlphaWEB = Autentifiko(loginAutentikim, passAutentikim);
                if (!pergjigjaAlphaWEB.Status)
                {
                    logu.Error("Nje gabim ndodhi gjate autentifikimit te skedulerit me web service-n Alpha WEB!");
                    pergjigjet.Add(pergjigjaAlphaWEB);
                    return pergjigjet;
                }

                logu.Info("Thirrja e web service-t me URL {0}...", Convert.ToString(ConfigurationManager.AppSettings["brmUrl"]));

                BrmAdapter brmService;
                try
                {
                    brmService = new BrmAdapter();
                }
                catch (Exception e)
                {
                    logu.Error(e, "Gabim gjate krijmit te service-t new DbCore.BRMAdapterServices.BRMAdapterServices()!");
                    pergjigjet.Add(new clsMesazh(false, "Gabim gjate krijmit te service-t new DbCore.BRMAdapterServices.BRMAdapterServices()! " + e));
                    return pergjigjet;
                }

                pergjigjet = brmService.AnulloArketimeBrm();
                return pergjigjet;
            }
            logu.Error("Kerkesa eshte kryer nga nje vend i pa autorizuar!");
            pergjigjet.Add(new clsMesazh(false, "Kerkesa eshte kryer nga nje vend i pa autorizuar!"));
            return pergjigjet;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsMesazh dergoRaportMeEmail(string loginAutentikim, string passAutentikim)
        {


            string ip = HttpContext.Current.Request.UserHostAddress;
            //if (ip.Equals(ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"])) //skip per momentin
            //{
            //    clsMesazh pergjigjaAlphaWEB = Autentifiko(loginAutentikim, passAutentikim);
            //    if (!pergjigjaAlphaWEB.Status)
            //    {
            //        logu.Error("Nje gabim ndodhi gjate autentifikimit te skedulerit me web service-n Alpha WEB!");
            //        return pergjigjaAlphaWEB;
            //    }
            int idGjuha = 0, idPerdoruesi = 0, idNderViti = 0;
            int idNdermarrje = clsNdermarrje.ktheIdNdermarrjeMeme();
            return EmailComposer.dergoEmailRaportet(idGjuha, idPerdoruesi, idNderViti, idNdermarrje);
            //}
            //return new clsMesazh(false, "Kerkesa eshte kryer nga nje vend i pa autorizuar!");
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsMesazh importAutomatikPerdorues(string templateImporti, int idNdermarrje, int idPerdorues, int idVitNdermarrje)
        {
            try
            {
                if (String.IsNullOrEmpty(templateImporti))
                    return new DbCore.clsMesazh(false, "Mungon template i importit! Nuk u importua asnje rresht!");
                int idGjuheImporti = clsPerdorues.ktheGjuhePerdoruesi(idPerdorues);
                CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuheImporti);

                DataTable gabime = new DataTable();
                gabime.Columns.Add("Kodi");
                gabime.Columns.Add("Gabimi");
                gabime.Columns.Add("Rreshti");
                clsKonfigImporti konfigImp = new clsKonfigImporti(templateImporti, idNdermarrje);
                if (konfigImp.Id == 0)
                    return new DbCore.clsMesazh(false, String.Format("Template i importit ( {0} ) nuk ekziston! Nuk u importua asnje rresht!", konfigImp.Emer));


                DbCore.clsMesazh mesazh = DbCore.clsFunksione.importDokumenteshNgaWS(konfigImp, idNdermarrje, idPerdorues, ref gabime, idVitNdermarrje);
                return mesazh;
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new DbCore.clsMesazh(false, ex.Message);
            }
        }

        #endregion
        /// <summary>
        /// Kontrollon autentifikimin e perdoruesit te web servisit te VODAFONE
        /// </summary>
        /// <param name="loginAutentikim">String me nje user default</param>
        /// <param name="enc">Koha e hyrjes e enkriptuar me klasen clsEnDecVodafone</param>
        /// <returns>Kthen mesazh nese eshte kryer apo jo autentifikimi</returns>
        private clsMesazh Autentifiko(string loginAutentikim, string password, string enc)
        {
            DbCore.Integrime.clsWebServiceAuten wsAut = new DbCore.Integrime.clsWebServiceAuten(loginAutentikim, "", enc);
            if (!wsAut.IsAutentik())
            {
                return new clsMesazh(false, "Autentifikimi eshte i gabuar! Ju lutem vendosni nje celes te sakte!");
            }
            int sekondaTeToleruara = Convert.ToInt32(ConfigurationManager.AppSettings["toleroSekonda"]);
            TimeSpan diffTime = DateTime.Now - wsAut.LoginTime();
            if (diffTime.TotalSeconds > sekondaTeToleruara)
            {
                return new clsMesazh(false, "Celesi nuk eshte me i vlefshem!");
            }
            return new clsMesazh(true, "Autentifikimi u krye me sukses");
        }
        private clsMesazh Autentifiko(string loginAutentikim, string password)
        {
            DbCore.Integrime.clsWebServiceAuten wsAut = new DbCore.Integrime.clsWebServiceAuten(loginAutentikim, password);
            if (!wsAut.IsAutentik())
            {
                return new clsMesazh(false, "Autentifikimi eshte i gabuar! Ju lutem vendosni nje celes te sakte!");
            }
            return new clsMesazh(true, "Autentifikimi u krye me sukses");
        }

        /// <summary>
        /// Krijon autorizimin per dyqanin te userave, vetem nese ekziston dyqani
        /// </summary>
        /// <param name="dyqani">Kodi i dyqanit ku do krijohet autorizimi</param>
        /// <param name="user">Useri per te cilin do krijohet autorizimi</param>
        /// <param name="idUserKrijuesi">Id e userit qe po krijon autorizimin</param>
        /// <returns></returns>
        private clsMesazh krijoAutorizimPerDyqane(string dyqani, clsShtoUserVodafone user, int idUserKrijuesi)
        {
            clsMesazh pergjigjaAlphaWEB = new clsMesazh();
            DbCore.DbAdmin.clsAutorizimKoka autorizimWEB = new DbCore.DbAdmin.clsAutorizimKoka();
            //Merr autorizim nese ekziston
            autorizimWEB.KodiAutorizim = dyqani;
            autorizimWEB = autorizimWEB.merrAutorizimNgaKodi();
            //Ruaj
            if (autorizimWEB.IdAutorizimKoka <= 0)
            {
                pergjigjaAlphaWEB = new clsMesazh(false, "Nuk ekziston autorizimi me kod " + autorizimWEB.KodiAutorizim + "!");
                return pergjigjaAlphaWEB;
            }

            pergjigjaAlphaWEB = modifikoUseraAutorizime(autorizimWEB, user, idUserKrijuesi, false);

            if (pergjigjaAlphaWEB.Status)
            {
                if (user.Administrator != null)
                {
                    //Dhenia e autorizimit per administratoret per dyqanet nese nuk ekziston si autorizim
                    for (int i = 0; i < user.Administrator.Length; i++)
                    {
                        pergjigjaAlphaWEB = modifikoUseraAutorizime(autorizimWEB, user, idUserKrijuesi, true, i);
                        if (!pergjigjaAlphaWEB.Status)
                        {
                            return pergjigjaAlphaWEB;
                        }
                    }
                }
            }

            return pergjigjaAlphaWEB;
        }

        /// <summary>
        /// Ruan perdoruesit nga eTopUp ne AlphaWEB
        /// </summary>
        /// <param name="userWEB">Objekt i klases clsPerdorues. Perdoruesi qe do te ruhet ne AlphaWEB.</param>
        /// <param name="userETOPUP">Objekti i klases clsShtoUserVodafone. Perdoruesi i ardhur nga Vodafone</param>
        /// <param name="mesazh">Mesazhi nese ruajtja eshte bere e sakte ose jo.</param>
        /// <param name="idUser">Id e userit qe po kryen veprimin</param>
        /// <param name="idNdermarrje">Id e ndermarrjes ne te cilen duhet te kete akses useri qe po krijohet.</param>
        /// <returns>Kthen mesazh nese eshte kryer ose jo krijimi i userit te ri</returns>
        /// 

        [System.Web.Services.WebMethod(EnableSession = true)]
        private DbCore.clsMesazh kaloTeDhenatNePerdoruesWEB(DbCore.DbAdmin.clsPerdorues userWEB, DbCore.clsShtoUserVodafone userETOPUP, int idUser, int idNdermarrje)
        {
            //if (userWEB.IdPerdorues > 0 && userETOPUP.Status == (int)statusVeprimi.Aktive)
            //{
            //    return new clsMesazh(false, "Ekziston njehere perdoruesi me username " + userETOPUP.PerdoruesUsername + "!");
            //}

            if (userWEB.IdPerdorues < 1 && userETOPUP.Status == (int)statusVeprimi.Deleted)
            {
                return new clsMesazh(false, "Nuk ekziston perdoruesi me username " + userETOPUP.PerdoruesUsername + "! Prandaj nuk mund te fshihet"); ;
            }
            if (userWEB.IdPerdorues > 0)
            {
                if (userETOPUP.Status == (int)statusVeprimi.Deleted)
                {
                    return userWEB.fshi();
                }
            }
            userWEB.EmriPerdorues = userETOPUP.EmriPerdorues;
            userWEB.MbiemriPerdorues = userETOPUP.MbiemriPerdorues;
            if (userWEB.IdPerdorues < 1)
            {
                userWEB.PerdoruesUsername = userETOPUP.PerdoruesUsername;
                userWEB.PerdoruesPassword = PasswordHelper.HashLogin(userETOPUP.PerdoruesUsername, userETOPUP.PerdoruesPassword);                
                userWEB.IdQyteti = DbCore.DbAdmin.clsQyteti.ktheIdQytetiSipasEmerPerNdermMeme(userETOPUP.Qyteti);
                userWEB.PerdoruesAdresa = userETOPUP.PerdoruesAdresa;
                userWEB.PerdoruesEmail = userETOPUP.PerdoruesEmail;
                userWEB.PerdoruesTel = userETOPUP.PerdoruesTelefon;
                userWEB.PerdoruesFax = userETOPUP.PerdoruesFax;
                userWEB.IdGjuha = 0;

                userWEB.InfoHapur = true;
                userWEB.PerdoruesAktiv = userETOPUP.Status == (int)statusVeprimi.Aktive ? true : false;

                userWEB.IdPerdoruesi = idUser;
                userWEB.IdStatusDok = 1;
                //userWEB.IdTheme = 1;
                userWEB.IdStilRaporti = 2;
                userWEB.DateKrijimiPassword = DateTime.Now;
                userWEB.PerdoruesIKycur = false;
                userWEB.PasswordIPerkohshem = false;
                userWEB.KontrollPassword = false;
            }

            //Marrja e rolit
            DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            DbCore.DbAdmin.colRolPerdorues rolePer = new DbCore.DbAdmin.colRolPerdorues();
            DbCore.DbAdmin.clsRolPerdorues rp = new DbCore.DbAdmin.clsRolPerdorues();
            rp.IdRoli = DbCore.DbAdmin.clsRoli.ktheIdRoli(userETOPUP.RoliPerdoruesit, ndermarrja.IdLicenca);
            int idNdermarjeVit = mySessionObjects.ktheNdermarrjeVit(Session);

            if (rp.IdRoli == -1)
                return new clsMesazh(false, String.Format("Nuk ekziston roli {0} per licensen e ndermarrjes {1} qe i eshte bashkengjitur username-it!", userETOPUP.RoliPerdoruesit, ndermarrja.NdermarrjeKodi));

            rolePer.Add(rp);
            userWEB.OColRolPerdoruesi = rolePer;

            if (userWEB.IdPerdorues < 1)
            {
                return userWEB.ruaj(ndermarrja.IdNdermarrje, idNdermarjeVit);
            }
            return userWEB.modifiko();
        }

        /// <summary>
        /// Kryen lidhjen e userave me autorizimet
        /// </summary>
        /// <param name="autorizimWEB"></param>
        /// <param name="userETOPUP"></param>
        /// <param name="useri"></param>
        /// <param name="administrator"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private DbCore.clsMesazh modifikoUseraAutorizime(DbCore.DbAdmin.clsAutorizimKoka autorizimWEB, DbCore.clsShtoUserVodafone userETOPUP, int useri, bool administrator, int index = 0)
        {
            DbCore.DbAdmin.clsAutorizimTrupi trupi = new DbCore.DbAdmin.clsAutorizimTrupi();
            DbCore.DbAdmin.clsPerdorues userNeWEB = new DbCore.DbAdmin.clsPerdorues();

            if (administrator)
            {
                userNeWEB.kthePerdoruesSipasUsername(userETOPUP.Administrator[index]);
            }
            else
            {
                userNeWEB.kthePerdoruesSipasUsername(userETOPUP.PerdoruesUsername);
            }
            if (userNeWEB.IdPerdorues > 0)
            {
                trupi.IdPerdorues = userNeWEB.IdPerdorues;
                autorizimWEB.OColTrupi = new DbCore.DbAdmin.colAutorizimetTrupi(autorizimWEB.IdAutorizimKoka);
                if (autorizimWEB.OColTrupi.Count(a => a.IdPerdorues == trupi.IdPerdorues) == 0)
                    autorizimWEB.OColTrupi.Add(trupi);
                return autorizimWEB.modifikoAutorizimKokaAndTrupi(autorizimWEB);
            }
            return new clsMesazh(false, "Nuk ekziston perdoruesi me username " + userETOPUP.PerdoruesUsername + " si administrator ndermarrje!");
        }

        /// <summary>
        /// Krijimi i autorizimi nese nuk ekziston
        /// </summary>
        /// <param name="autorizimWEB"></param>
        /// <param name="dyqaniEtopUP"></param>
        /// <param name="mesazh"></param>
        /// <param name="useri"></param>
        /// <param name="ndermarrja"></param>
        /// <returns></returns>
        private DbCore.clsMesazh krijoTeDhenaAutorizime(DbCore.DbAdmin.clsAutorizimKoka autorizimWEB, DbCore.clsShtoDyqanVodafone dyqaniEtopUP, int useri, int ndermarrja)
        {
            //if (autorizimWEB.IdAutorizimKoka > 0 && dyqaniEtopUP.Status == (int)statusVeprimi.Aktive)
            //{
            //    mesazh.PershkrimMesazhi = "Ekziston njehere autorizimi me kod " + autorizimWEB.KodiAutorizim + "!";
            //    mesazh.StatusMesazhi = false;
            //    return mesazh;
            //}

            if (autorizimWEB.IdAutorizimKoka < 1 && dyqaniEtopUP.Status == (int)statusVeprimi.Deleted)
            {
                return new clsMesazh(false, "Nuk ekziston autorizimi me kod " + autorizimWEB.KodiAutorizim + "!");
            }

            if (autorizimWEB.IdAutorizimKoka > 0) //nqs ekziston
            {
                if (dyqaniEtopUP.Status == (int)statusVeprimi.Deleted)
                {
                    return autorizimWEB.fshi();
                }
            }
            else
                autorizimWEB.KodiAutorizim = dyqaniEtopUP.Kodi;
            autorizimWEB.PershkrimAutorizim = "Autorizim i krijuar per dyqanin me kod " + dyqaniEtopUP.Kodi + "!";
            autorizimWEB.IdNdermarje = ndermarrja;
            autorizimWEB.IdPerdoruesi = useri;
            autorizimWEB.IdStatusDok = 1;
            autorizimWEB.OColTrupi = new DbCore.DbAdmin.colAutorizimetTrupi();

            if (dyqaniEtopUP.Administrator != null)
            {
                //Dhenia e autorizimit per administratoret per dyqanet nese nuk ekziston si autorizim
                for (int i = 0; i < dyqaniEtopUP.Administrator.Length; i++)
                {
                    DbCore.DbAdmin.clsAutorizimTrupi trupi = new DbCore.DbAdmin.clsAutorizimTrupi();
                    DbCore.DbAdmin.clsPerdorues userNeWEB = new DbCore.DbAdmin.clsPerdorues();
                    userNeWEB.kthePerdoruesSipasUsername(dyqaniEtopUP.Administrator[0]);
                    if (userNeWEB.IdPerdorues > 0)
                    {
                        trupi.IdPerdorues = userNeWEB.IdPerdorues;
                        if (autorizimWEB.OColTrupi.Count(a => a.IdPerdorues == userNeWEB.IdPerdorues) == 0)
                            autorizimWEB.OColTrupi.Add(trupi);
                    }
                }
            }


            if (autorizimWEB.IdAutorizimKoka < 1)
            {
                return autorizimWEB.ruaj();
            }
            return autorizimWEB.modifiko();
        }

        /// <summary>
        /// Krijimi i deges administrative nese nuk ekziston
        /// </summary>
        /// <param name="degeAdministrativeWEB"></param>
        /// <param name="dyqaniEtopUP"></param>
        /// <param name="mesazh"></param>
        /// <param name="useri"></param>
        /// <param name="ndermarrja"></param>
        /// <returns></returns>
        private DbCore.clsMesazh krijoTeDhenaDegeAdministrative(DbCore.DbRegjistrim.clsDegeAdministrative degeAdministrativeWEB, DbCore.clsShtoDyqanVodafone dyqaniEtopUP, int useri, int ndermarrja)
        {

            //if (degeAdministrativeWEB.IdDegeAdministrative > 0 && dyqaniEtopUP.Status == (int)statusVeprimi.Aktive)
            //{
            //    return new clsMesazh(false,"Ekziston njehere dega administrative me kod " + degeAdministrativeWEB.Kodi + "!");
            //}

            if (degeAdministrativeWEB.IdDegeAdministrative < 1 && dyqaniEtopUP.Status == (int)statusVeprimi.Deleted)
            {
                return new clsMesazh(false, "Nuk ekziston dega administrative me kod " + degeAdministrativeWEB.Kodi + "!");
            }

            if (degeAdministrativeWEB.IdDegeAdministrative > 0)
            {
                if (dyqaniEtopUP.Status == (int)statusVeprimi.Deleted)
                {
                    return degeAdministrativeWEB.fshi();
                }
            }
            else //nqs i ri 
                degeAdministrativeWEB.Kodi = dyqaniEtopUP.Kodi;
            degeAdministrativeWEB.Pershkrimi = dyqaniEtopUP.Pershkrimi;
            degeAdministrativeWEB.Adresa = dyqaniEtopUP.Adresa;
            degeAdministrativeWEB.DateRegjistrimi = dyqaniEtopUP.DataAktivizimit;
            degeAdministrativeWEB.IdNdermarje = ndermarrja;
            degeAdministrativeWEB.IdPerdorues = useri;
            degeAdministrativeWEB.IdStatusDok = 1;
            degeAdministrativeWEB.Aktiv = dyqaniEtopUP.Status == (int)statusVeprimi.Aktive ? true : false;
            //Konfigurimi i Ambjentit. Duhet specifikuar kodi default qe duhet per krijimin.
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod("DA", ndermarrja);
            degeAdministrativeWEB.IdKonfig = konfig.IdKonfigAmbjente;
            if (degeAdministrativeWEB.IdDegeAdministrative < 1)
            {
                return degeAdministrativeWEB.ruaj();
            }
            return degeAdministrativeWEB.modifiko();
        }

        /// <summary>
        /// Krijimi i njesise administrative nese nuk ekziston
        /// </summary>
        /// <param name="njesiAdministrativeWEB"></param>
        /// <param name="dyqaniEtopUP"></param>
        /// <param name="mesazh"></param>
        /// <param name="useri"></param>
        /// <param name="ndermarrja"></param>
        /// <returns></returns>
        private DbCore.clsMesazh krijoTeDhenaNjesiAdministrative(DbCore.DbRegjistrim.clsNjesiAdministrative njesiAdministrativeWEB, DbCore.clsShtoDyqanVodafone dyqaniEtopUP, int useri, int ndermarrja)
        {
            //if (njesiAdministrativeWEB.IdNjesiAdministrative > 0 && dyqaniEtopUP.Status == (int)statusVeprimi.Aktive)
            //{
            //    mesazh.PershkrimMesazhi = "Ekziston njehere dega administrative me kod " + njesiAdministrativeWEB.Kodi + "!";
            //    mesazh.StatusMesazhi = false;
            //    return mesazh;
            //}
            if (njesiAdministrativeWEB.IdNjesiAdministrative < 1 && dyqaniEtopUP.Status == (int)statusVeprimi.Deleted)
            {
                return new clsMesazh(false, "Nuk ekziston dega administrative me kod " + njesiAdministrativeWEB.Kodi + "!");
            }
            if (njesiAdministrativeWEB.IdNjesiAdministrative > 0)
            {
                if (dyqaniEtopUP.Status == (int)statusVeprimi.Deleted)
                {
                    return njesiAdministrativeWEB.fshi(useri,ndermarrja);
                }
            }
            else
                njesiAdministrativeWEB.Kodi = dyqaniEtopUP.Kodi;
            njesiAdministrativeWEB.Pershkrimi = dyqaniEtopUP.Pershkrimi;
            njesiAdministrativeWEB.Adresa = dyqaniEtopUP.Adresa;
            njesiAdministrativeWEB.DateRegjistrimi = dyqaniEtopUP.DataAktivizimit;
            njesiAdministrativeWEB.IdNdermarje = ndermarrja;
            njesiAdministrativeWEB.IdPerdorues = useri;
            njesiAdministrativeWEB.IdStatusDok = 1;
            njesiAdministrativeWEB.NdjekjeGjendje = true;
            njesiAdministrativeWEB.Aktiv = dyqaniEtopUP.Status == (int)statusVeprimi.Aktive ? true : false;
            DbCore.DbAdmin.colLidhjetAutorizim colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
            DbCore.DbAdmin.clsLidhjeAutorizim lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
            lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(dyqaniEtopUP.Kodi);
            colLidhjet.Add(lidhje);
            DbCore.DbRegjistrim.clsDegeAdministrative degaAdministrativeWEB = new DbCore.DbRegjistrim.clsDegeAdministrative();
            degaAdministrativeWEB.Kodi = dyqaniEtopUP.Kodi;
            degaAdministrativeWEB.IdNdermarje = ndermarrja;
            degaAdministrativeWEB = degaAdministrativeWEB.merrDegeSipasKodit();
            njesiAdministrativeWEB.IdDegeAdministrative = degaAdministrativeWEB.IdDegeAdministrative;
            njesiAdministrativeWEB.OColLidhjetAutorizim = colLidhjet;
            //Konfigurimi i Ambjentit. Duhet specifikuar kodi default qe duhet per krijimin.
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod("DA", ndermarrja);
            njesiAdministrativeWEB.IdKonfig = konfig.IdKonfigAmbjente;
            //Marrja e Inventarizimit. Eshte vendosur 1 default pasi nuk perdoret.
            njesiAdministrativeWEB.IdInventarizimi = 1;
            int idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;            
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (njesiAdministrativeWEB.IdDegeAdministrative < 1)
                return njesiAdministrativeWEB.ruajMagazine(null,0, idPeriudheZgjedhur, konfig.IdNivel);
            return njesiAdministrativeWEB.modifikoMagazine(0, idPeriudheZgjedhur, konfig.IdNivel, rm, cultinf);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <param name="kategoriSeriali"></param>
        /// <param name="data"></param>
        /// <param name="idLlojDokumentMag"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public (clsMesazh, clsMesazh) DergoFileTransferimiSerialeUnike(int idNdermarje, string kategoriSeriali, string data, int idLlojDokumentMag, int idMetoda)
        {
            try
            {
                return TransferimSerialeUnike.dergoFileTransferimSerialeUnike(idNdermarje, kategoriSeriali, data, idLlojDokumentMag, idMetoda);
            }
            catch (Exception ex)
            {
                return (new clsMesazh(true, ""), new clsMesazh(false, ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public (clsMesazh, clsMesazh) DergoFileTransferimiGjendjeAparate(int idNdermarje, string data, int idMetodeTransferimi)
        {
            try
            {
                return TransferimSerialeUnike.dergoFileTransferimGjendjeAparate(idNdermarje, data, idMetodeTransferimi);
            }
            catch (Exception ex)
            {
                return (new clsMesazh(true, ""), new clsMesazh(false, ex.Message));
            }
        }
    }
}
