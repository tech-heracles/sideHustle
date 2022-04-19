using System;
using DevExpress.Web;
using System.Web.UI.WebControls;
using DbCore;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaKategoriShpenzimi : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != string.Empty)
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];
            else
                vleraQueryString = string.Empty;

            PercaktoTemplateMenu();
            var idKonfigAmbjenti = clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "LKSH");
            var kerkosaposhkruar = DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "KSSH") == "Po";
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po";
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                MbushPopUpListeNgaDb();
                KonfiguroPopupGride(idKonfigAmbjenti, true, kerkosaposhkruar, endlessScroll);
                GridUtil.AplikoFilterDefault(gvKategoria, idKonfigAmbjenti);
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKategoria", idKonfigAmbjenti, "LupaKategoriShpenzimi.aspx");
            }
            else
            {
                MbushPopUpListeNgaSession();
                KonfiguroPopupGride(idKonfigAmbjenti, false, kerkosaposhkruar, endlessScroll);
            }
        }

        private void MbushPopUpListeNgaSession()
        {
            object tmpObject;
            mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                MbushPopUpListeNgaDb();
                return;
            }
            gvKategoria.DataSource = tmpObject;
            gvKategoria.DataBind();
        }

        private void MbushPopUpListeNgaDb()
        {
            var dt = DbCore.DbKontabiliteti.colKategoriShpenzimi.MerrKategorineNdermarjeDt(IdNdermarrja, Request.QueryString["lloji"] != null);
            mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvKategoria.DataSource = dt;
            gvKategoria.DataBind();
        }

        private void KonfiguroPopupGride(int idKonfigAmbjenti, bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvKategoria, "gvKategoria", "LupaKategoriShpenzimi.aspx", idKonfigAmbjenti, visibleIndex, IdGjuha);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvKategoria, "Id", kerkosaposhkruar, endlessScroll);
        }

        protected void gvKategoria_DataBound(object sender, EventArgs e)
        {
            
            if (gvKategoria.Columns["#"] == null)
            {
                gvKategoria.Columns.Add(new GridViewCommandColumn("#")
                {
                    ShowSelectCheckbox = true,
                    Width = Unit.Percentage(2),
                    VisibleIndex = 0
                });
            }

            gvKategoria.Settings.ShowFilterRow = true;
            gvKategoria.KeyFieldName = "Id";
            gvKategoria.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvKategoria_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvKategoria.Selection.UnselectAll();
        }

        protected void gvKategoria_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKategoria.PageIndex;
            e.Properties["cpPageRow"] = gvKategoria.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKategoria.VisibleRowCount;
        }

        protected void gvKategoria_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == string.Empty)
                {
                    gvKategoria.FilterExpression = string.Empty;
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvKategoria", "LupaKategoriShpenzimi.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKategoria.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKategoria);
                    }
                }
            }

            gvKategoria.Selection.UnselectAll();
        }
        
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "LupaKategoriShpenzimi.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme);
            ASPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new DbCore.DbAdmin.clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };
            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvKategoria", "LupaKategoriShpenzimi.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKategoria.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Id", gvKategoria);
            //var kolona = gvKategoria.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Id";
            //    filtri.DrejtimRenditje = true;
            //}
            
            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKategoria", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKategoriShpenzimi.aspx");
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = string.Empty;
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvKategoria", "LupaKategoriShpenzimi.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi == null) return;

            filtra.IdPerdoruesi = IdPerdoruesi;

            var mesazh = filtra.fshi();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKategoria", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKategoriShpenzimi.aspx");
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = string.Empty;
            gvKategoria.FilterExpression = string.Empty;
        }
    }
}
