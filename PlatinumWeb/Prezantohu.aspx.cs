using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AlphaWeb.Services;
using CacheLayer;
using DbCore;
using DbCore.DbAdmin;
using DbCore.IMBUtils;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Security;
using DbCore.Otp;
using DevExpress.Web;
using JWT;
using PlatinumWeb.ApplicationUtils.Pages;
using Converter = DbCore.IMBUtils.Types.Converter;

namespace PlatinumWeb
{
	public partial class login : MyPageBase
	{
		public ITestService TestService { get; set; }
		private const string PARAMETER_NAME = "enc=";
		private DataTable dtServera;
		private const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
		FirebaseConfiguration fb = new FirebaseConfiguration();
		protected async void Page_Load(object sender, EventArgs e)
		{
#if DEBUG
            var test = TestService.GetTestData();
#endif

			//return the control to the calling method
			if (!IsPostBack)
			{

				ASPxLabel8.Text = $@"© {DateTime.Now.Year} IMB";
				LabelInfo.Text = "";
				//lblCapsLock.Text = MessagesResource.Messages["capsLockWarning"];
				//ruaj connstring default ne hapje te pare
				//MyConnectionsManager.SetSelectedConNameServer(Session.SessionID, MyConnectionsManager.ConnStringNameDefault);
				var idGjuha = MerrIdGjuha();
				mySessionObjects.ruajGjuhe(Session, idGjuha);
				var autoLogin = WebConfigurationManager.AppSettings["AutoLoginAsVizitor"];
				if (autoLogin != null && Convert.ToBoolean(autoLogin))
				{
					if (autoLoginForDefaultUser(rm, ci, idGjuha))
						return;
				}
				var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
				clsLogin.mbushServerCombo(Session.SessionID, combo);

				Login1.Focus();
				HtmlForm form = (HtmlForm)FindControl("form1");
				ASPxButton LoginButton = (ASPxButton)Login1.FindControl("LoginButton");
				string urlHelpi = clsFunksione.ktheUrlHelpi("").Item1;


				if (form != null && LoginButton != null)
				{
					form.DefaultButton = LoginButton.UniqueID;
				}

				ASPxLabel PasswordRecoveryLink = Login1.FindControl("PasswordRecoveryLink") as ASPxLabel;

				emertoKontrolletSipasGjuhes(rm, ci, PasswordRecoveryLink, LoginButton);
				if (Request.QueryString["enc"] != null)
				{
					loginETopUpVod(Request.QueryString["enc"], rm, ci);
				}
				//ruhet ne web config nese do shfaqet linku per resetim passowrdi ose jo, pasi ne politikat e fjalekalimit nuk mund te vendoset per sa kohe nuk kemi asnje te dhene per ndermarrjen ose licencen ne momentin qe hapet faqja e login
				bool shfaqLinkResetPass = Convert.ToBoolean(WebConfigurationManager.AppSettings["shfaqLinkResetPass"]);
				if (shfaqLinkResetPass)
				{
					PasswordRecoveryLink.Visible = shfaqLinkResetPass;
					if (Request.QueryString["user"] != null) //tregon qe eshte klikuar linku i resetimit te passwordit
					{
						ASPxTextBox txtUsername = Login1.FindControl("UserName") as ASPxTextBox;
						//string username1 = this.Login1.UserName;
						string username = Request.QueryString["user"].ToString();
						if (!username.Equals(""))
						{
							clsMesazh resetPassMesazh = clsFunksione.dergoVerificationLink(username, false, idGjuha);
							if (resetPassMesazh.KodMesazhi == 1) //kodmesazhi 1 ne rastin kur licenca e perdoruesit nuk lejon resetimin e passwordit per perdoruesit e saj te percaktuar te konfigurimet e fjalekalimit.
							{
								PasswordRecoveryLink.Enabled = false;
								PasswordRecoveryLink.Visible = false;
							}
							LabelInfo.Text = resetPassMesazh.PershkrimMesazhi;
							return;
						}
						LabelInfo.Text = MessagesResource.Messages["msgLoginPlotesoPerodruesin"];
						return;
					}
				}
				else
				{
					if (Request.QueryString["user"] != null || Request.QueryString["harroPw"] != null)
					{
						string sulm = MessagesResource.Messages["msgLoginResetimFjalekalimiIPaautorizuar"];
						clsTrackUser.shtoUserLoginFail(sulm, Login1.UserName, Session.SessionID, Request.UserHostAddress); //Rasti kur po sulmohet per resetim pass dhe linku I resetimin te pass eshte I fshehur
						LabelInfo.Text = sulm;
						return;
					}
				}
				string arsye = Request.QueryString["arsye"];
				if (string.IsNullOrEmpty(arsye) == false)
					if (arsye == "logout") if (arsye == "logout") GlobalCacheManager.DestroySessionCache(Session.SessionID);
				if (Request.Url.ToString().Contains("authToken="))
					await loginWithFirebaseToken();
				else if (Request.Url.ToString().Contains("uid="))
					await loginWithFirebaseUid(Request.Url.ToString().Split(new string[] { "uid=", "endUid" }, StringSplitOptions.None)[1]);
				else
				{
					if (string.IsNullOrEmpty(arsye))
						return;
					else
					{
						if (arsye.Contains("error"))
						{
							ImbLogger.Error("arsye.Contains(\"error\") - Nuk duhet te ndodhe kjo");
							var mesazhErrori = arsye.Split('_')[1];
							if (!mesazhErrori.Equals(""))
							{
								LabelInfo.Text = mesazhErrori;
								clsFunksione.logout(Session, true, true, true);
								clsLogin.mbushServerCombo(Session.SessionID, combo);
								return;
							}
						}
					}

				}

				VendosInfoSipasArsyes(arsye, idGjuha);
				clsLogin.mbushServerCombo(Session.SessionID, combo);
			}



		}
		//protected void Page_LoadComplete(object sender, EventArgs e)
		//{
		//    //FirebaseConfiguration fc = new FirebaseConfiguration();
		//    //fc.getUserPassword();
		//    //loginWithFirebase();
		//}  
		private void VendosInfoSipasArsyes(string arsye, int idGjuha)
		{
			switch (arsye)
			{

				case "FaqePaautorizuar":
					LabelInfo.Text = MessagesResource.Messages["msgLoginNukKeniTeDrejtaPerTuLoguarNeFaqe"];
					break;
				case "perfundoiLicenca":
					LabelInfo.Text = MessagesResource.Messages["msgLoginLicencaKaSkaduar"];
					break;
				case "problemLicenca":
					LabelInfo.Text = MessagesResource.Messages["msgLoginProblemLicence"];
					break;
				case "MbarimSessioni":
					LabelInfo.Text = MessagesResource.Messages["msgLoginSessionKaMbaruar"];
					break;
				case "logout":
					LabelInfo.Text = MessagesResource.Messages["msgLoginLidhjaUShkeputMeSukses"];
					clsFunksione.logout(Session, true, true, true);
					break;
				case "double":
					LabelInfo.Text = MessagesResource.Messages["msgLoginLidhjaUShkeputSeULoguatNeVendTjeter"];
					break;
				case "aprovimDokNdermarrjeGabuar":
					LabelInfo.Text = MessagesResource.Messages["msgAprovimDokNdermarrjeGabuar"];
					break;
				case "LinkIPavlefshem":
					LabelInfo.Text = "Ky link nuk është i vlefshëm!";
					break;
				case "ndryshoGjuhe":
					ndryshoGjuhe(rm, ci, idGjuha);
					break;
				case "faturaUser":
					LabelInfo.Text = "Perdoruesi qe u loguhat eshte perdorues fatura.alpha!";
					break;
				case "PerdoruesiNukEkziston":
					LabelInfo.Text = "Perdoruesi nuk ekziston!";
					clsFunksione.logout(Session, true);
					break;
			}
		}

