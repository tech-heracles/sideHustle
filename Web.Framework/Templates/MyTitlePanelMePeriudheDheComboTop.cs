
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
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using Web.Framework.Templates;

namespace PlatinumWeb.Templates
{
    public class MyTitlePanelMePeriudheDheComboTop : MyTitlePanelTemplateMePeriudhe
    {
        private ASPxLabel _lblTopRows;
        private ASPxComboBox _cmbTopRows;
        private ASPxButton _btnKerko;
        private ASPxComboBox _cmbMenyraFiltrimi;
        private bool meComboAutomatike = true;
        public MyTitlePanelMePeriudheDheComboTop(string emriGrides, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idkonfigAmbjenti, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo cultureInfo, bool meExpandCollapse, GridViewExportedRowType exportType, bool meComboAutomatike, bool periudheGjitheVitet) : base(emriGrides, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, cultureInfo, meExpandCollapse, exportType, periudheGjitheVitet, false)
        {
            this.meComboAutomatike = meComboAutomatike;
        }

        public override void InstantiateIn(Control container)
        {
            Grid = ((DevExpress.Web.GridViewTitleTemplateContainer)container).Grid;

            Grid.SettingsBehavior.FilterRowMode = (GridViewFilterRowMode)Periudhat.TopRowsControl.MenyreFiltrimi;

            var tbl = KrijoTable(1, 17);
            if(meComboAutomatike)
                MbushTableMeControle(tbl, BaseControls(), KontrolletEPeriudhes(Periudhat.TopRowsControl.MenyreFiltrimi == 0), Periudhat.TopRowsControl.KontrolletETopRows(RadDtDok, hfState, Grid));
            else
                MbushTableMeControle(tbl, BaseControls(), KontrolletEPeriudhes(Periudhat.TopRowsControl.MenyreFiltrimi == 0));
            if(meComboAutomatike)
                btnRuajKolonat.Click += RuajKonfigurimeShtesePerGriden;
            container.Controls.Add(tbl);
        }

        private void RuajKonfigurimeShtesePerGriden(object sender, EventArgs arg)
        {
            if (gridaKoka.MenyreFiltrimi != Periudhat.TopRowsControl.MenyreFiltrimi || gridaKoka.TopRows != Periudhat.TopRowsControl.TopRows)
            {
                //ka nevoj per te ruajtje
                gridaKoka.MenyreFiltrimi = Periudhat.TopRowsControl.MenyreFiltrimi;
                gridaKoka.TopRows = Periudhat.TopRowsControl.TopRows;
                gridaKoka.Modifiko();
            }
        }
    }
}