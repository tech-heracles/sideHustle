using DbCore;
using DevExpress.Web;
using System;
using System.Web.UI;
using System.Globalization;
using System.Resources;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class KonfigurimeFtp : MyPageBase
    {
        private const string komponenteEmri = "KonfigurimeFtp.aspx";
        private const int idKomponente = 3047;
        private int idKonfig;
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
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
                EmrateTabeve();
                //mbushHiddenFieldMePerkthime(cultinf, rm);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                konfiguroVleraFillestare(idPerd, idNdermarrje, rm, cultinf, idgjuha);
                mbushListeKonfigurimeshFtp(idNdermarrje);
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "Konfigurime Gride");
                //hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, komponenteEmri);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvKonfigurimeFTP", gvKonfigurimeFTP, cmbKonfigurimi.Text.Split(';')[0], "3047", idgjuha);
            }
            else
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                mbushListeKonfigurimeshFtpNgaSessioni();
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
            }
            emraTeLabelave(DbCore.mySessionObjects.ktheCultureInfo(Session));
            GridUtil.konfigGrideListeEMadhePaTheme(gvKonfigurimeFTP, "IdKonfigurimFtp");
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerd, idNdermarrje);
            idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
            gvKonfigurimeFTP.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerd, idNdermarrje, idViti, idgjuha, idKonfig, komponenteEmri, rm, cultinf);
            //clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvKonfigurimeFTP", idKonfig, komponenteEmri);
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajKonfigurimFtp();
            }
        }

        private void ruajKonfigurimFtp()
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (!Page.IsValid)
            {
                hfStatusi.Value = "false";
                return;
            }
            try
            {
                DbCore.clsMesazh mesazh = kontrolloKonfigurim();
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }

                int idStatusDok = 1;

                DbCore.DbAdmin.clsKonfigurimFtp konfigurim = new DbCore.DbAdmin.clsKonfigurimFtp(0, txtKodi.Text, txtHostName.Text, txtUsername.Text, txtPassword.Text, int.Parse(txtPorta.Text), idNdermarrje, idPerdorues, cbEnableSsl.Checked, idStatusDok, cmbMetoda.SelectedIndex, cbEshteSFTP.Checked, txtFolderPath.Text.Replace("\\","/"));
                konfigurim.IdKonfigurimFtp = int.Parse(hfId.Value.ToString());
                if (hfShtimModifikim.Value == "shtim")
                    mesazh = konfigurim.Ruaj();
                else // modifikim                     
                    mesazh = konfigurim.Modifiko();
                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgRuajtjaEKonfigurimit"], pnlMesazhi);
                    hfStatusi.Value = "true";

                    if (hfShtimModifikim.Value == "shtim")
                        shtoKonfigurimFtpNeGrid(idNdermarrje, konfigurim.IdKonfigurimFtp);
                    else //modifikim
                        modifikoKonfigurimFtpNeGrid(idNdermarrje, konfigurim.IdKonfigurimFtp);
                }
                else
                {
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimNeRuajtjeKonfigurimi"], pnlMesazhi);
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            } catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimNeRuajtjeKonfigurimi"], pnlMesazhi);
            }
        }

        private DbCore.clsMesazh kontrolloKonfigurim()
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (txtKodi.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniKodin"]);
            if (hfShtimModifikim.Value == "shtim" && DbCore.DbAdmin.clsKonfigurimFtp.ekzistonKonfigurimiFtp(txtKodi.Text, idNdermarrje))
                return new clsMesazh(false, String.Format("Ekziston konfigurimi Ftp me kod {0}!", txtKodi.Text));
            if (txtHostName.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniHostName"]);
            if (txtPassword.Text != txtVerifikoPassword.Text)
                return new clsMesazh(false, MessagesResource.Messages["msgFjalekalimJoInjejteMeVerfikimin"]);
            if (txtUsername.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniUsername"]);
            int porta;
            if (!(int.TryParse(txtPorta.Text, out porta)))
                return new clsMesazh(false, MessagesResource.Messages["msgPorteNumber"]);
            if (cbEshteSFTP.Checked && txtFolderPath.Text == "")
                return new clsMesazh(false, "Per konfigurimet SFTP duhet te percaktoni Folder Path.");

            return new clsMesazh(true);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "KonfigurimeFtp.aspx", this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        private void emraTeLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            lblPassword.Text = MessagesResource.Messages["labelEmailFjalekalimi"] + ":";
            lblVerifikoPassword.Text = MessagesResource.Messages["labelVerifikoPassword"] + ":";
            lblPorta.Text = MessagesResource.Messages["lblPorta"] + ":";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (ASPxPageControl1.ActiveTabIndex == 0)
            {
                rreshtat = gvKonfigurimeFTP.GetSelectedFieldValues("IdKonfigurimFtp");
            }
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhniNjeKonfigurim"], pnlMesazhi);
                return;
            }
            var mesazh = new DbCore.clsMesazh();

            var TeFshire = new List<string>();
            var TePaFshire = new List<string>();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsKonfigurimFtp clsKonfigurimFtp = new DbCore.DbAdmin.clsKonfigurimFtp(Convert.ToInt32(id), idNdermarrje);

                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.Value.ToString()));

                bool lidhur = DbCore.DbAdmin.clsKonfigurimFtp.eshteKonfigurimiFtpILidhur(clsKonfigurimFtp.IdKonfigurimFtp.ToString(), konf.IdNivel.ToString());
                
                if (lidhur)
                {
                    TePaFshire.Add(clsKonfigurimFtp.Kodi);
                    continue;
                }

                mesazh = clsKonfigurimFtp.Fshi();
                if (clsKonfigurimFtp.IdKonfigurimFtp == 0)
                    continue;
                if (mesazh.Status)
                {
                    hiqKonfigurimFtpNgaGrida(clsKonfigurimFtp.IdKonfigurimFtp);
                    TeFshire.Add(clsKonfigurimFtp.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;

            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", MessagesResource.Messages["labelRaportKonfigurimiMeKod"] + " ", String.Join(";", TePaFshire), MessagesResource.Messages["msgShtoArtikullSuffixNjejesGabimi"]);

            else if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", MessagesResource.Messages["labelRaportKonfigurimetMeKode"] + " ", String.Join(";", TePaFshire), MessagesResource.Messages["msgShtoArtikullSuffixShumesGabimi"]);

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["labelRaportKonfigurimiMeKod"] + " ", String.Join(";", TeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixNjejesSuksesi"]);

            else if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["labelRaportKonfigurimetMeKode"] + " ", String.Join(";", TeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixShumesSuksesi"]);
          
            if (mesazhInfoGabim != string.Empty && mesazhInfoSukses != string.Empty)
                mesazhInfoGabim += MessagesResource.Messages["msgLidhesMesazhi"] + mesazhInfoSukses;
            if (mesazhInfoGabim != string.Empty)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        private void hiqKonfigurimFtpNgaGrida(int idKonfigFtp)
        {
            if (this.gvKonfigurimeFTP.DataSource != null)
            {
                DataTable dt = (DataTable)gvKonfigurimeFTP.DataSource;
                DataRow[] drs = dt.Select("IdKonfigurimFtp = " + idKonfigFtp);
                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["labelRaportMesazhGabimiDyKonfigurimeNeGride"]);
                if (drs.Length == 0)
                    return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvKonfigurimeFTP.DataBind();
            }
            else
                mbushListeKonfigurimeshFtp(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        private void mbushListeKonfigurimeshFtp(int idNdermarrje)
        {
            var dt = DbCore.DbAdmin.clsKonfigurimFtp.merrKonfigurimeFtpSipasNdermDhePerdorues(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKonfigurimeFTP.DataSource = dt;
            gvKonfigurimeFTP.DataBind();
            dt.Dispose();
        }

        protected void gvKonfigurimeFTP_DataBound(object sender, EventArgs e)
        {
            if (gvKonfigurimeFTP.Columns["#"] == null)
            {
                var check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.SetColVisibleIndex(0);
                check.Width = Unit.Percentage(2);

                gvKonfigurimeFTP.Settings.ShowFilterRow = true;
                gvKonfigurimeFTP.Columns.Add(check);
                gvKonfigurimeFTP.KeyFieldName = "IdKonfigurimFtp";
                gvKonfigurimeFTP.SettingsBehavior.AllowSelectByRowClick = true;
                gvKonfigurimeFTP.SettingsBehavior.AllowFocusedRow = true;
                gvKonfigurimeFTP.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKonfigurimeFTP.Settings.ShowFilterRowMenu = true;
            }
        }

        protected void gvKonfigurimeFTP_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (e.CallbackName == "COLUMNMOVE" && gvKonfigurimeFTP.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                gvKonfigurimeFTP.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            }
            //mbushListeKonfigurimeshFtp(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            GridUtil.ToolTipButonaveMbiGride(gvKonfigurimeFTP, cultinf, rm);
        }

        protected void gvKonfigurimeFTP_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdQyteti")
            {
                try
                {
                    var vlera = Converter.ConvertToInt(e.Value);
                    if (vlera == -3 || vlera == 0)
                    {
                        e.Criteria = null;
                    }
                } catch (Exception err)
                {
                    ImbLogger.Error(err.Message);
                    e.Criteria = null;
                }
            }
            else
            {
                if (e.Column.FieldName == "IdLlogari")
                {
                    try
                    {
                        var vlera = Converter.ConvertToInt(e.Value);
                        if (vlera == 0)
                        {
                            e.Criteria = null;
                        }
                    } catch (Exception err)
                    {
                        ImbLogger.Error(err.Message);
                        e.Criteria = null;
                    }
                }
            }
        }

        /// <summary>
        /// Metoda qe thirret kur grida ben callback
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Argumentat</param>
        protected void gvKonfigurimeFTP_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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

            gvKonfigurimeFTP.Selection.UnselectAll();
        }

        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelBlerjeShitjeTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["MenuItemKonfigurimFtp"];
        }

        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi)
        {
            ApplicationUtils.ASPxControlUtils.KonfigurimComboGride.shtoMetodeKonfigurimiFTP(gvKonfigurimeFTP);
            GridUtil.konfigGrideListeEMadhePaTheme(gvKonfigurimeFTP, "IdKonfigurimFtp");
            gvKonfigurimeFTP.Columns["#"].Width = 30;
            gvKonfigurimeFTP.Columns["#"].VisibleIndex = 0;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.mbushComboMetodaFtp(cmbMetoda);

            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 165, rm, cultinf, idGjuha);

            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonfillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        private void mbushListeKonfigurimeshFtpNgaSessioni()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushListeKonfigurimeshFtp((int)hfState["idNdermarrje"]);
            else
            {
                gvKonfigurimeFTP.DataSource = tmpObject;
                gvKonfigurimeFTP.DataBind();
                tmpObject.Dispose();
            }
        }

        private void shtoKonfigurimFtpNeGrid(int idNdermarrje, int idKonfigurimFtp)
        {
            if (gvKonfigurimeFTP.DataSource != null)
            {
                DataTable dt = (DataTable)gvKonfigurimeFTP.DataSource;
                DataRow[] drs = dt.Select("IdKonfigurimFtp = " + idKonfigurimFtp);
                if (drs.Length > 0)
                    throw new Exception(MessagesResource.Messages["labelRaportMesazhKonfiguimiEkziston"]);
                DataRow newArtDr = DbCore.DbAdmin.clsKonfigurimFtp.merrKonfigurimFtpSipasIdDR(idKonfigurimFtp);
                dt.ImportRow(newArtDr);
            }
            else
                mbushListeKonfigurimeshFtp(idNdermarrje);

            konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
        }
        private void modifikoKonfigurimFtpNeGrid(int idNdermarrje, int idKonfigurimFtp)
        {
            if (gvKonfigurimeFTP.DataSource != null)
            {
                DataTable dt = (DataTable)gvKonfigurimeFTP.DataSource;
                DataRow[] drs = dt.Select("IdKonfigurimFtp = " + idKonfigurimFtp);
                if (drs.Length > 1)
                    throw new DbCore.MyException(MessagesResource.Messages["labelRaportMesazhGabimiDyKonfigurimeNeGride"]);
                if (drs.Length == 0)
                    return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.clsKonfigurimFtp.merrKonfigurimFtpSipasIdDR(idKonfigurimFtp);
                //dt.Rows.Remove(dr);
                //dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else
                mbushListeKonfigurimeshFtp(idNdermarrje);
        }

    }

    //namespace EncryptStringSample

}