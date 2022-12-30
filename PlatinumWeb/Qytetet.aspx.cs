using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Globalization;
using System.Resources;
using DbCore;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Qytetet : MyPageBase
    {
        ArrayList vlerat = new ArrayList();
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        //private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje qytet!";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching, so can't be viewed offline
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            } 
            
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListQytetet", 1, "Qytetet.aspx");
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            if (!IsPostBack)
            {
                //Session.Add("mesazh", ":Green");
                
                mbushHiddenFieldMePerkthime(ci, rm);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);

                konfiguroVleraFillestare();
                konfiguroGride();
                // new DbCore.clsFunksione().konfiguroMenu(ASPxMenu1);
                // ASPxMenu1.AutoPostBack = false;
                // new DbCore.clsFunksione().percaktoTedrejtatPerKeteFaqe("Qytetet.aspx", ASPxMenu1);

                if (Request.QueryString["indexrow"] != null)
                {
                    grid_ListQytetet.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
                }
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Qytetet.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="ci">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {

            hfState.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", ci));

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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Qytetet.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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

        private void konfiguroVleraFillestare()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.colQytetet colQyt = new DbCore.DbAdmin.colQytetet();
            colQyt.mbushQytetetNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbAdmin.colQytetet colQyt = dbAdmin.merrQytetetNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            grid_ListQytetet.DataSource = colQyt;
            grid_ListQytetet.DataBind();
        }

        protected void grid_ListQytetet_DataBound(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (grid_ListQytetet.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                grid_ListQytetet.Columns.Add(check);
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //check.SetColVisibleIndex(0);

                grid_ListQytetet.SettingsText.CommandUpdate = rm.GetString("buttonRuaj", ci);
                grid_ListQytetet.SettingsText.CommandCancel = rm.GetString("labelAnullo",ci);

                grid_ListQytetet.Settings.ShowFilterRow = true;
                grid_ListQytetet.KeyFieldName = "IdQyteti";
                grid_ListQytetet.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListQytetet.SettingsBehavior.AllowFocusedRow = true;
                grid_ListQytetet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ListQytetet.Settings.ShowFilterRowMenu = true;
            }
        }

        private void konfiguroGride()
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_ListQytetet, "grid_ListQytetet", "Qytetet.aspx");
            GridUtil.konfiguroGrideListeEvogelPaTheme(grid_ListQytetet, "IdQyteti");
            grid_ListQytetet.Columns["#"].VisibleIndex = 0;
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListQytetet", "Qytetet.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListQytetet", 1, "Qytetet.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status)
                {
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                konfiguroVleraFillestare();
                grid_ListQytetet.FilterExpression = String.Empty;
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("grid_ListQytetet", "NjesiArtikulli.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListQytetet", "Qytetet.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_ListQytetet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiQyteti", grid_ListQytetet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_ListQytetet.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodiQyteti";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListQytetet", 1, "Qytetet.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";

        }

        protected void grid_ListQytetet_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Filter)
            {
                ASPxComboBox combo = (sender as ASPxGridView).FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;
            }
        }

        protected void grid_ListQytetet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "EmriQyteti")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        protected void grid_ListQytetet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
        }

        protected void grid_ListQytetet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsQyteti qyteti = new DbCore.DbAdmin.clsQyteti();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session) , DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Qytetet.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", ci));
                return;
            }
            qyteti.IdQyteti = int.Parse(e.Keys["IdQyteti"].ToString());
            qyteti.KodiQyteti = e.NewValues["KodiQyteti"].ToString();
            qyteti.EmriQyteti = e.NewValues["EmriQyteti"].ToString();
            qyteti.IdNdermarja = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            qyteti.IdPerdoruesi = oPerdorues.IdPerdorues;
            qyteti.IdStatusDok = 1;
            if (qyteti.EmriQyteti != "" && qyteti.KodiQyteti != "")
            {
                e.Cancel = true;
                DbCore.clsMesazh m = qyteti.modifiko();
                if (m.Status)
                {
                    //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = m.PershkrimMesazhi + ":Green";
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgModifikimiMeSuksesGreen", ci));
                }
                else
                {
                    //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = m.PershkrimMesazhi + ":Red";
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgModifikimiMeGabimeRed", ci));
                }
                grid_ListQytetet.CancelEdit();
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            List<object> rreshtat = grid_ListQytetet.GetSelectedFieldValues("IdQyteti");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniTePaktenNjeQytet", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            string[] qytetetLidhura = new string[rreshtat.Count];
            int i = 0;
            bool done = false;
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (int id in rreshtat)
            {

                DbCore.DbAdmin.clsQyteti clsQyt = new DbCore.DbAdmin.clsQyteti(id);
                if (!clsQyt.eshteQytetILidhur())
                {
                    clsQyt.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    mesazh = clsQyt.fshi();
                }
                else
                {
                    done = true;
                    qytetetLidhura[i++] = clsQyt.EmriQyteti;
                }
            }
            if (done)
            {
                String qytetLidhur = String.Join(", ", qytetetLidhura, 0, i);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgQyteti", ci) + qytetLidhur + rm.GetString("msgNukMundTeFshihetEshteILidhur", ci), pnlMesazhi);
            }
            else
            {
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaPerfundoiMeSukses", ci), pnlMesazhi);
            }

            konfiguroVleraFillestare();
            pnlGrida.Update();

        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Modifiko")
            //{
            //    int indeksi = grid_ListQytetet.FocusedRowIndex;
            //    grid_ListQytetet.StartEdit(indeksi);
            //}

            //else
            //    if (e.Item.Name == "Shto")
            //    {
            //        Response.Redirect("Shto_Qytet.aspx");
            //    }
        }

        protected void grid_ListQytetet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void grid_ListQytetet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DbCore.DbAdmin.clsQyteti qyteti = new DbCore.DbAdmin.clsQyteti();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Qytetet.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", ci));
                return;
            }
            qyteti = new DbCore.DbAdmin.clsQyteti();
            qyteti.KodiQyteti = e.NewValues["KodiQyteti"].ToString();
            qyteti.EmriQyteti = e.NewValues["EmriQyteti"].ToString();
            qyteti.IdNdermarja = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            qyteti.IdPerdoruesi = oPerdorues.IdPerdorues;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            qyteti.IdStatusDok = 1;
            if (qyteti.EmriQyteti != "" && qyteti.KodiQyteti != "")
            {
                e.Cancel = true;
                mesazh = qyteti.ruaj();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgGabimiNeRuajtje", ci)+ ":Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimiNeRuajtje", ci), pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", ci));
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                    grid_ListQytetet.CancelEdit(); 
                    konfiguroVleraFillestare();
                }
            }
        }

        protected void grid_ListQytetet_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (hfRuaj.Value == "Ruaj")
                if (!grid_ListQytetet.IsNewRowEditing)
                {
                    grid_ListQytetet.DoRowValidation();
                }
        }

        protected void grid_ListQytetet_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            foreach (GridViewColumn column in grid_ListQytetet.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (e.NewValues[dataColumn.FieldName] == null)
                    {
                        e.Errors[dataColumn] = rm.GetString("msgVleraNukMundTeJeteNull", ci);
                    }
                }
            }
            if (e.Errors.Count > 0) e.RowError = rm.GetString("msgStrukturaAdministrativePlotesoFushat", ci);

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0) e.RowError = rm.GetString("msgStrukturaAdministrativeKorrigjoGabimet",ci);
        }

        protected void grid_ListQytetet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grid_ListQytetet.FilterExpression = "";
                else
                {

                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListQytetet", "Qytetet.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        grid_ListQytetet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_ListQytetet);
                        konfiguroVleraFillestare();

                    }
                }
            }
            konfiguroGride();
        }

        protected void grid_ListQytetet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ListQytetet.PageIndex;
            e.Properties["cpPageRow"] = grid_ListQytetet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ListQytetet.VisibleRowCount;
        }
    }
}
