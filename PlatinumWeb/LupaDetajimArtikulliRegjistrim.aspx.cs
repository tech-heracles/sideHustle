using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using System.Globalization;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaDetajimArtikulliRegjistrim : MyPageBase
    {
        bool hiqbutoninLidh = false;
        bool hiqbutoninShto = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            if (Request.QueryString["vjenNga"] != null && Request.QueryString["vjenNga"] != "")
                hiqbutoninLidh = Request.QueryString["vjenNga"].ToString() == "kodbar";

            if (Request.QueryString["klonim"] != null && Request.QueryString["klonim"] != "")
                hiqbutoninShto = Request.QueryString["klonim"].ToString() == "true";

            bool kerkosaposhkruar = true; 
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "DetArtRegj");
            var endlessScroll = (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po");
            hfState.Set("idPerdoruesi", idPerdoruesi);
            hfState.Set("idNdermarrje", idNdermarrje);
            if (!IsPostBack)
            {
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
                kerkosaposhkruar = (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po");
                mbushPopUpListe(idNdermarrje, idPerdoruesi);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, ci, endlessScroll);
                gvLupaDetArtRegj.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaDetArtRegj")))
                {
                    mbushPopUpListe(idNdermarrje, idPerdoruesi);
                    konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, ci, endlessScroll);
                }
            }
        }

        private void mbushPopUpListeNgaSession(int idPerdoruesi, int idNdermarrje)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListe(idNdermarrje, idPerdoruesi);
            else
            {
                gvLupaDetArtRegj.DataSource = tmpObject;
                gvLupaDetArtRegj.DataBind();
            }
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaDetajimArtikulliRegjistrim.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
            if (hiqbutoninLidh)
            {
                var item = aSPxMenu1.Items.FindByName("Lidh");
                if (item != null)
                    item.ClientVisible = false;
            }
            if (hiqbutoninShto)
            {
                var item = aSPxMenu1.Items.FindByName("Shto");
                if (item != null)
                    item.ClientVisible = false;
            }
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDetArtRegj", "LupaDetajimArtikulliRegjistrim.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaDetArtRegj.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDetajimArtikulli", gvLupaDetArtRegj);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaDetArtRegj.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdDetajimArtikulli";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDetArtRegj", 1, "LupaDetajimArtikulliRegjistrim.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDetArtRegj", "LupaDetajimArtikulliRegjistrim.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDetArtRegj", 1, "LupaDetajimArtikulliRegjistrim.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaDetArtRegj.FilterExpression = String.Empty;
            }
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        private void mbushPopUpListe(int idNdermarrje, int idPerdorues)
        {
            DbCore.DbInventari.colDetajimeArtikulli col = new DbCore.DbInventari.colDetajimeArtikulli();
            DbCore.DbInventari.colDetajimeArtikulli colGjendje = new DbCore.DbInventari.colDetajimeArtikulli();
            int idartikulli = String.IsNullOrEmpty(Request.QueryString["idArtikulli"]) ? 0 : int.Parse(Request.QueryString["idArtikulli"]);

            if (idartikulli > 0)
            {
                int lloji = 1;
                if (Request.QueryString["lloji"] != null)
                    lloji = int.Parse(Request.QueryString["lloji"]);

                col.ktheDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(idartikulli, idNdermarrje, idPerdorues, lloji);

                if (Request.QueryString["vjenNga"] == "dalje" || Request.QueryString["vjenNga"] == "shitje" || Request.QueryString["vjenNga"] == "EkzekutimProdhimi")
                {
                    DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                    art.mbushArtikull(idartikulli);

                    int idKategoria = lloji == 1 ? art.IdKategoriDetajimi : art.IdKategoriDetajimi2;

                    if (idKategoria == 3 || idKategoria == 4)
                    {
                        int detajimi1 = (lloji == 2 && !String.IsNullOrEmpty(Request.QueryString["detajimi1"])) ? DbCore.DbInventari.clsDetajimArtikulli.ktheIdDetajimi(Request.QueryString["detajimi1"], idNdermarrje) : 0;
                        string magazina = Request.QueryString["mag"];
                        int idMag = !String.IsNullOrEmpty(magazina) ? clsNjesiAdministrative.ktheIdMagazine(magazina, idNdermarrje) : -1;
                        DateTime dateDok = !String.IsNullOrEmpty(Request.QueryString["dateDok"]) ? Convert.ToDateTime(Request.QueryString["dateDok"]) : DateTime.Now;
                        double sasi = 0;
                        foreach (DbCore.DbInventari.clsDetajimArtikulli det in col)
                        {
                            if (lloji == 2 && detajimi1 != 0)
                                sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(art, idMag, dateDok, detajimi1, det.IdDetajimArtikulli);
                            else
                                sasi = clsTrupiMagazina.merrSasiSipasDetajimit(art, idMag, dateDok, det.IdDetajimArtikulli, lloji);

                            if (sasi > 0)
                                colGjendje.Add(det);
                        }
                        col = colGjendje;
                    }
                }
            }

            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, col);
            gvLupaDetArtRegj.DataSource = col;
            gvLupaDetArtRegj.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, CultureInfo ci, bool endlessScroll)
        {
            shto_Lloj();
            //shto_Autorizim();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaDetArtRegj, "gvLupaDetArtRegj", "LupaDetajimArtikulliRegjistrim.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhePopupi(gvLupaDetArtRegj, "IdDetajimArtikulli");
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaDetArtRegj, "IdDetajimArtikulli", kerkosaposhkruar, endlessScroll);
        }

        protected void gvLupaDetArtRegj_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
        }

        protected void gvLupaDetArtRegj_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaDetArtRegj.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvLupaDetArtRegj.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaDetArtRegj.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                gvLupaDetArtRegj.Settings.ShowFilterRow = true;
                gvLupaDetArtRegj.Columns.Add(check);
                gvLupaDetArtRegj.KeyFieldName = "IdDetajimArtikulli";
                gvLupaDetArtRegj.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaDetArtRegj.SettingsBehavior.AllowFocusedRow = true;
            }
        }

    
        private void shto_Lloj()
        {//shtohen komboja me Autorizimeve tek grida 
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaDetArtRegj.Columns["LlojDetajimArtikulli"].VisibleIndex;
            gvLupaDetArtRegj.Columns.Remove(gvLupaDetArtRegj.Columns["LlojDetajimArtikulli"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Alfanumerik", 1);
            colnew.PropertiesComboBox.Items.Add("Numerik", 2);
            colnew.PropertiesComboBox.Items.Add("Date", 3);
            colnew.FieldName = "LlojDetajimArtikulli";
            colnew.VisibleIndex = visibleindex;
            gvLupaDetArtRegj.Columns.Add(colnew);
        }

        //private void shtoKoloneTeRe(String emri)
        //{
        //    GridViewDataTextColumn colnew = new GridViewDataTextColumn();
        //    gvLupaDetArtRegj.Columns.Add(colnew);
        //    colnew.Caption = emri;
        //}   

        protected void gvLupaDetArtRegj_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaDetArtRegj.Selection.UnselectAll();
        }

        protected void gvLupaDetArtRegj_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

            if (e.Column.FieldName == "KontrollGjendje" || e.Column.FieldName == "KontrollCmimi")
            {
                ConfigureAspxComboBox.mbushComboAktivJoaktiv((e.Editor as ASPxComboBox));
            }

        }

        protected void gvLupaDetArtRegj_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvLupaDetArtRegj.VisibleRowCount;
        }
    }
}
