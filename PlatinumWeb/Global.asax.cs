using RestApi.WebAPI;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web;
using System.Web.SessionState;
using DbCore;
using DbCore.DbAdmin;
using static DbCore.mySessionObjects;
using DevExpress.XtraReports.Web.ReportDesigner;
using DbCore.Raporte;
using System.Threading;
using DevExpress.XtraReports.Native;
using CacheLayer;
using System.Linq;
using System.Web.Configuration;
using System.Configuration;
using DbCore.IMBUtils;
using System.Web.Routing;
using Owin;
using Microsoft.Owin;
using System.Web.Hosting;
using DevExpress.XtraReports.Security;
using System.Diagnostics;
using System.Reflection;
using DbCore.IMBUtils;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.Pages;
using Autofac;
using Autofac.Integration.Web;
using AlphaWeb.Infrastructure.Logging;
using AlphaWeb.Core.Logging;
using AlphaWeb.Services;
using AlphaWeb.Core.Infrastructure;
using AlphaWeb.Core.Interfaces.Infrastructure;
using AlphaWeb.Core.Configuration;
using AlphaWeb.Core.Common;
using Web.Framework.Infrastructure;
using DbCore.IMBUtils.Messages;
using System.Resources;
using AutoMapper;
using DbCore.DbListPagesat;

using AlphaWebReports;
using DbCore.DbArkaBanka;
using PlatinumWeb.ApplicationUtils;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Web.Framework;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native.Services;

namespace PlatinumWeb
{
    public class Global : HttpApplication, IRequiresSessionState, IContainerProviderAccessor
    {

        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

            var engine = EngineContext.Create();
            var alphaWebConfig = ConfigurationManager.GetSection("AlphaWebConfig") as AlphaWebConfig;
            engine.Initialize(alphaWebConfig, new RequestLifetimeScopeManager());
            _containerProvider = new ContainerProvider(engine.ContainerManager.Container);

            //TODO GETSON zevendesoje me ApplicationCache
            //Versioni i ri i devexpress ka by default Deny perdorimin e scripteve
            ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
            Application["validInstall"] = true;
            Application["loginAttempts"] = new Dictionary<string, Dictionary<int, int>>();
            Application["maxNrDergimEmailPerUser"] = new Dictionary<string, int>();
            Application["userAktiv"] = new Dictionary<string, string>();
            try
            {
                //lexon file-in e konfigurimit te aplikacionit
                var webConfig = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
                //lexon seksionin e sesionit
                var sessionSection = (SessionStateSection)webConfig.GetSection("system.web/sessionState");
                //vendos ne connectrionString pool gjithe connection stringet 
                MyConnectionsManager.SetConnectionStringDefault(webConfig.ConnectionStrings.ConnectionStrings);
                MyConnectionsManager.InitializeConnectionStringsPool(colServerConnectionStrings.GetAllConnectionStringsAsDictionary());
                InitializeObjects();
                RouteTable.Routes.MapOwinPath("/External", app => new Startup().Configuration(app));
                
                //inicializon cache
                CacheConfiguration.ConfigureCache(sessionSection.Timeout);
                Configs.SessionCacheExpirationTime = TimeSpan.FromMinutes(clsServerConfiguration.LexoKonfigurimSipasKey<double>(ServerKonfigKey.SESSION_CACHE_TIMEOUT, MyConnectionsManager.ConnStringNameDefault));

                //regjistron web api
                WebApiConfig.Register(GlobalConfiguration.Configuration);
                //regjistron connections per report designerin
                DefaultReportDesignerContainer.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
                //regjistron serializuesin qe do perdori raport designeri per datasource-in e raporteve
                SerializationService.RegisterSerializer(CustomUntypedDataSetSerializer.Name, new CustomUntypedDataSetSerializer());
                //vendos pathin ku do ruhen layout-et e raporteve qe do krijohen/modifikohen nga raport designeri
                clsReportDesigner.InitializeCustomReportsPath(Server.MapPath("CUSTOMREPORTS"));
                //vendos pathin ku do ruhen folderi arkiva per fotot.
                AlphaWebReports.raporteUtil.InitializeArkivaPath(Server.MapPath("Arkiva"));
                //Versioni i ri i devexpress ka by default Deny perdorimin e scripteve
                ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
                RegisterDevExpressContents();
                //ruajme ne cache te gjitha komponentet e aplikacionit
                GlobalCacheManager.MyAppCache.Set("komponentet", colKomponentet.MerriTeGjitha(), TimeSpan.FromDays(30));
                //meqe aplikacioni po startohet logjikish te gjithe perdoruesit duhet te jene offline
                //var trackUsers = new clsTrackUser(MyConnectionsManager.GetPoolConnectionNames());
                //trackUsers.modifikoAllOffline(DateTime.Now);
                GlobalConfiguration.Configuration.EnsureInitialized();
                Map();
                clsFunksione.konfiguroNLog(MyConnectionsManager.ConnStringNameDefault);
            }
            catch (ConfigurationErrorsException ex)
            {
                ImbLogger.Error(ex, "Nuk u lexua file i konfigurimit Web.config");
            }
        }

