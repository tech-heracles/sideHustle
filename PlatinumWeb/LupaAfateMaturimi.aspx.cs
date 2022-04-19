using System;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaAfateMaturimi : MyPageBase
    {
        private static string Komponente => "LupaAfateMaturimi.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            var vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];

            MbushPopUpListe();

            var idKonfigambjenti = clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "AftMat");

            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaAfatMaturimi, idKonfigambjenti);
                KonfiguroPopupGride(idKonfigambjenti, true);
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaAfatMaturimi", idKonfigambjenti, Komponente);
            }
            else
            {
                KonfiguroPopupGride(idKonfigambjenti, false);
            }
        }

        #region Menu

        protected void Menu_ItemClick(object sender, MenuItemEventArgs e)
        {

        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuinfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, Komponente, this, menuinfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme);
            menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var hiddenField = new HiddenField();
            menu_msg_Frame.RuajFilter(gvLupaAfatMaturimi, Komponente, Convert.ToInt32(cmbKonfigurimi.Value), ref hiddenField);
        }

        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var hiddenField = new HiddenField();
            menu_msg_Frame.FshiFilter(gvLupaAfatMaturimi, Komponente, Convert.ToInt32(cmbKonfigurimi.Value), ref hiddenField);
        } 

        #endregion

        #region Grid

        private void MbushPopUpListe()
        {
            var veprimi = Request.QueryString["veprimi"];
            var col = veprimi != null
                ? new colMaturimet(veprimi != "1", IdNdermarrja)
                : new colMaturimet(IdNdermarrja);
            gvLupaAfatMaturimi.DataSource = col;
            gvLupaAfatMaturimi.DataBind();
        }

        private void KonfiguroPopupGride(int idKonfigambjenti, bool visibleindex)
        {
            KonfigurimComboGride.ShtoDateFillimi(gvLupaAfatMaturimi);
            KonfigurimComboGride.ShtoPeriudhe(gvLupaAfatMaturimi);
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(gvLupaAfatMaturimi, rm, ci, "LlojMaturimi");
            var kerkosaposhkruar = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po";
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAfatMaturimi, "gvLupaAfatMaturimi", Komponente, idKonfigambjenti, visibleindex, IdGjuha);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAfatMaturimi, "IdMaturimi", kerkosaposhkruar, endlessScroll);
        }

        protected void gvLupaAfatMaturimi_DataBound(object sender, EventArgs e)
        {
            gvLupaAfatMaturimi.Settings.ShowFilterRow = true;
            gvLupaAfatMaturimi.KeyFieldName = "IdMaturimi";
            gvLupaAfatMaturimi.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaAfatMaturimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaAfatMaturimi.Selection.UnselectAll();
        }

        protected void gvLupaAfatMaturimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaAfatMaturimi.PageIndex;
            e.Properties["cpPageRow"] = gvLupaAfatMaturimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaAfatMaturimi.VisibleRowCount;
        }

        protected void gvLupaAfatMaturimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var hiddenField = new HiddenField();
            GridUtil.GridCustomCallbackDefault(sender, e, gvLupaAfatMaturimi, Komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hiddenField);
        } 

        #endregion
    }
}