		private int MerrIdGjuha()
		{
			var qsGjuha = Converter.MerrVlereOseDefault<string>(Request.QueryString["gjuha"]);
			if (!string.IsNullOrWhiteSpace(qsGjuha))
			{
				switch (qsGjuha)
				{
					case "AL": return 0;
					case "EN": return 1;
					case "FR": return 2;
					default: return 0;
				}
			}
			return clsServerConfiguration.LexoKonfigurimSipasKey<int>(ServerKonfigKey.Gjuha_Default);
		}

		/// <summary>
		/// Ben restart te programit duke ndryshuar gjuhen sipas zgjedhjes. Nese plotesohet QueryString "ktheNe" te ridrejton ne faqen ekzistuese nga ku u kerkuar nderrimi i gjuhes
		/// </summary>
		/// <param name="rm">ResourceManager</param>
		/// <param name="ci">CultureInfo e re</param>
		/// <param name="idGjuha">Gjuha e re e kerkuar</param>
		/// <returns>true/false</returns>
		private bool ndryshoGjuhe(ResourceManager rm, CultureInfo ci, int idGjuha)
		{
			var user = mySessionObjects.kthePerdorues(Session);
			//clsFunksione.logout(Session, true, true, true);
			mySessionObjects.ruajGjuhe(Session, idGjuha);
			clsMesazh mesazh = clsFunksione.validoPerdoruesinNeLogin(HttpContext.Current, user.PerdoruesUsername, user.PerdoruesPassword, false, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss", ci), false, rm, ci, "", "", "", "", true);
			string redirectToPage = Request.QueryString["ktheNe"] == null ? "" : Request.QueryString["ktheNe"];
			if (mesazh.Status)
				mesazh = clsFunksione.avancoPerpara(Response, Session, mySessionObjects.ktheIdPerdoruesi(Session), rm, ci, (bool)Application["validInstall"], false, false, true, redirectToPage);

			return mesazh.Status;
		}

		/// <summary>
		/// Logim ne menyre automatike pa dritare Login. Duhet te ekzistoje konfigurimi ne web.config: AutoLoginAsVizitor=true si dhe nje user default i celur ne program i cili do te shtohet edhe ne fushat GIS_USERNAME_DEFAULT dhe GIS_USERPASSWORD_DEFAULT te DB
		/// </summary>
		/// <param name="rm">ResourceManager</param>
		/// <param name="ci">CultureInfo</param>
		/// <param name="idGjuha">Gjuha e login</param>
		/// <returns>true/false</returns>
		private bool autoLoginForDefaultUser(ResourceManager rm, CultureInfo ci, int idGjuha)
		{
			clsMesazh mesazh = new clsMesazh(false, "");
			string arsye = Request.QueryString["arsye"];

			if (arsye != null) return mesazh.Status;
			var userDefault = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GIS_USERNAME_DEFAULT);
			var passwordDefault = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GIS_USERPASSWORD_DEFAULT);

