using System;
using DbCore.DbShare;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Raportet : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StiliPerRaportet();
            if (!IsPostBack)
            {
                EmrateLabelave();
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                }

                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                }

                var idModuli = int.Parse(Request.QueryString["idmod"]);
                
                var dt = clsRaporti.KtheListRaportesh(IdNdermarrja, IdPerdoruesi, IdGjuha, IdViti, idModuli);
                hfState.Add("listaRap", Newtonsoft.Json.JsonConvert.SerializeObject(dt));
                var stringKonfig = clsRaporti.KtheListKonfigRaportesh(IdPerdoruesi, IdNdermarrja, idModuli);
                hfState.Add("konfigRap", string.IsNullOrEmpty(stringKonfig) ? "" : stringKonfig);
                hfState.Set("idModuli", idModuli);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idGjuha", IdGjuha);
            }
        }

        protected void btnHelp_Init(object sender, EventArgs e)
        {
            var kompRap = new clsKomponente("RaportetAllNew.aspx");
            btnHelp.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompRap.UrlHelpSuffix).Item1 + "\');}";
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateLabelave()
        {
            btnHelp.ToolTip = MessagesResource.Messages["tooltipBtnHelp"];
            btnCollapseAll.ToolTip = MessagesResource.Messages["tooltipBtnCollapseAll"];
            btnExpandAll.ToolTip = MessagesResource.Messages["tooltipBtnExpandAll"];
        }

        private void StiliPerRaportet()
        {
            if (Convert.ToInt32(Request.QueryString["idTheme"]) == 1941)
            {
                menuHomePage.Attributes.Add("class", "Box_MetropolisBlue");
            }
        }
    }
}