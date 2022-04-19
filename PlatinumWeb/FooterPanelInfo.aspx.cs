using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DbCore.DbAdmin;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Xml.Serialization;
using System.Globalization;
using System.Resources;
using System.Reflection;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class FooterPanelInfo : MyPageBase
    {
        //periudha kontabel merret nga sesioni
        clsPeriudhaKontabel periudha = new clsPeriudhaKontabel();
  

        protected void Page_Load(object sender, EventArgs e)
        {
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo ci;
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!IsPostBack)
            {
                EmrateLabelave(DbCore.mySessionObjects.ktheCultureInfo(Session));
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                periudha = new clsPeriudhaKontabel(periudha.IdPeriudha, idgjuha);
                if (periudha != null)
                { 
                           btnPeriudha.Text = periudha.EmerPeriudha + " (" + periudha.FillimiPeriudha.Year + ")";
                        if (periudha.Ekycur)
                            btnPeriudha.ForeColor = Color.Gray;
            }
            if (DbCore.mySessionObjects.ekzistonIdNdermarrje(Session))
            {
                clsNdermarrje oNdermarrje = new DbCore.DbAdmin.clsNdermarrje();
                oNdermarrje.IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                oNdermarrje = oNdermarrje.merrSipasID();
                txtNdermarrja.Text = oNdermarrje.NdermarrjeKodi;
                lblEmriNdermarrje.Text = oNdermarrje.NdermarrjePershkrimi;
            }

        }
    }



        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
          private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            lblNdermarrje.Text = rm.GetString("labelFooterNdermarrja", ci);
            lblPeriudha.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
        }
       
    }
}