using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using CacheLayer;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.IMBUtils.Fiskalizimi.API;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Security;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
	public partial class FaqeKryesore : MyPageBase
	{
		protected void Page_PreInit(object sender, EventArgs e)
		{
			base.Page_PreInit(sender, e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var cookie = Request.Cookies["adresa"];
				if (cookie == null)
				{
					cookie = new HttpCookie("adresa");
				}
				if (!DbCore.mySessionObjects.isLogedIn(Session))
				{
					cookie.Value = DbCore.IMBUtils.Paths.defaultLoginPath;
					cookie.Expires = DateTime.Now.AddDays(1);
					Response.Cookies.Add(cookie);
					DbCore.clsFunksione.logout(Session, true, string.Empty, false);
					return;
				}
				int idgjuha = mySessionObjects.ktheGjuhe(Session);
				var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
				if (periudha != null)
					periudha = new clsPeriudhaKontabel(periudha.IdPeriudha, idgjuha);

				var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
				hfState.Set("activateSignalR", System.Web.Configuration.WebConfigurationManager.AppSettings["activateSignalR"].ToString());
				hfState.Set("periudha", JsonConvert.SerializeObject(periudha));
				hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
				var user = DbCore.mySessionObjects.kthePerdorues(Session);
				var idPerdoruesi = user.IdPerdorues;
				var ndermarrja = DbCore.mySessionObjects.ktheKodNdermarrje(Session);

				var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
				string stringKonfigMenuMajtas = DbCore.DbShare.clsKonfigMenu.ktheListKonfigMenu(idPerdoruesi, idNdermarrje);
				hfState.Set("adminUser", false);
				clsPerdorues perdorues = new clsPerdorues(mySessionObjects.ktheIdPerdoruesi(Session));
				foreach (var role in perdorues.OColRolPerdoruesi)
				{
					clsRoli rol = new clsRoli(role.IdRoli);
					if (rol.KodRoli == "RA" || rol.KodRoli == "RAS") hfState.Set("adminUser", true);
				}
				hfState.Set("konfigMenuMajtas", String.IsNullOrEmpty(stringKonfigMenuMajtas) ? "" : stringKonfigMenuMajtas);
				hfState.Set("idPerdoruesi", idPerdoruesi);
				hfState.Set("ShfaqPerdoruesMenu", user.ShfaqPerdoruesMenu);
				hfState.Set("EmerMbiemerPerdorues", user.EmriPerdorues + ' ' + user.MbiemriPerdorues);
				hfState.Set("organizata", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar());
				hfState.Set("emailPerdoruesi", user.PerdoruesEmail);
				hfState.Set("Username", user.PerdoruesUsername);
				hfState.Set("shenime", user.Shenime);
				hfState.Set("Kadnderrmarje", ndermarrja);
				var licenca = new clsLicenca();
				licenca.mbushLicencen(IdPerdoruesi);
				hfState.Set("chatAktiv", licenca.ChatAktiv);
				hfState.Set("chatLink", licenca.ChatLink);
				hfState.Set("chatPortHttp", licenca.ChatPortHttp);
				hfState.Set("chatPortHttps", licenca.ChatPortHttps);
				//PageAsyncTask t = new PageAsyncTask(showPopUp);
				//Page.RegisterAsyncTask(t);
				Page.ExecuteRegisteredAsyncTasks();
				hfState.Set("googleAnalytics", licenca.GoogleAnalytics);
				hfState.Set("googleAnalyticsTrackingId", licenca.GoogleAnalyticsTrackingId);
				if (user.ShfaqPerdoruesMenu)
				{
					ASPxLabel1.Text = user.EmriPerdorues + ' ' + user.MbiemriPerdorues;
				}
				hfState.Set("idNdermarrje", idNdermarrje);
				var mesazhiPerodruesit = DbCore.clsFunksione.KtheMesazhPerPerdoruesin();
				if (!String.IsNullOrEmpty(mesazhiPerodruesit))
				{
					popUpAzhornim.ShowOnPageLoad = true;
					popUpAzhornim.Text = mesazhiPerodruesit;
				}
				var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
				//var msgSkadimLicence = DbCore.DbAdmin.clsLicenca.KontrolloSkadiminLicences(idPerdoruesi, rm, ci);
				//if (!msgSkadimLicence.Status)
				//{
				//    if (msgSkadimLicence.PershkrimMesazhi == "Problem ne validimin e licences!")
				//        DbCore.clsFunksione.logout(Session, true, "problemLicenca");
				//    else DbCore.clsFunksione.logout(Session, true, "perfundoiLicenca");
				//}
				//if (!msgSkadimLicence.PershkrimMesazhi.Equals(string.Empty))
				//{
				//    popUpDiteTeMbeturaLicenca.ShowOnPageLoad = true;
				//    lblDiteTeMbeturaTeLicenca.Text = msgSkadimLicence.PershkrimMesazhi;
				//}
				string urlImazhPerdoruesi = DbCore.DbShare.clsArkiva.ktheImazhPerdoruesi(idPerdoruesi);
				if (String.IsNullOrEmpty(urlImazhPerdoruesi) || !System.IO.File.Exists(Server.MapPath(urlImazhPerdoruesi)))
					urlImazhPerdoruesi = "images/new/perdorues.png";
				DevExpress.Web.MenuItem ikonaImazhPerdorues = ASPxMenu1.Items.FindByName("ikonaImazhPerdorues");
				ikonaImazhPerdorues.Image.Url = urlImazhPerdoruesi;
				ikonaImazhPerdorues.Image.Width = 25;
				ikonaImazhPerdorues.Image.Height = 25;
				bool kycurMobile = clsPerdorues.kthePerdoruesKycurMobile(idPerdoruesi);
				hfState.Set("kycurMobile", kycurMobile);
				var IdNdermViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString();
				var KodViti = DbCore.mySessionObjects.ktheVitiNdermarrjes(Session).ToString();
				var pathBck = "height: 100%; width: 100%;";

				backDiv.Attributes.Add("style", pathBck);
				if (user.KontrollPassword)
				{
					var konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi);
					if (konfig.NdryshimPasswordiDetyruar || konfig.SkadoPassword)
					{
						var passVlefshem = DbCore.clsFunksione.kontrolloPassword(konfig, idPerdoruesi, HttpContext.Current, rm, ci);
						if (passVlefshem.Status)
						{
							enableMenuTeDrejta(idPerdoruesi, rm, ci);
						}
						else
						{
							Response.Redirect("NdryshimFjalekalimi.aspx");
							return;
						}
					}
					else
					{
						enableMenuTeDrejta(idPerdoruesi, rm, ci);
					}
				}
				else
				{
					enableMenuTeDrejta(idPerdoruesi, rm, ci);
				}
				string exCertificateDate = clsFunksioneFiskalizimi.kontrolloNeseCertifikataKaSkaduar(idNdermarrje);
				if (exCertificateDate != "")
					hfState.Set("SkadimCertifikate", exCertificateDate);
				else hfState.Set("SkadimCertifikate", "");
				EmrateLabelave(ci);
				var idTheme = !String.IsNullOrEmpty(Request.QueryString["idTheme"]) ? Convert.ToInt32(Request.QueryString["idTheme"]) : DbCore.DbAdmin.clsThemesAmbjente.ktheIdTheme(idPerdoruesi);
				hfState.Set("idTheme", idTheme);
				var komponente = !String.IsNullOrEmpty(Request.QueryString["ambienti"]) ? buildQueryStringNgaAmbienti(Request.QueryString) : string.Empty;
				if (!String.IsNullOrEmpty(Request.QueryString["ambDef"]) && Request.QueryString["ambDef"] == "Dashboard.aspx")
					komponente = "Dashboard.aspx";
				if (komponente == string.Empty && (Request.QueryString["vjenNga"] != "CRM" || Request.QueryString["vjenNga"] != "GIS"))
				{
					//komponente = DbCore.DbAdmin.clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
					komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, "Default.aspx", KodViti);
				}
				if (komponente == string.Empty)
				{
					komponente = "Default.aspx";
				}
				var scopeID = Request.Params[ScopeManager.ScopeIdKey];
				komponente = DbCore.clsFunksione.shtoVarToUrl(komponente, "idTheme", idTheme.ToString());
				komponente = DbCore.clsFunksione.shtoVarToUrl(komponente, ScopeManager.ScopeIdKey, scopeID);
				ASPxSplitter1.GetPaneByName("paneKryesor").ContentUrl = komponente;

				cookie.Value = komponente;
				cookie.Expires = DateTime.Now.AddDays(1);
				Response.Cookies.Add(cookie);

				var komponenteFooter = DbCore.clsFunksione.shtoVarToUrl("FooterPanelInfo.aspx", "idTheme", idTheme.ToString());
				komponenteFooter = clsFunksione.shtoVarToUrl(komponenteFooter, ScopeManager.ScopeIdKey, scopeID);
				ASPxSplitter1.GetPaneByName("Footer").ContentUrl = komponenteFooter;

				var njoftimet = new DbCore.DbShare.colNjoftime(idPerdoruesi, DateTime.Now);
				hfState.Set("njoftimet", JsonConvert.SerializeObject(njoftimet));
				hfState.Set("afishonjoftime", user.ShfaqMesazhePopup);

			}
			if (IsPostBack && !IsCallback)
			{
				if (!HttpContext.Current.User.Identity.IsAuthenticated)
				{
					FormsAuthentication.RedirectToLoginPage();
				}
			}

		}
		public static string buildQueryStringNgaAmbienti(System.Collections.Specialized.NameValueCollection queryStringCol)
		{
			var removeString = "ambienti" + "=" + queryStringCol["ambienti"];
			var queryString = queryStringCol.ToString();
			var index = queryString.IndexOf(removeString);
			queryString = queryString.Remove(index, removeString.Length);
			queryString = queryString.Replace("&&", "&");
			queryString = queryString.StartsWith("&") ? queryString.Remove(0, 1) : queryString.EndsWith("&") ? queryString.Remove(queryString.Length - 1, 1) : queryString;
			return queryStringCol["ambienti"] + "?" + queryString;
		}
		protected void refreshFooter(object sender, EventArgs e)
		{
			ASPxSplitter1.GetPaneByName("Footer").ContentUrl = "FooterPanelInfo.aspx";
		}


		public void TeDrejta()
		{
			try
			{
				var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
				var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
				var idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);

				var dt = DbCore.DbAdmin.colTeDrejtaRoli.merrTeDrejtaRoliDheRaporteshMeEmraKomponentesh(idPerdoruesi, idNdermarrje, idviti);
				for (var i = 0; i < ASPxMenu1.Items.Count; i++)
				{
					var countmenu = 0;
					for (var j = 0; j < ASPxMenu1.Items[i].Items.Count; j++)
					{
						var countnenmenu = 0;
						for (var m = 0; m < ASPxMenu1.Items[i].Items[j].Items.Count; m++)
						{
							var countnmenu = 0;
							for (var p = 0; p < ASPxMenu1.Items[i].Items[j].Items[m].Items.Count; p++)
							{
								var countnennenmenu = 0;
								for (var q = 0; q < ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items.Count; q++)
								{
									if (ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Name != string.Empty)
									{
										if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
										{
											ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Visible = true;
											countnennenmenu++;
											continue;
										}
										var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Name.Split('&')[0] + "'");
										if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
										{
											ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Visible = true;
											countnennenmenu++;
										}
										else
										{
											ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Visible = false;
										}
									}
								}

								if (ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Name != string.Empty)
								{
									if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
									{
										ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
										countnmenu++;
										continue;
									}

									var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Name.Split('&')[0] + "'");
									if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
									{
										ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
										countnmenu++;
									}
									else
									{
										if (countnennenmenu > 0)
										{
											ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
											countnmenu++;
										}
										else
										{
											ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = false;
										}
									}
								}
								else
								{
									if (countnennenmenu > 0)
									{
										ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
										countnmenu++;
									}
									else
									{
										ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = false;
									}
								}
							}
							if (ASPxMenu1.Items[i].Items[j].Items[m].Name != string.Empty)
							{
								if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
								{
									ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
									countnenmenu++;
									continue;
								}
								if (ASPxMenu1.Items[i].Items[j].Items[m].Name == "ImportWK.aspx?lloji=importwk" && !DbCore.mySessionObjects.merrEshteMemeSesioni(Session))
								{
									ASPxMenu1.Items[i].Items[j].Items[m].Visible = false;
									continue;
								}
								var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Items[m].Name.Split('&')[0] + "'");
								if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
								{
									ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
									countnenmenu++;
								}
								else
								{
									if (countnmenu > 0)
									{
										ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
										countnenmenu++;
									}
									else
									{
										ASPxMenu1.Items[i].Items[j].Items[m].Visible = false;
									}
								}
							}
							else
							{
								if (countnmenu > 0)
								{
									ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
									countnenmenu++;
								}
								else
								{
									ASPxMenu1.Items[i].Items[j].Items[m].Visible = false;
								}
							}
						}
						if (ASPxMenu1.Items[i].Items[j].Name != string.Empty)
						{
							if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
							{
								ASPxMenu1.Items[i].Items[j].Visible = true;
								countmenu++;
								continue;
							}

							var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Name.Split('&')[0] + "'");
							if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
							{
								ASPxMenu1.Items[i].Items[j].Visible = true;
								countmenu++;
							}
							else
							{
								if (countnenmenu > 0)
								{
									ASPxMenu1.Items[i].Items[j].Visible = true;
									countmenu++;
								}
								else
								{
									ASPxMenu1.Items[i].Items[j].Visible = false;
								}
							}
						}
						else
						{
							if (countnenmenu > 0)
							{
								ASPxMenu1.Items[i].Items[j].Visible = true;
								countmenu++;
							}
							else
							{
								ASPxMenu1.Items[i].Items[j].Visible = false;
							}
						}
					}
					if (countmenu == 0)
					{
						ASPxMenu1.Items[i].Visible = false;
					}
					else
					{
						ASPxMenu1.Items[i].Visible = true;
					}
				}
				for (var k = 0; k < ASPxNavBar1.Groups.Count; k++) //fsheh/shfaq elemenet e navbarit sipas te drejtave
				{
					var count = 0;
					DevExpress.Web.NavBarGroup tmpGrup = ASPxNavBar1.Groups[k];

					if (tmpGrup.ToString() == "Dashboard")
					{
						var drMobile = dt.Select("KOMPONEMRI= 'Dashboard.aspx'");
						if (drMobile[0]["D_AMB"].ToString() == "1")
							count++;
					}

					if (tmpGrup.ToString() == "Mobile")
					{
						var drMobile = dt.Select("TEXTMODUL= 'Mobile'");
						for (var m = 0; m < drMobile.Length; m++)
						{
							if (drMobile[m]["KOMPONEMRI"].ToString() != string.Empty)
							{
								if (drMobile[m]["D_AMB"].ToString() == "1")
								{
									count++;
								}
							}
						}

					}
					for (var l = 0; l < tmpGrup.Items.Count; l++)
					{
						if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
						{
							tmpGrup.Items[l].Visible = true;
							count++;
							continue;
						}
						if (tmpGrup.Items[l].Name != string.Empty)
						{
							var dr = dt.Select("KOMPONEMRI= '" + tmpGrup.Items[l].Name.Split('&')[0] + "'");
							tmpGrup.Items[l].Visible = true;
							if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
							{
								tmpGrup.Items[l].Visible = true;
								count++;
							}
							else
							{
								tmpGrup.Items[l].Visible = false;
							}
						}
						else
							System.Diagnostics.Trace.WriteLine("Shume gabim - turp");
					}

					tmpGrup.Visible = true;
					tmpGrup.ClientVisible = count != 0;

				}
				//   shfaqHelp();
				DevExpress.Web.MenuItem ikonaImazhPerdoruesMenuLart = ASPxMenu1.Items.FindByName("ikonaImazhPerdorues");
				ikonaImazhPerdoruesMenuLart.Visible = true;
				ikonaImazhPerdoruesMenuLart.Items.FindByName("LupaPersonalizoPerdorues.aspx").Visible = true;
				ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe").Visible = true;
				ikonaImazhPerdoruesMenuLart.Items.FindByName("dalje").Visible = true;
				//DevExpress.Web.MenuItem grupitHelpMenuLart = ASPxMenu1.Items.FindByName("settings");
				//grupitHelpMenuLart.Visible = true;
				//grupitHelpMenuLart.ClientVisible = true;
				//ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe").ClientVisible = true;
				ASPxNavBar1.Groups.FindByName("settings").Visible = true;
				ASPxNavBar1.Groups.FindByName("settings").ClientVisible = true;
				if (ASPxNavBar1.Groups.FindByName("Mobile").ClientVisible)
				{
					ASPxNavBar1.Groups.FindByName("Mobile").ClientVisible = !(bool)hfState["kycurMobile"];
					ASPxNavBar1.Groups.FindByName("Mobile").Visible = !(bool)hfState["kycurMobile"];
				}
			}
			catch (Exception err)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
			}
		}

		private void enableMenuTeDrejta(int idPerdoruesi, ResourceManager rm, CultureInfo ci)
		{
			var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
			var KodViti = DbCore.mySessionObjects.ktheVitiNdermarrjes(Session).ToString();
			if (idNdermarrje == 0)
			{
				DbCore.clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
			}
			else
			{
				if (Request.QueryString["vjenNga"] != "CRM")
				{
					if ((Request.UrlReferrer != null && (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["vjenNga"] == "CRM"))
						|| (DbCore.mySessionObjects.merrObjectNgaSesioni(Session) != null && DbCore.mySessionObjects.merrObjectNgaSesioni(Session).ToString() == "shfaqdefault"))
					{
						int idTheme = clsThemesAmbjente.ktheIdTheme(idPerdoruesi);
						//string komponente = DbCore.DbAdmin.clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
						string komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, String.Empty, KodViti);
						if (komponente != "")
						{
							Response.Redirect(DbCore.clsFunksione.shtoVarToUrl(komponente, "idTheme", idTheme.ToString()), false);
							return;
						}
						DbCore.mySessionObjects.ruajObjectNeSesion(Session, "");
					}
				}

			}

			//DbCore.clsFunksione.vendosPeriudhenKlientSide(Session, hfPeriudhKontabel);
			TeDrejta();
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
			var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
			enableMenuTeDrejta(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), rm, ci);
		}
		protected void VerifikoEmail_Click(object sender, EventArgs e)
		{
			PasswordHelper.GjenroApiKey("");
		}

		/// <summary>
		/// Gjen elementin e menuse lart sipas rruges se emrave (Name) nga rrenja, p.sh. Menu("konfigurime", "instrumenta", "Shto_Vit.aspx").
		/// Nese elementi nuk ekziston kthehet nje element jashte menuse, keshtu heqja e nje elementi nga menuja nuk rrezon faqen.
		/// </summary>
		private MenuItem Menu(params string[] rruga)
		{
			var elementet = ASPxMenu1.Items;
			MenuItem elementi = null;
			foreach (var emri in rruga)
			{
				elementi = elementet.Cast<MenuItem>().FirstOrDefault(m => m.Name == emri);
				if (elementi == null)
					return new MenuItem();
				elementet = elementi.Items;
			}
			return elementi;
		}

		/// <summary>
		/// Gjen grupin e navbar-it sipas emrit; kthen nje grup jashte navbar-it nese nuk ekziston.
		/// </summary>
		private NavBarGroup NavGrup(string emri)
		{
			return ASPxNavBar1.Groups.FindByName(emri) ?? new NavBarGroup();
		}

		/// <summary>
		/// Gjen elementin e nje grupi te navbar-it sipas emrit; kthen nje element jashte navbar-it nese nuk ekziston.
		/// </summary>
		private NavBarItem NavElement(string grupi, string emri)
		{
			return ASPxNavBar1.Groups.FindByName(grupi)?.Items.FindByName(emri) ?? new NavBarItem();
		}

		/// <summary>
		/// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
		/// </summary>
		/// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
		private void EmrateLabelave(CultureInfo ci)
		{
			var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

			//ASPxHyperLink2.Text = " | " + rm.GetString("labelLogOut", ci);
			Menu("administrimi", "Webhooks.aspx").Visible = true;

			Menu("administrimi").Text = rm.GetString("MenuItemAdminstrimi", ci);
			Menu("administrimi", "Asistenti.aspx").Text = rm.GetString("MenuItemAsistenti", ci);
			Menu("administrimi", "MessageToAll.html").Text = "Dergo Mesazh";
			Menu("administrimi", "fjalekalimi").Text = rm.GetString("MenuItemFjalekalimi", ci);
			Menu("administrimi", "fjalekalimi", "PolitikaFjalekalimi.aspx").Text = rm.GetString("MenuItemPolitikaFjalekalimi", ci);
			Menu("administrimi", "fjalekalimi", "NdryshimFjalekalimi.aspx").Text = rm.GetString("MenuItemNdryshimFjalekalimi", ci);
			Menu("administrimi", "HistorikuEmail.aspx").Text = rm.GetString("MenuItemHistorikuEmail", ci);
			Menu("administrimi", "AuditimUser.aspx").Text = rm.GetString("MenuItemHyrjetDaljetNeProgram", ci);
			Menu("administrimi", "KonfigurimeEmail.aspx").Text = rm.GetString("MenuItemKonfigurimEmail", ci);
			Menu("administrimi", "KonfigurimeFtp.aspx").Text = rm.GetString("MenuItemKonfigurimFtp", ci);
			Menu("administrimi", "LogeSistemi.aspx").Text = rm.GetString("MenuItemLogeSistemi", ci);

			Menu("administrimi", "MbylljePeriudhe.html").Text = rm.GetString("MenuItemMbylljePeriudhe", ci);
			Menu("administrimi", "Shto_SkemaWorkFlow.aspx").Text = rm.GetString("MenuItemSkemaWorkFlow", ci);
			Menu("administrimi", "struktura-organizimit").Text = rm.GetString("MenuItemStrukturaOrganizimit", ci);
			Menu("administrimi", "struktura-organizimit", "LupaGrupNdermarrje.aspx").Text = rm.GetString("MenuItemGrupimNdermarrjesh", ci);
			Menu("administrimi", "struktura-organizimit", "Shto_Ndermarrje.aspx").Text = rm.GetString("MenuItemNdermarrjet", ci);
			Menu("administrimi", "struktura-organizimit", "Shto_DegeAdministrative.aspx").Text = rm.GetString("MenuItemDegetAdministrative", ci);
			Menu("administrimi", "struktura-organizimit", "StrukturaAdministrative.aspx").Text = rm.GetString("MenuItemDepartamentet", ci);
			Menu("administrimi", "te-drejtat").Text = rm.GetString("MenuItemTeDrejtat", ci);
			Menu("administrimi", "te-drejtat", "ShtoModifiko_Grup_Perdoruesish.aspx").Text = rm.GetString("MenuItemRolet", ci);
			Menu("administrimi", "te-drejtat", "Shto_Perdorues.aspx").Text = rm.GetString("MenuItemPerdoruesit", ci);
			Menu("administrimi", "te-drejtat", "Shto_Autorizimet.aspx").Text = rm.GetString("MenuItemAutorizimet", ci);
			Menu("administrimi", "Webhooks.aspx").Text = rm.GetString("MenuItemWebhooks", ci);

			Menu("konfigurime").Text = rm.GetString("MenuItemKonfigurime", ci);
			Menu("konfigurime", "amortizimi").Text = rm.GetString("MenuItemAmortizimi", ci);
			Menu("konfigurime", "amortizimi", "StandarteAmortizimi.aspx").Text = rm.GetString("MenuItemStandardeAmortizimi", ci);
			Menu("konfigurime", "amortizimi", "Shto_RregullaAmortizimi.aspx").Text = rm.GetString("MenuItemRregullaAmortizimi", ci);

			Menu("konfigurime", "cmimet").Text = rm.GetString("MenuItemCmimet", ci);
			Menu("konfigurime", "cmimet", "Shto_NivelCmimi.aspx").Text = rm.GetString("MenuItemNivelCmimi", ci);
			Menu("konfigurime", "cmimet", "percaktim-cmimi").Text = rm.GetString("MenuItemPercaktimCmimi", ci);
			Menu("konfigurime", "cmimet", "percaktim-cmimi", "CmimeArtikulli.aspx?lloji=shitje").Text = rm.GetString("MenuItemCmimetShitjeve", ci);
			Menu("konfigurime", "cmimet", "percaktim-cmimi", "CmimeArtikulli.aspx?lloji=blerje").Text = rm.GetString("MenuItemCmimetBlerje", ci);
			Menu("konfigurime", "dokumenta").Text = rm.GetString("MenuItemDokumenta", ci);
			Menu("konfigurime", "dokumenta", "KonfigurimRegjistrimi.aspx").Text = rm.GetString("MenuItemKategoriDokumenti", ci);
			Menu("konfigurime", "dokumenta", "GrupimDokumentash.aspx").Text = rm.GetString("MenuItemGrupetDokumentave", ci);
			Menu("konfigurime", "import-eksport").Text = rm.GetString("MenuItemEksportImportTeDhenash", ci);
			Menu("konfigurime", "import-eksport", "konfig-formati").Text = rm.GetString("MenuItemKonfigurimFormati", ci);
			Menu("konfigurime", "import-eksport", "konfig-formati", "KonfigurimFormatImporti.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("konfigurime", "import-eksport", "konfig-formati", "Shto_KonfigurimFormatImporti.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("konfigurime", "import-eksport", "Eksport.aspx").Text = rm.GetString("MenuItemEksport", ci);
			Menu("konfigurime", "import-eksport", "Import.aspx").Text = rm.GetString("MenuItemImport", ci);
			Menu("konfigurime", "import-eksport", "ImportWK.aspx?lloji=importwk").Text = rm.GetString("MenuItemImportShitjeWinlineCarta", ci);
			Menu("konfigurime", "import-eksport", "ImportWK.aspx?lloji=importfk").Text = rm.GetString("MenuItemImportFleteKontabel", ci);
			Menu("konfigurime", "import-eksport", "transferimDaljePopup").Text = rm.GetString("MenuItemTransferimDalje", ci);
			Menu("konfigurime", "ElementePerIntegrim.aspx").Text = rm.GetString("MenuItemElementePerIntegrim", ci);
			Menu("konfigurime", "instrumenta").Text = rm.GetString("MenuItemInstrumenta", ci);
			Menu("konfigurime", "instrumenta", "FormatNumrash.aspx").Text = rm.GetString("MenuItemFormatiNumrave", ci);
			Menu("konfigurime", "instrumenta", "FormatePrintimi.aspx").Text = rm.GetString("MenuItemFormatePrintimi", ci);
			Menu("konfigurime", "instrumenta", "Shto_FushatShtese.aspx").Text = rm.GetString("MenuItemFushatShtese", ci);
			Menu("konfigurime", "instrumenta", "GjeneroKodbar.aspx").Text = rm.GetString("menuItemGjeneroKodbar", ci);
			Menu("konfigurime", "instrumenta", "Shto_ModelInfoArtikulli.aspx").Text = rm.GetString("MenuItemInfo", ci);
			Menu("konfigurime", "instrumenta", "KonfigurimKasash.aspx").Text = rm.GetString("MenuItemKonfigurimKasash", ci);
			Menu("konfigurime", "instrumenta", "Shto_Monedhe.aspx").Text = rm.GetString("MenuItemMonedhat", ci);
			Menu("konfigurime", "instrumenta", "Shto_NrAutom.aspx").Text = rm.GetString("MenuItemNumratAutomatike", ci);
			Menu("konfigurime", "instrumenta", "Qytetet.aspx").Text = rm.GetString("MenuItemQytete", ci);
			Menu("konfigurime", "instrumenta", "Shto_NivelTvsh.aspx").Text = rm.GetString("MenuItemTaksat", ci);
			Menu("konfigurime", "instrumenta", "Shto_Vit.aspx").Text = rm.GetString("MenuItemVitet", ci);
			Menu("konfigurime", "instrumenta", "KategoriArkive.aspx").Text = rm.GetString("MenuItemKategoriArkive", ci);
			Menu("konfigurime", "instrumenta", "PostoEPaySlip.aspx").Text = rm.GetString("MenuItemPostoNeEPaySlip", ci);

			Menu("konfigurime", "Shto_KategoriShpenzimi.aspx").Text = rm.GetString("MenuItemKategoriShpenzimi", ci);
			Menu("konfigurime", "konfig-dokumenti").Text = rm.GetString("MenuItemKonfigurimDokumenti", ci);
			Menu("konfigurime", "konfig-dokumenti", "konfig-celje").Text = rm.GetString("MenuItemKonfigurimCelje", ci);
			Menu("konfigurime", "konfig-dokumenti", "konfig-celje", "KonfigDokumentash.aspx?idsuperkat=1").Text = rm.GetString("MenuItemLista", ci);
			Menu("konfigurime", "konfig-dokumenti", "konfig-celje", "KonfigurimDokumentash.aspx?idsuperkat=1&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("konfigurime", "konfig-dokumenti", "konfig-regjistrime").Text = rm.GetString("MenuItemKonfigurimeRegjistrime", ci);
			Menu("konfigurime", "konfig-dokumenti", "konfig-regjistrime", "KonfigDokumentash.aspx?idsuperkat=2").Text = rm.GetString("MenuItemLista", ci);
			Menu("konfigurime", "konfig-dokumenti", "konfig-regjistrime", "KonfigurimDokumentash.aspx?idsuperkat=2&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("konfigurime", "konfig-dokumenti", "KonfigDokumentash.aspx?idsuperkat=3").Text = rm.GetString("MenuItemKonfigurimeLupa", ci);
			Menu("konfigurime", "konfig-dokumenti", "KonfigDokumentash.aspx?idsuperkat=3", "KonfigDokumentash.aspx?idsuperkat=3").Text = rm.GetString("MenuItemLista", ci);
			Menu("konfigurime", "konfig-dokumenti", "KonfigDokumentash.aspx?idsuperkat=3", "KonfigurimDokumentash.aspx?idsuperkat=3&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("konfigurime", "qk").Text = rm.GetString("MenuItemQendraKosto", ci);
			Menu("konfigurime", "qk", "LlogariShperndarjeQK.aspx").Text = rm.GetString("MenuItemLlogariQK", ci);
			Menu("konfigurime", "qk", "KonfigurimeQK.aspx").Text = rm.GetString("MenuItemKonfigurimeQK", ci);
			Menu("konfigurime", "rap-financiare").Text = rm.GetString("MenuItemRaporteFinanciare", ci);
			Menu("konfigurime", "rap-financiare", "KonfigPASH.aspx?lloji=Bilanc").Text = rm.GetString("MenuItemFormatBilanci", ci);
			Menu("konfigurime", "rap-financiare", "KonfigPASH.aspx?lloji=Pash").Text = rm.GetString("MenuItemFormatPASH", ci);
			Menu("konfigurime", "rap-financiare", "KonfigPASH.aspx?lloji=Cashflow").Text = rm.GetString("MenuItemFormatCashFlow", ci);
			Menu("konfigurime", "rap-financiare", "KonfigPASH.aspx?lloji=Buxhetor").Text = rm.GetString("MenuItemFormatBuxhetore", ci);
			Menu("konfigurime", "rap-financiare", "format-OJF").Text = rm.GetString("MenuItemFormatOJF", ci);
			Menu("konfigurime", "rap-financiare", "format-OJF", "KonfigPASH.aspx?lloji=BilancOJF").Text = rm.GetString("MenuItemFormatBilanciOFJ", ci);
			Menu("konfigurime", "rap-financiare", "format-OJF", "KonfigPASH.aspx?lloji=PashOJF").Text = rm.GetString("MenuItemFormatPASHOJF", ci);
			Menu("konfigurime", "rap-financiare", "format-OJF", "KonfigPASH.aspx?lloji=CashflowOJF").Text = rm.GetString("MenuItemFormatCashFlowOJF", ci);

			Menu("konfigurime", "skedulimi-punonjesve").Text = rm.GetString("MenuItemSkedulimiPunonjesve", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "KonfigurimListOrari.aspx").Text = rm.GetString("MenuItemKonfigurimListeOrare", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "KalendarFestash.aspx").Text = rm.GetString("MenuItemKalendariFestave", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "LegjendaListOrareve.aspx").Text = rm.GetString("MenuItemLegjendaListOrareve", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "ProfesioneTitujPune.aspx").Text = rm.GetString("MenuItemProfesioneDhePozicione", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "Vendndodhjet.aspx").Text = rm.GetString("MenuItemVendodhjet", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "GrupimeLocaleGlobale.aspx").Text = rm.GetString("MenuItemLokaleGlobale", ci);
			Menu("konfigurime", "skedulimi-punonjesve", "KodeProfesione.aspx").Text = rm.GetString("MenuItemKodeProfesione", ci);

			Menu("konfigurime", "Shto_KPF.aspx").Text = rm.GetString("MenuItemStrukturatLlogarive", ci);
			Menu("konfigurime", "konfig-urdher-pagese").Text = rm.GetString("MenuItemKonfigurimUrdherPagesa", ci);
			Menu("konfigurime", "konfig-urdher-pagese", "KonfigUrdherPagese.aspx?lloji=Grup").Text = rm.GetString("MenuItemGrupe", ci);
			Menu("konfigurime", "konfig-urdher-pagese", "KonfigUrdherPagese.aspx?lloji=Titull").Text = rm.GetString("MenuItemKodProgrami", ci);
			Menu("konfigurime", "konfig-urdher-pagese", "KonfigUrdherPagese.aspx?lloji=Kapitull").Text = rm.GetString("MenuItemKapituj", ci);
			Menu("konfigurime", "zbritje-analitike").Text = rm.GetString("MenuItemZbritjeAnalitike", ci);
			Menu("konfigurime", "zbritje-analitike", "Shto_NivelZbritje.aspx").Text = rm.GetString("MenuItemNivelZbritje", ci);
			Menu("konfigurime", "zbritje-analitike", "ZbritjeAnalitike.aspx").Text = rm.GetString("MenuItemPercaktimZbritje", ci);
			Menu("konfigurime", "konfig-gis").Text = rm.GetString("MenuItemGISKonfigurime", ci);
			Menu("konfigurime", "konfig-gis", "GISWorkspace.aspx").Text = rm.GetString("MenuItemGISKonfiguroWS", ci);


			Menu("celje").Text = rm.GetString("MenuItemCelje", ci);
			Menu("celje", "Shto_AgjentShitje.aspx").Text = rm.GetString("MenuItemAgjentetShitjes", ci);
			Menu("celje", "arka-banka").Text = rm.GetString("MenuItemArkaBanka", ci);
			Menu("celje", "arka-banka", "Shto_Banka.aspx?ab=arka").Text = rm.GetString("MenuItemCeljaArkave", ci);
			Menu("celje", "arka-banka", "Shto_Banka.aspx?ab=banka").Text = rm.GetString("MenuItemCeljaBankave", ci);
			Menu("celje", "artikujt").Text = rm.GetString("MenuItemArtikujt", ci);
			Menu("celje", "artikujt", "Shto_Artikull.aspx?llojiart=afatshkurter").Text = rm.GetString("MenuItemArtikujt", ci);
			Menu("celje", "artikujt", "Shto_Artikull.aspx?llojiart=aqt").Text = rm.GetString("MenuItemArtikujtAfatgjate", ci);
			Menu("celje", "artikujt", "atribute-artikujsh").Text = rm.GetString("MenuItemAtributeTeArtikujve", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "NjesiArtikulli.aspx").Text = rm.GetString("MenuItemNjesiteMatese", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "KodifikimArtikulli.aspx?llojiart=afatshkurter").Text = rm.GetString("MenuItemGrupetArtikujve", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "KodifikimArtikulli.aspx?llojiart=aqt").Text = rm.GetString("MenuItemGrupetArtikujveAfatgjate", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "DetajimeArtikulli.aspx").Text = rm.GetString("MenuItemDatajime", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "Shto_SerialeUnike.aspx").Text = rm.GetString("MenuItemSerialeUnike", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "Shto_KategoriSeriali.aspx").Text = rm.GetString("MenuItemKategoriSeriali", ci);
			Menu("celje", "artikujt", "atribute-artikujsh", "Shto_FormatSeriali.aspx").Text = rm.GetString("MenuItemFormatSeriali", ci);
			Menu("celje", "Shto_Automjete.aspx").Text = rm.GetString("MenuItemAutomjetet", ci);

			Menu("celje", "komponente-buxheti").Text = rm.GetString("MenuItem_Buxheti", ci);
			Menu("celje", "komponente-buxheti", "B_KategoriBuxhetimi.aspx?lupe=false").Text = rm.GetString("MenuItem_01_KategoriBuxhetimi", ci);
			Menu("celje", "komponente-buxheti", "B_KomponenteBuxheti.aspx").Text = rm.GetString("MenuItem_KomponenteBuxheti", ci);
			Menu("celje", "komponente-buxheti", "B_KomponenteBuxhetiVlere.aspx").Text = rm.GetString("MenuItemHedhjaTeDhenave", ci);

			Menu("celje", "elemnte-page").Text = rm.GetString("MenuItemElementePage", ci);
			Menu("celje", "elemnte-page", "struktura-page").Text = rm.GetString("MenuItemStrukturaPage", ci);
			Menu("celje", "elemnte-page", "struktura-page", "KategoriPage.aspx").Text = rm.GetString("MenuItemKategoriPage", ci);
			Menu("celje", "elemnte-page", "struktura-page", "ShtesaPage.aspx").Text = rm.GetString("MenuItemShtesaPage", ci);
			Menu("celje", "elemnte-page", "struktura-page", "SigurimeSuplementare.aspx").Text = rm.GetString("MenuItemSigurimeSuplementare", ci);
			Menu("celje", "elemnte-page", "Shto_KomponentePage.aspx?lloji=true").Text = rm.GetString("MenuItemKomponenteListepagese", ci);
			Menu("celje", "elemnte-page", "Shto_KomponentePage.aspx?lloji=false").Text = rm.GetString("MenuItemKomponentePage", ci);
			Menu("celje", "elemnte-page", "Shto_Sigurimet.aspx").Text = rm.GetString("MenuItemSigurimet", ci);
			Menu("celje", "elemnte-page", "Shto_Tatime.aspx").Text = rm.GetString("MenuItemTatimeMbiPagen", ci);
			Menu("celje", "elemente-prodhimi").Text = rm.GetString("MenuItemElementeProdhimi", ci);
			Menu("celje", "elemente-prodhimi", "Shto_Burime.aspx").Text = rm.GetString("MenuItemBurimet", ci);
			Menu("celje", "elemente-prodhimi", "Shto_Aktivitete.aspx").Text = rm.GetString("MenuItemAktivitetet", ci);

			Menu("celje", "karta-klienti").Text = rm.GetString("MenuItemKartaKlienti", ci);
			Menu("celje", "karta-klienti", "Shto_KartaKlienti.aspx").Text = rm.GetString("MenuItemKarteKlient", ci);
			Menu("celje", "karta-klienti", "Shto_PolitikeKartaKlienti.aspx").Text = rm.GetString("MenuItemPolitikeKartaKlienti", ci);
			Menu("celje", "karta-klienti", "ShperndaDhurate.aspx").Text = rm.GetString("MenuItemKartaShperndaDhurata", ci);
			Menu("celje", "kf").Text = rm.GetString("MenuItemKlientFurnitor", ci);
			Menu("celje", "kf", "Shto_KlientFurnitor.aspx?kf=furnitor").Text = rm.GetString("MenuItemFurnitoret", ci);
			Menu("celje", "kf", "Shto_KlientFurnitor.aspx?kf=klient").Text = rm.GetString("MenuItemKlientet", ci);
			Menu("celje", "kf", "atribute-kf").Text = rm.GetString("MenuItemAtributePerKlientFurnitor", ci);
			Menu("celje", "kf", "atribute-kf", "Shto_AfateMaturimi.aspx").Text = rm.GetString("MenuItemAfatetEMaturimit", ci);
			Menu("celje", "kf", "atribute-kf", "Shto_KategoriZbritje.aspx").Text = rm.GetString("MenuItemKategoriteZbritjeve", ci);
			Menu("celje", "kf", "atribute-kf", "MenyraTransporti.aspx").Text = rm.GetString("MenuItemLlojeteTransportit", ci);
			Menu("celje", "kf", "atribute-kf", "KushteDergimi.aspx").Text = rm.GetString("MenuItemKushtDergimi", ci);
			Menu("celje", "kf", "atribute-kf", "KushtePagese.aspx").Text = rm.GetString("MenuItemKushtPagese", ci);
			Menu("celje", "kf", "atribute-kf", "GrupimeKlientFurnitor.aspx?kf=klient").Text = rm.GetString("MenuItemGrupetKlienteve", ci);
			Menu("celje", "kf", "atribute-kf", "GrupimeKlientFurnitor.aspx?kf=furnitor").Text = rm.GetString("MenuItemGrupetFurnitoreve", ci);

			Menu("celje", "Shto_Llogari.aspx").Text = rm.GetString("MenuItemLlogarite", ci);
			Menu("celje", "LlojDifekti.aspx").Text = rm.GetString("MenuItemLlojDefekti", ci);
			Menu("celje", "Makro.aspx").Text = rm.GetString("MenuItemMakro", ci);
			Menu("celje", "njesi-administrative").Text = rm.GetString("MenuItemNjesiAdministrative", ci);
			Menu("celje", "njesi-administrative", "Shto_PikeShitjeFurnizimi.aspx?sf=shitje").Text = rm.GetString("MenuItemPikatShitjeve", ci);
			Menu("celje", "njesi-administrative", "Shto_PikeShitjeFurnizimi.aspx?sf=furnizim").Text = rm.GetString("MenuItemPikatFurnizimit", ci);
			Menu("celje", "njesi-administrative", "Shto_NjesiAdministrative.aspx").Text = rm.GetString("MenuItemMagazinat", ci);
			Menu("celje", "njesi-administrative", "Shto_NjesiVartese.aspx").Text = rm.GetString("MenuItemNjesiVartese", ci);
			Menu("celje", "njesi-administrative", "Shto_NjesiProdhimi.aspx").Text = rm.GetString("MenuItemNjesiProdhimi", ci);
			Menu("celje", "Shto_Punonjes.aspx").Text = rm.GetString("MenuItemPunonjes", ci);

			Menu("celje", "komponente-qk").Text = rm.GetString("MenuItemKomponenteQendraKosto", ci);
			Menu("celje", "komponente-qk", "Shto_QendraKosto.aspx").Text = rm.GetString("MenuItemQendraKostoShto", ci);
			Menu("celje", "komponente-qk", "Shto_ObjektivaKosto.aspx").Text = rm.GetString("MenuItemObjektivaKosto", ci);
			Menu("celje", "komponente-qk", "Shto_SkemaQendraKosto.aspx").Text = rm.GetString("MenuItemSkemaQendraKosto", ci);
			Menu("celje", "StatusRiparimi.aspx").Text = rm.GetString("MenuItemStatusRiparimi", ci);
			Menu("celje", "Shto_Transportues.aspx").Text = rm.GetString("MenuItemTransportues", ci);




			Menu("regjistrime").Text = rm.GetString("MenuItemRegjistrime", ci);

			Menu("regjistrime", "amortizimi").Text = rm.GetString("MenuItemAmortizimi", ci);
			Menu("regjistrime", "amortizimi", "regjistrim-amortizimi").Text = rm.GetString("MenuItemRegjistrimAmortizimi", ci);
			Menu("regjistrime", "amortizimi", "regjistrim-amortizimi", "RegjistrimAmortizimi.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "amortizimi", "regjistrim-amortizimi", "Shto_RegjistrimAmortizimi.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi").Text = rm.GetString("MenuItemRivleresimAmortizimi", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi", "amortizim-fillestar").Text = rm.GetString("MenuItemAmortizimiFillestar", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi", "amortizim-fillestar", "RivleresimeAmortizimi.aspx?lloj=amortizim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi", "amortizim-fillestar", "Shto_RivleresimeAmortizimi.aspx?lloj=amortizim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi", "rilveresim").Text = rm.GetString("MenuItemRivleresim", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi", "rilveresim", "RivleresimeAmortizimi.aspx?lloj=rivleresim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "amortizimi", "rivleresim-amortizimi", "rilveresim", "Shto_RivleresimeAmortizimi.aspx?lloj=rivleresim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "amortizimi", "RillogaritjeAmortizimi.aspx").Text = rm.GetString("MenuItemRillogaritjeAmortizimi", ci);
			Menu("regjistrime", "aprovime").Text = rm.GetString("MenuItemAprovime", ci);
			Menu("regjistrime", "aprovime", "ListeAprovimi.aspx?status=aprovim").Text = rm.GetString("MenuItemAprovimet", ci);
			Menu("regjistrime", "aprovime", "ListeAprovimi.aspx?status=kerkese").Text = rm.GetString("MenuItemKerkesePerAprovim", ci);

			Menu("regjistrime", "fatura-blerje").Text = rm.GetString("MenuItemFaturatBlerjeve", ci);
			Menu("regjistrime", "fatura-blerje", "RegjistrimDokumentash.aspx?shitje_blerje=blerje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "fatura-blerje", "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);

			Menu("regjistrime", "fatura-blerje", "FaturaBlerjeEinvoice.aspx").Text = "Fatura Blerje Einvoice";
			Menu("regjistrime", "fatura-blerje", "FaturaBlerjeEinvoice.aspx").Visible = true;


			Menu("regjistrime", "EkzekutimBuxheti").Text = rm.GetString("MenuItem_EkzekutimBuxheti", ci);
			Menu("regjistrime", "EkzekutimBuxheti", "PlanifikimEkzekutimBuxheti").Text = rm.GetString("MenuItem_PlanifikimEkzekutimBuxheti", ci);
			Menu("regjistrime", "EkzekutimBuxheti", "PlanifikimEkzekutimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=planifikimEkzekutimi").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "EkzekutimBuxheti", "PlanifikimEkzekutimBuxheti", "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=planifikimEkzekutimi&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "EkzekutimBuxheti", "EkzekutimBuxheti").Text = rm.GetString("MenuItem_EkzekutimBuxheti", ci);
			Menu("regjistrime", "EkzekutimBuxheti", "EkzekutimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=ekzekutim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "EkzekutimBuxheti", "EkzekutimBuxheti", "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=ekzekutim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);

			Menu("regjistrime", "flete-doganore").Text = rm.GetString("MenuItemFleteDoganore", ci);
			Menu("regjistrime", "flete-doganore", "import").Text = rm.GetString("MenuItemImport", ci);
			Menu("regjistrime", "flete-doganore", "import", "FleteDoganore.aspx?lloji=import").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "flete-doganore", "import", "Shto_FleteDoganore.aspx?lloji=import&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "flete-doganore", "export").Text = rm.GetString("MenuItemEksport", ci);
			Menu("regjistrime", "flete-doganore", "export", "FleteDoganore.aspx?lloji=export").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "flete-doganore", "export", "Shto_FleteDoganore.aspx?lloji=export&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "fk").Text = rm.GetString("MenuItemFleteKontabel", ci);
			Menu("regjistrime", "fk", "FleteKontabel.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "fk", "Shto_FleteKontabel.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "fk", "KontabilizimDokumenti.aspx").Text = rm.GetString("AmbjentKontabilizimi", ci);

			Menu("regjistrime", "lidhja-dokumentave").Text = rm.GetString("MenuItemLidhjaDokumentave", ci);
			Menu("regjistrime", "lidhja-dokumentave", "ListaLidhjaDokumentave.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "lidhja-dokumentave", "LidhjaDokumentave.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "listpagesa").Text = rm.GetString("MenuItemListPagesa", ci);
			Menu("regjistrime", "listpagesa", "ListPagesa.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "listpagesa", "Shto_ListPagesa.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "magazina").Text = rm.GetString("filterMagazina", ci);
			Menu("regjistrime", "magazina", "regjistrime-hyrje").Text = rm.GetString("NavBarItemDokumentatHyrjeve", ci);
			Menu("regjistrime", "magazina", "regjistrime-hyrje", "RegjistrimMagazine.aspx?lloj=hyrje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "magazina", "regjistrime-hyrje", "Shto_RegjistrimMagazine.aspx?lloj=hyrje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "magazina", "regjistrime-dalje").Text = rm.GetString("NavBarItemDokumentatDaljeve", ci);
			Menu("regjistrime", "magazina", "regjistrime-dalje", "RegjistrimMagazine.aspx?lloj=dalje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "magazina", "regjistrime-dalje", "Shto_RegjistrimMagazine.aspx?lloj=dalje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "magazina", "NdryshimCmimi.aspx").Text = rm.GetString("MenuItemNdryshimCmimi", ci);
			Menu("regjistrime", "magazina", "inventarizim-artikulli").Text = rm.GetString("NavBarItemInventarizimASH", ci);
			Menu("regjistrime", "magazina", "inventarizim-artikulli", "RegjistrimInventarizimi.aspx?lloj=ash").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "magazina", "inventarizim-artikulli", "Shto_RegjistrimInventarizimi.aspx?lloj=ash&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "magazina", "inventarizim-aqt").Text = rm.GetString("NavBarItemInventarizimAGJ", ci);
			Menu("regjistrime", "magazina", "inventarizim-aqt", "RegjistrimInventarizimi.aspx?lloj=agj").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "magazina", "inventarizim-aqt", "Shto_RegjistrimInventarizimi.aspx?lloj=agj&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "magazina", "ndryshim-sasi-cmim").Text = rm.GetString("MenuItemNdryshimCmimSasi", ci);
			Menu("regjistrime", "magazina", "ndryshim-sasi-cmim", "RegjistrimNdryshimCmimSasi.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "magazina", "ndryshim-sasi-cmim", "Shto_RegjistrimNdryshimCmimSasi.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "magazina", "RivleresimMagazine.aspx").Text = rm.GetString("MenuItemRivleresimInventarit", ci);

			Menu("regjistrime", "prodhim").Text = rm.GetString("MenuItemProdhimi", ci);
			Menu("regjistrime", "prodhim", "planifikim-prodhimi").Text = rm.GetString("MenuItemPlanifikimiProdhimit", ci);
			Menu("regjistrime", "prodhim", "planifikim-prodhimi", "Planifikimi.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "prodhim", "planifikim-prodhimi", "Shto_Planifikim.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "prodhim", "skedulim-prodhimi").Text = rm.GetString("MenuItemSkedulimProdhimit", ci);
			Menu("regjistrime", "prodhim", "skedulim-prodhimi", "SkedulimProdhimi.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "prodhim", "skedulim-prodhimi", "Shto_SkedulimProdhimi.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "prodhim", "ekzekutim-prodhimi").Text = rm.GetString("MenuItemEkzekutimiProdhimit", ci);
			Menu("regjistrime", "prodhim", "ekzekutim-prodhimi", "EkzekutimProdhimi.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "prodhim", "ekzekutim-prodhimi", "Shto_Ekzekutim.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "prodhim", "GjeneroProjektProdhimi.aspx").Text = rm.GetString("MenuItemGjeneroProjektinProdhimit", ci);
			Menu("regjistrime", "regjistrim-qk").Text = rm.GetString("MenuItemQendraKosto", ci);
			Menu("regjistrime", "regjistrim-qk", "RegjistrimQendraKosto.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "regjistrim-qk", "Shto_RegjistrimQendraKosto.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "receta-optike").Text = rm.GetString("MenuItemRecetaOptike", ci);
			Menu("regjistrime", "receta-optike", "RecetaOptike.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "receta-optike", "Shto_RecetaOptike.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "regjistrim-riparimi").Text = rm.GetString("MenuItemRegjistrimRiparimi", ci);
			Menu("regjistrime", "regjistrim-riparimi", "RegjistrimRiparimi.aspx").Text = rm.GetString("MenuItemStatusiCelMeProbleme", ci);
			Menu("regjistrime", "regjistrim-riparimi", "Shto_RegjistrimRiparimi.aspx").Text = rm.GetString("MenuItemRiparimiAparateveTePrishura", ci);
			Menu("regjistrime", "rezervime").Text = rm.GetString("MenuItemRezervime", ci);
			Menu("regjistrime", "rezervime", "regjistrime-hyrje").Text = rm.GetString("NavBarItemDokumentatHyrjeve", ci);
			Menu("regjistrime", "rezervime", "regjistrime-hyrje", "RegjistrimRezervimi.aspx?lloj=hyrje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "rezervime", "regjistrime-hyrje", "Shto_RegjistrimRezervimi.aspx?lloj=hyrje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "rezervime", "regjistrime-dalje").Text = rm.GetString("NavBarItemDokumentatDaljeve", ci);
			Menu("regjistrime", "rezervime", "regjistrime-dalje", "RegjistrimRezervimi.aspx?lloj=dalje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "rezervime", "regjistrime-dalje", "Shto_RegjistrimRezervimi.aspx?lloj=dalje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);

			Menu("regjistrime", "SigurimBuxheti").Text = rm.GetString("MenuItem_SigurimBuxheti", ci);
			Menu("regjistrime", "SigurimBuxheti", "PlanifikimBuxheti").Text = rm.GetString("MenuItem_03_PlanifikimBuxheti", ci);
			Menu("regjistrime", "SigurimBuxheti", "PlanifikimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=planifikim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "SigurimBuxheti", "PlanifikimBuxheti", "B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=planifikim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "SigurimBuxheti", "MiratimBuxheti").Text = rm.GetString("MenuItem_02_MiratimBuxheti", ci);
			Menu("regjistrime", "SigurimBuxheti", "MiratimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=miratim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "SigurimBuxheti", "MiratimBuxheti", "B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=miratim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "SigurimBuxheti", "AlokimBuxheti").Text = rm.GetString("MenuItem_04_AlokimBuxheti", ci);
			Menu("regjistrime", "SigurimBuxheti", "AlokimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=alokim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "SigurimBuxheti", "AlokimBuxheti", "B_Shto_RegjistrimAlokimBuxheti.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "SigurimBuxheti", "RialokimBuxheti").Text = rm.GetString("MenuItem_RialokimBuxheti", ci);
			Menu("regjistrime", "SigurimBuxheti", "RialokimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=rialokim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "SigurimBuxheti", "RialokimBuxheti", "B_Shto_RegjistrimRialokimBuxheti.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "SigurimBuxheti", "PerfitimBuxheti").Text = rm.GetString("MenuItem_PerfitimBuxheti", ci);
			Menu("regjistrime", "SigurimBuxheti", "PerfitimBuxheti", "B_RegjistrimBuxheti.aspx?lloji=perfitim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "SigurimBuxheti", "PerfitimBuxheti", "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=perfitim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);

			Menu("regjistrime", "fatura-shitjesh").Text = rm.GetString("MenuItemFaturatShitjeve", ci);
			Menu("regjistrime", "fatura-shitjesh", "RegjistrimDokumentash.aspx?shitje_blerje=shitje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "fatura-shitjesh", "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "fatura-shitjesh", "FaturaShitjeEinvoice.aspx").Text = rm.GetString("MenuItemRaportShitjetEinvoice", ci);

			Menu("regjistrime", "fatura-shitjesh", "FaturaShitjeEinvoice.aspx").Visible = true;

			Menu("regjistrime", "fatura-shitjesh", "GjeneroFaturePermbledhese.aspx").Text = rm.GetString("menuItemFaturaShitjeGjeneroFaturePermbledhese", ci);
			Menu("regjistrime", "fatura-shitjesh", "GjenerimAutomatik.aspx").Text = rm.GetString("menuItemFaturaShitjeRuajteAutomatikeDokumentave", ci);



			Menu("regjistrime", "shperndarja-shpenzimeve").Text = rm.GetString("MenuItemShperndarjaShpenzimeve", ci);
			Menu("regjistrime", "shperndarja-shpenzimeve", "ShperndarjeShpenzimesh.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "shperndarja-shpenzimeve", "Shto_ShperndarjeShpenzimesh.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "urdher-pagesa").Text = rm.GetString("MenuItemUrdherPagesa", ci);
			Menu("regjistrime", "urdher-pagesa", "UrdherPagesa.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "urdher-pagesa", "Shto_UrdherPagesa.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-arka-banka").Text = rm.GetString("MenuItemVeprimeArkaBanka", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka").Text = rm.GetString("MenuItemVeprimeArka", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka", "arketimet").Text = rm.GetString("MenuItemArketimet", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka", "arketimet", "VeprimeBanka.aspx?lloji=arketim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka", "arketimet", "ShtoVeprimBanka.aspx?lloji=arketim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka", "pagesat").Text = rm.GetString("MenuItemPagesat", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka", "pagesat", "VeprimeBanka.aspx?lloji=pagese").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-arka", "pagesat", "ShtoVeprimBanka.aspx?lloji=pagese&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka").Text = rm.GetString("MenuItemVeprimeBanka", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka", "derdhjet-bankare").Text = rm.GetString("MenuItemDerdhjetBankare", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka", "derdhjet-bankare", "VeprimeBanka.aspx?lloji=derdhje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka", "derdhjet-bankare", "ShtoVeprimBanka.aspx?lloji=derdhje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka", "terheqjet-bankare").Text = rm.GetString("MenuItemTerheqjetBankare", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka", "terheqjet-bankare", "VeprimeBanka.aspx?lloji=terheqje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-arka-banka", "veprime-banka", "terheqjet-bankare", "ShtoVeprimBanka.aspx?lloji=terheqje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-kf").Text = rm.GetString("MenuItemVeprimeMeKlientFurnitor", ci);
			Menu("regjistrime", "veprime-kf", "veprime-kf").Text = rm.GetString("MenuItemVeprimeKlientFurnitor", ci);
			Menu("regjistrime", "veprime-kf", "veprime-kf", "VeprimeKF.aspx").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-kf", "veprime-kf", "Shto_VeprimeKF.aspx").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-kf", "azhronim-kf").Text = rm.GetString("MenuItemAzhornimeKlientFurnitor", ci);
			Menu("regjistrime", "veprime-kf", "azhronim-kf", "AzhornimKlientFurnitor.aspx?vep=azhornim").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-kf", "azhronim-kf", "Shto_AzhornimKlientFurnitor.aspx?vep=azhornim").Text = rm.GetString("MenuItemERe", ci);
			Menu("regjistrime", "veprime-kf", "mbyllje-kf").Text = rm.GetString("MenuItemMbylljeKlientFurnitor", ci);
			Menu("regjistrime", "veprime-kf", "mbyllje-kf", "AzhornimKlientFurnitor.aspx?vep=mbyllje").Text = rm.GetString("MenuItemLista", ci);
			Menu("regjistrime", "veprime-kf", "mbyllje-kf", "Shto_AzhornimKlientFurnitor.aspx?vep=mbyllje").Text = rm.GetString("MenuItemERe", ci);




			Menu("raportet").Text = rm.GetString("MenuItemRaportet", ci);
			Menu("raportet", "Raportet.aspx?idmod=21").Text = rm.GetString("MenuItemAmortizimi", ci);
			Menu("raportet", "Raportet.aspx?idmod=2").Text = rm.GetString("MenuItemRaportArka", ci);
			Menu("raportet", "Raportet.aspx?idmod=6").Text = rm.GetString("MenuItemRaportBanka", ci);
			Menu("raportet", "Raportet.aspx?idmod=19").Text = rm.GetString("MenuItemRaportBussinesIntelligence", ci);
			Menu("raportet", "Raportet.aspx?idmod=13").Text = rm.GetString("MenuItemRaportBlerjet", ci);
			Menu("raportet", "FaturaBlerjeEinvoice.aspx").Text = rm.GetString("MenuItemRaportFaturatEBlrejes", ci);
			Menu("raportet", "FaturaBlerjeEinvoice.aspx").Visible = true;

			Menu("raportet", "Raportet.aspx?idmod=17").Text = rm.GetString("MenuItemRaportBurimetNjerezore", ci);
			Menu("raportet", "Raportet.aspx?idmod=57").Text = rm.GetString("MenuItemBuxheti", ci);
			Menu("raportet", "Raportet.aspx?idmod=16").Text = rm.GetString("MenuItemRaportInventari", ci);
			Menu("raportet", "Raportet.aspx?idmod=9").Text = rm.GetString("MenuItemRaportKlientetdheFurnitoret", ci);
			Menu("raportet", "Raportet.aspx?idmod=7").Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
			Menu("raportet", "Raportet.aspx?idmod=22").Text = rm.GetString("MenuItemRaporteMenaxheriale", ci);
			Menu("raportet", "Raportet.aspx?idmod=18").Text = rm.GetString("MenuItemRaportProdhimi", ci);
			Menu("raportet", "Raportet.aspx?idmod=24").Text = rm.GetString("MenuItemRaportCRM", ci);
			Menu("raportet", "Raportet.aspx?idmod=20").Text = rm.GetString("MenuItemRaportQendratKostos", ci);
			Menu("raportet", "Raportet.aspx?idmod=12").Text = rm.GetString("MenuItemRaportShitjet", ci);
			Menu("raportet", "FaturaShitjeEinvoice.aspx").Text = rm.GetString("MenuItemRaportShitjetEinvoice", ci);
			Menu("raportet", "FaturaShitjeEinvoice.aspx").Visible = true;
			Menu("raportet", "RaporteGrida.aspx?lloji=GjendjaEMagazines").Text = rm.GetString("MenuItemRaportiGjendjaEMagazines", ci);
			Menu("raportet", "RaporteGrida.aspx?lloji=GjendjaEArtikujveMeSeriale").Text = rm.GetString("MenuItemRaportGjendjaEArtikujveMeSeriale", ci);
			Menu("raportet", "RaporteGrida.aspx?lloji=gjendjaArtikujveIMEI").Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEI", ci);
			Menu("raportet", "RaporteGrida.aspx?lloji=gjendjaArtikujveIMEIEkspozitor").Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEIEkspozitor", ci);


			DevExpress.Web.MenuItem ikonaImazhPerdoruesMenuLart = ASPxMenu1.Items.FindByName("ikonaImazhPerdorues");
			ikonaImazhPerdoruesMenuLart.Visible = true;
			var perdoruesEmerItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("LupaPersonalizoPerdorues.aspx");
			perdoruesEmerItem.Visible = true;
			perdoruesEmerItem.Text = DbCore.mySessionObjects.kthePerdorues(Session).PerdoruesUsername;
			var mesazheItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe");
			mesazheItem.Visible = true;
			mesazheItem.Text = rm.GetString("labelmesazh", ci);
			var daljeItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("dalje");
			daljeItem.Visible = true;
			daljeItem.Text = rm.GetString("labelLogOut", ci);
			daljeItem.NavigateUrl = $"{DbCore.IMBUtils.Paths.defaultLoginPath}?arsye=logout&google=true";
			//ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe").ClientVisible = true;
			var fjalekalimItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("NdryshimFjalekalimi.aspx");
			fjalekalimItem.Text = rm.GetString("labelEmailFjalekalimi", ci);









			ASPxNavBar1.Groups.FindByName("settings").Visible = true;

			NavGrup("Administrimi").Text = rm.GetString("MenuItemAdminstrimi", ci);
			NavElement("Administrimi", "ShtoModifiko_Grup_Perdoruesish.aspx").Text = rm.GetString("MenuItemRolet", ci);
			NavElement("Administrimi", "Shto_Perdorues.aspx").Text = rm.GetString("MenuItemPerdoruesit", ci);
			NavElement("Administrimi", "Shto_Ndermarrje.aspx").Text = rm.GetString("MenuItemNdermarrjet", ci);

			NavGrup("kontabiliteti").Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
			NavElement("kontabiliteti", "Shto_KPF.aspx").Text = rm.GetString("MenuItemStrukturatLlogarive", ci);
			NavElement("kontabiliteti", "Shto_Llogari.aspx").Text = rm.GetString("MenuItemLlogarite", ci);
			NavElement("kontabiliteti", "FleteKontabel.aspx").Text = rm.GetString("NavBarItemAmbjentiKontabilizimit", ci);

			NavGrup("inventari").Text = rm.GetString("MenuItemRaportInventari", ci);
			NavElement("inventari", "Shto_Artikull.aspx?llojiart=afatshkurter").Text = rm.GetString("MenuItemArtikujt", ci);
			NavElement("inventari", "RegjistrimMagazine.aspx?lloj=hyrje").Text = rm.GetString("NavBarItemDokumentatHyrjeve", ci);
			NavElement("inventari", "Shto_RegjistrimMagazine.aspx?lloj=hyrje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemRegjistrimetHyrjeve", ci);
			NavElement("inventari", "RegjistrimMagazine.aspx?lloj=dalje").Text = rm.GetString("NavBarItemDokumentatDaljeve", ci);
			NavElement("inventari", "Shto_RegjistrimMagazine.aspx?lloj=dalje&shtim_modifikim=shtim").Text = rm.GetString("MenuItemRegjistrimetDaljeve", ci);

			NavGrup("blerjeShitje").Text = rm.GetString("NavBarItemBlerjetDheShitjet", ci);
			NavElement("blerjeShitje", "CmimeArtikulli.aspx?lloji=shitje").Text = rm.GetString("MenuItemCmimetShitjeve", ci);
			NavElement("blerjeShitje", "CmimeArtikulli.aspx?lloji=blerje").Text = rm.GetString("MenuItemCmimetBlerje", ci);
			NavElement("blerjeShitje", "Shto_KlientFurnitor.aspx?kf=klient").Text = rm.GetString("MenuItemKlientet", ci);
			NavElement("blerjeShitje", "Shto_KlientFurnitor.aspx?kf=furnitor").Text = rm.GetString("MenuItemFurnitoret", ci);
			NavElement("blerjeShitje", "RegjistrimDokumentash.aspx?shitje_blerje=blerje").Text = rm.GetString("MenuItemFaturatBlerjeve", ci);
			NavElement("blerjeShitje", "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim").Text = rm.GetString("NavBarItemRegjistrimetBlerjeve", ci);
			NavElement("blerjeShitje", "RegjistrimDokumentash.aspx?shitje_blerje=shitje").Text = rm.GetString("MenuItemFaturatShitjeve", ci);
			NavElement("blerjeShitje", "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim").Text = rm.GetString("NavBarItemRegjistrimetShitjeve", ci);
			NavElement("blerjeShitje", "FaturaBlerjeEinvoice.aspx?").Text = "Fatura Blerje Einvoice";
			NavElement("blerjeShitje", "FaturaBlerjeEinvoice.aspx?").Visible = true;


			NavGrup("arkaBanka").Text = rm.GetString("MenuItemRaportArkadheBanka", ci);
			NavElement("arkaBanka", "VeprimeBanka.aspx?lloji=derdhje").Text = rm.GetString("MenuItemDerdhjetBankare", ci);
			NavElement("arkaBanka", "ShtoVeprimBanka.aspx?lloji=derdhje&shtim_modifikim=shtim").Text = rm.GetString("NavBarItemRegjistrimiDerdhjeve", ci);
			NavElement("arkaBanka", "VeprimeBanka.aspx?lloji=terheqje").Text = rm.GetString("MenuItemTerheqjetBankare", ci);
			NavElement("arkaBanka", "ShtoVeprimBanka.aspx?lloji=terheqje&shtim_modifikim=shtim").Text = rm.GetString("NavBarItemRegjistrimiTerheqjeve", ci);
			NavElement("arkaBanka", "VeprimeBanka.aspx?lloji=arketim").Text = rm.GetString("MenuItemArketimet", ci);
			NavElement("arkaBanka", "ShtoVeprimBanka.aspx?lloji=arketim&shtim_modifikim=shtim").Text = rm.GetString("NavBarItemRegjistrimiArketimeve", ci);
			NavElement("arkaBanka", "VeprimeBanka.aspx?lloji=pagese").Text = rm.GetString("MenuItemPagesat", ci);
			NavElement("arkaBanka", "ShtoVeprimBanka.aspx?lloji=pagese&shtim_modifikim=shtim").Text = rm.GetString("NavBarItemRegjistrimiPagesave", ci);




			NavGrup("hr").Text = rm.GetString("MenuItemRaportBurimetNjerezore", ci);
			NavElement("hr", "StrukturaAdministrative.aspx").Text = rm.GetString("MenuItemDepartamentet", ci);
			NavElement("hr", "Shto_KomponentePage.aspx?lloji=true").Text = rm.GetString("MenuItemKomponenteListepagese", ci);
			NavElement("hr", "Shto_Punonjes.aspx").Text = rm.GetString("MenuItemPunonjes", ci);
			NavElement("hr", "ListPagesa.aspx").Text = rm.GetString("MenuItemListPagesa", ci);
			//urdher pagesa
			NavGrup("pagesa").Text = rm.GetString("MenuItemKonfigurimUrdherPagesa", ci);
			NavElement("pagesa", "KonfigUrdherPagese.aspx?lloji=Grup").Text = rm.GetString("MenuItemGrupe", ci);
			NavElement("pagesa", "KonfigUrdherPagese.aspx?lloji=Titull").Text = rm.GetString("MenuItemTituj", ci);
			NavElement("pagesa", "KonfigUrdherPagese.aspx?lloji=Kapitull").Text = rm.GetString("MenuItemKapituj", ci);
			NavElement("pagesa", "Shto_UrdherPagesa.aspx").Text = rm.GetString("NavBarItemRegjUrdherpagesave", ci);
			NavElement("pagesa", "UrdherPagesa.aspx").Text = rm.GetString("NavBarItemUrdherpagesat", ci);



			NavGrup("prodhimi").Text = rm.GetString("MenuItemRaportProdhimi", ci);
			NavElement("prodhimi", "Planifikimi.aspx").Text = rm.GetString("MenuItemPlanifikimiProdhimit", ci);
			NavElement("prodhimi", "Shto_Planifikim.aspx").Text = rm.GetString("NavBarItemPlanifikimRi", ci);
			NavElement("prodhimi", "EkzekutimProdhimi.aspx").Text = rm.GetString("MenuItemEkzekutimiProdhimit", ci);
			NavElement("prodhimi", "Shto_Ekzekutim.aspx").Text = rm.GetString("NavBarItemEkzekutimRi", ci);
			NavElement("prodhimi", "GjeneroProjektProdhimi.aspx").Text = rm.GetString("MenuItemGjeneroProjektinProdhimit", ci);

			NavGrup("qk").Text = rm.GetString("MenuItemQendraKosto", ci);
			NavElement("qk", "KonfigurimeQK.aspx").Text = rm.GetString("MenuItemKonfigurimeQK", ci);
			NavElement("qk", "Shto_QendraKosto.aspx").Text = rm.GetString("MenuItemQendraKostoShto", ci);
			NavElement("qk", "Shto_SkemaQendraKosto.aspx").Text = rm.GetString("MenuItemSkemaQendraKosto", ci);
			NavElement("qk", "Shto_ObjektivaKosto.aspx").Text = rm.GetString("MenuItemRegjistrimQK", ci);
			NavElement("qk", "RegjistrimQendraKosto.aspx").Text = rm.GetString("MenuItemRegjistrimQKRe", ci);



			NavGrup("amortizimi").Text = rm.GetString("MenuItemAmortizimi", ci);
			NavElement("amortizimi", "Shto_Artikull.aspx?llojiart=aqt").Text = rm.GetString("MenuItemArtikujtAfatgjate", ci);
			NavElement("amortizimi", "RivleresimeAmortizimi.aspx?lloj=amortizim").Text = rm.GetString("MenuItemAmortizimiFillestar", ci);
			NavElement("amortizimi", "Shto_RivleresimeAmortizimi.aspx?lloj=amortizim&shtim_modifikim=shtim").Text = rm.GetString("MenuItemAmortizimiFillestarRi", ci);
			NavElement("amortizimi", "RegjistrimAmortizimi.aspx").Text = rm.GetString("MenuItemRegjistrimAmortizimi", ci);
			NavElement("amortizimi", "Shto_RegjistrimAmortizimi.aspx").Text = rm.GetString("MenuItemRegjistrimAmortizimiRi", ci);




			NavGrup("aprovime-dokumentash").Text = rm.GetString("MenuItemAprovimetDok", ci);
			NavElement("aprovime-dokumentash", "ListeAprovimi.aspx?status=aprovim").Text = rm.GetString("MenuItemAprovimet", ci);
			NavElement("aprovime-dokumentash", "ListeAprovimi.aspx?status=kerkese").Text = rm.GetString("MenuItemKerkesePerAprovim", ci);


			NavGrup("rap").Text = rm.GetString("MenuItemRaportet", ci);
			NavElement("rap", "Raportet.aspx?idmod=7").Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
			NavElement("rap", "Raportet.aspx?idmod=13").Text = rm.GetString("MenuItemRaportBlerjet", ci);
			NavElement("rap", "FaturaBlerjeEinvoice.aspx?").Visible = true;
			NavElement("rap", "Raportet.aspx?idmod=12").Text = rm.GetString("MenuItemRaportShitjet", ci);
			NavElement("rap", "Raportet.aspx?idmod=16").Text = rm.GetString("MenuItemRaportInventari", ci);
			NavElement("rap", "Raportet.aspx?idmod=9").Text = rm.GetString("MenuItemRaportKlientetdheFurnitoret", ci);
			NavElement("rap", "Raportet.aspx?idmod=2").Text = rm.GetString("MenuItemRaportArka", ci);
			NavElement("rap", "Raportet.aspx?idmod=6").Text = rm.GetString("MenuItemRaportBanka", ci);
			NavElement("rap", "Raportet.aspx?idmod=17").Text = rm.GetString("MenuItemRaportBurimetNjerezore", ci);
			NavElement("rap", "Raportet.aspx?idmod=18").Text = rm.GetString("MenuItemRaportProdhimi", ci);
			NavElement("rap", "Raportet.aspx?idmod=20").Text = rm.GetString("MenuItemRaportQendratKostos", ci);
			NavElement("rap", "Raportet.aspx?idmod=21").Text = rm.GetString("MenuItemAmortizimi", ci);
			NavElement("rap", "Raportet.aspx?idmod=19").Text = rm.GetString("MenuItemRaportBussinesIntelligence", ci);
			NavElement("rap", "RaporteGrida.aspx?lloji=GjendjaEMagazines").Text = rm.GetString("MenuItemRaportiGjendjaEMagazines", ci);
			NavElement("rap", "RaporteGrida.aspx?lloji=GjendjaEArtikujveMeSeriale").Text = rm.GetString("MenuItemRaportGjendjaEArtikujveMeSeriale", ci);
			NavElement("rap", "RaporteGrida.aspx?lloji=gjendjaArtikujveIMEI").Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEI", ci);
			NavElement("rap", "RaporteGrida.aspx?lloji=gjendjaArtikujveIMEIEkspozitor").Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEIEkspozitor", ci);
			NavElement("rap", "Raportet.aspx?idmod=22").Text = rm.GetString("MenuItemRaporteMenaxheriale", ci);
			NavElement("rap", "Raportet.aspx?idmod=24").Text = rm.GetString("MenuItemRaporteCRM", ci);    // Raportet CRM
			NavElement("rap", "Raportet.aspx?idmod=57").Text = rm.GetString("MenuItemRaportBuxheti", ci);






			NavGrup("bi").Text = rm.GetString("MenuItemGroupBusinessIntelligence", ci); //"Business Intelligence";
			NavElement("bi", "Raport_PivotGrid.aspx?idModuli=12").Text = rm.GetString("labelShitje", ci);
			NavElement("bi", "Raport_PivotGrid.aspx?idModuli=16").Text = rm.GetString("lblRaportMagazina", ci);
			NavElement("bi", "Raport_PivotGrid.aspx?idModuli=13").Text = rm.GetString("labelBlerje", ci);


			NavGrup("map").Text = rm.GetString("MenuGroupHarta", ci);
			NavElement("map", "GoogleHarte.aspx?lloji=mag").Text = rm.GetString("MenuItem_0_Harta", ci);  // Hartat e Njesive Administrative
			NavElement("map", "GoogleHarte.aspx?lloji=magshitje").Text = rm.GetString("MenuItem_1_Harta", ci);   // Hartat e shitjeve sipas magazinave
			NavElement("map", "GoogleHarte.aspx?lloji=klientshitje").Text = rm.GetString("MenuItem_2_Harta", ci);   // Harta e shitjeve sipas klienteve
			NavElement("map", "GoogleHarte.aspx?lloji=funritorblerje").Text = rm.GetString("MenuItem_3_Harta", ci);   // Harta e shitjeve sipas furnitoreve
			NavElement("map", "GoogleHarte.aspx?lloji=marzhishitje").Text = rm.GetString("MenuItem_4_Harta_Marzhi", ci);   //   Marzhi i shitjes sipas klienteve
			NavElement("map", "GoogleHarte.aspx?lloji=pikashitje").Text = rm.GetString("MenuItem_5_Harta", ci);   //   Harta e shitjeve sipas pikave te shitjes
			NavElement("map", "GoogleHarte.aspx?lloji=maggjendje").Text = rm.GetString("MenuItem_6_Harta", ci);   //  Harta e gjendjes se magazinave
			NavElement("map", "GoogleHarte.aspx?lloji=amortizimShqiptar").Text = rm.GetString("MenuItem_7_Harta", ci);    // Harta e amortizimit te aseteve ne perqindje  
			NavElement("map", "GoogleHarte.aspx?lloji=klientKoordinata").Text = rm.GetString("MenuItem_HartaEKlienteve", ci);    //Harta e klienteve



			NavGrup("crm").Text = rm.GetString("MenuItemGrupCRM", ci); //"CRM";
			NavElement("crm", "CRMDefault.aspx").Text = rm.GetString("MenuItemCRM", ci);
			NavElement("crm", "CRMRouteAgjenti.aspx").Text = rm.GetString("CRMRoute", ci);
			NavElement("crm", "CRMFushaAnkete.aspx").Text = rm.GetString("CRMFushaAnkete", ci);
			NavElement("crm", "CRMAnketa.aspx").Text = rm.GetString("CRMAnketa", ci);
			NavElement("crm", "CRMListaAnketa.aspx").Text = rm.GetString("CRMListeAnketa", ci);
			NavElement("crm", "CRMLidhAnkete.aspx").Text = rm.GetString("CRMLidhAnkete", ci);
			NavElement("crm", "CRMHistoriku.aspx").Text = rm.GetString("CRMHistoriku", ci);
			NavElement("crm", "CRMDetyra.aspx").Text = rm.GetString("CRMDetyra", ci);
			NavElement("crm", "Raportet.aspx?idmod=24").Text = rm.GetString("CRMRaporte", ci);

			NavGrup("gis").Text = rm.GetString("MenuItemGroupGIS", ci);  // "GIS"; 
			NavElement("gis", "GISDefault.aspx").Text = rm.GetString("MenuItemGIS", ci);


			NavGrup("analizeBuxheti").Text = rm.GetString("MenuItemGrupAnalizBuxheti", ci);//"Analiza e Buxhetit";
			NavElement("analizeBuxheti", "ABKonfiguroFusha.aspx").Text = rm.GetString("MenuItem_0_AnalizBuzheti", ci); //Konfigurimi i zerave per ambjentet e analizes se buxhetit//
			NavElement("analizeBuxheti", "ABParashikimShpenzimeKonfig.aspx").Text = rm.GetString("MenuItem_3_AnalizBuxheti", ci);  //Regjistrimi i buxhetit permbledhes//
			NavElement("analizeBuxheti", "ABKonfiguroShpenzimeOperative.aspx").Text = rm.GetString("MenuItem_4_AnalizBuxheti", ci);   //Regjistrimi i parashikimit te shpenzimeve per personelin//
			NavElement("analizeBuxheti", "ABBuxhetPermbledhes.aspx").Text = rm.GetString("MenuItem_5_AnalizBuxheti", ci);   //Regjistrimi i parashikimit te te ardhurave//
			NavElement("analizeBuxheti", "ABParashikimShpenzPersoneli.aspx").Text = rm.GetString("MenuItem_6_AnalizBuxheti", ci);   //Regjistrimi i shpenzimeve kapitale//
			NavElement("analizeBuxheti", "ABParashikimTeArdhura.aspx").Text = rm.GetString("MenuItem_7_AnalizBuxheti", ci);    //Regjistrimi i projektbuxhetit per tre vite
			NavElement("analizeBuxheti", "ABShpenzimeKapitale.aspx").Text = rm.GetString("MenuItem_8_AnalizBuxheti", ci); //Regjistrimi i planifikimit te produkteve  // 
			NavElement("analizeBuxheti", "ABPBuxheti3Vjecar.aspx").Text = rm.GetString("MenuItem_9_AnalizBuxheti", ci);  //Regjistrimi i shpenzimeve operative 
			NavElement("analizeBuxheti", "ABPlanifikimiIProdukteve.aspx").Text = rm.GetString("MenuItem_12_AnalizBuxheti", ci);  //Regjistrimi i pasqyres organike
			NavElement("analizeBuxheti", "ABShpenzimeOperative.aspx").Text = rm.GetString("MenuItem_13_AnalizBuxheti", ci); //Regjistrimi i evidences statistikore
			NavElement("analizeBuxheti", "ABInventariPerdorues.aspx").Text = rm.GetString("MenuItem_31_AnalizBuxheti", ci); //Planifikim dhe realizim
			NavElement("analizeBuxheti", "ABInventariVite.aspx").Text = rm.GetString("MenuItem_28_AnalizBuxheti", ci); //Regjistrim i realizimit te prokurimeve publike
			NavElement("analizeBuxheti", "ABPasqyraOrganike.aspx").Text = rm.GetString("MenuItem_30_AnalizBuxheti", ci); //Regjistrim i parashikimit te prokurimeve publike
			NavElement("analizeBuxheti", "ABEvidencaStatistikore.aspx").Text = rm.GetString("MenuItem_14_AnalizBuxheti", ci);  //Raporti per projekt buxhetin permbledhes
			NavElement("analizeBuxheti", "ABPlanifikimRealizim.aspx").Text = rm.GetString("MenuItem_15_AnalizBuxheti", ci); //Raporti per parashikimin e te ardhurave
			NavElement("analizeBuxheti", "ABProkurimePublike.aspx").Text = rm.GetString("MenuItem_16_AnalizBuxheti", ci); //Raporti per parashikimin e shpenzimeve per personelin
			NavElement("analizeBuxheti", "ABProkurimePublikeParashikim.aspx").Text = rm.GetString("MenuItem_17_AnalizBuxheti", ci);  //Raporti per projekt buxhetin ne zerin e shpenzimeve operative ne vitet pasardhes
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=BuxhetPermbledhes").Text = rm.GetString("MenuItem_18_AnalizBuxheti", ci);  //Raporti per projekt buxhetin ne zerin e shpenzimeve operative ne 3 vitet pasardhese
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ParashikimiTeArdhura").Text = rm.GetString("MenuItem_22_AnalizBuxheti", ci);   //Raporti per parashikimin e shpenzimeve kapitale
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ParashikimShpenzPersoneli").Text = rm.GetString("MenuItem_20_AnalizBuxheti", ci);  //Raporti per projekt buxhetin 3 vjecar
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ShpenzimeOperative").Text = rm.GetString("MenuItem_19_AnalizBuxheti", ci);     //Raporti per shpenzimet operative ne baze mujore
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ShpenzimeOperative3Vjecar").Text = rm.GetString("MenuItem_21_AnalizBuxheti", ci); //Raporti per planifikimin e produkteve te programit
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ShpenzimeKapitale").Text = rm.GetString("MenuItem_23_AnalizBuxheti", ci);  //Raporti per parashikimin e shpenzimeve per vitin pasardhes(Raportuese)
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=PBuxheti3Vjecar").Text = rm.GetString("MenuItem_24_AnalizBuxheti", ci);            //Raporti permbledhes per shpenzimet operative
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ShpenzimeOperativeMujore").Text = rm.GetString("MenuItem_27_AnalizBuxheti", ci);  // Raporti i evidences statistikore(Raportuese)
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=PlanifikimiIProdukteve").Text = rm.GetString("MenuItem_25_AnalizBuxheti", ci);      //Raporti i inventarit sipas viteve (Raportuese)
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ParashikimShpenzimeshRaportuese").Text = rm.GetString("MenuItem_26_AnalizBuxheti", ci);     //Raporti i inventarit sipas perdoruesve(Raportuese)
			NavElement("analizeBuxheti", "ABRaporti.aspx?raporti=ShpenzimeOperativePermbledhese").Text = rm.GetString("MenuItem_29_AnalizBuxheti", ci);     //Regjistri i realizimit te prokurimeve publike
			NavElement("analizeBuxheti", "ABPivotGrid.aspx?raporti=EvidencaStatistikore").Text = rm.GetString("MenuItem_32_AnalizBuxheti", ci);     //Tabela permbledhese e planifikimeve dhe realizimeve

			//Buxheti
			NavGrup("buxheti").Text = rm.GetString("MenuItemBuxheti", ci);
			NavElement("buxheti", "B_KategoriBuxhetimi.aspx?lupe=false").Text = rm.GetString("MenuItem_01_KategoriBuxhetimi", ci);
			NavElement("buxheti", "B_KomponenteBuxheti.aspx").Text = rm.GetString("MenuItem_KomponenteBuxheti", ci);
			NavElement("buxheti", "B_KomponenteBuxhetiVlere.aspx").Text = rm.GetString("MenuItemHedhjaTeDhenave", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=planifikim").Text = rm.GetString("MenuItem_03_PlanifikimBuxheti", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=miratim").Text = rm.GetString("MenuItem_02_MiratimBuxheti", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=alokim").Text = rm.GetString("MenuItem_04_AlokimBuxheti", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=rialokim").Text = rm.GetString("MenuItem_RialokimBuxheti", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=perfitim").Text = rm.GetString("MenuItem_PerfitimBuxheti", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=planifikimEkzekutimi").Text = rm.GetString("MenuItem_PlanifikimEkzekutimBuxheti", ci);
			NavElement("buxheti", "B_RegjistrimBuxheti.aspx?lloji=ekzekutim").Text = rm.GetString("MenuItem_EkzekutimBuxheti", ci);




			NavGrup("Mobile").Text = rm.GetString("MobileMenu", ci);
			//Settings
			NavGrup("settings").Text = rm.GetString("MenuItemSettings", ci);


			hfState.Set("MenuItemMbyll", rm.GetString("MenuItemMbyll", ci));
			hfState.Set("labelRuajNdryshimet", rm.GetString("labelRuajNdryshimet", ci));
			hfState.Set("labelTitulliModal", rm.GetString("labelTitulliModal", ci));
			hfState.Set("labelMesazhModal", rm.GetString("labelMesazhModal", ci));
			hfState.Set("MenuItemFshirje", rm.GetString("MenuItemFshirje", ci));

			popupUniversal.HeaderText = rm.GetString("txtZgjidhPeriudhenKontabel", ci);


		}
	}
}
