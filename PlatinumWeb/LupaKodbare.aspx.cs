using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using DbCore;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class LupaKodbare : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];
            int idKonfigAmbjenti = clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "KODBAR");

            if (!Page.IsCallback)
            {
                MbushPopUpListeKodbaresh();
                KonfiguroPopupGride(idKonfigAmbjenti);
            }

            PercaktoTemplateMenu();

            if (!IsPostBack)
            {
                GridUtil.AplikoFilterDefault(gvLupaKodbar, idKonfigAmbjenti);
            }
        }

        /// <summary>
        /// Mbush griden e popupit me te dhena
        /// </summary>
        private void MbushPopUpListeKodbaresh()
        {
            colKodbare colKodbareArtikulli = new colKodbare();

            if (!string.IsNullOrEmpty(Request.QueryString["mbushNgaArtikulli"]) && !string.IsNullOrEmpty(Request.QueryString["idartikulli"]) && Request.QueryString["mbushNgaArtikulli"] == "true")
                colKodbareArtikulli = new colKodbare(System.Convert.ToInt32(Request.QueryString["idartikulli"]));
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["value"]))
                {
                    var serializusi = new JavaScriptSerializer();
                    var kodbaret = (object[])serializusi.DeserializeObject(Request.QueryString["value"]);
                    colKodbareArtikulli = new colKodbare(kodbaret);
                }
            }

            int countKodbarePerTeShtuar = (colKodbareArtikulli.Count > 0) ? 1 : 5;
            ShtoKodbareNeCollectionSipasCounter(countKodbarePerTeShtuar, ref colKodbareArtikulli);

            gvLupaKodbar.DataSource = colKodbareArtikulli;
            gvLupaKodbar.DataBind();
        }

        private void ShtoKodbareNeCollectionSipasCounter(int counter, ref colKodbare colKodbareArtikulli)
        {
            for (int i = 0; i < counter; i++)
                colKodbareArtikulli.Add(new clsKodbari());
        }

        /// <summary>
        /// Konfiguron popupgriden
        /// </summary>
        private void KonfiguroPopupGride(int idKonfigambjenti)
        {
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvLupaKodbar, "gvLupaKodbar", "LupaKodbare.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvLupaKodbar, "IdKodbari");
            gvLupaKodbar.Settings.UseFixedTableLayout = false;
            PercaktoTemplateKodbari();
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
               gvLupaKodbar.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
        }

        /// <summary>
        /// Percaktohen templatet per fushat e grides
        /// </summary>
        private void PercaktoTemplateKodbari()
        {
            var col0 = gvLupaKodbar.Columns[MessagesResource.Messages["lblFshiBtn"]] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 0;

            var col1 = gvLupaKodbar.Columns["Pershkrimi"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTextTemplate();

            var col2 = gvLupaKodbar.Columns["Njesia"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyComboTemplate();

            var col3 = gvLupaKodbar.Columns["Detajim1"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyComboTemplate();

            var col4 = gvLupaKodbar.Columns["Detajim2"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyComboTemplate();

        }

        protected void gvLupaKodbar_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) =>
            gvLupaKodbar.DataBind();

        /// <summary>
        /// Kur grida ben callback te ruajme te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKodbar_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int key = -1;

            if (e.Parameters != "")
            {
                key = int.Parse(e.Parameters);
            }

            var kodbare = new colKodbare();
            var serializusi = new JavaScriptSerializer();
            var kodbaret = (object[])serializusi.DeserializeObject(hfKodbare.Value);

            foreach (var kodbar in kodbaret)
            {
                var kodbari = new clsKodbari();
                if (((Dictionary<string, object>)kodbar)["pershkrimi"] != null
                    && ((Dictionary<string, object>)kodbar)["pershkrimi"].ToString() != "")
                {
                    kodbari.Pershkrimi = (((Dictionary<string, object>)(kodbar)))["pershkrimi"].ToString();
                }

                if (((Dictionary<string, object>)kodbar)["njesia"] != null
                    && ((Dictionary<string, object>)kodbar)["njesia"].ToString() != "")
                {
                    kodbari.Njesia = Convert.ToInt32((((Dictionary<string, object>)(kodbar)))["njesia"].ToString());
                    var detajimi1 = ((Dictionary<string, object>)kodbar)["detajimi1"].ToString();
                    var detajimi2 = ((Dictionary<string, object>)kodbar)["detajimi2"].ToString();
                    kodbari.Detajim1 = int.Parse(detajimi1);
                    kodbari.Detajim2 = int.Parse(detajimi2);
                }

                kodbare.Add(kodbari);
            }

            var kodbarnew = new clsKodbari();
            kodbare.Add(kodbarnew);

            if (key != -1)
            {
                if (key < kodbare.Count)
                {
                    kodbare.RemoveAt(key);
                    if (key == kodbare.Count)
                        kodbare.Add(new clsKodbari());
                }
            }

            if (kodbare.Count == 0)
            {
                for (int i = 0; i < gvLupaKodbar.VisibleRowCount; i++)
                    kodbare.Add(new clsKodbari());
            }

            gvLupaKodbar.DataSource = kodbare;
            gvLupaKodbar.DataBind();
            PercaktoTemplateKodbari();
        }

        protected void gvLupaKodbar_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpNoRows"] = gvLupaKodbar.VisibleRowCount;

        /// <summary>
        /// Shton butonin fshi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKodbar_DataBound(object sender, EventArgs e)
        {
            if (gvLupaKodbar.Columns["Fshi"] != null) return;

            gvLupaKodbar.Columns.Add(new GridViewDataTextColumn
            {
                Caption = MessagesResource.Messages["lblFshiBtn"],
                Width = 50
            });

            gvLupaKodbar.KeyFieldName = "IdKodbari";
            gvLupaKodbar.SettingsBehavior.AllowSelectByRowClick = false;
            gvLupaKodbar.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// Krijon rreshat sipas modelit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKodbar_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                var col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                var col1 = ((ASPxGridView)sender).Columns["Pershkrimi"] as GridViewDataTextColumn;
                var col2 = ((ASPxGridView)sender).Columns["Njesia"] as GridViewDataTextColumn;
                var col3 = gvLupaKodbar.Columns["Detajim1"] as GridViewDataTextColumn;
                var col4 = gvLupaKodbar.Columns["Detajim2"] as GridViewDataTextColumn;
                var btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxTextBox;
                var cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                var cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cmbBox") as ASPxComboBox;
                var cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "cmbBox") as ASPxComboBox;

                if (cmb1 != null)
                {
                    cmb1.Items.Add("Njesia 1", 1);
                    cmb1.Items.Add("Njesia 2", 2);
                }
                
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex + ");}";
                }
                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "txtPershkrimi" + e.VisibleIndex;
                    txt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedPershkrimi(txtPershkrimi" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                    txt1.ClientSideEvents.GotFocus = "function (s,e){SetKey(" + e.VisibleIndex + ");}";

                    
                }

                if (cmb1 != null)
                {
                    cmb1.ClientInstanceName = "cmbNjesia" + e.VisibleIndex;
                    cmb1.ClientSideEvents.SelectedIndexChanged = "function(s,e){TextChangedNjesia(cmbNjesia" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                }

                if (cmb2 != null)
                {
                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb2);
                    cmb2.ClientInstanceName = "cmbDetajimi1t" + e.VisibleIndex;
                    cmb2.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickDet1(cmbDetajimi1t" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                    cmb2.EnableCallbackMode = false;

                    var col = new colDetajimeArtikulli();

                    if (!string.IsNullOrEmpty(Request.QueryString["idartikulli"]))
                        col.ktheDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(int.Parse(Request.QueryString["idartikulli"]), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), 1);

                    cmb2.DataSource = col;
                    cmb2.ValueField = "IdDetajimArtikulli";
                    cmb2.TextField = "KodDetajimArtikulli";
                    cmb2.DataBind();

                }

                if (cmb3 != null)
                {
                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                    cmb3.ClientInstanceName = "cmbDetajimi2t" + e.VisibleIndex;
                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickDet2(cmbDetajimi2t" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                    cmb3.EnableCallbackMode = false;

                    var col = new colDetajimeArtikulli();

                    if (!string.IsNullOrEmpty(Request.QueryString["idartikulli"]))
                        col.ktheDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(int.Parse(Request.QueryString["idartikulli"]), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), 2);

                    cmb3.DataSource = col;
                    cmb3.ValueField = "IdDetajimArtikulli";
                    cmb3.TextField = "KodDetajimArtikulli";
                    cmb3.DataBind();
                }
            }
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "LupaKodbare.aspx", this, MenuInfo, true, false, false, Meme);
            ASPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }
    }
}
