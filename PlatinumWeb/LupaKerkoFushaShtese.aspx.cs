using DbCore;
using DbCore.DbAdmin;
using DbCore.DbGIS;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKerkoFushaShtese : MyPageBase
    {
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idKonfigambjenti;
        private string kodLlojModeliFushaShtese;
        private int subjektFushaShtese;
        const string emerKomponente = "LupaKerkoFushaShtese.aspx";
        System.Globalization.CultureInfo ci => DbCore.mySessionObjects.ktheCultureInfo(Session);



        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];



            if (!IsPostBack)
             {

                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                kodLlojModeliFushaShtese = Request.QueryString["kodLlojModeliFushaShtese"]?.ToString();
                subjektFushaShtese = Convert.ToInt32(Request.QueryString["subjekti"]?.ToString());
                hfState["idGjuha"] = idGjuha;
                hfState["idNdermarrje"] = idNdermarrje;
                hfState["idPerdoruesi"] = idPerdoruesi;
                hfState["idViti"] = idViti;
                hfState["kodLlojModeliFushaShtese"] = kodLlojModeliFushaShtese;
                hfState["subjektFushaShtese"] = subjektFushaShtese;
                KonfiguroVleraFillestare(idNdermarrje, idPerdoruesi, idGjuha, idViti);
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idViti = (int)hfState["idViti"];
                kodLlojModeliFushaShtese = (string)hfState["kodLlojModeliFushaShtese"];
                subjektFushaShtese = (int)hfState["subjektFushaShtese"];
            }

            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);

            PerkthimeNgaResourceManager();
            mbushComboFiltra();
            if (!(Request.Params["__CALLBACKPARAM"] != null && Request.Params["__CALLBACKPARAM"].Contains("LayerChanged")))
                KonfiguroDataSourceGride(false, false);
            gvLupaKerko.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfigambjenti, emerKomponente, 3048, "", rm, ci);
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci">Merr culture info perkatese</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void PerkthimeNgaResourceManager()
        {
            btnKerko.Text = rm.GetString("ReportToolbarButtonSearch", ci);
            gvLupaKerko.SettingsText.EmptyDataRow = rm.GetString("lblNukKaTeDhena", ci);
            gvLupaKerko.SettingsText.FilterBarCreateFilter = rm.GetString("grideGISKerkimCreateFilter", ci);
            gvLupaKerko.SettingsText.FilterBarClear = rm.GetString("grideGISKerkimClearFilter", ci);
            cmbGrupo.Text = rm.GetString("labelLupaKerkoFushaShteseGrupo", ci);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerKomponente, this, MenuInfo, null, null, true, false, false, false, Request.QueryString["theme"] != null && Request.QueryString["theme"] == "Moderno" ? true : false);
         aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);

        }

        private void KonfiguroVleraFillestare(int idNdermarrje, int idPerdorues, int idGjuha, int idVitNdermarrje)
        {
            mbushComboModeli(idPerdoruesi, idNdermarrje);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallbackPaButon(cmbLayers);
        }

        public void mbushComboModeli(int idPerdoruesi, int idNdermarrje)
        {//mbush kombon e modelit me te dhena nga databasa            

            var colModeli = new colModeletFushaShtese(kodLlojModeliFushaShtese, idNdermarrje, idPerdoruesi);
            cmbLayers.DataSource = colModeli;
            cmbLayers.TextField = "KodiModeliFushashtese";
            cmbLayers.ValueField = "IdModeliFushaShtese";
            cmbLayers.DataBind();

        }
        private void mbushGriden(int idPerdoruesi, int idNermarrje, int idModeli, string filter, string[] kolonat)
        {
            gvLupaKerko.Selection.UnselectAll();
            DataTable data = colVleraFushaShtese.MerrVleraFushaShteseSipasModelitDheFushavePerGride(idNdermarrje, idModeli, string.Join(",",kolonat), filter, idGjuha, subjektFushaShtese);
            hfGrida["filterExpression"] = gvLupaKerko.FilterExpression;
            gvLupaKerko.Columns.Clear();
            gvLupaKerko.AutoGenerateColumns = true;

            gvLupaKerko.DataSource = data;
            gvLupaKerko.DataBind();

            mySessionObjects.ruajGrideNeSessionLupa(Session, data, false);
        }

        private void konfiguroGride(ASPxGridView grida, string kolonat)
        {
            string[] cols = kolonat.Replace("[", "").Replace("]", "").Split(',');

            for (int i = 0; i < cols.Length; i++)
            {
                if (grida.Columns[cols[i]] != null)
                    grida.Columns[cols[i]].Visible = false;
            }
            double gjeresi = 100 / grida.VisibleColumns.Count;
            for (int i = 0; i < grida.VisibleColumns.Count; i++)
            {
                grida.VisibleColumns[i].Width = Unit.Percentage(gjeresi);
            }
            if (grida.Columns["#"] != null)
                grida.Columns["#"].Width = Unit.Pixel(17);
            grida.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            gvLupaKerko.SettingsBehavior.AllowSelectByRowClick = true;
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKerko, cols[0], true, false);
        }

        protected void gvLupaKerko_DataBound(object sender, EventArgs e)
        {
            if (gvLupaKerko.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                gvLupaKerko.Columns.Add(check);
                check.VisibleIndex = 0;
                check.Width = Unit.Pixel(17);

                check.ShowSelectCheckbox = true;
            }
            //if (gvLupaKerko.Columns["Status"] != null)
            //    shtoKolonStatusi();
        }

        protected void gvLupaKerko_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            KonfiguroDataSourceGride(false, false);
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
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
            bool ndryshoiLayeri = false;
            bool ndryshoiGrupimi = false;

            if (e.Parameter.Contains("LayerChanged"))
            {
                ndryshoiLayeri = true;
                gvLupaKerko.FilterExpression = "";
                mySessionObjects.ruajObjectNeSesion(Session, null, "KolonaGride");
            }
            else if (e.Parameter.Contains("GrupoChanged"))
                ndryshoiGrupimi = true;
            else if (e.Parameter.Contains("Filter"))
            {
                gvLupaKerko.FilterExpression = colFiltraGIS.MerrFilterExpressionSipasID(int.Parse(e.Parameter.Split(';')[1]));
            }
            else if (Convert.ToInt32(cmbFiltri.Value) != 0)
            {
                gvLupaKerko.FilterExpression = colFiltraGIS.MerrFilterExpressionSipasID(Convert.ToInt32(cmbFiltri.Value));
            }
            else
            {
                gvLupaKerko.FilterExpression = e.Parameter;
            }
            KonfiguroDataSourceGride(ndryshoiLayeri, ndryshoiGrupimi);
        }

        private dynamic merrVlerenNgaCombo()
        {
            //vlera e combos eshte ne formatin IdLayerType_IdLayer
            //per te shmangur duplikimet eshte bere qe idLayer te jete 0 per te gjithe layerat jo baze
            int idLayer = -1;
            string[] vlera = cmbLayers.Value.ToString().Split('_');
            if (vlera[0] == "3")//layer baze
                idLayer = int.Parse(vlera[1]);
            else
                idLayer = int.Parse(vlera[0]);//merrr tipin e layerit
            return new
            {
                IdLayeri = idLayer,
                KodLayeri = cmbLayers.Text,
                IdLayerType = int.Parse(vlera[0])
            };
        }

        private void KonfiguroDataSourceGride(bool ndryshoiLayeri, bool ndryshoiGrupimi)
        {
            if (cmbLayers.SelectedItem != null)
            {
                var idModeli = Convert.ToInt32(cmbLayers.Value);
                idNdermarrje = (int)hfState["idNdermarrje"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                string[] kolonat = null;

                string filterExpression = hfGrida.Contains("filterExpression") ? Convert.ToString(hfGrida["filterExpression"]) : "";

                if (!filterExpression.Equals(gvLupaKerko.FilterExpression) || (string.IsNullOrEmpty(filterExpression) && string.IsNullOrEmpty(gvLupaKerko.FilterExpression) && ndryshoiLayeri) || ndryshoiGrupimi)
                {
                    kolonat = clsModeliFushaShtese.MerrFushaModeli(idModeli, idGjuha);
                    mySessionObjects.RuajNeSession(Session, kolonat, $"{emerKomponente}_KolonaGride");
                    mbushGriden(idPerdoruesi, idNdermarrje, idModeli, krijoFilterString(), kolonat);
                }
                else
                {
                    object gridDataSource = null;
                    mySessionObjects.merrGrideNgaSessioniLupa(Session, out gridDataSource);
                    if (gridDataSource != null)
                    {
                        gvLupaKerko.DataSource = gridDataSource;
                        gvLupaKerko.DataBind();

                        kolonat = mySessionObjects.MerrNgaSession<string[]>(Session, $"{emerKomponente}_KolonaGride") ?? clsModeliFushaShtese.MerrFushaModeli(idModeli, idGjuha);
                        konfiguroGride(gvLupaKerko, kolonat[1]);
                    }
                }
                if (gvLupaKerko.Columns.Count > 0)
                {
                    konfiguroGride(gvLupaKerko, kolonat[1]);
                }
            }
        }

        private void shtoKolonStatusi()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


            GridViewDataComboBoxColumn newCol = new GridViewDataComboBoxColumn();

            var colStatusi = gvLupaKerko.Columns["Status"];
            //rasti per layerat qe kane statuse,perjashtohen layarat pa status
            if (colStatusi.Visible)
            {
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisTeGjitha", ci), null);       //Të gjitha
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisSaktësuar", ci), 1);         //E saktësuar
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisPasaktësuar", ci), 2);       //E pasaktësuar
                newCol.PropertiesComboBox.Items.Add(rm.GetString("statusGisPlanifikuar", ci), 3);       //E planifikuar

                newCol.PropertiesComboBox.ValueType = typeof(Int32);
                newCol.FieldName = "Status";
                newCol.Caption = "Status";
                newCol.VisibleIndex = colStatusi.VisibleIndex;

                gvLupaKerko.Columns.Remove(colStatusi);
                gvLupaKerko.Columns.Add(newCol);
            }
        }

        private string krijoFilterString()
        {
            filterControll.FilterExpression = gvLupaKerko.FilterExpression;
            string expression = filterControll.GetFilterExpressionForMsSql(); ;
            string filterPerSql = "";

            if (string.IsNullOrWhiteSpace(expression))
                filterPerSql = string.Empty;
            else
                filterPerSql = " AND " + expression;
            return filterPerSql.Replace("\"", "");
        }

        protected void gvLupaKerko_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
        }

        #region FILTRI PERSONALIZUAR

        private void mbushComboFiltra()
        {
            if (cmbLayers.SelectedItem != null)
            {
                colFiltraGIS col = new colFiltraGIS(Convert.ToString(cmbLayers.Value), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                col.Insert(0, new clsFiltraGIS());
                cmbFiltri.DataSource = col;
                cmbFiltri.ValueField = "Id";
                cmbFiltri.TextField = "Kodi";
                cmbFiltri.DataBind();
                pnlfiltri.Update();
            }
        }

        private void fshifilter()
        {


            if (cmbFiltri.SelectedIndex == -1)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiNotFilter", ci), pnlMesazhi); //Nuk keni zgjedhur asnje filter
                return;
            }
            int idLayerType = 0;
            int.TryParse(cmbLayers.Value?.ToString(), out idLayerType);
            if (idLayerType == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiLayerEkzist", ci), pnlMesazhi); //"Ky tip layeri nuk ekziston!"
                return;
            }

            int idfiltri = int.Parse(cmbFiltri.Value.ToString());
            clsFiltraGIS filtri = new clsFiltraGIS();

            filtri.Id = idfiltri;
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.clsMesazh mesazh = filtri.fshi();

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgGISKerkimiRuajFilter", ci), pnlMesazhi); //Filtri u ruajt me sukses!"

                cmbFiltri.Text = "";
                mbushComboFiltra();
                gvLupaKerko.FilterExpression = "";
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        private void ruajfilter()
        {


            if (cmbFiltri.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiAskFilterName", ci), pnlMesazhi); //Jepni emrin e filtrit
                return;
            }
            if (cmbLayers.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiAskLayerType", ci), pnlMesazhi); //Jepni tipin  e layerit
                return;
            }

            string idLayerType = cmbLayers.Value as string;
            if (idLayerType == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGISKerkimiLayerEkzist", ci), pnlMesazhi); //"Ky tip layeri nuk ekziston!"
                return;
            }

            clsFiltraGIS filtri = new clsFiltraGIS(0, cmbFiltri.Text, idLayerType, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), 1, gvLupaKerko.FilterExpression, gvLupaKerko.FilterExpression);
            DbCore.clsMesazh mesazh = filtri.ruaj();

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgGISKerkimiRuajFilter", ci), pnlMesazhi); //Filtri u ruajt me sukses!"

                mbushComboFiltra();
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

        #endregion FILTRI PERSONALIZUAR
    }
}