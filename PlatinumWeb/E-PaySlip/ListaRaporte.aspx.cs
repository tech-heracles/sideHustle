using DbCore;
using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb.E_PaySlip
{
    public partial class ListaRaporte : MyPageBase
    {
        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (!DbCore.mySessionObjects.isLogedIn(Session) || periudha == null) {
                DbCore.clsFunksione.Logout(Session, true, true, false, DbCore.IMBUtils.Paths.loginPathEpaySlip, "FaqePaautorizuar");
                return;
            }

            if (!mySessionObjects.MerrNgaSession<bool>(Session, "EPaySlipPinAuthentification"))
            {
                Response.Redirect("OneTimePinForm.aspx");
                return;
            }

            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var ndermarrje = new clsNdermarrje(idNdermarrje);
            hfState.Set("NdermarrjeKodi", ndermarrje.NdermarrjeKodi);
                 

            
            if (periudha != null)
            {
                dtdoknga.Set("dtdoknga", periudha.FillimiPeriudha.ToString());
                dtdokderi.Set("dtdokderi", periudha.MbarimiPeriudha.ToString());
            }
            
            int idGjuha = mySessionObjects.ktheGjuhe(Session);
            hfState.Set("idGjuha", idGjuha);
            System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            vendosEmraLabela(ci, rm);
            
            

            

        }
        protected void vendosEmraLabela(CultureInfo ci, ResourceManager rm)
        {

            //  dalje.Text = "Log out";
         //   lblLista.Text = rm.GetString("lblListaRaporteve", ci);
            dalje.InnerText = rm.GetString("lblDalje", ci);
            hfState.Set("NdryshoFjalekalim", rm.GetString("lblNdryshoFjalekalim", ci));
            hfState.Set("lblListaRaporteve", rm.GetString("lblListaRaporteve", ci));
        }
   
    }
}