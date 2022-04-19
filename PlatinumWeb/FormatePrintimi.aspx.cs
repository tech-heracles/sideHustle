using DbCore;
using System;
using PlatinumWeb.ApplicationUtils.Pages;
using DevExpress.XtraReports.Web;

namespace PlatinumWeb
{
    public partial class FormatePrintimi : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HfState.Count == 0)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }

                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                }

                HfState.Set("idPerdoruesi", IdPerdoruesi);
                HfState.Set("idGjuha", IdGjuha);
                HfState.Set("idNdermarrje", IdNdermarrja);
                HfState.Set("idViti", IdViti);
                HfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                HfState.Set("Meme", Meme);
            }
        }

        protected void rvRaporti_Unload(object sender, EventArgs e)
        {
            ((ReportViewer)sender).Report = null;
        }
    }
}