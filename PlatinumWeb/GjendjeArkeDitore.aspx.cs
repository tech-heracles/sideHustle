using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Fiskalizimi.API;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
	public partial class GjendjeArkeDitore : MyPageBase
	{
		private const string komponenteEmri = "GjendjeArkeDitore.aspx";
		private const string idKompon = "20045";
		private int idKonfig;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!DbCore.mySessionObjects.isLogedIn(Session))
			{
				DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
			}

			int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
			if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
			{
				Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
			}

			int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
			int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
			var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
			int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
			var cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);

			if (!IsPostBack)
			{
				mbushHiddenFieldMePerkthime();
				hfState.Set("idGjuha", IdGjuha);
				hfState.Set("idNdermarrje", idNdermarrje);
				konfiguroVleraFillestare(idPerd, idNdermarrje, rm, cultinf, idgjuha);
				mbushListeGjendjeshDitore(idNdermarrje);
				var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
				tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "Konfigurime Gride");
				tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, komponenteEmri);
				hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
				hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
				konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
				GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvGjendjeDitore", gvGjendjeDitore, cmbKonfigurimi.Text.Split(';')[0], idKompon, idgjuha);
			}
			else
			{
				mbushGrideNgaSessioni();
				konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
			}

			GridUtil.konfigGrideListeEMadhePaTheme(gvGjendjeDitore, "IdGjendjeDitore");
			percaktoTemplateMenu(ASPxMenu1, idViti, idPerd, idNdermarrje);
			idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
			gvGjendjeDitore.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerd, idNdermarrje, idViti, idgjuha, idKonfig, komponenteEmri, rm, cultinf);

			AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
		}

		protected void ASPxMenu1_DataBound(object sender, EventArgs e)
		{
			percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
		}
		protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
		{
			if (e.Item.Name == "Ruaj")
			{
				Page.Validate("entries");
				ruajGjendjeDitore();
			}
		}
		private void ruajGjendjeDitore()
		{
			if (!Page.IsValid)
			{
				hfStatusi.Value = "false";
				return;
			}

			int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
			int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

			try
			{
				clsMesazh mesazh;
				clsGjendjeArkeDitore gjendjeArkeDitore;
				if (hfShtimModifikim.Value == "shtim")
				{

					string kodi = cmbArka.Text.Substring(0, cmbArka.Text.IndexOf(" "));
					var arka = new clsBanka();
					arka.mbushBankeSipasKodit(kodi, idNdermarrje);
					clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
					if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
					{
						if (nderm.Fiskalizimi)
						{
							if (arka.KodiTCR == "" || arka.KodiTCR == null)
								clsMenuInfo.ShtoMesazhInformues(MenuInfo, "Arka Nuk Ka Kodin TCR!", pnlMesazhi);
							else
							{
								var gjnerimArkeDitore = clsFunksioneFiskalizimi.gjeneroVeprimeMeArken(new clsNdermarrje(idNdermarrje), txtVlera.Text, dteData.Text, kodi, arka.KodiTCR, "INITIAL");
								var kerkesa = clsFunksioneFiskalizimi.InvokeService(gjnerimArkeDitore, "", false);
							}
						}
					}

					gjendjeArkeDitore = new clsGjendjeArkeDitore(int.Parse(hfId.Value.ToString()), int.Parse(cmbArka.Value.ToString()), Convert.ToDateTime(dteData.Text), Convert.ToDouble(txtVlera.Text), idNdermarrje, 1, idPerdorues);
					mesazh = gjendjeArkeDitore.Ruaj();
				}

				else
				{
					gjendjeArkeDitore = new clsGjendjeArkeDitore(int.Parse(hfId.Value.ToString()), Convert.ToDouble(txtVlera.Text), idNdermarrje, 1, idPerdorues);
					mesazh = gjendjeArkeDitore.Modifiko();
				}
				if (mesazh.Status)
				{
					clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
					hfStatusi.Value = "true";

					if (hfShtimModifikim.Value == "shtim")
						shtoRreshtNeGrid(idNdermarrje, gjendjeArkeDitore.IdGjendjeDitore);
					else
						modifikoRreshtNeGrid(idNdermarrje, gjendjeArkeDitore.IdGjendjeDitore);
				}
				else
				{
					hfStatusi.Value = "false";
					clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
				}
				ASPxPageControl1.ActiveTabIndex = 0;
			}
			catch (Exception ex)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimRuajtje"], pnlMesazhi);
			}
		}

		private void shtoRreshtNeGrid(int idNdermarrje, int idGjendje)
		{
			if (gvGjendjeDitore.DataSource != null)
			{
				DataTable dt = (DataTable)gvGjendjeDitore.DataSource;
				DataRow[] drs = dt.Select("IdGjendjeDitore = " + idGjendje);
				if (drs.Length > 0)
					throw new Exception("Ky regjistrim ekziston ne gride!");

				DataRow newDr = clsGjendjeArkeDitore.merrGjendjeDitoreSipasIdDR(idGjendje);
				dt.ImportRow(newDr);
			}
			else
				mbushListeGjendjeshDitore(idNdermarrje);

			konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
		}
		private void modifikoRreshtNeGrid(int idNdermarrje, int idGjendje)
		{
			if (gvGjendjeDitore.DataSource != null)
			{
				DataTable dt = (DataTable)gvGjendjeDitore.DataSource;
				DataRow[] drs = dt.Select("IdGjendjeDitore = " + idGjendje);
				if (drs.Length > 1)
					throw new DbCore.MyException("Dy rreshta me te njejten Id!");
				if (drs.Length == 0)
					return;
				DataRow dr = drs[0];
				DataRow newArtDr = clsGjendjeArkeDitore.merrGjendjeDitoreSipasIdDR(idGjendje);

				object[] arr = newArtDr.ItemArray;
				dr.ItemArray = arr;
			}
			else
				mbushListeGjendjeshDitore(idNdermarrje);
		}
		protected void ButtonOk_Click2(object sender, EventArgs e)
		{
			int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
			if (new clsNdermarrje(idNdermarrje).Fiskalizimi)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mundeni te fshini arken diore!", pnlMesazhi);
				return;
			}
			List<object> rreshtat;
			if (ASPxPageControl1.ActiveTabIndex == 0)
			{
				rreshtat = gvGjendjeDitore.GetSelectedFieldValues("IdGjendjeDitore");
			}
			else
			{
				rreshtat = new List<object>();
				rreshtat.Add(hfId.Value);
			}

			if (rreshtat.Count == 0)
			{
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgSelektoniNjeRresht"], pnlMesazhi);
				return;
			}
			var mesazh = new DbCore.clsMesazh();
			int TeFshire = 0;

			foreach (object id in rreshtat)
			{
				clsGjendjeArkeDitore gjendjeArkeDitore = new clsGjendjeArkeDitore();
				gjendjeArkeDitore.IdGjendjeDitore = int.Parse(id.ToString());

				mesazh = gjendjeArkeDitore.Fshi();

				if (mesazh.Status)
				{
					hiqRreshtNgaGrida(gjendjeArkeDitore.IdGjendjeDitore);
					TeFshire++;
					ASPxPageControl1.ActiveTabIndex = 0;
					hfStatusi.Value = "true";
				}
			}

			if (TeFshire == 1)
				clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeRreshti"], pnlMesazhi);

			else if (TeFshire > 1)
				clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeRreshtash"], pnlMesazhi);

			else
				clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Rreshti nuk u fshi nga grida!", pnlMesazhi);
		}
		private void hiqRreshtNgaGrida(int idGjendje)
		{
			if (gvGjendjeDitore.DataSource != null)
			{
				DataTable dt = (DataTable)gvGjendjeDitore.DataSource;
				DataRow[] drs = dt.Select("IdGjendjeDitore = " + idGjendje);
				if (drs.Length > 1)
					throw new Exception("Dy rreshta me te njejten Id!");
				if (drs.Length == 0)
					return;
				DataRow dr = drs[0];
				dt.Rows.Remove(dr);
				gvGjendjeDitore.DataBind();
			}
			else
				mbushListeGjendjeshDitore(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
		}
		private void mbushHiddenFieldMePerkthime()
		{
			ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["MenuItemLista"];
			ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["MenuItemERe"];
			hfState.Set("msgZgjidhniArken", MessagesResource.Messages["msgZgjidhniArken"]);
		}
		private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
		{
			ASPxPageControl1.ActiveTabIndex = 0;
			ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 3, "GJAD", rm, cultinf, idGjuha);
			cmbKonfigurimi.SelectedIndex = 0;
			ConfigureAspxComboBox.mbushComboArkat(IdNdermarrja, IdPerdoruesi, cmbArka, false);
			ConfigureAspxComboBox.shtoKolonaPerArkaBanka(cmbArka);
			ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbArka);

			var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
			konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
			hfKonfillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
		}
		private void mbushListeGjendjeshDitore(int idNdermarrje)
		{
			var dt = clsGjendjeArkeDitore.merrGjendjeArkeDitoreSipasNderm(idNdermarrje);
			DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
			gvGjendjeDitore.DataSource = dt;
			gvGjendjeDitore.DataBind();
			dt.Dispose();
		}
		private void konfiguroGride(int idNdermarrje, string kodKonfigurimi)
		{
			GridUtil.konfigGrideListeEMadhePaTheme(gvGjendjeDitore, "IdGjendjeDitore");
			gvGjendjeDitore.Columns["#"].Width = 30;
			gvGjendjeDitore.Columns["#"].VisibleIndex = 0;
		}
		private void mbushGrideNgaSessioni()
		{
			DataTable tmpObject;
			bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
			if (!sukses)
				mbushListeGjendjeshDitore((int)hfState["idNdermarrje"]);
			else
			{
				gvGjendjeDitore.DataSource = tmpObject;
				gvGjendjeDitore.DataBind();
				tmpObject.Dispose();
			}
		}
		protected void gvGjendjeDitore_DataBound(object sender, EventArgs e)
		{
			if (gvGjendjeDitore.Columns["#"] == null)
			{
				var check = new DevExpress.Web.GridViewCommandColumn("#");
				check.ShowSelectCheckbox = true;
				check.SetColVisibleIndex(0);
				check.Width = Unit.Percentage(2);

				gvGjendjeDitore.Settings.ShowFilterRow = true;
				gvGjendjeDitore.Columns.Add(check);
				gvGjendjeDitore.KeyFieldName = "IdGjendjeDitore";
				gvGjendjeDitore.SettingsBehavior.AllowSelectByRowClick = true;
				gvGjendjeDitore.SettingsBehavior.AllowFocusedRow = true;
				gvGjendjeDitore.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
				gvGjendjeDitore.Settings.ShowFilterRowMenu = true;
			}
		}
		protected void gvGjendjeDitore_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
		{
			var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
			if (e.CallbackName == "COLUMNMOVE" && gvGjendjeDitore.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
			{
				gvGjendjeDitore.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
			}
			GridUtil.ToolTipButonaveMbiGride(gvGjendjeDitore, cultinf, rm);
		}
		protected void gvGjendjeDitore_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
		{
			if (e.Column.FieldName == "IdGjendjeDitore")
			{
				try
				{
					var vlera = Converter.ConvertToInt(e.Value);
					if (vlera == -3 || vlera == 0)
					{
						e.Criteria = null;
					}
				}
				catch (Exception err)
				{
					ImbLogger.Error(err.Message);
					e.Criteria = null;
				}
			}
		}

		/// <summary>
		/// Metoda qe thirret kur grida ben callback
		/// </summary>
		/// <param name="sender">Derguesi</param>
		/// <param name="e">Argumentat</param>
		protected void gvGjendjeDitore_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
		{
			var idkomponente = string.Empty;
			var kodkonfigurimi = string.Empty;

			var arr = e.Parameters.Split(';');
			if (arr.Length == 2)
			{
				kodkonfigurimi = arr[1];
				idkomponente = arr[0];
			}
			else
			{
				idkomponente = e.Parameters;
			}

			gvGjendjeDitore.Selection.UnselectAll();
		}
		private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
		{
			clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponenteEmri, this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
		}

		protected void cmbArka_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
		{
			if (IsCallback)
			{
				if (Request.Params["__CALLBACKID"].Contains("cmbArka"))
				{
					var value = 0;
					if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
						return;
					ConfigureAspxComboBox.mbushComboArkaBankaById((ASPxComboBox)source, value);
				}
			}
		}

		protected void cmbArka_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
		{
			if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbArka"))
				ConfigureAspxComboBox.mbushComboArkaBankaSipasFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, Request.QueryString["lloji"], IdPerdoruesi, IdNdermarrja);
		}
	}
}