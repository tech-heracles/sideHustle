using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaLlojDokumenti : MyPageBase
    {
        private DbCore.DbAdmin.clsLlojDokumenti oLlojDok;
        protected void Page_Load(object sender, EventArgs e)
        {
            var vleraQueryString = string.Empty;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != string.Empty)
            {
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            }
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LlojDok");
            var kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else
                kerkosaposhkruar = false;
            mbushPopUpListeLlojDoks();
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaLlojDok, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaLlojDok", idKonfigambjenti, "LupaLlojDokumenti.aspx");
            }
            else
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            var array = Request.QueryString["array"];
        }
        private void mbushPopUpListeLlojDoks()
        {
            oLlojDok = new DbCore.DbAdmin.clsLlojDokumenti();
            oLlojDok.IdModuli = 7;
            var llojDoks = oLlojDok.merriTeGjithe();

            gvLupaLlojDok.DataSource = llojDoks;
            gvLupaLlojDok.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaLlojDok, "gvLupaLlojDok", "LupaLlojDokumenti.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaLlojDok, "IdLlojDokumenti", kerkosaposhkruar, endlessScroll);
        }

        protected void gvLupaLlojDok_DataBound(object sender, EventArgs e)
        {
            gvLupaLlojDok.Settings.ShowFilterRow = true;
            gvLupaLlojDok.KeyFieldName = "IdLlojDokumenti";
            gvLupaLlojDok.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaLlojDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaLlojDok.Selection.UnselectAll();
        }

        protected void gvLupaLlojDok_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }


        protected void gvLupaLlojDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == string.Empty)
                {
                    gvLupaLlojDok.FilterExpression = string.Empty;
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaLlojDok", "LupaLlojDokumenti.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaLlojDok.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaLlojDok);
                    }
                }
            }
            gvLupaLlojDok.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaLlojDokumenti.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaLlojDok", "LupaLlojDokumenti.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaLlojDok.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdLlojDokumenti", gvLupaLlojDok);
            //var kolona = gvLupaLlojDok.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdLlojDokumenti";
            //    filtri.DrejtimRenditje = true;
            //}
            var oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            var mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaLlojDok", Convert.ToInt32(cmbKonfigurimi.Value), "LupaLlojDokumenti.aspx");
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
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaLlojDok", "LupaLlojDokumenti.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                var mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaLlojDok", Convert.ToInt32(cmbKonfigurimi.Value), "LupaLlojDokumenti.aspx");
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
                gvLupaLlojDok.FilterExpression = String.Empty;
            }
        }
    }
}
