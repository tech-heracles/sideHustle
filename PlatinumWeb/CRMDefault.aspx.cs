using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore;
using DbCore.DbAdmin;

namespace PlatinumWeb
{
    public partial class CRMDefault : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idVitNdermarrje = mySessionObjects.ktheIdVitNdermarrje(Session);
            hfState.Set("idPerdoruesi", idPerdoruesi);
            hfState.Set("idNdermarrje", idNdermarrje);
            hfState.Set("idVitNdermarrje", idVitNdermarrje);
            if (!IsPostBack)
            {
                lblUserEmri.Text = mySessionObjects.ktheEmerPerdorues(Session);
                var licenca = new clsLicenca();
                licenca.mbushLicencen(IdPerdoruesi);
                hfState.Set("googleAnalytics", licenca.GoogleAnalytics);
                hfState.Set("googleAnalyticsTrackingId", licenca.GoogleAnalyticsTrackingId);
            }
        }
    }
}