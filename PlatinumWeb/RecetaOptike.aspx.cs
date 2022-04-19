using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using PlatinumWeb.Templates;
using System.Collections;
using DbCore.DbInventari;
using System.Globalization;
using System.Resources;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore;
using DbCore.DbShare;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DevExpress.Utils.OAuth.Provider;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Filters;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e RecetaOptike
    /// </summary>
    public partial class RecetaOptike : MyPageBase
    {

        string komponente = "RecetaOptike.aspx";
        private int idNdermarrje;
        private string guidString;
        private int idkonfigurimi;
        private int idviti;
        private int idNdermViti;
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {


            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }

            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(IdGjuha, ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), IdPerdoruesi, idNdermarrje);

            if (!IsPostBack)
            {
                idNdermViti = mySessionObjects.ktheNdermarrjeVit(Session);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("IdGjuha", IdGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idNdermarrjeVit", idNdermViti);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, idNdermarrje, cmbKonfigurimi, 161, "LDRO", rm, ci, IdGjuha);
                var konf = new clsKonfigurimAmbjenti();
                cmbKonfigurimi.SelectedIndex = 0;
                konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                gvRecetaOptike.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, 3059, "IdKoka", rm, ci, false);

                mbushGridNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje);

            }
            else
            {

                idNdermarrje = hfState.Get<int>("idNdermarrje");
                idNdermViti = hfState.Get<int>("idNdermarrjeVit");
                guidString = hfState.Get<string>("guidString");

                gvRecetaOptike.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, 3059, "IdKoka", rm, ci, false);
                mbushGridNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje);

            }

            GridUtil.konfigGrideListeEMadhePaTheme(gvRecetaOptike, "IdKoka");
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, "gvRecetaOptike", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
        }




        /// <summary>
        /// perkthen label 
        /// </summary>
        public void perktheLabel()
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
        }

        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="IdGjuha"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int IdGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, mySessionObjects.merrEshteMemeSesioni(Session));

        }

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(IdGjuha, ASPxMenu1, (int)hfState["idViti"], (int)hfState["IdPerdoruesi"], (int)hfState["idNdermarrje"]);
        }


        private void mbushGridNgaSession(int idNdermarrje)
        {
            var periudha = this.MerrPeriudhe(hfState);
            var tmpObject = gvRecetaOptike.MerrDataSourceMePeriduheNeSession<DataTable>(Session, komponente, periudha, guidString);


            if (tmpObject == null)
            {
                mbushGridNgaDB(idNdermarrje);
            }
            else
            {
                gvRecetaOptike.DataSource = tmpObject;
                gvRecetaOptike.DataBind();
                tmpObject.Dispose();
            }
        }


        private void mbushGridNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena            
            var periudha = this.MerrPeriudhe(hfState);
            DataTable dt = colKokaRecetaOptike.MerrRecetaSipasPeriudhes(idNdermarrje, Periudha.DataDokNga, Periudha.DataDokDeri);
            gvRecetaOptike.RuajDataSourceMePeriduheNeSession(Session, komponente, periudha, dt, guidString);
            gvRecetaOptike.DataSource = dt;
            gvRecetaOptike.DataBind();

        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdPerdoruesi"></param>
        private void konfiguroGride(int idNdermarrje)
        {
            gvRecetaOptike.Columns["#"].VisibleIndex = 0;

            KonfigurimComboGride.ShtoStatus(gvRecetaOptike, rm, ci);
            KonfigurimComboGride.ShtoModel(gvRecetaOptike, 160, idNdermarrje, IdPerdoruesi, IdGjuha, Session, komponente, guidString, "IdKonfigAmbjente");
            KonfigurimComboGride.ShtoNivel(gvRecetaOptike, 160, idNdermarrje, IdPerdoruesi, IdGjuha, Session, komponente, guidString);
            KonfigurimComboGride.shtoKlient(gvRecetaOptike, idNdermarrje, IdPerdoruesi, Session, komponente, guidString, "IdKlient");
            GridUtil.konfigGrideListeEMadhePaTheme(gvRecetaOptike, "IdKoka");
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvRecetaOptike", gvRecetaOptike, "LDRO", "3059", IdGjuha);

        }




        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRecetaOptike_DataBound(object sender, EventArgs e)
        {
            if (gvRecetaOptike.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvRecetaOptike.Settings.ShowFilterRow = true;
                gvRecetaOptike.Settings.ShowHeaderFilterButton = true;
                gvRecetaOptike.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRecetaOptike.Settings.ShowFilterRowMenu = true;
                gvRecetaOptike.Columns.Add(check);
                gvRecetaOptike.Settings.ShowGroupPanel = true;
                gvRecetaOptike.KeyFieldName = "IdKoka";
                gvRecetaOptike.SettingsBehavior.AllowSelectByRowClick = true;
                gvRecetaOptike.SettingsBehavior.AllowFocusedRow = true;
            }

        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {

        }




        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {


            if (e.Item.Name == "PrintPreview")
            {
                if (gvRecetaOptike.FocusedRowIndex == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem zgjidhni një faturë për të printuar!", pnlMesazhi);
                    Container.Attributes["src"] = "";
                }
                else
                {
                    int idKoka = int.Parse(gvRecetaOptike.GetRowValues(gvRecetaOptike.FocusedRowIndex, "IdKoka").ToString());
                    int idRaportDesign = clsKokaRecetaOptike.ktheIdRaportDesign(idKoka);
                    int idRaporti = clsRaporti.KtheIdRaporti(IdGjuha, idRaportDesign);

                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + idKoka + "&printo=false&raportdyte=jo&iddesign=" + idRaportDesign;

                }
            }
            //konfiguroGride(idNdermarrje, rm, ci);
        }



        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {

            List<object> rreshtat = gvRecetaOptike.GetSelectedFieldValues("IdKoka");

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
                return;
            }

            List<string> TeFshire = new List<string>(), PeriudheKycur = new List<string>();

            foreach (object id in rreshtat)
            {
                var koka = new clsKokaRecetaOptike(Convert.ToInt32(id));

                var mesazh = koka.Fshi();

                bool ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(koka.DtDok, idNdermarrje);

                if (ekycur)
                {
                    PeriudheKycur.Add(koka.NrDok);
                    continue;
                }

                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(koka.IdKoka);
                    #endregion
                    TeFshire.Add(koka.NrDok);
                }

            }

        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdPerdoruesi"></param>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void hiqNgaGrida(int idkoka)
        {
            if (this.gvRecetaOptike.DataSource != null)
            {
                DataTable dt = (DataTable)gvRecetaOptike.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimiNdodhen2Receta", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvRecetaOptike.DataSource = dt;
                gvRecetaOptike.DataBind();
                dt.Dispose();
            }

            else
            {
                mbushGridNgaDB(idNdermarrje);
            }
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRecetaOptike_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRecetaOptike_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (gvRecetaOptike.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;

        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRecetaOptike_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvRecetaOptike.PageIndex;
            e.Properties["cpPageRow"] = gvRecetaOptike.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvRecetaOptike.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRecetaOptike_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRecetaOptike_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            //if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Klienti")
            //{
            //    e.Values.Clear();
            //    e.AddValue("(Te gjithe)", string.Empty, "true");
            //    e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
            //    e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
            //    e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
            //    e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
            //    e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
            //    e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
            //    e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            //}
            //else
            //{
            //    e.Values.Clear();
            //    e.AddValue("(Te gjithe)", string.Empty, "true");
            //}
        }

        protected void ButtonOk2_Click2(object sender, EventArgs e)
        {

        }
    }
}