			if (userDefault == null || passwordDefault == null) return mesazh.Status;

			mesazh = clsFunksione.validoPerdoruesinNeLogin(HttpContext.Current, userDefault.ToString(), passwordDefault.ToString(), false, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss", ci), false, rm, ci, "", "", "", "", false);
			if (mesazh)
				mesazh = clsFunksione.avancoPerpara(Response, Session, mySessionObjects.ktheIdPerdoruesi(Session), rm, ci, (bool)Application["validInstall"]);
			if (mesazh.Status)
				clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri pa google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);
			return mesazh.Status;
		}
		protected async void logInWithGmail(object sender, EventArgs e)
		{
			await loginWithFirebaseUid(txtUID.Text);
			return;
		}

		private void emertoKontrolletSipasGjuhes(ResourceManager rm, CultureInfo ci, ASPxLabel PasswordRecoveryLink, ASPxButton LoginButton)
		{


			PasswordRecoveryLink.Text = MessagesResource.Messages["msgLoginKeniHaruuarFjalekalimin"];
			//ASPxHyperLink NdryshoOrganizate = Login1.FindControl("NdryshoOrganizate") as ASPxHyperLink;
			ASPxHyperLink lblGjuhaAL = Login1.FindControl("lblGjuhaAL") as ASPxHyperLink;
			ASPxHyperLink lblGjuhaEN = Login1.FindControl("lblGjuhaEN") as ASPxHyperLink;
			ASPxHyperLink lblGjuhaFR = Login1.FindControl("lblGjuhaFR") as ASPxHyperLink;
			lblGjuhaAL.NavigateUrl = $"{Paths.defaultLoginPath}?gjuha=AL";
			lblGjuhaEN.NavigateUrl = $"{Paths.defaultLoginPath}?gjuha=EN";
			lblGjuhaFR.NavigateUrl = $"{Paths.defaultLoginPath}?gjuha=FR";
			//lblGjuhaALKesh.NavigateUrl = $"{Paths.defaultLoginPath}?gjuha=AL";
			//lblGjuhaENKesh.NavigateUrl = $"{Paths.defaultLoginPath}?gjuha=EN";
			//lblGjuhaFRKesh.NavigateUrl = $"{Paths.defaultLoginPath}?gjuha=FR";
			ASPxHiddenField loginHiddenField = Login1.FindControl("loginHiddenField") as ASPxHiddenField;

			loginHiddenField.Set("userlbl", MessagesResource.Messages["lblLoginPerdoruesi"]);
			loginHiddenField.Set("passlbl", MessagesResource.Messages["lblLoginPassword"]);
			loginHiddenField.Set("srvlbl", MessagesResource.Messages["cmbzgjidhServerin"]);
			//NdryshoOrganizate.Text = MessagesResource.Messages["NdryshoOrganizate"];
			switch (ci.ToString())
			{
				case "sq-AL":
					LoginButton.Text = "Hyrje";
					break;
				case "en-US":
					LoginButton.Text = "Login";
					break;
				case "fr-FR":
					LoginButton.Text = "Entrer";
					break;
			}
		}

