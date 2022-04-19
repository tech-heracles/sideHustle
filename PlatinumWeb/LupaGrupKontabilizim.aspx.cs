using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaGrupKontabilizimi : MyPageBase
    {
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        ArrayList vlerat = new ArrayList();
        private DbCore.DbKontabiliteti.clsGrupKontabilizimi grupoverview;

        protected void Page_Load(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
          
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            if (!Page.IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                mbushPopUpListeGrupeKontabilizimi();
                konfiguroPopupGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupKontabilizim.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim) 
                    gvLupaGrKont.CancelEdit();
            }
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaGrupKontabilizim.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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

        private void mbushPopUpListeGrupeKontabilizimi()
        {//mbush griden e popupit me te dhena

            DbCore.DbKontabiliteti.colGrupeKontabilizimi colGrupe = new DbCore.DbKontabiliteti.colGrupeKontabilizimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvLupaGrKont.DataSource = colGrupe;
            gvLupaGrKont.DataBind();
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaGrKont, "gvLupaGrKont", "LupaGrupKontabilizim.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/GrKont", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.konfiguroGrideListeEvogelPopupiPaTheme(gvLupaGrKont, "IdGrupKontabilizimi", endlessScroll);
            mbushPopUpListeGrupeKontabilizimi();
        }

        protected void gvLupaGrKont_DataBound(object sender, EventArgs e)
        {
            gvLupaGrKont.SettingsText.CommandUpdate = "Ruaj";
            gvLupaGrKont.KeyFieldName = "IdGrupKontabilizimi";
            gvLupaGrKont.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaGrKont.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void gvLupaGrKont_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback

            mbushPopUpListeGrupeKontabilizimi();
            gvLupaGrKont.Selection.UnselectAll();
        }

        protected void gvLupaGrKont_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }

        protected void gvLupaGrKont_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupKontabilizim.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            grupoverview = new DbCore.DbKontabiliteti.clsGrupKontabilizimi();
            grupoverview.NrGrupKontabilizimi = e.NewValues["NrGrupKontabilizimi"].ToString();
            grupoverview.PershkrimGrupKontabilizimi = e.NewValues["PershkrimGrupKontabilizimi"].ToString();
            grupoverview.IdPerdoruesi = oPerdorues.IdPerdorues;
            grupoverview.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            grupoverview.IdStatusDok = 1;
            if (isValidGrupKontabilizimiOverview())
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
                gvLupaGrKont.CancelEdit(); gvLupaGrKont.SettingsEditing.Mode = GridViewEditingMode.Inline;
                mbushPopUpListeGrupeKontabilizimi();

                gvLupaGrKont.AddNewRow();
            }
            else mbushPopUpListeGrupeKontabilizimi();
            e.Cancel = true;
        }

        protected void gvLupaGrKont_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara

            foreach (GridViewColumn column in gvLupaGrKont.Columns)
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
        }

        protected void gvLupaGrKont_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (!gvLupaGrKont.IsNewRowEditing)
            {
                gvLupaGrKont.DoRowValidation();
            }
        }

        private void ruajGrupKontabilizimiOverview()
        {//ben ruajtjen  e nje rreshti te ri
            
            if (Page.IsValid == false)
                return;
            else
            {
                gvLupaGrKont.UpdateEdit();
                if (isValidGrupKontabilizimiOverview())
                {
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    mesazh = grupoverview.ruaj();
                    if (!mesazh.Status == true)
                    {
                      clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!",pnlMesazhi);
                    }
                    else
                    {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                    }

                    mbushPopUpListeGrupeKontabilizimi();
                    gvLupaGrKont.AddNewRow();
                }
            }
        }
        //kontrollon nese grupi i kontabilizimit ekziston
        private bool isValidGrupKontabilizimiOverview()
        {
            bool isValid;
            isValid = true;

            if (grupoverview == null)
            {
                isValid = false;
            }
            else
                {
                DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
                if (dbKontabiliteti.ekzistonGrupKontabilizimi(grupoverview.NrGrupKontabilizimi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
                {
                    isValid = false;
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje grup kontabilizimi me kete numer!Ju lutemi shenoni nje nr tjeter.:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo,"Ekziston nje grup kontabilizimi me kete numer!Ju lutemi shenoni nje nr tjeter.",pnlMesazhi);
                    dbKontabiliteti.Dispose();
                    return isValid;
                }
                dbKontabiliteti.Dispose();
            }
            return isValid;
        }

        protected void gvLupaGrKont_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupKontabilizim.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbKontabiliteti.clsGrupKontabilizimi grup = new DbCore.DbKontabiliteti.clsGrupKontabilizimi();
            grup.IdGrupKontabilizimi = int.Parse(e.Keys["IdGrupKontabilizimi"].ToString());
            grup.NrGrupKontabilizimi = e.NewValues["NrGrupKontabilizimi"].ToString();
            grup.PershkrimGrupKontabilizimi = e.NewValues["PershkrimGrupKontabilizimi"].ToString(); ;
            grup.IdPerdoruesi = oPerdorues.IdPerdorues;
            grup.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            grup.IdStatusDok = 1;
          
            if (grup.PershkrimGrupKontabilizimi != "" && grup.NrGrupKontabilizimi != "")
            {
                e.Cancel = true;
                grup.modifiko();   
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                gvLupaGrKont.CancelEdit();
            }
            gvLupaGrKont.SettingsEditing.Mode = GridViewEditingMode.Inline;
            mbushPopUpListeGrupeKontabilizimi();
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e nje grup kontabilizimi
            List<object> rreshtat = gvLupaGrKont.GetSelectedFieldValues("IdGrupKontabilizimi");
            DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            foreach (int id in rreshtat)
            {
                DbCore.DbKontabiliteti.clsGrupKontabilizimi clsgrupKont = new DbCore.DbKontabiliteti.clsGrupKontabilizimi(id);
                
                if (dbKontabiliteti.kaFleteKontabel(clsgrupKont.IdGrupKontabilizimi))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky grup ka flete kontabel",pnlMesazhi);
                    }
                else
                    {
                    clsgrupKont.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                        clsgrupKont.fshi();
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                        mbushPopUpListeGrupeKontabilizimi();
                    }
            }
            dbKontabiliteti.Dispose();

        }
        //public void konfiguroMenu(ASPxMenu menu)
        //{//konfigurimi i menu si toolbar

        //    menu.CssFilePath = "~/App_Themes/Aqua/{0}/styles.css";
        //    menu.CssPostfix = "Aqua";
        //    menu.ImageFolder = "~/App_Themes/Aqua/{0}/";
        //    menu.ItemSpacing = 0;

        //    menu.SeparatorHeight = 100;
        //    menu.SeparatorWidth = 1;

        //    menu.ImageSpacing = 7;
        //    menu.Height = 7;
        //    for (int i = 0; i < 5; i++)
        //    {
        //        menu.Items.Add();
        //    }
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

        //}
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvLupaGrKont.FocusedRowIndex;
                gvLupaGrKont.StartEdit(indeksi);
                gvLupaGrKont.SettingsEditing.Mode = GridViewEditingMode.EditForm;

            }

            //else if (e.Item.Name == "Ruaj")
            //{
            //    Page.Validate();
            //    ruajGrupKontabilizimiOverview();
            //}
            else if (e.Item.Name == "Pastro")
            {
                gvLupaGrKont.SettingsEditing.Mode = GridViewEditingMode.Inline;
                gvLupaGrKont.AddNewRow();
            }
            else if (e.Item.Name == "Shto")
            {
                gvLupaGrKont.AddNewRow();
                gvLupaGrKont.SettingsEditing.Mode = GridViewEditingMode.Inline;
            }
        }


    }
}
