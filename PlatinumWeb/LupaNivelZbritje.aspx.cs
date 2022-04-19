using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using DbCore.DbShare;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaNivelZbritje : MyPageBase
    {
        public static int idNdermVit = -1;
        private DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        protected void Page_Load(object sender, EventArgs e)
        {
            var array = Request.QueryString["array"];

            var vleraQueryString = string.Empty;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != string.Empty)
            {
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            }
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "NivZb");
            var kerkosaposhkruar = true;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else
                kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvNivelZbritje, idKonfigambjenti);
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivZb", idKonfigambjenti, "LupaNivelZbritje.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            }
        }
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeNgaDB();
            }
            else
            {
                gvNivelZbritje.DataSource = tmpObject;
                gvNivelZbritje.DataBind();
            }
        }
        private void mbushPopUpListeNgaDB()
        {
            var oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            var colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            var dt = DbCore.DbInventari.colNiveleZbritjesh.merrNiveleZbritjeshNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvNivelZbritje.DataSource = dt;


            gvNivelZbritje.DataBind();
            dt.Dispose();
        }
        private void mbushPopUpListeNiveleZbritje()
        {
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            var colNiveleZbritjesh = new DbCore.DbInventari.colNiveleZbritjesh();
            colNiveleZbritjesh.mbushGjitheNiveleZbritjeshSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            this.gvNivelZbritje.DataSource = colNiveleZbritjesh;
            gvNivelZbritje.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {
            shtoPrioritet();
            shtoPrind();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvNivelZbritje, "gvLupaNivZb", "LupaNivelZbritje.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvNivelZbritje, "IdNivelZbritje", kerkosaposhkruar, endlessScroll);
        }

        private void shtoPrind()
        {
            var visibleindex = gvNivelZbritje.Columns["IdPrindi"].VisibleIndex;
            gvNivelZbritje.Columns.Remove(gvNivelZbritje.Columns["IdPrindi"]);

            var colnew = new GridViewDataComboBoxColumn();
            var nivelet = new DbCore.DbInventari.colNiveleZbritjesh();
            nivelet.mbushGjitheNiveleZbritjeshSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            var niveli = new DbCore.DbInventari.clsNivelZbritje();
            niveli.IdNivelZbritje = 0;
            nivelet.Add(niveli);
            colnew.PropertiesComboBox.DataSource = nivelet;
            colnew.PropertiesComboBox.TextField = "PershkrimNivelZbritje";
            colnew.PropertiesComboBox.ValueField = "IdNivelZbritje";
            colnew.FieldName = "IdPrindi";
            colnew.VisibleIndex = visibleindex;
            gvNivelZbritje.Columns.Add(colnew);
        }

        private void shtoPrioritet()
        {
            var visibleindex = gvNivelZbritje.Columns["PrioritetiNivelZbritje"].VisibleIndex;
            gvNivelZbritje.Columns.Remove(gvNivelZbritje.Columns["PrioritetiNivelZbritje"]);
            var colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("0", 0);
            colnew.PropertiesComboBox.Items.Add("1", 1);
            colnew.PropertiesComboBox.Items.Add("2", 2);
            colnew.PropertiesComboBox.Items.Add("3", 3);
            colnew.PropertiesComboBox.Items.Add("4", 4);
            colnew.PropertiesComboBox.Items.Add("5", 5);
            colnew.PropertiesComboBox.Items.Add("6", 6);
            colnew.PropertiesComboBox.Items.Add("7", 7);
            colnew.PropertiesComboBox.Items.Add("8", 8);
            colnew.PropertiesComboBox.Items.Add("9", 9);
            colnew.PropertiesComboBox.Items.Add("10", 10);
            colnew.PropertiesComboBox.Items.Add("11", 11);
            colnew.PropertiesComboBox.Items.Add("12", 12);
            colnew.PropertiesComboBox.Items.Add("13", 13);
            colnew.PropertiesComboBox.Items.Add("14", 14);
            colnew.PropertiesComboBox.Items.Add("15", 15);
            colnew.PropertiesComboBox.Items.Add("16", 16);
            colnew.PropertiesComboBox.Items.Add("17", 17);
            colnew.PropertiesComboBox.Items.Add("18", 18);
            colnew.PropertiesComboBox.Items.Add("19", 19);
            colnew.PropertiesComboBox.Items.Add("20", 20);
            colnew.FieldName = "PrioritetiNivelZbritje";
            colnew.VisibleIndex = visibleindex;
            gvNivelZbritje.Columns.Add(colnew);
        }

        protected void gvNivelZbritje_DataBound(object sender, EventArgs e)
        {
            gvNivelZbritje.Settings.ShowFilterRow = true;
            gvNivelZbritje.KeyFieldName = "IdNivelZbritje";
            gvNivelZbritje.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvNivelZbritje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvNivelZbritje.Selection.UnselectAll();
        }

        protected void gvNivelZbritje_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }
        

        protected void gvNivelZbritje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == string.Empty)
                {
                    gvNivelZbritje.FilterExpression = string.Empty;
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivZb", "LupaNivelZbritje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvNivelZbritje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvNivelZbritje);
                    }
                }
            }
            gvNivelZbritje.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaNivelZbritje.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
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
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivZb", "LupaNivelZbritje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvNivelZbritje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivelZbritje", gvNivelZbritje);
            //var kolona = gvNivelZbritje.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivelZbritje";
            //    filtri.DrejtimRenditje = true;
            //}
            var oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            var mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivZb", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNivelZbritje.aspx");
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
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNivZb", "LupaNivelZbritje.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                var mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNivZb", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNivelZbritje.aspx");
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
                gvNivelZbritje.FilterExpression = String.Empty;
            }
        }
    }
}
