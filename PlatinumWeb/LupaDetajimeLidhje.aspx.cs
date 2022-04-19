using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaDetajimeLidhje : MyPageBase
    {
        private int idKonfigambjenti;
        protected void Page_Load(object sender, EventArgs e)
        {
            var vleraQueryString = string.Empty;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != string.Empty)
            {
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            }
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);




            var idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LDL", idNdermarrje);
            var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
            if (vleraQueryString != string.Empty)
            {
                var idte = vleraQueryString.Split('-');
                if (idte.Length > 1)
                {
                    var konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    for (var i = 0; i < idte.Length; i++)
                    {
                        konfigLupa.IdKonfigAmbjente = Convert.ToInt32(idte[i]);
                        konfigLupa = konfigLupa.merrSipasId();
                        if (konfigLupa.IdNivel == idNivel)
                        {
                            idKonfigambjenti = Convert.ToInt32(idte[i]);
                        }
                    }
                }
                else
                {
                    if (idte.Length == 1)
                    {
                        idKonfigambjenti = Convert.ToInt32(Request.QueryString["idKonfigAmbjente"].ToString());
                        if (idKonfigambjenti == 0)
                        {
                            merrKonfiguriminDefaultTeLupes(idNivel);
                        }
                    }
                    else
                    {
                        merrKonfiguriminDefaultTeLupes(idNivel);
                    }
                }
            }
            else
            {
                merrKonfiguriminDefaultTeLupes(idNivel);
            }


            var kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
            {
                kerkosaposhkruar = true;
            }
            else
            {
                kerkosaposhkruar = false;
            }
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;
            mbushPopUpListe(idPerdoruesi, idNdermarrje);
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaDetajime, idKonfigambjenti);
                konfiguroPopupGride(true, kerkosaposhkruar, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDetajime", idKonfigambjenti, "LupaDetajimeLidhje.aspx");
            }
            else
            {
                konfiguroPopupGride(false, kerkosaposhkruar, endlessScroll);
            }

            gvLupaDetajime.Columns["#"].VisibleIndex = 0;
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvLupaDetajime, cultinf, rm);
        }

        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            idKonfigambjenti = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idNivel);
        }

        private void mbushPopUpListe(int idPerdoruesi, int idNdermarrje)
        {
            var col = new DbCore.DbInventari.colDetajimeArtikulli();
            var veprimi = -1;
            if (Request.QueryString["veprimi"] != null)
            {
                veprimi = int.Parse(Request.QueryString["veprimi"]);
                col.ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategoriseJoArtikulli(idNdermarrje, idPerdoruesi, veprimi, int.Parse(Request.QueryString["idArtikulli"]));
            }
            else
            {
                col.mbushDetajimeSipasNdermarrjesAndAutorizim(idNdermarrje, idPerdoruesi);
            }
            gvLupaDetajime.DataSource = col;
            gvLupaDetajime.DataBind();
        }

        private void konfiguroPopupGride(bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {
            shto_Lloj();
            shto_Autorizim();
            shto_Kategori();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaDetajime, "gvLupaDetajime", "LupaDetajimeLidhje.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaDetajime, "IdDetajimArtikulli", kerkosaposhkruar, endlessScroll);
        }

        protected void gvDetajimArtikulli_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaDetajime.Columns["#"] == null)
            {
                var check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                gvLupaDetajime.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaDetajime.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);

                gvLupaDetajime.Settings.ShowFilterRow = true;
                gvLupaDetajime.Columns.Add(check);

                gvLupaDetajime.KeyFieldName = "IdDetajimArtikulli";
                gvLupaDetajime.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaDetajime.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        private void shto_Autorizim()
        {
            var visibleindex = gvLupaDetajime.Columns["IdNivelAutorizimi"].VisibleIndex;
            gvLupaDetajime.Columns.Remove(gvLupaDetajime.Columns["IdNivelAutorizimi"]);
            var colnew = new GridViewDataComboBoxColumn();
            var colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka();
            colAutorizim.mbushGjitheAutorizimet(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            colnew.PropertiesComboBox.DataSource = colAutorizim;
            colnew.PropertiesComboBox.TextField = "KodiAutorizim";
            colnew.PropertiesComboBox.ValueField = "IdAutorizimKoka";
            colnew.FieldName = "IdNivelAutorizimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaDetajime.Columns.Add(colnew);
        }

        private void shto_Lloj()
        {
            var visibleindex = gvLupaDetajime.Columns["LlojDetajimArtikulli"].VisibleIndex;
            gvLupaDetajime.Columns.Remove(gvLupaDetajime.Columns["LlojDetajimArtikulli"]);
            var colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Alfanumerik", 1);
            colnew.PropertiesComboBox.Items.Add("Numerik", 2);
            colnew.PropertiesComboBox.Items.Add("Date", 3);

            colnew.VisibleIndex = visibleindex;
            colnew.FieldName = "LlojDetajimArtikulli";
            gvLupaDetajime.Columns.Add(colnew);
        }

        private void shto_Kategori()
        {
            var visibleindex = gvLupaDetajime.Columns["KategoriDetajimi"].VisibleIndex;
            gvLupaDetajime.Columns.Remove(gvLupaDetajime.Columns["KategoriDetajimi"]);
            var colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Detajim 1", 1);
            colnew.PropertiesComboBox.Items.Add("Detajim 2", 2);
            colnew.FieldName = "KategoriDetajimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaDetajime.Columns.Add(colnew);
        }

        protected void gvDetajimArtikulli_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvLupaDetajime, cultinf, rm);
        }

        protected void gvDetajimArtikulli_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }









































































        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaDetajime_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idkomponente = string.Empty;
            var kodkonfigurimi = string.Empty;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == string.Empty)
                {
                    gvLupaDetajime.FilterExpression = string.Empty;
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDetajime", "LupaDetajimeLidhje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaDetajime.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaDetajime);
                    }
                }
            }
            else
            {
                if (arr.Length == 2)
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            }
            gvLupaDetajime.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaDetajimeLidhje.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
            aSPxMenu1.Items.FindByName("Ruaj").ClientVisible = true;
        }


        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDetajime", "LupaDetajimeLidhje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaDetajime.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDetajimArtikulli", gvLupaDetajime);
            //var kolona = gvLupaDetajime.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //    {
            //        filtri.DrejtimRenditje = true;
            //    }
            //    else
            //    {
            //        filtri.DrejtimRenditje = false;
            //    }
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdDetajimArtikulli";
            //    filtri.DrejtimRenditje = true;
            //}
            var oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            var mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDetajime", Convert.ToInt32(cmbKonfigurimi.Value), "LupaDetajimeLidhje.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.Status == true)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            cmbFiltra.Text = string.Empty;
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDetajime", "LupaDetajimeLidhje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                var mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDetajime", Convert.ToInt32(cmbKonfigurimi.Value), "LupaDetajimeLidhje.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                cmbFiltra.Text = string.Empty;
                gvLupaDetajime.FilterExpression = String.Empty;
            }
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                LidhArtikullMeDetajime();
            }
        }

        private void LidhArtikullMeDetajime()
        {
            var mesazh = new DbCore.clsMesazh();
            var idte = gvLupaDetajime.GetSelectedFieldValues("IdDetajimArtikulli");
            var art = new DbCore.DbInventari.clsArtikulli(int.Parse(Request.QueryString["idArtikulli"]));
            for (var i = 0; i < idte.Count; i++)
            {                
                mesazh = DbCore.DbInventari.clsDetajimPerArt.ruajLidhje(art, int.Parse(idte[i].ToString()), int.Parse(Request.QueryString["lloji"]), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
            }

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            mbushPopUpListe(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
    }
}
