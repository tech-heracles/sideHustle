using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaGrupBanke : MyPageBase
    {
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        ArrayList vlerat = new ArrayList();
        private DbCore.DbArkaBanka.clsGrupBanke grupoverview;
        protected void Page_Load(object sender, EventArgs e)
        {   //   konfiguroMenu(ASPxMenu1);      
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (!Page.IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                mbushPopUpListeGrupeBanke();
                konfiguroPopupGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupBanke.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim) gvLupaGrBanka.CancelEdit();
            }            
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);            
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaGrupBanke.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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

        private void mbushPopUpListeGrupeBanke()
        {//mbush griden e popupit me te dhena
            DbCore.DbArkaBanka.colGrupeBanke colGrupe = new DbCore.DbArkaBanka.colGrupeBanke();
            string veprimi = Request.QueryString["veprimi"];
            if (veprimi.EqualsAnyIgnoreCase("Arka"))
                colGrupe = new DbCore.DbArkaBanka.colGrupeBanke(false, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            else if (veprimi.EqualsAnyIgnoreCase("Banka"))
                colGrupe = new DbCore.DbArkaBanka.colGrupeBanke(true, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            else
                new DbCore.DbArkaBanka.colGrupeBanke(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvLupaGrBanka.DataSource = colGrupe;
            gvLupaGrBanka.DataBind();
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaGrBanka, "gvLupaGrBanka", "LupaGrupBanke.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/GrBnk", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.konfiguroGrideListeEvogelPopupiPaTheme(gvLupaGrBanka, "IdGrupBanke", endlessScroll);
            mbushPopUpListeGrupeBanke();
        }

        protected void gvLupaGrBanka_DataBound(object sender, EventArgs e)
        {
            gvLupaGrBanka.SettingsText.CommandUpdate = "Ruaj";
            gvLupaGrBanka.KeyFieldName = "IdGrupBanke";
            gvLupaGrBanka.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaGrBanka.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void gvLupaGrBanka_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        { //kur popupgrida ben callback
            mbushPopUpListeGrupeBanke();
            gvLupaGrBanka.Selection.UnselectAll();
        }

        protected void gvLupaGrBanka_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }

        protected void gvLupaGrBanka_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupBanke.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            grupoverview = new DbCore.DbArkaBanka.clsGrupBanke();
            grupoverview.NrGrupBanke = e.NewValues["NrGrupBanke"].ToString();
            grupoverview.PershkrimGrupBanke = e.NewValues["PershkrimGrupBanke"].ToString();
            grupoverview.IdPerdoruesi = oPerdorues.IdPerdorues;
            grupoverview.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (Request.QueryString["veprimi"] == "Arka")
                grupoverview.LlojArkaBanka = false;
            else grupoverview.LlojArkaBanka = true;
            grupoverview.IdStatusDok = 1;
            e.Cancel = true;
            if (isValidGrupBankeOverview())
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = grupoverview.ruaj();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                }
                gvLupaGrBanka.CancelEdit(); gvLupaGrBanka.SettingsEditing.Mode = GridViewEditingMode.Inline;
                mbushPopUpListeGrupeBanke();

                gvLupaGrBanka.AddNewRow();
            }
            else mbushPopUpListeGrupeBanke();
        }

        protected void gvLupaGrBanka_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara

            foreach (GridViewColumn column in gvLupaGrBanka.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {

                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }

            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";

            }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";

            }
            mbushPopUpListeGrupeBanke();
        }

        protected void gvLupaGrBanka_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (!gvLupaGrBanka.IsNewRowEditing)
            {
                gvLupaGrBanka.DoRowValidation();
            }
        }

        private void ruajGrupBankeOverview()
        {//ben ruajtjen  e nje rreshti te ri

            if (Page.IsValid == false)
                return;
            else
            {
                gvLupaGrBanka.UpdateEdit();
                if (isValidGrupBankeOverview())
                {
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    mesazh = grupoverview.ruaj();
                    if (!mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                    }

                    mbushPopUpListeGrupeBanke();
                    konfiguroPopupGride();

                    gvLupaGrBanka.AddNewRow();
                }
                else
                {
                    mbushPopUpListeGrupeBanke();

                }
            }
        }

        //kontrollon nese grupi i bankes ekziston
        private bool isValidGrupBankeOverview()
        {
            bool isValid;
            isValid = true;

            if (grupoverview == null)
            {
                isValid = false;
            }
            else
            {
                DbCore.DbArkaBanka.clsDatabaseArkaBanka dbArkaBanka = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
                if (dbArkaBanka.ekzistonGrupBanke(grupoverview.NrGrupBanke, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
                {
                    isValid = false;
                    //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = "Ekziston nje grup banke me kete numer!Ju lutemi shenoni nje nr tjeter.:Red";
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje grup banke me kete numer!Ju lutemi shenoni nje nr tjeter.:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje grup banke me kete numer!Ju lutemi shenoni nje nr tjeter.", pnlMesazhi);
                    return isValid;
                }
                dbArkaBanka.Dispose();
            }
            return isValid;
        }

        protected void gvLupaGrBanka_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {//ben modifikimin e nje rreshti

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupBanke.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbArkaBanka.clsGrupBanke grup = new DbCore.DbArkaBanka.clsGrupBanke();

            grup.IdGrupBanke = int.Parse(e.Keys["IdGrupBanke"].ToString());
            grup.NrGrupBanke = e.NewValues["NrGrupBanke"].ToString();
            grup.PershkrimGrupBanke = e.NewValues["PershkrimGrupBanke"].ToString();
            grup.IdPerdoruesi = oPerdorues.IdPerdorues;
            grup.IdStatusDok = 1;
            grup.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (Request.QueryString["veprimi"] == "Arka")
                grup.LlojArkaBanka = false;
            else grup.LlojArkaBanka = true;
            if (grup.PershkrimGrupBanke != "" && grup.NrGrupBanke != "")
            {
                e.Cancel = true;
                grup.modifiko();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                gvLupaGrBanka.CancelEdit();
            }
            gvLupaGrBanka.SettingsEditing.Mode = GridViewEditingMode.Inline; 
            mbushPopUpListeGrupeBanke();

        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e nje grup kontabilizimi
            List<object> rreshtat = gvLupaGrBanka.GetSelectedFieldValues("IdGrupBanke");
            DbCore.DbArkaBanka.clsDatabaseArkaBanka dbArkaBanka = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
            foreach (int id in rreshtat)
            {

                DbCore.DbArkaBanka.clsGrupBanke clsGrup = new DbCore.DbArkaBanka.clsGrupBanke(id);
                
                if (dbArkaBanka.kaBanke(clsGrup.IdGrupBanke))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky grup ka banka", pnlMesazhi);
                }
                else
                {
                    clsGrup.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    clsGrup.fshi();
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                    mbushPopUpListeGrupeBanke();
                }
            }
            dbArkaBanka.Dispose();
        }

        //public void konfiguroMenu(ASPxMenu menu)
        //    {//konfigurimi i menu si toolbar

        //    menu.CssFilePath = "~/App_Themes/Aqua/{0}/styles.css";
        //    menu.CssPostfix = "Aqua";
        //    menu.ImageFolder = "~/App_Themes/Aqua/{0}/";
        //    menu.ItemSpacing = 0;

        //    menu.SeparatorHeight = 100;
        //    menu.SeparatorWidth = 1;

        //    menu.ImageSpacing = 7;
        //    menu.Height = 7;
        //    menu.Items.Clear();
        //    for (int i = 0; i < 5; i++)
        //        {
        //        menu.Items.Add();
        //        }
        //    menu.AutoPostBack = true;
        //    menu.Items[0].Name = "Modifiko";
        //    menu.Items[0].Text = "Modifiko";
        //    menu.Items[0].Image.Url = "~/images/01.bmp";
        //    menu.Items[1].Name = "RuajRresht";
        //    menu.Items[1].Text = "Ruaj";
        //    menu.Items[1].Image.Url = "~/images/03.bmp";
        //    menu.Items[2].Name = "Fshi";
        //    menu.Items[2].Text = "Fshi";
        //    menu.Items[2].Image.Url = "~/images/05.bmp";
        //    menu.Items[3].Name = "Pastro";
        //    menu.Items[3].Text = "Pastro";
        //    menu.Items[3].Image.Url = "~/images/05.bmp";
        //    menu.Items[4].Name = "Shto";
        //    menu.Items[4].Text = "Shto";
        //    menu.Items[4].Image.Url = "~/images/03.bmp";
        //    menu.VerticalPopOutImage.Height = 11;
        //    menu.VerticalPopOutImage.Width = 11;
        //    menu.ItemStyle.ImageSpacing = 5;
        //    menu.ItemStyle.PopOutImageSpacing = 18;
        //    menu.SubMenuStyle.GutterWidth = 0;
        //    menu.SubMenuItemStyle.ImageSpacing = 7;
        //    menu.BorderWidth = 1;
        //    menu.HorizontalPopOutImage.Height = 7;
        //    menu.HorizontalPopOutImage.Width = 7;

        //    }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvLupaGrBanka.FocusedRowIndex;
                gvLupaGrBanka.StartEdit(indeksi);
                gvLupaGrBanka.SettingsEditing.Mode = GridViewEditingMode.EditForm;

            }

            //else if (e.Item.Name == "RuajRresht")
            //    {
            //    Page.Validate();
            //    ruajGrupBankeOverview();
            //    }
            else if (e.Item.Name == "Pastro")
            {
                gvLupaGrBanka.SettingsEditing.Mode = GridViewEditingMode.Inline;
                gvLupaGrBanka.AddNewRow();
            }
            else if (e.Item.Name == "Shto")
            {
                gvLupaGrBanka.AddNewRow();
                gvLupaGrBanka.SettingsEditing.Mode = GridViewEditingMode.Inline;
            }
        }

        protected void gvLupaGrBanka_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {

        }
    }
}