        protected void InitializeObjects()
        {
            MessagesResource.Messages.Initialize
                    (
                        () =>
                        {
                            if (HttpContext.Current == null || HttpContext.Current.Session == null) return 0;
                            return mySessionObjects.ktheGjuhe(HttpContext.Current.Session);
                        },
                        new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"))
                    );

            clsFunksione.Initialise(AspxWebControlUtils.RedirectOnCallback);
            raporteUtil.Initialize(colBankat.merrBankatENdermarrjes);
        }

        protected void Map()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<clsThemesAmbjente, clsThemesAmbjente>();
                cfg.CreateMap<clsTeDrejtaRoli, clsTeDrejtaRoli>();
                cfg.CreateMap<clsDrejtaTabi, clsDrejtaTabi>();
                cfg.CreateMap<clsTeDrejtaRaporte, clsTeDrejtaRaporte>();
                cfg.CreateMap<clsStrukturaAdministrative, clsStrukturaAdministrative>();
            });
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            int idPerdoruesi = 0;
            int idNdermarrje = 0;
            int idNdermVit = 0;

            //kontrollohet nese gjate kesaj kerkese eshte krijuar nje session i ri,pasi sessioni i vjeter ka skaduar.
            //ne kete rast kerkesa duhet te behet redirect ne login page dhe te rikrijohet sesioni per userin
            //kjo  gje eshte e nevojshme pasi ne baze te sessionit qe ka bere timeout ka patur gjera te rendesishme qe identifikojne perdoruesin
            if (HttpApplicationHelper.KaSkaduarSessioni(Session, Request))
            {
                clsFunksione.logout(Session, true, true, false, "MbarimSessioni");
                clsLogin.ClearSessionCache(Session.SessionID);
                ImbLogger.LogTrace($"Skadim Sesioni -> SessionId:{Session.SessionID} - Url:{Request.Url.PathAndQuery}");
                return;
            }

            if (HttpApplicationHelper.KaNevojeTeJeteILoguar(Request.Url.AbsolutePath))
            {
                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idNdermVit = mySessionObjects.ktheIdVitNdermarrje(Session);
            }
            try
            {
                if (HttpApplicationHelper.EshteUrlPaScopeID(Request.Url.AbsolutePath, Request.Url.Query) || HttpApplicationHelper.EshteUrlLogini(Request.Url.AbsolutePath))
                {
                    if (HttpApplicationHelper.IsApiUrl(Request) && !GlobalCacheManager.EshteScopeIdAktiv())
                        HttpApplicationHelper.WriteUnAuthorizetResponse(Response, true);
                    ImbLogger.LogTrace($"PaScope -> SessionId:{Session.SessionID} - IdPerdoruesi:{idPerdoruesi} - IdNdermarrje:{idNdermarrje} - idNdermVit:{idNdermVit} - Url:{Request.Url.PathAndQuery}");
                    return;
                }
                if (!GlobalCacheManager.EshteScopeIdAktiv())
                    Context.RedirectToLoginNdermarrje();
            } catch (Exception ex) {
                ImbLogger.LogTrace($"LOG NGA METODA EshteScopeIdAktiv()  Exception -> {ex},  Exception Data - > {ex.Data.Values}");
            }
            var nameValuesCurrent = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            var currentKey = nameValuesCurrent.Get(ScopeManager.ScopeIdKey);
            var generateNewScope = nameValuesCurrent.Get(ScopeManager.NewScopeIdKey);

            var idnderm = nameValuesCurrent.Get(ScopeManager.IdNdermKey);
            var idndermVit = nameValuesCurrent.Get(ScopeManager.IdNdermVitKey);

            ImbLogger.LogTrace($"MeScope -> SessionId:{Session.SessionID} - IdPerdoruesi:{idPerdoruesi} - IdNdermarrje:{idNdermarrje} - idNdermVit:{idNdermVit} - CurrentScopeId:{currentKey} - GenerateNewScopeId:{generateNewScope} - Url:{Request.Url.PathAndQuery}");

            if ((generateNewScope != null && generateNewScope == bool.TrueString) || (string.IsNullOrWhiteSpace(currentKey) && !string.IsNullOrWhiteSpace(idnderm) && !string.IsNullOrWhiteSpace(idndermVit)))
            {
                var scopeId = clsFunksione.KrijoScopeTeRiNgaScopeIVjeter(Context, idnderm, idndermVit);
                nameValuesCurrent.Remove(ScopeManager.NewScopeIdKey);
                nameValuesCurrent.Set(ScopeManager.ScopeIdKey, scopeId);

                ImbLogger.LogTrace($"MeScope -> SessionId:{Session.SessionID} - IdPerdoruesi:{idPerdoruesi} - IdNdermarrje:{idNdermarrje} - idNdermVit:{idNdermVit} - CurrentScopeId:{currentKey} - GenerateNewScopeId:{generateNewScope} - Generated_New_Scope:{scopeId} - Url:{Request.Url.PathAndQuery}");

                Response.Redirect($"{Request.Url.AbsolutePath}?{nameValuesCurrent}");
            }
        }

        protected void Application_PostAuthorizeRequest(object sender, EventArgs e)
        {
            if (HttpApplicationHelper.EshteFaqeRaporti(Request)) return;
            //per faqet e tjera dhe api sessioni duhet te jete readonly
            //raportit i duhet i modifikueshem sepse e perdor devexpress per te ruajtur imazhet
            //nese e ben readonly nuk shfaqen imazhet ne raporte
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.ReadOnly);
            //perdoret qe te vendoset ne cookie data e serverit me qellim qe te perdoret si date e sugjeruar ne ambiente regjistrimi
            var cookie = new HttpCookie("dateServeri", DateTime.Now.ToString("MM/dd/yyyy"));
            //cookie.Secure = true;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            switch (Request.Url.AbsolutePath.ToString())
            {
                case "/arber":
                    Response.Redirect("https://support.alpha.al/1ff42q");
                    break;
                case "/kerolina":
                    Response.Redirect("https://support.alpha.al/8y27sx");
                    break;
                case "/karolina":
                    Response.Redirect("https://support.alpha.al/8y27sx");
                    break;
                case "/southcarolina":
                    Response.Redirect("https://support.alpha.al/8y27sx");
                    break;
                case "/northcarolina":
                    Response.Redirect("https://support.alpha.al/8y27sx");
                    break;
                case "/era":
                    Response.Redirect("https://support.alpha.al/c4txt3");
                    break;
                case "/enkela":
                    Response.Redirect("https://support.alpha.al/esyp9d");
                    break;
                case "/armegi":
                    Response.Redirect("https://support.alpha.al/8qwalv");
                    break;
                case "/ditmir":
                    Response.Redirect("https://support.alpha.al/2j1nqb");
                    break;
                case "/exhelina":
                    Response.Redirect("https://support.alpha.al/49d5ie");
                    break;
                case "/enxhelina":
                    Response.Redirect("https://support.alpha.al/49d5ie");
                    break;
                case "/elena":
                    Response.Redirect("https://support.alpha.al/7uqoy0");
                    break;
                case "/frenkli":
                    Response.Redirect("https://support.alpha.al/6tn9nw");
                    break;
                case "/viron":
                    Response.Redirect("https://support.alpha.al/ehz1u");
                    break;
                case "/marianxhela":
                    Response.Redirect("https://support.alpha.al/24hbjm");
                    break;
                case "/suela":
                    Response.Redirect("https://alphawiki.notion.site/Suela-Lleshaj-e6b9a8a9ce094046bb03332b85dfbf46");
                    break;

            }
            //nese kerkesa eshte per te hapur login page ose login_ndermarrje nuk ka nevoj per kontroll scope
            if (HttpApplicationHelper.EshteUrlPaScopeID(Request.Url.AbsolutePath, Request.Url.Query) || HttpApplicationHelper.EshteUrlLogini(Request.Url.AbsolutePath) || Request.Url.AbsolutePath.ContainsAnyIgnoreCase(ScopeManager.LoginNdermarrjePath))
                return;
            if (Request.Url.AbsolutePath.Contains("TestDI.aspx")) return;
            //marrim vlerat e vendosur ne querystring per kete kerkese
            var nameValuesCurrent = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            var currentKey = nameValuesCurrent.Get(ScopeManager.ScopeIdKey);

            if (string.IsNullOrWhiteSpace(currentKey))
            {
                //nese referuesi mungon ath duhet shkuar per te marr scopeID
                if (Request.UrlReferrer == null)
                {
                    var idnderm = nameValuesCurrent.Get(ScopeManager.IdNdermKey);
                    var idndermVit = nameValuesCurrent.Get(ScopeManager.IdNdermVitKey);
                    if (!string.IsNullOrWhiteSpace(idnderm) && !string.IsNullOrWhiteSpace(idndermVit))
                        return;
                    Context.RedirectToLoginNdermarrje();
                }

                //kerko scopeID nga referuesi
                var nameValuesUrlReferuesi = HttpUtility.ParseQueryString(Request.UrlReferrer.Query);
                currentKey = nameValuesUrlReferuesi.Get(ScopeManager.ScopeIdKey);

                if (!ScopeManager.IsScopeIdVlefshem(currentKey)) Context.RedirectToLoginNdermarrje();

                nameValuesCurrent.Set(ScopeManager.ScopeIdKey, currentKey);
                Response.Redirect($"{Request.Url.AbsolutePath}?{nameValuesCurrent}");
            }
            else
            {
                if (!ScopeManager.IsScopeIdVlefshem(currentKey)) Context.RedirectToLoginNdermarrje();
            }

        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //nese perdoruesi ka bere kerkese per nje api method dhe nuk eshte me i autentikuar ath shkruajme ne response
            //nje unauthorized content
            if (HttpApplicationHelper.IsApiUrl(Request) && !Request.IsAuthenticated)
                HttpApplicationHelper.WriteUnAuthorizetResponse(Response, false);

        }



        protected void Session_Start(object sender, EventArgs e)
        {
            GlobalCacheManager.CreateNewSessionCache(Session.SessionID);
            // Lejohet deri ne nje nr te caktuar tentativash per t'u loguar ne sistem 
            ruajLoginCount(Session, 0);
            // percakton nqs perdorusi eshte loguar apo jo 
            ruajIsLoggedIn(Session, "No");
            ruajEmerPerdoruesiNeSesion(Session, null);
            Session["dateCreated"] = DateTime.Now;
            Debug.Write(Session["dateCreated"]);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //GlobalCacheManager.MyAppCache.Clear();
            if(Server.GetLastError() != null)
            {
                var ex = Server.GetLastError();
                if (ex.GetType() == typeof(System.Web.HttpException))
                {
                    // The Complete Error Handling Example generates
                    // some errors using URLs with "NoCatch" in them;
                    // ignore these here to simulate what would happen
                    // if a global.asax handler were not implemented.
                    if (ex.Message.Contains("NoCatch") || ex.Message.Contains("maxUrlLength"))
                        return;
                }
                if (ex is ThreadAbortException)
                    return;
                HttpApplicationHelper.WriteErrorOnResponse(Response, ex);
                if (ex.Message != "Mungon konteksti i kerkeses!")
                    ImbLogger.Error(ex, "Application_Error");
                Server.ClearError();

            }
           
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                ImbLogger.Info($"Logout! Perdoruesit me username: {merrEmerPerdoruesiNgaSesioni(Session)} dhe sessionid {Session.SessionID}, i perfundoi sesioni!");
                clsLogin.signOutUser(Session.SessionID);
                clsLogin.ClearSessionCache(Session.SessionID);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                clsLogin.ClearSessionCache(Session.SessionID);
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //GlobalCacheManager.MyAppCache.Clear();
            //var trackUsers = new clsTrackUser(MyConnectionsManager.GetPoolConnectionNames());
            //trackUsers.modifikoAllOffline(DateTime.Now);
        }

        private void RegisterDevExpressContents()
        {
            //SessionState nevojitet per ngarkimin e report Designer
            DevExpress.XtraReports.Web.ReportDesigner.Native.ReportDesignerBootstrapper.SessionState = SessionStateBehavior.Required;
            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = SessionStateBehavior.Required;
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.Register<CacheCleanerSettings, CustomCacheCleanerSettings>();
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.Register<StoragesCleanerSettings, CustomStoragesCleanerSettings>();
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.UseFileDocumentStorage(Server.MapPath("~/App_Data/PreviewCache"));
            DevExpress.XtraReports.Web.ASPxWebDocumentViewer.StaticInitialize();
        }
    }

}