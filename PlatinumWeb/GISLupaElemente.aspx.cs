using DbCore;
using DbCore.DbGIS;
using DbCore.DbShare;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class GISLupaElemente : MyPageBase
    {
    

        private readonly string emerKomponente = "GISLupaElemente.aspx";
        private readonly string llojFushe = "GIS";
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idGjuha;
        private int gid;
        private int idkonfigurimi;
        private int idVitNdermarrje;

        protected void Page_Load(object sender, EventArgs e)
        {

            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int.TryParse(Request.QueryString["gid"]?.ToString(), out gid);

            if (!IsPostBack)
            {
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                idVitNdermarrje = mySessionObjects.ktheIdVitNdermarrje(Session);
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idVitNdermarrje, emerKomponente);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idVitNdermarrje", idVitNdermarrje);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfigurVleraFillestare(idPerdoruesi, idNdermarrje, idGjuha, rm, cultinf);
            }
            else
            {
                idGjuha = (int)hfState.Get("idGjuha");
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idVitNdermarrje = (int)hfState.Get("idVitNdermarrje");
            }

            percaktoTemplateMenu(ASPxMenu1, idVitNdermarrje, idPerdoruesi, idNdermarrje);
        }

        private void konfigurVleraFillestare(int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo cultinf)
        {
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 151, "GisEl", rm, cultinf, idGjuha);
            idkonfigurimi = clsFunksioneGIS.ktheIdKonfigurimiNgaGisElement(gid);
            cmbKonfigurimi.SelectedIndex = cmbKonfigurimi.Items.FindByValue(idkonfigurimi.ToString()).Index;
            ucFushatShtese.KonfiguroVleraFillestare(idNdermarrje, idPerdoruesi, idGjuha, emerKomponente, llojFushe, gid, idkonfigurimi);
            ucFushatShtese.percaktoTemplateFushash();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("fushatShteseTab", cultinf);

        }
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ruaj":
                    RuajFushaShtesePerElemtinGis();
                    break;
            }
        }

        private void RuajFushaShtesePerElemtinGis()
        {

            try
            {
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idVitNdermarrje, emerKomponente);
                if (!tedrejtaInfo.DMod)
                    throw new MyException(MessagesResource.Messages["msgNukKeniTeDrejta"]);
                var fushatShtese = ucFushatShtese.merrFushatShtese();


                var elementiGis = new clsElementGIS
                {
                    FushatShtese = fushatShtese,
                    Gid = gid
                };
                var mesazh = elementiGis.Ruaj();
                if (mesazh)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Vlerat u ruajten me sukses!", pnlMesazhi); hfStatusi.Value = "true";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Deshtoi ruajtja e vlerave!", pnlMesazhi); hfStatusi.Value = "false";
                }
            }
            catch (MyException myex)
            {
                hfStatusi.Value = "false";
                ImbLogger.Error(myex);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myex.Message, pnlMesazhi);
            }
            catch (Exception ex)
            {
                hfStatusi.Value = "false";
                ImbLogger.Error(ex);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Deshtoi ruajtja e vlerave!", pnlMesazhi);

            }
        }

        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerKomponente, this, MenuInfo, null, null, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }
    }
}