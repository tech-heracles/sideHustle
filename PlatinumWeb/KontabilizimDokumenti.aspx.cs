using System;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Data;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore;
using DbCore.DbKontabiliteti;
using System.Text.RegularExpressions;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class KontabilizimDokumenti : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }

            PercaktoTemplateMenu();

            if (!IsPostBack)
            {
                KonfiguroVleraFillestare();

                MbushGridenElementeshNgaDb();
                KonfiguroGrideElementesh();
                GridUtil.konfigGrideListeEMadhePaTheme(gvKontabilizimDokumenti, "Id");
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "gvKontabilizimDokumenti", gvKontabilizimDokumenti, "KD", "4009", IdGjuha);
            }
            else
            {
                MbushGridenNgaSession();
                KonfiguroGrideElementesh();
            }

            gvKontabilizimDokumenti.PercaktoTitlePanel(this, _menu, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, Convert.ToInt32(cmbKonfigurimi.Value), "KontabilizimDokumenti.aspx", rm, ci);
        }

        private void KonfiguroVleraFillestare()
        {
            ConfigureAspxComboBox.mbushComboGrupKontabilizimi(IdNdermarrja, cmbGrupKontabilizimi);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtNrGrupKontabilizimi);
            ConfigureAspxComboBox.mbushKomboPerdoruesish(IdNdermarrja, btnPerdorues);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 203, "KD", rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;

            var col = new colKonfigurimAmbjenti();
            col.mbushKonfigAmbjentiGjeneruarNgaFK(IdNdermarrja);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategoriseKoontabilizim(txtNrGrupKontabilizimi, col);

            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (periudha != null)
            {
                txtNgaDok.Date = periudha.FillimiPeriudha;
                txtDeriDok.Date = periudha.MbarimiPeriudha;
            }

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbKonfigurimi.Value), IdGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        #region Grid

        private void MbushGridenElementeshNgaDb()
        {
            string datanga;
            string dataderi;
            var oPeriudha = mySessionObjects.merrPeriudheKontabel(Session);
            switch (radDtDok.Value)
            {
                case "Aktuale":
                    datanga = oPeriudha.FillimiPeriudha.ToShortDateString();
                    dataderi = oPeriudha.MbarimiPeriudha.ToShortDateString();
                    break;
                case "VitiUshtrimor":
                    datanga = "01/01/" + oPeriudha.MbarimiPeriudha.Year;
                    dataderi = "31/12/" + oPeriudha.MbarimiPeriudha.Year;
                    break;
                default:
                    datanga = txtNgaDok.Text;
                    dataderi = txtDeriDok.Text;
                    break;
            }

            var col = clsKokaFleteKontabel.ktheGjitheDokumentatFleteKontabelStatusDraft(IdNdermarrja, datanga, dataderi, IdPerdoruesi, txtNumer.Text, cmbGrupKontabilizimi.Text, txtNrGrupKontabilizimi.Text, Convert.ToInt32(btnPerdorues.Value));
            mySessionObjects.ruajObjectNeSesion(Session, col, "gvKontabilizimDokumenti");
            gvKontabilizimDokumenti.DataSource = col;
            gvKontabilizimDokumenti.DataBind();
        }

        private void KonfiguroGrideElementesh()
        {
            KonfigurimComboGride.ShtoGrupetKontabilizimit(gvKontabilizimDokumenti, IdNdermarrja, Session, "KontabilizimDokumenti.aspx", GuidString);
            KonfigurimComboGride.ShtoModelMeDataSource(gvKontabilizimDokumenti, () =>
            {
                var konfigurimet = new colKonfigurimAmbjenti();
                konfigurimet.mbushKonfigAmbjSipasIdKategori(5, IdNdermarrja, IdPerdoruesi, IdGjuha);
                return konfigurimet;
            }, Session, "KontabilizimDokumenti.aspx", GuidString);
        }

        private void MbushGridenNgaSession()
        {
            gvKontabilizimDokumenti.DataSource = mySessionObjects.merrObjectNgaSesioni(Session, "gvKontabilizimDokumenti") as DataTable;
            gvKontabilizimDokumenti.DataBind();
        }

        protected void gvKontabilizimDokumenti_DataBound(object sender, EventArgs e)
        {
            gvKontabilizimDokumenti.KeyFieldName = "Id";
            gvKontabilizimDokumenti.Columns["Id"].Visible = false;
            GridUtil.ShtoCommandColumnNeDatabound(gvKontabilizimDokumenti, "#", "Id");
            gvKontabilizimDokumenti.Settings.ShowFilterRow = true;
            gvKontabilizimDokumenti.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvKontabilizimDokumenti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            MbushGridenElementeshNgaDb();
        }

        protected void gvKontabilizimDokumenti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            GridUtil.GridCustomJsProperties(sender, e, gvKontabilizimDokumenti);
        }

        protected void gvKontabilizimDokumenti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            GridUtil.GridAfterPerformCallback(sender, e, gvKontabilizimDokumenti, _menu);
        }

        protected void gvKontabilizimDokumenti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e) =>
          GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Id", "IdKonfigAmbjente");

        #endregion

        #region Menu

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
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "KontabilizimDokumenti.aspx", this, MenuInfo, null, null, true, false, false, Meme);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Kontabilizo")
            {
                var rreshta = gvKontabilizimDokumenti.GetSelectedFieldValues("Id");
                if (rreshta.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukEshteSelektuarAsnjeRresht", ci), pnlMesazhi);
                    return;
                }

                var mesazh = clsKokaFleteKontabel.UpdateStatusDheDateDokumentaKontabel(string.Join(",", rreshta.ToArray()), txtDateRegjistrimi.Value.ToString(), IdPerdoruesi);
                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    MbushGridenElementeshNgaDb();
                    gvKontabilizimDokumenti.FilterExpression = "";
                }
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kontabilizimi i dokumentave te selektuar nuk u be!", pnlMesazhi);
            }
        }

        #endregion
    }
}
