using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using Web.Framework.Templates;

namespace PlatinumWeb.Templates
{
    public class MyTitlePanelTemplateMeRefresh : MyBaseTitlePanelTemplate
    {
        private ASPxButton _btnRefresh;
        bool ekzekutoCallbackButonRefresh;
        bool meTopRows;
        public TopRowsControl TopRowsControl { get; set; }


        public MyTitlePanelTemplateMeRefresh(string emriGrides,Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idkonfigAmbjenti, int idPerdoruesi, 
            int idNdermarrje, int idViti, int idGjuha, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo cultureInfo, 
            bool meExpandCollapse, GridViewExportedRowType exportType, bool vetemSelectButtons = false, bool ekzekutoCallbackButonRefresh=true, bool meTopRows = false) : 
            base(emriGrides,page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, cultureInfo, meExpandCollapse, exportType, vetemSelectButtons)
        {
            this.ekzekutoCallbackButonRefresh= ekzekutoCallbackButonRefresh;
            this.meTopRows = meTopRows;
        }


        /// <summary>
        /// inicializimi i template-it
        /// </summary>
        /// <param name="container"></param>
        public override void InstantiateIn(Control container)
        {
            Grid = ((DevExpress.Web.GridViewTitleTemplateContainer)container).Grid;

            var tbl = KrijoTable(1, 13);
            if (meTopRows)
            {
                if (!CurrentPage.IsPostBack)
                    TopRowsControl = new TopRowsControl
                    {
                        TopRows = (gridaKoka.TopRows == 0 ? -1 : gridaKoka.TopRows),//nese ka vleren 0 ath ska konfig te zgjedhur
                        MenyreFiltrimi = gridaKoka.MenyreFiltrimi,
                        MePeriudhe = false
                    };
                else
                    TopRowsControl = CurrentPage.MerrTopRowsControl(hfState);

                Grid.SettingsBehavior.FilterRowMode = (GridViewFilterRowMode)TopRowsControl.MenyreFiltrimi;
                MbushTableMeControle(tbl, BaseControls(), KontrolliRefresh(), TopRowsControl.KontrolletETopRows(null, hfState, Grid));
                btnRuajKolonat.Click += RuajKonfigurimeShtesePerGriden;
                hfState.Set(TopRowsControl.KeyFieldTopRows, JsonConvert.SerializeObject(TopRowsControl));
            }
            else
                MbushTableMeControle(tbl, BaseControls(), KontrolliRefresh());
            container.Controls.Add(tbl);
        }

        #region INICIALIZIM KONTROLLESH
  
        protected void InicializoButoninRefresh()
        {
            _btnRefresh = new ASPxButton();

            _btnRefresh.ID = "butoniRefresh";
            _btnRefresh.ClientInstanceName = "butoniRefresh";
            _btnRefresh.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/butoni_refresh.png";//ndryshoje
            _btnRefresh.ToolTip = rm.GetString("btnRefresh", cultinf);
            _btnRefresh.AutoPostBack = false;
            _btnRefresh.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/butoni_refresh_W.png";//ndryshoje
            _btnRefresh.Image.Height = 16;
            _btnRefresh.UseSubmitBehavior = false;
            _btnRefresh.ClientSideEvents.Click = BtnRefreshClientSideClick();
            if (ekzekutoCallbackButonRefresh)
            {
                Grid.CustomCallback += grid_RegDok_CustomCallback;
                Grid.AfterPerformCallback += grid_RegDok_AfterPerformCallback;
            }
         
        }
        #endregion

        protected virtual List<Control> KontrolliRefresh()
        {
            var Controls = new List<Control>();
            InicializoButoninRefresh();
  
            Controls.Add(_btnRefresh);
            return Controls;
           
        }

        #region EVENTET CLIENTSIDE
        private string BtnRefreshClientSideClick()
        {
            return $@"function(s,e)
          {{
             
             var modifiedAutoFilter ={Grid.ClientInstanceName}.filterHelper.GetChangedAutoFilterValues();
             if (Object.keys(modifiedAutoFilter).length > 0) {Grid.ClientInstanceName}.filterHelper.ApplyMultiColumnAutoFilter();
             else {Grid.ClientInstanceName}.PerformCallback('{GridUtil.refresh}'); 
          }}";
        }
        #endregion
        #region Eventet SERVER SIDE

        private void RuajKonfigurimeShtesePerGriden(object sender, EventArgs arg)
        {
            if (gridaKoka.MenyreFiltrimi != TopRowsControl.MenyreFiltrimi || gridaKoka.TopRows != TopRowsControl.TopRows)
            {
                //ka nevoj per te ruajtje
                gridaKoka.MenyreFiltrimi = TopRowsControl.MenyreFiltrimi;
                gridaKoka.TopRows = TopRowsControl.TopRows;
                gridaKoka.Modifiko();
            }
        }

        protected void grid_RegDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e){
        }
        protected void grid_RegDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.Args.Length == 1 && e.Args[0] == GridUtil.refresh && CurrentPage.EshteCallbackuIm(Grid.ID))
                ((MyPageBase)CurrentPage).MbushGrideNgaDb();
        }
        #endregion

    }
}