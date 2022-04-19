using DbCore;
using DbCore.DbAdmin;
using DbCore.DbGIS;
using DevExpress.Web;
using System;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb
{
    public partial class GISLupaKerkimHapsinor : MyPageBase
    {
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        // private int idKonfigambjenti;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);

                idGjuha = mySessionObjects.ktheGjuhe(Session);

                hfState["idGjuha"] = idGjuha;
                hfState["idNdermarrje"] = idNdermarrje;
                hfState["idPerdoruesi"] = idPerdoruesi;
                hfState["idViti"] = idViti;
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idViti = (int)hfState["idViti"];
            }

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            KonfiguroDataSourceGride(rm,ci);
            if (!IsPostBack)
            {
                shtolayers();
                shtoKolonStatusi(ci, rm);
            }
            PerkthimeNgaResourceManager(ci, rm);
            gvLupaKerko.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, 1, "GISLupaKerko.aspx", 2027, "gid", rm, ci);
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci">Merr culture info perkatese</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void PerkthimeNgaResourceManager(CultureInfo ci, ResourceManager rm)
        {
            btnKerko.Text = rm.GetString("ReportToolbarButtonSearch", ci);
            gvLupaKerko.SettingsText.EmptyDataRow = rm.GetString("lblNukKaTeDhena", ci);
            gvLupaKerko.SettingsText.FilterBarCreateFilter = rm.GetString("grideGISKerkimCreateFilter", ci);
            gvLupaKerko.SettingsText.FilterBarClear = rm.GetString("grideGISKerkimClearFilter", ci);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "GISLupaKerkimHapsinor.aspx", this, MenuInfo, null, null, true, false, false, false, Request.QueryString["theme"] != null && Request.QueryString["theme"] == "Moderno" ? true : false);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        private void mbushGriden(int idPerdoruesi, int idNermarrje)
        {
        }

        private void AplikoGjeresiStandarte(ASPxGridView grida)
        {
            double gjeresi = 100 / grida.VisibleColumns.Count;

            for (int i = 0; i < grida.VisibleColumns.Count; i++)
            {
                if ( i==0 )
                    grida.VisibleColumns[i].Width = Unit.Percentage(5);
                else if ( i==3 )
                    grida.VisibleColumns[i].Width = Unit.Percentage(40);
                else
                    grida.VisibleColumns[i].Width = Unit.Percentage(gjeresi);
            }
        }

        protected void gvLupaKerko_DataBound(object sender, EventArgs e)
        {
            if (gvLupaKerko.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                gvLupaKerko.Columns.Add(check);
                check.VisibleIndex = 0;
                check.Width = Unit.Pixel(15);

                check.ShowSelectCheckbox = true;
            }
            gvLupaKerko.SettingsBehavior.AllowSelectByRowClick = true;

            shtolayers();
        }

        protected void gvLupaKerko_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //gvLupaKerko.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            //percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaKerko_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void gvLupaKerko_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaKerko.PageIndex;
            e.Properties["cpPageRow"] = gvLupaKerko.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaKerko.VisibleRowCount;
        }

        protected void callBackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //gvLupaKerko.FilterExpression = e.Parameter;

            // KonfiguroDataSourceGride();
        }

        private void KonfiguroDataSourceGride(ResourceManager rm, CultureInfo ci)
        {
            if (Request.QueryString["bBox"] != null)
            {
                string boundBox = Request.QueryString["bBox"];
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idViti = mySessionObjects.ktheIdVitNdermarrje(Session);

                if (!IsPostBack)
                {
                    object ds = colLidhjeObjekteWebGis.MerrObjekteSipasBoundBoxit(idNdermarrje, boundBox, idPerdoruesi, idViti, idGjuha);
                    gvLupaKerko.DataSource = ds;
                    gvLupaKerko.DataBind();
                    mySessionObjects.ruajGrideNeSessionLupa(Session, ds);
                }
                else
                {
                    object ds = null;

                    mySessionObjects.merrGrideNgaSessioniLupa(Session, out ds);
                    if (ds != null)
                    {
                        gvLupaKerko.DataSource = ds;
                        gvLupaKerko.DataBind();
                    }
                }
            }
            if (gvLupaKerko.Columns.Count > 0)
            {
                GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKerko, "gid", true, false);
                AplikoGjeresiStandarte(gvLupaKerko);
            }
        }

        private void shtolayers()
        {

            gvLupaKerko.KonfiguroCombo("id_layer", "IdType_IdLayer", "Title", () => {
                using (clsDatabaseGIS db = new clsDatabaseGIS())
                {
                    return db.merrLlojLayeriPerCombo(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheGjuhe(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdVitNdermarrje(Session), false);
                }
            }
            , Session, "GISLupaKerkimHapsinor.aspx", string.Empty);

            var colLayer = gvLupaKerko.Columns["id_layer"];
            if (colLayer.Visible) colLayer.Caption = "Layer";
            gvLupaKerko.Columns["gid"].Visible = false;
            gvLupaKerko.Columns["IDLAYER"].Visible = false;
            gvLupaKerko.Columns["the_geom"].Visible = false;
        }

        private void shtoKolonStatusi(CultureInfo ci, ResourceManager rm)
        {
            GridViewDataComboBoxColumn newCol = new GridViewDataComboBoxColumn();

            var colStatusi = gvLupaKerko.Columns["Statusi"];
            if (colStatusi.Visible)//rasti per layerat qe kane statuse,perjashtohen layarat pa status
            {
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisTeGjitha", ci), null);
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisSaktësuar", ci), 1);
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisPasaktësuar", ci), 2);
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisPlanifikuar", ci), 3);

                newCol.PropertiesComboBox.ValueType = typeof(Int32);
                newCol.FieldName = "Statusi";
                newCol.Caption = "Status";
                newCol.VisibleIndex = colStatusi.VisibleIndex;

                gvLupaKerko.Columns.Remove(colStatusi);
                gvLupaKerko.Columns.Add(newCol);
            }
        }

        protected void gvLupaKerko_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {/*
            ASPxTextBox editor = e.Editor as ASPxTextBox;
            if (editor != null)
            {
                editor.ClientSideEvents.Init = "function(s, e)  {  s.ValueChanged.ClearHandlers(); s.KeyDown.ClearHandlers();   s.KeyDown.AddHandler( function(s, e) {  if(e.htmlEvent.keyCode ==13) ASPxClientUtils.PreventEventAndBubble(e.htmlEvent); } ) ;  } ";
                editor.ClientInstanceName = "filterRow_" + e.Column.FieldName;
            }*/
        }

        private void mbushComboFiltra(string idLayerType, CultureInfo ci, ResourceManager rm)
        {
            colFiltraGIS col = new colFiltraGIS(idLayerType, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            col.Insert(0, new clsFiltraGIS());
            cmbFiltri.DataSource = col;
            cmbFiltri.ValueField = "Id";
            cmbFiltri.TextField = rm.GetString("labelKodi", ci); 
            cmbFiltri.DataBind();
            pnlfiltri.Update();
        }

        private void fshifilter()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (cmbFiltri.SelectedIndex == -1)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiNotFilter", ci), pnlMesazhi);
                return;
            }
            string idLayerType = cmbLayers.Value as string;
            if (idLayerType == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiLayerEkzist", ci), pnlMesazhi);
                return;
            }

            int idfiltri = int.Parse(cmbFiltri.Value.ToString());
            clsFiltraGIS filtri = new clsFiltraGIS();

            filtri.Id = idfiltri;
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.clsMesazh mesazh = filtri.fshi();

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgGISKerkimiRuajFilter", ci), pnlMesazhi);

                cmbFiltri.Text = "";
                mbushComboFiltra(idLayerType, ci, rm);
                gvLupaKerko.FilterExpression = "";
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        private void ruajfilter()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (cmbFiltri.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiAskFilterName", ci), pnlMesazhi);
                return;
            }
            if (cmbLayers.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiAskLayerType", ci), pnlMesazhi);
                return;
            }

            string idLayerType = cmbLayers.Value as string;
            if (idLayerType == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiLayerEkzist", ci), pnlMesazhi);
                return;
            }

            clsFiltraGIS filtri = new clsFiltraGIS(0, cmbFiltri.Text, idLayerType, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), 1, gvLupaKerko.FilterExpression, gvLupaKerko.FilterExpression);
            DbCore.clsMesazh mesazh = filtri.ruaj();
            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgGISKerkimiRuajFilter", ci), pnlMesazhi);

                mbushComboFiltra(idLayerType, ci, rm);
                cmbFiltri.Text = "";
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        protected void btnRuaj_Click(object sender, EventArgs e)
        {
            ruajfilter();
        }

        protected void btnFshi_Click(object sender, EventArgs e)
        {
            fshifilter();
        }
    }
}