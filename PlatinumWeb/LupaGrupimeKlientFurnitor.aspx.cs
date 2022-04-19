using System;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaGrupimeKlientFurnitor : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];

            PercaktoTemplateMenu();

            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "GrupKF");
            var kerkosaposhkruar = DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po";
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            hfState.Set("idNdermarrje", IdNdermarrja);
            MbushPopUpListeKodifikimi();
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaGrupKlientFurnitor, idKonfigambjenti);
                KonfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaGrupKlientFurnitor", idKonfigambjenti, "LupaGrupimeKlientFurnitor.aspx");
            }
            else
                KonfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, endlessScroll);
        }

        private void KonfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaGrupKlientFurnitor, "gvLupaGrupKlientFurnitor", "LupaGrupimeKlientFurnitor.aspx", idKonfigambjenti, visibleIndex, IdGjuha);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaGrupKlientFurnitor, "IdGrupi", kerkosaposhkruar, endlessScroll);
        }

        private void MbushPopUpListeKodifikimi()
        {
            var grupet = new DbCore.DbKontabiliteti.colGrupeKF();
            int llojikf;
            switch (Request.QueryString["kf"])
            {
                case "klient":
                    llojikf = 0;
                    break;
                case "furnitor":
                    llojikf = 1;
                    break;
                default:
                    llojikf = -1;
                    break;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["llojKodifikimi"]))
                grupet.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(int.Parse(Request.QueryString["llojKodifikimi"]), IdNdermarrja, llojikf);
            else
                grupet.MerrGrupeKfSipasLlojitKf(IdNdermarrja, llojikf);

            if (llojikf == -1)
                grupet.MerrGrupeKfSipasLlojit(int.Parse(Request.QueryString["llojKodifikimi"]), IdNdermarrja);

            gvLupaGrupKlientFurnitor.DataSource = grupet;
            gvLupaGrupKlientFurnitor.DataBind();

            ShtoPrind(llojikf, int.Parse(Request.QueryString["llojKodifikimi"]));
        }

        private void ShtoPrind(int llojikf, int llojkodifikimi)
        {
            gvLupaGrupKlientFurnitor.Columns.Remove(gvLupaGrupKlientFurnitor.Columns["IdPrindi"]);

            
            var grupet = new DbCore.DbKontabiliteti.colGrupeKF();

            if (llojkodifikimi != 0)
                grupet.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(llojkodifikimi, IdNdermarrja, llojikf);
            else
                grupet.MerrGrupeKfSipasLlojitKf(IdNdermarrja, llojikf);

            if (llojikf == -1)
                grupet.MerrGrupeKfSipasLlojit(llojkodifikimi, IdNdermarrja);

            grupet.Add(new DbCore.DbKontabiliteti.clsGrupeKF { IdGrupi = 0 });

            var colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.DataSource = grupet;
            colnew.PropertiesComboBox.TextField = "PershkrimGrupi";
            colnew.PropertiesComboBox.ValueField = "IdGrupi";
            colnew.FieldName = "IdPrindi";
            gvLupaGrupKlientFurnitor.Columns.Add(colnew);
        }

        protected void gvLupaGrupKlientFurnitor_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaGrupKlientFurnitor.Selection.UnselectAll();
        }

        protected void gvLupaGrupKlientFurnitor_DataBound(object sender, EventArgs e)
        {
            gvLupaGrupKlientFurnitor.Settings.ShowFilterRow = true;
            gvLupaGrupKlientFurnitor.KeyFieldName = "IdGrupi";
            gvLupaGrupKlientFurnitor.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaGrupKlientFurnitor_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        protected void gvLupaGrupKlientFurnitor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaGrupKlientFurnitor.FilterExpression = "";
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvLupaGrupKlientFurnitor", "LupaGrupimeKlientFurnitor.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaGrupKlientFurnitor.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaGrupKlientFurnitor);
                    }
                }
            }

            if (string.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaGrupKlientFurnitor.Selection.UnselectAll();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "LupaGrupimeKlientFurnitor.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme);
            ASPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var cmbFiltra = ((MenuFilter)ASPxMenu1.Items.FindByName("TemplatedItemFilter").Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new DbCore.DbAdmin.clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false
            };

            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvLupaGrupKlientFurnitor", "LupaGrupimeKlientFurnitor.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaGrupKlientFurnitor.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdGrupi", gvLupaGrupKlientFurnitor);
            //var kolona = gvLupaGrupKlientFurnitor.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdGrupi";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;
            filtri.IdStatusDok = 1;

            var mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaGrupKlientFurnitor", Convert.ToInt32(cmbKonfigurimi.Value), "LupaGrupimeKlientFurnitor.aspx");
            PercaktoTemplateMenu();
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var cmbFiltra = ((MenuFilter)ASPxMenu1.Items.FindByName("TemplatedItemFilter").Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvLupaGrupKlientFurnitor", "LupaGrupimeKlientFurnitor.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = IdPerdoruesi;
                var mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaGrupKlientFurnitor", Convert.ToInt32(cmbKonfigurimi.Value), "LupaGrupimeKlientFurnitor.aspx");
                PercaktoTemplateMenu();

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaGrupKlientFurnitor.FilterExpression = String.Empty;
            }
        }
    }
}