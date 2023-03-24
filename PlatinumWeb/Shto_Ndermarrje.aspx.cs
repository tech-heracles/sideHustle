using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Web.Script.Serialization;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore;
using DbCore.IMBUtils.Extensions;
using DevExpress.Xpo.Logger;
using DbCore.IMBUtils.Messages;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DevExpress.Web.ASPxSpreadsheet.Internal.Forms;

namespace PlatinumWeb
{
    public partial class Shto_Ndermarrje : MyPageBase
    {
        const string UploadDirectory = "~/images/";
        private string ThumbnailFileName;
        private int idgjuha, idviti, idPerdoruesi, idNdermarrje;

        protected void Page_Init(object sender, EventArgs e)
        {
            //mbushListeNdermarrjesh();
            //konfiguroGride();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            LoadString(hfMsgShtoNdermarrje, rm, cultinf);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (!Page.IsPostBack)
            {
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);

                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idgjuha);
                mbushGridNdermarjeshNgaDB(idPerdoruesi, idNdermarrje);
                mbushHiddenFieldMePerkthime(cultinf, rm);

                ThumbnailFileName = "Logo" + new Random().Next() + ".bmp";
                DbCore.mySessionObjects.ruajThumbnailFileNameNeSesion(Session, ThumbnailFileName);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Ndermarrje.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 129);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Ndermarrjet", ASPxGridView_Ndermarrjet, cmbKonfigurimi.Text.Split(';')[0], 129.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                ThumbnailFileName = DbCore.mySessionObjects.merrThumbnailFileNameNgaSesioni(Session);
                mbushGridNdermarjeshNgaSession(idPerdoruesi, idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 129);
                ConfigureAspxComboBox.mbushComboLicencat(cmbLicenca,false);
            }
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Ndermarrjet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Ndermarrje.aspx");
            DbCore.mySessionObjects.ruajNdermarjeselectSession(Session, hfId.Value.ToString());
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Ndermarrjet, "IdNdermarrje");
            mbushLlojLicence();
            GridUtil.EmrateButonaveMbiGride(ASPxGridView_Ndermarrjet);
        }
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgPlotesoniLimitinShitjesKupon", rm.GetString("msgPlotesoniLimitinShitjesKupon", cultinf));
            hfState.Set("msgLimitiShitjesKuponNegative", rm.GetString("msgLimitiShitjesKuponNegative", cultinf));
            hfState.Set("msgLimitiShitjesKuponNumer", rm.GetString("msgLimitiShitjesKuponNumer", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("labelAdministrimiKontakt", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("labelAdministrimiNderrmarjeModel", cultinf);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("labelAdministrimiLogoNdermarrje", cultinf);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            bool superuser = false;
            //DbCore.DbAdmin.colRolPerdorues rp = new DbCore.DbAdmin.colRolPerdorues();

            // rp.mbushRolePerdoruesSipasPerdoruesi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            int roli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RSU");
            if (roli == -1)
            {
                superuser = true;
                hfSuperUser.Value = true.ToString();
            }
            //foreach (DbCore.DbAdmin.clsRolPerdorues r in rp)
            //    if (r.IdRoli == -1)
            //    {
            //        superuser = true;
            //        hfSuperUser.Value = true.ToString();
            //        break;
            //    }
            if (superuser)
            {
                clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_Ndermarrje.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false);
            }
            else
                clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_Ndermarrje.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("NdryshoLlojLicence").ClientVisible = false;
            aSPxMenu1.Items.FindByName("HiqLogo").ClientVisible = false;
            if (ASPxPageControl1.ActiveTabIndex == 1 && superuser)
                aSPxMenu1.Items.FindByName("NdryshoLlojLicence").ClientVisible = true;
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = DbCore.mySessionObjects.merrNdermarjeReNgaSesioni(Session);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = (cmbMeme.SelectedIndex != -1) ? shtoNdermarje(cmbMeme.Text, ndermarrje) : shtoNdermarje(cmbNdermarjet.Text, ndermarrje);
            if (mesazh.Status == true)
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                int admin = -1;
                //DbCore.DbAdmin.colRolPerdorues rp = new DbCore.DbAdmin.colRolPerdorues();
                //DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarje(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), -1);
                DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), -1);
                //rp.mbushRolePerdoruesSipasPerdoruesi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                int roli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RA");
                if (roli > 0) admin = roli;
                //foreach (DbCore.DbAdmin.clsRolPerdorues r in rp)
                //{
                //    DbCore.DbAdmin.clsRoli roli = new DbCore.DbAdmin.clsRoli(r.IdRoli);
                //    if (roli.KodRoli.Equals("RA"))
                //        admin = roli.IdRoli;
                //}
                if (admin != -1)
                {
                    DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), admin);
                }
                if (ndermarrje.IdPrindi != 0)
                {
                    DbCore.DbAdmin.clsRoli.hiqTeDrejtaBij(ndermarrje.IdNdermarrje);
                }
                pastroFusha();
                hfStatusi.Value = "true";
                shtoNdermarjeNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), ndermarrje.IdNdermarrje);
                shtoNdermarrjeNeCmbRaportues(ndermarrje.IdNdermarrje, ndermarrje.NdermarrjeKodi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        protected void ButtonOk2_Click2(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = DbCore.mySessionObjects.merrNdermarjeReNgaSesioni(Session);
            ndermarrje.IdLicenca = int.Parse(cmbLicenca.Value.ToString());
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = (cmbMeme.SelectedIndex != -1) ? shtoNdermarje(cmbMeme.Text, ndermarrje) : shtoNdermarje(cmbNdermarjet.Text, ndermarrje);
            if (mesazh.Status)
            {
                ndermarrje = new DbCore.DbAdmin.clsNdermarrje(ndermarrje.IdNdermarrje);
                DbCore.DbAdmin.clsRoli roli = new DbCore.DbAdmin.clsRoli();                
                 mesazh = roli.ruajRoleDefaultMeTeDrejtaRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), int.Parse(cmbLlojLicence.Value.ToString()),ndermarrje.IdLicenca);
            }
            if (mesazh.Status == true)
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                int admin = -1;                
                DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), -1);
                int roli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RA");
                if (roli > 0) admin = roli;
              
                if (admin != -1)
                {
                    DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), admin);
                }
                if (ndermarrje.IdPrindi != 0)
                {
                    DbCore.DbAdmin.clsRoli.hiqTeDrejtaBij(ndermarrje.IdNdermarrje);
                }
                pastroFusha();
                hfStatusi.Value = "true";
                shtoNdermarjeNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), ndermarrje.IdNdermarrje);
                shtoNdermarrjeNeCmbRaportues(ndermarrje.IdNdermarrje, ndermarrje.NdermarrjeKodi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        protected void ButtonOk3_Click2(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje(int.Parse(hfId.Value));
            DbCore.DbAdmin.clsRoli roli = new DbCore.DbAdmin.clsRoli();
            mesazh.Status = roli.ndryshoLlojLicence(ndermarrje.IdLicenca, int.Parse(cmbLlojLicence.Value.ToString()));

            if (mesazh.Status == true)
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci), pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            //DbCore.DbAdmin.clsNdermarrje ndermarrje = (DbCore.DbAdmin.clsNdermarrje)CacheLayer.GlobalCacheManager.MySessionCache["ndermarjere"];
            DbCore.DbAdmin.clsNdermarrje ndermarrje = DbCore.mySessionObjects.merrNdermarjeReNgaSesioni(Session);
            ndermarrje.IdLicenca = int.Parse(cmbLicenca.Value.ToString());
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = (cmbMeme.SelectedIndex != -1) ? shtoNdermarje(cmbMeme.Text, ndermarrje) : shtoNdermarje(cmbNdermarjet.Text, ndermarrje);
            
            if (mesazh.Status)
            {
                ndermarrje = new DbCore.DbAdmin.clsNdermarrje(ndermarrje.IdNdermarrje);
                DbCore.DbAdmin.clsRoli roli = new DbCore.DbAdmin.clsRoli();
                mesazh = roli.ruajRoleDefaultMeTeDrejtaRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), int.Parse(cmbLlojLicence.Value.ToString()), ndermarrje.IdNdermarrje);
            }
            if (mesazh.Status == true)
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                //Session.Add("Imazhi", null);
                DbCore.mySessionObjects.ruajImazhNeSesion(Session, null);
                DbCore.mySessionObjects.ruajpathneSession(Session, null);
                int admin = -1;
                //DbCore.DbAdmin.colRolPerdorues rp = new DbCore.DbAdmin.colRolPerdorues();
                //DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarje(ndermarrje.IdNdermarrje, ndermarrje.IdViti, idPerdoruesi, -1);
                DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, idPerdoruesi, -1);
                int roli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RA");
                if (roli > 0) admin = roli;
                //rp.mbushRolePerdoruesSipasPerdoruesi(idPerdoruesi);
                //foreach (DbCore.DbAdmin.clsRolPerdorues r in rp)
                //{

                //    DbCore.DbAdmin.clsRoli roli = new DbCore.DbAdmin.clsRoli(r.IdRoli);
                //    if (roli.KodRoli.Equals("RA"))
                //        admin = roli.IdRoli;
                //}
                if (admin != -1)
                {
                    //DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarje(ndermarrje.IdNdermarrje, ndermarrje.IdViti, idPerdoruesi, admin);
                    DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, idPerdoruesi, admin);
                }
                if (ndermarrje.IdPrindi != 0)
                {
                    DbCore.DbAdmin.clsRoli.hiqTeDrejtaBij(ndermarrje.IdNdermarrje);
                }
                pastroFusha();
                hfStatusi.Value = "true";
                shtoNdermarjeNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, ndermarrje.IdNdermarrje);
                shtoNdermarrjeNeCmbRaportues(ndermarrje.IdNdermarrje, ndermarrje.NdermarrjeKodi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        protected void ASPxGridView_Ndermarrjet_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_Ndermarrjet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_Ndermarrjet.Settings.ShowFilterRow = true;
                ASPxGridView_Ndermarrjet.Columns.Add(check);
                ASPxGridView_Ndermarrjet.KeyFieldName = "IdNdermarrje";
                ASPxGridView_Ndermarrjet.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Ndermarrjet.SettingsBehavior.AllowFocusedRow = true;
                ASPxGridView_Ndermarrjet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Ndermarrjet.Settings.ShowFilterRowMenu = true;
            } //ASPxGridView_Ndermarrjet.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_ListNdermarrjet", "Shto_Ndermarrje.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_ListNdermarrjet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Ndermarrje.aspx");
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idGjuha);
                hfStatusi.Value = "true";
                ASPxGridView_Ndermarrjet.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListNdermarrjet", "Shto_Ndermarrje.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Ndermarrjet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NdermarrjeKodi", ASPxGridView_Ndermarrjet);
            
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListNdermarrjet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Ndermarrje.aspx");
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Ndermarrjet.GetSelectedFieldValues("IdNdermarrje");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = ASPxGridView_Ndermarrjet.GetSelectedFieldValues("IdNdermarrje");
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiZgjidhniNdermarrje", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>();
            //List<string> TePaFshire = new List<string>();
            int idPeroduresILoguar = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrjeLoguar = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsNdermarrje tmpNdermarrje = new DbCore.DbAdmin.clsNdermarrje(Convert.ToInt32(id));
                if (Convert.ToInt32(id) == idNdermarrjeLoguar)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiNukMundTeFshiniNderm", ci), pnlMesazhi);
                    continue;
                }
                else
                    if (DbCore.DbAdmin.clsNdermarrje.eshtePrind(Convert.ToInt32(id)))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiNukMundTeFshiniPrind", ci), pnlMesazhi);
                        continue;
                    }
                    else mesazh = tmpNdermarrje.fshi(idPeroduresILoguar);
                if (tmpNdermarrje.IdNdermarrje == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNdermarjeNgaGrida(idPeroduresILoguar, idNdermarrjeLoguar, tmpNdermarrje.IdNdermarrje);
                    #endregion
                    TeFshire.Add(tmpNdermarrje.NdermarrjeKodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            //string mesazhInfoGabim = ""; 
            string mesazhInfoSukses = "";
            //if (TePaFshire.Count == 1)
            //    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimi);
            //else
            //    if (TePaFshire.Count > 1)
            //        mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimi);

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAdministrimiNdermarrjaKod", ci), String.Join(";", TeFshire), rm.GetString("msgAdministrimiFshiMeSukses", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAdministrimiNdermarrjetKod", ci), String.Join(";", TeFshire), rm.GetString("msgAdministrimiFshineMeSukses", ci));
            //if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
            //    mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            //if (mesazhInfoGabim != "")
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            //else
            if (mesazhInfoSukses != "")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi); //mbushListeNdermarrjesh();
        }

        protected void btnShfaqImazh_Click(object sender, EventArgs e)
        {
            //ASPxBinaryImage1.ContentBytes = ((Byte[])CacheLayer.GlobalCacheManager.MySessionCache["Imazhi"]);
            ASPxBinaryImage1.ContentBytes = DbCore.mySessionObjects.merrImazhNgaSesioni(Session);
            pnlImazh.Update();
            if (ASPxBinaryImage1.Width.Value > ASPxBinaryImage1.Height.Value)
            {
                ASPxBinaryImage1.Width = 200;
                ASPxBinaryImage1.Style.Add("height", "auto");
            }
            else
            {
                ASPxBinaryImage1.Height = 200;
                ASPxBinaryImage1.Style.Add("width", "auto");
            }
        }

        private void hiqNdermarjeNgaGrida(int idPerdoruesi, int idNdermarrjeLoguar, int idNdermarrje)
        {
            if (this.ASPxGridView_Ndermarrjet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Ndermarrjet.DataSource;
                DataRow[] drs = dt.Select("IdNdermarrje = " + idNdermarrje);
                if (drs.Length > 1)
                {
                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    throw new DbCore.MyException(rm.GetString("msgAdministrimiGabimDyNdermarrjeNjeId", ci));
                }
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Ndermarrjet.DataBind();
            }
            else
                mbushGridNdermarjeshNgaDB(idPerdoruesi, idNdermarrjeLoguar);
        }

        private void shtoNdermarjeNeGrid(int idNdermarrjeLoguar, int idPerdoruesi, int idndermarje)
        {
            if (ASPxGridView_Ndermarrjet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Ndermarrjet.DataSource;
                DataRow[] drs = dt.Select("IdNdermarrje = " + idndermarje);
                if (drs.Length > 0)
                {
                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    throw new DbCore.MyException(rm.GetString("msgAdministrimiGabimNdermarrjaEkziston", ci));
                }
                DataRow newArtDr = DbCore.DbAdmin.colNdermarrjet.merrSipasNdermarjeDR(idndermarje);
                dt.ImportRow(newArtDr);
                //cmbRaportuesi.Items.Add(
            }
            else mbushGridNdermarjeshNgaDB(idPerdoruesi, idNdermarrjeLoguar);
        }

        private void modifikoNdermarjeNeGrid(int idNdermarrje, int idPerdorues, int idndermarje)
        {
            if (ASPxGridView_Ndermarrjet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Ndermarrjet.DataSource;
                DataRow[] drs = dt.Select("IdNdermarrje = " + idndermarje);
                if (drs.Length > 1)
                {
                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    throw new DbCore.MyException(rm.GetString("msgAdministrimiGabimDyNdermarrjeNjeId", ci));
                }
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.colNdermarrjet.merrSipasNdermarjeDR(idndermarje);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNdermarjeshNgaDB(idPerdorues, idNdermarrje);
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
                ruajNdermarrje();
            }
            if (e.Item.Name == "HiqLogo")
            {
                //Session.Add("Imazhi", null);
                DbCore.mySessionObjects.ruajImazhNeSesion(Session, null);
                //ASPxBinaryImage1.ContentBytes = ((Byte[])CacheLayer.GlobalCacheManager.MySessionCache["Imazhi"]);
                ASPxBinaryImage1.ContentBytes = null; //DbCore.mySessionObjects.merrImazhNgaSesioni(Session);
                pnlImazh.Update();
            }
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            //inicializoObjekte();
            ConfigureAspxComboBox.mbushComboMonedhatNdermarje(monedha_ASPxComboBox);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.colQytetet colQyt = new DbCore.DbAdmin.colQytetet();
            colQyt.mbushGjitheQytetetPozitive(-1);
            //DbCore.DbAdmin.colQytetet colQyt = dbAdmin.merrGjitheQytetetPozitive(-1);
            mbushComboQytete();
            ConfigureAspxComboBox.mbushComboLicencat(cmbLicenca,true);
            mbushLlojLicence();
            ConfigureAspxComboBox.mbushComboVitet(txtViti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtViti);
            ConfigureAspxComboBox.mbushComboLlojiNdermarje(cmbLlojNdermarje);
            ConfigureAspxComboBox.mbushComboNdermarrje(idPerdoruesi, cmbNdermarjet, 0,true);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 29, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()),  idgjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtGrupi);
            ConfigureAspxComboBox.mbushComboGrupeNdermarrjesh(new DbCore.DbAdmin.clsNdermarrje(idNdermarrje).IdLicenca, txtGrupi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallbackPaButon(cmbMeme);
            //DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            int idLicenca = DbCore.DbAdmin.clsNdermarrje.ktheLicenceNdermarrje(idNdermarrje);
            DbCore.DbAdmin.colNdermarrjet ndermarrjetELicences = new DbCore.DbAdmin.colNdermarrjet();
            ndermarrjetELicences.merrNdermarrjet(idPerdoruesi, idLicenca);
            ConfigureAspxComboBox.mbushComboNdermarjeRaportues(ndermarrjetELicences, cmbRaportuesi);
            ConfigureAspxComboBox.mbushComboNdermarjeMeme(idLicenca, cmbMeme);
            cmbLlojLicence.SelectedIndex = 1;
            //cmbKonfigurimi.SelectedIndex = -1;
        }
        private void mbushComboQytete()
        {
            qyteti_ASPxComboBox.DataSource = null;
            qyteti_ASPxComboBox.TextField = "EmriQyteti";
            qyteti_ASPxComboBox.ValueField = "IdQyteti";
            qyteti_ASPxComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            qyteti_ASPxComboBox.DataBind();
        }

        private void mbushLlojLicence()
        {
            DbCore.DbAdmin.colLlojLicence lloj = new DbCore.DbAdmin.colLlojLicence();
            lloj.mbushGjitheLlojeLicence();

            cmbLlojLicence.DataSource = lloj;
            cmbLlojLicence.TextField = "Pershkrim";
            cmbLlojLicence.ValueField = "IdLlojLicence";
            cmbLlojLicence.DataBind();

        }


        

        private void mbushGridNdermarjeshNgaSession(int idPerdoruesiLoguar, int idNdermarrjeLoguar)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNdermarjeshNgaDB(idPerdoruesiLoguar, idNdermarrjeLoguar);
            else
            {
                ASPxGridView_Ndermarrjet.DataSource = tmpObject;
                ASPxGridView_Ndermarrjet.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridNdermarjeshNgaDB(int idPerdoruesiLoguar, int idNdermarrje)
        {//mbush griden e popupit me te dhena       
            DbCore.DbAdmin.clsNdermarrje n = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);

            DataTable dt = DbCore.DbAdmin.colNdermarrjet.merrNdermarjetDT(idPerdoruesiLoguar, n.IdLicenca);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Ndermarrjet.DataSource = dt;
            ASPxGridView_Ndermarrjet.DataBind();
            dt.Dispose();
        }

        //private void mbushListeNdermarrjesh(int idNdermarrje)
        //{
        //    dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsNdermarrje n = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
        //    DbCore.DbAdmin.colNdermarrjet colNdermarrjet = new DbCore.DbAdmin.colNdermarrjet();
        //    colNdermarrjet.mbushGjitheNdermarrjet(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), n.IdLicenca);
        //    ASPxGridView_Ndermarrjet.DataSource = colNdermarrjet;
        //    ASPxGridView_Ndermarrjet.DataBind();
        //}

        //private void inicializoObjekte()
        //    {
        //    //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    }

        private void konfiguroGride(int idNdermarrjeLoguar, string kodKonfigurimi, int idKomponente)
        {
             this.ASPxGridView_Ndermarrjet.Columns["#"].VisibleIndex = 0;
        }

        private void shtoVit()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            ASPxGridView_Ndermarrjet.Columns.Remove(ASPxGridView_Ndermarrjet.Columns["IdViti"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colVitet col = new DbCore.DbAdmin.colVitet();
            col.Add(new DbCore.DbAdmin.clsViti(0, "", new DateTime(), new DateTime(), "", true, true, 0, 0, 0, 0, new DateTime(), 0));
            col.merrGjitheVitetENdermarjes(-1);
            //col = dbAdmin.merrGjitheVitet();

            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.TextField = "KodiViti";
            colnew.PropertiesComboBox.ValueField = "IdViti";
            colnew.FieldName = "IdViti";
            ASPxGridView_Ndermarrjet.Columns.Add(colnew);
        }

        //sherben per ta bere ne forme combo-je shtyllen e monedhave
        private void shtoMonedhat()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();

            ASPxGridView_Ndermarrjet.Columns.Remove(ASPxGridView_Ndermarrjet.Columns["NdermarrjeMonedha"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            colMonedhat.Add(new DbCore.DbAdmin.clsMonedha(0, "", "", true, 0, 0, 0, 0, 0, new DbCore.DbAdmin.colLidhjetAutorizim(), 0));
            //clsFunksione funk = new DbCore.clsFunksione();
            colMonedhat.mbushGjitheMonedhatAktive(-1, 0);
            //colMonedhat = dbAdmin.merrGjitheMonedhatAktive(-1, 0);
            colnew.PropertiesComboBox.DataSource = colMonedhat;
            colnew.PropertiesComboBox.TextField = "PershkrimiMonedha";
            colnew.PropertiesComboBox.ValueField = "IdMonedha";
            colnew.FieldName = "NdermarrjeMonedha";
            ASPxGridView_Ndermarrjet.Columns.Add(colnew);
        }

        //sherben per ta bere ne forme combo-je shtyllen e qyteteve
        private void shtoQytetet()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            ASPxGridView_Ndermarrjet.Columns.Remove(ASPxGridView_Ndermarrjet.Columns["NdermarrjeQyteti"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colQytetet colQytetet = new DbCore.DbAdmin.colQytetet();
            colQytetet.Add(new DbCore.DbAdmin.clsQyteti(-1, "", "", 0, 0, 0));
            colQytetet.mbushGjitheQytetetPozitive(-1);
            //colQytetet .AddRange (dbAdmin.merrGjitheQytetetPozitive(-1));
            colnew.PropertiesComboBox.DataSource = colQytetet;
            colnew.PropertiesComboBox.TextField = "EmriQyteti";
            colnew.PropertiesComboBox.ValueField = "IdQyteti";
            colnew.FieldName = "NdermarrjeQyteti";
            ASPxGridView_Ndermarrjet.Columns.Add(colnew);
        }

        private void pastroFusha()
        {
            txtViti.Text = "";
            kodi_TextBox.Text = "";
            pershkrimi_TextBox.Text = "";
            vendi_TextBox.Text = "";
            nipt_TextBox.Text = "";
            txtNrTvsh.Text = "";
            email_TextBox.Text = "";
            licenca_TextBox.Text = "";
            kodi_fiskal_TextBox.Text = "";
            tel_TextBox.Text = "";
            fax_TextBox.Text = "";
            monedha_ASPxComboBox.SelectedIndex = -1;
            qyteti_ASPxComboBox.SelectedIndex = -1;
            ASPxGridView_Ndermarrjet.CancelEdit();
            ASPxGridView_Ndermarrjet.AddNewRow();
            HiddenField1.Value = "";
            llog_check.Checked = false;
            grup_llog_check.Checked = false;
            kpf_check.Checked = false;
            monedha_check.Checked = false;
            rap_fin_check.Checked = false;
            qytete_check.Checked = false;
            skema_check.Checked = false;
            nivel_cmimi_check.Checked = false;
            nivel_zbritje_check.Checked = false;
            nivelTVSH_check.Checked = false;
            maturimi_check.Checked = false;
            men_pageses_check.Checked = false;
            men_trans_check.Checked = false;
            kategori_zbritje_check.Checked = false;
            kushte_drg_check.Checked = false;
            cbfiskalizimi.Checked = false;
            txtkodbiznesi.Text="";
            cbMeTvsh.Checked = false;
        }

        //sherben per te ruajtur nje ndermarrje
        private void ruajNdermarrje()
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            if (Page.IsValid == false)
                return;
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            bool superuser = false;
            int admin = -1;
            //DbCore.DbAdmin.colRolPerdorues rp = new DbCore.DbAdmin.colRolPerdorues();
            int role_rsu = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RSU");
            int role_ra = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RA");
            if (role_rsu == -1) superuser = true;
            else if (role_ra > 0) admin = role_ra;

            DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (vit.mbushVitetMet(txtViti.Text, -1) == false)
            {
                hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiVitiSekziston", ci), pnlMesazhi);
                //  mbushListeNdermarrjesh();
                return;
            }            
            int idNdermarrjeLoguar = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsNdermarrje ndermarrjeLoguar = new DbCore.DbAdmin.clsNdermarrje(idNdermarrjeLoguar);
            DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje();
            
            try
            {
                ndermarrje = krijoNdermarrje(ndermarrjeLoguar.IdLicenca, idPerdoruesi);
            }
            catch (DbCore.MyException myEx)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(myEx.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myEx.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Ndermarrje.aspx");
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiNukKeniTeDrejteVeprimi", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                
                DbCore.DbAdmin.clsLicenca licenca = new DbCore.DbAdmin.clsLicenca(ndermarrjeLoguar.IdLicenca);
                
                DataTable dt = DbCore.DbAdmin.colNdermarrjet.merrNdermarjetDT(idPerdoruesi, licenca.IdLicenca);
                if (licenca.NrNdermarjesh == dt.Rows.Count)
                {
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiKaluarNrNdermarrjeve", ci), pnlMesazhi);
                    return;
                }
                if (superuser)
                {
                    DbCore.mySessionObjects.ruajNdermarjeReNeSesion(Session, ndermarrje);
                    clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("msgAdministrimiRoliDefaultPerNdermarrjen", ci), pnlMesazhi, idgjuha, false);
                    hfStatusi.Value = "false";
                    return;
                }
                vazhdoRuajtjeNdermarrjeje(ndermarrje, rm, ci, idNdermarrjeLoguar);
                return;
            }
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiNukKeniTeDrejteVeprimi", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            ndermarrje.IdNdermarrje = int.Parse(hfId.Value.ToString());
            DbCore.DbAdmin.clsNdermarrje ndermeksistuese = new DbCore.DbAdmin.clsNdermarrje(ndermarrje.IdNdermarrje);
            ndermarrje.IdTakse = ndermeksistuese.IdTakse;
            ndermarrje.Pathname = ndermeksistuese.Pathname;
            mesazh = ndermarrje.modifiko();
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
                ASPxPageControl1.ActiveTabIndex = 0;
                return;
            }
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci), pnlMesazhi);
            DbCore.mySessionObjects.ruajImazhNeSesion(Session, null);
            DbCore.mySessionObjects.ruajpathneSession(Session, null);
            DbCore.mySessionObjects.ruajNdermRaportuese(Session, cmbRaportuesi.Text != String.Empty ? Convert.ToInt32(cmbRaportuesi.Value) : 0);
            pastroFusha();
            hfStatusi.Value = "true";
            modifikoNdermarjeNeGrid(idNdermarrjeLoguar, idPerdoruesi, ndermarrje.IdNdermarrje);
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        protected void vazhdoRuajtjeNdermarrjeje(DbCore.DbAdmin.clsNdermarrje ndermarrje, ResourceManager rm, CultureInfo ci, int idNdermarrjeLoguar)
        {
            ndermarrje.IdTakse = 0;
            DbCore.clsMesazh mesazh = (cmbMeme.SelectedIndex != -1) ? shtoNdermarje(cmbMeme.Text, ndermarrje) : shtoNdermarje(cmbNdermarjet.Text, ndermarrje);
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
                ASPxPageControl1.ActiveTabIndex = 0;
                return;
            }
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiSukses", ci), pnlMesazhi);
            DbCore.mySessionObjects.ruajImazhNeSesion(Session, null);
            DbCore.mySessionObjects.ruajpathneSession(Session, null);
            pastroFusha();
            hfStatusi.Value = "true";

            int admin = -1;
            DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), -1);
            int roli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "RA");
            if (roli > 0)
                admin = roli;
           
            shtoNdermarjeNeGrid(idNdermarrjeLoguar, idPerdoruesi, ndermarrje.IdNdermarrje);
            shtoNdermarrjeNeCmbRaportues(ndermarrje.IdNdermarrje, ndermarrje.NdermarrjeKodi);
            if (admin != -1)
            {
                DbCore.DbAdmin.clsRoli.ruajRolAdminNdermarjeDheRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, idPerdoruesi, admin);
            }
            if (ndermarrje.IdPrindi != 0)
            {
                DbCore.DbAdmin.clsRoli.hiqTeDrejtaBij(ndermarrje.IdNdermarrje);
            }
            ASPxPageControl1.ActiveTabIndex = 0;
            return;
        }

        public DbCore.clsMesazh shtoNdermarje(String kodNdermarrje, DbCore.DbAdmin.clsNdermarrje ndermarrje)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            string konfigurimiDefault = "";
            if (llog_check.Checked)
            {
                konfigurimiDefault += "T_GRUPILLOGARIA" + ":true;";
                konfigurimiDefault += "T_NENGRUPILLOGARIA" + ":true;";
                konfigurimiDefault += "T_KPF" + ":true;";
                konfigurimiDefault += "T_MONEDHA" + ":true;";
                konfigurimiDefault += "T_LLOGARI" + ":true;";
                konfigurimiDefault += "T_BUXHETI" + ":true;";
                konfigurimiDefault += "T_EDUKIMI" + ":true;";
                konfigurimiDefault += "T_VITET:true;";
                konfigurimiDefault += "T_MONEDHAUPD" + ":" + monedha_check.Checked + ";";
                konfigurimiDefault += "T_NJESIARTIKULLI" + ":" + njesi_check.Checked + ";";
                konfigurimiDefault += "T_PASQYRAFINANCIAREKOKA" + ":" + rap_fin_check.Checked + ";";
                konfigurimiDefault += "T_KOKASKEMAKONTAB" + ":" + skema_check.Checked + ";";
                konfigurimiDefault += "T_MATURIMI" + ":" + maturimi_check.Checked + ";";
                konfigurimiDefault += "T_MENYRATRANSPORTI" + ":" + men_trans_check.Checked + ";";
                konfigurimiDefault += "T_KUSHTEDERGIMI" + ":" + kushte_drg_check.Checked + ";";
                konfigurimiDefault += "T_NIVELCMIMI" + ":" + nivel_cmimi_check.Checked + ";";
                konfigurimiDefault += "T_KOKAKATEGORIZBRITJE" + ":" + kategori_zbritje_check.Checked + ";";
                konfigurimiDefault += "T_BUXHETILLOG" + ":true;";
                konfigurimiDefault += "T_QYTETI" + ":" + qytete_check.Checked + ";";
                konfigurimiDefault += "T_TAKSAT" + ":" + nivelTVSH_check.Checked + ";";
                konfigurimiDefault += "T_NIVELZBRITJE" + ":" + nivel_zbritje_check.Checked + ";";
                konfigurimiDefault += "T_TAKSATUPD" + ":" + nivelTVSH_check.Checked;
            }
            else
            {
                konfigurimiDefault += "T_LLOGARI" + ":" + llog_check.Checked + ";";
                konfigurimiDefault += "T_GRUPILLOGARIA" + ":" + grup_llog_check.Checked + ";";
                konfigurimiDefault += "T_NENGRUPILLOGARIA" + ":" + grup_llog_check.Checked + ";";
                konfigurimiDefault += "T_VITET:true;";
                konfigurimiDefault += "T_KPF" + ":" + kpf_check.Checked + ";";
                konfigurimiDefault += "T_MONEDHA" + ":" + monedha_check.Checked + ";";
                konfigurimiDefault += "T_NJESIARTIKULLI" + ":" + njesi_check.Checked + ";";
                konfigurimiDefault += "T_PASQYRAFINANCIAREKOKA" + ":" + rap_fin_check.Checked + ";";
                konfigurimiDefault += "T_KOKASKEMAKONTAB" + ":" + skema_check.Checked + ";";
                konfigurimiDefault += "T_MATURIMI" + ":" + maturimi_check.Checked + ";";
                konfigurimiDefault += "T_MENYRATRANSPORTI" + ":" + men_trans_check.Checked + ";";
                konfigurimiDefault += "T_KUSHTEDERGIMI" + ":" + kushte_drg_check.Checked + ";";
                konfigurimiDefault += "T_NIVELCMIMI" + ":" + nivel_cmimi_check.Checked + ";";
                konfigurimiDefault += "T_KOKAKATEGORIZBRITJE" + ":" + kategori_zbritje_check.Checked + ";";
                konfigurimiDefault += "T_BUXHETILLOG" + ":" + kpf_check.Checked + ";";
                konfigurimiDefault += "T_EDUKIMI" + ":true;";
                konfigurimiDefault += "T_QYTETI" + ":" + qytete_check.Checked + ";";
                konfigurimiDefault += "T_TAKSAT" + ":" + nivelTVSH_check.Checked + ";";
                konfigurimiDefault += "T_NIVELZBRITJE" + ":" + nivel_zbritje_check.Checked + ";";
                konfigurimiDefault += "T_TAKSATUPD" + ":" + nivelTVSH_check.Checked;
            }
            DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(kodNdermarrje);
            int idnderm = ndermarrja.IdNdermarrje;
            ndermarrje.Lloji = ndermarrja.Lloji;
            ndermarrje.Pathname = ndermarrja.Pathname;
            ndermarrja.Dispose();
            mesazh = ndermarrje.ruaj(konfigurimiDefault, idnderm);
            return mesazh;
        }

        public DbCore.DbAdmin.clsNdermarrje krijoNdermarrje(int idLicenca, int idPerdoruesi, string email, string fax, string kodiNderm, string kodiFiskal, string licenca, int monedha, string nipt, double limitShitjes, string pershkrimi, int idstatusDok, string emerFurnitori, string kodFurnitori, string mrLlogariFurnitori, string nrTvsh, int raportuesi, int idQyteti, string grupi, bool eshtePrind, bool ownShop, bool logNderm, string meme, string tel, string vendi, int idViti, byte[] imazh, int llojNdermarrje, int nivelstrukture,bool fiskalizim,string kodbiznesi,string pathname,bool metvsh)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje();
            ndermarrje.IdLicenca = idLicenca;
            ndermarrje.NdermarrjeEMail = email;
            ndermarrje.NdermarrjeFax = fax;
            ndermarrje.NdermarrjeKodi = kodiNderm;
            ndermarrje.NdermarrjeKodiFiskal = kodiFiskal;
            ndermarrje.NdermarrjeLicenca = licenca;
            ndermarrje.NdermarrjeMonedha = monedha;
            ndermarrje.NdermarrjeNipt = nipt;
            ndermarrje.LimitiShitjes = limitShitjes;
            ndermarrje.NdermarrjePershkrimi = pershkrimi;
            ndermarrje.IdStatusDok = idstatusDok;
            ndermarrje.EmerFurnitori = emerFurnitori;
            ndermarrje.KodFurnitori = kodFurnitori;
            ndermarrje.NrLlogariFurnitori = mrLlogariFurnitori;
            ndermarrje.NrTvsh = nrTvsh;
            ndermarrje.Raportuesi = raportuesi;
            ndermarrje.Nivelstrukture = nivelstrukture;
            ndermarrje.NdermarrjeQyteti = idQyteti;
            int idgrupB;
            if (grupi != "")
                if (new DbCore.DbAdmin.clsGrupNdermarrje(grupi, idLicenca) != null)
                    idgrupB = new DbCore.DbAdmin.clsGrupNdermarrje(grupi, idLicenca).Id;
                else
                    idgrupB = 0;
            else
                idgrupB = 0;
            ndermarrje.IdGrupi = idgrupB;
            ndermarrje.Prind = eshtePrind;
            ndermarrje.OwnShop = ownShop;
            ndermarrje.LogNdermarrje = logNderm;
            ndermarrje.Fiskalizimi = fiskalizim;
            ndermarrje.Kodbiznesi = kodbiznesi;
            ndermarrje.Pathname = pathname==null? new DbCore.DbAdmin.clsNdermarrje(IdNdermarrja).Pathname : pathname;
            ndermarrje.MeTvsh = metvsh;
            DbCore.mySessionObjects.ruajRuajLogNeSesion(Session, ndermarrje.LogNdermarrje);
            int prind;
            DbCore.DbAdmin.clsNdermarrje nderprindi = new DbCore.DbAdmin.clsNdermarrje(meme);
            if (meme != "")
                if (nderprindi.IdNdermarrje != 0)
                {
                    if (nderprindi.IdLicenca != idLicenca)
                    {
                        throw new DbCore.MyException("Ndermarrja meme nuk ekziston!");
                    }
                    prind = nderprindi.IdNdermarrje;
                }
                else
                {
                    prind = 0;
                    throw new DbCore.MyException("Ndermarrja meme nuk ekziston!");
                }
            else
                prind = 0;
            ndermarrje.IdPrindi = prind;
            ndermarrje.NdermarrjeTel = tel;
            ndermarrje.NdermarrjeVendi = vendi;
            ndermarrje.IdPerdoruesi = idPerdoruesi;
            ndermarrje.IdViti = idViti;
            ndermarrje.NdermarrjeLogo = imazh;
            ndermarrje.LlojNdermarje = llojNdermarrje;
            ndermarrje.Fiskalizimi = fiskalizim;
            return ndermarrje;
        }

        //krijon nje objekt te tipit ndermarrje duke marre vlerat nga text boxet
        private DbCore.DbAdmin.clsNdermarrje krijoNdermarrje(int idLicenca, int idPerdoruesi)
        {   int idnder=Convert.ToInt32(cmbNdermarjet.Value);
            if (hfShtimModifikim.Value == "modifikim" && kodi_TextBox.Text != "" )
            {
                DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(kodi_TextBox.Text);
                idnder = nd.IdNdermarrje;
            }
            int idQyteti = -1;
            double limiti;
            if (!(qyteti_ASPxComboBox.SelectedItem == null || qyteti_ASPxComboBox.SelectedItem.Value.ToString() == ""))
            {
                idQyteti = Convert.ToInt32(qyteti_ASPxComboBox.SelectedItem.Value);
            }
            else
            {
                DbCore.DbAdmin.clsQyteti qyteti = new DbCore.DbAdmin.clsQyteti(qyteti_ASPxComboBox.Text,idnder);
                idQyteti = qyteti.IdQyteti;
            }
            int idViti;
            if (hfShtimModifikim.Value == "modifikim" && !(txtViti.SelectedItem == null || txtViti.SelectedItem.Value.ToString() == ""))
                idViti = Convert.ToInt32(txtViti.SelectedItem.Value);
            else
            {
                DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
                vit.mbushVitetMet(txtViti.Text, idnder);
                idViti = vit.IdViti;
            }
            int idmonedha;
            if (hfShtimModifikim.Value == "modifikim" && !(monedha_ASPxComboBox.SelectedItem == null || monedha_ASPxComboBox.SelectedItem.Value.ToString() == ""))
                idmonedha = Convert.ToInt32(monedha_ASPxComboBox.SelectedItem.Value);
            else
            {
                DbCore.DbAdmin.clsMonedha  mon = new DbCore.DbAdmin.clsMonedha();
                mon.mbushMonedhePershk(monedha_ASPxComboBox.Text, idnder);
                idmonedha = mon.IdMonedha;
            }
            int raportuesi = 0;
            int nivelstrukture = 1; // kur ndermarrja nuk ka ndermarrje raportuese (nuk ka prind) 

            if (cmbRaportuesi.SelectedItem != null)
            {
                raportuesi = int.Parse(cmbRaportuesi.SelectedItem.Value.ToString());
                bool jocikel = kontrolloRaportues(int.Parse(hfId.Value.ToString()), raportuesi);
                if (!jocikel)
                    throw new DbCore.MyException("Ndermarrja e raportimit nuk eshte e sakte! Ju lutemi, zgjidhni nje ndermarrje tjeter.");
                if (raportuesi == 0)
                    nivelstrukture = 1; 
                else
                    nivelstrukture = DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrjeRaportimi(raportuesi) == 0 ? 2 : 3; // kur prindi ka prind, eshte niveli 3, kur s'ka eshte 2 
            }
           
            DbCore.DbAdmin.clsNdermarrje ndermarrje = krijoNdermarrje(idLicenca, idPerdoruesi, email_TextBox.Text, fax_TextBox.Text, kodi_TextBox.Text, kodi_fiskal_TextBox.Text, licenca_TextBox.Text,idmonedha, nipt_TextBox.Text, Convert.ToDouble(txtLimitiShitjes.Text), pershkrimi_TextBox.Text, 1, "", "", "", txtNrTvsh.Text, raportuesi, idQyteti, txtGrupi.Text, cbPrind.Checked, cbOwnShop.Checked, cbLogu.Checked, cmbMeme.Text, tel_TextBox.Text, vendi_TextBox.Text, idViti, DbCore.mySessionObjects.merrImazhNgaSesioni(Session), 1, nivelstrukture,cbfiskalizimi.Checked,txtkodbiznesi.Text, DbCore.mySessionObjects.merrPathNgaSesioni(Session),cbMeTvsh.Checked);
            return ndermarrje;
        }

        /// <summary>
        /// Funksion rekursiv qe kthen ndermarrjen raportuese te nje ndermarrje. Qellimi eshte qe te mos behen cikle tek ndermarrjet raportuese.
        /// </summary>
        /// <param name="idNdermarrje">Id e ndermarrjes qe po i caktojme raportuesin</param>
        /// <param name="idRaportues">Id e ndermarrjes raportues. Ne cdo cikel zevendesohet raportuesi, me raportuesin e vet.</param>
        /// <returns>true nqs nuk formohen cikle, false nqs krijohen cikle dhe nuk lejohet te caktohet kjo si ndermarrje raportimi.</returns>
        protected bool kontrolloRaportues(int idNdermarrje, int idRaportues)
        {
            if (hfShtimModifikim.Value == "shtim")
                return true;

            if (idRaportues > 0)
            {
                if (idNdermarrje != idRaportues)
                {
                    idRaportues = DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrjeRaportimi(idRaportues);
                    return kontrolloRaportues(idNdermarrje, idRaportues);
                }
                else return false;
            }
            else if (idNdermarrje == idRaportues)
                return false;
            else
                return true;
        }

        protected void ASPxGridView_Ndermarrjet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Ndermarrjet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Ndermarrjet.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.EmrateButonaveMbiGride(ASPxGridView_Ndermarrjet);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        protected void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (e.Parameter != "null")
            {
                int lloji = int.Parse(e.Parameter.ToString());
                ConfigureAspxComboBox.mbushComboNdermarrje(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), cmbNdermarjet, lloji,true);
            }
        }

        protected void ASPxGridView_Ndermarrjet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_Ndermarrjet.FilterExpression = "";
                else
                {
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraEmri(arr[2], idNdermarrje);
                    int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_ListNdermarrjet", "Shto_Ndermarrje.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Ndermarrjet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Ndermarrjet);
                        int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                        CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                        konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idGjuha);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
                idkomponente = e.Parameters;
            //konfiguroGride();
            // funksion.percaktoVisibleColumnsGridSipasKodKonfigurimi(ASPxGridView_Ndermarrjet, kodkonfigurimi, idkomponente);
            ASPxGridView_Ndermarrjet.Selection.UnselectAll();
        }

        protected void ASPxGridView_Ndermarrjet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Ndermarrjet.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Ndermarrjet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Ndermarrjet.VisibleRowCount;
        }

        protected void ASPxGridView_Ndermarrjet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "EmerLlogari1" || e.Column.FieldName == "NdermarrjePershkrimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }

        }

        protected void ASPxGridView_Ndermarrjet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NdermarrjeMonedha" || e.Column.FieldName == "NdermarrjeQyteti" || e.Column.FieldName == "IdViti")
            {
                if (Converter.ConvertToInt(e.Value)==0 || Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        string RuajFilePostuar(UploadedFile uploadedFile)
        {

            if (!uploadedFile.IsValid)
                return string.Empty;
            //Session.Add("Imazhi", uploadedFile.FileBytes);
            DbCore.mySessionObjects.ruajImazhNeSesion(Session, uploadedFile.FileBytes);

            //ThumbnailFileName = CacheLayer.GlobalCacheManager.MySessionCache["ThumbnailFileName"].ToString();
            //string fileName = Path.Combine(MapPath(UploadDirectory), ThumbnailFileName);
            //Image original = Image.FromStream(uploadedFile.FileContent);
            //if (original.PhysicalDimension.Height > 250 || original.PhysicalDimension.Width > 250)
            //{
            //    if (original.PhysicalDimension.Height > original.PhysicalDimension.Width)

            //        koeficientiZvog = original.PhysicalDimension.Height / 250;
            //    else
            //        koeficientiZvog = original.PhysicalDimension.Width / 250;
            //}
            //else
            //    koeficientiZvog = 1;
            //System.Drawing.Bitmap newPic = new System.Drawing.Bitmap((int)(original.PhysicalDimension.Width / koeficientiZvog), (int)(original.PhysicalDimension.Height / koeficientiZvog));
            //System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(newPic);
            //gr.DrawImage(original, 0, 0, (int)(original.PhysicalDimension.Width / koeficientiZvog), (int)(original.PhysicalDimension.Height / koeficientiZvog));
            //newPic.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //gr.Dispose();
            //newPic.Dispose();
            //original.Dispose();
            //GC.Collect();
            return ThumbnailFileName;
        }

        public static byte[] imazhNeByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static System.Drawing.Image byteArrayNeImazh(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        protected void ngarkoImazh_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = RuajFilePostuar(e.UploadedFile);
        }
        protected void ucEmerSkedari_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var connString = MyConnectionsManager.GetSelectedConNameServer();
            string UploadDirectory = "~/Certifikatat/";
            if (!Directory.Exists(MapPath(UploadDirectory)))
            {
                Directory.CreateDirectory(MapPath(UploadDirectory));
            }
            idNdermarrje = Convert.ToInt32(DbCore.mySessionObjects.merrNdermarjeselectSesioni(Session));
           
            UploadDirectory += $"{ connString}/{idNdermarrje}/";
            if (!Directory.Exists(MapPath(UploadDirectory)))
            {
                Directory.CreateDirectory(MapPath(UploadDirectory));
            }
            
            string extension = System.IO.Path.GetExtension(ucEmerSkedari.UploadedFiles[0].FileName);
            string filename = extension == ".p12" ? "certifikata" : "password";


            if (!File.Exists(UploadDirectory + filename + extension))
            {
                ucEmerSkedari.SaveAs(MapPath(UploadDirectory) + filename + extension);

                DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                ndermarrje.RuajNdermPath(idNdermarrje, UploadDirectory);



                DbCore.mySessionObjects.ruajpathneSession(Session, UploadDirectory);
            }
                



        }
        protected void upload_Clilck(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            var path = DbCore.mySessionObjects.merrPathNgaSesioni(Session)==null?ndermarrje.Pathname: DbCore.mySessionObjects.merrPathNgaSesioni(Session);

            if (path == null)
                return;
            int fileCount = Directory.GetFiles(MapPath(path), "*.*", SearchOption.TopDirectoryOnly).Length;
            if (fileCount == 1)
            {
                string[] files = Directory.GetFiles(MapPath(path));
                string extension = Path.GetExtension(files[0]);

                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Vazhdoi me ngarkimin e " + (extension == ".p12" ? "password-it!" : "certifikates p12!"), pnlMesazhi);
                
            }
            if(fileCount==2 )
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "File jane ngarkuan me sukses !", pnlMesazhi);
        }
            
        protected void pastro_Clilck(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrNdermarjeselectSesioni(Session));

            var path = DbCore.mySessionObjects.merrPathNgaSesioni(Session) == null ? ndermarrje.Pathname : DbCore.mySessionObjects.merrPathNgaSesioni(Session);

            if (path == null || path == "" || MapPath(path) == MapPath("") || !MapPath(path).Contains("Certifikatat"))
                return;
            string[] files = Directory.GetFiles(MapPath(path));
            int fileCount = Directory.GetFiles(MapPath(path), "*.*", SearchOption.TopDirectoryOnly).Length;
            if (fileCount > 0)
            {
                foreach (string file in files)
                {
                    File.Delete(file);

                }
                Directory.Delete(MapPath(path));
               clsMesazh mesazh;
                mesazh = ndermarrje.RuajNdermPath(idNdermarrje, null);
                DbCore.mySessionObjects.ruajpathneSession(Session, null);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Filet u fshin me sukses!", pnlMesazhi);

            }
            
        }
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, ASPxGridView_Ndermarrjet.ID, "Shto_Ndermarrje.aspx", "FilterDefault", ASPxGridView_Ndermarrjet.FilterExpression, ASPxGridView_Ndermarrjet, "NdermarrjeKodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Ndermarrjet, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 129, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "grid_ListNdermarrjet ", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Ndermarrje.aspx");

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmratLabelave()
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            popFshi.HeaderText = rm.GetString("labelAdministrimiKujdes", ci);
            lblMsgbox.Text = rm.GetString("labelAdministrimiMsgJeniSigurt", ci);
            ButtonOk.Text = rm.GetString("btnAdministrimiOk", ci);
            ButtonCancel.Text = rm.GetString("btnAdministrimiCancel", ci);
            popLlojLicence.Text = rm.GetString("labelAdministrimiKujdes", ci);
            lblLlojLicence.Text = rm.GetString("labelAdministrimiLlojiLicenses", ci);
            ButtonOk2.Text = rm.GetString("btnAdministrimiOk", ci);
            ButtonOk3.Text = rm.GetString("btnAdministrimiOk", ci);
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", ci);
            konfigurimi_Label.Text = rm.GetString("labelAdministrimiModeli", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblKodi")).Text = rm.GetString("labelAdministrimiKodi", ci);
            ((ASPxTextBox)ASPxPageControl1.TabPages[1].FindControl("kodi_TextBox")).ValidationSettings.RegularExpression.ErrorText = rm.GetString("msgAdministrimiNrKarakteresh", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblPershkrimi")).Text = rm.GetString("filterAdministrimiPershkrimi", ci);
            ((ASPxMemo)ASPxPageControl1.TabPages[1].FindControl("pershkrimi_TextBox")).ValidationSettings.RegularExpression.ErrorText = rm.GetString("msgAdministrimiThonjezaDyshe", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblNipti")).Text = rm.GetString("labelAdministrimiNIPT", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblLicenca")).Text = rm.GetString("labelAdministrimiLicenca", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblKodiFiskal")).Text = rm.GetString("labelAdministrimiKodiFiskal", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblMonedha")).Text = rm.GetString("labelAdministrimiMonedha", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblViti")).Text = rm.GetString("labelAdministrimiViti", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblNrTvsh")).Text = rm.GetString("labelAdministrimiNrTvsh", ci);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("labelAdministrimiKontakt", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("lblEmail")).Text = rm.GetString("labelAdministrimiEmail", ci);
            ((ASPxTextBox)ASPxPageControl1.TabPages[2].FindControl("email_TextBox")).ValidationSettings.RegularExpression.ErrorText = rm.GetString("msgAdministrimiFormatGabuar", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("lblVendi")).Text = rm.GetString("labelAdministrimiVendi", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("lblQyteti")).Text = rm.GetString("labelAdministrimiQyteti", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("lblTel")).Text = rm.GetString("labelAdministrimiTel", ci);
            ((ASPxTextBox)ASPxPageControl1.TabPages[2].FindControl("tel_TextBox")).ValidationSettings.RegularExpression.ErrorText = rm.GetString("msgAdministrimiVetemNumra", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("lblFax")).Text = rm.GetString("labelAdministrimiFax", ci);
            ((ASPxTextBox)ASPxPageControl1.TabPages[2].FindControl("fax_TextBox")).Text = rm.GetString("msgAdministrimiVetemNumra", ci);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("labelAdministrimiNderrmarjeModel", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[3].FindControl("lblNdermarjaTransferuese")).Text = rm.GetString("labelAdministrimiNdermarrjeTransferuese", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("llog_check")).Text = rm.GetString("checkboxAdministrimiPlaniStandartllog", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("grup_llog_check")).Text = rm.GetString("checkboxAdministrimiGrupeNengrupellog", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("kpf_check")).Text = rm.GetString("checkboxAdministrimiStrukturallog", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("monedha_check")).Text = rm.GetString("checkboxAdministrimiMonedha", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("njesi_check")).Text = rm.GetString("checkboxAdministrimiNjesite", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("rap_fin_check")).Text = rm.GetString("checkboxAdministrimiRapFinanciare", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("skema_check")).Text = rm.GetString("checkboxAdministrimiSkemaKontabilizimi", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("nivelTVSH_check")).Text = rm.GetString("checkboxAdministrimiTaksat", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("qytete_check")).Text = rm.GetString("checkboxAdministrimiQytete", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[3].FindControl("ASPxLabel21")).Text = rm.GetString("labelAdministrimiKonfigurimeKF", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("maturimi_check")).Text = rm.GetString("checkboxAdministrimiMaturimi", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("men_pageses_check")).Text = rm.GetString("checkboxAdministrimiMenyraPagese", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("men_trans_check")).Text = rm.GetString("checkboxAdministrimiMenyraTransport", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("kushte_drg_check")).Text = rm.GetString("checkboxAdministrimiKushteDergimi", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("nivel_cmimi_check")).Text = rm.GetString("checkboxAdministrimiNivelCmimi", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("nivel_zbritje_check")).Text = rm.GetString("checkboxAdministrimiNivelZbritje", ci);
            ((ASPxCheckBox)ASPxPageControl1.TabPages[3].FindControl("kategori_zbritje_check")).Text = rm.GetString("checkboxAdministrimiKategoriZbritje", ci);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("labelAdministrimiLogoNdermarrje", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[4].FindControl("lblZgjidhImaxh")).Text = rm.GetString("labelAdministrimiZgjidhLogo", ci);
            ((ASPxUploadControl)ASPxPageControl1.TabPages[4].FindControl("ngarkoImazh")).ValidationSettings.MaxFileSizeErrorText = rm.GetString("msgAdministrimiImazhiiMadh", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[4].FindControl("lblTipLejuar")).Text = rm.GetString("labelAdministrimiTipLejuar", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[4].FindControl("lblMaksimumiLejuar")).Text = rm.GetString("labelAdministrimiMaksimumiLejuar", ci);
            ((ASPxButton)ASPxPageControl1.TabPages[4].FindControl("btnNgarko")).Text = rm.GetString("btnAdministrimiNgarko", ci);
            popupUniversal.HeaderText = rm.GetString("popupAdministrimiUniversal", ci);
        }

        protected void OnPreRender_ASPxGridView_Ndermarrjet(object sender, EventArgs e)
        {
            EmratLabelave();
        }

        private void LoadString(ASPxHiddenField hfMsgShtoNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            hfMsgShtoNdermarrje.Clear();
            hfMsgShtoNdermarrje.Add("MsgZgjidhNdermarrje", rm.GetString("MsgAdministrimZgjidhNdermarrje", ci));
        }

        protected void btnVazhdoRuajtje_Click(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = DbCore.mySessionObjects.merrNdermarjeReNgaSesioni(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            int idNdermarrjeLoguar = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            vazhdoRuajtjeNdermarrjeje(ndermarrje, rm, ci, idNdermarrjeLoguar);
        }

        protected void txtGrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("txtGrupi"))
                {
                    DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                    ConfigureAspxComboBox.mbushComboGrupeNdermarrjesh(nderm.IdLicenca, txtGrupi);              
                }
            }
        }

        protected void cmbMeme_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbMeme"))
                {
                    DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                    ConfigureAspxComboBox.mbushComboNdermarjeMeme(nderm.IdLicenca, cmbMeme);
                }
            }
        }

        private void shtoNdermarrjeNeCmbRaportues(int idNdermarrje, string kodNdermarrje)
        {
            hfState.Set("idNdermERe", idNdermarrje);
            hfState.Set("kodNdermERe", kodNdermarrje);
        }
    }
    // kerkesa per fiskalizim
    //???????
    //var faturat = clsFunksioneFiskalizimi.merrVleratEFaturaveEinvoice(xml, "Einvoices", true);


    //XmlDocument doc = new XmlDocument();
    //doc.LoadXml(faturat);
            
    //        var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
    //hfState.Set("idNdermarrje",idNdermarrje);

           
           

    //        hfStateNdermarrje.Value = idNdermarrje.ToString();

    //        //pershtatja e re per hfstate - idNdermarrje

    //        hfState.Set("json", json);
    //        hfState.Set("idNdermarrje", idNdermarrje);
}