		/// <summary>
		/// Metode qe perdoret vetem per VODAFONE, per log-in nga eTopUp.
		/// </summary>
		/// <param name="enc">String e enkriptuar e parametrave</param>
		private void loginETopUpVod(string enc, ResourceManager rm, CultureInfo ci)
		{
			try
			{
				string decryptedQuery = clsEnDecVodafone.dekriptoMesazh(enc);
				NameValueCollection myQuery = HttpUtility.ParseQueryString(decryptedQuery);
				int sekondaTeToleruara = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["toleroSekonda"]);
				if (myQuery["username"] != "" && myQuery["webService"] != "")
				{
					TimeSpan diffTime = DateTime.Now - Convert.ToDateTime(myQuery["dataLogin"], new CultureInfo("en-us", false));
					if (diffTime.TotalSeconds > sekondaTeToleruara)
					{
						LabelInfo.Text = MessagesResource.Messages["msgLoginLinkuPerSingleSignONJoIVlefshem"];
						clsFunksione.logout(Session, false, true, true);
						return;
					}
					if (loginAutentification(myQuery["username"], myQuery["dataLogin"], Boolean.Parse(myQuery["webService"]), rm, ci, myQuery["ndermarrja"], myQuery["ipkasa"], myQuery["emerprinteri"], myQuery["dyqani"]))
						clsFunksione.avancoPerpara(Response, Session, mySessionObjects.ktheIdPerdoruesi(Session), rm, ci, (bool)Application["validInstall"], false, false, false);
					else
					{
						LabelInfo.Text = MessagesResource.Messages["msgLoginNukUKryeLogimi"];
						clsFunksione.logout(Session, false, true, true);
					}
				}
			}
			catch (MyException m)
			{
				ImbLogger.Error(m);
				LabelInfo.Text = m.Message;
				clsFunksione.logout(Session, false, true, true);
			}
			catch (Exception m)
			{
				ImbLogger.Error(m);
				LabelInfo.Text = MessagesResource.Messages["msgLoginLinkuPerSingleSignONJoIVlefshem"];
				clsFunksione.logout(Session, false, true, true);
			}
		}

		protected async void Login1_Authenticate(object sender, AuthenticateEventArgs e)
		{


			ASPxButton LoginButton = (ASPxButton)Login1.FindControl("LoginButton2");
			ASPxLabel PasswordRecoveryLink = Login1.FindControl("PasswordRecoveryLink") as ASPxLabel;

			emertoKontrolletSipasGjuhes(rm, ci, PasswordRecoveryLink, LoginButton);
			var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
			string connName;
			if (combo?.Value == null)
			{
				clsLogin.setServer(Session.SessionID, 0);
			}
			else
			{
				clsMesazh mesazh = clsLogin.setServer(Session.SessionID, Convert.ToInt32(combo.Value));
				if (!mesazh.Status)
				{
					Login1.FailureText = mesazh.PershkrimMesazhi;
					e.Authenticated = false;
					return;
				}
			}
			string organizata = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
			var user = new clsPerdorues(Login1.UserName);
			//var orgs = await fb.getOrganizationsForAlphax();
			//if (!orgs.Contains(organizata))
			//{
			//	bool superUser = false;
			//	foreach (var role in user.OColRolPerdoruesi)
			//	{
			//		clsRoli rol = new clsRoli(role.IdRoli);
			//		if (rol.KodRoli == "RSU")
			//		{
			//			superUser = true;
			//			break;
			//		}
			//	}
			//	if (!superUser)
			//	{
			//		Session.Clear();
			//		Response.Redirect("https://alphax.al");
			//		return;
			//	}
			//}


			if (user.Shenime != "")
			{
				bool validim = loginAutentification(Login1.UserName, clientDate.Get("clientDate").ToString(), false, rm, ci);
				if (validim)
				{

					string verifiedEmail = user.Shenime;
					Dictionary<string, object> firebaseUser = await fb.getUserDetailsWithEmail(verifiedEmail);
					if (firebaseUser.Count != 0)
					{
						if (firebaseUser.ContainsKey("alphaOrganization"))
							if (firebaseUser["alphaOrganization"].ToString() == combo.Text)
							{
								clsPerdorues perdoruesGoogle = new clsPerdorues(firebaseUser["username"].ToString(), firebaseUser["email"].ToString(), true);
								if (perdoruesGoogle.IdPerdorues != 0 && (perdoruesGoogle.IdPerdoruesi == user.IdPerdorues))
									await loginWithFirebaseUid(firebaseUser["uid"].ToString());
							}
					}
				}


			}


			//marrim gjuhen nga quersytring ose db nese nuk ka gje ne querystring
			var idGjuha = MerrIdGjuha();
			mySessionObjects.ruajGjuhe(Session, idGjuha);

			DbCore.DbAdmin.clsKonfigurimeFjalekalimi konf = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(user.IdPerdorues);
			konf.mbushKonfigurimSipasPerdoruesit(user.IdPerdorues);

			// if first time login is not done

			if (konf.Twofacorauth)
			{
				if (!Convert.ToBoolean(step1Complete.Value))
				{
					ASPxTextBox pass = (ASPxTextBox)Login1.FindControl("Password");
					var Otp_qr = (Literal)Login1.FindControl("Otp_qr");
					var otp_div = (Control)Login1.FindControl("otp_div");
					var login_div = (Control)Login1.FindControl("login_div");


					// check if user/pass is valid
					if (clsFunksione.validoPerdoruesUsernmaePass(HttpContext.Current, Login1.UserName, pass.Text))
					{
						step1Complete.Value = true.ToString();



						var userToken = user.Otp_Token;

						var imageSource = "<div></div>";


						if (string.IsNullOrWhiteSpace(userToken))
						{
							userToken = Guid.NewGuid().ToString();
							clsPerdorues.modifikoOtp(user.IdPerdorues, userToken);

							var auth = new OtpAuthenticator(userToken, Login1.UserName);
							imageSource = $"<img src='https://chart.apis.google.com/chart?cht=qr&chs=250x250&chl={auth.GetOtpUrl()}'/>";
						}

						Otp_qr.Text = $"{imageSource}";

						otp_div.Visible = true;
						login_div.Visible = false;

						e.Authenticated = false;
					}
				}

				else
				{
					// verify OTP
					var userToken = clsFunksione.merrTokeninOtpTeUserit(Login1.UserName);

					var auth = new OtpAuthenticator(userToken, Login1.UserName);

					var inputCode = ((ASPxTextBox)Login1.FindControl("Kodi")).Text;

					if (!(inputCode.Replace(" ", "").Trim() == auth.GetPin()))
					{
						((Label)Login1.FindControl("otpError")).Text = "OTP Eshte Gabim";
						e.Authenticated = false;
						return;
					}

					if (loginAutentification(Login1.UserName, clientDate.Get("clientDate").ToString(), false, rm, ci))
					{

						var mesazh = clsFunksione.avancoPerpara(Response, Session, user.IdPerdorues, rm, ci, (bool)Application["validInstall"]);
						if (mesazh.Status) return;
						Login1.FailureText = mesazh.PershkrimMesazhi;
						e.Authenticated = false;
					}
					else
						e.Authenticated = false;
				}
			}
			else if (loginAutentification(Login1.UserName, clientDate.Get("clientDate").ToString(), false, rm, ci))
			{
				var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
				var org = MyConnectionsManager.GetSelectedConNameServer();
				if (hflocal.Value == "" || hflocal.Value != org)
				{


					try
					{

						String strRedirect = String.Format("https://imb-licence.ew.r.appspot.com/rest/getTerms?organisation={0}", org);
						var myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(strRedirect);
						myHttpWebRequest.Method = "GET";
						myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
						hfTerms.Value = org;
						//Get Response
						var myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
						using (Stream dataStream = myHttpWebResponse.GetResponseStream())
						{
							// Open the stream using a StreamReader for easy access.
							StreamReader reader = new StreamReader(dataStream);
							// Read the content.
							string responseFromServer = reader.ReadToEnd();
							if (responseFromServer == "false")
							{
								popupUniversal1.ClientSideEvents.Init = @"function() {     popupUniversal1.Show(); 
                 }
                     ";
							}
							else
							{

								mySessionObjects.ruajTerms(Session, true);
								var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
								if (mesazh.Status) clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri pa google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);
								if (mesazh.Status) return;
								Login1.FailureText = mesazh.PershkrimMesazhi;

							}


						}

						// Close the response.
						myHttpWebResponse.Close(); return;
					}
					catch (WebException ex)
					{
						string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
					}
				}
				else
				{
					mySessionObjects.ruajTerms(Session, true);
					var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
					if (mesazh.Status) clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri pa google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);
					if (mesazh.Status) return;
					Login1.FailureText = mesazh.PershkrimMesazhi;
				}
			}
		}
		internal async Task loginWithFirebaseToken()
		{
			try
			{
				var dictionary = await fb.getUserPassword(Request.Url.ToString().Split(new string[] { "authToken=" }, StringSplitOptions.None)[1]);
				string passwordHashed = (dictionary.ContainsKey("passwordHash") == true ? dictionary["passwordHash"].ToString() : "");
				string organization = (dictionary.ContainsKey("alphaOrganization") == true ? dictionary["clientDatabase"].ToString() : "");
				string email = (dictionary.ContainsKey("email") == true ? dictionary["email"].ToString() : "");
				string username = (dictionary.ContainsKey("username") == true ? dictionary["username"].ToString() : "");
				//organization = "praktike1-test"; // chnage to organization
				ASPxTextBox password = (ASPxTextBox)Login1.FindControl("Password");
				password.Text = dictionary["passwordHash"].ToString();
				Login1.UserName = username;
				ASPxButton LoginButton = (ASPxButton)Login1.FindControl("LoginButton2");
				ASPxLabel PasswordRecoveryLink = Login1.FindControl("PasswordRecoveryLink") as ASPxLabel;

				emertoKontrolletSipasGjuhes(rm, ci, PasswordRecoveryLink, LoginButton);
				var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
				string connName;
				combo.Value = combo.Items.FindByText(organization).Value;
				if (combo?.Value == null)
				{
					clsLogin.setServer(Session.SessionID, 0);
				}
				else
				{
					clsMesazh mesazh = clsLogin.setServer(Session.SessionID, Convert.ToInt32(combo.Value));
					if (!mesazh.Status)
					{
						Login1.FailureText = mesazh.PershkrimMesazhi;
						return;
					}
				}
				var user = new clsPerdorues(username, email, true);
				//marrim gjuhen nga quersytring ose db nese nuk ka gje ne querystring
				var idGjuha = MerrIdGjuha();
				mySessionObjects.ruajGjuhe(Session, idGjuha);

				DbCore.DbAdmin.clsKonfigurimeFjalekalimi konf = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(user.IdPerdorues);
				konf.mbushKonfigurimSipasPerdoruesit(user.IdPerdorues);

				// if first time login is not done


				if (loginAutentificationWithFirebase(username, email, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), false, rm, ci))
				{
					var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
					var org = MyConnectionsManager.GetSelectedConNameServer();
					if (hflocal.Value == "" || hflocal.Value != org)
					{


						try
						{

							String strRedirect = String.Format("https://imb-licence.ew.r.appspot.com/rest/getTerms?organisation={0}", org);
							var myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(strRedirect);
							myHttpWebRequest.Method = "GET";
							myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
							hfTerms.Value = org;
							//Get Response
							var myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
							using (Stream dataStream = myHttpWebResponse.GetResponseStream())
							{
								// Open the stream using a StreamReader for easy access.
								StreamReader reader = new StreamReader(dataStream);
								// Read the content.
								string responseFromServer = reader.ReadToEnd();
								if (responseFromServer == "false")
								{
									popupUniversal1.ClientSideEvents.Init = @"function() {     popupUniversal1.Show(); 
                }
                    ";
								}
								else
								{

									mySessionObjects.ruajTerms(Session, true);
									var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
									if (mesazh.Status)
									{
										mySessionObjects.ruajEmerPerdoruesiNeSesion(Session, user.EmriPerdorues + " " + user.MbiemriPerdorues);
										clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri me google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);

									}
									if (mesazh.Status) return;
									Login1.FailureText = mesazh.PershkrimMesazhi;

								}


							}

							// Close the response.
							myHttpWebResponse.Close(); return;
						}
						catch (WebException ex)
						{
							string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
						}
					}
					else
					{
						mySessionObjects.ruajTerms(Session, true);
						var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
						if (mesazh.Status)
							clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri me google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);
						if (mesazh.Status) return;
						Login1.FailureText = mesazh.PershkrimMesazhi;
					}
				}

			}
			catch (Exception ex)
			{
				ImbLogger.LogErrorWebApi(ex.Message);
			}


		}
		internal async Task loginWithFirebaseUid(string uid)
		{
			try
			{
				//uid = "12GTeNTQBbdBvZMiVo5TX0SPmf13"; // change to uid

				var dictionary = await fb.getUserDetailsWithUID(uid);
				string passwordHashed = (dictionary.ContainsKey("passwordHash") == true ? dictionary["passwordHash"].ToString() : "");
				string organization = (dictionary.ContainsKey("alphaOrganization") == true ? dictionary["alphaOrganization"].ToString() : "");
				if (organization == "")
				{
					panelDiv.Visible = true;
					return;
				}
				string dtFunditLogin = (dictionary.ContainsKey("alphaLastLogedInDate") == true ? dictionary["alphaLastLogedInDate"].ToString() : "1990-01-01 00:00:00");
				DateTime now = DateTime.Now;
				if (txtFirstLogin.Text == "true")
				{
					if (new DateTime(DateTime.Parse(dtFunditLogin).Ticks) < new DateTime(now.Year, now.Month, now.Day, 0, 0, 0))
					{
						fb.updateLogInTime(uid);
						return;
					}
				}

				string email = (dictionary.ContainsKey("email") == true ? dictionary["email"].ToString() : "");
				string username = (dictionary.ContainsKey("username") == true ? dictionary["username"].ToString() : "");
				//organization = "praktike1-test";//change to organization
				ASPxTextBox password = (ASPxTextBox)Login1.FindControl("Password");
				password.Text = dictionary["passwordHash"].ToString();
				Login1.UserName = username;
				ASPxButton LoginButton = (ASPxButton)Login1.FindControl("LoginButton2");
				ASPxLabel PasswordRecoveryLink = Login1.FindControl("PasswordRecoveryLink") as ASPxLabel;

				emertoKontrolletSipasGjuhes(rm, ci, PasswordRecoveryLink, LoginButton);
				var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
				string connName;
				combo.Value = combo.Items.FindByText(organization).Value;
				if (combo?.Value == null)
				{
					clsLogin.setServer(Session.SessionID, 0);
				}
				else
				{
					clsMesazh mesazh = clsLogin.setServer(Session.SessionID, Convert.ToInt32(combo.Value));
					if (!mesazh.Status)
					{
						Login1.FailureText = mesazh.PershkrimMesazhi;
						return;
					}
				}
				var user = new clsPerdorues(username, email, true);

				//marrim gjuhen nga quersytring ose db nese nuk ka gje ne querystring
				var idGjuha = MerrIdGjuha();
				mySessionObjects.ruajGjuhe(Session, idGjuha);

				DbCore.DbAdmin.clsKonfigurimeFjalekalimi konf = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(user.IdPerdorues);
				konf.mbushKonfigurimSipasPerdoruesit(user.IdPerdorues);

				// if first time login is not done

				if (loginAutentificationWithFirebase(username, email, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), false, rm, ci))
				{
					var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
					var org = MyConnectionsManager.GetSelectedConNameServer();
					if (hflocal.Value == "" || hflocal.Value != org)
					{


						try
						{

							String strRedirect = String.Format("https://imb-licence.ew.r.appspot.com/rest/getTerms?organisation={0}", org);
							var myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(strRedirect);
							myHttpWebRequest.Method = "GET";
							myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
							hfTerms.Value = org;
							//Get Response
							var myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
							using (Stream dataStream = myHttpWebResponse.GetResponseStream())
							{
								// Open the stream using a StreamReader for easy access.
								StreamReader reader = new StreamReader(dataStream);
								// Read the content.
								string responseFromServer = reader.ReadToEnd();
								if (responseFromServer == "false")
								{
									popupUniversal1.ClientSideEvents.Init = @"function() {     popupUniversal1.Show(); 
                }
                    ";
								}
								else
								{

									mySessionObjects.ruajTerms(Session, true);
									var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
									if (mesazh.Status)
									{
										mySessionObjects.ruajEmerPerdoruesiNeSesion(Session, user.EmriPerdorues + " " + user.MbiemriPerdorues);
										clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri me google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);
									}
									if (mesazh.Status) return;

									Login1.FailureText = mesazh.PershkrimMesazhi;

								}


							}

							// Close the response.
							myHttpWebResponse.Close(); return;
						}
						catch (WebException ex)
						{
							string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();

						}
					}
					else
					{
						mySessionObjects.ruajTerms(Session, true);
						var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
						if (mesazh.Status) return;

						Login1.FailureText = mesazh.PershkrimMesazhi;
					}
				}

				LabelInfo.Text = Login1.FailureText;

			}
			catch (Exception ex)
			{
				ASPxHiddenField loginHiddenField = Login1.FindControl("loginHiddenField") as ASPxHiddenField;

				loginHiddenField.Set("userlbl", MessagesResource.Messages["lblLoginPerdoruesi"]);
				loginHiddenField.Set("passlbl", MessagesResource.Messages["lblLoginPassword"]);
				loginHiddenField.Set("srvlbl", MessagesResource.Messages["cmbzgjidhServerin"]);
				ImbLogger.LogErrorWebApi(ex.Message);
			}


		}

		//protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
		//{
		//    otp_div.Visible = true;
		//    login_div.Visible = false;
		//    return;

		//    var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
		//    string connName;
		//    if (combo?.Value == null)
		//    {
		//        clsLogin.setServer(Session.SessionID, 0);
		//    }
		//    else
		//    {
		//        clsMesazh mesazh = clsLogin.setServer(Session.SessionID, Convert.ToInt32(combo.Value));
		//        if (!mesazh.Status)
		//        {
		//            Login1.FailureText = mesazh.PershkrimMesazhi;
		//            e.Authenticated = false;
		//            return;
		//        }
		//    }
		//    //marrim gjuhen nga quersytring ose db nese nuk ka gje ne querystring
		//    var idGjuha = MerrIdGjuha();
		//    mySessionObjects.ruajGjuhe(Session, idGjuha);
		//    if (loginAutentification(Login1.UserName, clientDate.Get("clientDate").ToString(), false, rm, ci))
		//    {
		//        var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
		//        var mesazh = clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
		//        if (mesazh.Status) return;
		//        Login1.FailureText = mesazh.PershkrimMesazhi;
		//        e.Authenticated = false;
		//    }
		//    else
		//        e.Authenticated = false;
		//}

		internal bool loginAutentification(string username, string data, bool webServise, ResourceManager rm, CultureInfo ci, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "")
		{
			ASPxTextBox pass = (ASPxTextBox)Login1.FindControl("Password");
			clsMesazh mesazh = clsFunksione.validoPerdoruesinNeLogin(HttpContext.Current, username, pass.Text, Login1.RememberMeSet, data, webServise, rm, ci, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS, false);
			if (mesazh)
			{
				return true;

			}

			Login1.FailureText = mesazh.PershkrimMesazhi;
			return false;
		}
		internal bool loginAutentificationWithFirebase(string username, string email, string data, bool webServise, ResourceManager rm, CultureInfo ci, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "")
		{
			ASPxTextBox pass = (ASPxTextBox)Login1.FindControl("Password");
			clsMesazh mesazh = clsFunksione.validoPerdoruesinNeLoginWithFirebase(HttpContext.Current, username, email, pass.Text, Login1.RememberMeSet, data, webServise, rm, ci, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS, false);
			if (mesazh)
			{
				return true;

			}

			Login1.FailureText = mesazh.PershkrimMesazhi;
			return false;
		}

		protected void ButtonOk_Click(object sender, EventArgs e)
		{
			try
			{
				var org = MyConnectionsManager.GetSelectedConNameServer();
				var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://imb-licence.ew.r.appspot.com/rest/addTerms");
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "POST";

				using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					DateTime date = DateTime.Now;
					string json = new JavaScriptSerializer().Serialize(new
					{
						termsId = "",
						organisation = org,
						AWUsername = Login1.UserName,
						acceptedTime = string.Format("{0:s}", date)


					});
					streamWriter.Write(json);
				}

				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					popupUniversal1.ClientSideEvents.Init =
						@"function() {  popupUniversal1.Hide(); }";

					var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
					mySessionObjects.ruajTerms(Session, true);
					var mesazh = clsFunksione.avancoPerpara(Response, Session, IdPerdoruesi, rm, ci, (bool)Application["validInstall"]);
					if (mesazh.Status)
						clsFunksione.dergoLogAlphaweb("", "Logim", "Logim useri pa google", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), Login1.UserName);
					if (mesazh.Status) return;

				}
			}
			catch (WebException ex)
			{
				string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
			}





		}
		//protected void cmbServerat_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
		//{
		//    var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
		//    if (IsCallback)
		//    {
		//        if (Request.Params["__CALLBACKID"].Contains("cmbServerat"))
		//        {

		//            clsLogin.mbushServerCombo(Session.SessionID, combo, e.Filter, e.BeginIndex, e.EndIndex + 1);
		//        }
		//    }
		//}
		//protected void cmbServerat_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
		//{
		//    var combo = Login1.FindControl("cmbServerat") as ASPxComboBox;
		//    if (IsCallback)
		//    {
		//        if (Request.Params["__CALLBACKID"].Contains("cmbServerat"))
		//        {

		//            int value = 0;
		//            if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
		//                return;
		//            clsLogin.mbushServerComboID(Session.SessionID, combo, value);

		//        }
		//    }
		//}
	}
}