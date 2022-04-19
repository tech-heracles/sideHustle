using System;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Data;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKartaKlienti : MyPageBase
    {
        private string _klienti = "";
        private const string KomponenteEmri = "LupaKartaKlienti.aspx";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
            _klienti = Request.QueryString["idKlienti"];
            if (_klienti == "null")
                _klienti = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaKartaKlienti", 1, KomponenteEmri);

            if (!IsPostBack)
            {
                MbushPopUpListeNgaDb(Convert.ToInt32(_klienti));
                GridUtil.AplikoFilterDefault(gvLupaKartaKlienti, 1);
                KonfiguroPopupGride();
            }
            else
            {
                MbushPopUpListeNgaSession();
            }
        }

        private void MbushPopUpListeNgaSession()
        {
            mySessionObjects.merrGrideNgaSessioniLupa(Session, out var tmpObject);
            if (tmpObject == null)
            {
                MbushPopUpListeNgaDb(Convert.ToInt32(_klienti));
                return;
            }

            gvLupaKartaKlienti.DataSource = tmpObject;
            gvLupaKartaKlienti.DataBind();
        }

        private void MbushPopUpListeNgaDb(int idKlienti)
        {
            var dt = idKlienti == 0
                ? colKarta.MerrKartatSipasNdermarrjes(IdNdermarrja) 
                : colKarta.MerrKartatSipasKlientit(IdNdermarrja, idKlienti);

            mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaKartaKlienti.DataSource = dt;
            gvLupaKartaKlienti.DataBind();
            dt.Dispose();
        }

        private void KonfiguroPopupGride()
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaKartaKlienti, "gvLupaKartaKlienti", "LupaKartaKlienti.aspx", 1, true, IdGjuha);
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("KK", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKartaKlienti, "IdKarta", true, endlessScroll);
        }

        protected void gvLupaKartaKlienti_DataBound(object sender, EventArgs e)
        {
            gvLupaKartaKlienti.Settings.ShowFilterRow = true;
            gvLupaKartaKlienti.KeyFieldName = "IdKarta";
            gvLupaKartaKlienti.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaKartaKlienti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaKartaKlienti.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        protected void gvLupaKartaKlienti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var hfStatusi = new HiddenField();
            GridUtil.GridCustomCallbackDefault(sender, e, gvLupaKartaKlienti, KomponenteEmri, IdNdermarrja, IdGjuha, null, ref hfStatusi);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, KomponenteEmri, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme);
            ASPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {            
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };

            var koka = new clsGridaKoka(IdGjuha, "gvLupaKartaKlienti", KomponenteEmri, IdNdermarrja);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKartaKlienti.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiBanka", gvLupaKartaKlienti);
            //var kolona = gvLupaKartaKlienti.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodiBanka";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaKartaKlienti", 1, KomponenteEmri);
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(IdGjuha, "gvLupaKartaKlienti", KomponenteEmri, IdNdermarrja);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                var mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaKartaKlienti", 1, KomponenteEmri);
                PercaktoTemplateMenu();

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaKartaKlienti.FilterExpression = string.Empty;
            }
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }
    }
}